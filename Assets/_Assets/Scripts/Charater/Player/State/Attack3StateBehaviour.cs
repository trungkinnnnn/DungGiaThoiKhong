using UnityEngine;

public class Attack3StateBehaviour : AttackStateBehavior
{
    private GameObject _rockObj;
    public Attack3StateBehaviour(PlayerContext context,GameObject rockObj)
    {
        _context = context;
        _data = _context.DataAttack.attack3;
        _rockObj = rockObj;
    }

    protected override void SetHitbox(bool value)
    {
        _rockObj.SetActive(value);
    }

    protected override void ChangeStateAttack() { }

    protected override void PlayAnimation()
    {
        Debug.Log("State Attack3");
        _context.Animator.PlayAniAttack3();
    }


}
