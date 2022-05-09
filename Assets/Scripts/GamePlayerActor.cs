using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

using static AnimationParamString;

public class GamePlayerActor : Actor2D
{
    [Header("Speed")]
    public float runSpeed = 5f;
    public float jumpSpeed = 25f;
    public float dashSpeed = 40f;

    [Header("Threshold")]
    public float jumpThreshold = .1f;

    private float jumpTime = 0f;

    private Rigidbody2D rg2D;
    private CollisionChecker collisionChecker;
    private Vector2 tempV2;

    private float horizontalInput;
    private float verticalInput;
    private float lastInputHorizontalInput;

    public int canJumpCount;

    [Header("Actor Status")]
    public bool wallGrab;
    public bool wallSlide;
    public bool wallJump;

    public bool canMove;

    [Header("Dash")]
    public DashTrail dashTrail;
    public bool hasDashed;
    public bool isDashing;

    protected override void Awake()
    {
        base.Awake();
        rg2D = GetComponent<Rigidbody2D>();
        collisionChecker = GetComponent<CollisionChecker>();
        collisionChecker.OnGroundTouch -= OnGroundTouch;
        collisionChecker.OnGroundTouch += OnGroundTouch;

        canMove = true;
    }

    private void Update()
    {
        if (collisionChecker.onGround || !collisionChecker.OnWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }
        if (collisionChecker.onGround)
            jumpTime = jumpThreshold;
        else
            jumpTime -= Time.deltaTime;

        if (!isDashing)
            rg2D.gravityScale = 3;

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0)
            lastInputHorizontalInput = horizontalInput;

        if (collisionChecker.OnWall && !collisionChecker.onGround && canMove)
        {
            wallGrab = Input.GetButton("Grab");
            wallSlide = !wallGrab;
        }

        // ÐÐ×ß
        Walk();

        if (wallGrab)
        {
            WallGrab();
        }

        if (wallSlide)
        {
            WallSlide();
        }

        // ÌøÔ¾
        if (collisionChecker.onGround || jumpTime > 0)
        {
            if (Input.GetButtonDown("Jump"))
                Jump(Vector2.up);
        }

        if (Input.GetButtonUp("Jump") && !wallJump) {
            if (rg2D.velocity.y > 0)
            {
                tempV2.Set(rg2D.velocity.x, .5f * rg2D.velocity.y);
                rg2D.velocity = tempV2;
            }
        }

        if (Input.GetButtonDown("Jump") && collisionChecker.OnWall && !collisionChecker.onGround)
        {
            Debug.Log("WallJump");
            WallJump();
        }

        if (Input.GetButtonDown("Dash") && !hasDashed)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                Debug.Log("Dash");
                Dash();
            }
        }
    }

    private void Dash()
    {
        hasDashed = true;
        isDashing = true;
        // shake camera not work with cinemachine
        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        dashTrail.ShowTrail(this);

        Vector2 dir = new Vector2(horizontalInput, verticalInput);
        rg2D.velocity = dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    private IEnumerator DashWait()
    {
        rg2D.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        yield return new WaitForSeconds(.1f);

        rg2D.gravityScale = 3;
        GetComponent<BetterJump>().enabled = true;
        isDashing = false;
    }

    private void OnGroundTouch()
    {
        hasDashed = false;
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(0.1f));
        Vector2 wallDir = collisionChecker.onLeftWall ? Vector2.right : Vector2.left;
        Jump(0.6f * (Vector2.up + wallDir));
    }

    private void WallSlide()
    {
        if (!canMove)
            return;
        tempV2.Set(rg2D.velocity.x, -1f);
        rg2D.velocity = tempV2;
    }

    private void WallGrab()
    {
        rg2D.gravityScale = 0;
        float speedModifyer = verticalInput > 0 ? .5f : 1f;
        tempV2.Set(rg2D.velocity.x, verticalInput * speedModifyer * runSpeed);
        rg2D.velocity = tempV2;
    }

    private void Jump(Vector2 dir)
    {
        tempV2.Set(rg2D.velocity.x, 0);
        tempV2 += dir * jumpSpeed;
        rg2D.velocity = tempV2;
        jumpTime = 0f;
    }

    private void Walk()
    {
        if (wallGrab || !canMove || isDashing)
            return;
        tempV2.Set(horizontalInput.Normalize() * runSpeed, rg2D.velocity.y);
        rg2D.velocity = tempV2;
    }


    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        HandlePlayerAnimation();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void HandlePlayerAnimation()
    {
        if (animator == null)
            return;

        animator.SetFloat(FLOAT_HORIZONTAL_INPUT, horizontalInput);
        animator.SetFloat(FLOAT_SPEEDX, rg2D.velocity.x);
        animator.SetFloat(FLOAT_SPEEDY, rg2D.velocity.y);

        bool moving = rg2D.velocity.magnitude > 0.0000001f;
        animator.SetBool(BOOL_MOVING, moving);

        animator.SetBool(BOOL_GROUND, collisionChecker.onGround);

        animator.SetBool(BOOL_WALL_GRAB, wallGrab);

        animator.SetBool(BOOL_WALL_SLIDE, wallSlide);

        // check if need flip render
        bool flipX;
        if (wallGrab || wallSlide)
            flipX = collisionChecker.onLeftWall;
        else
            flipX = lastInputHorizontalInput < 0;
        spriteRenderer.flipX = flipX;
    }
}
