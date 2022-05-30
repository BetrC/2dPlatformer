using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : State
{
    protected Hero hero;
    protected HeroData heroData;

    /// <summary>
    /// 当前状态所对应的状态机参数
    /// </summary>
    private string animatorBoolParam;

    protected int FacingDirection => hero.movement.FacingDirection;

    public bool CanReceiveHit;

    public HeroState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine)
    {
        this.hero = hero;
        this.animatorBoolParam = animatorBoolParam;
        heroData = hero.heroData;
        CanReceiveHit = true;
    }

    public override void Enter()
    {
        base.Enter();
        if (animatorBoolParam != "")
            hero.animator.SetBool(animatorBoolParam, true);
    }

    public override void Exit()
    {
        base.Exit();
        if (animatorBoolParam != "")
            hero.animator.SetBool(animatorBoolParam, false);
    }

    public virtual void OnReceiveHit(float damage, Vector2 hitBackVelocity)
    {
        if (!CanReceiveHit)
            return;
        hero.HitBackState.SetHitBackVelocity(hitBackVelocity);
        stateMachine.ChangeState(hero.HitBackState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
