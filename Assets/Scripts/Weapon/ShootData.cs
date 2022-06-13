using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/ShootData")]
public class ShootData : ScriptableObject
{

    public float shootInterval = .3f;
    public AbstractProjectile projectile;
}
