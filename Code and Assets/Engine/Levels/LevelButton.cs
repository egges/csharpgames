using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.UI
{
    /// <summary>
    /// A class that can represent a single button on a level selection screen.
    /// </summary>
    public class LevelButton : Button
    {
        /// <summary>
        /// The index of the level that this button represents.
        /// </summary>
        public int LevelIndex { get; private set; }

        LevelStatus status;
        protected TextGameObject label;

        /// <summary>
        /// Creates a new <see cref="LevelButton"/> with the given level index and level status.
        /// </summary>
        /// <param name="levelIndex">The index of the level to which this button corresponds.</param>
        /// <param name="startStatus">The initial status of the associated level.</param>
        public LevelButton(int levelIndex, LevelStatus startStatus)
            : base(null, 0.9f)
        {
            LevelIndex = levelIndex;
            Status = startStatus;
        }

        /// <summary>
        /// Gets or sets the status of this level button.
        /// When you change the status, the button will receive a different sprite.
        /// </summary>
        public LevelStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                sprite = new SpriteSheet(getSpriteNameForStatus(status), depth);
                sprite.SheetIndex = (LevelIndex - 1) % sprite.NumberOfSheetElements;
            }
        }

        protected virtual string getSpriteNameForStatus(LevelStatus status)
        {
            if (status == LevelStatus.Locked)
                return "Sprites/UI/spr_level_locked";
            if (status == LevelStatus.Unlocked)
                return "Sprites/UI/spr_level_unsolved";
            return "Sprites/UI/spr_level_solved";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (label != null)
                label.Draw(gameTime, spriteBatch);
        }
    }
}