using Model;

namespace Stub
{
    public class StubPlayerScores
    {
        private readonly Dictionary<string, int> playerWins = new()
        {
            { "Alice", 3},
            { "Bob", 5},
            { "Charlie", 2},
            { "Diana", 4},
            { "Ethan", 1},
            { "Fiona", 6},
            { "George", 0},
            { "Hannah", 2},
            { "Isaac", 7},
            { "Julia", 5}
        };

        /// <summary>
        /// Returns a dictionary with HumanPlayer keys and their wins.
        /// </summary>
        public IReadOnlyDictionary<HumanPlayer, int> GetAllScores()
        {
            return playerWins.ToDictionary(
                kvp => new HumanPlayer(kvp.Key),
                kvp => kvp.Value);
        }
    }
}  
