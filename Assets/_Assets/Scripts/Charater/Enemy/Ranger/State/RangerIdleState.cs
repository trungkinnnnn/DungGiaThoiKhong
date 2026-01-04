using UnityEngine;

public class RangerIdleState : IStateBehaviour
{
    private RangerContext _context;
    private float _timeHoveState;
    public RangerIdleState(RangerContext rangerContext)
    {
        _context = rangerContext;
    }
    public void Enter()
    {
        _timeHoveState = _context.DataMovement.timeStop;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        _timeHoveState -= Time.deltaTime;
        if (_timeHoveState <= 0)
        {
            _context.Parameters.IsRunning = true;
        }
    }
}
