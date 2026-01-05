using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MeleeController
{
    [SerializeField] float _dashForce = 15f;

    private BikeAttackState _attackState;
    protected override void InitState()
    {
        base.InitState();
        _attackState = new BikeAttackState(_context, _dashForce);
    }

    protected override void SetUpTransition()
    {
        _stateMachine.AddAnyTransition(_attackState, () => _context.Parameters.CanAttack && _context.Parameters.IsCombat && _context.Parameters.TimeAttack <= 0);
        base.SetUpTransition();
        _stateMachine.AddTransition(_attackState, _chaseState,() => !_context.Parameters.CanAttack && !_context.Parameters.IsBlock);
    }
}
