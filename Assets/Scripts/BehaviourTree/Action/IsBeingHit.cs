using System.Collections;
using UnityEngine;
using TheKiwiCoder;

namespace BT
{
    public class IsBeingHit : ActionNode
    {
        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            return blackboard.BeHit ? State.Success : State.Failure;
        }
    }
}