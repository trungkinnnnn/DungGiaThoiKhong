using UnityEngine;

public class MoveRangerAttackState : IStateBehaviour
{
    private RangerContext _context;
   
    public MoveRangerAttackState(RangerContext rangerContext)
    {
        _context = rangerContext;
    }
    public void Enter() { _context.Parameters.CanAttack = false; }


    public void Exit() { _context.Parameters.DesiredVelocity = Vector2.zero; }

    public void Update()
    {
        MoveIntoAttackZone();
    }

    private void MoveIntoAttackZone()
    {
        Vector2 dronePos = _context.Transform.position;
        Vector2 playerPos = _context.Player.transform.position;

        float distance = Vector2.Distance(dronePos, playerPos);

        Vector2 velocity = Vector2.zero;
        Vector2 dirToPlayer = (playerPos - dronePos).normalized;

        FacePlayer(dirToPlayer.x);

        if(distance > _context.DataAttack.maxRanger)
        {
            velocity = dirToPlayer * _context.DataMovement.speedCombat;
        }else if(distance < _context.DataAttack.minRanger)
        {
            Vector2 evadeDir = new Vector2(-dirToPlayer.x, 1f).normalized;
            velocity = evadeDir * _context.DataMovement.speedCombat;
        }else
        {
            velocity = Vector2.zero;
            _context.Parameters.CanAttack = true;
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