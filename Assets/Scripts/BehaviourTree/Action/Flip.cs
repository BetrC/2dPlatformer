using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Flip : ActionNode
{

    protected override void OnStart() {

    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        context.movement.Flip();
        return State.Success;
    }
}
