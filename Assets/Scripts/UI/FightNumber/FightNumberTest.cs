using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FightNumberTest : MonoBehaviour
{
    public Text text;
    public Button button;
    public Transform head;
    int count = 0;
    public FightNumType type;

    public FightNumberConfig enemyHitconfig;

    public FightNumberConfig heroHitConfig;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        count++;
        Begin(count, head, type);
    }

    private FightNumberConfig GetConfigByHitType(FightNumType type)
    {
        switch (type)
        {
            case FightNumType.HeroHit:
                return heroHitConfig;
            case FightNumType.EnemyHit:
                return enemyHitconfig;
        }
        return enemyHitconfig;
    }

    public void Begin(int num, Transform caster, FightNumType hitType)
    {
        text.text = num.ToString();
        text.gameObject.SetActive(true);

        var config = GetConfigByHitType(hitType);

        Sequence s = DOTween.Sequence();
        s.AppendCallback(() =>
        {
            text.transform.position = (Vector2)caster.transform.position + config.initPos;
            text.transform.localScale = config.initScale;
            text.color = config.initColor;
        })
            .AppendCallback(() =>
            {
                text.transform.DOMove((Vector2)caster.transform.position + config.targetPos, config.moveTime).SetEase(config.moveCurve);
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
