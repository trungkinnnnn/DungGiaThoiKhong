using System.Collections;
using System.Net.WebSockets;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerDataMovement _dataMovement;
    [SerializeField] Transform _groudCheckTransform;
    [SerializeField] LayerMask _layerGroundMask;

    private float speedMovement;
    private bool _isGrounded = true;
    private bool _canDash = true;

    private PlayerAnimationController _ani;
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
        _ani = GetComponent<PlayerAnimationController>();
        speedMovement = _dataMovement.speedNormal;
    }

    private void Update()
    {
        if (_input.Block) return;
        CheckGround();
        HandleMove();
        HandleJump();
        HandleDash();
        ChangeScaleX();
    }

    private void HandleMove()
    {
        _ani.PlayAniRunning(_input.Horizontal != 0);
        var targetVelocityX = _input.Horizontal * speedMovement;
        float smooth = _dataMovement.accelerationTime;

        float newVelocityX = Mathf.Lerp(_rig.velocity.x, targetVelocityX, smooth * Time.fixedDeltaTime);
        _rig.velocity = new Vector2(newVelocityX, _rig.velocity.y);
    }

    private void HandleJump()
    {
        if (_isGrounded && _input.JumPressed)
        {
            _ani.PlayAniJumping();
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
        _input.Block = true;

        float originalGravity = _rig.gravityScale;
        _rig.gravityScale = 0f;

        _rig.velocity = new Vector2(_rig.velocity.x, 0f);

        float dir = Mathf.Sign(transform.localScale.x);
        _rig.AddForce(Vector2.right * dir * _dataMovement.dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_dataMovement.dashTime);

        _rig.gravityScale = originalGravity;
        _input.Block = false;
        _input.ResetDash();

        yield return new WaitForSeconds(_dataMovement.timeDelayDash);
        _canDash = true;
    }    

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groudCheckTransform.position, 0.2f, _layerGroundMask);
        _ani.SetIsGround(_isGrounded);
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

    public void IsAttack(bool value)
    {
        speedMovement = value ? _dataMovement.speedAttack : _dataMovement.speedNormal;
    }    
    
}
