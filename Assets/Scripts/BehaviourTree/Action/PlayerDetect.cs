using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public enum DetectType
{
    StrightLine,
    Circle,
}

public class PlayerDetect : ActionNode
{

    public LayerMask whatIsPlayer;

    public DetectType detectType;

    public float detectDistance = 4f;

    protected override void OnStart() {

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        bool hit = false;
        if (detectType == DetectType.StrightLine)
        {
            hit = Physics2D.Raycast(context.transform.position, context.transform.right, detectDistance, whatIsPlayer);
        }
        else if (detectType == DetectType.Circle)
        {
            hit = Physics2D.OverlapCircle(context.transform.position, detectDistance, whatIsPlayer);
        }
        return hit ? State.Success : State.Failure;
    }

    public override void DrawGizmos(Transform transform)
    {
        Gizmos.color = Color.yellow;
        if (detectType == DetectType.StrightLine)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.right * detectDistance);
        }
        else if (detectType == DetectType.Circle)
        {
            Gizmos.DrawWireSphere(transform.position, detectDistance);
        }
    }
}
