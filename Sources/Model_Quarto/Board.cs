using Manager;
using System.Text;

namespace Model
{
    public class Board : IBoard
    {
        private const int MAXSIZE = 4;

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
    }
}