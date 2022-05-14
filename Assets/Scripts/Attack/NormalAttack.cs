using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalAttack : MonoBehaviour
{
    public List<AttackConf> confList;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lastCastConfIndex = confList.Count - 1;
    }

    private long lastTimestamp = 0;
    private int lastCastConfIndex = -1;


    public void TriggerAttack()
    {
        if (!CanCast())
            return;

        AttackConf conf = confList[lastCastConfIndex];
        long curTimestamp = MiscUtility.GetNowUnixTimeSeconds();
        int nextCastConfIndex = (lastCastConfIndex + 1) % confList.Count;

        if (curTimestamp > lastTimestamp + conf.linkTime)
            nextCastConfIndex = 0;
        conf = confList[nextCastConfIndex];

        // playAnim
        animator.Play(conf.AnimStateName);
        // record last cast attack
        lastTimestamp = curTimestamp;
        lastCastConfIndex = nextCastConfIndex;
    }

    
    // 是否在播放攻击动画
    public bool IsPlayingAttackAnim
    {
        get
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            foreach (AttackConf conf in confList)
            {
                if (stateInfo.IsName(conf.AnimStateName))
                {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// 当前状态是否可释放攻击
    /// </summary>
    /// <returns></returns>
    private bool CanCast()
    {
        if (confList.Count == 0)
            return false;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (IsPlayingAttackAnim && stateInfo.normalizedTime < .8f)
            return false;
        return true;
    }

    public void CastAttack(AttackConf attackConf)
    {
        //AttackConf conf = attackConf as AttackConf;
        if (attackConf == null)
        {
            LogUtility.LogWarning($"Skill Name [{attackConf.skName} conf not exist!]");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + (Vector3)attackConf.hitPointOffset, attackConf.attackRange, attackConf.hitLayerMask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Health>().TakeDamage(attackConf.attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        foreach(AttackConf attackConf in confList)
        {
            Gizmos.color = attackConf.debugRangeColor;
            Gizmos.DrawWireSphere(transform.position + (Vector3)attackConf.hitPointOffset, attackConf.attackRange);
        }
    }
}
