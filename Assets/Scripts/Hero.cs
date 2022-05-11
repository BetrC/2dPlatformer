using UnityEngine;
using static AnimationParamString;

public class Hero : Actor2D
{

    [Header("Speed")]
    public float runSpeed = 5f;
    public float jumpSpeed = 25f;
    //public float dashSpeed = 40f;

    [Header("Threshold")]
    public float jumpThreshold = .1f;
    private float jumpTime = 0f;

    private Rigidbody2D rg2D;
    private CollisionChecker collisionChecker;
    private Vector2 tempV2;

    private float horizontalInput;
    private float lastInputHorizontalInput;



    [Header("Actor Status")]
    public bool jumping;

    private PlayerInput playerInput;
    public bool pressAttack;

    protected override void Awake()
    {
        base.Awake();
        rg2D = GetComponent<Rigidbody2D>();
        collisionChecker = GetComponent<CollisionChecker>();
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
    }

    private void Update()
    {
        horizontalInput = playerInput.Player.Movement.ReadValue<float>();
        Walk();

        if (horizontalInput != 0)
            lastInputHorizontalInput = horizontalInput;

        // jumpthreshold
        if (collisionChecker.onGround)
            jumpTime = jumpThreshold;
        else
            jumpTime -= Time.deltaTime;

        // normal attack
        if (playerInput.Player.normalAttack.triggered && !pressAttack)
        {
            pressAttack = true;
        }

        SetAnimation();
    }

    private void Walk()
    {
        if (pressAttack)
            return;

        tempV2.Set(horizontalInput.Normalize() * runSpeed, rg2D.velocity.y);
        rg2D.velocity = tempV2;
    }

    public void JumpPressed()
    {
        if (pressAttack)
            return;

        if (!collisionChecker.onGround && jumpTime <= 0)
            return;
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
    }
}
