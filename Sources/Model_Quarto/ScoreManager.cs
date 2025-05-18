using Manager;
using Serialize;

namespace Model
{
    /// <summary>
    /// Manages the victory scores for each human player.
    /// Provides methods to add, retrieve, save, and load scores.
    /// </summary>
    public class ScoreManager : IScoreManager
    {
        /// <summary>
        /// Dictionary that maps a player's name to their number of victories.
        /// </summary>
        private Dictionary<string, int> Scores { get; set; } = new();

        /// <summary>
        /// Serializer responsible for loading and saving score data.
        /// </summary>
        private readonly PlayerScoreTracker serializer = new();

        /// <summary>
        /// Adds a victory to the given player.
        /// </summary>
        /// <param name="winner">The player who won the game.</param>
        public void AddVictory(IPlayer winner)
        {
            if (winner is null) return;

            string name = winner.Name;

            if (Scores.ContainsKey(name))
                Scores[name]++;
            else
                Scores[name] = 1;
        }

        /// <summary>
        /// Gets the score of the specified player.
        /// </summary>
        /// <param name="player">The player whose score is being retrieved.</param>
        /// <returns>The number of victories the player has.</returns>
        public int GetScore(IPlayer player)
        {
            if (player is null) return 0;

            return Scores.TryGetValue(player.Name, out int score) ? score : 0;
        }

        /// <summary>
        /// Loads the scores from the file.
        /// </summary>
        public void LoadScores()
        {
            Scores = serializer.Load<Dictionary<string, int>>() ?? new();
        }

        /// <summary>
        /// Saves the scores to the file.
        /// </summary>
        public void SaveScores()
        {
            serializer.Save(Scores);
        }

        /// <summary>
        /// Returns a copy of all scores.
        /// </summary>
        /// <returns>A dictionary of player names and their victory counts.</returns>
        public Dictionary<string, int> GetAllScores()
        {
            return new Dictionary<string, int>(Scores);
        }
    }
}
