using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// Represents a grid of jewel objects.
/// </summary>
class JewelGrid : GameObject
{
    Jewel[,] grid;

    int cellSize;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public JewelGrid(int width, int height, int cellSize)
    {
        // copy the width, height, and cell size
        Width = width;
        Height = height;
        this.cellSize = cellSize;

        Reset();
    }

    public override void Reset()
    {
        // initialize the grid
        grid = new Jewel[Width, Height];

        // fill the grid with random jewels
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
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
        for (int y = Height - 1; y > 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                grid[x, y] = grid[x, y - 1];
                grid[x, y].LocalPosition = GetCellPosition(x, y);
            }
        }

        // refill the top row
        for (int x = 0; x < Width; x++)
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
    public Vector2 GetCellPosition(int x, int y)
    {
        return new Vector2(x * cellSize, y * cellSize);
    }

    public void ShiftRowRight(int selectedRow)
    {
        // store the rightmost jewel as a backup
        Jewel last = grid[Width - 1, selectedRow];

        // replace all jewels by their left neighbor
        for (int x = Width - 1; x > 0; x--)
        {
            grid[x, selectedRow] = grid[x - 1, selectedRow];
            grid[x, selectedRow].LocalPosition = GetCellPosition(x, selectedRow);
        }

        // re-insert the old rightmost jewel on the left
        grid[0, selectedRow] = last;
        last.LocalPosition = GetCellPosition(0, selectedRow);
    }

    public void ShiftRowLeft(int selectedRow)
    {
        // store the leftmost jewel as a backup
        Jewel first = grid[0, selectedRow];

        // replace all jewels by their right neighbor
        for (int x = 0; x < Width - 1; x++)
        {
            grid[x, selectedRow] = grid[x + 1, selectedRow];
            grid[x, selectedRow].LocalPosition = GetCellPosition(x, selectedRow);
        }

        // re-insert the old leftmost jewel on the right 
        grid[Width - 1, selectedRow] = first;
        grid[Width - 1, selectedRow].LocalPosition = GetCellPosition(Width - 1, selectedRow);
    }
}
