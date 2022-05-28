using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeaponChargeSword : WeaponSword
{
    private bool isCharging;

    public override void Enter()
    {
        base.Enter();
        isCharging = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isCharging && !InputManager.Instance.SecondryAttackHolding)
        {
            state.isAbilityDone = true;
        }
    }


    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isCharging = false;
    }
}
