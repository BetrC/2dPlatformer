using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageableActor : Actor, IDamageable, IHitBackable
{
    protected SpriteRenderer spriteRenderer;
    protected Health health;

    public void HitBack(Vector2 angle, float strength, float xDir)
    {
        // do nothing
    }

    public void TakeDamage(float damage)
    {

    }

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = gameObject.GetComponentInChildren<Health>();

        health.onHealthUpdated -= OnHealthUpdated;
        health.onHealthUpdated += OnHealthUpdated;

        health.onDie -= OnDie;
        health.onDie += OnDie;
    }

    protected virtual void OnDie()
    {

    }

    protected virtual void OnHealthUpdated(float curHealth, float deltaChange)
    {

    }
}
