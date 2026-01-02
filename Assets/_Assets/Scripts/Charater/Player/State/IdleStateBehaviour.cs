using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateBehaviour : IStateBehaviour
{

    private PlayerContext _context;

    public IdleStateBehaviour(PlayerContext context)
    {
        _context = context;
    }

    public void Enter()
    {
        _context.Rigidbody.velocity = new Vector2(0, _context.Rigidbody.velocity.y);
        _context.Parameters.currentSpeed = _context.DataMovement.speedNormal;
        _context.Animator.PlayAniRunning(false);
        
    }

    public void Exit() { }

    public void Update() { }
    
}
