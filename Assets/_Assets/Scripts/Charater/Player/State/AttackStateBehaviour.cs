using UnityEngine;

public class AttackStateBehaviour : IStateBehaviour
{
    private PlayerContext _context;
    public AttackStateBehaviour(PlayerContext playerContext)
    {
        _context = playerContext;
    }

    public void Enter()
    {
        Debug.Log("State Attack");
        _context.Animator.PlayAniAttack();
        _context.Parameters.AttackTimer = _context.DataAttack.timeAttack;
        _context.Input.Block = true;
        _context.Parameters.CanAttack = false;
        MonoBehaviourHelper.Instance.StartCoroutine(AttackCooldown());
        
    }

    public void Exit()
    {

    }

    public void Update()
    {
        UpdatePosition();
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_context.Parameters.AttackTimer);
        _context.Input.Block = false;
        yield return new WaitForSeconds(_context.DataAttack.attackCooldown);
        _context.Parameters.CanAttack = true;
    }

    private void UpdatePosition()
    {
        float targetVelocityX = _context.Parameters.Horizontal * _context.DataMovement.speedAttack;
        float smooth = _context.DataMovement.accelerationTime;

        float newVeloctyX = Mathf.Lerp(_context.Rigidbody.velocity.x, targetVelocityX, smooth * Time.fixedDeltaTime);

        _context.Rigidbody.velocity = new Vector2(newVeloctyX, _context.Rigidbody.velocity.y);
    }
}
