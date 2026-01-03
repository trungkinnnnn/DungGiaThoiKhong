using UnityEngine;

public class MeleeContext
{
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public EnemyDataAttack DataAttack;
    public EnemyDataMovement DataMovement;
    public EnemyAnimationController Animator;
    public EnemyParameters Parameters;

    public PlayerSingle Player;

    public MeleeContext(Transform transform, Rigidbody2D rigidbody2D, EnemyDataAttack dataAttack, EnemyDataMovement dataMovement, EnemyAnimationController animator)
    {
        Transform = transform;
        Rigidbody = rigidbody2D;
        DataAttack = dataAttack;
        DataMovement = dataMovement;
        Animator = animator;
        Parameters = new EnemyParameters();
        Player = PlayerSingle.Instance;
    }
}

