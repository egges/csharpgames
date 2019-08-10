using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

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

    static List<LevelStatus> progress;

    /// <summary>
    /// The total number of levels in the game.
    /// </summary>
    public static int NumberOfLevels
    {
        get { return progress.Count; }
    }

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

        // load the player's progress from a file
        LoadProgress();

        // add the game states
        GameStateManager.AddGameState(StateName_Title, new TitleMenuState());
        GameStateManager.AddGameState(StateName_Options, new OptionsMenuState());
        GameStateManager.AddGameState(StateName_LevelSelect, new LevelMenuState());
        GameStateManager.AddGameState(StateName_Help, new HelpState());
        GameStateManager.AddGameState(StateName_Playing, new PlayingState());

        // start at the title screen
        GameStateManager.SwitchTo(StateName_Title);

        // play the background music
        AssetManager.PlaySong("Sounds/snd_music", true);
    }

    void LoadProgress()
    {
        // prepare a list of LevelStatus values
        progress = new List<LevelStatus>();

        // read the "levels_status" file; add a LevelStatus for each line
        StreamReader r = new StreamReader("Content/Levels/levels_status.txt");
        string line = r.ReadLine();
        while (line != null)
        {
            if (line == "locked")
                progress.Add(LevelStatus.Locked);
            else if (line == "unlocked")
                progress.Add(LevelStatus.Unlocked);
            else if (line == "solved")
                progress.Add(LevelStatus.Solved);

            // go to the next line
            line = r.ReadLine();
        }
        r.Close();
    }

    public static LevelStatus GetLevelStatus(int levelIndex)
    {
        return progress[levelIndex - 1];
    }

    static void SetLevelStatus(int levelIndex, LevelStatus status)
    {
        progress[levelIndex - 1] = status;
    }

    public static void MarkLevelAsSolved(int levelIndex)
    {
        // mark this level as solved
        SetLevelStatus(levelIndex, LevelStatus.Solved);

        // if there is a next level, mark it as unlocked
        if (levelIndex < NumberOfLevels)
        {
            if (GetLevelStatus(levelIndex + 1) == LevelStatus.Locked)
                SetLevelStatus(levelIndex + 1, LevelStatus.Unlocked);
        }

        // store the new level status
        SaveProgress();
    }

    public static void SaveProgress()
    {
        // write to the "levels_status" file; add a LevelStatus for each line
        StreamWriter w = new StreamWriter("Content/Levels/levels_status.txt");
        foreach (LevelStatus status in progress)
        {
            if (status == LevelStatus.Locked)
                w.WriteLine("locked");
            else if (status == LevelStatus.Unlocked)
                w.WriteLine("unlocked");
            else
                w.WriteLine("solved");
        }
        w.Close();
    }

    /// <summary>
    /// Sends the player to the next level, 
    /// or back to the level selection screen if there is no next level.
    /// </summary>
    /// <param name="levelIndex"></param>
    public static void GoToNextLevel(int levelIndex)
    {
        if (levelIndex == NumberOfLevels)
            GameStateManager.SwitchTo(StateName_LevelSelect);

        else
        {
            PlayingState playingState =
            (PlayingState)GameStateManager.GetGameState(StateName_Playing);
            playingState.LoadLevel(levelIndex + 1);
        }
    }
}