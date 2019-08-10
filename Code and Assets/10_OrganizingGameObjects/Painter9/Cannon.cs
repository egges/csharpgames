using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Cannon : ThreeColorGameObject
{
    Texture2D cannonBarrel;
    Vector2 barrelOrigin;
    float barrelRotation;

    public Cannon(ContentManager Content) : base(Content, "spr_cannon_red", "spr_cannon_green", "spr_cannon_blue")
    {
        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
        position = new Vector2(72, 405);
        barrelOrigin = new Vector2(cannonBarrel.Height / 2, cannonBarrel.Height / 2);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // change the color when the player presses R/G/B
        if (inputHelper.KeyPressed(Keys.R))
        {
            Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            Color = Color.Blue;
        }

        // change the angle depending on the mouse position
        double opposite = inputHelper.MousePosition.Y - position.Y;
        double adjacent = inputHelper.MousePosition.X - position.X;
        barrelRotation = (float)Math.Atan2(opposite, adjacent);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(cannonBarrel, position, null, Color.White, barrelRotation, barrelOrigin, 1.0f, SpriteEffects.None, 0);
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        base.Reset();
        barrelRotation = 0f;
    }

    public Vector2 BallPosition
    {
        get
        {
            float opposite = (float)Math.Sin(barrelRotation) * cannonBarrel.Width * 0.75f;
            float adjacent = (float)Math.Cos(barrelRotation) * cannonBarrel.Width * 0.75f;
            return position + new Vector2(adjacent, opposite);
        }
    }
}