using Manager;
using System.Collections.ObjectModel;
using System.Text;

namespace Model
{
    /// <summary>
    /// Class of the bag containing the piece available to play on the board
    /// </summary>
    public class Bag : IBag
    {
        /// <summary>
        /// <c>Ctor</c> of the Class <c>Bag</c>.
        /// </summary>
        public Bag()
        {
            Baglist = new ReadOnlyObservableCollection<IPiece>(pieces);
            Init();
        }

        /// <summary>
        /// Contains the collection of <c>Piece</c> that the player can put on the <c>Board</c>.
        /// </summary>
        public ReadOnlyObservableCollection<IPiece> Baglist { get; private set; }

        private readonly ObservableCollection<IPiece> pieces = new();

        /// <summary>
        /// Checks if the <c>Bag</c>
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return !Baglist.Any();
        }

        /// <summary>
        /// Adds a <c>Piece</c> object to the <c>Bag</c>.
        /// </summary>
        /// <param name="piece"> The <c>Piece</c> we want to add into the bag </param>
        /// <exception cref="InvalidOperationException"> when the piece is already in the collection </exception>
        public void AddPiece(IPiece piece)
        {
            if (!Baglist.Contains(piece))
            {
                pieces.Add(piece);
            }
            else
            {
                throw new InvalidOperationException("This piece already exist in the bag.");
            }
        }

        /// <summary>
        /// Retrieves and removes a <c>Piece</c> at the specified index from the <c>Bag</c>.
        /// </summary>
        /// <param name="index">The index of the <c>Piece</c> to retrieve and remove.</param>
        /// <returns>The <c>Piece</c> removed from the bag.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
        public IPiece? TakePiece(IPiece? piece)
        {
            ArgumentNullException.ThrowIfNull(piece);
            return pieces.Remove(piece) ? piece : null;
        }

        /// <summary>
        /// Removes a <c>Piece</c> object from the bag
        /// </summary>
        /// <param name="piece"></param>
        public void Remove(IPiece? piece)
        {
            if (piece is not null)
                pieces.Remove(piece);
        }

        /// <summary>
        /// Create every <c>Piece</c> of the game which is added to the <c>Baglist</c>
        /// </summary>
        private void Init()
        {
            List<Piece> listPiece = new List<Piece> {
                new(true, true, true, true),
                new(true, true, true, false),
                new(true, true, false, true),
                new(true, true, false, false),
                new(true, false, true, true),
                new(true, false, true, false),
                new(true, false, false, true),
                new(true, false, false, false),
                new(false, true, true, true),
                new(false, true, true, false),
                new(false, true, false, true),
                new(false, true, false, false),
                new(false, false, true, true),
                new(false, false, true, false),
                new(false, false, false, true),
                new(false, false, false, false)
                };

            foreach (var piece in listPiece)
            {
                AddPiece(piece);
            }
        }

        /// <summary>
        /// Construct a display of all the <c>Piece</c>
        /// </summary>
        /// <returns> String of the list of <c>Piece</c> with their number </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int i = 1;
            foreach (var piece in Baglist)
            { 
                sb.Append($"{i}. {piece.ToString()}\n");
                i++;
            }

            return sb.ToString();
        }
    }
}