using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum FightNumType
{
    HeroHit,
    EnemyHit,
    HeroCure,
}

public class FightNumber : MonoBehaviour
{
    public Text text;


    public FightNumberConfig enemyHitconfig;

    public FightNumberConfig heroHitConfig;

    public FightNumberConfig heroCureConfig;


    private FightNumberConfig GetConfigByHitType(FightNumType type)
    {
        switch (type)
        {
            case FightNumType.HeroHit:
                    return heroHitConfig;
                case FightNumType.EnemyHit:
                    return enemyHitconfig;
                case FightNumType.HeroCure:
                    return heroCureConfig;
        }
        return enemyHitconfig;
    }

    public void Begin(float value, Transform receiver, FightNumType hitType)
    {

        text.text = hitType == FightNumType.HeroCure ? "+" + value.ToString() : value.ToString();
        text.gameObject.SetActive(true);

        var config = GetConfigByHitType(hitType);

        Vector2 screenPos = Camera.main.WorldToScreenPoint(receiver.position);

        Sequence s = DOTween.Sequence();
        s.AppendCallback(() =>
        {
            text.transform.position = screenPos + config.initPos;
            text.transform.localScale = config.initScale;
            text.color = config.initColor;
        })
            .AppendCallback(() =>
            {
                text.transform.DOMove(screenPos + config.targetPos, config.moveTime).SetEase(config.moveCurve);
            })
            .AppendCallback(() =>
            {
                text.transform.DOScale(config.targetScale, config.scaleTime).SetEase(config.scaleCurve);
            }).AppendCallback(() =>
            {
                text.DOColor(config.targetColor, config.colorTime).SetEase(config.colorCurve);
            })
            .AppendInterval(Mathf.Max(config.moveTime, config.scaleTime, config.colorTime))
        .AppendCallback(() =>
        {
            Release();
        });
    }

    private void Release()
    {
        text.gameObject.SetActive(false);
    }

}
