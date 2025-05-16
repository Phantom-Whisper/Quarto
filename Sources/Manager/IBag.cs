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

        ReadOnlyObservableCollection<IPiece> Baglist { get; }
    }
}
