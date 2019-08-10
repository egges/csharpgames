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

        LoadAnimation("Sprites/LevelObjects/Sparky/spr_electrocute@6x5",
			"electrocute", false, 0.1f);
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
        timeUntilDrop = minTimer 
            + (float)ExtendedGame.Random.NextDouble() * (maxTimer - minTimer);
    }
	
    ...