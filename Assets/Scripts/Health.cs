using System;
using UnityEngine;
public class Health : MonoBehaviour
{
    private float _healthValue;
    public float maxHalthValue;

    private void Awake()
    {
        HealthValue = maxHalthValue;
    }

    public float HealthValue
    {
        get { return _healthValue; }
        set {
            if (_healthValue == value)
                return;
            _healthValue = value;
            onHealthUpdated?.Invoke(_healthValue);
        }
    }

    public Action<float> onHealthUpdated;

    public void TakeDamage(float value)
    {
        HealthValue -= value;
        LogUtility.Log($"Object: {gameObject.name}, take {value} damage, Health now is {HealthValue}");
        CheckDie();
    }

    public void CheckDie()
    {
        if (HealthValue <= 0)
        {
            LogUtility.Log($"Object {gameObject.name} Die, Destroy Object");
            Destroy(gameObject);
        }
    }

}