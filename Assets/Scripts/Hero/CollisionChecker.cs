using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

public class CollisionChecker : MonoBehaviour
{

    [Header("碰撞检测参数")]
    public float collisionRadius;
    public Vector2 groudCheckOffset, leftWallCheckOffset, rightWallCheckOffset;

    /// <summary>
    /// 地面的layer
    /// </summary>
    public LayerMask whatIsGround;

    [Header("墙体检测参数")]
    public Transform wallCheckTransform;
    public float wallCheckDistance;

    public Transform ledgeCheckTransform;
    public float ledgeCheckDistance;


    /// <summary>
    /// （身体中央）墙体的位置
    /// </summary>
    public Vector2 WallCheckPosition => wallCheckTransform.position;

    /// <summary>
    /// 检测头是否超过墙体的位置
    /// </summary>
    public Vector2 LedgeCheckPosition => ledgeCheckTransform.position;

    /// <summary>
    /// 是否站在地上
    /// </summary>
    public bool OnGround => Physics2D.OverlapCircle((Vector2)transform.position + groudCheckOffset, collisionRadius, whatIsGround);

    /// <summary>
    /// 左侧是否与墙体碰撞
    /// </summary>
    public bool OnLeftWall => Physics2D.OverlapCircle((Vector2)transform.position + leftWallCheckOffset, collisionRadius, whatIsGround);

    /// <summary>
    /// 右侧是否与墙体碰撞
    /// </summary>
    public bool OnRightWall => Physics2D.OverlapCircle((Vector2)transform.position + rightWallCheckOffset, collisionRadius, whatIsGround);

    /// <summary>
    /// 是否与墙体发生碰撞（左侧或右侧）
    /// </summary>
    public bool OnWall => OnLeftWall || OnRightWall;

    /// <summary>
    /// 身体射线检测到墙体
    /// </summary>
    public bool WallDetected => Physics2D.Raycast(WallCheckPosition, transform.right, wallCheckDistance, whatIsGround);

    /// <summary>
    /// 头部射线检测到墙体
    /// </summary>
    public bool LedgeDetected => Physics2D.Raycast(LedgeCheckPosition, transform.right, ledgeCheckDistance, whatIsGround);



    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + groudCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftWallCheckOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightWallCheckOffset, collisionRadius);
    }
}
