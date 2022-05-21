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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManger.Instance.xInput;
        xNormalInput = InputManger.Instance.XNormalInput;
        onGround = hero.collisionChecker.onGround;

        if (hero.JumpState.IsTriggerJump())
        {
            stateMachine.ChangeState(hero.JumpState);
        }
        else if (hero.InAirState.GroundTouchJump())
        {
            hero.InAirState.ResetGroundTouchTime();
            stateMachine.ChangeState(hero.JumpState);
        }
        else if(!onGround)
        {
            stateMachine.ChangeState(hero.InAirState);
            hero.InAirState.SetCoyoteTime();
        }
    }
}
