using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/AttackConf")]
public class AttackConf : ScriptableObject
{

    [Header("�����λ��")]
    [Tooltip("��Խ�ɫ�����λ��")]
    public Vector2 hitPointOffset = Vector2.zero;
    
    [Header("��������")]
    public float attackRange = 0;

    [Header("�����˺�")]
    public float attackDamage = 0;

    [Header("�ν���һ������ʱ��")]
    public float linkTime = 0;

    [Header("��������")]
    public AnimationClip attackAnimation;

    public string AnimStateName => attackAnimation.name;

}
