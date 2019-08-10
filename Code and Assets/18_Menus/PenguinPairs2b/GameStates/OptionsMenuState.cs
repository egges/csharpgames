using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

class OptionsMenuState : GameState
{
    Button backButton;
    Switch hintsSwitch;
    Slider musicVolumeSlider;

    public OptionsMenuState()
    {
        // add a background
        SpriteGameObject background = new SpriteGameObject("Sprites/spr_background_options");
        gameObjects.AddChild(background);

        // add a back button
        backButton = new Button("Sprites/UI/spr_button_back");
        backButton.LocalPosition = new Vector2(415, 720);
        gameObjects.AddChild(backButton);

        // add a switch for enabling/disabling hints
        // - text
        TextGameObject hintsLabel = new TextGameObject("Fonts/MenuFont", Color.DarkBlue);
        hintsLabel.Text = "Hints";
        hintsLabel.LocalPosition = new Vector2(150, 340);
        gameObjects.AddChild(hintsLabel);
        // - switch
        hintsSwitch = new Switch("Sprites/UI/spr_switch@2");
        hintsSwitch.LocalPosition = new Vector2(650, 340);
        gameObjects.AddChild(hintsSwitch);

        // add a slider to control the background music volume
        // - text
        TextGameObject musicVolumeLabel = new TextGameObject("Fonts/MenuFont", Color.DarkBlue);
        musicVolumeLabel.Text = "Music Volume";
        musicVolumeLabel.LocalPosition = new Vector2(150, 480);
        gameObjects.AddChild(musicVolumeLabel);
        // - slider
        musicVolumeSlider = new Slider("Sprites/UI/spr_slider_bar", "Sprites/UI/spr_slider_button", 0, 1, 8);
        musicVolumeSlider.LocalPosition = new Vector2(650, 500);
        gameObjects.AddChild(musicVolumeSlider);

        // apply the initial game settings
        musicVolumeSlider.Value = MediaPlayer.Volume;
        hintsSwitch.Selected = PenguinPairs.HintsEnabled;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
            ExtendedGame.GameStateManager.SwitchTo(PenguinPairs.StateName_Title);

        if (hintsSwitch.Pressed)
            PenguinPairs.HintsEnabled = hintsSwitch.Selected;

        if (musicVolumeSlider.ValueChanged)
            MediaPlayer.Volume = musicVolumeSlider.Value;
    }
}