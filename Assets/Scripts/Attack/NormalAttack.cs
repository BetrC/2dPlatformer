using UnityEngine;
using System.Collections;

public class NormalAttack : MonoBehaviour
{
    [Header("打击点")]
    public Transform hitPoint;

    [Header("Attack 1 Info")]
    public float attack1HitRange;
    public float attack1Damage;

    [Header("Attack 2 Info")]
    public float attack2HitRange;
    public float attack2Damage;

    [Header("Attack 3 Info")]
    public float attack3HitRange;
    public float attack3Damage;

    public LayerMask hitLayerMask;

    public void Attack1()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, attack1HitRange, hitLayerMask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Health>().TakeDamage(attack1Damage);
            LogUtility.Log("Normal Attack 1");
        }
    }

    public void Attack2()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, attack2HitRange, hitLayerMask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Health>().TakeDamage(attack2Damage);
            LogUtility.Log("Normal Attack 2");
        }
    }

    public void Attack3()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint.position, attack3HitRange, hitLayerMask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Health>().TakeDamage(attack3Damage);
            LogUtility.Log("Normal Attack 3");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitPoint.position, attack1HitRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitPoint.position, attack2HitRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPoint.position, attack3HitRange);
    }
}
