using UnityEngine;

public class InputManger : Singleton<InputManger>
{
    private PlayerInput _input;

    public PlayerInput Input
    {
        get
        {
            if (_input == null)
            {
                _input = new PlayerInput();
                _input.Player.Enable();
            }
            return _input;
        }
        set
        {
            _input = value;
        }
    }

    public float xInput => Input.Player.Movement.ReadValue<float>();

    public int XNormalInput => (int)xInput.Normalize();

    public bool JumpPressed => Input.Player.JumpPress.triggered;

    public bool JumpReleased => Input.Player.JumpRelease.triggered;

    public bool AttackPressed => Input.Player.normalAttack.triggered;

    public bool SecondAttackPressed => Input.Player.chargeAttack.triggered;

    public bool DashPressed => Input.Player.Dash.triggered;
}
