public class PlayerParameters
{
    // Input
    public float Horizontal;

    // Trigger
    public bool JumPressed;
    public bool AttackPressed;
    public bool DashPressed;

    

    public bool IsGrounded;
    public bool IsFalling => !IsGrounded;
    public bool CanDash = true;
    public bool CanAttack = true;

    public float DashTimer;
    public float AttackTimer;
}
