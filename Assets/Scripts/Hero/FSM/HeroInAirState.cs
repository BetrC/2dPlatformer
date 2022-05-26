using System;
using UnityEngine;

public class HeroInAirState : HeroState
{

    protected bool onGround;

    /// <summary>
    /// 土狼时间
    /// </summary>
    public float coyoteTime;
    public bool enableCoyoteTime;

    public float wallJumpCoyoteTime;
    public bool enableWallJumpCoyoteTime;

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

    /// <summary>
    /// 跳跃的土狼时间
    /// </summary>
    private void UpdateCoyoteTime()
    {
        if (coyoteTime >= 0)
            coyoteTime -= Time.deltaTime;
        if (coyoteTime < 0 && enableCoyoteTime)
        {
            enableCoyoteTime = false;
            hero.JumpState.DecreaseJumpTime();
        }
    }

    /// <summary>
    /// 蹬墙跳的土狼时间
    /// </summary>
    private void UpdateWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime >= 0)
            wallJumpCoyoteTime -= Time.deltaTime;
        if (wallJumpCoyoteTime < 0 && enableWallJumpCoyoteTime)
            enableWallJumpCoyoteTime = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        UpdateCoyoteTime();
        UpdateWallJumpCoyoteTime();
        if (jumpFallTime >= 0)
        {
            jumpFallTime -= Time.deltaTime;
        }

        xInput = InputManager.Instance.XInput;
        xNormalInput = InputManager.Instance.XNormalInput;

        // 小跳
        if (InputManager.Instance.JumpReleased && hero.movement.CurrentVelocity.y > 0)
        {
            //CLog.Log($"小跳 : {hero.movement.CurrentVelocity.y}");
            hero.movement.SetVelocityY(hero.movement.CurrentVelocity.y / 2);
        }

        if (onGround && hero.movement.CurrentVelocity.y < 0.1f)
        {
            stateMachine.ChangeState(hero.LandState);
        } 
        else if (enableWallJumpCoyoteTime && InputManager.Instance.JumpPressed)
        {
            stateMachine.ChangeState(hero.WallJumpState);
        }
        else if (hero.JumpState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.JumpState);
            enableCoyoteTime = false;
        }
        else if (InputManager.Instance.JumpPressed)
        {
            jumpFallTime = heroData.jumpFallThreshould;
        }
        else if (hero.DashState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.DashState);
        } else if (hero.AttackState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.AttackState);
        } else if (hero.WallGrabState.IsTriggered())
        {
            stateMachine.ChangeState(hero.WallGrabState);
        }else if (hero.WallSlideState.IsTriggered())
        {
            stateMachine.ChangeState(hero.WallSlideState);
        }


        if (isExitingState)
            return;

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
        enableCoyoteTime = true;
    }

    public void SetWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = heroData.wallJumpCoyoteTime;
        enableWallJumpCoyoteTime = true;
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
