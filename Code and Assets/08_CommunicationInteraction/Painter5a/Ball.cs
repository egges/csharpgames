using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class Ball
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 position, origin;
    Color color;

    public Ball(ContentManager Content)
    {
        colorRed = Content.Load<Texture2D>("spr_ball_red");
        colorGreen = Content.Load<Texture2D>("spr_ball_green");
        colorBlue = Content.Load<Texture2D>("spr_ball_blue");
        origin = new Vector2(colorRed.Width / 2.0f, colorRed.Height / 2.0f);
        Reset();
    }

    public void HandleInput(InputHelper inputHelper)
    {
    }

    public void Update(GameTime gameTime)
    {
        Color = Painter.GameWorld.Cannon.Color;
        position = Painter.GameWorld.Cannon.BallPosition;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // determine the sprite based on the current color
        Texture2D currentSprite;
        if (Color == Color.Red)
            currentSprite = colorRed;
        else if (Color == Color.Green)
            currentSprite = colorGreen;
        else
            currentSprite = colorBlue;

        // draw that sprite
        spriteBatch.Draw(currentSprite, position, null, Color.White, 
            0f, origin, 1.0f, SpriteEffects.None, 0);
    }

    public void Reset()
    {
        position = new Vector2(65, 390);
        Color = Color.Blue;
    }

    public Vector2 Position
    {
        get { return position; }
    }

    public Color Color
    {
        get { return color; }
        private set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            color = value;
        }
    }
}

