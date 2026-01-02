using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] PlayerDataAttack _dataAttack;
    [SerializeField] PlayerDataMovement _dataMovement;

    [Header("Ground")]
    [SerializeField] Transform _groundCheckTransform;
    [SerializeField] LayerMask _layerGroundMark;

    private Movement _movement;
    private PlayerContext _context;
    private StateMachine _stateMachine;
    private IPlayerInput _input;


    // State
    private IdleStateBehaviour _idleState;
    private RunStateBehaviour _runState;
    private JumpUpStateBehaviour _jumpUpState;
    private JumpDownStateBehaviour _jumpDownState;
    private DashStateBehaviour _dashState;
    private Attack1StateBehaviour _attackState;
    private Attack2StateBehaviour _attack2State;
    private Attack3StateBehaviour _attack3State;

    private void Awake()
    {
        #if UNITY_ANDROID
                _input = new MoblieInput();
        #else
                _input = new PCInput();
        #endif

        InitContext();
        InitState();
        SetUpTransition();

        _movement = new Movement(_context);
    }

    private void InitContext()
    {
        _context = new PlayerContext(
            _input,
            GetComponent<Rigidbody2D>(),
            GetComponent<PlayerAnimationController>(),
            transform,
            _dataMovement,
            _dataAttack
            );
    }

    private void InitState()
    {
        _stateMachine = new StateMachine();

        _idleState = new IdleStateBehaviour(_context);
        _runState = new RunStateBehaviour(_context);
        _jumpUpState = new JumpUpStateBehaviour(_context);
        _jumpDownState = new JumpDownStateBehaviour(_context);
        _dashState = new DashStateBehaviour(_context);

        _attack3State = new Attack3StateBehaviour(_context);
        _attack2State = new Attack2StateBehaviour(_context, _stateMachine, _attack3State);
        _attackState = new Attack1StateBehaviour(_context, _stateMachine, _attack2State);
    }       

    private void SetUpTransition()
    {

        // ========== AnyState Trigger =========
        _stateMachine.AddAnyTransition(_attackState, () =>
            _context.Parameters.AttackPressed && _context.Parameters.AttackTimer <= 0
        );

        _stateMachine.AddAnyTransition(_dashState, () =>
            _context.Parameters.DashTimer <= 0 && _context.Parameters.DashPressed
        );

        _stateMachine.AddAnyTransition(_jumpUpState, () =>
            _context.Parameters.JumPressed && _context.Parameters.IsGrounded
        );

        _stateMachine.AddAnyTransition(_jumpDownState, () =>
            !_context.Parameters.IsGrounded && _context.Rigidbody.velocity.y < -0.5f && 
            !_context.Input.Block
        );



        // ============== Idle <=> Run ===========

        _stateMachine.AddTransition(_idleState, _runState, () =>
            Mathf.Abs(_context.Parameters.Horizontal) >= 0.1f
        );

        _stateMachine.AddTransition(_runState, _idleState, () =>
            Mathf.Abs(_context.Parameters.Horizontal) <= 0.1f
        );

        // ============== JumpFlow ================
        _stateMachine.AddTransition(_jumpUpState, _jumpDownState, () => _context.Rigidbody.velocity.y <= 0 && !_context.Input.Block);
        _stateMachine.AddTransition(_jumpDownState, _idleState, () => _context.Parameters.IsGrounded );

        // ============== DashFlow ================
        _stateMachine.AddTransition(_dashState, _idleState, () => _context.Parameters.DoneDash && _context.Parameters.IsGrounded);
        _stateMachine.AddTransition(_dashState, _jumpDownState, () => _context.Parameters.DoneDash && !_context.Parameters.IsGrounded);

        // ============== AttackFlow ==============
        _stateMachine.AddTransition(_attackState, _jumpDownState, () => _context.Parameters.DoneAttack && !_context.Parameters.IsGrounded);
        _stateMachine.AddTransition(_attackState, _idleState, () => _context.Parameters.DoneAttack && _context.Parameters.IsGrounded);
        _stateMachine.AddTransition(_attack2State, _idleState, () => _context.Parameters.DoneAttack);
        _stateMachine.AddTransition(_attack3State, _idleState, () => _context.Parameters.DoneAttack);
    }

    private void Start()
    {
        _stateMachine.Init(_idleState);
    }

    private void Update()
    {
        UpdateParameters();
        _stateMachine.Update();
        UpdateVisuals();
    }

    private void FixedUpdate()
    {
        _movement.UpdatePosition();
    }

    private void UpdateParameters()
    {
        UpdateParaInput();
        UpdateParaGround();
        UpdateParaTimers();
    }    

    private void UpdateParaInput()
    {
        _context.Parameters.Horizontal = _input.Horizontal;
        _context.Parameters.AttackPressed = _input.AttackPressed;
        _context.Parameters.DashPressed = _input.DashPressed;
        _context.Parameters.JumPressed = _input.JumPressed;
    }

    private void UpdateParaGround()
    {
        bool isGround = Physics2D.OverlapCircle(_groundCheckTransform.position, 0.2f, _layerGroundMark);
        _context.Parameters.IsGrounded = isGround;
        _context.Animator.SetIsGround(isGround);
    }

    private void UpdateParaTimers()
    {
        if(_context.Parameters.DashTimer > 0) _context.Parameters.DashTimer -= Time.deltaTime;
        if(_context.Parameters.AttackTimer > 0) _context.Parameters.AttackTimer -= Time.deltaTime;
    }    

    private void UpdateVisuals()
    {
        if(_context.Parameters.Horizontal != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = _context.Parameters.Horizontal;
            transform.localScale = scale;
        }    
    }    

    // =============== Service ==============
    public IPlayerInput GetInput() => _input;

}
