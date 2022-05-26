using System;
using UnityEngine;

public class HeroTouchWallState : HeroState
{

    protected float xInput;
    protected int xNormalInput;

    protected float yInput;
    protected float yNormalInput;

    protected bool onGround;
    protected bool onWall;

    public static ParticleSystem wallParticle;

    public HeroTouchWallState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hero.movement.SetGravityScale(0f);
        hero.movement.SetBetterJumpEnable(false);
        if(wallParticle == null)
        {
            wallParticle = ParticleSystem.Instantiate(heroData.wallSideParticle, hero.transform);
        }
        wallParticle.transform.localPosition = new Vector3(heroData.wallSideParticleOffsetX, 0, 0);
        wallParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
        hero.movement.SetGravityScale(heroData.defaultGravityScale);
        hero.movement.SetBetterJumpEnable(true);
        wallParticle.Stop();
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
        onGround = hero.collisionChecker.OnGround;
        onWall = IsOnWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.XInput;
        xNormalInput = InputManager.Instance.XNormalInput;
        yInput = InputManager.Instance.YInput;
        yNormalInput = InputManager.Instance.YNormalInput;

       
        if (hero.WallJumpState.TriggeredAbility())
        {
            stateMachine.ChangeState(hero.WallJumpState);
        }
        else if (hero.LedgeClimbState.IsTriggered())
        {
            stateMachine.ChangeState(hero.LedgeClimbState);
        }
        else if (hero.WallGrabState.IsTriggered())
        {
            if (yNormalInput == 0)
                stateMachine.ChangeState(hero.WallGrabState);
        }
        else if (onGround)
        {
            stateMachine.ChangeState(hero.IdleState);
        }
        else if (!IsPushingWall())
        {
            hero.InAirState.SetWallJumpCoyoteTime();
            stateMachine.ChangeState(hero.InAirState);
        }
    }

    public bool IsPushingWall() => onWall && FacingDirection == xNormalInput;

    public bool IsOnWall() => hero.collisionChecker.OnLeftWall && FacingDirection == -1 || hero.collisionChecker.OnRightWall && FacingDirection == 1;

    public virtual bool IsTriggered() { return false; }
}
