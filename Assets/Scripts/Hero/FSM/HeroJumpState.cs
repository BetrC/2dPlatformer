using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumpState : HeroAbilityState
{
    public int jumpCountLeft;

    private ParticleSystem jumpParticle;

    public HeroJumpState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpCountLeft--;
        hero.movement.SetVelocityY(heroData.jumpSpeed);
        isAbilityDone = true;

        ShowJumpParticle();
        SoundManager.Instance.PlaySound("hero_jump");
    }

    public bool CanJump()
    {
        return jumpCountLeft > 0;
    }


    public override bool TriggeredAbility()
    {
        //Debug.Log(jumpCountLeft);
        return InputManager.Instance.JumpPressed && CanJump();
    }

    internal void DecreaseJumpTime()
    {
        jumpCountLeft--;
    }

    public void ResetJumpCount()
    {
        jumpCountLeft = AbilityManager.Instance.IsAbilityActive(Ability.SecondJump) ? 2 : 1;
    }

    public void ShowJumpParticle()
    {
        if (jumpParticle == null)
            jumpParticle = GameObject.Instantiate(heroData.jumpParticle, hero.transform);
        jumpParticle.Play();
    }
}
