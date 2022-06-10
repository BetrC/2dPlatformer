using TheKiwiCoder;
using UnityEngine;

namespace BT
{
    public class Teleport : ActionNode
    {
        private Vector2 targetPosition;

        public float teleportMaxDistance = 10f;
        public float waitTime;

        public float teleCD = 5f;
        
        public LayerMask whatIsPlayerOrWall;

        private float startTime;
        private float lastTeleTime;
        private bool incd;

        protected override void OnStart()
        {
            incd = InCD();
            if (!incd)
            {
                context.triggerCollider.enabled = false;
                startTime = Time.time;
                FindTeleportTargetPosition();
                lastTeleTime = Time.time;
                context.animator.ResetAllTrigger();
                context.animator.SetTrigger("tele");
            }
        }

        protected override void OnStop()
        {
            context.triggerCollider.enabled = true;
        }

        protected override State OnUpdate()
        {
            if (incd)
                return State.Failure;
            
            if (Time.time > startTime + waitTime)
            {
                context.gameObject.transform.position = targetPosition;
                return State.Success;
            }
            return State.Running;
        }

        void FindTeleportTargetPosition()
        {
            RaycastHit2D hit = Physics2D.Raycast(context.transform.position, context.transform.right, teleportMaxDistance,
                whatIsPlayerOrWall);
            if (hit.collider)
                targetPosition = hit.point - (Vector2)context.transform.right;
            else
                targetPosition = context.transform.position + context.transform.right * teleportMaxDistance;
        }
        
        private bool InCD()
        {
            return Time.time < lastTeleTime + teleCD;
        }
    }
}