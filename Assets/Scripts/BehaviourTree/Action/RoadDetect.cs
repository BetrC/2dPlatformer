using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RoadDetect : ActionNode
{
    public Vector2 edgeCheckPositionOffset;
    public float edgeCheckDistance = 1f;

    public Vector2 ledgeCheckPositionOffset;
    public float ledgeCheckDistance = 1f;

    public LayerMask whatIsGround;

    //** runtime only
    protected Vector2 EdgeCheckPosition => (Vector2)context.transform.position + new Vector2(edgeCheckPositionOffset.x * context.movement.FacingDirection, edgeCheckPositionOffset.y);

    //** runtime only
    protected Vector2 LedgeCheckPosition => (Vector2)context.transform.position + new Vector2(ledgeCheckPositionOffset.x * context.movement.FacingDirection, ledgeCheckPositionOffset.y);


    public bool EdgeDetected => Physics2D.Raycast(EdgeCheckPosition,
                                                  context.transform.right,
                                                  edgeCheckDistance, whatIsGround);

    public bool LedgeDetected => Physics2D.Raycast(LedgeCheckPosition,
                                                   Vector2.down,
                                                   ledgeCheckDistance, whatIsGround);

    protected override void OnStart() {

    }

    protected override void OnStop() {

    }

    protected override State OnUpdate() {
        if (EdgeDetected || !LedgeDetected)
            return State.Failure;
        return State.Success;
    }

    public override void DrawGizmos(Transform transform)
    {
        Vector2 pos = transform.position;
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(pos + new Vector2(edgeCheckPositionOffset.x * transform.right.x, edgeCheckPositionOffset.y),
                        pos + new Vector2(edgeCheckPositionOffset.x * transform.right.x, edgeCheckPositionOffset.y) + (Vector2)transform.right * edgeCheckDistance);

        Gizmos.DrawLine(pos + new Vector2(ledgeCheckPositionOffset.x * transform.right.x, ledgeCheckPositionOffset.y),
                        pos + new Vector2(ledgeCheckPositionOffset.x * transform.right.x, ledgeCheckPositionOffset.y) + Vector2.down * ledgeCheckDistance);
    }
}
