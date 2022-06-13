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

    protected Dictionary<int, float> damagablesHitTimeDic;

    public virtual void Init(HeroAttackState state)
    {
        this.state = state;
        lastCastTime = 0;
        damagablesHitTimeDic = new Dictionary<int, float>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        damagablesHitTimeDic.Clear();
    }

    public virtual void LogicUpdate() { }

    protected abstract void SetHeroMovementVelocity();

    protected abstract void CheckAttackCast(Collider2D collision);

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckAttackCast(collision);
    }

    public abstract bool IsTriggerWeapon();


    #region 动画回调

    /// <summary>
    /// 自定义动画回调
    /// </summary>
    public virtual void AnimationTrigger()
    {
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
