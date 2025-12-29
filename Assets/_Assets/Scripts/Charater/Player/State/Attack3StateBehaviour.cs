using UnityEngine;

public class Attack3StateBehaviour : AttackStateBehavior
{
    public Attack3StateBehaviour(PlayerContext context)
    {
        _context = context;
        _data = _context.DataAttack.attack3;
    }
    protected override void OffActiveAttack() { return; }

    protected override void OnActiveAttack() { return; }

    protected override void PlayAnimation()
    {
        Debug.Log("State Attack3");
        _context.Animator.PlayAniAttack3();
    }

    public override void Exit()
    {
        base.Exit();
        _context.Parameters.CanAttack3 = false;
    }
}
