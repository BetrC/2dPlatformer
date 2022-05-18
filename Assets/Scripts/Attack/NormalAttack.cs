using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalAttack : MonoBehaviour
{
    public List<AttackConf> confList;

    private Animator animator;
    private Transform casterTrans;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        casterTrans = transform.parent;
        lastCastConfIndex = confList.Count - 1;
    }

    private long lastTimestamp = 0;
    private int lastCastConfIndex = -1;


    public void TriggerAttack()
    {
        if (!CanCast())
            return;

        AttackConf conf = confList[lastCastConfIndex];
        long curTimestamp = MiscUtility.GetNowUnixTimeMilliSeconds();
        int nextCastConfIndex = (lastCastConfIndex + 1) % confList.Count;

        if (curTimestamp > lastTimestamp + (long)(conf.linkTime * 1000))
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
    public bool CanCast()
    {
        if (confList.Count == 0)
            return false;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (IsPlayingAttackAnim && stateInfo.normalizedTime < .8f)
            return false;
        return true;
    }

    private Vector3 GetHitPoint(AttackConf conf)
    {
        return casterTrans.position + ((Vector3)conf.hitPointOffset).Multiple(casterTrans.right);
    }

    private void OnDrawGizmos()
    {
        if (casterTrans == null)
            casterTrans = transform.parent;
        foreach(AttackConf attackConf in confList)
        {
            Gizmos.color = attackConf.debugRangeColor;
            Gizmos.DrawWireSphere(GetHitPoint(attackConf), attackConf.attackRange);
        }
    }
}
