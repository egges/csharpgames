using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class DiscoWorld : Game
{
    GraphicsDeviceManager graphics;
    Color background;

    [STAThread]
    static void Main()
    {
        DiscoWorld game = new DiscoWorld();
        game.Run();
    }

    public DiscoWorld()
    {
        graphics = new GraphicsDeviceManager(this);
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
        int redComponent = gameTime.TotalGameTime.Milliseconds / 4;
        background = new Color(redComponent, 0, 0);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(background);
    }
}