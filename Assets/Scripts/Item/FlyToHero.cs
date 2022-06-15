using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FlyToHero : MonoBehaviour
{
    public float moveSpeed = 20f;

    private void Start()
    {
        StartCoroutine(FlyTo(GameManager.Instance.hero));
    }


     IEnumerator FlyTo(Hero hero)
    {
        yield return new WaitForSeconds(1f);

        while (true)

        {
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        transform.DOComplete();
    }

}
