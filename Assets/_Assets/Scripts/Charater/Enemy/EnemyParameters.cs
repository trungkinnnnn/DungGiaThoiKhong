
using UnityEngine;

public class EnemyParameters
{
    public bool IsTop;
    public bool IsBottom;
    public bool IsForward;
    public Vector2 DesiredVelocity;

    public bool IsRunning = false;
    public bool IsCombat = true;
    public bool IsTakeDame;
    public bool IsBlock;

    public bool CanAttack = false;
    public float TimeAttack;
    
}
