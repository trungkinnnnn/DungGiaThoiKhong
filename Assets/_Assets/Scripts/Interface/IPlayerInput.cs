using UnityEngine;

public interface IPlayerInput
{
    bool Block {  get; set; }
    float Horizontal { get; }
    bool JumPressed { get; }
    bool DashPressed { get; }
    bool AttackPressed { get; } 
    public void ResetJump();
    public void ResetDash();
    public void ResetAttack();
    
}


public class PCInput : IPlayerInput
{
    public float Horizontal => Block ? 0 : Input.GetAxisRaw("Horizontal");

    public bool JumPressed => !Block && Input.GetKeyDown(KeyCode.Space);

    public bool DashPressed => !Block && Input.GetKeyDown(KeyCode.K);

    public bool AttackPressed => !Block && Input.GetKeyDown(KeyCode.J);

    public bool Block { get; set; } = false;

    public void ResetAttack() { }
    public void ResetDash() { }
    public void ResetJump() { }

}

public class MoblieInput : IPlayerInput
{
    private float _horizontal;
    private bool _jumpPressed;
    private bool _dashPressed;
    private bool _attackPressed;
    public bool Block { get; set; } = false;

    public float Horizontal => Block ? 0 : _horizontal;
    public bool JumPressed
    {
        get
        {
            if (Block) return false;
            bool value = _jumpPressed;
            _jumpPressed = false;
            return value;
        }
    }
    public bool DashPressed
    {
        get
        {
            if (Block) return false;
            bool value = _dashPressed;
            _dashPressed = false;
            return value;
        }
    }
    public bool AttackPressed
    {
        get
        {
            if (Block) return false;
            bool value = _attackPressed;
            _attackPressed = false;
            return value;
        }
    }

    public void OnPressLeft() => _horizontal = -1;
    public void OnPressRight() => _horizontal = 1;
    public void OnRelease() => _horizontal = 0;  
    public void OnJumpButton() => _jumpPressed = true;
    public void OnDashButton() => _dashPressed = true;
    public void OnAttackButton() => _attackPressed = true;
    public void ResetDash() => _dashPressed = false;
    public void ResetJump() => _jumpPressed = false;
    public void ResetAttack() => _attackPressed = false; 
}