using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

partial class Level : GameObjectList
{
    void LoadLevelFromFile(string filename)
    {
        // open the file
        StreamReader reader = new StreamReader(filename);

        // read the description
        string description = reader.ReadLine();

        // read the rows of the grid; keep track of the longest row
        int gridWidth = 0;

        List<string> gridRows = new List<string>();
        string line = reader.ReadLine();
        while (line != null)
        {
            if (line.Length > gridWidth)
                gridWidth = line.Length;

            gridRows.Add(line);
            line = reader.ReadLine();
        }

        // stop reading the file
        reader.Close();
        
        // create all game objects for the grid
        AddPlayingField(gridRows, gridWidth, gridRows.Count);

        // add game objects to show that general level info
        AddLevelInfoObjects(description);
    }

    void AddLevelInfoObjects(string description)
    {
        // - background box
        SpriteGameObject frame = new SpriteGameObject("Sprites/UI/spr_frame_hint", TickTick.Depth_UIBackground);
        frame.SetOriginToCenter();
        frame.LocalPosition = new Vector2(720, 50);
        AddChild(frame);

        // - text
        TextGameObject hintText = new TextGameObject("Fonts/HintFont", TickTick.Depth_UIForeground, Color.Black, TextGameObject.Alignment.Left);
        hintText.Text = description;
        hintText.LocalPosition = new Vector2(510, 40);
        AddChild(hintText);
    }

    void AddPlayingField(List<string> gridRows, int gridWidth, int gridHeight)
    {
        // create a parent object for everything
        GameObjectList playingField = new GameObjectList();

        // initialize the list of water drops
        waterDrops = new List<WaterDrop>();

        // prepare the grid arrays
        tiles = new Tile[gridWidth, gridHeight];

        // load the tiles
        for (int y = 0; y < gridHeight; y++)
        {
            string row = gridRows[y];
            for (int x = 0; x < gridWidth; x++)
            {
                // the row could be too short; if so, pretend there is an empty tile
                char symbol = '.';
                if (x < row.Length)
                    symbol = row[x];

                // load the tile
                AddTile(x, y, symbol);
            }
        }
    }

    void AddTile(int x, int y, char symbol)
    {
        // load the static part of the tile
        Tile tile = CharToStaticTile(symbol);
        tile.LocalPosition = GetCellPosition(x, y);
        AddChild(tile);

        // store a reference to that tile in the grid
        tiles[x, y] = tile;

        // load the dynamic part of the tile
        if (symbol == '1')
            LoadCharacter(x, y);
        else if (symbol == 'X')
            LoadGoal(x, y);
        else if (symbol == 'W')
            LoadWaterDrop(x, y);
        // TODO: other cases
    }

    Tile CharToStaticTile(char symbol)
    {
        switch (symbol)
        {
            case '-':
                return new Tile(Tile.Type.Platform, Tile.SurfaceType.Normal);
            case '#':
                return new Tile(Tile.Type.Wall, Tile.SurfaceType.Normal);
            case 'h':
                return new Tile(Tile.Type.Platform, Tile.SurfaceType.Hot);
            case 'H':
                return new Tile(Tile.Type.Wall, Tile.SurfaceType.Hot);
            case 'i':
                return new Tile(Tile.Type.Platform, Tile.SurfaceType.Ice);
            case 'I':
                return new Tile(Tile.Type.Wall, Tile.SurfaceType.Ice);
            default:
                return new Tile(Tile.Type.Empty, Tile.SurfaceType.Normal);
        }
    }

    void LoadCharacter(int x, int y)
    {
        // TODO: create the bomb character
    }

    void LoadGoal(int x, int y)
    {
        // create the exit object
        goal = new SpriteGameObject("Sprites/LevelObjects/spr_goal", TickTick.Depth_LevelObjects);
        goal.LocalPosition = GetCellPosition(x, y+1);
        goal.Origin = new Vector2(0, goal.Height);
        AddChild(goal);
    }

    void LoadWaterDrop(int x, int y)
    {
        // create the water drop object
        WaterDrop w = new WaterDrop();
        // place it around the center of the tile
        w.LocalPosition = GetCellPosition(x,y) + new Vector2(TileWidth / 2, TileHeight / 3);
        // add it to the game world
        AddChild(w);
        // store an extra reference to it
        waterDrops.Add(w);
    }
}