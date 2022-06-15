using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

[Serializable]
public class TipsData
{
    public string title;
    public string triggerDesc;
    public Sprite padSprite;
    public Sprite keySprite;

}

public class GameTips : MonoBehaviour
{
    public Text title;
    public Text trigger;
    public Image image;

    public void ShowTips(TipsData data)
    {
        title.text = data.title;
        trigger.text = data.triggerDesc;

        if (Gamepad.current != null)
            image.sprite = data.padSprite;
        else
            image.sprite = data.keySprite;
        image.SetNativeSize();

        gameObject.SetActive(true);
    }
}

