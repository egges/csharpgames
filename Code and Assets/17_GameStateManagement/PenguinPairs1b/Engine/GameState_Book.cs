using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class GameState
{
    protected GameObjectList gameObjects;

    protected GameState()
    {
        gameObjects = new GameObjectList();
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        gameObjects.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        gameObjects.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        gameObjects.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
        gameObjects.Reset();
    }
}
