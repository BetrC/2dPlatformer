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
    public bool onGround;

    public bool onLeftWall;
    public bool onRightWall;

    public bool lastFrameOnGround;

    public bool OnWall => onLeftWall || onRightWall;

    public Action OnGroundTouch;

    [Space]

    [Header("碰撞盒参数")]
    public float collisionRadius;
    public Vector2 groudCheckOffset, leftWallCheckOffset, rightWallCheckOffset;
    public LayerMask layerMask;

    private void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + groudCheckOffset, collisionRadius, layerMask);
        if (onGround && !lastFrameOnGround)
            OnGroundTouch?.Invoke();
        lastFrameOnGround = onGround;

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftWallCheckOffset, collisionRadius, layerMask);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightWallCheckOffset, collisionRadius, layerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + groudCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftWallCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightWallCheckOffset, collisionRadius);
    }
}
