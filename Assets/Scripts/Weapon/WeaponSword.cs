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

    protected override void CheckAttackCast(Collider2D collision)
    {
        // only handle object implement IDamageable
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable == null)
            return;

        WeaponAttackConf conf = weaponData[lastCastSequence];

        // we check this object is being attacked or not in this attack session
        int instanceId = collision.gameObject.GetInstanceID();
        float nowTime = Time.time;
        if (damagablesHitTimeDic.TryGetValue(instanceId, out float lastTime))
        {
            if (nowTime < lastTime + conf.hitSameObjectInterval)
            {
                return;
            }
        }

        CameraManager.Instance.ShakeCamera(3, .1f);
        GameManager.Instance.HitFreezeTime();
        SoundManager.Instance.PlaySound(conf.hitSound);
        damageable.TakeDamage(conf.damageValue, conf.HitBackNormalDir, conf.hitBackStrength, (collision.transform.position.x - transform.position.x).Normalize());
        damagablesHitTimeDic[instanceId] = nowTime;
        ShowEffect(conf, collision.transform);
    }

    protected override void SetHeroMovementVelocity()
    {
        var conf = weaponData[lastCastSequence];
        state.SetVelocityX(conf.movementSpeed);
    }


    private void ShowEffect(WeaponAttackConf conf, Transform transform)
    {
        if (conf.hitEffect != null)
        {
            Instantiate(conf.hitEffect, transform.position, Quaternion.identity);
        }
    }
}