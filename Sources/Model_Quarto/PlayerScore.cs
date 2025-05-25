namespace Model
{
    /// <summary>
    /// Represents a player’s score with their name and points.
    /// </summary>
    public class PlayerScore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerScore"/> class with a player name and score.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="score">The score of the player.</param>
        public PlayerScore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        /// <summary>
        /// Gets or sets the player’s name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the player’s score.
        /// </summary>
        public int Score { get; set; }
    }
}