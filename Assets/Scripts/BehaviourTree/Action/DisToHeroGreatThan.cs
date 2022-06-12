using TheKiwiCoder;
using UnityEngine;

namespace BT
{
    public class DisToHeroGreatThan : ActionNode
    {
        public float checkDistance = 10f;
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
            if (sqrDis > checkDistance * checkDistance)
                return State.Success;
            return State.Failure;
        }
    }
}