using UnityEngine;

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
                _input.Player.Enable();
            }
            return _input;
        }
        set
        {
            _input = value;
        }
    }

    public float XInput => Input.Player.Movement.ReadValue<Vector2>().x;

    public int XNormalInput => (int)XInput.Normalize();

    public float YInput => Input.Player.Movement.ReadValue<Vector2>().y;

    public float YNormalInput => (int)YInput.Normalize();

    public bool JumpPressed => Input.Player.JumpPress.triggered;

    public bool JumpReleased => Input.Player.JumpRelease.triggered;

    public bool AttackPressed => Input.Player.normalAttack.triggered;

    public bool SecondAttackPressed => Input.Player.chargeAttack.triggered;

    public bool DashPressed => Input.Player.Dash.triggered;

    public bool GrabWallPressing => Input.Player.GrabWall.inProgress;
}
