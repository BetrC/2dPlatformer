using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AnimatorSetTrigger : ActionNode
{
    public string triggerName;

    public bool resetAllTrigger = true;

    protected override void OnStart() {

    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {

        if (resetAllTrigger)
        {
            context.animator.ResetAllTrigger();
        }

        if (triggerName != "")
        {
            context.animator.SetTrigger(triggerName);
        }
        return State.Success;
    }
}
