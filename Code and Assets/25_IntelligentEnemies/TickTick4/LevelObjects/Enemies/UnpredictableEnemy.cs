using System;
using Engine;
using Microsoft.Xna.Framework;

/// <summary>
/// A variant of PatrollingEnemy that turns around at random moments.
/// </summary>
class UnpredictableEnemy : PatrollingEnemy
{
    const float minSpeed = 80, maxSpeed = 140;
    
    public UnpredictableEnemy(Level level, Vector2 startPosition) 
        : base(level, startPosition) { }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (waitTime <= 0 && ExtendedGame.Random.NextDouble() <= 0.01)
        {
            TurnAround();

            // select a random speed
            float randomSpeed = minSpeed + (float)ExtendedGame.Random.NextDouble() * (maxSpeed - minSpeed);
            velocity.X = Math.Sign(velocity.X) * randomSpeed;
        }
    }
}

