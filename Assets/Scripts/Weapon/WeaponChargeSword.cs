using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponChargeSword : WeaponSword
{
    private bool isCharging;

    public float animationXOffset = -0.825f;

    public override void Enter()
    {
        base.Enter();
        isCharging = true;
    }

    public override void Exit()
    {
        base.Exit();
        CheckSetPositionOffset();
    }

    private void CheckSetPositionOffset()
    {
        if (isAnimationEnd)
        {
            state.SetHeroPositionByXOffset(animationXOffset);
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isCharging && !InputManager.Instance.SecondryAttackHolding)
        {
            state.isAbilityDone = true;
        }
    }

    public override bool IsTriggerWeapon()
    {
        return InputManager.Instance.SecondryAttackHolding && AbilityManager.Instance.IsAbilityActive(Ability.SecondAttack);
    }


    /// <summary>
    /// 这个触发器表示蓄力完成
    /// </summary>
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isCharging = false;
    }
}
