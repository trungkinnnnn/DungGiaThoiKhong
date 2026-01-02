using UnityEngine;
public class IdleMoveState : IStateBehaviour
{
    private RangerContext _context;
    private float _timeHoveState;
    private float _moveDirX;
    private float _verticalTargetY;
    private float _changeVeticalTime;
    public IdleMoveState(RangerContext rangerContext) 
    {
        _context = rangerContext;
    }
    public void Enter()
    {
        _timeHoveState = _context.DataMovement.timeMove;
        _moveDirX = Random.value < 0.5 ? -1 : 1;
        _changeVeticalTime = Random.Range(1f, _context.DataMovement.timeMove);
        PickNewVerticalTarget();
        Flip();
    }

    private void PickNewVerticalTarget()
    {
        _verticalTargetY = Random.Range(-1f, 1f);
    }

    public void Exit() { _context.Parameters.DesiredVelocity = Vector2.zero; }

    public void Update()
    {
        UpdateTime();
        CalculateDesiredVelocity();
    }

    private void UpdateTime()
    {
        _timeHoveState -= Time.deltaTime;
        if (_timeHoveState <= 0)
        {
            _context.Parameters.IsRunning = false;
        }
    }

    private void CalculateDesiredVelocity()
    {
        Vector2 velocity = _context.Parameters.DesiredVelocity;

        velocity.x = _moveDirX * _context.DataMovement.speedNormal;
        velocity.y = Mathf.Lerp(velocity.y, _verticalTargetY * _context.DataMovement.speedVerticalY, Time.deltaTime);

        if (_context.Parameters.IsTop)
            velocity.y -= _context.DataMovement.speedVerticalY * Time.deltaTime;
        if(_context.Parameters.IsBottom)
            velocity.y += _context.DataMovement.speedVerticalY * Time.deltaTime;
        if(_context.Parameters.IsForward)
        {
            _moveDirX *= -1f;
            Flip();
            PickNewVerticalTarget();
        }

        _context.Parameters.DesiredVelocity = velocity;
    }

    private void Flip()
    {
        Vector3 transform = _context.Transform.localScale;
        transform.x = _moveDirX;
        _context.Transform.localScale = transform;
    }


}


