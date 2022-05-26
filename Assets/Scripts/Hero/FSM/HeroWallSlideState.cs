using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HeroWallSlideState : HeroTouchWallState
{
    
    public HeroWallSlideState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;
        hero.movement.SetVelocityY(heroData.wallSlideSpeed);
    }

    public override bool IsTriggered()
    {
        return InputManager.Instance.XNormalInput == hero.movement.FacingDirection && IsOnWall();
    }
}

