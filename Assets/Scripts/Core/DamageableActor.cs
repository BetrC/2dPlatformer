using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeEditor;
using UI;
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

    [HideInInspector]
    private Transform boneHead;

    protected Vector2 substitute;

    private HealthBar healthBar;

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
        boneHead = transform.Find("Head");
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
    
    /// 调用该方法直接即死
    public void DieDirectly()
    {
        health.TakeDamage(health.maxHalthValue);
    }

    public Vector3 GetHeadPosition()
    {
        if (boneHead == null)
            return transform.position;
        return boneHead.transform.position;
    }

    public void Bind(HealthBar healthBar)
    {
        this.healthBar = healthBar;
        this.healthBar.Bind(this);
        this.healthBar.OnHealthUpdated(health.HealthValue, health.maxHalthValue, 0);
    }

    public void Unbind(HealthBar healthBar)
    {
        if (healthBar != null)
            healthBar.UnBind(this);
    }

    protected virtual void OnTakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0)
    {
        SetHitProtection();
    }

    protected virtual void OnDie()
    {
        if(healthBar != null)
            Unbind(healthBar);
    }

    protected virtual void OnHealthUpdated(float curHealth, float maxHealth, float deltaChange)
    {
        if (healthBar != null)
            healthBar.OnHealthUpdated(curHealth, maxHealth, deltaChange);
    }
}
