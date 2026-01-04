using UnityEngine;

public class JumpDownStateBehaviour : IStateBehaviour
{
    private PlayerContext _context;
    public JumpDownStateBehaviour(PlayerContext context)
    {
        _context = context;
    }

    public void Enter()
    {
        _context.Animator.PlayAniJumDown();
    }

    public void Exit()
    {
        
    }


    public void Update()
    {
       
    }
}
