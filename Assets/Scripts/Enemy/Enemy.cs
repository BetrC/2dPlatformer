using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[RequireComponent(typeof(BehaviourTreeRunner))]
public class Enemy : DamageableActor
{
    [HideInInspector]
    public Collider2D hitBox;
    
    [HideInInspector]
    public Hazard hazard;

    [HideInInspector]
    public Movement movement;

    public Blackboard Blackboard => runner.tree.blackboard;

    private BehaviourTreeRunner runner;
    private string hitBoxTransName = "HitBox";

    protected override void Awake()
    {
        base.Awake();
        hitBox = transform.Find(hitBoxTransName).GetComponent<Collider2D>();
        hazard = GetComponentInChildren<Hazard>();
        runner = GetComponent<BehaviourTreeRunner>();
        movement = GetComponentInChildren<Movement>();
    }

    protected override void Start()
    {
        base.Start();
        Bind(UIHealthManager.Instance.GetHealthBarFromPool());
    }

    protected override void Update()
    {
        base.Update();
        movement.LogicUpdate();
        SetBlackBoradValue();
    }

    protected override void OnTakeDamage(float damage, Vector2 angle = default, float strength = 0, float xDir = 0)
    {
        base.OnTakeDamage(damage, angle, strength, xDir);
        substitute.Set(angle.x * xDir, angle.y);
        substitute.Normalize();
        substitute *= strength;

        Blackboard.BeHit = true;
        Blackboard.HitBackVelocity = substitute;
    }


    private void SetBlackBoradValue()
    {
        Blackboard.CurHealth = health.HealthValue;
        Blackboard.MaxHealth = health.maxHalthValue;
    }

    /// <summary>
    /// 某一攻击的伤害数值
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageInterval"></param>
    public void SetHazardDamageInfo(float damage, float damageInterval = .1f)
    {
        if (hazard != null)
        {
            hazard.damage = damage;
            hazard.damageInterval = damageInterval;
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        Blackboard.Dead = true;
    }
}
