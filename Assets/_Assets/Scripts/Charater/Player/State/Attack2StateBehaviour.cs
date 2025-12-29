using UnityEngine;

public class Attack2StateBehaviour : AttackStateBehavior
{

    public Attack2StateBehaviour(PlayerContext context)
    {
        _context = context;
        _data = _context.DataAttack.attack2;
    }

    protected override void OffActiveAttack()
    {
       _context.Parameters.CanAttack3 = false;
    }

    protected override void OnActiveAttack()
    {
        _context.Parameters.CanAttack3 = true;
    }

    protected override void PlayAnimation()
    {
        Debug.Log("State Attack2");
        _context.Animator.PlayAniAttack2();
    }

    public override void Exit()
    {
        base.Exit();
        _context.Parameters.CanAttack2 = false; 
    }
}
