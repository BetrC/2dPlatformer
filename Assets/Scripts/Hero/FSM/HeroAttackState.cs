using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : HeroAbilityState
{

    /// <summary>
    /// µ±Ç°¹¥»÷ÐòÁÐ
    /// </summary>
    public int curSequence;

    public Weapon weapon;

    public HeroAttackState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {

    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.Exit();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.Init(this);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public void SetVelocityX(float vX)
    {
        hero.movement.SetVelocityX(hero.movement.FacingDirection * vX);
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.AttackPressed;
    }
}
