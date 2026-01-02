using TMPro;
using UnityEngine;

public class RangerController : MonoBehaviour
{
    [SerializeField] RangerDataAttack _dataAttack;
    [SerializeField] EnemyDataMovement _dataMovement;

    [SerializeField] GameObject _attackObj;

    private WorldBoundsChecker _boundsChecker;

    private RangerContext _context;
    private StateMachine _stateMachine;
    private IdleState _idleState;
    private IdleMoveState _idleMoveState;
    private MoveRangerAttackState _moveAttackState;
    private RangerAttackState _attackState;
    private void Start()
    {
        InitContext();
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
        _idleState = new IdleState(_context);
        _idleMoveState = new IdleMoveState(_context);
        _moveAttackState = new MoveRangerAttackState(_context);
        _attackState = new RangerAttackState(_context, _attackObj);
    }

    private void SetUpTransition()
    {
        // ============== AnyState =================
        _stateMachine.AddAnyTransition(_attackState, () => _context.Parameters.CanAttack && _context.Parameters.TimeAttack <= 0);
        _stateMachine.AddAnyTransition(_moveAttackState, () => !_context.Parameters.IsBlock && _context.Parameters.IsCombat);

        // ============== IdleMove flow ==============
        _stateMachine.AddTransition(_idleMoveState, _idleState, () => !_context.Parameters.IsBlock && !_context.Parameters.IsRunning);
        _stateMachine.AddTransition(_idleState, _idleMoveState, () => !_context.Parameters.IsBlock && _context.Parameters.IsRunning);

        // ============== Attack Flow ================
        _stateMachine.AddTransition(_attackState, _moveAttackState, () => !_context.Parameters.CanAttack && _context.Parameters.IsCombat);

    }

    private void Update()
    {
        UpdateParameters();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _context.Rigidbody2D.velocity = _context.Parameters.DesiredVelocity;
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

}
