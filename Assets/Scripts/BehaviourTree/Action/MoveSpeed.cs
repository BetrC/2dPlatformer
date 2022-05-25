using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveSpeed : ActionNode
{
    public float speed = 4f;

    protected override void OnStart() {

    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        context.movement.SetFacingDirVelocityX(speed);
        return State.Success;
    }
}
