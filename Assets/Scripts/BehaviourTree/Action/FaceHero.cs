using System.Collections;
using UnityEngine;
using TheKiwiCoder;

namespace Assets.Scripts.BehaviourTree.Action
{
    public class FaceHero : ActionNode
    {
        protected override void OnStart()
        {

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