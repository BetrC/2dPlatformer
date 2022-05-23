using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    [HideInInspector]
    public int AttackNum;

    public WeaponAttackConf[] WeaponAttackConfs;

    public WeaponAttackConf this [int index] => WeaponAttackConfs[index];

    private void Awake()
    {
        AttackNum = WeaponAttackConfs.Length;
    }
}
