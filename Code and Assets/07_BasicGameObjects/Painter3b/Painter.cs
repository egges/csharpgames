using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class Painter : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D background;
    Cannon cannon;

    InputHelper inputHelper;

    [STAThread]
    static void Main()
    {
        Painter game = new Painter();
        game.Run();
    }

    public Painter()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        inputHelper = new InputHelper();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        background = Content.Load<Texture2D>("spr_background");
        
        cannon = new Cannon(Content);
    }

    public void HandleInput()
    {
        if (inputHelper.KeyPressed(Keys.R))
        {
            cannon.Color = Color.Red;
        }
        else if (inputHelper.KeyPressed(Keys.G))
        {
            cannon.Color = Color.Green;
        }
        else if (inputHelper.KeyPressed(Keys.B))
        {
            cannon.Color = Color.Blue;
        }

        double opposite = inputHelper.MousePosition.Y - cannon.Position.Y;
        double adjacent = inputHelper.MousePosition.X - cannon.Position.X;
        cannon.Angle = (float)Math.Atan2(opposite, adjacent);
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        HandleInput();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        cannon.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }
}