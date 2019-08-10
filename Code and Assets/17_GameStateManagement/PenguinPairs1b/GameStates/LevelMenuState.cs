using Microsoft.Xna.Framework.Input;

class LevelMenuState : GameState
{
    public LevelMenuState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_levelselect");
        gameObjects.AddChild(background);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.Back))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Title);
    }
}

