using UnityEngine;
public class MeleeChaseState : IStateBehaviour
{
    private MeleeContext _context;
    public MeleeChaseState(MeleeContext context)
    {
        _context = context;
    }
    public void Enter() {
        _context.Parameters.CanAttack = false; 
    }

    public void Exit() { _context.Parameters.DesiredVelocity = Vector2.zero; }

    public void Update()
    {
        MoveIntoAttackZone();
    }

    private void MoveIntoAttackZone()
    {
        Vector2 meleePos = _context.Transform.position;
        Vector2 playerPos = _context.Player.transform.position;

        float distance = Vector2.Distance(meleePos, playerPos);

        Vector2 velocity = Vector2.zero;
        Vector2 dirToPlayer = (playerPos - meleePos).normalized;

        FacePlayer(dirToPlayer.x);

        if (distance > _context.DataAttack.maxRanger)
        {
            velocity.x = dirToPlayer.x * _context.DataMovement.speedCombat;
            _context.Animator.SetBoolAniRunning(true);
        }
        else
        {
            velocity = Vector2.zero;
            _context.Parameters.CanAttack = true;
            _context.Animator.SetBoolAniRunning(false);
        }

        _context.Parameters.DesiredVelocity = Vector2.MoveTowards(_context.Parameters.DesiredVelocity, 
                                                velocity, _context.DataMovement.acceleration * Time.deltaTime);

    }

    private void FacePlayer(float dirX)
    {
        Vector3 scale = _context.Transform.localScale;
        scale.x = dirX > 0 ? 1 : -1;
        _context.Transform.localScale = scale;
    }
}
