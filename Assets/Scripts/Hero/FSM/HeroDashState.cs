using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class HeroDashState : HeroAbilityState
{
    public DashTrail dashTrail;

    public int dashTime;

    public HeroDashState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dashTime--;
        if (dashTrail == null)
            dashTrail = GameObject.FindObjectOfType<DashTrail>();
        dashTrail.ShowTrail(hero);
        hero.movement.SetFacingDirVelocityX(heroData.dashSpeed);
        hero.movement.SetGravityScale(heroData.dashGravityScale);
        hero.movement.SetBetterJumpEnable(false);
    }


    public override void Exit()
    {
        base.Exit();
        hero.movement.SetGravityScale(heroData.defaultGravityScale);
        hero.movement.SetBetterJumpEnable(true);
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.DashPressed && CanDash();
    }

    private bool CanDash()
    {
        return dashTime > 0;
    }

    public void ResetDashTime()
    {
        dashTime = heroData.canDashTime;
    }

    public override void AnimationFinishTrigger()
    {
        isAbilityDone = true;
    }
}
