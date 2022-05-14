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
}
