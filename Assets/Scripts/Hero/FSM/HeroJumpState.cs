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
        jumpCountLeft --;
        hero.movement.SetVelocityY(heroData.jumpSpeed);
        isAbilityDone = true;
        if (jumpParticle == null)
            jumpParticle = GameObject.Instantiate(heroData.jumpParticle, hero.transform);
        jumpParticle.Play();
    }

    public bool CanJump()
    {
        return jumpCountLeft > 0;
    }


    public bool IsTriggerJump()
    {
        return InputManger.Instance.JumpPressed && CanJump();
    }

    public void ResetJumpCount()
    {
        jumpCountLeft = heroData.canJumpTime;
    }
}
