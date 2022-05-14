using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/AttackConf")]
public class AttackConf : ScriptableObject
{
    [Header("招式名称")]
    public string skName = "";

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

    [Header("攻击层 默认Enemy")]
    public LayerMask hitLayerMask;

    [Header("Debug 显示范围")]
    public Color debugRangeColor = Color.cyan;

    public string AnimStateName => attackAnimation.name;


    private void Awake()
    {
        if (hitLayerMask == LayerMask.NameToLayer("None"))
            hitLayerMask = LayerMask.NameToLayer("Enemy");
        if (skName == "")
            skName = name;
    }
}
