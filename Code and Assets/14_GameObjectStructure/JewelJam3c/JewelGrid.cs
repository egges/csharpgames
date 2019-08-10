using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Represents a grid of jewel objects.
/// </summary>
class JewelGrid : GameObject
{
    Jewel[,] grid;

    int gridWidth, gridHeight, cellSize;

    public JewelGrid(int width, int height, int cellSize, Vector2 offset)
    {
        // copy the width, height, and cell size
        gridWidth = width;
        gridHeight = height;
        this.cellSize = cellSize;

        // copy the position
        LocalPosition = offset;

        Reset();
    }

    public override void Reset()
    {
        // initialize the grid
        grid = new Jewel[gridWidth, gridHeight];

        // fill the grid with random jewels
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // add a new jewel to the grid
                grid[x, y] = new Jewel(ExtendedGame.Random.Next(3));
                // set the parent and position of that jewel
                grid[x, y].Parent = this;
                grid[x, y].LocalPosition = GetCellPosition(x, y);
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // when the player presses the spacebar, move all jewels one row down
        if (inputHelper.KeyPressed(Keys.Space))
            MoveRowsDown();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Jewel jewel in grid)
            jewel.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
    /// </summary>
    void MoveRowsDown()
    {
        // shift all rows down
        for (int y = gridHeight - 1; y > 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                grid[x, y] = grid[x, y - 1];
                grid[x, y].LocalPosition = GetCellPosition(x, y);
            }
        }

        // refill the top row
        for (int x = 0; x < gridWidth; x++)
        {
            grid[x, 0] = new Jewel(ExtendedGame.Random.Next(3));
            grid[x, 0].Parent = this;
            grid[x, 0].LocalPosition = GetCellPosition(x, 0);
        }
    }

    /// <summary>
    /// Converts cell coordinates to a position in the game world, relative to the grid's own position.
    /// </summary>
    /// <param name="x">The x-coordinate of a grid cell.</param>
    /// <param name="y">The y-coordinate of a grid cell.</param>
    /// <returns>A Vector2 instance that represents the game world position of the given grid cell.</returns>
    Vector2 GetCellPosition(int x, int y)
    {
        return new Vector2(x * cellSize, y * cellSize);
    }
}
