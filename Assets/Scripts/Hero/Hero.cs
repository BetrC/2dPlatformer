using System;
using UnityEngine;
using UnityEngine.Assertions;
using static AnimationParamString;

public class Hero : DamageableActor
{
    public HeroData heroData;

    public Movement movement;

    public StateMachine stateMachine;

    public CollisionChecker collisionChecker;

    #region state

    public HeroIdleState IdleState;

    public HeroRunState RunState;
    public HeroInAirState InAirState;
    public HeroJumpState JumpState;
    public HeroDashState DashState;
    public HeroAttackState AttackState;
    public HeroLandState LandState;
    public HeroWallClimbState WallClimbState;
    public HeroWallGrabState WallGrabState;
    public HeroWallSlideState WallSlideState;
    public HeroWallJumpState WallJumpState;
    public HeroLedgeClimbState LedgeClimbState;
    public HeroHitBackState HitBackState;
    public HeroDieState DieState;

    public HeroState CurrentState => stateMachine.currentState as HeroState;

    #endregion

    #region weapon

    public RuntimeWeaponInfo primaryWeapon;

    public RuntimeWeaponInfo secondaryWeapon;

    public RuntimeWeaponInfo shootWeapon;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        movement = GetComponentInChildren<Movement>();
        collisionChecker = GetComponentInChildren<CollisionChecker>();
        hitProtectionTime = heroData.hitProtectionTime;

        Assert.IsNotNull(primaryWeapon.prefab, "primary weapon prefab is Null, please check your setting");
        Assert.IsNotNull(secondaryWeapon.prefab, "secondry weapon prefab is Null, please check your setting");
        Assert.IsNotNull(shootWeapon.prefab, "shoot weapon prefab is Null, please check your setting");

        // init weapon
        primaryWeapon.Init(transform.Find("WeaponRoot"));
        secondaryWeapon.Init(transform.Find("WeaponRoot"));
        shootWeapon.Init(transform.Find("WeaponRoot"));

        // init state
        InitStateMachine();

        Assert.IsNotNull(heroData, "heroData is Null, please check your setting");
    }

    protected override void Start()
    {
        base.Start();

        movement.SetGravityScale(heroData.defaultGravityScale);
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine();
        IdleState = new HeroIdleState(stateMachine, this, BOOL_IDLE);
        RunState = new HeroRunState(stateMachine, this, BOOL_RUN);
        InAirState = new HeroInAirState(stateMachine, this, BOOL_INAIR);
        JumpState = new HeroJumpState(stateMachine, this, BOOL_INAIR);
        DashState = new HeroDashState(stateMachine, this, BOOL_DASH);

        AttackState = new HeroAttackState(stateMachine, this, BOOL_ATTACK);
        AttackState.SetWeapon(primaryWeapon.weapon);

        LandState = new HeroLandState(stateMachine, this, BOOL_LAND);
        WallClimbState = new HeroWallClimbState(stateMachine, this, BOOL_WALL_CLIMB);
        WallGrabState = new HeroWallGrabState(stateMachine, this, BOOL_WALL_GRAB);
        WallSlideState = new HeroWallSlideState(stateMachine, this, BOOL_WALL_SLIDE);
        WallJumpState = new HeroWallJumpState(stateMachine, this, BOOL_INAIR);
        LedgeClimbState = new HeroLedgeClimbState(stateMachine, this, BOOL_LEDGE_HANG);

        HitBackState = new HeroHitBackState(stateMachine, this, BOOL_HIT_BACK);
        DieState = new HeroDieState(stateMachine, this, BOOL_DEAD);
        

        stateMachine.Init(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        movement.LogicUpdate();
        CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }

    public override bool IsHittable()
    {
        return base.IsHittable() && CurrentState.CanReceiveHit;
    }

    protected override void OnTakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0)
    {
        base.OnTakeDamage(damage, angle, strength, xDir);
        substitute.Set(angle.x * xDir, angle.y);
        substitute.Normalize();
        substitute *= strength;
        CurrentState.OnReceiveHit(damage, substitute);
    }

    protected override void OnDie()
    {
        base.OnDie();
        stateMachine.ChangeState(DieState);
    }

    public virtual void AnimationTrigger() {
        CurrentState.AnimationTrigger();
    }

    public virtual void AnimationFinishTrigger()
    {
        CurrentState.AnimationFinishTrigger();
    }

    public void Respawn()
    {
        transform.position = GameManager.Instance.respawnTransform.position;
        health.Reset();
        gameObject.SetActive(true);
        BindHealthBar();
        stateMachine.ChangeState(IdleState);
    }


    protected override void BindHealthBar()
    {
        if (healthBar == null)
            Bind(GameObject.Find("HeroHealthBar").GetComponent<HealthBar>(), false);
        else
            Bind(healthBar, false);
    }

    protected override void OnHealthUpdated(float curHealth, float maxHealth, float deltaChange)
    {
        base.OnHealthUpdated(curHealth, maxHealth, deltaChange);
        if (deltaChange < 0)
        {
            FightNumberManager.Instance.ShowFightNumber(-deltaChange, boneHead, FightNumType.HeroHit);
        }
        if (deltaChange > 0)
        {
            FightNumberManager.Instance.ShowFightNumber(-deltaChange, boneHead, FightNumType.HeroCure);
        }
    }

    public void Dispose()
    {
        Unbind(healthBar);
    }
}
