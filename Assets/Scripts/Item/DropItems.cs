using UnityEngine;
using DG.Tweening;
using System;

[Serializable]
public class DropData
{
    public GameObject item;

    [Range(0f, 1f)]
    public float dropRate;

    public int minNum;

    public int maxNum;
}


[RequireComponent(typeof(DamageableActor))]
public class DropItems : MonoBehaviour
{
    public DropData[] drops;


    [Range(1, 10)]
    public float xOffsetMax = 3;

    [Range(1, 10)]
    public float yOffsetMax = 3;

    private void Start()
    {
        GetComponent<DamageableActor>().health.onDie.AddListener(DropItem);
    }

    public void DropItem()
    {
        foreach(DropData data in drops)
        {
            float rate = UnityEngine.Random.Range(0f, 1f);
            if (rate > data.dropRate)
                continue;

            int count = UnityEngine.Random.Range(data.minNum, data.maxNum);
            for (int i = 1; i <= count; i++)
            {
                GameObject obj = Instantiate(data.item, transform.position, Quaternion.identity);

                float xOffset = UnityEngine.Random.Range(-xOffsetMax, xOffsetMax);
                // 只向下drop
                float yOffset = -UnityEngine.Random.Range(0, yOffsetMax);

                Vector3 targetPos = transform.position + new Vector3(xOffset, yOffset, 0);
                obj.transform.DOPath(new Vector3[] {transform.position, targetPos}, 1f, PathType.CatmullRom);
            }
        }
    }
}

