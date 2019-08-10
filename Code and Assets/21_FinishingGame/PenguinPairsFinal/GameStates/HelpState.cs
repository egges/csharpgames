using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;

class HelpState : GameState
{
    Button backButton;

    public HelpState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_help");
        gameObjects.AddChild(background);

        // add a back button
        backButton = new Button("Sprites/UI/spr_button_back");
        backButton.LocalPosition = new Vector2(415, 720);
        gameObjects.AddChild(backButton);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (backButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Title);
    }
}