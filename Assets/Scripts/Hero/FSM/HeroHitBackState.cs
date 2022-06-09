using UnityEngine;


/// <summary>
/// 击退
/// </summary>
public class HeroHitBackState : HeroAbilityState
{
    //public bool hitTime;

    public Vector2 hitBackVelocity;
    
    public HeroHitBackState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hero.movement.SetVelocity(hitBackVelocity);
        SoundManager.Instance.PlaySound("hero_hit");
    }

    public override void Exit()
    {
        base.Exit();
        hitBackVelocity.Set(0, 0);
    }

    public void SetHitBackVelocity(Vector2 v)
    {
        hitBackVelocity.Set(v.x, v.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;
    }



    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
}

