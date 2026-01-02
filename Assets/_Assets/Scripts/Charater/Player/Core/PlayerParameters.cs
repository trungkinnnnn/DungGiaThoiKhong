using UnityEngine.Experimental.GlobalIllumination;

public class PlayerParameters
{
    // Input
    public float Horizontal;
    public float currentSpeed;

    // Trigger
    public bool JumPressed;
    public bool AttackPressed;
    public bool DashPressed;

    public bool IsGrounded;
    public bool DoneAttack = true;
    public bool DoneDash = true;

    public float DashTimer;
    public float AttackTimer;
}
