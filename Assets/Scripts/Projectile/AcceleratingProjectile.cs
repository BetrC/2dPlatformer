using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AcceleratingProjectile : AbstractProjectile
{
    public float speed = 5.0f;

    public float lifeTime = 4f;

    private Vector3 direction;
    private Vector3 velocity;

    private void Start()
    {
        Invoke(nameof(DestroyProjectile), lifeTime);
    }

    public override void SetForce(Vector2 force)
    {
        this.force = force;
        direction = force.normalized;
    }

    void Update()
    {
        velocity += direction * speed * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
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
    }
}
