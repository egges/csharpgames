using System;
using Microsoft.Xna.Framework;

/// <summary>
/// A variant of PatrollingEnemy that follows the player whenever the player moves.
/// </summary>
class PlayerFollowingEnemy : PatrollingEnemy
{
    public PlayerFollowingEnemy(Level level, Vector2 startPosition) 
        : base(level, startPosition) { }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // if the player is moving and we're not already waiting, follow the player
        if (level.Player.CanCollideWithObjects && level.Player.IsMoving && velocity.X != 0)
        {
            float dx = level.Player.GlobalPosition.X - GlobalPosition.X;
            if (Math.Sign(dx) != Math.Sign(velocity.X) && Math.Abs(dx) > 100)
                TurnAround();
        }
    }
}

