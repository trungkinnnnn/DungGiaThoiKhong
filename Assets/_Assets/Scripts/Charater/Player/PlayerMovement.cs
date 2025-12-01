using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerDataMovement _dataMovement;
    [SerializeField] Transform _groudCheckTransform;
    [SerializeField] LayerMask _layerGroundMask;

    private bool _isGrounded = true;
    private bool _canDash = true;
    private bool _isDashed = false;

    private Rigidbody2D _rig;
    private IPlayerInput _input;

    private void Awake()
    {
        #if UNITY_ANDROID
                _input = new MoblieInput();
        #else
                _input = new PCInput();
        #endif
    }

    private void Start()
    {
        _rig = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDashed) return;
        CheckGround();
        HandleMove();
        HandleJump();
        HandleDash();
        ChangeScaleX();
    }

    private void HandleMove()
    {
        var velocity = _rig.velocity;
        velocity.x = _input.Horizontal * _dataMovement.speed;
        _rig.velocity = velocity;
    }

    private void HandleJump()
    {
        if (_isGrounded && _input.JumPressed)
        {
            _rig.AddForce(Vector2.up * _dataMovement.jumForce, ForceMode2D.Impulse);
            _input.ResetJump();
        }
    }

    private void HandleDash()
    {
        if(_input.DashPressed && _canDash)
        {
            StartCoroutine(Dash());
        }    
    }    

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashed = true;

        float originalGravity = _rig.gravityScale;
        _rig.gravityScale = 0f;

        _rig.velocity = new Vector2(_rig.velocity.x, 0f);

        float dir = Mathf.Sign(transform.localScale.x);
        _rig.AddForce(Vector2.right * dir * _dataMovement.dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_dataMovement.dashTime);

        _rig.gravityScale = originalGravity;
        _isDashed = false;  

        yield return new WaitForSeconds(_dataMovement.timeDelayDash);
        _canDash = true;
    }    

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groudCheckTransform.position, 0.2f, _layerGroundMask);
    }    

    private void ChangeScaleX()
    {
        if (_input.Horizontal == 0) return;

        Vector3 origin = transform.localScale;
        origin.x = _input.Horizontal;
        transform.localScale = origin;
    }    

    // ============= Serivce ==============
     public IPlayerInput GetPlayerInput() => _input;    

}
