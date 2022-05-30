using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static AnimationParamString;

public abstract class Weapon : MonoBehaviour
{

    protected float lastCastTime;
    protected int lastCastSequence;

    protected HeroAttackState state;
    protected Animator animator;

    protected bool isAnimationEnd;

    /// <summary>
    /// 潜在击打列表
    /// </summary>
    protected List<Collider2D> p_damageList;


    public virtual void Init(HeroAttackState state)
    {
        this.state = state;
        lastCastTime = 0;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        p_damageList = new List<Collider2D>();
        gameObject.SetActive(false);
    }

    public virtual void Enter()
    {
        isAnimationEnd = false;
        gameObject.SetActive(true);
        animator.SetBool(BOOL_ATTACK, true);
    }

    public virtual void Exit()
    {
        animator.SetBool(BOOL_ATTACK, false);
        gameObject.SetActive(false);
    }

    public virtual void LogicUpdate() { }

    protected abstract void SetHeroMovementVelocity();

    protected abstract void CheckAttackCast();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        p_damageList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_damageList.Remove(collision);
    }

    #region 动画回调

    /// <summary>
    /// 自定义动画回调
    /// </summary>
    public virtual void AnimationTrigger()
    {
    }

    /// <summary>
    /// 动画打击点回调
    /// </summary>
    public void AnimationCastTrigger()
    {
        CheckAttackCast();
    }

    /// <summary>
    /// 动画结束点回调
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        isAnimationEnd = true;
        state.isAbilityDone = true;
    }

    /// <summary>
    /// 应用攻击位移
    /// </summary>
    public void AnimationStartMovementTrigger()
    {
        SetHeroMovementVelocity();
    }

    /// <summary>
    /// 停止攻击位移
    /// </summary>
    public void AnimationStopMovementTrigger()
    {
        state.SetVelocityX(0);
    }

    /// <summary>
    /// 开启检测角色转向
    /// </summary>
    public void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    /// <summary>
    /// 关闭角色转向
    /// </summary>
    public void AnimationTurnOnFlipTigger()
    {
        state.SetFlipCheck(true);
    }

    #endregion
}
