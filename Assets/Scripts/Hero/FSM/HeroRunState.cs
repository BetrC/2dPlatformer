using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRunState : HeroOnGroundState
{

    private ParticleSystem runParticle;

    public HeroRunState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (runParticle == null)
        {
            runParticle = GameObject.Instantiate(heroData.runParticle, hero.transform);
        }
        runParticle.Play();
    }

    public override void Exit()
    {
        base.Exit();
        runParticle.Stop();
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
        hero.movement.SetVelocityX(xNormalInput * heroData.runSpeed);
    }
}
