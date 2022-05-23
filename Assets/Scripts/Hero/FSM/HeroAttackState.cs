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

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.Init(this);
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.AttackPressed;
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

}
