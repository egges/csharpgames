using Microsoft.Xna.Framework;

class JewelJamGameWorld : GameObjectList
{
    const int GridWidth = 5;
    const int GridHeight = 10;
    const int CellSize = 85;
    
    // The size of the game world, in game units.
    public Point Size { get; private set; }
    
    // The player's current score.
    public int Score { get; private set; }

    public JewelJamGameWorld()
    {
        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        Size = new Point(background.Width, background.Height);
        AddChild(background);

        // add a "playing field" parent object for the grid and all related objects
        GameObjectList playingField = new GameObjectList();
        playingField.LocalPosition = new Vector2(85, 150);
        AddChild(playingField);

        // add the grid to the playing field
        JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize);
        playingField.AddChild(grid);

        // add the row selector to the playing field
        playingField.AddChild(new RowSelector(grid));

        // reset some game parameters
        Reset();
    }

    public void AddScore(int points)
    {
        Score += points;
    }
    
    public override void Reset()
    {
        base.Reset();
        Score = 0;
    }
}