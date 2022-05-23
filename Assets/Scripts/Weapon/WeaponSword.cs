using UnityEngine;
using static AnimationParamString;

public class WeaponSword : Weapon
{
    public WeaponData weaponData;

    public override void Init(HeroAttackState state)
    {
        base.Init(state);
        lastCastSequence = weaponData.AttackNum - 1;
    }

    public override void Enter()
    {
        base.Enter();
        CalculateCurAttackSequence();
        animator.SetInteger(INT_ATTACK_SEQUENCE, lastCastSequence);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void CalculateCurAttackSequence()
    {
        WeaponAttackConf lastConf = weaponData[lastCastSequence];

        float curTime = Time.time;
        int nextCastSequence = (lastCastSequence + 1) % weaponData.AttackNum;
        if (curTime > lastCastTime + lastConf.attackLinkTime)
        {
            nextCastSequence = 0;
        }
        lastCastSequence = nextCastSequence;
        lastCastTime = curTime;
    }

    protected override void CheckAttackCast()
    {
        base.CheckAttackCast();

    }
}