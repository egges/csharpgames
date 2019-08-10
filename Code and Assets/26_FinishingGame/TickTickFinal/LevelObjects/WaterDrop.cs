using System;
using Microsoft.Xna.Framework;
using Engine;

class WaterDrop : SpriteGameObject
{
    Level level;
    protected float bounce;
    Vector2 startPosition;

    public WaterDrop(Level level, Vector2 startPosition) : base("Sprites/LevelObjects/spr_water", TickTick.Depth_LevelObjects)
    {
        this.level = level;
        this.startPosition = startPosition;

        SetOriginToCenter();

        Reset();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + LocalPosition.X;
        bounce = (float)Math.Sin(t) * 0.2f;
        localPosition.Y += bounce;

        // check if the player collects this water drop
        if (Visible && level.Player.CanCollideWithObjects && HasPixelPreciseCollision(level.Player))
        {
            Visible = false;
            ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_watercollected");
        }
            
    }

    public override void Reset()
    {
        localPosition = startPosition;
        Visible = true;
    }
}