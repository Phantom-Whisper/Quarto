namespace Model
{
    public class Player : IEquatable<Player>
    {

        public Player(string pseudo)
        {
            Pseudo = pseudo ?? throw new ArgumentNullException(nameof(pseudo));
            NbWin = 0;
        }

        public string Pseudo
        {
            get;
            init;
        }


        public int NbWin
        {
            get;
            set;
        }

        public void AddWin()
        {
            NbWin++;
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
            return base.Equals(obj);
        }

        /// <summary>
        /// This method gives us the hashcode of a <c>Player</c>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Pseudo.GetHashCode();
    }
}