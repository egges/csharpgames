using Microsoft.Xna.Framework.Input;

class OptionsMenuState : GameState
{
    public OptionsMenuState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_options");
        gameObjects.AddChild(background);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Title);
    }
}