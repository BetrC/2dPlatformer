using System.Collections;
using UnityEngine;
using TheKiwiCoder;

namespace BT
{
    public class FaceHero : ActionNode
    {

        private Hero hero;

        protected override void OnStart()
        {
            if (hero == null)
                hero = GameObject.FindObjectOfType<Hero>();
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            float xDiff = hero.transform.position.x - context.transform.position.x;
            xDiff = xDiff >= 0 ? 1 : -1;
            context.movement.CheckFlip((int)xDiff);
            return State.Success;
        }
    }
}