using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

class TitleMenuState : GameState
{
    Button playButton, optionsButton, helpButton;

    public TitleMenuState()
    {
        // load the title screen
        SpriteGameObject titleScreen = new SpriteGameObject("Sprites/spr_titlescreen");
        gameObjects.AddChild(titleScreen);

        // add a play button
        playButton = new Button("Sprites/UI/spr_button_play");
        playButton.LocalPosition = new Vector2(415, 540);
        gameObjects.AddChild(playButton);

        // add an options button
        optionsButton = new Button("Sprites/UI/spr_button_options");
        optionsButton.LocalPosition = new Vector2(415, 650);
        gameObjects.AddChild(optionsButton);

        // add a help button
        helpButton = new Button("Sprites/UI/spr_button_help");
        helpButton.LocalPosition = new Vector2(415, 760);
        gameObjects.AddChild(helpButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (playButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_LevelSelect);
        
        else if (optionsButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Options);
        
        else if (helpButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Help);
    }
}