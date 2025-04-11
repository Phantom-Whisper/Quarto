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

        public bool IsEmpty()
        {
            return !Baglist.Any();
        }

        public void AddPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Baglist.Add(new Piece(isSquare,isLight,isBig,isFull));
        }

        public void RemovePiece(Piece piece)
        {
            if (Baglist != null)
            {
                Baglist.Remove(piece);
            }
            else
            {
                throw new InvalidOperationException("La pièce n'est pas dans la liste");
            }
            
        }

    }
}
