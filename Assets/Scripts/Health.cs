using System;
using UnityEngine;
using UnityEngine.Events;

public class OnHealthUpdated : UnityEvent<float, float> { };
public class OnDie : UnityEvent { };

public class Health : MonoBehaviour
{
    private float _healthValue;
    public float maxHalthValue = 20;

    public OnHealthUpdated onHealthUpdated;
    public OnDie onDie;

    private void Awake()
    {
        _healthValue = maxHalthValue;
        onHealthUpdated = new OnHealthUpdated();
        onDie = new OnDie();
    }

    public float HealthValue
    {
        get { return _healthValue; }
        set {
            if (_healthValue == value)
                return;
            float deltaValue = value - _healthValue;
            _healthValue = value;
            onHealthUpdated.Invoke(_healthValue, deltaValue);
        }
    }

    public void TakeDamage(float value)
    {
        HealthValue -= value;
        CLog.Log($"Object: {gameObject.name}, take {value} damage, Health now is {HealthValue}");
        CheckDie();
    }

    public void CheckDie()
    {
        if (HealthValue <= 0)
        {
            CLog.Log($"Object {gameObject.name} Die, Destroy Object");
            onDie?.Invoke();
            //Destroy(gameObject);
        }
    }

}