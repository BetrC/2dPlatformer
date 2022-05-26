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

    public HeroTouchWallState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hero.movement.SetGravityScale(0f);
        hero.movement.SetBetterJumpEnable(false);
    }

    public override void Exit()
    {
        base.Exit();
        hero.movement.SetGravityScale(heroData.defaultGravityScale);
        hero.movement.SetBetterJumpEnable(true);
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
        onGround = hero.collisionChecker.onGround;
        onWall = IsOnWall();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.XInput;
        xNormalInput = InputManager.Instance.XNormalInput;
        yInput = InputManager.Instance.YInput;
        yNormalInput = InputManager.Instance.YNormalInput;

        if (hero.WallGrabState.IsTriggered())
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
            stateMachine.ChangeState(hero.InAirState);
        }


    }

    public bool IsPushingWall() => onWall && hero.movement.FacingDirection == xNormalInput;

    public bool IsOnWall() => hero.collisionChecker.onLeftWall && hero.movement.FacingDirection == -1 || hero.collisionChecker.onRightWall && hero.movement.FacingDirection == 1;

    public virtual bool IsTriggered() { return false; }
}
