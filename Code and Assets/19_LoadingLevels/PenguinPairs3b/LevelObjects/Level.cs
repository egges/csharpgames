using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

class Level : GameObjectList
{
    const int TileWidth = 73;
    const int TileHeight = 72;

    const string MovableAnimalLetters = "brgycpmx";

    public int LevelIndex { get; private set; }

    int targetNumberOfPairs;

    Tile[,] tiles;
    Animal[,] animalsOnTiles;

    SpriteGameObject hintArrow;

    public Level(int levelIndex, string filename)
    {
        LevelIndex = levelIndex;
        LoadLevelFromFile(filename);
    }

    void LoadLevelFromFile(string filename)
    {
        // open the file
        StreamReader reader = new StreamReader(filename);

        // read the title and description
        string title = reader.ReadLine();
        string description = reader.ReadLine();

        // add game objects to show that general level info
        AddLevelInfoObjects(title, description);

        // read the number of required pairs
        targetNumberOfPairs = int.Parse(reader.ReadLine());

        // read the hint, but don't add the game object yet (because it should be part of the grid)
        string[] hint = reader.ReadLine().Split(' ');
        int hintX = int.Parse(hint[0]);
        int hintY = int.Parse(hint[1]);
        int hintDirection = StringToDirection(hint[2]);
        hintArrow = new SpriteGameObject("Sprites/LevelObjects/spr_arrow_hint@4", hintDirection);
        hintArrow.LocalPosition = GetCellPosition(hintX, hintY);

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
    }

    /// <summary>
    /// Adds game objects that show the level's general information: two text objects and a background image.
    /// </summary>
    /// <param name="title">The title of the level.</param>
    /// <param name="description">The one-line description of the level.</param>
    void AddLevelInfoObjects(string title, string description)
    {
        // - background box
        SpriteGameObject infoBackground = new SpriteGameObject("Sprites/spr_level_info");
        infoBackground.SetOriginToCenter();
        infoBackground.LocalPosition = new Vector2(600, 820);
        AddChild(infoBackground);

        // - title text
        TextGameObject titleText = new TextGameObject("Fonts/HelpFont", Color.Blue, TextGameObject.Alignment.Center);
        titleText.Text = LevelIndex + " - " + title;
        titleText.LocalPosition = new Vector2(600, 786);
        AddChild(titleText);

        // - description text
        TextGameObject descriptionText = new TextGameObject("Fonts/HelpFont", Color.DarkBlue, TextGameObject.Alignment.Center);
        descriptionText.Text = description;
        descriptionText.LocalPosition = new Vector2(600, 820);
        AddChild(descriptionText);
    }

    void AddPlayingField(List<string> gridRows, int gridWidth, int gridHeight)
    {
        // create a parent object for everything
        GameObjectList playingField = new GameObjectList();

        // this playing field should be roughly centered on the screen
        Vector2 gridSize = new Vector2(gridWidth * TileWidth, gridHeight * TileHeight);
        playingField.LocalPosition = new Vector2(600, 420) - gridSize / 2.0f;

        // prepare the grid arrays
        tiles = new Tile[gridWidth, gridHeight];
        animalsOnTiles = new Animal[gridWidth, gridHeight];

        // load the tiles and animals
        for (int y = 0; y < gridHeight; y++)
        {
            string row = gridRows[y];
            for (int x = 0; x < gridWidth; x++)
            {
                // the row could be too short; if so, pretend there is an empty tile
                char symbol = ' ';
                if (x < row.Length)
                    symbol = row[x];

                // load the non-changing part of the tile
                AddTile(x, y, symbol);

                // load the animal part of the tile, if it exists
                AddAnimal(x, y, symbol);
            }
        }

        // add the tiles to the playing field
        for (int y = 0; y < gridHeight; y++)
            for (int x = 0; x < gridWidth; x++)
                playingField.AddChild(tiles[x, y]);

        // add the animals after that, so that they will be drawn on top
        for (int y = 0; y < gridHeight; y++)
            for (int x = 0; x < gridWidth; x++)
                if (animalsOnTiles[x, y] != null)
                    playingField.AddChild(animalsOnTiles[x, y]);

        // add the hint arrow (so that it will be drawn on top), but make it invisible for now
        hintArrow.Visible = false;
        playingField.AddChild(hintArrow);

        // finally, add the playing field to the level
        AddChild(playingField);
    }

