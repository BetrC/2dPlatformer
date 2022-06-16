using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameFinishUI : MonoBehaviour
{
    public Text strawBerryNum;

    public Text desc;

    private void Start()
    {

    }

    private void OnEnable()
    {
        ShowResult();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    public void ShowResult()
    {
        StopAllCoroutines();
        StartCoroutine(ShowStrawBerryNum());
    }

    IEnumerator ShowStrawBerryNum()
    {
        strawBerryNum.text = $"{0}/{GameManager.maxStrawberryCount}";
        desc.gameObject.SetActive(false);
        int count = 0;
        while(count <= GameManager.Instance.StrawberryCount)
        {
            strawBerryNum.text = $"{count++}/{GameManager.maxStrawberryCount}";
            yield return new WaitForSeconds(.05f);
        }

        desc.text = GameManager.Instance.StrawberryCount < GameManager.maxStrawberryCount ? "Almost There!" : "You Are Absolutely An Animal!!";
        desc.gameObject.SetActive(true);
    }


}
