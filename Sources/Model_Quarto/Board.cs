using Manager;
using System.ComponentModel;
using System.Text;

namespace Model
{
    /// <summary>
    /// Represents a 2D board that holds game pieces in cells.
    /// Notifies observers when a piece is inserted via INotifyPropertyChanged.
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// Maximum board size constant.
        /// </summary>
        private const int MAXSIZE = 4;

        /// <summary>
        /// The backing grid of cells.
        /// </summary>
        private readonly ICell[,] grid;

        /// <summary>
        /// Event to notify when a property changes, used for data binding.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Initializes a new 4x4 board.
        /// </summary>
        public Board() : this(MAXSIZE, MAXSIZE) { }

        /// <summary>
        /// Initializes a board of specified size.
        /// </summary>
        /// <param name="row">Number of rows.</param>
        /// <param name="col">Number of columns.</param>
        /// <exception cref="ArgumentException">Thrown when size is not 4x4.</exception>
        public Board(int row, int col)
        {
            if (row != MAXSIZE || col != MAXSIZE)
                throw new ArgumentException($"The maximum allowed board size is {MAXSIZE}x{MAXSIZE}");

            grid = new ICell[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grid[i, j] = new Cell();
                }
            }
        }

        /// <inheritdoc/>
        public int SizeX => grid.GetLength(0);

        /// <inheritdoc/>
        public int SizeY => grid.GetLength(1);

        /// <inheritdoc/>
        public ICell[,] Grid => grid;

        /// <inheritdoc/>
        public void InsertPiece(IPiece piece, int row, int col)
        {
            if (!IsOnBoard(row, col))
                throw new InvalidOperationException($"Position ({row}, {col}) is outside the board bounds.");

            if (!IsEmpty(row, col))
                throw new InvalidOperationException($"Cell ({row}, {col}) is already occupied.");

            grid[row, col].Piece = piece;
            OnPropertyChanged($"Grid[{row},{col}]");
        }

        /// <inheritdoc/>
        public bool IsEmpty(int row, int col)
        {
            return IsOnBoard(row, col) && grid[row, col].IsEmpty;
        }

        /// <inheritdoc/>
        public IPiece GetPiece(int row, int col)
        {
            if (!IsOnBoard(row, col))
                throw new ArgumentException("Coordinates are outside the board.");
            return grid[row, col].Piece!;
        }

        /// <inheritdoc/>
        public bool IsOnBoard(int row, int col)
        {
            return row >= 0 && col >= 0 && row < SizeX && col < SizeY;
        }

        /// <inheritdoc/>
        public bool IsBoardFull()
        {
            for (int row = 0; row < SizeX; row++)
                for (int col = 0; col < SizeY; col++)
                    if (IsEmpty(row, col))
                        return false;
            return true;
        }

        /// <inheritdoc/>
        public int PositionXPiece(IPiece piece)
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    if (grid[i, j].Piece == piece)
                        return i;

            throw new InvalidOperationException("Piece not found on the board.");
        }

        /// <inheritdoc/>
        public int PositionYPiece(IPiece piece)
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    if (grid[i, j].Piece == piece)
                        return j;

            throw new InvalidOperationException("Piece not found on the board.");
        }

        /// <inheritdoc/>
        public (int row, int col) PositionPiece(IPiece piece)
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    if (grid[i, j].Piece == piece)
                        return (i, j);

            throw new InvalidOperationException("Piece not found on the board.");
        }

        /// <inheritdoc/>
        public List<IPiece> GetAllPieces()
        {
            var list = new List<IPiece>();
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    if (!grid[i, j].IsEmpty && grid[i, j].Piece != null)
                        list.Add(grid[i, j].Piece!);
            return list;
        }

        /// <inheritdoc/>
        public IEnumerable<List<IPiece>> CombinationsOf4(List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count < 4)
                yield break;

            int n = pieces.Count;
            for (int i = 0; i < n - 3; i++)
                for (int j = i + 1; j < n - 2; j++)
                    for (int k = j + 1; k < n - 1; k++)
                        for (int l = k + 1; l < n; l++)
                            yield return new List<IPiece> { pieces[i], pieces[j], pieces[k], pieces[l] };
        }

        /// <inheritdoc/>
        public void ClearBoard()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    grid[i, j].Piece = null;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for a property.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("     ");
            for (int col = 0; col < SizeY; col++)
                sb.AppendFormat("{0,5} ", col);
            sb.AppendLine();

            sb.AppendLine(new string('-', 6 + SizeY * 6));

            for (int row = 0; row < SizeX; row++)
            {
                sb.AppendFormat("{0,3} |", row);
                for (int col = 0; col < SizeY; col++)
                {
                    sb.AppendFormat("{0,5} ", grid[row, col].Piece?.ToString() ?? ".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}