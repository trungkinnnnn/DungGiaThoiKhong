using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStateBehavior : IStateBehaviour
{
    protected PlayerContext _context;
    protected AttackData _data;
    private AttackPhase _phase;
    private float _timer;

    public void Enter()
    {
        _phase = AttackPhase.Startup;
        PlayAnimation();
        _context.Parameters.AttackTimer = _context.DataAttack.comboCooldown;
        _context.Input.Block = true;
    }

    protected abstract void PlayAnimation();

    public virtual void Exit()
    {
        _timer = 0;
    }

    public void Update()
    {
        UpdatePosition();
        HandleAttackTiming();
    }

    private void UpdatePosition()
    {
        float targetVelocityX = _context.Parameters.Horizontal * _context.DataMovement.speedAttack;
        float smooth = _context.DataMovement.accelerationTime;

        float newVeloctyX = Mathf.Lerp(_context.Rigidbody.velocity.x, targetVelocityX, smooth * Time.fixedDeltaTime);

        _context.Rigidbody.velocity = new Vector2(newVeloctyX, _context.Rigidbody.velocity.y);
    }

    private void HandleAttackTiming()
    {
        _timer += Time.deltaTime;

        switch (_phase)
        {
            case AttackPhase.Startup:
                if (_timer >= _data.startup)
                {
                    //On hitbox
                    _phase = AttackPhase.Active;
                }
                break;
            case AttackPhase.Active:
                if (_timer >= _data.active + _data.startup)
                {
                    // Off Hitbox
                    _phase = AttackPhase.Recovery;
                    OnActiveAttack();
                }
                break;
            case AttackPhase.Recovery:
                if (_timer >= _data.active + _data.startup + _data.comboWindow)
                {
                    OffActiveAttack();
                }
                break;
        }

        if(_timer >= _data.timeBlock)
        {
            _context.Input.Block = false;
        }
    }

    protected abstract void OnActiveAttack();
    protected abstract void OffActiveAttack();

}


