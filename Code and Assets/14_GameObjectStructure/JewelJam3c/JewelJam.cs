using Microsoft.Xna.Framework;
using System;

class JewelJam : ExtendedGame
{
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
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        gameWorld.AddChild(background);

        // add the grid
        JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize, GridOffset);
        gameWorld.AddChild(grid);

        // set the world size to the width and height of the background sprite
        worldSize = new Point(background.Width, background.Height);

        // to let the new world size take effect, we need to set the FullScreen property again
        FullScreen = false;
    }

}