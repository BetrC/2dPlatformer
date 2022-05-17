using System.Collections;
using UnityEngine;
using static AnimationParamString;

public class Hero : Actor2D
{

    [Header("Speed")]
    public float runSpeed = 5f;
    public float jumpSpeed = 25f;
    public float attackShiftSpeed = 1f;
    public float dashSpeed = 40f;
    public float dashTime = .4f;

    [Header("Threshold")]
    public float jumpThreshold = .15f;
    public float fallJumpThreshould = .1f;

    private float jumpTime = 0f;
    private float fallJumpTime = 0f;

    

    private Rigidbody2D rg2D;
    private CollisionChecker collisionChecker;
    private Vector2 tempV2;

    private float horizontalInput;
    private float lastInputHorizontalInput;

    private bool lockMove = false;
    private bool isDashing = false;

    [Header("Actor Status")]
    public bool jumping;

    private PlayerInput playerInput;
    private NormalAttack normalAttack;

    public DashTrail dashTrail;

    protected override void Awake()
    {
        base.Awake();
        rg2D = GetComponent<Rigidbody2D>();
        collisionChecker = GetComponent<CollisionChecker>();
        normalAttack = GetComponent<NormalAttack>();
        collisionChecker.OnGroundTouch -= OnGroundTouch;
        collisionChecker.OnGroundTouch += OnGroundTouch;

        playerInput = InputManger.Instance.Input;
        playerInput.Player.Enable();
        playerInput.Player.JumpPress.performed += ctx => JumpPressed();
        playerInput.Player.JumpRelease.performed += ctx => JumpReleased();
    }

    void OnGroundTouch()
    {
        jumping = false;
        if (fallJumpTime > 0f)
        {
            JumpPressed();
        }
    }

    private void Update()
    {
        horizontalInput = playerInput.Player.Movement.ReadValue<float>();

        // jumpthreshold
        if (collisionChecker.onGround)
        {
            jumpTime = jumpThreshold;
            fallJumpTime = 0f;
        }else
        {
            jumpTime -= Time.deltaTime;
            fallJumpTime -= Time.deltaTime;
        }

        // normal attack
        if (playerInput.Player.normalAttack.triggered && !isDashing)
        {
            normalAttack.TriggerAttack();
        }

        // dash
        if (playerInput.Player.Dash.triggered)
        {
            Dash();
        }

        CheckLockMove();

        if (horizontalInput != 0 && !lockMove)
            lastInputHorizontalInput = horizontalInput;

        Walk();

        SetAnimation();
    }

    private void CheckLockMove()
    {
        lockMove = !normalAttack.CanCast() || isDashing;
    }

    private void Dash()
    {
        if (lockMove)
            return;
        isDashing = true;
        dashTrail.ShowTrail(this);
    }

    public void DashEnd()
    {
        isDashing = false;
    }

    private void Walk()
    {
        if (lockMove)
        {
            if (normalAttack.IsPlayingAttackAnim)
            {
                // attack shift
                tempV2.Set(Mathf.Sign(lastInputHorizontalInput) * attackShiftSpeed, rg2D.velocity.y);
            }else if (isDashing)
            {
                tempV2.Set(Mathf.Sign(lastInputHorizontalInput) * dashSpeed, rg2D.velocity.y);
            } else
            {
                // lock move
                tempV2.Set(0f, rg2D.velocity.y);
            }
        }
        else
        {
            // normal walk
            tempV2.Set(horizontalInput.Normalize() * runSpeed, rg2D.velocity.y);
        }

        rg2D.velocity = tempV2;
    }

    public void JumpPressed()
    {
        if (!collisionChecker.onGround && jumpTime <= 0)
        {
            fallJumpTime = fallJumpThreshould;
            return;
        }

        tempV2.Set(rg2D.velocity.x, 0);
        tempV2 += Vector2.up * jumpSpeed;
        rg2D.velocity = tempV2;
        jumpTime = 0f;
        jumping = true;
    }

    public void JumpReleased()
    {
        if (rg2D.velocity.y > 0)
        {
            tempV2.Set(rg2D.velocity.x, rg2D.velocity.y / 2);
        }
        rg2D.velocity = tempV2;
    }

    public void SetAnimation()
    {
        // flip
        float yAngle = 0;
        if (lastInputHorizontalInput < 0)
            yAngle = 180;
        transform.rotation = Quaternion.Euler(0, yAngle, 0);

        if (animator == null)
            return;
        animator.SetFloat(FLOAT_HORIZONTAL_INPUT, Mathf.Abs(horizontalInput));
        animator.SetFloat(FLOAT_SPEEDY, Mathf.Abs(rg2D.velocity.y));
        animator.SetBool(BOOL_GROUND, collisionChecker.onGround);
        animator.SetBool("IsCastingSkill", normalAttack.IsPlayingAttackAnim);
        animator.SetBool("isDashing", isDashing);
    }
}
