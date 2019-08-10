using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class ThreeColorGameObject
{
    protected Texture2D colorRed, colorGreen, colorBlue;
    Color color;
    protected Vector2 position, origin, velocity;
    protected float rotation;

    protected ThreeColorGameObject(ContentManager content, string redSprite, string greenSprite, string blueSprite)
    {
        // load the three sprites
        colorRed = content.Load<Texture2D>(redSprite);
        colorGreen = content.Load<Texture2D>(greenSprite);
        colorBlue = content.Load<Texture2D>(blueSprite);

        // default origin: center of a sprite
        origin = new Vector2(colorRed.Width / 2.0f, colorRed.Height / 2.0f);
        
        // initialize other things
        position = Vector2.Zero;
        velocity = Vector2.Zero;
        rotation = 0;

        Reset();
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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
            rotation, origin, 1.0f, SpriteEffects.None, 0);
    }

    public virtual void Reset()
    {
        Color = Color.Blue;
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