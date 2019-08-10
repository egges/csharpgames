using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Tile : GameObject
{
    public enum Type { Normal, Empty, Wall, Hole };

    public Type TileType { get; private set; }

    SpriteGameObject image;

    public Tile(Type type, int x, int y)
    {
        TileType = type;

        // add an image depending on the type
        if (type == Type.Wall)
            image = new SpriteGameObject("Sprites/LevelObjects/spr_wall");
        else if (type == Type.Hole)
            image = new SpriteGameObject("Sprites/LevelObjects/spr_hole");
        else if (type == Type.Normal)
            image = new SpriteGameObject("Sprites/LevelObjects/spr_field@2", (x + y) % 2);

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
}