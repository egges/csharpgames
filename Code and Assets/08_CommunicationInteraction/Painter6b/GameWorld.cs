using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class GameWorld
{
    Texture2D background;
    Ball ball;
    PaintCan can1, can2, can3;
    Cannon cannon;

    public GameWorld(ContentManager Content)
    {
        background = Content.Load<Texture2D>("spr_background");
        cannon = new Cannon(Content);
        ball = new Ball(Content);
        can1 = new PaintCan(Content, 480.0f, Color.Red);
        can2 = new PaintCan(Content, 610.0f, Color.Green);
        can3 = new PaintCan(Content, 740.0f, Color.Blue);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        cannon.HandleInput(inputHelper);
        ball.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        ball.Update(gameTime);
        can1.Update(gameTime);
        can2.Update(gameTime);
        can3.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        ball.Draw(gameTime, spriteBatch);
        cannon.Draw(gameTime, spriteBatch);
        can1.Draw(gameTime, spriteBatch);
        can2.Draw(gameTime, spriteBatch);
        can3.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public Cannon Cannon
    {
        get { return cannon; }
    }

    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
}
