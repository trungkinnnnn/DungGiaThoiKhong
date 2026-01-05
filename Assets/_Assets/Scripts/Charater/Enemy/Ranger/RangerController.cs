using TMPro;
using UnityEngine;

public class RangerController : MonoBehaviour, IComBat
{
    [SerializeField] EnemyDataAttack _dataAttack;
    [SerializeField] EnemyDataMovement _dataMovement;

    [SerializeField] GameObject _attackObj;

    private WorldBoundsChecker _boundsChecker;

    private RangerContext _context;
    private StateMachine _stateMachine;
    private RangerIdleState _idleState;
    private RangerPatrolState _patrolState;
    private RangerChaseState _chaseState;
    private RangerAttackState _attackState;

    private void Awake()
    {
        InitContext();
    }

    private void Start()
    {
        
        InitState();
        SetUpTransition();

        _boundsChecker = GetComponent<WorldBoundsChecker>();
        _stateMachine.Init(_idleState);
    }

    private void InitContext()
    {
        _context = new RangerContext(
            transform,
            GetComponent<Rigidbody2D>(),
            _dataAttack,
            _dataMovement,
            GetComponent<EnemyAnimationController>()
           );
    }

    private void InitState()
    {
        _stateMachine = new StateMachine();
        _idleState = new RangerIdleState(_context);
        _patrolState = new RangerPatrolState(_context);
        _chaseState = new RangerChaseState(_context);
        _attackState = new RangerAttackState(_context, _attackObj);
    }

    private void SetUpTransition()
    {
        // ============== AnyState =================
        _stateMachine.AddAnyTransition(_attackState, () => _context.Parameters.CanAttack && _context.Parameters.TimeAttack <= 0);
        _stateMachine.AddAnyTransition(_chaseState, () => !_context.Parameters.IsBlock && _context.Parameters.IsCombat);

        // ============== ChaseFlow ==================
        _stateMachine.AddTransition(_chaseState, _patrolState, () => !_context.Parameters.IsBlock && !_context.Parameters.IsCombat);

        // ============== IdleMove flow ==============
        _stateMachine.AddTransition(_patrolState, _idleState, () => !_context.Parameters.IsBlock && !_context.Parameters.IsRunning);
        _stateMachine.AddTransition(_idleState, _patrolState, () => !_context.Parameters.IsBlock && _context.Parameters.IsRunning);

        // ============== Attack Flow ================
        _stateMachine.AddTransition(_attackState, _chaseState, () => !_context.Parameters.CanAttack && _context.Parameters.IsCombat);

    }

    private void Update()
    {
        UpdateParameters();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _context.Rigidbody.velocity = _context.Parameters.DesiredVelocity;
    }

    private void UpdateParameters()
    {
        UpdateBoundsCheck();
        UpdateTime();
    }   

    private void UpdateBoundsCheck()
    {
        _context.Parameters.IsTop = _boundsChecker.IsTop();
        _context.Parameters.IsBottom = _boundsChecker.IsBottom();
        _context.Parameters.IsForward = _boundsChecker.IsForward();
    }    

    private void UpdateTime()
    {
        if(_context.Parameters.TimeAttack > 0) _context.Parameters.TimeAttack -= Time.deltaTime;
    }


    // ============= Service ===========
    public void SetParaCombat(bool value) => _context.Parameters.IsCombat = value;
}
