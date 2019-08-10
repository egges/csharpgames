using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

partial class Level : GameObjectList
{
    public const int TileWidth = 72;
    public const int TileHeight = 55;

    Tile[,] tiles;
    List<WaterDrop> waterDrops;
    Player player;
    public int LevelIndex { get; private set; }

    SpriteGameObject goal;

    public Level(int levelIndex, string filename)
    {
        LevelIndex = levelIndex;

        // load the background
        GameObjectList backgrounds = new GameObjectList();
        SpriteGameObject backgroundSky = new SpriteGameObject("Sprites/Backgrounds/spr_sky", TickTick.Depth_Background);
        backgroundSky.LocalPosition = new Vector2(0, 825 - backgroundSky.Height);
        backgrounds.AddChild(backgroundSky);
        AddChild(backgrounds);

        // load the rest of the level
        LoadLevelFromFile(filename);
    }

    public Vector2 GetCellPosition(int x, int y)
    {
        return new Vector2(x * TileWidth, y * TileHeight);
    }

    public Point GetTileCoordinates(Vector2 position)
    {
        return new Point((int)Math.Floor(position.X / TileWidth), (int)Math.Floor(position.Y / TileHeight));
    }

    public Tile.Type GetTileType(int x, int y)
    {
        // If the x-coordinate is out of range, treat the coordinates as a wall tile.
        // This will prevent the character from walking outside the level.
        if (x < 0 || x >= tiles.GetLength(0))
            return Tile.Type.Wall;

        // If the y-coordinate is out of range, treat the coordinates as an empty tile.
        // This will allow the character to still make a full jump near the top of the level.
        if (y < 0 || y >= tiles.GetLength(1))
            return Tile.Type.Empty;

        return tiles[x, y].TileType;
    }

    public Tile.SurfaceType GetSurfaceType(int x, int y)
    {
        // If the tile with these coordinates doesn't exist, return the normal surface type.
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
            return Tile.SurfaceType.Normal;

        // Otherwise, return the actual surface type of the tile.
        return tiles[x, y].Surface;
    }
}

