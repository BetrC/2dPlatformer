using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawBerryUI : MonoBehaviour
{
    public Text num;
    

    // Start is called before the first frame update
    void Start()
    {
        num.text = GameManager.Instance.StrawberryCount.ToString();
        GameManager.Instance.StrawBerryNumUpdate.AddListener(OnStrawBerryNumUpdate);
    }


    void OnStrawBerryNumUpdate(int value)
    {
        num.text = value.ToString();
    }

}
