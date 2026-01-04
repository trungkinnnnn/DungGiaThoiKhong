using UnityEngine;

public class JumpUpStateBehaviour : IStateBehaviour
{
    private PlayerContext _context;

    public JumpUpStateBehaviour(PlayerContext context)
    {
        _context = context;
    }

    public void Enter()
    {
        _context.Animator.PlayAniJumpUp();
        _context.Rigidbody.AddForce(Vector2.up * _context.DataMovement.jumForce , ForceMode2D.Impulse);
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        
    }
}
