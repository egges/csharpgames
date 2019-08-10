using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class FlyingSprites : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D balloon, background;
    Vector2 balloonPosition;

    [STAThread]
    static void Main()
    {
        FlyingSprites game = new FlyingSprites();
        game.Run();
    }

    public FlyingSprites()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        balloon = Content.Load<Texture2D>("spr_lives");
        background = Content.Load<Texture2D>("spr_background");
    }

    protected override void Update(GameTime gameTime)
    {
        int yPosition = 480 - gameTime.TotalGameTime.Milliseconds / 2;
        balloonPosition = new Vector2(300, yPosition);
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