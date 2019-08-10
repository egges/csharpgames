using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class PaintCan
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 position, origin, velocity;
    Color color, targetcolor;

    public PaintCan(ContentManager Content, float positionOffset, Color target)
    {
        colorRed = Content.Load<Texture2D>("spr_can_red");
        colorGreen = Content.Load<Texture2D>("spr_can_green");
        colorBlue = Content.Load<Texture2D>("spr_can_blue");
        origin = new Vector2(colorRed.Width, colorRed.Height) / 2;

        targetcolor = target;
        position = new Vector2(positionOffset, 100);

        Reset();
    }

    public void Update(GameTime gameTime)
    {
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
        color = Color.Blue;
        velocity = Vector2.Zero;
    }

    public Vector2 Position
    {
        get { return position; }
    }

    public Color Color
    {
        get { return color; }
        set
        {
            if (value != Color.Red && value != Color.Green && value != Color.Blue)
            {
                return;
            }
            color = value;
        }
    }
}

