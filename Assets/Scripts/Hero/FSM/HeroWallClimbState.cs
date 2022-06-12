using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HeroWallClimbState : HeroTouchWallState
{

    public HeroWallClimbState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!InputManager.Instance.GrabWallHolding && IsPushingWall())
        {
            stateMachine.ChangeState(hero.WallSlideState);
        }

        if (isExitingState)
            return;
        hero.movement.SetVelocityY(yNormalInput * heroData.wallClimbSpeed);
    }


    public override bool IsTriggered()
    {
        return yNormalInput != 0;
    }
}

