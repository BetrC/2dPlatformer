using System;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    public float damage;

    public ParticleSystem explosionEffect;

    public GameObject Shooter { get; set; }

    protected Vector2 force;

    public event Action<AbstractProjectile> OnProjectileDestroyed;

    public abstract void SetForce(Vector2 force);

    protected void DestroyProjectile()
    {
        OnProjectileDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Can't hurt yourself
        if (collision.gameObject == Shooter)
            return;

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage, Vector2.zero, 0);
        }

        DestroyProjectile();
    }
}