    /// <summary>
    /// Adds a tile at a grid position, by reading a given symbol.
    /// </summary>
    /// <param name="x">The x-coordinate of the tile.</param>
    /// <param name="y">The y-coordinate of the tile.</param>
    /// <param name="symbol">The symbol to read.</param>
    void AddTile(int x, int y, char symbol)
    {
        // create a tile with the appropriate type
        Tile tile = new Tile(CharToTileType(symbol), x, y);
        tile.LocalPosition = GetCellPosition(x, y);
        // store a reference in the grid
        tiles[x, y] = tile;
    }

    /// <summary>
    /// Determines and returns the tile type for a given symbol.
    /// </summary>
    /// <param name="symbol">The symbol to check.</param>
    /// <returns>The TileType represented by this symbol.</returns>
    Tile.Type CharToTileType(char symbol)
    {
        // the standard cases: tiles without an animal on them
        if (symbol == ' ')
            return Tile.Type.Empty;
        if (symbol == '.')
            return Tile.Type.Normal;
        if (symbol == '#')
            return Tile.Type.Wall;
        if (symbol == '_')
            return Tile.Type.Hole;

        // every other symbol can be either a hole tile or a normal tile
        if (GetAnimalInHoleIndex(symbol) >= 0)
            return Tile.Type.Hole;

        return Tile.Type.Normal;
    }

    /// <summary>
    /// Possibly adds an animal at a grid position, by reading a given symbol.
    /// If the symbol does not represent an animal, then nothnig happens.
    /// </summary>
    /// <param name="x">The x-coordinate of the tile.</param>
    /// <param name="y">The y-coordinate of the tile.</param>
    /// <param name="symbol">The symbol to read.</param>
    void AddAnimal(int x, int y, char symbol)
    {
        Animal result = null;

        // a shark?
        if (symbol == '@')
            result = new Shark();

        // a penguin or seal that is not in a hole?
        if (result == null)
        {
            int animalIndex = GetAnimalIndex(symbol);
            if (animalIndex >= 0)
                result = new MovableAnimal(animalIndex, false);
        }

        // a penguin or seal that *is* in a hole?
        if (result == null)
        {
            int animalIndex = GetAnimalInHoleIndex(symbol);
            if (animalIndex >= 0)
                result = new MovableAnimal(animalIndex, true);
        }

        // if we've loaded an animal now, add it to the grid
        if (result != null)
        {
            result.LocalPosition = GetCellPosition(x, y);
            animalsOnTiles[x, y] = result;
        }
    }

    /// <summary>
    /// Tries to convert a character to an index for a "non-holed" penguin or seal.
    /// </summary>
    /// <param name="symbol">The character to check.</param>
    /// <returns>An index (between 0 and 7) representing the type of penguin or seal, 
    /// or -1 if the given character does not represent such an animal.</returns>
    int GetAnimalIndex(char symbol)
    {
        return MovableAnimalLetters.IndexOf(symbol);
    }

    /// <summary>
    /// Tries to convert a character to an index for a "holed" penguin or seal.
    /// </summary>
    /// <param name="symbol">The character to check.</param>
    /// <returns>An index (between 0 and 7) representing the type of penguin or seal, 
    /// or -1 if the given character does not represent such an animal.</returns>
    int GetAnimalInHoleIndex(char symbol)
    {
        return MovableAnimalLetters.ToUpper().IndexOf(symbol);
    }

    int StringToDirection(string directionText)
    {
        if (directionText == "right")
            return 0;
        if (directionText == "up")
            return 1;
        if (directionText == "left")
            return 2;
        return 3;
    }

    public Vector2 GetCellPosition(int x, int y)
    {
        return new Vector2(x * TileWidth, y * TileHeight);
    }
}
