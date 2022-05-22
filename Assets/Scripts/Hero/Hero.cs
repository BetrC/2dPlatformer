using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    #endregion

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponentInChildren<Movement>();
        collisionChecker = GetComponentInChildren<CollisionChecker>();
        InitStateMachine();

        Assert.IsNotNull(heroData, "heroData is Null, please check your setting");
    }

    private void Start()
    {
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
        stateMachine.Init(IdleState);
    }

    private void Update()
    {
        movement.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public virtual void AnimationTrigger() {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
