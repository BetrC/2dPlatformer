using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D RB;

    [HideInInspector]
    public BetterJump betterJump;

    public int FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    public Vector2 CurrentVelocity { get; private set; }

    /// <summary>
    /// RB 速度的替身，临时变量，最终将使用该变量来设置RB velocity
    /// </summary>
    private Vector2 substitute;

    private void Awake()
    {
        RB = GetComponentInParent<Rigidbody2D>();
        betterJump = GetComponentInParent<BetterJump>();
        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    public void SetVelocity(Vector2 vec)
    {
        substitute = vec;
        SetVelocity(vec.x, vec.y);
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        SetVelocity(angle.x * velocity * direction, angle.y * velocity);
    }

    public void SetVelocityX(float x)
    {
        SetVelocity(x, CurrentVelocity.y);
    }

    public void SetVelocityY(float y)
    {
        SetVelocity(CurrentVelocity.x, y);
    }

    public void SetFacingDirVelocityX(float x)
    {
        SetVelocityX(x * FacingDirection);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        substitute.Set(xVelocity, yVelocity);
        DoSetVelocity();
    }

    private void DoSetVelocity()
    {
        if (!CanSetVelocity)
            return;

        RB.velocity = substitute;
        CurrentVelocity = RB.velocity;
    }


    public void CheckFlip(int xNormalInput)
    {
        if (xNormalInput * FacingDirection == -1)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0f, 180f, 0f);
    }

    public void SetBetterJumpEnable(bool enabled)
    {
        if (betterJump != null)
            betterJump.enabled = enabled;
    }

    public void SetGravityScale(float gravityScale)
    {
        RB.gravityScale = gravityScale;
    }
}
