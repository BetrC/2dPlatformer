using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{
    public ItemData data;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hero上有两个Collider，为了避免触发两次，这里只判断CircleCollider的碰撞
        CircleCollider2D collider = collision as CircleCollider2D;
        if (collider == null)
            return;

        Hero hero = collision.gameObject.GetComponent<Hero>();
        if (hero == null)
            return;

        ItemManager.Instance.UseItem(hero, data);

        if (data.sound != "")
        {
            SoundManager.Instance.PlaySound(data.sound);
        }
        if (data.particle != null)
            EffectManager.Instance.PlayOneShot(data.particle, transform.position);

        Destroy(gameObject);
    }
}

