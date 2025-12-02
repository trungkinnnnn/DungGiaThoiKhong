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
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    public bool JumPressed => Input.GetKeyDown(KeyCode.Space);

    public bool DashPressed => Input.GetKeyDown(KeyCode.K);

    public bool AttackPressed => Input.GetKeyDown(KeyCode.J);

    public bool Block { get; set; } = false;

    public void ResetAttack() { }
    public void ResetDash() { }
    public void ResetJump() { }

}

public class MoblieInput : IPlayerInput
{
    public float Horizontal { get; set; }
    public bool JumPressed { get; set; }
    public bool DashPressed { get; set; }
    public bool AttackPressed { get;set; }
    public bool Block { get; set; } = false;

    public void OnPressLeft() => Horizontal = -1;
    public void OnPressRight() => Horizontal = 1;
    public void OnRelease() => Horizontal = 0;  
    public void OnJumpButton() => JumPressed = true;
    public void OnDashButton() => DashPressed = true;
    public void OnAttackButton() => AttackPressed = true;
    public void ResetDash() => DashPressed = false;
    public void ResetJump() => JumPressed = false;
    public void ResetAttack() => AttackPressed = false; 
}