using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityDetailUI : MonoBehaviour
{
    public Image icon;
    public Text title;

    public Text description;

    public Image btnPad;
    public Image btnKeyboard;

    public Button confirm;

    private void OnEnable()
    {
        GameUIManager.Instance.onUISubmit.AddListener(OnSubmit);
    }

    private void OnDisable()
    {
        GameUIManager.Instance.onUISubmit.RemoveListener(OnSubmit);
    }

    private void Awake()
    {
        confirm.onClick.AddListener(OnClickConfirm);
    }

    public void ShowDetail(ItemData abilityItemData)
    {
        if (abilityItemData.useFunc != UseFunc.ActiveAbility)
            return;

        Ability ability = (Ability)abilityItemData.useParam;
        icon.sprite = abilityItemData.icon;
        title.text = abilityItemData.name;

        description.text = abilityItemData.description;
        btnPad.sprite = GetImage("pad_" + abilityItemData.useParam);
        btnKeyboard.sprite = GetImage("key_" + abilityItemData.useParam);
        btnPad.SetNativeSize();
        btnKeyboard.SetNativeSize();

        gameObject.SetActive(true);
    }

    private Sprite GetImage(string name)
    {
        return Resources.Load<Sprite>("Image/" + name);
    }

    private void OnClickConfirm()
    {
        GameUIManager.Instance.HideAbilityDetailUI();
    }

    public void OnSubmit(InputAction.CallbackContext obj)
    {
        confirm.onClick.Invoke();
    }
}

