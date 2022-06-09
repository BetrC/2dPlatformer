using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
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
        DamageableActor actor = collider.GetComponent<DamageableActor>();
        actor.DieDirectly();
    }


    private void OnDrawGizmos()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( transform.position + (Vector3)collider.offset, collider.size );
    }
}