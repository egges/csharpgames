using Microsoft.Xna.Framework;

class PlayingState : GameState
{
    Button hintButton, retryButton, quitButton;

    public PlayingState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_level");
        gameObjects.AddChild(background);

        // add a "hint" button
        hintButton = new Button("Sprites/UI/spr_button_hint");
        hintButton.LocalPosition = new Vector2(916, 20);
        gameObjects.AddChild(hintButton);

        // add a "retry" button, initially invisible
        retryButton = new Button("Sprites/UI/spr_button_retry");
        retryButton.LocalPosition = new Vector2(916, 20);
        retryButton.Visible = false;
        gameObjects.AddChild(retryButton);

        // add a "quit" button
        quitButton = new Button("Sprites/UI/spr_button_quit");
        quitButton.LocalPosition = new Vector2(1058, 20);
        gameObjects.AddChild(quitButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        // if the "quit" button is pressed, return to the level selection screen
        if (quitButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_LevelSelect);
    }
}