using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hazard : MonoBehaviour
{

    public float damage;    

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckCollision(collision.gameObject);
    }

    private void CheckCollision(GameObject collider)
    {
        if (collider.layer != LayerMask.NameToLayer("Player"))
            return;
        Hero hero = collider.GetComponent<Hero>();
        if (hero == null || !hero.IsHittable())
            return;
        
        // 反冲方向
        Vector2 recoilDirection = (collider.transform.position - transform.position).normalized;
        hero.TakeDamage(damage, recoilDirection, hero.heroData.hitBackSpeed, 1);
    }
}
