using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRunState : HeroOnGroundState
{
    public HeroRunState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(xInput) < 0.1f)
        {
            stateMachine.ChangeState(hero.IdleState);
        }

        if (isExitingState)
            return;

        hero.movement.CheckFlip(xNormalInput);
        hero.movement.SetVelocityX( xNormalInput * heroData.runSpeed );
    }
}
