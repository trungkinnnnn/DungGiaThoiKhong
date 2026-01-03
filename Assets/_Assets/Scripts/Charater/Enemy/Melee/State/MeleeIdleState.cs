using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MeleeIdleState : IStateBehaviour
{
    private MeleeContext _context;
    private float _timeHoveState;
    public MeleeIdleState(MeleeContext context)
    {
        _context = context;
    }
    public void Enter()
    {
        _timeHoveState = _context.DataMovement.timeStop;
        _context.Animator.SetBoolAniRunning(false);
        Debug.Log("Idle");
    }

    public void Exit()
    {

    }

    public void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        _timeHoveState -= Time.deltaTime;
        if (_timeHoveState <= 0)
        {
            _context.Parameters.IsRunning = true;
        }
    }
}
