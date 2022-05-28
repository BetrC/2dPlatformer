using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public int AttackNum => WeaponAttackConfs.Length;

    public WeaponAttackConf[] WeaponAttackConfs;

    public WeaponAttackConf this [int index] => WeaponAttackConfs[index];

}
