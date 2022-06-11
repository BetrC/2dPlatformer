using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthBar;

        public DamageableActor bindActor;

        public Gradient gradient;
        public Image fill;

        private void Awake()
        {
            healthBar = GetComponent<Slider>();
        }

        public void Bind(DamageableActor actor)
        {
            gameObject.SetActive(true);
            bindActor = actor;
        }

        public void UnBind(DamageableActor actor)
        {
            bindActor = null;
            UIHealthManager.Instance.Release(this);
        }
        
        private void Update()
        {
            // 跟随角色位置
            if (bindActor != null)
            {
                transform.position = bindActor.GetHeadPosition();
            }
        }

        public void OnHealthUpdated(float curHealth, float maxHealth, float deltaChange)
        {
            float percent = Mathf.Clamp01(curHealth / maxHealth);
            healthBar.DOComplete();
            healthBar.DOValue(percent, .3f, false);
            fill.color = gradient.Evaluate(percent);
        }

    }
}