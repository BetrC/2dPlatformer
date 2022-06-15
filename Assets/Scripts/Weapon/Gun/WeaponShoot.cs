
using UnityEngine;

public class WeaponShoot : Weapon
{
    public float shootInterval;

    public Transform shootPointTrans;

    public ObjectPool pool;

    private float lastShootTime = 0f;


    public override void Init(HeroAttackState state)
    {
        base.Init(state);
        pool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
    }

    public override bool IsTriggerWeapon()
    {
        return InputManager.Instance.ShootHolding && AbilityManager.Instance.IsAbilityActive(Ability.Shoot);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsTriggerWeapon())
        {
            state.isAbilityDone = true;
            return;
        }

        if (Time.time >= lastShootTime + shootInterval)
        {
            ShootOneBullet();
            lastShootTime = Time.time;
        }
    }

    public void ShootOneBullet()
    {
        GameObject obj = pool.GetFromPool();
        AbstractProjectile bullet = obj.GetComponent<BulletProjectile>();
        bullet.transform.position = shootPointTrans.position;
        bullet.transform.rotation = shootPointTrans.rotation;
        bullet.Shooter = GameManager.Instance.hero.gameObject;
        bullet.SetForce(new Vector2(state.FacingDirection, 0));
        bullet.gameObject.SetActive(true);


        /// TODO PlaySound
        SoundManager.Instance.PlaySound("shoot");
    }


    protected override void CheckAttackCast(Collider2D collision)
    {
        return;
    }

    protected override void SetHeroMovementVelocity()
    {
        return;
    }
}
