using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageableActor : Actor, IDamageable
{
    protected Health health;


    /// <summary>
    /// 受击保护
    /// </summary>
    public float hitProtectionTime = 0f;

    [HideInInspector]
    public float hitProtectionTimeLeft;
    public bool IsHittable => hitProtectionTimeLeft <= 0;

    protected override void Awake()
    {
        hitProtectionTimeLeft = 0;
        base.Awake();
        health = GetComponent<Health>();
    }

    protected virtual void Start()
    {
        health.onHealthUpdated.AddListener(OnHealthUpdated);
        health.onDie.AddListener(OnDie);
    }

    protected virtual void Update()
    {
        if (hitProtectionTimeLeft > 0)
            hitProtectionTimeLeft -= Time.deltaTime;
    }

    private void SetHitProtection()
    {
        hitProtectionTimeLeft = hitProtectionTime;
    }

    public void HitBack(Vector2 angle, float strength, float xDir)
    {
        // do nothing
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0 || IsHittable)
        {
            health.TakeDamage(damage);
            if (damage > 0)
                SetHitProtection();
        }
    }

    public void TakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0)
    {
        TakeDamage(damage);
        if (IsHittable && damage > 0)
        {
            // Hit back
        }
    }

    protected virtual void OnDie()
    {

    }

    protected virtual void OnHealthUpdated(float curHealth, float deltaChange)
    {

    }
}
