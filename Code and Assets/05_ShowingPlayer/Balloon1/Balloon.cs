using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

class Balloon : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D balloon, background;
    Vector2 balloonPosition;

    [STAThread]
    static void Main()
    {
        Balloon game = new Balloon();
        game.Run();
    }

    public Balloon()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        balloon = Content.Load<Texture2D>("spr_lives");
        background = Content.Load<Texture2D>("spr_background");
    }

    protected override void Update(GameTime gameTime)
    {
        MouseState currentMouseState = Mouse.GetState();
        balloonPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.Draw(balloon, balloonPosition, Color.White);
        spriteBatch.End();
    }
}