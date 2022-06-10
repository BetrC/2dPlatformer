using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
namespace BT
{
    /// <summary>
    /// 检测前方有无道路
    /// </summary>
    public class RoadDetect : ActionNode
    {
        
        public Vector2 ledgeCheckPositionOffset;
        public float ledgeCheckDistance;

        public LayerMask whatIsGround;
        

        //** runtime only
        protected Vector2 LedgeCheckPosition => (Vector2)context.transform.position + new Vector2(ledgeCheckPositionOffset.x * context.movement.FacingDirection, ledgeCheckPositionOffset.y);
        

        public bool LedgeDetected => Physics2D.Raycast(LedgeCheckPosition,
                                                       Vector2.down,
                                                       ledgeCheckDistance, whatIsGround);

        protected override void OnStart()
        {
            if (ledgeCheckPositionOffset == Vector2.zero)
                ledgeCheckPositionOffset.Set(context.boxCollider.size.x / 2 - context.boxCollider.offset.x + .8f, 0);
            if (ledgeCheckDistance == 0)
                ledgeCheckDistance = context.boxCollider.size.y / 2 - context.boxCollider.offset.y + .8f;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            return LedgeDetected ? State.Success : State.Failure;
        }

        public override void DrawGizmos(Transform transform)
        {
            Vector2 pos = transform.position;
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(pos + new Vector2(ledgeCheckPositionOffset.x * transform.right.x, ledgeCheckPositionOffset.y),
                            pos + new Vector2(ledgeCheckPositionOffset.x * transform.right.x, ledgeCheckPositionOffset.y) + Vector2.down * ledgeCheckDistance);
        }
    }
}