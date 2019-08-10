using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Jewel : SpriteGameObject
{
    public int ColorType { get; private set; }
    public int ShapeType { get; private set; }
    public int NumberType { get; private set; }

    Rectangle spriteRectangle;

    /// <summary>
    /// Creates a new Jewel of a random type.
    /// </summary>
    public Jewel() : base("spr_jewels")
    {
        ColorType = ExtendedGame.Random.Next(3);
        ShapeType = ExtendedGame.Random.Next(3);
        NumberType = ExtendedGame.Random.Next(3);

        // The sprite is a single sheet that contains all possible jewel sprites.
        // Calculate the part of that sprite that we want to draw.
        int index = 9 * ColorType + 3 * ShapeType + NumberType;
        spriteRectangle = new Rectangle(index * sprite.Height, 0, sprite.Height, sprite.Height);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        // draw the correct sprite part at the jewel's position
        spriteBatch.Draw(sprite, GlobalPosition, spriteRectangle, Color.White);
    }
}