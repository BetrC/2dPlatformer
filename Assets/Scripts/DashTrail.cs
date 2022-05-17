using UnityEngine;
using DG.Tweening;


public class DashTrail : MonoBehaviour
{
    public float trailInterval = .05f;
    public Color trailColor;
    public Color fadeColor;
    public float fadeTime = .5f;

    public void ShowTrail(Actor2D actor)
    {
        var actorSprite = actor.GetComponent<SpriteRenderer>();
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            sequence.AppendCallback(() =>
            {
                child.position = actor.transform.position;
            }
            )
                .AppendCallback(() => child.transform.rotation = actor.transform.rotation)
                .AppendCallback(() => spriteRenderer.sprite = actorSprite.sprite)
                .Append(spriteRenderer.material.DOColor(trailColor, 0f))
                .AppendCallback(() =>
                {
                    spriteRenderer.material.DOKill();
                    spriteRenderer.material.DOColor(fadeColor, fadeTime);
                }).AppendInterval(trailInterval);
        }
    }
}
