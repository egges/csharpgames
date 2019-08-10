using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteSheet
{
    Texture2D sprite;
    Rectangle spriteRectangle;
    int sheetIndex, sheetColumns, sheetRows;

    public int Width { get { return sprite.Width / sheetColumns; } }
    public int Height { get { return sprite.Height / sheetRows; } }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }
    public Rectangle Bounds
    {
        get { return new Rectangle(0, 0, Width, Height); }
    }
    public int NumberOfSheetElements
    {
        get { return sheetColumns * sheetRows; }
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        spriteBatch.Draw(sprite, position, spriteRectangle, Color.White,
            0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
    }
}