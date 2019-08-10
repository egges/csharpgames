using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// A class that can represent a game object with a sprite.
    /// </summary>
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
        /// The depth (between 0 and 1) at which this object should be drawn. 
        /// A larger value means that the object will be drawn on top.
        /// </summary>
        protected float depth;

        /// <summary>
        /// Creates a new SpriteGameObject with a given sprite name.
        /// </summary>
        /// <param name="spriteName">The name of the sprite to load.</param>
        /// <param name="depth">The depth at which the object should be drawn.</param>
        /// <param name="sheetIndex">The sheet index of the sprite to use initially.</param>
        public SpriteGameObject(string spriteName, float depth, int sheetIndex=0)
        {
            this.depth = depth;

            if (spriteName != null)
                sprite = new SpriteSheet(spriteName, depth, sheetIndex);
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
            if (!Visible)
                return;

            // draw the sprite at its *global* position in the game world
           if (sprite != null)
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
            Origin = sprite.Center;
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

        /// <summary>
        /// Checks and returns if this <see cref="SpriteGameObject"/> 
        /// has at least one non-transparent pixel inside the given rectangle.
        /// </summary>
        /// <param name="other">A rectangle in the game world.</param>
        /// <returns>true if this object overlaps with the given rectangle 
        /// and that intersection contains at least one non-transparent pixel.
        /// Returns false otherwise.</returns>
        public bool HasPixelPreciseCollision(Rectangle other)
        {
            // calculate the intersection between the two bounding boxes
            Rectangle b = CollisionDetection.CalculateIntersection(BoundingBox, other);

            // if at least one pixel in this part is not transparent, then there's a collision
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    // get the correct pixel coordinates of both sprites
                    int thisX = b.X - (int)(GlobalPosition.X - Origin.X) + x;
                    int thisY = b.Y - (int)(GlobalPosition.Y - Origin.Y) + y;

                    if (!sprite.IsPixelTransparent(thisX, thisY))
                        return true;
                }
            }

            // otherwise, there is no collision
            return false;
        }

        /// <summary>
        /// Checks and returns if this <see cref="SpriteGameObject"/> has at least one
        /// overlapping non-transparent pixel with another <see cref="SpriteGameObject"/>.
        /// </summary>
        /// <param name="other">A <see cref="SpriteGameObject"/> to compare to.</param>
        /// <returns>true if the two objects overlap and that intersection contains 
        /// at least one pixel that is non-transparent for both objects.
        /// Returns false otherwise.</returns>
        public bool HasPixelPreciseCollision(SpriteGameObject other)
        {
            // calculate the intersection between the two bounding boxes
            Rectangle b = CollisionDetection.CalculateIntersection(BoundingBox, other.BoundingBox);
            
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    // get the correct pixel coordinates of both sprites
                    int thisX = b.X - (int)(GlobalPosition.X - Origin.X) + x;
                    int thisY = b.Y - (int)(GlobalPosition.Y - Origin.Y) + y;
                    int otherX = b.X - (int)(other.GlobalPosition.X - other.Origin.X) + x;
                    int otherY = b.Y - (int)(other.GlobalPosition.Y - other.Origin.Y) + y;
                    
                    // if both pixels are not transparent, then there is a collision
                    if (!sprite.IsPixelTransparent(thisX, thisY) && !other.sprite.IsPixelTransparent(otherX, otherY))
                        return true;
                }
            }

            // otherwise, there is no collision
            return false;
        }
    }
}