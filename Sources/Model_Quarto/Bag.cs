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

        private ObservableCollection<Piece> baglist;

        public ObservableCollection<Piece> Baglist
        {
            get => baglist ??= new ObservableCollection<Piece>();
            set => baglist = value ?? new ObservableCollection<Piece>();
        }

        public void AddPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Baglist.Add(new Piece(isSquare,isLight,isBig,isFull));
        }

        public void RemovePiece(Piece piece)
        {
            if (baglist != null)
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
