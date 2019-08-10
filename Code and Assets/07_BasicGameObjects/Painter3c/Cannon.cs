using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class Cannon
{
    Texture2D cannonBarrel, colorRed, colorGreen, colorBlue;
    Vector2 barrelPosition, barrelOrigin, colorOrigin;
    Color currentColor;
    float angle;

    public Cannon(ContentManager Content)
    {
        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
        colorRed = Content.Load<Texture2D>("spr_cannon_red");
        colorGreen = Content.Load<Texture2D>("spr_cannon_green");
        colorBlue = Content.Load<Texture2D>("spr_cannon_blue");

        currentColor = Color.Blue;

        barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
        colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
        barrelPosition = new Vector2(72, 405);
    }

    public void HandleInput(InputHelper inputHelper)
    {
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

        double opposite = inputHelper.MousePosition.Y - barrelPosition.Y;
        double adjacent = inputHelper.MousePosition.X - barrelPosition.X;
        Angle = (float)Math.Atan2(opposite, adjacent);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1f, SpriteEffects.None, 0);

        // determine the sprite based on the current color
        Texture2D currentSprite;
        if (currentColor == Color.Red)
            currentSprite = colorRed;
        else if (currentColor == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        // draw that sprite
        spriteBatch.Draw(currentSprite, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
    }

    public void Reset()
    {
        currentColor = Color.Blue;
        angle = 0.0f;
    }

    public Vector2 Position
    {
        get { return barrelPosition; }
    }

    float Angle
    {
        get { return angle; }
        set { angle = value; }
    }

    public Color Color
    {
        get { return currentColor; }
        private set
        {
            // only allow the colors red, green, and blue
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            currentColor = value;
        }
    }
}
