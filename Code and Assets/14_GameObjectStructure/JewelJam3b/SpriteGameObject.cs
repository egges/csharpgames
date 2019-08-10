using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteGameObject : GameObject
{
    protected Texture2D sprite;
    protected Vector2 origin;

    public SpriteGameObject(string spriteName)
    {
        sprite = ExtendedGame.ContentManager.Load<Texture2D>(spriteName);
        origin = Vector2.Zero;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Visible)
        {
            spriteBatch.Draw(sprite, Position, null, Color.White,
                0, origin, 1.0f, SpriteEffects.None, 0);
        }
    }

    public int Width { get { return sprite.Width; } }

    public int Height { get { return sprite.Height; } }

    /// <summary>
    /// Gets a Rectangle that describes this game object's current bounding box.
    /// This is useful for collision detection.
    /// </summary>
    public Rectangle BoundingBox
    {
        get
        {
            // get the sprite's bounds
            Rectangle spriteBounds = sprite.Bounds;
            // add the object's position to it as an offset
            spriteBounds.Offset(Position - origin);
            return spriteBounds;
        }
    }
}