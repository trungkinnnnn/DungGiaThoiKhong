using UnityEditor.Purchasing;
using UnityEngine;

public interface IPlayerInput
{
    float Horizontal { get; }
    bool JumPressed { get; }

    public void LastUpdate();
}


public class PCInput : IPlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    public bool JumPressed => Input.GetKeyDown(KeyCode.Space);
    public void LastUpdate() { }
}

public class MoblieInput : IPlayerInput
{
    public float Horizontal { get; set; }
    public bool JumPressed { get; set; }

    public void OnPressLeft() => Horizontal = -1;
    public void OnPressRight() => Horizontal = 1;
    public void OnRelease() => Horizontal = 0;  

    public void OnJumpButton() => JumPressed = true;

    public void LastUpdate() => JumPressed = false;

}