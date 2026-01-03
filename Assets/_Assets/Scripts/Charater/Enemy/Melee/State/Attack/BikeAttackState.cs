using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeAttackState : IStateBehaviour
{
    private MeleeContext _context;
    private float _dashForce;
    private float _time;
    private AttackPhase _phase;
    private bool _isAttacking = false;
    public BikeAttackState(MeleeContext context, float dashForce)
    {
        _context = context;
        _dashForce = dashForce;
    }

    public void Enter()
    {
        _context.Parameters.IsBlock = true;
        _context.Animator.PlayAniAttack();
    }

    public void Exit()
    {
        _context.Parameters.TimeAttack = _context.DataAttack.cooldownAttack;
        _time = 0;
        _isAttacking = false;
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
                if (_time >= _context.DataAttack.timeAttackStart)
                {
                    EnterActive();
                }
                break;
            case AttackPhase.Active:
                if (_time >= _context.DataAttack.timeAttackActive)
                {
                    EnterRecovery();
                }else
                {
                    AddForce();
                }
                break;
            case AttackPhase.Recovery:
                if (_time >= _context.DataAttack.timeAttackEnd)
                {
                    _context.Parameters.IsBlock = false;
                    _context.Parameters.CanAttack = false;
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

    private void AddForce()
    {
        float dir = Mathf.Sign(_context.Transform.localScale.x);
        _context.Rigidbody.AddForce(Vector2.right * dir * _dashForce, ForceMode2D.Impulse);
    }


}
