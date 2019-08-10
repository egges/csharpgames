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

    // a sprite to draw at the mouse position, as an example of using ScreenToWorld
    Texture2D cursorSprite;

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

        // calculate and set the viewport to use
        GraphicsDevice.Viewport = CalculateViewport(screenSize);

        // calculate how the graphics should be scaled, so that the game world fits inside the window
        spriteScale = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / worldSize.X, (float)GraphicsDevice.Viewport.Height / worldSize.Y, 1);
    }

    /// <summary>
    /// Calculates and returns the viewport to use, so that the game world fits on the screen while preserving its aspect ratio.
    /// </summary>
    /// <param name="windowSize">The size of the screen on which the world should be drawn.</param>
    /// <returns>A Viewport object that will show the game world as large as possible while preserving its aspect ratio.</returns>
    Viewport CalculateViewport(Point windowSize)
    {
        // create a Viewport object
        Viewport viewport = new Viewport();
        
        // calculate the two aspect ratios
        float gameAspectRatio = (float)worldSize.X / worldSize.Y;
        float windowAspectRatio = (float)windowSize.X / windowSize.Y;

        // if the window is relatively wide, use the full window height
        if (windowAspectRatio > gameAspectRatio)
        {
            viewport.Width = (int)(windowSize.Y * gameAspectRatio);
            viewport.Height = windowSize.Y;
        }
        // if the window is relatively high, use the full window width
        else
        {
            viewport.Width = windowSize.X;
            viewport.Height = (int)(windowSize.X / gameAspectRatio);
        }

        // calculate and store the top-left corner of the viewport
        viewport.X = (windowSize.X - viewport.Width) / 2;
        viewport.Y = (windowSize.Y - viewport.Height) / 2;

        return viewport;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // load the background sprite
        background = Content.Load<Texture2D>("spr_background");
        // load the cursor sprite
        cursorSprite = Content.Load<Texture2D>("spr_single_jewel1");

        // set the world size to the width and height of that sprite
        worldSize = new Point(background.Width, background.Height);

        // set the window size, and calculate how the game world should be scaled
        windowSize = new Point(500, 800);
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

        // draw a sprite at the mouse position
        spriteBatch.Draw(cursorSprite, ScreenToWorld(inputHelper.MousePosition), Color.White);

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

    /// <summary>
    /// Converts a position in screen coordinates to a position in world coordinates.
    /// </summary>
    /// <param name="screenPosition">A position in screen coordinates.</param>
    /// <returns>The corresponding position in world coordinates.</returns>
    Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        Vector2 viewportTopLeft = new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);
        float screenToWorldScale = worldSize.X / (float)GraphicsDevice.Viewport.Width;
        return (screenPosition - viewportTopLeft) * screenToWorldScale;
    }

}