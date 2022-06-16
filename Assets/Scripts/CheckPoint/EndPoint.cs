
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class EndPoint : MonoBehaviour
{
    public bool hasTriggered = false;

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (hasTriggered)
            return;
        if (collision is CircleCollider2D && collision.CompareTag("Player") && InputManager.Instance.LeavePressed)
        {
            hasTriggered = true;
            Leave();
        }
    }

    protected virtual void Leave()
    {
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        GameUIManager.Instance.HideTips();
    }
}