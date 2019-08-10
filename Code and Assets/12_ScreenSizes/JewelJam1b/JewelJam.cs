using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class JewelJam : Game
{
    // standard MonoGame objects for graphics and sprites
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    InputHelper inputHelper;

    /// <summary>
    /// The width and height of the game world, in game units.
    /// </summary>
    Point worldSize;

    /// <summary>
    /// The width and height of the window, in pixels.
    /// </summary>
    Point windowSize;

    /// <summary>
    /// A matrix used for scaling the game world so that it fits inside the window.
    /// </summary>
    Matrix spriteScale;

    // the background sprite
    Texture2D background;

    [STAThread]
    static void Main()
    {
        JewelJam game = new JewelJam();
        game.Run();
    }

    public JewelJam()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;

        inputHelper = new InputHelper();
    }

    /// <summary>
    /// Scales the window to the desired size, and calculates how the game world should be scaled to fit inside that window.
    /// </summary>
    void ApplyResolutionSettings(bool fullScreen)
    {
        // make the game full-screen or not
        graphics.IsFullScreen = fullScreen;
        
        // get the size of the screen to use: either the window size or the full screen size
        Point screenSize;
        if (fullScreen)
            screenSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        else
            screenSize = windowSize;
        
        // scale the window to the desired size
        graphics.PreferredBackBufferWidth = screenSize.X;
        graphics.PreferredBackBufferHeight = screenSize.Y;
        
        graphics.ApplyChanges();

        // calculate how the graphics should be scaled, so that the game world fits inside the window
        spriteScale = Matrix.CreateScale((float)screenSize.X / worldSize.X, (float)screenSize.Y / worldSize.Y, 1);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // load the background sprite
        background = Content.Load<Texture2D>("spr_background");

        // set the world size to the width and height of that sprite
        worldSize = new Point(background.Width, background.Height);

        // set the window size, and calculate how the game world should be scaled
        windowSize = new Point(1024, 768);
        FullScreen = false;
    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update();

        if (inputHelper.KeyPressed(Keys.Escape))
            Exit();

        if (inputHelper.KeyPressed(Keys.F5))
            FullScreen = !FullScreen;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // start drawing sprites, applying the scaling matrix
        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);

        // draw the background sprite
        spriteBatch.Draw(background, Vector2.Zero, Color.White);

        spriteBatch.End();
    }

    /// <summary>
    /// Gets or sets whether the game is running in full-screen mode.
    /// </summary>
    bool FullScreen
    {
        get { return graphics.IsFullScreen; }
        set { ApplyResolutionSettings(value); }
    }

}