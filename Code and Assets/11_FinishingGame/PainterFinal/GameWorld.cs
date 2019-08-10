using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Represents the entire game world. 
/// The overall game should have a single instance of this class.
/// </summary>
class GameWorld
{
    // assets: sprites and fonts
    Texture2D background, gameover, livesSprite, scoreBar;
    SpriteFont gameFont;

    // game objects: ball, paint cans, and cannon.
    Ball ball;
    PaintCan can1, can2, can3;
    Cannon cannon;

    /// <summary>
    /// The current number of lives that the player has.
    /// </summary>
    int lives;

    /// <summary>
    /// The current score of the player.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Creates a new GameWorld instance. 
    /// This method loads all relevant MonoGame assets and initializes all game objects: 
    /// the cannon, the ball, and the paint cans. 
    /// It also initializes all other variables so that the game can start.
    /// </summary>
    /// <param name="Content">A ContentManager object, required for loading assets.</param>
    public GameWorld(ContentManager Content)
    {
        // load sprites and fonts
        gameover = Content.Load<Texture2D>("spr_gameover");
        livesSprite = Content.Load<Texture2D>("spr_lives");
        background = Content.Load<Texture2D>("spr_background");
        scoreBar = Content.Load<Texture2D>("spr_scorebar");
        gameFont = Content.Load<SpriteFont>("PainterFont");

        // initialize game objects: cannon, ball, and paint cans
        cannon = new Cannon(Content);
        ball = new Ball(Content);
        can1 = new PaintCan(Content, 480.0f, Color.Red);
        can2 = new PaintCan(Content, 610.0f, Color.Green);
        can3 = new PaintCan(Content, 740.0f, Color.Blue);

        // initialize the score and the number of lives
        Score = 0;
        lives = 5;
    }

    /// <summary>
    /// Performs input handling for the entire game world.
    /// </summary>
    /// <param name="inputHelper">An object that contains information about the mouse and keyboard state.</param>
    public void HandleInput(InputHelper inputHelper)
    {
        // the cannon and the ball both do input handling
        if (!IsGameOver)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }

        // in the "game over" state, pressing the spacebar will reset the game
        else if (inputHelper.KeyPressed(Keys.Space))
        {
            Reset();
        }
    }

    /// <summary>
    /// Updates all game objects for one frame of the game loop.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    public void Update(GameTime gameTime)
    {
        // in the "game over" state, don't update any objects
        if (IsGameOver)
            return;

        ball.Update(gameTime);
        can1.Update(gameTime);
        can2.Update(gameTime);
        can3.Update(gameTime);
    }

    /// <summary>
    /// Draws the game world in its current state.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    /// <param name="spriteBatch">The sprite batch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();

        // draw the background and score bar
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.Draw(scoreBar, new Vector2(10, 10), Color.White);

        // draw all game objects
        ball.Draw(gameTime, spriteBatch);
        cannon.Draw(gameTime, spriteBatch);
        can1.Draw(gameTime, spriteBatch);
        can2.Draw(gameTime, spriteBatch);
        can3.Draw(gameTime, spriteBatch);

        // draw the score
        spriteBatch.DrawString(gameFont, "Score: " + Score, new Vector2(20, 18), Color.White);

        // draw the number of lives
        for (int i = 0; i < lives; i++)
            spriteBatch.Draw(livesSprite, new Vector2(i * livesSprite.Width + 15, 60), Color.White);

        // if the game is over, draw the game-over sprite
        if (lives <= 0)
            spriteBatch.Draw(gameover, new Vector2(Painter.ScreenSize.X - gameover.Width, Painter.ScreenSize.Y - gameover.Height) / 2, Color.White);

        spriteBatch.End();
    }

    /// <summary>
    /// Resets the game world to its initial state, so that a new game is ready to begin.
    /// </summary>
    public void Reset()
    {
        lives = 5;
        Score = 0;

        // reset all game objects
        cannon.Reset();
        ball.Reset();
        can1.Reset();
        can2.Reset();
        can3.Reset();
        can1.ResetMinSpeed();
        can2.ResetMinSpeed();
        can3.ResetMinSpeed();
    }

    /// <summary>
    /// Gets the game world's Ball object.
    /// </summary>
    public Ball Ball
    {
        get { return ball; }
    }

    /// <summary>
    /// Gets the game world's Cannon object.
    /// </summary>
    public Cannon Cannon
    {
        get { return cannon; }
    }

    /// <summary>
    /// Decreases the player's number of lives by one.
    /// </summary>
    public void LoseLife()
    {
        lives--;
    }

    /// <summary>
    /// Checks and returns whether or not the game is over. 
    /// Returns true if the player has no more lives left, and false otherwise.
    /// </summary>
    bool IsGameOver
    {
        get { return lives <= 0; }
    }

    /// <summary>
    /// Checks and returns whether a given position lies outside the screen.
    /// </summary>
    /// <param name="position">A position in the game world.</param>
    /// <returns>true if the given position lies outside the screen, and false otherwise.</returns>
    public bool IsOutsideWorld(Vector2 position)
    {
        return position.X < 0 || position.X > Painter.ScreenSize.X || position.Y > Painter.ScreenSize.Y;
    }
    
}
