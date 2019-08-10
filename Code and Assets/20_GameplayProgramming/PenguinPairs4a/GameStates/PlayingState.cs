using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class PlayingState : GameState
{
    Level level;

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

        if (level != null)
            level.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (level != null)
            level.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (level != null)
            level.Draw(gameTime, spriteBatch);
    }

    public void LoadLevel(int levelIndex)
    {
        level = new Level(levelIndex, "Content/Levels/level" + levelIndex + ".txt");

        // update the visibilty of the hint button
        hintButton.Visible = PenguinPairs.HintsEnabled;
    }
}