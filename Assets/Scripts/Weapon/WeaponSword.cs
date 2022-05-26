using System;
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
        SetHeroVelocityX();
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

    public void SetHeroVelocityX()
    {
        var conf = weaponData[lastCastSequence];
        state.SetVelocityX(conf.movementSpeed);
    }

    protected override void CheckAttackCast()
    {
        base.CheckAttackCast();

        WeaponAttackConf conf = weaponData[lastCastSequence];
        // 倒序防止删除时出问题
        for(int i = p_damageList.Count - 1; i >= 0; i--)
        {
            var obj = p_damageList[i];
            IDamageable damageable = obj.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(conf.damageValue);
                ShowEffect(conf, obj.transform);
            }
        }
    }

    private void ShowEffect(WeaponAttackConf conf, Transform transform)
    {
        if (conf.hitEffect != null)
        {
            Instantiate(conf.hitEffect, transform.position, Quaternion.identity);
        }
        if (conf.hitBloodEffect != null)
        {
            Instantiate(conf.hitBloodEffect, transform.position, Quaternion.identity);
        }
    }
}