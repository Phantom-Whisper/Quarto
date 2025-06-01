using Manager;
using System.ComponentModel;

namespace Model
{
    /// <summary>
    /// Represents a cell on the board which may contain a piece.
    /// Implements INotifyPropertyChanged to allow UI updates when the cell changes.
    /// </summary>
    public class Cell : ICell, INotifyPropertyChanged
    {
        private IPiece? _piece;

        /// <inheritdoc/>
        public IPiece? Piece
        {
            get => _piece;
            set
            {
                if (_piece != value)
                {
                    _piece = value;
                    OnPropertyChanged(nameof(Piece));
                    OnPropertyChanged(nameof(IsEmpty));
                }
            }
        }

        /// <inheritdoc/>
        public bool IsEmpty => _piece == null;

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns a string that represents the current cell.
        /// </summary>
        public override string ToString()
        {
            return _piece?.ToString() ?? ".";
        }
    }
}
