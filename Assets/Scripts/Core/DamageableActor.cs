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

    protected Vector2 substitute;

    public float Width
    {
        get
        {
            if (boxCollider == null)
                return 0f;
            return boxCollider.size.x;
        }
    }

    private BoxCollider2D boxCollider;

    protected override void Awake()
    {
        hitProtectionTimeLeft = 0;
        base.Awake();
        health = GetComponent<Health>();
        boxCollider = GetComponent<BoxCollider2D>();
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

    public virtual bool IsHittable() => hitProtectionTimeLeft <= 0;


    public void TakeDamage(float damage, Vector2 angle = default, float strength = 0, int xDir = 0)
    {
        if (damage <= 0 || IsHittable())
        {
            health.TakeDamage(damage);
            if (damage > 0)
                OnTakeDamage(damage, angle, strength, xDir);
        }
    }


    protected virtual void OnTakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0)
    {
        SetHitProtection();
    }

    protected virtual void OnDie()
    {

    }

    protected virtual void OnHealthUpdated(float curHealth, float deltaChange)
    {

    }
}
