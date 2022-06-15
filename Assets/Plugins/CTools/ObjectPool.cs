using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    public List<GameObject> poolObjects;
    public GameObject objectToPool;
    public int initAmount;

    void Awake()
    {
        poolObjects = new List<GameObject>();
        for(int i = 0; i < initAmount; i++)
        {
            NewPoolItem();
        }
    }

    public GameObject GetFromPool()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }

        return NewPoolItem();
    }

    public void Release(GameObject poolItem)
    {
        poolItem.SetActive(false);
    }

    public void ResetPool()
    {
        foreach (var poolObject in poolObjects)
        {
            Release(poolObject);
        }
    }

    private GameObject NewPoolItem()
    {
        GameObject tmp = GameObject.Instantiate(objectToPool, gameObject.transform);
        tmp.SetActive(false);
        poolObjects.Add(tmp);
        return tmp;
    }
    
}