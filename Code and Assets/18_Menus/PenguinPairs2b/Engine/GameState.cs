using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class that manages all objects belonging to a single game state.
/// </summary>
abstract class GameState : IGameLoopObject
{
    /// <summary>
    /// The game objects associated to this game state.
    /// </summary>
    protected GameObjectList gameObjects;

    /// <summary>
    /// Creates a new GameState object with an empty list of game objects.
    /// </summary>
    protected GameState()
    {
        gameObjects = new GameObjectList();
    }

    /// <summary>
    /// Calls HandleInout for all objects in this GameState.
    /// </summary>
    /// <param name="inputHelper">An object required for handling player input.</param>
    public virtual void HandleInput(InputHelper inputHelper)
    {
        gameObjects.HandleInput(inputHelper);
    }

    /// <summary>
    /// Calls Update for all objects in this GameState.
    /// </summary>
    /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
    public virtual void Update(GameTime gameTime)
    {
        gameObjects.Update(gameTime);
    }

    /// <summary>
    /// Draws all objects in this GameState.
    /// </summary>
    /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        gameObjects.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Calls Reset for all objects in this GameState.
    /// </summary>
    public virtual void Reset()
    {
        gameObjects.Reset();
    }
}
