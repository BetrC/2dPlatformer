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


        if (isExitingState)
            return;
        hero.movement.SetVelocityY(yNormalInput * 2f);
    }


    public override bool IsTriggered()
    {
        return yNormalInput != 0;
    }
}

