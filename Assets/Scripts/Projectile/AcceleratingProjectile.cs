using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AcceleratingProjectile : AbstractProjectile
{
    public float lifeTime = 4f;

    private Vector3 direction;
    public Vector3 velocity;

    public float acceleration = 5.0f;

    public bool hitDestroySelf = false;

    private void Start()
    {
        Invoke(nameof(DestroyProjectile), lifeTime);
    }

    public override void SetForce(Vector2 force)
    {
        this.force = force;
        direction = force.normalized;

        velocity.x *= force.x;
        velocity.y *= force.y;
    }
    protected override bool HitDestroySelf()
    {
        return hitDestroySelf;
    }

    void Update()
    {
        velocity += direction * acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
