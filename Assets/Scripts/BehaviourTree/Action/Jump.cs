using TheKiwiCoder;
using UnityEngine;

namespace BT
{
    public class Jump : ActionNode
    {

        public Vector2 jumpVelocity;
        
        protected override void OnStart()
        {

            CLog.Log("jump");
            context.movement.SetFacingDirVelocityX(jumpVelocity.x);
            context.movement.SetVelocityY(jumpVelocity.y);

        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }
    }
}