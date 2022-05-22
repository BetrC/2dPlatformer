using System;
using UnityEngine;
public class Health : MonoBehaviour
{
    private float _healthValue;
    public float maxHalthValue;

    private void Awake()
    {
        _healthValue = maxHalthValue;
    }

    public float HealthValue
    {
        get { return _healthValue; }
        set {
            if (_healthValue == value)
                return;
            float deltaValue = value - _healthValue;
            _healthValue = value;
            onHealthUpdated?.Invoke(_healthValue, deltaValue);
        }
    }

    public Action<float, float> onHealthUpdated;
    public Action onDie;

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