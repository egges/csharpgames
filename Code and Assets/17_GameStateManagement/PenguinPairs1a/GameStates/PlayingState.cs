using Microsoft.Xna.Framework.Input;

class PlayingState : GameState
{
    public PlayingState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_level");
        gameObjects.AddChild(background);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_LevelSelect);
    }
}