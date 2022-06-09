using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
            return;
        GameManager.Instance.RecordRespawnPoint(transform);
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)collider2D.offset, collider2D.size);
    }
}