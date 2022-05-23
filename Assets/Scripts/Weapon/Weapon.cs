using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static AnimationParamString;

public class Weapon : MonoBehaviour
{

    public float lastCastTime;
    public int lastCastSequence;

    protected HeroAttackState state;

    protected Animator animator;

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
        gameObject.SetActive(true);
        animator.SetBool(BOOL_ATTACK, true);
    }

    public virtual void Exit()
    {
        animator.SetBool(BOOL_ATTACK, false);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        p_damageList.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p_damageList.Remove(collision);
    }

    protected virtual void CheckAttackCast()
    {

    }

    public virtual void AnimationCastTrigger()
    {
        CheckAttackCast();
    }

    public void AnimationFinishTrigger()
    {
        state.isAbilityDone = true;
    }
}
