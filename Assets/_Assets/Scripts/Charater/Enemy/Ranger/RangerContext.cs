using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerContext
{
    public Transform Transform;
    public Rigidbody2D Rigidbody2D;
    public RangerDataAttack DataAttack;
    public EnemyDataMovement DataMovement;
    public EnemyAnimationController Animator;
    public RangerParameters Parameters;

    public PlayerSingle Player;

    public RangerContext(Transform transform ,Rigidbody2D rigidbody2D, RangerDataAttack dataAttack, EnemyDataMovement dataMovement, EnemyAnimationController animator)
    {
        Transform = transform;
        Rigidbody2D = rigidbody2D;
        DataAttack = dataAttack;
        DataMovement = dataMovement;
        Animator = animator;
        Parameters = new RangerParameters();
        Player = PlayerSingle.Instance;
    }
}

