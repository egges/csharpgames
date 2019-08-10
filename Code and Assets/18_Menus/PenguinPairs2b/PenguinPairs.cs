using System;
using Microsoft.Xna.Framework;

class PenguinPairs : ExtendedGame
{
    public const string StateName_Title = "title";
    public const string StateName_Help = "help";
    public const string StateName_Options = "options";
    public const string StateName_LevelSelect = "levelselect";
    public const string StateName_Playing = "playing";

    /// <summary>
    /// Whether or not hints are enabled in the game.
    /// </summary>
    public static bool HintsEnabled { get; set; }

    [STAThread]
    static void Main()
    {
        PenguinPairs game = new PenguinPairs();
        game.Run();
    }

    public PenguinPairs()
    {
        IsMouseVisible = true;
        HintsEnabled = true;
    }
    
    protected override void LoadContent()
    {
        base.LoadContent();

        // set a custom world and window size
        worldSize = new Point(1200, 900);
        windowSize = new Point(1024, 768);

        // to let these settings take effect, we need to set the FullScreen property again
        FullScreen = false;

        // add the game states
        GameStateManager.AddGameState(StateName_Title, new TitleMenuState());
        GameStateManager.AddGameState(StateName_Options, new OptionsMenuState());
        GameStateManager.AddGameState(StateName_LevelSelect, new LevelMenuState());
        GameStateManager.AddGameState(StateName_Help, new HelpState());
        GameStateManager.AddGameState(StateName_Playing, new PlayingState());

        // start at the title screen
        GameStateManager.SwitchTo(StateName_Title);
    }
}