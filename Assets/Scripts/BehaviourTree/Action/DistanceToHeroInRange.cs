using TheKiwiCoder;
using UnityEngine;

namespace BT
{
    public class DistanceToHeroInRange : ActionNode
    {
        public float minDistance = 0f;
        public float maxDistance = 10f;
        
        private Hero hero;
        
        protected override void OnStart()
        {
            hero = GameManager.Instance.hero;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            
            float sqrDis = Vector2.SqrMagnitude(context.transform.position - hero.transform.position);
            if (minDistance * minDistance <= sqrDis && sqrDis <= maxDistance * maxDistance)
                return State.Success;
            return State.Failure;
        }

        public override void DrawGizmos(Transform transform)
        {
            base.DrawGizmos(transform);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minDistance);
            Gizmos.DrawWireSphere(transform.position, maxDistance);

        }
    }
}