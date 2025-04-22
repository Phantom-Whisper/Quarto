using System.Collections;
using System.Collections.ObjectModel;

namespace Model
{
    public class Bag
    {
        public Bag()
        {
            Baglist = new ObservableCollection<Piece>();
        }

        public ObservableCollection<Piece> Baglist { get; private set; }

        public void AddPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Piece newPiece = new Piece(isSquare, isLight, isBig, isFull);

            if (!Baglist.Contains(newPiece))
            {
                Baglist.Add(newPiece);
            }
            else
            {
                throw new InvalidOperationException("This piece already exist in the bag.");
            }
        }

        public void RemovePiece(Piece piece)
        {
            ArgumentNullException.ThrowIfNull(piece, nameof(piece));

            if (!Baglist.Contains(piece))
            {
                throw new InvalidOperationException("The piece is not in the bag");
            }

            Baglist.Remove(piece);
        }

    }
}
