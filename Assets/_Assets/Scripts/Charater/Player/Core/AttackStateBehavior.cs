
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
        _context.Parameters.DoneAttack = false; 
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
                if(_timer >= _data.startup)
                {
                    EnterActive();
                }
                break;
            case AttackPhase.Active:
                if(_timer >= _data.active)
                {
                    EnterRecovery();
                }
                break;
            case AttackPhase.Recovery:
                TryCombo();
                if(_timer >= _data.timeBlock)
                {
                    _context.Parameters.DoneAttack = true;
                    Exit();
                }    
                break;
        }

    }

    private void EnterActive()
    {
        // onhitbox
        _phase = AttackPhase.Active;
        _timer = 0;
    }

    private void EnterRecovery()
    {
        // off hitbox
        _phase = AttackPhase.Recovery;
        _timer = 0;

        _context.Input.Block = false;
    }

    private void TryCombo()
    {
        if (!_context.Input.AttackPressed) return;
        ChangeStateAttack();
    }

    protected abstract void ChangeStateAttack();

}


