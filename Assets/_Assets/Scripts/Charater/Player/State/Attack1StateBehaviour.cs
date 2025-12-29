using UnityEngine;

public class Attack1StateBehaviour : AttackStateBehavior
{
    public Attack1StateBehaviour(PlayerContext context)
    {
        _context = context;
        _data = context.DataAttack.attack1;
    }
    protected override void OffActiveAttack()
    {
        _context.Parameters.CanAttack2 = false;
    }

    protected override void OnActiveAttack()
    {
        _context.Parameters.CanAttack2 = true;
    }

    protected override void PlayAnimation()
    {
        Debug.Log("State Attack1");
        _context.Animator.PlayAniAttack();
    }
}
