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
    public float damageValue = 10;

    /// <summary>
    /// 攻击位移
    /// </summary>
    public float movementSpeed = 3f;

    /// <summary>
    /// 攻击同一对象的间隔时间(同一对象在该时间内不会受到同一攻击多次)
    /// </summary>
    public float hitSameObjectInterval = .2f;

    /// <summary>
    /// 击退
    /// </summary>
    public float hitBackStrength = 3f;

    public float attackLinkTime = 2f;

    public ParticleSystem hitEffect;

    /// <summary>
    /// 击退方向
    /// </summary>
    public Vector2 hitBackDir = Vector2.one;

    public Vector2 HitBackNormalDir => hitBackDir.normalized;
}

