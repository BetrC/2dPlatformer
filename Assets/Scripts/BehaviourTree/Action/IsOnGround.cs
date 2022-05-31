using System.Collections;
using UnityEngine;
using TheKiwiCoder;


namespace BT
{
    public class IsOnGround : ActionNode
    {

        public float groundCheckDistance = 0.5f;
        public LayerMask whatIsGround;

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            bool onGround = Physics2D.Raycast(context.transform.position, Vector2.down, groundCheckDistance, whatIsGround);
            if (onGround)
                return State.Success;
            return State.Failure;
        }
    }
}