using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HeroWallGrabState : HeroTouchWallState
{

    public HeroWallGrabState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        wallParticle.Stop();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!InputManager.Instance.GrabWallHolding && IsPushingWall())
        {
            stateMachine.ChangeState(hero.WallSlideState);
        }
        else if (yNormalInput != 0)
        {
            stateMachine.ChangeState(hero.WallClimbState);
        }

        if (isExitingState)
            return;
        // 确保贴墙
        hero.movement.SetFacingDirVelocityX(6f);
        hero.movement.SetVelocityY(0f);
    }

    public override bool IsTriggered()
    {
        return InputManager.Instance.GrabWallHolding && IsOnWall();
    }
}

