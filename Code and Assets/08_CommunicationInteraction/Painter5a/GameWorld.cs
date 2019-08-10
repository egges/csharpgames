using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class GameWorld
{
    Texture2D background;
    Ball ball;
    Cannon cannon;

    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        cannon = new Cannon(Content);
        ball = new Ball(Content);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        cannon.HandleInput(inputHelper);
        ball.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        ball.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        ball.Draw(gameTime, spriteBatch);
        cannon.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public Cannon Cannon
    {
        get { return cannon; }
    }
}
