using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// An enemy that patrols back and forth on a platform.
/// </summary>
class PatrollingEnemy : AnimatedGameObject
{
    protected Level level; 
    Vector2 startPosition; // The position at which this enemy starts.
    protected float waitTime; // The current remaining time before the enemy turns around.
    const float totalWaitTime = 0.5f; // The time it takes before the enemy turns around.
    const float walkSpeed = 120; // The horizontal speed at which the enemy moves.

    public PatrollingEnemy(Level level, Vector2 startPosition) : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;

        LoadAnimation("Sprites/LevelObjects/Flame/spr_flame@9", "default", true, 0.1f);

        Reset();
    }

    public override void Reset()
    {
        // start by moving to the right
        waitTime = 0;
        velocity.X = walkSpeed;
        PlayAnimation("default");
        sprite.Mirror = false;

        // go to the start position
        localPosition = startPosition;
        Origin = new Vector2(sprite.Width / 2, sprite.Height);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // if we're waiting at the edge of a platform, turn around after some time
        if (waitTime > 0)
        {
            waitTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (waitTime <= 0)
                TurnAround();
        }

        // otherwise, if we've reached the edge of a platform, start waiting
        else if (!CanMoveForward())
        {
            waitTime = totalWaitTime;
            velocity.X = 0;
        }

        // a collision with the player causes the player to die
        if (level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
            level.Player.Die();
    }

    /// <summary>
    /// Checks and returns if this enemy can keep moving in its current direction.
    /// </summary>
    protected bool CanMoveForward()
    {
        // determine the position to check
        Rectangle bbox = BoundingBox;
        Vector2 side;
        side.Y = bbox.Bottom + 1;
        if (sprite.Mirror)
            side.X = BoundingBox.Left;
        else
            side.X = BoundingBox.Right;
        
        Point tilePos = level.GetTileCoordinates(side);

        // we can continue moving if there's a platform below, and no wall ahead
        return level.GetTileType(tilePos.X, tilePos.Y) != Tile.Type.Empty
            && level.GetTileType(tilePos.X, tilePos.Y - 1) != Tile.Type.Wall;
    }

    /// <summary>
    /// Starts moving the enemy in the other direction.
    /// </summary>
    protected void TurnAround()
    {
        sprite.Mirror = !sprite.Mirror;
        velocity.X = walkSpeed;
        if (sprite.Mirror)
            velocity.X *= -1;
    }
}
