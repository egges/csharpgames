using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class SpriteGameObject : GameObject
    {
        /// <summary>
        /// The sprite that this object can draw on the screen.
        /// </summary>
        protected SpriteSheet sprite;

        /// <summary>
        /// The origin ('offset') to use when drawing the sprite on the screen.
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The sheet index of the attached sprite sheet.
        /// </summary>
        public int SheetIndex
        {
            get { return sprite.SheetIndex; }
            set { sprite.SheetIndex = value; }
        }

        /// <summary>
        /// Creates a new SpriteGameObject with a given sprite name.
        /// </summary>
        /// <param name="spriteName">The name of the sprite to load.</param>
        /// <param name="sheetIndex">The sheet index of the sprite to use initially.</param>
        public SpriteGameObject(string spriteName, int sheetIndex = 0)
        {
            sprite = new SpriteSheet(spriteName, sheetIndex);
            Origin = Vector2.Zero;
        }

        /// <summary>
        /// Draws this SpriteGameObject on the screen, using its global position and origin. 
        /// Note that the object will only get drawn if it's actually marked as visible.
        /// </summary>
        /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
        /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
                sprite.Draw(spriteBatch, GlobalPosition, Origin);
        }

        /// <summary>
        /// Gets the width of this object in the game world, according to its sprite.
        /// </summary>
        public int Width { get { return sprite.Width; } }

        /// <summary>
        /// Gets the height of this object in the game world, according to its sprite.
        /// </summary>
        public int Height { get { return sprite.Height; } }

        /// <summary>
        /// Updates this object's origin so that it lies in the center of the sprite.
        /// </summary>
        public void SetOriginToCenter()
        {
            Origin = new Vector2(Width / 2.0f, Height / 2.0f);
        }

        /// <summary>
        /// Gets a Rectangle that describes this game object's current bounding box.
        /// This is useful for collision detection.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                // get the sprite's bounds
                Rectangle spriteBounds = sprite.Bounds;
                // add the object's position to it as an offset
                spriteBounds.Offset(GlobalPosition - Origin);
                return spriteBounds;
            }
        }
    }
}