using Microsoft.Xna.Framework.Input;

class HelpState : GameState
{
    public HelpState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_help");
        gameObjects.AddChild(background);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Title);
    }
}