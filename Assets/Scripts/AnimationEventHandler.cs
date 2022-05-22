using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public void HandleEvent(Object @object)
    {
        if (@object is AttackConf)
        {
            CastAttack(@object as AttackConf);
        }
    }

    private void CastAttack(AttackConf attackConf)
    {
        if (attackConf == null)
        {
            CLog.LogWarning($"Skill Name [{attackConf.skName} conf not exist!]");
            return;
        }

        Vector3 hitPoint = transform.position + ((Vector3)attackConf.hitPointOffset).Multiple(transform.right);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint, attackConf.attackRange, attackConf.hitLayerMask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Health>().TakeDamage(attackConf.attackDamage);
        }
    }
}
