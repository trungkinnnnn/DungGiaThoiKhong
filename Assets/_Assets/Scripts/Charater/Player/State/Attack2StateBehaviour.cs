using UnityEngine;

public class Attack2StateBehaviour : AttackStateBehavior
{
    protected StateMachine _stateMachine;
    private IStateBehaviour _stateAttack3;
    public Attack2StateBehaviour(PlayerContext context, StateMachine stateMachine, IStateBehaviour state)
    {
        _context = context;
        _data = _context.DataAttack.attack2;
        _stateMachine = stateMachine;
        _stateAttack3 = state;
    }

    protected override void ChangeStateAttack()
    {
         _stateMachine.ChangeState(_stateAttack3);
    }
    protected override void PlayAnimation()
    {
        Debug.Log("State Attack2");
        _context.Animator.PlayAniAttack2();
    }
}
