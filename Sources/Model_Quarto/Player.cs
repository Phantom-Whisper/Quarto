using Manager;

namespace Model
{
    /// <summary>
    /// Abstract Class player used by <c>HumanPlayer</c> and <c>AIPlayer</c>
    /// </summary>
    public abstract class Player : IPlayer
    {
        /// <summary>
        /// name of the player
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Constructor of player
        /// </summary>
        /// <param name="name">pseudo chosen</param>
        protected Player(string name)
        {
            Name = name;
        }

        /// <summary>
        /// abstract method of the main method to play a turn according to the type of player it's different
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <param name="gameManager"> the main interface that manage the game</param>
        /// <returns>the piece chosen for the opponent</returns>
        public abstract IPiece? PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager); // comportement à définir par les sous-classes
    }
}
