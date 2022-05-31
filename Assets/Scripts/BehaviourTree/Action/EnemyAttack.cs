using System.Collections;
using UnityEngine;
using TheKiwiCoder;

namespace BT
{
    public class EnemyAttack : ActionNode
    {

        public string attackClipName;
        public float damage = 2f;
        public float damageInterval = .1f;
        public float cd = 3f;

        private float animationLength;
        private float startTime;

        private float lastCastTime;

        private bool inCD;

        protected override void OnStart()
        {
            inCD = InCD();
            if (!inCD)
            {
                context.enemy.SetHazardDamageInfo(damage, damageInterval);
                context.animator.ResetAllTrigger();
                context.animator.Play(attackClipName);
                // 需要限制状态的名称和动画名称一样
                animationLength = context.animator.GetAnimationClipLength(attackClipName);
                startTime = Time.time;
            }
        }

        protected override void OnStop()
        {
            lastCastTime = startTime;
        }

        protected override State OnUpdate()
        {
            if (InCD())
                return State.Failure;
            if (Time.time >= startTime + animationLength)
                return State.Success;
            return State.Running;
        }

        private bool InCD()
        {
            return Time.time < lastCastTime + cd;
        }
    }
}