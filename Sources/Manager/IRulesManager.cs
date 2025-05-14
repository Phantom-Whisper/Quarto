using System.IO.Pipelines;

namespace Manager
{
    public interface IRulesManager
    {
        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <param name="bag">The piece bag.</param>
        /// <param name="board">The current game board.</param>
        /// <returns>The winning player, or null if no winner.</returns>
        IPlayer? IsGameOver(IBag bag, IBoard board);
    }
}
