using Manager;
using System.Text;

namespace Model
{
    /// <summary>
    /// CLass of the board containing the piece played and their position
    /// </summary>
    public class Board : IBoard
    {
        /// <summary>
        /// Constante of the size of the board
        /// </summary>
        private const int MAXSIZE = 4;

        /// <summary>
        /// Constructor of the board, the size depend on the constant <c>MAXSIZE</c>
        /// </summary>
        public Board() 
        {
            grid = new IPiece[MAXSIZE, MAXSIZE];
        }

        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Board</c>.
        /// </summary>
        /// <param name="row">Number of cells in the row-axis.</param>
        /// <param name="col">Number of cells in the col-axis.</param>
        public Board(int row, int col) 
        {
            if (row != MAXSIZE || col != MAXSIZE)
                throw new ArgumentException($"The maximum alowed size of the board is : {MAXSIZE}*{MAXSIZE}.");
            grid = new IPiece[row, col];
        }

        /// <summary>
        /// This property contains the number of cells in the row-axis.
        /// </summary>
        public int SizeX => grid.GetLength(0);

        /// <summary>
        /// This property contains the number of cells in the col-axis.
        /// </summary>
        public int SizeY => grid.GetLength(1);

        /// <summary>
        /// A grid of <c>Piece</c> making the <c>Board</c>.
        /// </summary>
        private readonly IPiece[,] grid;

        /// <summary>
        /// This property contains the status of the grid
        /// </summary>
        public IPiece[,] Grid
        {
            get
            {
                var copy = new IPiece[grid.GetLength(0), grid.GetLength(1)];
                Array.Copy(grid, copy, grid.Length);
                return copy;
            }
        }

        /// <summary>
        /// This property contains the number of Cells on the board
        /// </summary>
        public int NbCells
        {
            get => SizeX * SizeY;
        }

