using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class Painter : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;    
    InputHelper inputHelper;

    static GameWorld gameWorld;
    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }

    public static Vector2 ScreenSize { get; private set; }

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
        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); 
        gameWorld = new GameWorld(Content);        
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }
    
}