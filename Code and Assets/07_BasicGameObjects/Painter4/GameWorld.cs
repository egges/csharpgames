using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class GameWorld
{
    Texture2D background;
    Cannon cannon;

    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        cannon = new Cannon(Content);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        cannon.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        cannon.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}
