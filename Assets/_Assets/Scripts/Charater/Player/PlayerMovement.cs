using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerDataMovement _dataMovement;
    [SerializeField] Transform _groudCheckTransform;
    [SerializeField] LayerMask _layerGroundMask;

    private bool _isGrounded = true;

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
        CheckGround();
        Move();
        Jump();
    }

    private void Move()
    {
        var velocity = _rig.velocity;
        velocity.x = _input.Horizontal * _dataMovement.speed;
        _rig.velocity = velocity;
    }

    private void Jump()
    {
        if (_isGrounded && _input.JumPressed)
        {
            _rig.AddForce(Vector2.up * _dataMovement.jumForce, ForceMode2D.Impulse);
            _input.LastUpdate();
        }
    }
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groudCheckTransform.position, 0.2f, _layerGroundMask);
    }    

    // ============= Serivce ==============
     public IPlayerInput GetPlayerInput() => _input;    

}
