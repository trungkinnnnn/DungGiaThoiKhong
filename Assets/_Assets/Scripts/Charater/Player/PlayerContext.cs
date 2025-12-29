using UnityEngine;

public class PlayerContext
{

    public IPlayerInput Input;
    public Rigidbody2D Rigidbody;
    public PlayerAnimationController Animator;
    public Transform Transform;

    public PlayerDataMovement DataMovement;
    public PlayerDataAttack DataAttack;

    public PlayerParameters Parameters;

    public PlayerContext(IPlayerInput input, Rigidbody2D rigidbody, PlayerAnimationController animator, Transform transform, PlayerDataMovement dataMovement, PlayerDataAttack dataAttack)
    {
        Input = input;
        Rigidbody = rigidbody;
        Animator = animator;
        Transform = transform;
        DataMovement = dataMovement;
        DataAttack = dataAttack;
        Parameters = new PlayerParameters();
    }
}