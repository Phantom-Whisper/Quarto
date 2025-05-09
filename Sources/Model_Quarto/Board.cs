using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Board
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Board</c>.
        /// </summary>
        /// <param name="x">Number of cells in the x-axis.</param>
        /// <param name="y">Number of cells in the y-axis.</param>
        public Board(int x, int y) 
        {
            SizeX = x;
            SizeY = y;
            grid = new Piece[SizeX, SizeY];
        }

        /// <summary>
        /// This property contains the number of cells in the x-axis.
        /// </summary>
        public int SizeX
        {
            get;
        }

        /// <summary>
        /// This property contains the number of cells in the y-axis.
        /// </summary>
        public int SizeY
        {
            get;
        }

        /// <summary>
        /// A grid of <c>Piece</c> making the <c>Board</c>.
        /// </summary>
        public readonly Piece[,] grid;

        /// <summary>
        /// This method insert a <c>Piece</c> at a certain position on the <c>Board</c>.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="x">Position on the x-axis.</param>
        /// <param name="y">Position on the y-axis.</param>
        /// <exception cref="InvalidOperationException"> when the <c>Piece</c> can't be placed in the position </exception>
        public void InsertPiece(Piece piece, int x, int y)
        {
            if (IsEmpty(x, y) && IsOnBoard(x, y))
                grid[x, y] = piece;
            else
                throw new InvalidOperationException("The piece cannot be placed in this position.");
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current state of the <c>Board</c>.
        /// </returns>
        public override string ToString()
        {
            string str = "";
            int z = 0, x = 0;
            for (int i = -1; i < SizeY; i++)
            {
                if (i == -1)
                {
                    str += "x/y";
                }
                else
                {
                    if (i < 10)
                    {
                        str += "  " + i + "  ";
                    }
                    else
                    {
                        str += "  " + i + " ";
                    }
                }
                str += " | ";
            }
            str += "\n";
            for (int y = 0; y < SizeX; y++)
            {
                str += "--------";
            }
            str += "\n";
            str += z + "   |";
            foreach (var piece in grid)
            {
                if (x == SizeX)
                {
                    x = 0;
                    str += "\n";
                    for (int y = 0; y < SizeX; y++)
                    {
                        str += "--------";
                    }
                    str += "\n";
                    z++;
                    if (z < 10)
                    {
                        str += z + "   |";
                    }
                    else
                    {
                        str += z + "  |";
                    }
                }

                if (piece != null)
                {
                    str += piece.ToString();
                }
                else
                {
                    str += "     ";
                }

                str += " | ";
                x++;
            }
            return str;
        }

        /// <summary>
        /// This method tells if the cell at a certain position is empty or not
        /// </summary>
        /// <param name="x">Position on the x-axis</param>
        /// <param name="y">Position on the y-axis</param>
        /// <returns> a boolean : true = the <c>Board</c> is empty and false = the <c>Board</c> is not empty </returns>
        public bool IsEmpty(int x, int y)
        {
            if (grid[x, y] == null)
                return true;
            return false;
        }

        /// <summary>
        /// This method returns a <c>Piece</c> at a specific position on the <c>Board</c>
        /// </summary>
        /// <param name="x">Position on the x-axis</param>
        /// <param name="y">Position on the y-axis</param>
        /// <returns> piece wanted </returns>
        public Piece GetPiece(int x, int y) => grid[x, y];

        /// <summary>
        /// This method tells if the coordinates are within the limits of the <c>Board</c>
        /// </summary>
        /// <param name="x">Position on the x-axis</param>
        /// <param name="y">Position on the y-axis</param>
        /// <returns> boolean : true = is on the board and false = is out of the board </returns>
        public bool IsOnBoard(int x, int y)
        {
            if (x < 0 || y < 0 || x > SizeX || y > SizeY)
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
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    if (IsEmpty(x, y) == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int PositionXPiece(Piece piece)
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

        public int PositionYPiece(Piece piece)
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

        public (int x, int y) PositionPiece(Piece piece)
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
