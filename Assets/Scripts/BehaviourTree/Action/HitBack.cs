using System.Collections;
using UnityEngine;
using TheKiwiCoder;


namespace BT
{
    public class HitBack : ActionNode
    {

        public float HitBackTime = .3f;

        public bool HitBackDirZero = false;

        private float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
            if (!HitBackDirZero)
            {
                context.movement.SetVelocity(blackboard.HitBackVelocity);   
            }
        }

        protected override void OnStop()
        {
            blackboard.BeHit = false;
        }

        protected override State OnUpdate()
        {
            if (Time.time >= startTime + HitBackTime)
            {
                return State.Success;
            }
            return State.Running;
        }
    }
}