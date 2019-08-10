using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameWorld
{
    Texture2D background, gameover;
    Ball ball;
    PaintCan can1, can2, can3;
    Cannon cannon;
    int lives;
    Texture2D livesSprite;

    public GameWorld(ContentManager Content)
    {
        gameover = Content.Load<Texture2D>("spr_gameover");
        livesSprite = Content.Load<Texture2D>("spr_lives");
        background = Content.Load<Texture2D>("spr_background");
        cannon = new Cannon(Content);
        ball = new Ball(Content);
        can1 = new PaintCan(Content, 480.0f, Color.Red);
        can2 = new PaintCan(Content, 610.0f, Color.Green);
        can3 = new PaintCan(Content, 740.0f, Color.Blue);
        lives = 5;
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if (!IsGameOver)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }
        else if (inputHelper.KeyPressed(Keys.Space))
        {
            Reset();
        }
    }

    public void Update(GameTime gameTime)
    {
        if (IsGameOver)
            return;

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
        for (int i = 0; i < lives; i++)
        {
            spriteBatch.Draw(livesSprite, new Vector2(i * livesSprite.Width + 15, 20), Color.White);
        }

        if (IsGameOver)
        {
            spriteBatch.Draw(gameover, new Vector2(Painter.ScreenSize.X - gameover.Width, Painter.ScreenSize.Y - gameover.Height) / 2, Color.White);
        }

        spriteBatch.End();
    }

    void Reset()
    {
        lives = 5;
        cannon.Reset();
        ball.Reset();
        can1.Reset();
        can2.Reset();
        can3.Reset();
        can1.ResetMinSpeed();
        can2.ResetMinSpeed();
        can3.ResetMinSpeed();
    }

    public Ball Ball
    {
        get { return ball; }
    }

    public Cannon Cannon
    {
        get { return cannon; }
    }

    public void LoseLife()
    {
        lives--;
    }

    bool IsGameOver
    {
        get { return lives <= 0; }
    }

    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
}
