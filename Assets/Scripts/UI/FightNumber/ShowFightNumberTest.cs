using System;
using System.Collections;
using UnityEngine;

public class ShowFightNumberTest : MonoBehaviour
{

    private WaitForSeconds wait;

    [Range(0.01f, 1f)]
    public float interval = .1f;


    private void Start()
    {
        StartCoroutine(ShowFightNumber());
    }


     IEnumerator ShowFightNumber()
    {
        int value = 1;
        while (true)
        {
            yield return new WaitForSeconds(interval);
            FightNumberManager.Instance.ShowFightNumber(value++, transform, (FightNumType)(value % 3));
        }
    }
}
