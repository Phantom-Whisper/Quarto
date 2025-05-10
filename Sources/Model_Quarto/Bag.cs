using System.Collections;
using System.Collections.ObjectModel;

namespace Model
{
    public class Bag
    {

        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Bag</c>.
        /// </summary>
        public Bag()
        {
            Baglist = new ObservableCollection<Piece>();
        }

        /// <summary>
        /// This property contains the collection of <c>Piece</c> that the player can put on the <c>Board</c>.
        /// </summary>
        public ObservableCollection<Piece> Baglist { get; private set; }

        public bool IsEmpty()
        {
            return !Baglist.Any();
        }

        /// <summary>
        /// This method create and add a <c>Piece</c> to the <c>Bag</c>.
        /// </summary>
        /// <param name="isSquare"> boolean of the shape : true = Square and false = Round </param>
        /// <param name="isLight"> boolean of the color : true = Light and false = Dark </param>
        /// <param name="isBig"> boolean of the size : true = Big and false = Small </param>
        /// <param name="isFull"> boolean of the state : true = Full and false = Hollow </param>
        public void AddPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Piece newPiece = new(isSquare, isLight, isBig, isFull);

            if (!Baglist.Contains(newPiece))
            {
                Baglist.Add(newPiece);
            }
            else
            {
                throw new InvalidOperationException("This piece already exist in the bag.");
            }
        }

        /// <summary>
        /// This method remove a <c>Piece</c> from the <c>Bag</c>.
        /// </summary>
        /// <param name="piece"> <c>Piece</c> removed </param>
        /// <exception cref="InvalidOperationException"> when the piece is not in the collection </exception>
        public void RemovePiece(Piece piece)
        {
            ArgumentNullException.ThrowIfNull(piece);

            if (!Baglist.Remove(piece))
            {
                throw new InvalidOperationException("The piece is not in the bag");
            }

        }

        public Piece GetPiece(int index)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("The bag is empty.");
            }

            if (index < 0 || index >= Baglist.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }
            Piece piece = Baglist[index];
            Baglist.Remove(piece);
            return piece;
        }
    }
}