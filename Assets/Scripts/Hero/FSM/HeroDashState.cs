using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class HeroDashState : HeroAbilityState
{
    public int dashTime;

    public HeroDashState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dashTime--;
        DashTrail.Instance.ShowTrail(hero);

        hero.movement.SetFacingDirVelocityX(heroData.dashSpeed);
        hero.movement.SetGravityScale(heroData.dashGravityScale);
        hero.movement.SetBetterJumpEnable(false);
        SoundManager.Instance.PlaySound("hero_dash");
        CanReceiveHit = false;
    }


    public override void Exit()
    {
        base.Exit();
        hero.movement.SetGravityScale(heroData.defaultGravityScale);
        hero.movement.SetBetterJumpEnable(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (hero.WallSlideState.IsOnWall())
        {
            stateMachine.ChangeState(hero.WallSlideState);
        }
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.DashPressed && CanDash();
    }

    private bool CanDash()
    {
        return dashTime > 0 && AbilityManager.Instance.IsAbilityActive(Ability.Dash);
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
