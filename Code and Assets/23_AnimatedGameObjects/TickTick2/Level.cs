using Engine;
using Microsoft.Xna.Framework;
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
}

