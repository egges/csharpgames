using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class PaintCan
{
    Texture2D colorRed, colorGreen, colorBlue;
    Vector2 position, origin, velocity;
    Color color;

    public PaintCan(ContentManager Content)
    {
        colorRed = Content.Load<Texture2D>("spr_can_red");
        colorGreen = Content.Load<Texture2D>("spr_can_green");
        colorBlue = Content.Load<Texture2D>("spr_can_blue");
        origin = new Vector2(colorRed.Width, colorRed.Height) / 2;
        Reset();
    }

    public void Update(GameTime gameTime)
    {
        // TODO: We'll fill in this method soon.
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // same as Ball
    }

    public void Reset()
    {
        color = Color.Blue;
        velocity = Vector2.Zero;
    }
	
    // Position and Color properties: same as Ball
}

