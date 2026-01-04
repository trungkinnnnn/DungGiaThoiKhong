using UnityEngine;

public class PlayerContext
{

    public IPlayerInput Input;
    public Rigidbody2D Rigidbody;
    public Collider2D Collider;
    public PlayerAnimationController Animator;
    public Transform Transform;

    public PlayerDataMovement DataMovement;
    public PlayerDataAttack DataAttack;

    public PlayerParameters Parameters;

    public PlayerContext(IPlayerInput input, Rigidbody2D rigidbody,Collider2D collider, PlayerAnimationController animator, Transform transform, PlayerDataMovement dataMovement, PlayerDataAttack dataAttack)
    {
        Input = input;
        Rigidbody = rigidbody;
        Collider = collider;
        Animator = animator;
        Transform = transform;
        DataMovement = dataMovement;
        DataAttack = dataAttack;
        Parameters = new PlayerParameters();
    }
}