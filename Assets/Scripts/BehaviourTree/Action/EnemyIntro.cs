using TheKiwiCoder;
using UnityEngine;
using UnityEngine.Serialization;

namespace BT
{
    // 入場
    public class EnemyIntro : ActionNode 
    {
        
        public string introClipName;
        private float animationLength;
        private float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
            context.animator.ResetAllTrigger();
            context.animator.Play(introClipName);
            animationLength = context.animator.GetAnimationClipLength(introClipName);
        }

        protected override void OnStop()
        {
            blackboard.Introed = true;
        }

        protected override State OnUpdate()
        {
            if (Time.time >= startTime + animationLength)
                return State.Success;
            return State.Running;
        }
    }
}