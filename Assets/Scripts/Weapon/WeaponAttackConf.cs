using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class WeaponAttackConf
{
    public string attackName;

    /// <summary>
    /// 攻击伤害
    /// </summary>
    public float damageNum = 10;

    /// <summary>
    /// 攻击位移
    /// </summary>
    public float movementSpeed = 3f;

    /// <summary>
    /// 击退
    /// </summary>
    public float hitBackStrength = 3f;

    public float attackLinkTime = 2f;

    /// <summary>
    /// 击退方向
    /// </summary>
    public Vector2 hitBackDir = Vector2.one;
}

