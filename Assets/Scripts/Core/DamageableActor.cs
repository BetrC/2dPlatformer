using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageableActor : Actor, IDamageable
{
    public Health health;
    
    /// <summary>
    /// 受击保护
    /// </summary>
    public float hitProtectionTime = 0f;

    private float hitProtectionTimeLeft;

    protected Transform boneHead;

    protected Vector2 substitute;

    protected HealthBar healthBar;

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
        BindHealthBar();
    }

    protected virtual void BindHealthBar()
    {
        Bind(UIHealthManager.Instance.GetHealthBarFromPool());
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

    public virtual bool IsHittable() => hitProtectionTimeLeft <= 0 && !health.Dead;

    public void TakeDamage(float damage, Vector2 angle = default, float strength = 0, int xDir = 0)
    {
        if (damage <= 0 || IsHittable())
        {
            if (damage > 0)
                OnTakeDamage(damage, angle, strength, xDir);
            health.TakeDamage(damage);
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

    public void Bind(HealthBar healthBar, bool followTarget = true)
    {
        this.healthBar = healthBar;
        this.healthBar.Bind(this, followTarget);
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
