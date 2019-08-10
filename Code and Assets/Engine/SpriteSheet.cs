using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A class that can represent a sprite sheet: an image containing a grid of sprites.
    /// </summary>
    public class SpriteSheet
    {
        Texture2D sprite;
        Rectangle spriteRectangle;

        int sheetIndex;
        int sheetColumns;
        int sheetRows;

        /// <summary>
        /// Gets or sets whether the displayed sprite should be mirrored.
        /// </summary>
        public bool Mirror { get; set; }

        float depth;

        bool[,] pixelTransparency;

        /// <summary>
        /// Creates a new SpriteSheet with the given details.
        /// </summary>
        /// <param name="assetname">The name of the asset to load. 
        /// The number of sprites in the sheet will be extracted from this filename.</param>
        /// <param name="depth">The depth at which the sprite should be drawn.</param>
        /// <param name="sheetIndex">The sprite sheet index to use initially.</param>
        public SpriteSheet(string assetname, float depth, int sheetIndex=0)
        {
            this.depth = depth;
            
            // retrieve the sprite
            sprite = ExtendedGame.AssetManager.LoadSprite(assetname);

            // for all pixels, check if they are transparent
            Color[] colorData = new Color[sprite.Width * sprite.Height];
            sprite.GetData(colorData);
            pixelTransparency = new bool[sprite.Width, sprite.Height];
            for (int i = 0; i < colorData.Length; ++i)
                pixelTransparency[i % sprite.Width, i / sprite.Width] = colorData[i].A == 0;

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
            // mirror the sprite?
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Mirror)
                spriteEffects = SpriteEffects.FlipHorizontally;
            
            spriteBatch.Draw(sprite, position, spriteRectangle, Color.White,
                0.0f, origin, 1.0f, spriteEffects, depth);
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
                if (value < NumberOfSheetElements && value >= 0)
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

        /// <summary>
        /// Returns whether or not the pixel at a given coordinate is transparent.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <returns>true if the given pixel is fully transparent; false if it is not.</returns>
        public bool IsPixelTransparent(int x, int y)
        {
            int column = sheetIndex % sheetColumns;
            int row = sheetIndex / sheetColumns % sheetRows;

            return pixelTransparency[column * Width + x, row * Height + y];
        }
    }
}