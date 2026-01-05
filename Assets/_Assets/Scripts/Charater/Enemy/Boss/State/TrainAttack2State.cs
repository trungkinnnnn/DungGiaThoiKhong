using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAttack2State : IStateBehaviour
{
    private TrainContext _context;
    private GameObject _shieldObj;
    private float _time;
    private float _forceAttack = 1f;
    private AttackPhase _phase;
    public TrainAttack2State(TrainContext context,GameObject shieldObj)
    {
        _context = context;
        _shieldObj = shieldObj;
    }

    public void Enter()
    {
        _context.Parameters.IsBlock = true;
        _context.Animator.PlayAniAttack2();
    }

    public void Exit()
    {
        _context.Parameters.IsBlock = false;
        _context.Parameters.TimeAttack2 = _context.DataAttack2.cooldownAttack;
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
                if (_time >= _context.DataAttack2.timeAttackStart)
                {
                    EnterActive();
                }
                break;
            case AttackPhase.Active:
                if (_time >= _context.DataAttack2.timeAttackActive)
                {
                    EnterRecovery();
                }else
                {
                    AddForce();
                }
                break;
            case AttackPhase.Recovery:
                if (_time >= _context.DataAttack2.timeAttackEnd)
                {
                    _context.Parameters.IsBlock = false;
                    _context.Parameters.CanAttack2 = false;
                }
                break;
        }
    }

    private void EnterActive()
    {
        _time = 0;
        _phase = AttackPhase.Active;
        // onHitbox
        _shieldObj.SetActive(true);
    }

    private void EnterRecovery()
    {
        _time = 0;
        _phase = AttackPhase.Recovery;
        // offHitBox
        _shieldObj.SetActive(false);
    }

    private void AddForce()
    {
        float dir = Mathf.Sign(_context.Transform.localScale.x);
        _context.Rigidbody.AddForce(Vector2.right * dir * _forceAttack, ForceMode2D.Impulse);
    }
}

