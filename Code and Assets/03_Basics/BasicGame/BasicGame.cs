using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class BasicGame : Game
{
    GraphicsDeviceManager graphics;

    [STAThread]
    static void Main()
    {
        BasicGame game = new BasicGame();
        game.Run();
    }

    public BasicGame()
    {
        graphics = new GraphicsDeviceManager(this);
    }

    protected override void LoadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Olive);
    }
}