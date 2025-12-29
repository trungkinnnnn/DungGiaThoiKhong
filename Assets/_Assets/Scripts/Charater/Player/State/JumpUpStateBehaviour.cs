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

        Debug.Log("JumpUp");
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float targetVelocityX = _context.Parameters.Horizontal * _context.DataMovement.speedNormal;
        float smooth = _context.DataMovement.accelerationTime;

        float newVeloctyX = Mathf.Lerp(_context.Rigidbody.velocity.x, targetVelocityX, smooth * Time.fixedDeltaTime);

        _context.Rigidbody.velocity = new Vector2(newVeloctyX, _context.Rigidbody.velocity.y);
    }
}
