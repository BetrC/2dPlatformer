using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : HeroOnGroundState
{
    public HeroIdleState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(xInput) >= 0.1f)
        {
            stateMachine.ChangeState(hero.RunState);
        }

        if (!isExitingState)
        {
            hero.movement.SetVelocity(Vector2.zero);
        }
    }
}
