using Manager;
using Microsoft.VisualBasic;
using Serialize;
using System.Collections.ObjectModel;

namespace Model
{
    /// <summary>
    /// Manages the victory scores for each human player.
    /// Provides methods to add, retrieve, save, and load scores.
    /// </summary>
    public class ScoreManager : IScoreManager
    {
        /// <summary>
        /// Collection that holds player names and their number of victories.
        /// </summary>
        public ObservableCollection<PlayerScore> Scores { get; private set; } = new();

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

            var existing = Scores.FirstOrDefault(ps => ps.Name == winner.Name);
            if (existing != null)
            {
                existing.Score++;
            }
            else
            {
                Scores.Add(new PlayerScore(winner.Name, 1));
            }
        }

        /// <summary>
        /// Gets the score of the specified player.
        /// </summary>
        /// <param name="player">The player whose score is being retrieved.</param>
        /// <returns>The number of victories the player has.</returns>
        public int GetScore(IPlayer player)
        {
            if (player is null) return 0;

            return Scores.FirstOrDefault(ps => ps.Name == player.Name)?.Score ?? 0;
        }

        /// <summary>
        /// Loads the scores from the file.
        /// </summary>
        public void LoadScores()
        {
            Scores = serializer.Load<ObservableCollection<PlayerScore>>() ?? new();
        }

        /// <summary>
        /// Saves the scores to the file.
        /// </summary>
        public void SaveScores()
        {
            var sorted = Scores.OrderByDescending(ps => ps.Score).ToList();

            Scores.Clear();
            foreach (var item in sorted)
            {
                Scores.Add(item);
            }
            serializer.Save(Scores);
        }

        /// <summary>
        /// Returns the scores as an ObservableCollection.
        /// </summary>
        public ObservableCollection<PlayerScore> GetAllScores()
        {
            return Scores;
        }
    }
}
