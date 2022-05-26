using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameObjectPool
{
    public List<GameObject> Objects { get; set; }

    public static GameObjectPool CreatePool(int initCount)
    {
        return new GameObjectPool(initCount);
    }

    private GameObjectPool(int initCount)
    {
        Objects = new List<GameObject>();
    }

    public void Get()
    {

    }

    public void Recycle(GameObject gameObject)
    {
        gameObject.SetActive(false);
        Objects.Add(gameObject);
    }
}
