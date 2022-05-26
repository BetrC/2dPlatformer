using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

public class CollisionChecker : MonoBehaviour
{
    [Header("检测结果")]
    public bool OnGround;

    public bool OnLeftWall;
    public bool OnRightWall;

    public bool LastFrameOnGround;

    public bool OnWall => OnLeftWall || OnRightWall;

    public bool WallDetected;

    public bool LedgeDetected;

    public Action OnGroundTouch;

    [Space]

    [Header("碰撞盒参数")]
    public float collisionRadius;
    public Vector2 groudCheckOffset, leftWallCheckOffset, rightWallCheckOffset;

    public LayerMask whatIsGround;

    [Header("Wall Check")]
    public Transform wallCheckTransform;
    public float wallCheckDistance;

    public Transform ledgeCheckTransform;
    public float ledgeCheckDistance;

    [HideInInspector]
    public Vector2 WallCheckPosition => wallCheckTransform.position;

    [HideInInspector]
    public Vector2 LedgeCheckPosition => ledgeCheckTransform.position;

    private void Update()
    {
        OnGround = Physics2D.OverlapCircle((Vector2)transform.position + groudCheckOffset, collisionRadius, whatIsGround);
        if (OnGround && !LastFrameOnGround)
            OnGroundTouch?.Invoke();
        LastFrameOnGround = OnGround;

        OnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftWallCheckOffset, collisionRadius, whatIsGround);
        OnRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightWallCheckOffset, collisionRadius, whatIsGround);

        WallDetected = Physics2D.Raycast(WallCheckPosition, transform.right, wallCheckDistance, whatIsGround);

        LedgeDetected = Physics2D.Raycast(LedgeCheckPosition, transform.right, ledgeCheckDistance, whatIsGround);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + groudCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftWallCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightWallCheckOffset, collisionRadius);
    }
}
