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
            for (int y = 0; y < Height; y++)
                AddJewel(x, y, y);
    }

    void AddJewel(int x, int yTarget, int yStart)
    {
        // store the jewel in the grid
        grid[x, yTarget] = new Jewel();

        // set the parent and position of the jewel
        grid[x, yTarget].Parent = this;
        grid[x, yTarget].LocalPosition = GetCellPosition(x, yStart);
        grid[x, yTarget].TargetPosition = GetCellPosition(x, yTarget);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (Jewel jewel in grid)
            jewel.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Jewel jewel in grid)
            jewel.Draw(gameTime, spriteBatch);
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

    public override void HandleInput(InputHelper inputHelper)
    {
        // only respond to spacebar presses
        if (!inputHelper.KeyPressed(Keys.Space))
            return;

        int combo = 0;
        int extraScore = 10;

        int middleColumn = Width / 2;

        // go over the rows from top to bottom; try to find combinations in the middle column
        for (int y = 0; y < Height - 2; y++)
        {
            if (IsValidCombination(grid[middleColumn, y], grid[middleColumn, y + 1], grid[middleColumn, y + 2]))
            {
                // score points
                JewelJam.GameWorld.AddScore(extraScore);
                extraScore *= 2;
                combo++;

                // remove the three jewels, let the jewels above that fall down, and fill the gaps that remain
                RemoveJewel(middleColumn, y, -1);
                RemoveJewel(middleColumn, y + 1, -2);
                RemoveJewel(middleColumn, y + 2, -3);

                // skip 2 extra rows, because those are now empty
                y += 2;
            }
        }

        // play a sound, and check if a combo has been scored
        if (combo == 0)
            ExtendedGame.AssetManager.PlaySoundEffect("snd_error");
        else if (combo == 1)
            ExtendedGame.AssetManager.PlaySoundEffect("snd_single");
        else if (combo == 2)
        {
            JewelJam.GameWorld.DoubleComboScored();
            ExtendedGame.AssetManager.PlaySoundEffect("snd_double");
        }
        else if (combo == 3)
        {
            JewelJam.GameWorld.TripleComboScored();
            ExtendedGame.AssetManager.PlaySoundEffect("snd_triple");
        }
    }

    /// <summary>
    /// Removes the jewel at grid cell (x,y), and then moves other jewels down to fill in the gap that has appeared.
    /// </summary>
    /// <param name="x">The x coordinate of the jewel to remove.</param>
    /// <param name="y">The y coordinate of the jewel to remove.</param>
    void RemoveJewel(int x, int y, int yStartForNewJewel)
    {
        // move the jewels above this cell
        for (int row = y; row > 0; row--)
        {
            grid[x, row] = grid[x, row - 1];
            grid[x, row].TargetPosition = GetCellPosition(x, row);
        }

        // fill the top cell with a new random jewel
        AddJewel(x, 0, yStartForNewJewel);
    }

    bool IsValidCombination(Jewel a, Jewel b, Jewel c)
    {
        // For all three properties (color, shape, and number), 
        // that property should be the same for all jewels *or* different for all jewels.
        return IsConditionValid(a.ColorType, b.ColorType, c.ColorType)
            && IsConditionValid(a.ShapeType, b.ShapeType, c.ShapeType)
            && IsConditionValid(a.NumberType, b.NumberType, c.NumberType);
    }

    bool IsConditionValid(int a, int b, int c)
    {
        // a condition is valid if all three values are equal *or* all three values are different
        return AllEqual(a, b, c) || AllDifferent(a, b, c);
    }

    bool AllEqual(int a, int b, int c)
    {
        return a == b && b == c;
    }

    bool AllDifferent(int a, int b, int c)
    {
        return a != b && b != c && a != c;
    }

    public void ShiftRowRight(int selectedRow)
    {
        // store the rightmost jewel as a backup
        Jewel last = grid[Width - 1, selectedRow];

        // replace all jewels by their left neighbor
        for (int x = Width - 1; x > 0; x--)
        {
            grid[x, selectedRow] = grid[x - 1, selectedRow];
            grid[x, selectedRow].TargetPosition = GetCellPosition(x, selectedRow);
        }

        // re-insert the old rightmost jewel on the left
        grid[0, selectedRow] = last;
        last.LocalPosition = GetCellPosition(-1, selectedRow);
        last.TargetPosition = GetCellPosition(0, selectedRow);
    }

    public void ShiftRowLeft(int selectedRow)
    {
        // store the leftmost jewel as a backup
        Jewel first = grid[0, selectedRow];

        // replace all jewels by their right neighbor
        for (int x = 0; x < Width - 1; x++)
        {
            grid[x, selectedRow] = grid[x + 1, selectedRow];
            grid[x, selectedRow].TargetPosition = GetCellPosition(x, selectedRow);
        }

        // re-insert the old leftmost jewel on the right 
        grid[Width - 1, selectedRow] = first;
        first.LocalPosition = GetCellPosition(Width, selectedRow);
        first.TargetPosition = GetCellPosition(Width - 1, selectedRow);
    }

}