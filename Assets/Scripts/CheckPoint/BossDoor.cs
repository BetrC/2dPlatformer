using UnityEngine;

public class BossDoor :EndPoint
{
    private void Start()
    {
        FindObjectOfType<Boss>().health.onDie.AddListener(OnBossDie);
        gameObject.SetActive(false);
    }

    private void OnBossDie()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is CircleCollider2D && collision.CompareTag("Player"))
        {
            GameUIManager.Instance.ShowFinishUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision is CircleCollider2D && collision.CompareTag("Player"))
        {
            GameUIManager.Instance.HideFinishUI();
        }
    }

    protected override void Leave()
    {
        GameManager.Instance.ReturnToMenu();
        GameUIManager.Instance.HideFinishUI();
    }

}
