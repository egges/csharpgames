using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class GameObject
{
    public Vector2 LocalPosition { get; set; }
    protected Vector2 velocity;

    public bool Visible { get; set; }

    public GameObject Parent { get; set; }

    public GameObject()
    {
        LocalPosition = Vector2.Zero;
        velocity = Vector2.Zero;
        Visible = true;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        LocalPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
        velocity = Vector2.Zero;
    }

    /// <summary>
    /// Gets this object's global position in the game world, by adding its local position to the global position of its parent.
    /// </summary>
    public Vector2 GlobalPosition
    {
        get
        {
            if (Parent == null)
                return LocalPosition;
            return LocalPosition + Parent.GlobalPosition;
        }
    }

}