using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace BT
{
    /// <summary>
    /// 检测前方是否有墙
    /// </summary>
    public class WallDetect : ActionNode
    {
        
        public Vector2 wallCheckPositionOffset;
        public float wallCheckDistance = 0f;

        public LayerMask whatIsGround;
        
        //** runtime only
        protected Vector2 WallCheckPosition => (Vector2)context.transform.position + new Vector2(wallCheckPositionOffset.x * context.movement.FacingDirection, wallCheckPositionOffset.y);
        
        public bool WallDetected => Physics2D.Raycast(WallCheckPosition,
            context.transform.right,
            wallCheckDistance, whatIsGround);
        
        protected override void OnStart()
        {
            if (wallCheckDistance == 0)
                wallCheckDistance = context.boxCollider.size.x / 2 - context.boxCollider.offset.x + .8f;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return WallDetected ? State.Success : State.Failure;
        }

        public override void DrawGizmos(Transform transform)
        {
            Vector2 pos = transform.position;
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(pos + new Vector2(wallCheckPositionOffset.x * transform.right.x, wallCheckPositionOffset.y),
                pos + new Vector2(wallCheckPositionOffset.x * transform.right.x, wallCheckPositionOffset.y) + (Vector2)transform.right * wallCheckDistance);
        }
    }   
}