        /// <summary>
        /// Thid method insert a piece at a certain position on the board.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="row">Position on the row-axis.</param>
        /// <param name="col">Position on the col-axis.</param>
        /// <exception cref="InvalidOperationException"> when the <c>Piece</c> can't be placed in the position </exception>
        public void InsertPiece(IPiece piece, int row, int col)
        {
            if (IsEmpty(row, col) && IsOnBoard(row, col))
                grid[row, col] = piece;
            else
                throw new InvalidOperationException($"The piece cannot be placed in this position ({row},{col}).");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current state of the <c>Board</c>.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            int z = 0, row = 0;

            for (int i = -1; i < SizeY; i++)
            {
                if (i == -1)
                {
                    sb.Append("row/y | ");
                }
                else
                {
                    sb.AppendFormat("{0,4} | ", i); 
                }
            }
            sb.AppendLine();

            string horizontalSeparator = new string('-', (SizeY + 1) * 7); 
            sb.AppendLine(horizontalSeparator);

            sb.AppendFormat("{0,2}  |", z); 

            foreach (var piece in grid)
            {
                if (row == SizeX)
                {
                    row = 0;
                    sb.AppendLine();
                    sb.AppendLine(horizontalSeparator);
                    z++;
                    sb.AppendFormat("{0,2}  |", z);
                }

                sb.AppendFormat("{0,5} |", piece?.ToString() ?? ""); 
                row++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// This method tells if the cell at a certain position is empty or not
        /// </summary>
        /// <param name="row">Position on the row-axis</param>
        /// <param name="col">Position on the col-axis</param>
        /// <returns> a boolean : true = the <c>Board</c> is empty and false = the <c>Board</c> is not empty </returns>
        public bool IsEmpty(int row, int col)
        {
            if (IsOnBoard(row,col) && grid[row, col] == null)
                    return true;
            return false;
        }

        /// <summary>
        /// This method returns a <c>Piece</c> at a specific position on the <c>Board</c>
        /// </summary>
        /// <param name="row">Position on the row-axis</param>
        /// <param name="col">Position on the col-axis</param>
        /// <returns></returns>
        public IPiece GetPiece(int row, int col)
        {
            if (!IsOnBoard(row, col))
                throw new ArgumentException("There's no Piece outside the board.");
            return grid[row, col];
        }

        /// <summary>
        /// This method tells if the coordinates are within the limits of the <c>Board</c>
        /// </summary>
        /// <param name="row">Position on the row-axis</param>
        /// <param name="col">Position on the col-axis</param>
        /// <returns> boolean : true = is on the board and false = is out of the board </returns>
        public bool IsOnBoard(int row, int col)
        {
            if (row < 0 || col < 0 || row >= SizeX || col >= SizeY)
                return false;
            return true;
        }

        /// <summary>
        /// This method removes the <c>Board</c> of all the <c>Pieces</c> on it
        /// </summary>
        public void ClearBoard()
        {
            Array.Clear(grid, 0, grid.Length);
        }

        /// <summary>
        /// This method tells if the board is full by looking at every cells and searching for an empty one
        /// </summary>
        /// <returns>True if the board is full and false if is not </returns>
        public bool IsBoardFull()
        {
            for (int row = 0; row < SizeX; row++)
            {
                for (int col = 0; col < SizeY; col++)
                {
                    if (IsEmpty(row, col))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// method that search the absissa axis of a piece
        /// </summary>
        /// <param name="piece">the piece</param>
        /// <returns>the absissa axis of the piece </returns>
        /// <exception cref="InvalidOperationException"> if the piece is not on the board</exception>
        public int PositionXPiece(IPiece piece)
        {

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (GetPiece(i, j) == piece)
                    {
                        return i;
                    }
                }
            }
            throw new InvalidOperationException("Piece not in the board !");
        }

        /// <summary>
        /// method that search the ordinate axis of a piece
        /// </summary>
        /// <param name="piece">the piece</param>
        /// <returns>the ordinate axis of the piece </returns>
        /// <exception cref="InvalidOperationException"> if the piece is not on the board</exception>
        public int PositionYPiece(IPiece piece)
        {

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (GetPiece(i, j) == piece)
                    {
                        return j;
                    }
                }
            }
            throw new InvalidOperationException("Piece not in the board !");
        }

        /// <summary>
        /// method that search the coordinate of a piece
        /// </summary>
        /// <param name="piece">the piece</param>
        /// <returns> tuple of the coordinate of a piece </returns>
        /// <exception cref="InvalidOperationException"> if the piece is not on the board</exception>
        public (int row, int col) PositionPiece(IPiece piece)
        {

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (GetPiece(i, j) == piece)
                    {
                        return (i, j);
                    }
                }
            }
            throw new InvalidOperationException("Piece not in the board !");
        }

        /// <summary>
        /// Gets all pieces currently on the board.
        /// </summary>
        /// <param name="board">The game board to extract pieces from.</param>
        /// <returns>A list of all pieces currently placed on the board.</returns>
        public List<IPiece> GetAllPieces()
        {
            var pieces = new List<IPiece>();
            for (int row = 0; row < SizeX; row++)
            {
                for (int col = 0; col < SizeY; col++)
                {
                    var piece = GetPiece(row, col);
                    if (piece != null)
                    {
                        pieces.Add(piece);
                    }
                }
            }
            return pieces;
        }

        /// <summary>
        /// Generates all unique combinations of 4 pieces from a given list.
        /// </summary>
        /// <param name="pieces">The list of pieces to generate combinations from.</param>
        /// <returns>An enumerable of lists, each containing exactly 4 pieces.</returns>
        public IEnumerable<List<IPiece>> CombinationsOf4(List<IPiece>? pieces)
        {
            if (pieces == null || pieces.Count < 4)
                yield break;

            int n = pieces.Count;

            for (int i = 0; i < n - 3; i++)
            {
                for (int j = i + 1; j < n - 2; j++)
                {
                    for (int k = j + 1; k < n - 1; k++)
                    {
                        for (int l = k + 1; l < n; l++)
                        {
                            yield return new List<IPiece> { pieces[i], pieces[j], pieces[k], pieces[l] };
                        }
                    }
                }
            }
        }
    }
}