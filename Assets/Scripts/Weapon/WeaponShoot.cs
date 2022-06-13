
using UnityEngine;

public class WeaponShoot : Weapon
{
    public override bool IsTriggerWeapon()
    {
        return InputManager.Instance.ShootHolding && AbilityManager.Instance.IsAbilityActive(Ability.Shoot);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
