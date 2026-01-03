using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    [SerializeField] EnemyDataAttack _dataAttack;
    [SerializeField] EnemyDataMovement _dataMovement;

    private WorldBoundsChecker _boundsChecker;
    
    protected MeleeContext _context;
    protected StateMachine _stateMachine;
    private MeleeIdleState _idleState;
    private MeleePatrolState _patrolState;
    protected MeleeChaseState _chaseState;
    private void Start()
    {
        InitContext();
        InitState();
        SetUpTransition();

        _boundsChecker = GetComponent<WorldBoundsChecker>();
        _stateMachine.Init(_idleState);
        //_context.Parameters.IsCombat = false;
    }

    private void InitContext()
    {
        _context = new MeleeContext(
            transform,
            GetComponent<Rigidbody2D>(),
            _dataAttack,
            _dataMovement,
            GetComponent<EnemyAnimationController>()
            );
    }    


    protected virtual void InitState()
    {
        _stateMachine = new StateMachine();
        _idleState = new MeleeIdleState(_context);
        _patrolState = new MeleePatrolState(_context);
        _chaseState = new MeleeChaseState(_context);
    }

    protected virtual void SetUpTransition()
    {
        // ============== AnyState =================
        _stateMachine.AddAnyTransition(_chaseState, () => !_context.Parameters.IsBlock && _context.Parameters.IsCombat);

        // ============== IdleMove flow ==============
        _stateMachine.AddTransition(_patrolState, _idleState, () => !_context.Parameters.IsBlock && !_context.Parameters.IsRunning);
        _stateMachine.AddTransition(_idleState, _patrolState, () => !_context.Parameters.IsBlock && _context.Parameters.IsRunning);

    }

    private void Update()
    {
        UpdateParameters();
        _stateMachine?.Update();
    }

    private void FixedUpdate()
    {
        if (_context.Parameters.IsBlock) return;
        var velocity = _context.Rigidbody.velocity;
        velocity.x = _context.Parameters.DesiredVelocity.x;
        _context.Rigidbody.velocity = velocity;
    }

    private void UpdateParameters()
    {
        UpdateBounds();
        UpdateTime();
    }

    private void UpdateBounds()
    {
        _context.Parameters.IsForward = _boundsChecker.IsForward();
    }

    private void UpdateTime()
    {
        if(_context.Parameters.TimeAttack > 0) _context.Parameters.TimeAttack -= Time.deltaTime;    
    }

}
