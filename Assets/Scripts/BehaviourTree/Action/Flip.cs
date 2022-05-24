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
        context.movement.CheckFlip(-1 * context.movement.FacingDirection);
        return State.Success;
    }
}
