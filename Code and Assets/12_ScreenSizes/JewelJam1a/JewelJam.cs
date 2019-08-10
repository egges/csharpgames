using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class JewelJam : Game
{
    // standard MonoGame objects for graphics and sprites
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

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
    protected Texture2D background;

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
    }

    /// <summary>
    /// Scales the window to the desired size, and calculates how the game world should be scaled to fit inside that window.
    /// </summary>
    void ApplyResolutionSettings()
    {
        // scale the window to the desired size
        graphics.PreferredBackBufferWidth = windowSize.X;
        graphics.PreferredBackBufferHeight = windowSize.Y;
        graphics.ApplyChanges();

        // calculate how the graphics should be scaled, so that the game world fits inside the window
        spriteScale = Matrix.CreateScale((float)windowSize.X / worldSize.X, (float)windowSize.Y / worldSize.Y, 1);
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
        ApplyResolutionSettings();
    }

    protected override void Update(GameTime gameTime)
    {
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

}