using UnityEngine;

public class TrainPatrolState : IStateBehaviour
{
    private TrainContext _context;

    private float _timeHoveState;
    private float _moveDirX;
    public TrainPatrolState(TrainContext context)
    {
        _context = context;
    }
    public void Enter()
    {
        _context.Animator.SetBoolAniRunning(true);
        _timeHoveState = _context.DataMovement.timeMove;
        _moveDirX = Random.value < 0.5f ? -1 : 1;
        Flip();
    }

    public void Exit()
    {
        _context.Parameters.DesiredVelocity = Vector2.zero;
    }

    public void Update()
    {
        CalculateDesiredVelocity();
        UpdateTime();
    }

    private void CalculateDesiredVelocity()
    {
        Vector2 velocity = _context.Parameters.DesiredVelocity;
        velocity.x = _moveDirX * _context.DataMovement.speedNormal;

        if (_context.Parameters.IsForward)
        {
            _moveDirX *= -1;
            Flip();
        }
        _context.Parameters.DesiredVelocity = Vector2.MoveTowards(_context.Parameters.DesiredVelocity,
                                                velocity, _context.DataMovement.acceleration * Time.deltaTime);
    }

    private void UpdateTime()
    {
        _timeHoveState -= Time.deltaTime;
        if (_timeHoveState <= 0)
        {
            _context.Parameters.IsRunning = false;
        }
    }

    private void Flip()
    {
        Vector3 scale = _context.Transform.localScale;
        scale.x = _moveDirX;
        _context.Transform.localScale = scale;
    }
}
