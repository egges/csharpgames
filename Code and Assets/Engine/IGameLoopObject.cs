using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// An interface used for objects that can do "game loop"-related tasks:
    /// handling input, updating, drawing, and resetting.
    /// </summary>
    public interface IGameLoopObject
    {
        void HandleInput(InputHelper inputHelper);

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void Reset();
    }
}