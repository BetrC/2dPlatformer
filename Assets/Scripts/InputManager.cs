using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum InputActionType
{
    Player,
    UI,
}

public class InputManager : Singleton<InputManager>
{
    private PlayerInput _input;

    public PlayerInput Input
    {
        get
        {
            if (_input == null)
            {
                _input = new PlayerInput();
            }
            return _input;
        }
        set
        {
            _input = value;
        }
    }

    protected override void Init()
    {
        base.Init();
        SwitchInputActionMap(InputActionType.Player);

        Input.Player.Menu.performed += Menu_performed;
        Input.UI.Esc.performed += Esc_performed;
        Input.UI.Submit.performed += Submit_performed;
    }

    private void Submit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GameUIManager.Instance.OnSubmit(obj);
    }

    private void Esc_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GameUIManager.Instance.HideMenu();
    }

    private void Menu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(GameManager.Instance.IsCurSceneLevelScene())
            GameUIManager.Instance.ShowMenu();
    }

    public void SwitchInputActionMap(InputActionType actionType)
    {
        switch (actionType)
        {
            case InputActionType.Player:
                Input.Player.Enable();
                Input.UI.Disable();
                break;
            case InputActionType.UI:
                Input.Player.Disable();
                Input.UI.Enable();
                break;
        }
    }

    public float XInput => Input.Player.Movement.ReadValue<Vector2>().x;

    public int XNormalInput => XInput.Normalize();

    public float YInput => Input.Player.Movement.ReadValue<Vector2>().y;

    public float YNormalInput => YInput.Normalize();

    public bool JumpPressed => Input.Player.JumpPress.triggered;

    public bool JumpReleased => Input.Player.JumpRelease.triggered;

    public bool PrimaryAttackPressed => Input.Player.Attack.triggered;

    public bool SecondryAttackHolding => Input.Player.SecondAttack.IsPressed();

    public bool DashPressed => Input.Player.Dash.triggered;

    public bool GrabWallHolding => Input.Player.GrabWall.IsPressed();

    public bool ShootHolding => Input.Player.Shoot.IsPressed();

    public bool LeavePressed => Input.Player.Submit.triggered;
}
