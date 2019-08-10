using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteSheet
{
    Texture2D sprite;
    Rectangle spriteRectangle;

    int sheetIndex;
    int sheetColumns;
    int sheetRows;

    public SpriteSheet(string assetname, int sheetIndex = 0)
    {
        // retrieve the sprite
        sprite = ExtendedGame.AssetManager.LoadSprite(assetname);
        
        sheetColumns = 1;
        sheetRows = 1;

        // see if we can extract the number of sheet elements from the assetname
        string[] assetSplit = assetname.Split('@');
        if (assetSplit.Length >= 2)
        {
            // behind the last '@' symbol, there should be a number.
            // This number can be followed by an 'x' and then another number.
            string sheetNrData = assetSplit[assetSplit.Length - 1];
            string[] columnAndRow = sheetNrData.Split('x');
            sheetColumns = int.Parse(columnAndRow[0]);
            if (columnAndRow.Length == 2)
                sheetRows = int.Parse(columnAndRow[1]);
        }

        // apply the sheet index; this will also calculate spriteRectangle
        SheetIndex = sheetIndex;
    }

    /// <summary>
    /// Draws the sprite (or the appropriate part of it) at the desired position.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object used for drawing sprites.</param>
    /// <param name="position">A position in the game world.</param>
    /// <param name="origin">An origin that should be subtracted from the drawing position.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        spriteBatch.Draw(sprite, position, spriteRectangle, Color.White,
            0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Gets the width of a single sprite in this sprite sheet.
    /// </summary>
    public int Width
    {
        get { return sprite.Width / sheetColumns; }
    }

    /// <summary>
    /// Gets the height of a single sprite in this sprite sheet.
    /// </summary>
    public int Height
    {
        get { return sprite.Height / sheetRows; }
    }

    /// <summary>
    /// Gets a vector that represents the center of a single sprite in this sprite sheet.
    /// </summary>
    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    /// <summary>
    /// Gets or sets the sprite index within this sprite sheet to use. 
    /// If you set a new index, the object will recalculate which part of the sprite should be drawn.
    /// </summary>
    public int SheetIndex
    {
        get { return sheetIndex; }
        set
        {
            if (value >= 0 && value < NumberOfSheetElements)
            {
                sheetIndex = value;

                // recalculate the part of the sprite to draw
                int columnIndex = sheetIndex % sheetColumns;
                int rowIndex = sheetIndex / sheetColumns;
                spriteRectangle = new Rectangle(columnIndex * Width, rowIndex * Height, Width, Height);
            }
        }
    }

    /// <summary>
    /// Gets a Rectangle that represents the bounds of a single sprite in this sprite sheet.
    /// </summary>
    public Rectangle Bounds
    {
        get
        {
            return new Rectangle(0, 0, Width, Height);
        }
    }

    /// <summary>
    /// Gets the total number of elements in this sprite sheet.
    /// </summary>
    public int NumberOfSheetElements
    {
        get { return sheetColumns * sheetRows; }
    }
}