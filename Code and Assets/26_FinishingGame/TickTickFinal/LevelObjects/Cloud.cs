using Engine;
using Microsoft.Xna.Framework;

class Cloud : SpriteGameObject
{
    Level level;

    public Cloud(Level level) : base(null, TickTick.Depth_Background)
    {
        this.level = level;
        Reset();
    }

    public override void Reset()
    {
        Randomize();

        // give the cloud a starting position inside the level
        localPosition.X = ExtendedGame.Random.Next(-sprite.Width, level.BoundingBox.Width);
    }

    void Randomize()
    {
        // set a random sprite and depth
        float depth = TickTick.Depth_Background + (float)ExtendedGame.Random.NextDouble() * 0.2f;
        sprite = new SpriteSheet("Sprites/Backgrounds/spr_cloud_" + ExtendedGame.Random.Next(1, 6), depth);

        // set a random y-coordinate and speed
        float y = ExtendedGame.Random.Next(100, 600) - sprite.Height;
        float speed = ExtendedGame.Random.Next(10, 50);

        if (ExtendedGame.Random.Next(2) == 0)
        {
            // go from right to left
            localPosition = new Vector2(level.BoundingBox.Width, y);
            velocity = new Vector2(-speed, 0);
        }
        else
        {
            // go from left to right
            localPosition = new Vector2(-sprite.Width, y);
            velocity = new Vector2(speed, 0);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (velocity.X < 0 && localPosition.X < -sprite.Width)
            Randomize();
        else if (velocity.X > 0 && localPosition.X > level.BoundingBox.Width)
            Randomize();
    }
}
