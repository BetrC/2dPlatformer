using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageableActor : Actor, IDamageable, IHitBackable
{
    protected Health health;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        health.onHealthUpdated.AddListener(OnHealthUpdated);
        health.onDie.AddListener(OnDie);
    }

    public void HitBack(Vector2 angle, float strength, float xDir)
    {
        // do nothing
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    protected virtual void OnDie()
    {

    }

    protected virtual void OnHealthUpdated(float curHealth, float deltaChange)
    {

    }
}
