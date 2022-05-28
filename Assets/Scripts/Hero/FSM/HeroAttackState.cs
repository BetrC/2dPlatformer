using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : HeroAbilityState
{
    public Weapon weapon;

    public bool shouldCheckFlip;

    public HeroAttackState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        shouldCheckFlip = false;
        SetVelocityX(0f);
        CheckInputUsingWeapon();
        weapon.Enter();
    }

    public void CheckInputUsingWeapon()
    {
        Weapon useWeapon = hero.primaryWeaponInfo.weapon;
        if (InputManager.Instance.SecondryAttackHolding)
        {
            useWeapon = hero.secondaryWeaponInfo.weapon;
        }

        if (weapon != useWeapon)
        {
            SetWeapon(useWeapon);
        }
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
        weapon.LogicUpdate();

        if (shouldCheckFlip)
            hero.movement.CheckFlip(xNormalInput);
    }

    public void SetVelocityX(float vX)
    {
        hero.movement.SetFacingDirVelocityX(vX);
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.PrimaryAttackPressed || InputManager.Instance.SecondryAttackHolding;
    }

    public void SetFlipCheck(bool check)
    {
        shouldCheckFlip = check;
    }
}
