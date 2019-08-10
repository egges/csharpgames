namespace Engine
{
    /// <summary>
    /// An interface for a playing state that can load a level and mark a level as completed.
    /// Each game should have at least one state that implements this interface.
    /// </summary>
    public interface IPlayingState
    {
        void LoadLevel(int levelIndex);
        void LevelCompleted(int levelIndex);
    }
}
