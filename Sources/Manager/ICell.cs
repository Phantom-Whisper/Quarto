using System.ComponentModel;

namespace Manager
{
    /// <summary>
    /// Represents a single cell on the board that may or may not contain a piece.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Gets or sets the piece in the cell.
        /// </summary>
        IPiece? Piece { get; set; }

        /// <summary>
        /// Gets whether the cell is currently empty.
        /// </summary>
        bool IsEmpty { get; }
    }
}
