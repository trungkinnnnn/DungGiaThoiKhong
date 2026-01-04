using Unity.VisualScripting;
using UnityEngine;

public class DashStateBehaviour : IStateBehaviour
{
    private PlayerContext _context;
    private float _originalGravity;
    private float _timer = 0f;  
    public DashStateBehaviour(PlayerContext context)
    {
        _context = context;
    }

    public void Enter()
    {
        _context.Animator.PlayAniDashing();
        _context.Parameters.DashTimer = _context.DataMovement.cooldownDash;
        SetupParaDoneDashAndBlockInput(true);
        AddForceDash();
    }

    private void SetupParaDoneDashAndBlockInput(bool value)
    {
        _context.Parameters.DoneDash = !value;
        _context.Input.Block = value;
        _context.Collider.enabled = !value;
    }

    private void AddForceDash()
    {
        _originalGravity = _context.Rigidbody.gravityScale;
        _context.Rigidbody.gravityScale = 0f;

        _context.Rigidbody.velocity = new Vector2(_context.Rigidbody.velocity.x, 0f);

        float dir = Mathf.Sign(_context.Transform.localScale.x);
        _context.Rigidbody.AddForce(Vector2.right * dir * _context.DataMovement.dashForce, ForceMode2D.Impulse);
    }

    public void Exit()
    {
        _timer = 0f;
    }

    public void Update()
    {
        DoneDashing();
    }

    private void DoneDashing()
    {
        _timer += Time.deltaTime;
        if(_timer >= _context.DataMovement.dashTime)
        {
            _context.Rigidbody.gravityScale = _originalGravity;
            SetupParaDoneDashAndBlockInput(false);
        }    
    }

}