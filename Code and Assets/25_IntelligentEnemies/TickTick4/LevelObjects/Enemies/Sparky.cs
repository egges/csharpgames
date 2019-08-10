using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents the "Sparky" enemy that can drop down and electrocute the player.
/// </summary>
class Sparky : AnimatedGameObject
{
    Level level;
    Vector2 basePosition; // The bottom-most position that Sparky can reach.
    
    float timeUntilDrop; // The current remaining time until Sparky drops down.
    const float minTimer = 3, maxTimer = 5; // Range of possible times before the drops down.
    const float floatingHeight = 120; // The height at which Sparky floats above the ground.
    const float fallSpeed = 300, riseSpeed = 60; // The speed for falling and rising.

    public Sparky(Level level, Vector2 basePosition) : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.basePosition = basePosition;

        LoadAnimation("Sprites/LevelObjects/Sparky/spr_electrocute@6x5", "electrocute", false, 0.1f);
        LoadAnimation("Sprites/LevelObjects/Sparky/spr_idle", "idle", true, 0.1f);
        Reset();
    }

    public override void Reset()
    {
        // play the idle animation
        PlayAnimation("idle");
        
        // start by floating above the base position
        Origin = new Vector2(sprite.Width / 2, 135);
        localPosition = basePosition;
        localPosition.Y -= floatingHeight;
        velocity = Vector2.Zero;

        // set a random timer
        timeUntilDrop = minTimer + (float)ExtendedGame.Random.NextDouble() * (maxTimer - minTimer);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (timeUntilDrop > 0)
        {
            // decrease the timer. If enough time has passed, start falling down
            timeUntilDrop -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeUntilDrop <= 0)
            {
                PlayAnimation("electrocute");
                velocity.Y = fallSpeed;
            }
        }
        else
        {
            // stop falling?
            if (velocity.Y > 0 && localPosition.Y >= basePosition.Y)
                velocity.Y = 0;

            // start rising?
            if (velocity.Y == 0 && (sprite as Animation).AnimationEnded)
                velocity.Y = -riseSpeed;
            
            // stop rising?
            if (velocity.Y < 0 && localPosition.Y <= basePosition.Y - floatingHeight)
                Reset();

            // electrocute the player?
            if (IsDeadly && HasPixelPreciseCollision(level.Player))
                level.Player.Die();
        }
    }

    /// <summary>
    /// Returns whether this Sparky instance is currently harmful to the player.
    /// This is true during certain frames of the "electrocute" animation.
    /// </summary>
    bool IsDeadly
    {
        get { return timeUntilDrop <= 0 && SheetIndex >= 11 && SheetIndex <= 27; }
    }
}
