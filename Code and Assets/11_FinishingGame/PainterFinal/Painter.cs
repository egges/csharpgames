using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

/// <summary>
/// The overall game class of Painter.
/// </summary>
class Painter : Game
{
    // MonoGame helper objects for managing graphics and sprites
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    // our own helper object for managing keyboard and mouse input
    InputHelper inputHelper;

    // static references to objects that should be accessible by all classes
    static GameWorld gameWorld;

    /// <summary>
    /// Gets a reference to the game world.
    /// </summary>
    public static GameWorld GameWorld
    {
        get { return gameWorld; }
    }

    /// <summary>
    /// The width and height of the screen, wrapped inside a Vector2 object.
    /// </summary>
    public static Vector2 ScreenSize { get; private set; }

    /// <summary>
    /// The game's random number generator.
    /// </summary>
    public static Random Random { get; private set; }

    [STAThread]
    static void Main()
    {
        Painter game = new Painter();
        game.Run();
    }

    /// <summary>
    /// Initializes this Painter instance.
    /// </summary>
    Painter()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;

        // create the input helper and random number generator
        inputHelper = new InputHelper();
        Random = new Random();
    }

    /// <summary>
    /// Called by MonoGame when the GraphicsDevice has been prepared.
    /// This method is the first place where you can load assets and retrieve things such as the screen size.
    /// In this method, Painter initializes the game world.
    /// </summary>
    protected override void LoadContent()
    {
        // prepare the sprite batch that will be used in each call to the Draw method
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // compute the size of the screen
        ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        // create the game world! This will indirectly load all other relevant assets.
        gameWorld = new GameWorld(Content);

        // start playing the background music
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(Content.Load<Song>("snd_music"));
    }

    /// <summary>
    /// Called by MonoGame in each frame of the game loop, before Draw.
    /// This method contains the update logic of the game.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    protected override void Update(GameTime gameTime)
    {
        // update the input helper
        inputHelper.Update();

        // update the game world: first HandleInput, then Draw
        gameWorld.HandleInput(inputHelper);
        gameWorld.Update(gameTime);
    }

    /// <summary>
    /// Called by MonoGame in each frame of the game loop, after Update.
    /// This method should draw all game objects on the screen.
    /// </summary>
    /// <param name="gameTime">An object that contains information about the game time that has passed.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        gameWorld.Draw(gameTime, spriteBatch);
    }
}