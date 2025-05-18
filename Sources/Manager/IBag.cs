using System.Collections.ObjectModel;
using System.IO.Pipelines;

namespace Manager
{
    public interface IBag
    {
        /// <summary>
        /// Checks if the <c>Bag</c> object is empty.
        /// </summary>
        /// <returns><c>true</c> if the bag is empty; otherwise, <c>false</c>.</returns>
        bool IsEmpty();

        /// <summary>
        /// Removes a <c>Piece</c> object from the bag
        /// </summary>
        /// <param name="piece"></param>
        void Remove(IPiece? piece);

        ReadOnlyObservableCollection<IPiece> Baglist { get; }
    }
}
