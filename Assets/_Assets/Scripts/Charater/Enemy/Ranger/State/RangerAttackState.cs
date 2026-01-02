using Unity.VisualScripting;
using UnityEngine;

public class RangerAttackState : IStateBehaviour
{
    private RangerContext _context;
    private GameObject _attackObj;

    private bool _isAttacking;
    private float _time;
    private AttackPhase _phase;

    private float _rotateTimer = 0f;
    private Quaternion _startRot;
    private Quaternion _endRot;

    public RangerAttackState(RangerContext rangerContext, GameObject attack)
    {
        _context = rangerContext;
        _attackObj = attack;
    }
    public void Enter()
    {
        _context.Parameters.IsBlock = true;
        _isAttacking = false;
        _phase = AttackPhase.Startup;
        CalulateAngleAttack(_context.Player.transform.position);
    }

    public void Exit()
    {
        _rotateTimer = 0f;
        _time = 0f;
        _context.Parameters.CanAttack = false;
        _context.Parameters.TimeAttack = _context.DataAttack.cooldownAttack;
    }

    public void CalulateAngleAttack(Vector3 value)
    {
        Vector2 dir = value - _context.Transform.position;

        if (dir.x < 0) dir = -dir;

        Debug.Log(dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _startRot = _context.Transform.rotation;
        _endRot = Quaternion.Euler(0, 0, angle);
    }

    public void Update()
    {
        HandleAttackTiming();
    }

    private void HandleAttackTiming()
    {
        _time += Time.deltaTime;
        switch(_phase)
        {
            case AttackPhase.Startup:
                if(_time >= _context.DataAttack.timeAttackStart)
                {
                    EnterActive();
                }
                else
                {
                    RotationDir(_context.DataAttack.timeAttackStart);
                }    
                break;
            case AttackPhase.Active:
                if(_time >= _context.DataAttack.timeAttackActive)
                {
                    EnterRecovery();
                }
                else if(!_isAttacking)
                {
                    _attackObj.SetActive(true);
                    _context.Animator.PlayAniAttack();
                    _isAttacking = true;
                }    
                break;
            case AttackPhase.Recovery:
                if(_time >= _context.DataAttack.timeAttackEnd)
                {
                    _context.Parameters.IsBlock = false;
                    _context.Parameters.CanAttack = false;
                }else
                {
                    RotationDir(_context.DataAttack.timeAttackEnd);
                }    
                break;
        }    
    }

    private void EnterActive()
    {
        _time = 0;
        _phase = AttackPhase.Active;
    }

    private void EnterRecovery()
    {
        _time = 0;
        _phase = AttackPhase.Recovery;
        _attackObj.SetActive(false);

        _rotateTimer = 0;
        _startRot = _context.Transform.rotation;
        _endRot = Quaternion.identity;
    }

    private void RotationDir(float time)
    {
        _rotateTimer += Time.deltaTime;

        float t = Mathf.Clamp01(_rotateTimer / time);
        _context.Transform.rotation = Quaternion.Lerp(_startRot, _endRot, t);
    }

}
