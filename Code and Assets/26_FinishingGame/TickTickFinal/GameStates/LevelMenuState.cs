using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

class LevelMenuState : GameState
{
    Button backButton;

    // An array of extra references to the level buttons. 
    // This makes it easier to check if a level button has been pressed.
    LevelButton[] levelButtons;

    public LevelMenuState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/Backgrounds/spr_levelselect", TickTick.Depth_Background);
        gameObjects.AddChild(background);

        // add a back button
        backButton = new Button("Sprites/UI/spr_button_back", TickTick.Depth_UIForeground);
        backButton.LocalPosition = new Vector2(720, 690);
        backButton.SetOriginToCenter();
        gameObjects.AddChild(backButton);

        // Add a level button for each level.
        levelButtons = new LevelButton[ExtendedGameWithLevels.NumberOfLevels];

        Vector2 gridOffset = new Vector2(395, 175);
        const int buttonsPerRow = 4;
        const int spaceBetweenColumns = 20;
        const int spaceBetweenRows = 20;

        for (int i = 0; i < ExtendedGameWithLevels.NumberOfLevels; i++)
        {
            // create the button
            LevelButton levelButton = new LevelButton(i + 1, ExtendedGameWithLevels.GetLevelStatus(i + 1));

            // give it the correct position
            int row = i / buttonsPerRow;
            int column = i % buttonsPerRow;

            levelButton.LocalPosition = gridOffset + new Vector2(
                column * (levelButton.Width + spaceBetweenColumns),
                row * (levelButton.Height + spaceBetweenRows)
            );

            // add the button as a child object
            gameObjects.AddChild(levelButton);
            // also store it in the array of level buttons
            levelButtons[i] = levelButton;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        // if the back button is pressed, go back to the title screen
        if (backButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(ExtendedGameWithLevels.StateName_Title);

        // if a (non-locked) level button has been pressed, go to that level
        foreach (LevelButton button in levelButtons)
        {
            if (button.Pressed && button.Status != LevelStatus.Locked)
            {
                // go to the playing state
                ExtendedGame.GameStateManager.SwitchTo(ExtendedGameWithLevels.StateName_Playing);

                // load the correct level
                ExtendedGameWithLevels.GetPlayingState().LoadLevel(button.LevelIndex);

                return;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (LevelButton button in levelButtons)
        {
            if (button.Status != ExtendedGameWithLevels.GetLevelStatus(button.LevelIndex))
                button.Status = ExtendedGameWithLevels.GetLevelStatus(button.LevelIndex);
        }
    }
}
