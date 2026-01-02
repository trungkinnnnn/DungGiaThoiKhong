using UnityEngine;

public class Movement
{
    private PlayerContext _context;

    public Movement(PlayerContext context)
    {
        _context = context;
    }

    public void UpdatePosition()
    {
        float smooth = _context.DataMovement.acceleration;
        if (_context.Input.Block)
        {
            float targetX = Mathf.MoveTowards(_context.Rigidbody.velocity.x, 0, smooth * Time.fixedDeltaTime);
            _context.Rigidbody.velocity = new Vector2(targetX, _context.Rigidbody.velocity.y);
            return;
        }    
        float targetVelocityX = _context.Parameters.Horizontal * _context.Parameters.currentSpeed;
      
        float newVeloctyX = Mathf.Lerp(_context.Rigidbody.velocity.x, targetVelocityX, smooth * Time.fixedDeltaTime);

        _context.Rigidbody.velocity = new Vector2(newVeloctyX, _context.Rigidbody.velocity.y);
    }
}
