using UnityEngine;

public class RunStateBehaviour : IStateBehaviour
{
    private PlayerContext _context;

    public RunStateBehaviour(PlayerContext context)
    {
        _context = context;
    }   

    public void Enter()
    {
        _context.Animator.PlayAniRunning(true); 
    }

    public void Exit()
    {
       _context.Animator.PlayAniRunning(false);
    }

    public void Update() { }

}
