using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TipsArea : MonoBehaviour
{
    public TipsData data;

    public int maxTriggerTime = 3;
    private int triggerTime = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerTime >= maxTriggerTime || !collision.CompareTag("Player"))
            return;

        // 玩家身上有两个collider，只检测CircleCollider的触发
        CircleCollider2D collider2D = collision as CircleCollider2D;
        if (collider2D == null)
            return;

        GameUIManager.Instance.ShowTips(data);
        triggerTime++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        GameUIManager.Instance.HideTips();
    }
}
