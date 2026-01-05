using UnityEngine;

public class TrainAttack1State : IStateBehaviour
{
    private TrainContext _context;
    private float _time;
    private AttackPhase _phase;
    public TrainAttack1State(TrainContext context)
    {
        _context = context;
    }

    public void Enter()
    {
        _context.Parameters.IsBlock = true;
        _context.Animator.PlayAniAttack();
        Debug.Log("attack1");
    }

    public void Exit()
    {
        _context.Parameters.IsBlock = false;
        _context.Parameters.TimeAttack1 = _context.DataAttack1.cooldownAttack;
        _time = 0;
        _phase = AttackPhase.Startup;
        _context.Parameters.DesiredVelocity = Vector2.zero;
    }

    public void Update()
    {
        HandleAttackTiming();
    }

    private void HandleAttackTiming()
    {
        _time += Time.deltaTime;
        switch (_phase)
        {
            case AttackPhase.Startup:
                if (_time >= _context.DataAttack1.timeAttackStart)
                {
                    EnterActive();
                }
                break;
            case AttackPhase.Active:
                if (_time >= _context.DataAttack1.timeAttackActive)
                {
                    EnterRecovery();
                }
                break;
            case AttackPhase.Recovery:
                if (_time >= _context.DataAttack1.timeAttackEnd)
                {
                    _context.Parameters.IsBlock = false;
                    _context.Parameters.CanAttack1 = false;
                }
                break;
        }
    }

    private void EnterActive()
    {
        _time = 0;
        _phase = AttackPhase.Active;
        // onHitbox
    }

    private void EnterRecovery()
    {
        _time = 0;
        _phase = AttackPhase.Recovery;
        // offHitBox

    }

}

