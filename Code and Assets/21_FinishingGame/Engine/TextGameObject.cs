using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A game object that shows text (instead of an image).
    /// </summary>
    public class TextGameObject : GameObject
    {
        /// <summary>
        /// The font to use.
        /// </summary>
        protected SpriteFont font;

        /// <summary>
        /// The color to use when drawing the text.
        /// </summary>
        protected Color color;

        /// <summary>
        /// The text that this object should draw on the screen.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// An enum that describes the different ways in which a text can be aligned horizontally.
        /// </summary>
        public enum Alignment
        {
            Left, Right, Center
        }

        /// <summary>
        /// The horizontal alignment of this text.
        /// </summary>
        protected Alignment alignment;

        /// <summary>
        /// Creates a new TextGameObject with the given details.
        /// </summary>
        /// <param name="fontName">The name of the font to use.</param>
        /// <param name="color">The color with which the text should be drawn.</param>
        /// <param name="alignment">The horizontal alignment to use.</param>
        public TextGameObject(string fontName, Color color,
            Alignment alignment = Alignment.Left)
        {
            font = ExtendedGame.AssetManager.LoadFont(fontName);
            this.color = color;
            this.alignment = alignment;

            Text = "";
        }

        /// <summary>
        /// Draws this TextGameObject on the screen.
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            // calculate the origin
            Vector2 origin = new Vector2(OriginX, 0);

            // draw the text
            spriteBatch.DrawString(font, Text, GlobalPosition,
                color, 0f, origin, 1, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Gets the x-coordinate to use as an origin for drawing the text.
        /// This coordinate depends on the horizontal alignment and the width of the text.
        /// </summary>
        float OriginX
        {
            get
            {
                if (alignment == Alignment.Left) // left-aligned
                    return 0;

                if (alignment == Alignment.Right) // right-aligned
                    return font.MeasureString(Text).X;

                return font.MeasureString(Text).X / 2.0f; // centered
            }
        }
    }
}