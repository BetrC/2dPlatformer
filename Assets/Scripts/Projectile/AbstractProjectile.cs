using System;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    public float damage;

    public ParticleSystem explosionEffect;

    public string sound;

    public GameObject Shooter { get; set; }

    protected Vector2 force;

    public event Action<AbstractProjectile> OnProjectileDestroyed;

    public abstract void SetForce(Vector2 force);

    protected abstract bool HitDestroySelf();

    protected void DestroyProjectile()
    {
        OnProjectileDestroyed?.Invoke(this);

        if (explosionEffect != null)
            EffectManager.Instance.PlayOneShot(explosionEffect, transform.position);
        if (sound != "")
            SoundManager.Instance.PlaySound(sound);

        DestroyImp();
    }

    protected virtual void DestroyImp()
    {
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

        if (HitDestroySelf())
            DestroyProjectile();
    }
}