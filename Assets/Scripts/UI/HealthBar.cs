using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    public DamageableActor bindActor;

    public Gradient gradient;
    public Image fill;
    public Image fillBack;

    private bool followTarget = true;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    public void Bind(DamageableActor actor, bool _followTarget = true)
    {
        gameObject.SetActive(true);
        bindActor = actor;
        followTarget = _followTarget;
    }

    public void UnBind(DamageableActor actor)
    {
        bindActor = null;
        UIHealthManager.Instance.Release(this);
    }
        
    private void Update()
    {
        // 跟随角色位置
        if (bindActor != null && followTarget)
        {
            transform.position = bindActor.GetHeadPosition();
        }
    }

    public void OnHealthUpdated(float curHealth, float maxHealth, float deltaChange)
    {
        float percent = Mathf.Clamp01(curHealth / maxHealth);
        healthBar.value = percent;
        fill.color = gradient.Evaluate(percent);

        fillBack.DOComplete();
        fillBack.DOFillAmount(percent, .5f);
    }

}