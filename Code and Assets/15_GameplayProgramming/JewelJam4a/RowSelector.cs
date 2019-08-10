using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class RowSelector : SpriteGameObject
{
    /// <summary>
    /// The index of the row that the RowSelector is currently pointing at.
    /// </summary>
    int selectedRow;

    /// <summary>
    /// A reference to the game's grid of jewels.
    /// </summary>
    JewelGrid grid;

    public RowSelector(JewelGrid grid) : base("spr_selector_frame")
    {
        // store a reference to the grid
        this.grid = grid;

        // start at the top row
        selectedRow = 0;

        // choose the origin so that the sprite nicely wraps around a grid row
        origin = new Vector2(10, 10);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // move the row selector up or down?
        if (inputHelper.KeyPressed(Keys.Up))
            selectedRow--;
        else if (inputHelper.KeyPressed(Keys.Down))
            selectedRow++;

        // make sure the row selector doesn't go outside the grid
        selectedRow = MathHelper.Clamp(selectedRow, 0, grid.Height - 1);

        // set the position to match the selected row
        LocalPosition = grid.GetCellPosition(0, selectedRow);

        // shift the current row to the left or right?
        if (inputHelper.KeyPressed(Keys.Left))
            grid.ShiftRowLeft(selectedRow);
        else if (inputHelper.KeyPressed(Keys.Right))
            grid.ShiftRowRight(selectedRow);
    }
}