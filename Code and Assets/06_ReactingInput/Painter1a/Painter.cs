using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class Painter : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Texture2D background, cannonBarrel;
    Vector2 barrelPosition, barrelOrigin;
    float angle;

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
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        background = Content.Load<Texture2D>("spr_background");
        cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");

        barrelPosition = new Vector2(72, 405);
        barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;
    }

    protected override void Update(GameTime gameTime)
    {
        MouseState mouse = Mouse.GetState();

        // update the cannon barrel's angle only when the left mouse button is down
        if (mouse.LeftButton == ButtonState.Pressed)
        {
            double opposite = mouse.Y - barrelPosition.Y;
            double adjacent = mouse.X - barrelPosition.X;
            angle = (float)Math.Atan2(opposite, adjacent);
        }       
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        spriteBatch.Draw(background, Vector2.Zero, Color.White);
        spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
        spriteBatch.End();
    }
}