using UnityEngine;

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

    private void Start()
    {
        GetComponent<DamageableActor>().health.onDie.AddListener(DropItem);
    }


    public void DropItem()
    {
        foreach(DropData data in drops)
        {
            float rate = Random.Range(0f, 1f);
            if (rate < data.dropRate)
                continue;
            int count = Random.Range(data.minNum, data.maxNum);
            for (int i = 1; i <= count; i++)
            {
                GameObject obj = Instantiate(data.item, transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
    }
}

