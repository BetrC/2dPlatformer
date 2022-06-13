using UnityEngine;

public class FightNumberManager : MonoSingleton<FightNumberManager>
{
    public ObjectPool Pool;

    public void ShowFightNumber(float value, Transform receiver, FightNumType type)
    {
        GameObject obj = Pool.GetFromPool();
        FightNumber number = obj.GetComponent<FightNumber>();
        obj.SetActive(true);
        number.Begin(value, receiver, type);
    }
}
