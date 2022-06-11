using System;
using UnityEngine;
using UnityEngine.Events;

public class OnHealthUpdated : UnityEvent<float, float, float> { };
public class OnDie : UnityEvent { };

public class Health : MonoBehaviour
{
    private float _healthValue;
    public float maxHalthValue = 20;

    public OnHealthUpdated onHealthUpdated;
    public OnDie onDie;
    
    private bool isDead = false;

    private void Awake()
    {
        _healthValue = maxHalthValue;
        onHealthUpdated = new OnHealthUpdated();
        onDie = new OnDie();
        isDead = false;
    }

    public float HealthValue
    {
        get { return _healthValue; }
        set {
            if (_healthValue == value)
                return;
            float deltaValue = value - _healthValue;
            _healthValue = value;
            onHealthUpdated.Invoke(_healthValue, maxHalthValue, deltaValue);
        }
    }

    public void TakeDamage(float value)
    {
        if (isDead)
            return;
        HealthValue -= value;
        CLog.Log($"Object: {gameObject.name}, take {value} damage, Health now is {HealthValue}");
        CheckDie();
    }

    public void CheckDie()
    {
        if (HealthValue <= 0)
        {
            CLog.Log($"Object {gameObject.name} Die");
            isDead = true;
            onDie?.Invoke();
        }
    }

    public void Reset()
    {
        isDead = false;
        HealthValue = maxHalthValue;
    }
}