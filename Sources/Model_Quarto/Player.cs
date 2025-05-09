namespace Model
{
    public abstract class Player : IEquatable<Player>
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Player</c>.
        /// </summary>
        /// <param name="pseudo"> name of the player </param>
        /// <exception cref="ArgumentNullException"> if the string is null</exception>
        public Player(string pseudo)
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
        public bool Equals(Player? other) => other != null
            && Pseudo == other.Pseudo;

        /// <summary>
        /// This method tells if two objects are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj);
        }

        /// <summary>
        /// This method gives us the hashcode of a <c>Player</c>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Pseudo.GetHashCode();
    }
}
