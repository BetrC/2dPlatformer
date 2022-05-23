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
    protected List<IDamageable> damageables;


    public virtual void Init(HeroAttackState state)
    {
        this.state = state;
        lastCastTime = 0;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageables.Add(damageable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageables.Remove(damageable);
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
