using Microsoft.Xna.Framework;

class JewelJamGameWorld : GameObjectList
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
    /// The horizontal and vertical distance between two adjacent grid cells.
    /// </summary>
    const int CellSize = 85;

    /// <summary>
    /// The size of the game world, in game units.
    /// </summary>
    Point worldSize;

    /// <summary>
    /// A reference to the moving jewel cart.
    /// </summary>
    JewelCart jewelCart;

    /// <summary>
    /// The player's current score.
    /// </summary>
    int score;

    public JewelJamGameWorld()
    {
        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
        worldSize = new Point(background.Width, background.Height);
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

        // add a background sprite for the score object
        SpriteGameObject scoreFrame = new SpriteGameObject("spr_scoreframe");
        scoreFrame.LocalPosition = new Vector2(20, 20);
        AddChild(scoreFrame);

        // add the object that displays the score
        ScoreGameObject scoreObject = new ScoreGameObject();
        scoreObject.LocalPosition = new Vector2(270, 30);
        AddChild(scoreObject);

        // add the moving jewel cart
        jewelCart = new JewelCart(new Vector2(410, 230));
        AddChild(jewelCart);

        // reset some game parameters
        Reset();
    }

    /// <summary>
    /// Gets the size of the game world, in game units.
    /// </summary>
    public Point Size
    {
        get
        {
            return worldSize;
        }
    }

    public void AddScore(int points)
    {
        // increase the score
        score += points;

        // push the jewel cart back a bit
        jewelCart.PushBack();
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    public override void Reset()
    {
        base.Reset();
        score = 0;
    }
}