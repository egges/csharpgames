namespace Engine
{
    /// <summary>
    /// An enum containing the three possible progress statuses that a level can have: locked, unlocked, or solved.
    /// </summary>
    public enum LevelStatus
    {
        /// <summary>
        /// Indicates that a level is locked (and unavailable to the player).
        /// </summary>
        Locked,

        /// <summary>
        /// Indicates that a level has already been unlocked by the player, but hasn't been solved yet.
        /// </summary>
        Unlocked,

        /// <summary>
        /// Indicates that a level has been solved by the player.
        /// </summary>
        Solved
    }
}