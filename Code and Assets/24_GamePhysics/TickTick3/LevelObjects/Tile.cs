using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Tile : GameObject
{
    public enum Type { Empty, Wall, Platform };
    public enum SurfaceType { Normal, Hot, Ice };

    Type type;
    SurfaceType surface;

    SpriteGameObject image;

    public Tile(Type type, SurfaceType surface)
    {
        this.type = type;
        this.surface = surface;

        // add an image depending on the type
        string surfaceExtension = "";
        if (surface == SurfaceType.Hot)
            surfaceExtension = "_hot";
        else if (surface == SurfaceType.Ice)
            surfaceExtension = "_ice";

        if (type == Type.Wall)
            image = new SpriteGameObject("Sprites/Tiles/spr_wall" + surfaceExtension, TickTick.Depth_LevelTiles);
        else if (type == Type.Platform)
            image = new SpriteGameObject("Sprites/Tiles/spr_platform" + surfaceExtension, TickTick.Depth_LevelTiles);

        // if there is an image, make it a child of this object
        if (image != null)
            image.Parent = this;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // draw the image if it exists
        if (image != null)
            image.Draw(gameTime, spriteBatch);
    }

    public Type TileType
    {
        get { return type; }
    }

    public SurfaceType Surface
    {
        get { return surface; }
    }
}