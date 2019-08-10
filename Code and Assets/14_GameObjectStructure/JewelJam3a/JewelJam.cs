using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class JewelJam : ExtendedGame
{
    // the background sprite
    Texture2D background;
    
    // the different sprites for jewels
    Texture2D[] jewels;

    /// <summary>
    /// The grid of jewels, where each distinct integer value stands for a different jewel.
    /// </summary>
    int[,] grid;

    /// <summary>
    /// The width of the grid: the number of cells in the horizontal direction.
    /// </summary>
    const int GridWidth = 5;

    /// <summary>
    /// The height of the grid: the number of cells in the vertical direction.
    /// </summary>
    const int GridHeight = 10;

    /// <summary>
    /// The horizontal and distance between two adjacent grid cells.
    /// </summary>
    const int CellSize = 85;

    /// <summary>
    /// The position of the top-left corner of the grid in the game world.
    /// </summary>
    Vector2 GridOffset = new Vector2(85, 150);

    [STAThread]
    static void Main()
    {
        JewelJam game = new JewelJam();
        game.Run();
    }

    public JewelJam()
    {
        IsMouseVisible = true;

        // initialize the grid with random jewels
        grid = new int[GridWidth, GridHeight];
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                grid[x, y] = Random.Next(3);
            }
        }
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        // load the background sprite
        background = Content.Load<Texture2D>("spr_background");

        // load the jewel sprites
        jewels = new Texture2D[3];
        jewels[0] = Content.Load<Texture2D>("spr_single_jewel1");
        jewels[1] = Content.Load<Texture2D>("spr_single_jewel2");
        jewels[2] = Content.Load<Texture2D>("spr_single_jewel3");

        // calculate the world size, and recalculate how the world should be scaled
        worldSize = new Point(background.Width, background.Height);
        FullScreen = false;
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        
        // when the player presses the spacebar, move all jewels one row down
        if (inputHelper.KeyPressed(Keys.Space))
            MoveRowsDown();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // start drawing sprites, applying the scaling matrix
        spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScale);

        // draw the background sprite
        spriteBatch.Draw(background, Vector2.Zero, Color.White);

        // draw all jewels: one per grid cell
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                // compute the world position for this grid cell
                Vector2 position = GridOffset + new Vector2(x,y) * CellSize;

                // the grid stores a number; draw the sprite associated to that number
                int jewelIndex = grid[x, y];
                spriteBatch.Draw(jewels[jewelIndex], position, Color.White);
            }
        }
        spriteBatch.End();
    }

    /// <summary>
    /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
    /// </summary>
    void MoveRowsDown()
    {
        for (int y = GridHeight - 1; y > 0; y--)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                grid[x, y] = grid[x, y - 1];
            }
        }

        // refill the top row
        for (int x = 0; x < GridWidth; x++)
        {
            grid[x, 0] = Random.Next(3);
        }
    }
    
}