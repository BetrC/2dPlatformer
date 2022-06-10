using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace BT
{
    public enum DetectType
    {
        StrightLine,
        Circle,
    }

    public class PlayerDetect : ActionNode
    {

        public LayerMask whatIsPlayer;

        public DetectType detectType;

        public Vector2 detectOffset;
        public float detectDistance = 4f;

        private Vector2 start => (Vector2)context.transform.position + detectOffset;
        
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool hit = false;
            if (detectType == DetectType.StrightLine)
            {
                hit = Physics2D.Raycast(start, context.transform.right, detectDistance, whatIsPlayer);
            }
            else if (detectType == DetectType.Circle)
            {
                hit = Physics2D.OverlapCircle(start, detectDistance, whatIsPlayer);
            }
            return hit ? State.Success : State.Failure;
        }

        public override void DrawGizmos(Transform transform)
        {
            Gizmos.color = Color.yellow;
            Vector2 begin = (Vector2)transform.position + detectOffset;
            if (detectType == DetectType.StrightLine)
            {
                Gizmos.DrawLine(begin, begin + detectDistance * (Vector2)transform.right);
            }
            else if (detectType == DetectType.Circle)
            {
                Gizmos.DrawWireSphere(begin, detectDistance);
            }
        }
    }
}