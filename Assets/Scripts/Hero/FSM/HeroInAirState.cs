using System;
using UnityEngine;

public class HeroInAirState : HeroState
{

    protected bool onGround;

    /// <summary>
    /// 土狼时间
    /// </summary>
    public float coyoteTime;
    public float jumpFallTime;

    public float xInput;
    public int xNormalInput;


    public HeroInAirState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetGroundTouchTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (coyoteTime > 0)
        {
            coyoteTime -= Time.deltaTime;
        }

        xInput = InputManger.Instance.xInput;
        xNormalInput = InputManger.Instance.XNormalInput;

        if (onGround && hero.movement.CurrentVelocity.y < 0.1f)
        {
            stateMachine.ChangeState(hero.IdleState);
        }
        else if (coyoteTime >= 0 && hero.JumpState.IsTriggerJump())
        {
            stateMachine.ChangeState(hero.JumpState);
        } else if (InputManger.Instance.JumpPressed)
        {
            jumpFallTime = heroData.jumpFallThreshould;
        }

        if (isExitingState)
            return;

        // 小跳
        if (InputManger.Instance.JumpReleased && hero.movement.CurrentVelocity.y > 0)
        {
            hero.movement.SetVelocityY(hero.movement.CurrentVelocity.y / 2);
        }

        hero.movement.CheckFlip(xNormalInput);
        hero.movement.SetVelocityX(xNormalInput * heroData.runSpeed);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        onGround = hero.collisionChecker.onGround;
    }

    public void SetCoyoteTime()
    {
        coyoteTime = heroData.coyoteTime;
    }

    public bool GroundTouchJump()
    {
        if (onGround && jumpFallTime > 0)
        {
            return true;
        }
        return false;
    }

    public void ResetGroundTouchTime()
    {
        jumpFallTime = 0;
    }
}
