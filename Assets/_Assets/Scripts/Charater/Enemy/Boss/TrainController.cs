using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [SerializeField] EnemyDataAttack _dataAttack1;
    [SerializeField] EnemyDataAttack _dataAttack2;
    [SerializeField] EnemyDataAttack _dataAttack3;

    [SerializeField] EnemyDataMovement _dataMovement;

    [SerializeField] GameObject _shieldObj;
    [SerializeField] GameObject _attack3;

    private WorldBoundsChecker _boundsChecker;
    private PlayerSingle _playerSingle;

    private TrainContext _context;
    private StateMachine _stateMachine;
    private TrainIdleState _idleState;
    private TrainPatrolState _patrolState;
    private TrainChaseState _chaseState;
    private TrainAttack1State _attack1State;
    private TrainAttack2State _attack2State;
    private TrainAttack3State _attack3State;
    private void Start()
    {
        _playerSingle = PlayerSingle.Instance;
        InitContext();
        InitState();
        SetUpTransition();
        _boundsChecker = GetComponent<WorldBoundsChecker>();
        _stateMachine.Init(_idleState);
    }

    private void InitContext()
    {
        _context = new TrainContext(
            transform,
            GetComponent<Rigidbody2D>(),
            _dataAttack1,
            _dataAttack2,
            _dataAttack3,
            _dataMovement,
            GetComponent<TrainAnimationController>()
            );
    }    

    private void InitState()
    {
        _stateMachine = new StateMachine();
        _idleState = new TrainIdleState(_context);
        _chaseState = new TrainChaseState(_context);
        _patrolState = new TrainPatrolState(_context);
        _attack1State = new TrainAttack1State(_context);
        _attack2State = new TrainAttack2State(_context, _shieldObj);
        _attack3State = new TrainAttack3State(_context, _attack3);
    }

    private void SetUpTransition()
    {
        // ============== AnyState =================
        _stateMachine.AddAnyTransition(_attack3State, () => !_context.Parameters.IsBlock && _context.Parameters.TimeAttack3 <= 0);
        _stateMachine.AddAnyTransition(_attack2State, () => !_context.Parameters.IsBlock && _context.Parameters.TimeAttack2 <= 0 && _context.Parameters.CanAttack2);
        _stateMachine.AddAnyTransition(_attack1State, () => !_context.Parameters.IsBlock && _context.Parameters.TimeAttack1 <= 0 && _context.Parameters.CanAttack1);
        _stateMachine.AddAnyTransition(_chaseState, () => !_context.Parameters.IsBlock && _context.Parameters.IsCombat);

        // ============== IdleMove flow ==============
        _stateMachine.AddTransition(_patrolState, _idleState, () => !_context.Parameters.IsBlock && !_context.Parameters.IsRunning);
        _stateMachine.AddTransition(_idleState, _patrolState, () => !_context.Parameters.IsBlock && _context.Parameters.IsRunning);

        // ============= Attack Flow =================
        _stateMachine.AddTransition(_attack3State, _chaseState, () => !_context.Parameters.IsBlock);
        _stateMachine.AddTransition(_attack1State, _chaseState, () => !_context.Parameters.IsBlock);
       
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
        UpdateAttackDir();
    }

    private void UpdateBounds()
    {
        _context.Parameters.IsForward = _boundsChecker.IsForward();
    }

    private void UpdateTime()
    {
        if(_context.Parameters.TimeAttack1 > 0) _context.Parameters.TimeAttack1 -= Time.deltaTime;
        if(_context.Parameters.TimeAttack2 > 0) _context.Parameters.TimeAttack2 -= Time.deltaTime;
        if(_context.Parameters.TimeAttack3 > 0) _context.Parameters.TimeAttack3 -= Time.deltaTime;
    }

    private void UpdateAttackDir()
    {
        _context.Parameters.CanAttack2 = Vector2.Distance(transform.position, _playerSingle.transform.position) > _context.DataAttack2.maxRanger;
    }


}
