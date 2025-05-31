using Manager;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    /// <summary>
    /// Abstract Class player used by <c>HumanPlayer</c> and <c>AIPlayer</c>
    /// </summary>
    public abstract class Player : IPlayer, IEqualityComparer<Player>, INotifyPropertyChanged
    {
        private string? name;

        /// <summary>
        /// name of the player
        /// </summary>
        public string Name 
        {
            get => name ?? throw new InvalidOperationException("Name is not intialized");
            protected set
            {
                if (name == value) return;
                name = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string? propertyName = null)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        /// <summary>
        /// Constructor of player
        /// </summary>
        /// <param name="name">pseudo chosen</param>
        protected Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// abstract method of the main method to play a turn according to the type of player it's different
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <param name="gameManager"> the main interface that manage the game</param>
        /// <returns>the piece chosen for the opponent</returns>
        public abstract void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager); // comportement à définir par les sous-classes

        /// <summary>
        /// This method tells if two <c>Player</c> are the same.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Player? p1, Player? p2)
        {
            if (ReferenceEquals(p1, p2)) return true;

            if (p1 == null || p2 == null) return false;

            return p1.Name == p2.Name;
        }

        /// <summary>
        /// This method gives us the hashcode of a <c>Player</c>.
        /// </summary>
        /// <returns></returns>
        public int GetHashCode(Player player)
        {
            return player == null ? throw new ArgumentNullException(nameof(player)) : player.Name.GetHashCode();
        }
    }
}
