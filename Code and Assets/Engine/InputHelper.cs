using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
    /// <summary>
    /// A class that manages mouse and keyboard input.
    /// </summary>
    public class InputHelper
    {
        // the current and previous mouse state
        MouseState currentMouseState, previousMouseState;
        // the current and previous keyboard state
        KeyboardState currentKeyboardState, previousKeyboardState;

        // A reference to the game
        ExtendedGame game;

        public InputHelper(ExtendedGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// Updates this InputHelper object for one frame of the game loop.
        /// This method retrievse the current the mouse and keyboard state, and stores the previous states as a backup.
        /// </summary>
        public void Update()
        {
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Gets the current position of the mouse in screen coordinates.
        /// </summary>
        public Vector2 MousePositionScreen
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        /// <summary>
        /// Gets the current position of the mouse in world coordinates.
        /// </summary>
        public Vector2 MousePositionWorld
        {
            get { return game.ScreenToWorld(MousePositionScreen); }
        }

        /// <summary>
        /// Checks and returns whether the player has started pressing the left mouse button in the last frame of the game loop.
        /// </summary>
        /// <returns>true if the left mouse button is now pressed and was not yet pressed in the previous frame; false otherwise.</returns>
        public bool MouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Checks and returns whether the left mouse button is currently being held down.
        /// </summary>
        /// <returns>true if the left mouse button is currently being held down; false otherwise.</returns>
        public bool MouseLeftButtonDown()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Checks and returns whether the player has started pressing a certain keyboard key in the last frame of the game loop.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key is now pressed and was not yet pressed in the previous frame; false otherwise.</returns>
        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        /// <summary>
        /// Checks and returns whether the player has stopped pressing a certain keyboard key in the last frame of the game loop.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key is no longer pressed but was still pressed in the previous frame; false otherwise.</returns>
        public bool KeyReleased(Keys k)
        {
            return currentKeyboardState.IsKeyUp(k) && previousKeyboardState.IsKeyDown(k);
        }

        /// <summary>
        /// Checks and returns whether the player is currently holding a certain keyboard key down.
        /// </summary>
        /// <param name="k">The key to check.</param>
        /// <returns>true if the given key is currently being held down; false otherwise.</returns>
        public bool KeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }
    }
}