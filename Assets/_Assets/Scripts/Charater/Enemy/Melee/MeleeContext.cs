using UnityEngine;

public class MeleeContext
{
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public EnemyDataAttack DataAttack;
    public EnemyDataMovement DataMovement;
    public EnemyAnimationController Animator;
    public EnemyParameters Parameters;

    private PlayerSingle _player;

    public MeleeContext(Transform transform, Rigidbody2D rigidbody2D, EnemyDataAttack dataAttack, EnemyDataMovement dataMovement, EnemyAnimationController animator)
    {
        Transform = transform;
        Rigidbody = rigidbody2D;
        DataAttack = dataAttack;
        DataMovement = dataMovement;
        Animator = animator;
        Parameters = new EnemyParameters();
        _player = PlayerSingle.Instance;
    }

    public PlayerSingle Player
    {
        get
        {
            if (_player == null && PlayerSingle.Instance != null)
                _player = PlayerSingle.Instance;
            return _player;
        }
    }
}

