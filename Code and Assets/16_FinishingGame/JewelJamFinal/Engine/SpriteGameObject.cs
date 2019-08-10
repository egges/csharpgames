using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A game object with a sprite.
/// </summary>
class SpriteGameObject : GameObject
{
    /// <summary>
    /// The sprite that this object can draw on the screen.
    /// </summary>
    protected Texture2D sprite;

    /// <summary>
    /// The origin ('offset') to use when drawing the sprite on the screen.
    /// </summary>
    protected Vector2 origin;

    /// <summary>
    /// Creates a new SpriteGameObject with a given sprite name.
    /// </summary>
    /// <param name="spriteName">The name of the sprite to load.</param>
    public SpriteGameObject(string spriteName)
    {
        sprite = ExtendedGame.AssetManager.LoadSprite(spriteName);
        origin = Vector2.Zero;
    }

    /// <summary>
    /// Draws this SpriteGameObject on the screen, using its global position and origin. 
    /// Note that the object will only get drawn if it's actually marked as visible.
    /// </summary>
    /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Visible)
        {
            // draw the sprite at its *global* position in the game world
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White,
                0, origin, 1.0f, SpriteEffects.None, 0);
        }
    }

    /// <summary>
    /// Gets the width of this object in the game world, according to its sprite.
    /// </summary>
    public int Width { get { return sprite.Width; } }

    /// <summary>
    /// Gets the height of this object in the game world, according to its sprite.
    /// </summary>
    public int Height { get { return sprite.Height; } }

    /// <summary>
    /// Updates this object's origin so that it lies in the center of the sprite.
    /// </summary>
    public void SetOriginToCenter()
    {
        origin = new Vector2(Width / 2.0f, Height / 2.0f);
    }

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
            spriteBounds.Offset(GlobalPosition - origin);
            return spriteBounds;
        }
    }
}