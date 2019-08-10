using System.Collections.Generic;
using System.IO;

namespace Engine
{
    /// <summary>
    /// An abstract class that represents a game with multiple levels and their statuses.
    /// </summary>
    public abstract class ExtendedGameWithLevels : ExtendedGame
    {
        public static string StateName_Title = "title";
        public static string StateName_Help = "help";
        public static string StateName_LevelSelect = "levelselect";
        public static string StateName_Playing = "playing";

        static List<LevelStatus> progress;

        /// <summary>
        /// The total number of levels in the game.
        /// </summary>
        public static int NumberOfLevels
        {
            get { return progress.Count; }
        }

        /// <summary>
        /// Loads the player's level progress from a text file.
        /// </summary>
        protected void LoadProgress()
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

        /// <summary>
        /// Gets the <see cref="LevelStatus"/> of the level with the given index.
        /// </summary>
        /// <param name="levelIndex">The index of the level to check,<param>
        /// <returns>The <see cref="LevelStatus"/> of the requested level.</returns>
        public static LevelStatus GetLevelStatus(int levelIndex)
        {
            return progress[levelIndex - 1];
        }

        /// <summary>
        /// Sets the <see cref="LevelStatus"/> of the given level to the given value.
        /// </summary>
        /// <param name="levelIndex">The index of the level to change.<param>
        /// <param name="status">The new desired status of the level.</param>
        static void SetLevelStatus(int levelIndex, LevelStatus status)
        {
            progress[levelIndex - 1] = status;
        }

        /// <summary>
        /// Marks a certain level as solved, then unlocks the next level (if applicable), 
        /// and finally saves the player's progress again.
        /// </summary>
        /// <param name="levelIndex">The index of the level to mark as solved.</param>
        public static void MarkLevelAsSolved(int levelIndex)
        {
            // mark this level as solved
            SetLevelStatus(levelIndex, LevelStatus.Solved);

            // if there is a next level, mark it as unlocked
            if (levelIndex < NumberOfLevels && GetLevelStatus(levelIndex + 1) == LevelStatus.Locked)
                SetLevelStatus(levelIndex + 1, LevelStatus.Unlocked);

            // store the new level status
            SaveProgress();
        }

        /// <summary>
        /// Saves the player's progress to a file.
        /// </summary>
        static void SaveProgress()
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
        /// Sends the player to the next level in the game if such a level exists, 
        /// or to the level selection screen otherwise.
        /// </summary>
        /// <param name="levelIndex">The index of the current level.</param>
        public static void GoToNextLevel(int levelIndex)
        {
            // if this is the last level, go back to the level selection menu
            if (levelIndex == NumberOfLevels)
                GameStateManager.SwitchTo(StateName_LevelSelect);

            // otherwise, go to the next level
            else
                GetPlayingState().LoadLevel(levelIndex + 1);
        }

        /// <summary>
        /// Returns the game state with key StateName_Playing, cast to an IPlayingState object. 
        /// Each specific game should make sure that this game state actually exists.
        /// </summary>
        /// <returns>The game state with key StateName_Playing, cast to an IPlayingState object.</returns>
        public static IPlayingState GetPlayingState()
        {
            return (IPlayingState)GameStateManager.GetGameState(StateName_Playing);
        }
    }
}
