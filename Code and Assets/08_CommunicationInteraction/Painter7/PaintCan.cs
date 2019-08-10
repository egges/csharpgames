using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class PaintCan
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 position, origin, velocity;
    Color color, targetcolor;
    float minSpeed;

    public PaintCan(ContentManager Content, float positionOffset, Color target)
    {
        colorRed = Content.Load<Texture2D>("spr_can_red");
        colorGreen = Content.Load<Texture2D>("spr_can_green");
        colorBlue = Content.Load<Texture2D>("spr_can_blue");
        origin = new Vector2(colorRed.Width, colorRed.Height) / 2;

        targetcolor = target;
        minSpeed = 30;
        position = new Vector2(positionOffset, -origin.Y);

        Reset();
    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        minSpeed += 0.01f * dt;

        if (velocity != Vector2.Zero)
        {
            position += velocity * dt;

            // check if this paint can collides with the ball
            if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
            {
                Color = Painter.GameWorld.Ball.Color;
                Painter.GameWorld.Ball.Reset();
            }

            // reset the can if it leaves the screen
            if (Painter.GameWorld.IsOutsideWorld(position - origin))
            {
                Reset();
            }
        }

        // if the can is not moving, (re)spawn it with a certain probability
        else if (Painter.Random.NextDouble() < 0.01)
        {
            velocity = CalculateRandomVelocity();
            color = CalculateRandomColor();
        }
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
        position.Y = -origin.Y;
        velocity = Vector2.Zero;
    }

    Vector2 CalculateRandomVelocity()
    {
        return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
    }

    Color CalculateRandomColor()
    {
        int randomval = Painter.Random.Next(3);
        if (randomval == 0)
            return Color.Red;
        else if (randomval == 1)
            return Color.Green;
        else
            return Color.Blue;
    }

    public Vector2 Position
    {
        get { return position; }
    }

    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = colorRed.Bounds;
            spriteBounds.Offset(position - origin);
            return spriteBounds;
        }
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