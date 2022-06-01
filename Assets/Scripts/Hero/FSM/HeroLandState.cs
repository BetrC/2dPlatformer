using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HeroLandState : HeroOnGroundState
{
    public bool isAnimFinish;

    
    public HeroLandState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAnimFinish = false;
        //hero.JumpState.ShowJumpParticle();
    }

    public override void LogicUpdate()
    {
        if (isExitingState)
            return;

        base.LogicUpdate();

        if (isAnimFinish)
        {
            stateMachine.ChangeState(hero.IdleState);
        }
        else if (xInput != 0)
        {
            stateMachine.ChangeState(hero.RunState);
        }
    }



    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimFinish = true;
    }
}

