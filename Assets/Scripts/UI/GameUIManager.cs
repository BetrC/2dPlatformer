using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnUISubmit : UnityEvent<InputAction.CallbackContext> { };

public class GameUIManager : MonoSingleton<GameUIManager>
{

    public GameObject menuCanvas;

    public AbilityDetailUI abilityDetail;

    public GameTips TipsUI;

    public GameFinishUI FinishUI;

    public OnUISubmit onUISubmit = new();

    private void Start()
    {
        ButtonAddClickSound();
    }

    private void ButtonAddClickSound()
    {
        Button[] buttons = GameObject.FindObjectsOfType<Button>();
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() => SoundManager.Instance.PlaySound("click"));
        }
    }

    public void ShowTips(TipsData tipsData)
    {
        TipsUI.ShowTips(tipsData);
    }

    public void HideTips()
    {
        TipsUI.gameObject.SetActive(false);
    }

    public void ShowFinishUI()
    {
        FinishUI.gameObject.SetActive(true);
    }

    public void HideFinishUI()
    {
        FinishUI.gameObject.SetActive(false);
    }


    void OnUIShow()
    {
        GameManager.Instance.SetTimeScale(0);
        InputManager.Instance.SwitchInputActionMap(InputActionType.UI);
    }

    void OnUIHide()
    {
        GameManager.Instance.SetTimeScale(1);
        InputManager.Instance.SwitchInputActionMap(InputActionType.Player);
    }

    public void ShowAbilityDetail(ItemData abilityItem)
    {
        abilityDetail.ShowDetail(abilityItem);
        OnUIShow();
    }

    public void HideAbilityDetailUI()
    {
        if (abilityDetail.gameObject.activeInHierarchy)
            abilityDetail.gameObject.SetActive(false);
        OnUIHide();
    }

    public void ShowMenu()
    {
        menuCanvas.SetActive(true);
        OnUIShow();

    }

    public void HideMenu()
    {
        if (menuCanvas.activeInHierarchy)
            menuCanvas.SetActive(false);
        OnUIHide();
    }

    public void HideAllUI()
    {
        HideMenu();
        HideAbilityDetailUI();
    }

    public void OnSubmit(InputAction.CallbackContext obj)
    {
        onUISubmit.Invoke(obj);
    }
}
