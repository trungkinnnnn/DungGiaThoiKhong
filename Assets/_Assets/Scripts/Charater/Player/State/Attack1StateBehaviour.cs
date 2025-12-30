using UnityEngine;

public class Attack1StateBehaviour : AttackStateBehavior
{
    private PlayerStateMachine _stateMachine;
    private IStateBehaviour _stateAttack2;
    public Attack1StateBehaviour(PlayerContext context, PlayerStateMachine state, IStateBehaviour attack2)
    {
        _context = context;
        _data = context.DataAttack.attack1;
        _stateMachine = state;
        _stateAttack2 = attack2;
    }
    protected override void ChangeStateAttack()
    {
        if(_context.Parameters.IsGrounded)
        {
            _stateMachine.ChangeState(_stateAttack2);
        }        
    }

    protected override void PlayAnimation()
    {
        Debug.Log("State Attack1");
        if(_context.Parameters.IsGrounded)
        {
            _context.Animator.PlayAniAttack();
        }else
        {
            _context.Animator.PlayAniAttackJump();
        }    
        
    }
}
