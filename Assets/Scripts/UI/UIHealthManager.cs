﻿using System;
using UI;
using UnityEngine;

public class UIHealthManager : MonoSingleton<UIHealthManager>
{
    public ObjectPool HealthBarPool;

    public HealthBar GetHealthBarFromPool()
    {
        return HealthBarPool.GetFromPool().GetComponent<HealthBar>();
    }

    public void Release(HealthBar healthBar)
    {
        HealthBarPool.Release(healthBar.gameObject);
    }

    public void Reset()
    {
        HealthBarPool.Reset();
    }

    private void Update()
    {
        
    }
}