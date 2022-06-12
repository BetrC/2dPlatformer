using System.Collections;
using UnityEngine;
using TheKiwiCoder;


namespace BT
{
    public class IsOnGround : ActionNode
    {

        private float groundCheckDistance;
        public LayerMask whatIsGround;

        protected override void OnStart()
        {
            groundCheckDistance = context.boxCollider.size.y / 2 - context.boxCollider.offset.y + .4f;
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

        public override void DrawGizmos(Transform transform)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

        }
    }
}