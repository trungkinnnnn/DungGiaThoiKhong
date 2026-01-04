using UnityEngine;

public class TrainAttack3State : IStateBehaviour
{
    private TrainContext _context;
    private GameObject _attack3;
    private float _time;
    private AttackPhase _phase;
    public TrainAttack3State(TrainContext context, GameObject attack3)
    {
        _context = context;
        _attack3 = attack3;
    }

    public void Enter()
    {
        _context.Parameters.TimeAttack3 = _context.DataAttack3.cooldownAttack;
        _context.Parameters.IsBlock = true;
        _context.Animator.PlayAniAttack3();
    }

    public void Exit()
    {
        
        _time = 0;
        _phase = AttackPhase.Startup;
        _context.Animator.SetAniAttackDone(false);
        _context.Parameters.IsBlock = false;
        Debug.Log("Exit");
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
                if (_time >= _context.DataAttack3.timeAttackStart)
                {
                    EnterActive();
                }
                break;
            case AttackPhase.Active:
                if (_time >= _context.DataAttack3.timeAttackActive)
                {
                    EnterRecovery();
                }

                break;
            case AttackPhase.Recovery:
                if (_time >= _context.DataAttack3.timeAttackEnd)
                {
                    _context.Parameters.IsBlock = false;
                }
                break;
        }
    }

    private void EnterActive()
    {
        _time = 0;
        _phase = AttackPhase.Active;
        // onHitbox
        _attack3.SetActive(true);
    }

    private void EnterRecovery()
    {
        _time = 0;
        _phase = AttackPhase.Recovery;
        // offHitBox
        _attack3.SetActive(false);
        _context.Animator.SetAniAttackDone(true);
    }

}

