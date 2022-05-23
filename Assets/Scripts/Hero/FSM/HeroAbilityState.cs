using System;
using UnityEngine;

public class HeroAbilityState : HeroState
{

    protected float xInput;
    protected int xNormalInput;

    public bool isAbilityDone;
    bool onGround;

    public HeroAbilityState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public virtual bool TriggeredAbility()
    {
        return false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = InputManager.Instance.xInput;
        xNormalInput = InputManager.Instance.XNormalInput;

        if (isAbilityDone)
        {
            if (onGround && hero.movement.CurrentVelocity.y < 0.1f)
            {
                stateMachine.ChangeState(hero.IdleState);
            }
            else
            {
                stateMachine.ChangeState(hero.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        onGround = hero.collisionChecker.onGround;
    }
}
