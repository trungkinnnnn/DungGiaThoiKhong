using UnityEngine;

public class TrainContext
{
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public EnemyDataAttack DataAttack1;
    public EnemyDataAttack DataAttack2;
    public EnemyDataAttack DataAttack3;
    public EnemyDataMovement DataMovement;
    public TrainAnimationController Animator;
    public TrainParameters Parameters;

    public PlayerSingle Player;

    public TrainContext(
        Transform transform, 
        Rigidbody2D rigidbody2D, 
        EnemyDataAttack dataAttack1,
        EnemyDataAttack dataAttack2, 
        EnemyDataAttack dataAttack3, 
        EnemyDataMovement dataMovement,
        TrainAnimationController animator)
    {
        Transform = transform;
        Rigidbody = rigidbody2D;
        DataAttack1 = dataAttack1;
        DataAttack2 = dataAttack2;
        DataAttack3 = dataAttack3;
        DataMovement = dataMovement;
        Animator = animator;
        Parameters = new TrainParameters();
        Player = PlayerSingle.Instance;
    }
}
