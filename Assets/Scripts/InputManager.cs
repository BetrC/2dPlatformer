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
}
