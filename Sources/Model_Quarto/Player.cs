using Manager;

namespace Model
{
    public abstract class Player : IPlayer, IEqualityComparer<Player>
    {
            public string Name { get; protected set; }

            protected Player(string name)
            {
                Name = name;
            }

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
            public int GetHashCode(Player player) => Name.GetHashCode();
    }
}
