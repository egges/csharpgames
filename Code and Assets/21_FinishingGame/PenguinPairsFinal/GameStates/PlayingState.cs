using Engine;
using Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class PlayingState : GameState
{
    Level level;

    Button hintButton, retryButton, quitButton;
    SpriteGameObject completedOverlay;

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

        // add an overlay image
        completedOverlay = new SpriteGameObject("Sprites/spr_level_finished");
        gameObjects.AddChild(completedOverlay);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (level != null)
        {
            // if the "completed" overlay is visible, pressing the spacebar should send the player to the next level
            if (completedOverlay.Visible)
            {
                if (inputHelper.MouseLeftButtonPressed())
                    PenguinPairs.GoToNextLevel(level.LevelIndex);
            }

            // otherwise, update the level itself, and check for button presses
            else
            {
                level.HandleInput(inputHelper);

                if (hintButton.Pressed)
                    level.ShowHint();
                else if (retryButton.Pressed)
                    level.Reset();
                if (quitButton.Pressed)
                    ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_LevelSelect);
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (level != null)
            level.Update(gameTime);

        hintButton.Visible = PenguinPairs.HintsEnabled && !level.FirstMoveMade;
        retryButton.Visible = level.FirstMoveMade;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (level != null)
            level.Draw(gameTime, spriteBatch);
    }

    public void LoadLevel(int levelIndex)
    {
        level = new Level(levelIndex, 
            "Content/Levels/level" + levelIndex + ".txt");

        // hide the overlay image
        completedOverlay.Visible = false;
    }

    public void LevelCompleted(int levelIndex)
    {
        // show an overlay image
        completedOverlay.Visible = true;
        level.Visible = false;

        // play a sound
        ExtendedGame.AssetManager.PlaySoundEffect("Sounds/snd_won");

        // mark the level as solved, and unlock the next level
        PenguinPairs.MarkLevelAsSolved(levelIndex);
    }
}