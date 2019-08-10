using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// Represents a turtle enemy that sneezes periodically.
/// It can be used as a trampoline whenever it's not sneezing.
/// </summary>
class Turtle : AnimatedGameObject
{
    Level level; // A reference to the level in which the enemy lives.
    bool sneezing; // Whether the sneeze animation is currently being shown.
    float timer; // The remaining time before switching to another state.
    const float timeBetweenStates = 5.0f; // The time between the "sneeze" and "idle" states.
    const float launchSpeed = 1200; // The speed at which the player can get launched.

    public Turtle(Level level) : base(TickTick.Depth_LevelObjects)
    {
        this.level = level;
        LoadAnimation("Sprites/LevelObjects/Turtle/spr_sneeze@9", "sneeze", false, 0.2f);
        LoadAnimation("Sprites/LevelObjects/Turtle/spr_idle", "idle", true, 0.1f);
        Reset();
    }

    public override void Reset()
    {
        // go to the "idle" state
        sneezing = false;
        timer = timeBetweenStates;
        PlayAnimation("idle");
        // set the origin to the bottom of the turtle's body
        Origin = new Vector2(sprite.Width / 2, 120);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        // choose the correct animation to play
        if (sneezing) PlayAnimation("sneeze");
        else PlayAnimation("idle");

        // decrease the timer. If enough time has passed, switch between states
        timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer <= 0)
        {
            timer = timeBetweenStates;
            sneezing = !sneezing;
        }

        if (level.Player.CanCollideWithObjects)
        {
            // when spikes are out, a collision with the player causes the player to die
            if (HasSpikesOut && HasPixelPreciseCollision(level.Player))
                level.Player.Die();
            // otherwise, the player gets launched up if it touches the turtle while falling
            else if (level.Player.IsFalling && HasPixelPreciseCollision(level.Player))
                level.Player.Jump(launchSpeed);
        }
    }
    
    bool HasSpikesOut
    {
        get { return sneezing && sprite.SheetIndex >= 2; }
    }
}
