using Microsoft.Xna.Framework.Input;

class TitleMenuState : GameState
{
    public TitleMenuState()
    {
        // load the title screen
        SpriteGameObject titleScreen = new SpriteGameObject("Sprites/spr_titlescreen");
        gameObjects.AddChild(titleScreen);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Keys.L))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_LevelSelect);
        else if (inputHelper.KeyPressed(Keys.O))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Options);
        else if (inputHelper.KeyPressed(Keys.H))
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Help);
    }
}