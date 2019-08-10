using System;
using Microsoft.Xna.Framework;
using Engine;

class WaterDrop : SpriteGameObject
{
    protected float bounce;

    public WaterDrop() : base("Sprites/LevelObjects/spr_water", TickTick.Depth_LevelObjects)
    {
        SetOriginToCenter();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + LocalPosition.X;
        bounce = (float)Math.Sin(t) * 0.2f;
        localPosition.Y += bounce;
    }
}