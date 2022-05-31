using System.Collections;
using UnityEngine;
using TheKiwiCoder;

public class EnemyAttack : ActionNode
{

    public string attackClipName;

    public float damage = 2f;
    public float damageInterval = .1f;

    private float animationLength;
    private float startTime;

    protected override void OnStart()
    {
        context.enemy.SetHazardDamageInfo(damage, damageInterval);
        context.animator.ResetAllTrigger();
    context.animator.Play(attackClipName);
        // 需要限制状态的名称和动画名称一样
        animationLength = context.animator.GetAnimationClipLength(attackClipName);
        startTime = Time.time;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Time.time >= startTime + animationLength)
            return State.Success;
        return State.Running;
    }
}