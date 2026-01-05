using UnityEngine;

public class TrainParameters 
{
    public bool IsForward;
    public Vector2 DesiredVelocity;

    public bool IsRunning = false;
    public bool IsCombat = false;
    public bool IsTakeDame;
    public bool IsBlock;

    public bool CanAttack1 = false;
    public bool CanAttack2 = false;
    public float TimeAttack1;
    public float TimeAttack2;
    public float TimeAttack3;
}
