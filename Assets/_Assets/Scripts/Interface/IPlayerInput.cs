using UnityEditor.Purchasing;
using UnityEngine;

public interface IPlayerInput
{
    float Horizontal { get; }
    bool JumPressed { get; }
    bool DashPressed { get; }
    public void ResetJump();
    public void ResetDash();
}


public class PCInput : IPlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    public bool JumPressed => Input.GetKeyDown(KeyCode.Space);

    public bool DashPressed => Input.GetKeyDown(KeyCode.K);
    public void ResetDash() { }
    public void ResetJump() { }

}

public class MoblieInput : IPlayerInput
{
    public float Horizontal { get; set; }
    public bool JumPressed { get; set; }
    public bool DashPressed { get; set; }

    public void OnPressLeft() => Horizontal = -1;
    public void OnPressRight() => Horizontal = 1;
    public void OnRelease() => Horizontal = 0;  
    public void OnJumpButton() => JumPressed = true;
    public void OnDashButton() => DashPressed = true;
    public void ResetDash() => DashPressed = false;
    public void ResetJump() => JumPressed = false;

}