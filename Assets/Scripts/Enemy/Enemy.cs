using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[RequireComponent(typeof(BehaviourTreeRunner))]
public class Enemy : DamageableActor
{
    public Collider2D hitBox;
    public Hazard hazard;

    public Blackboard Blackboard => runner.tree.blackboard;

    private BehaviourTreeRunner runner;
    private string hitBoxTransName = "HitBox";

    protected override void Awake()
    {
        base.Awake();
        hitBox = transform.Find(hitBoxTransName).GetComponent<Collider2D>();
        hazard = GetComponentInChildren<Hazard>();
        runner = GetComponent<BehaviourTreeRunner>();
    }

    protected override void Update()
    {
        base.Update();
        SetBlackBoradValue();
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
}
