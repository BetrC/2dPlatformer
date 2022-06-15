
using UnityEngine;

public class Projectile : AbstractProjectile
{
    public override void SetForce(Vector2 force)
    {
        this.force = force;
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    protected override bool HitDestroySelf()
    {
        return true;
    }
}
