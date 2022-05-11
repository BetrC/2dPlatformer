using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/AttackConf")]
public class AttackConf : ScriptableObject
{

    [Header("打击点位移")]
    [Tooltip("相对角色坐标的位移")]
    public Vector2 hitPointOffset = Vector2.zero;
    
    [Header("攻击距离")]
    public float attackRange = 0;

    [Header("攻击伤害")]
    public float attackDamage = 0;

    [Header("衔接下一攻击的时间")]
    public float linkTime = 0;

    [Header("攻击动画")]
    public AnimationClip attackAnimation;

    public string AnimStateName => attackAnimation.name;

}
