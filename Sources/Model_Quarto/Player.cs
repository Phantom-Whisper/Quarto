using Manager;

namespace Model
{
    /*public abstract class Player : IEqualityComparer<Player>
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Player</c>.
        /// </summary>
        /// <param name="pseudo"> name of the player </param>
        /// <exception cref="ArgumentNullException"> if the string is null</exception>
        protected Player(string pseudo)
        {
            Pseudo = !string.IsNullOrWhiteSpace(pseudo)
                ? pseudo
                : throw new ArgumentNullException(nameof(pseudo));
            NbWin = 0;
        }

        /// <summary>
        /// This property contains the name of the <c>Player</c>
        /// </summary>
        public string Pseudo
        {
            get;
            init;
        }

        /// <summary>
        /// This property contains the number of games won by the <c>Player</c>
        /// </summary>
        public int NbWin
        {
            get;
            set;
        }

        /// <summary>
        /// This method tells if two <c>Player</c> are the same.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Player? p1, Player? p2)
        {
            if (ReferenceEquals(p1, p2)) return true;

            if (p1 == null || p2 == null) return false;

            return p1.Pseudo == p2.Pseudo;
        }

        /// <summary>
        /// This method gives us the hashcode of a <c>Player</c>.
        /// </summary>
        /// <returns></returns>
        public int GetHashCode(Player player) => Pseudo.GetHashCode();
    }
    */
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
