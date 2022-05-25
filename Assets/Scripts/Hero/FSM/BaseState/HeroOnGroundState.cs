using System;
using UnityEngine;

public class HeroOnGroundState : HeroState
{

    protected float xInput;
    protected int xNormalInput;
    protected bool onGround;

    public HeroOnGroundState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hero.JumpState.ResetJumpCount();
        hero.DashState.ResetDashTime();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
        onGround = hero.collisionChecker.onGround;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.XInput;
        xNormalInput = InputManager.Instance.XNormalInput;

        if (hero.JumpState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.JumpState);
        }
        else if (hero.InAirState.GroundTouchJump())
        {
            hero.InAirState.ResetGroundTouchTime();
            stateMachine.ChangeState(hero.JumpState);
        }
        else if (hero.DashState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.DashState);
        } else if (hero.AttackState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.AttackState);
        }
        else if(!onGround)
        {
            stateMachine.ChangeState(hero.InAirState);
            hero.InAirState.SetCoyoteTime();
        }
    }
}
