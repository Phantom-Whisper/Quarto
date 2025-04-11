using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Thid method insert a piece at a certain position on the board.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="x">Position on the x-axis.</param>
        /// <param name="y">Position on the y-axis.</param>
        /// <exception cref="InvalidOperationException"></exception>
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
        /// <returns></returns>
        public bool IsEmpty(int x, int y)
        {
            if (grid[x, y] == null)
                return true;
            return false;
        }

        /// <summary>
        /// This method returns a piece at a specific position on the board
        /// </summary>
        /// <param name="x">Position on the x-axis</param>
        /// <param name="y">Position on the y-axis</param>
        /// <returns></returns>
        public Piece GetPiece(int x, int y) => grid[x, y];

        /// <summary>
        /// This method tells if the coordinates are within the limits of the board
        /// </summary>
        /// <param name="x">Position on the x-axis</param>
        /// <param name="y">Position on the y-axis</param>
        /// <returns></returns>
        public bool IsOnBoard(int x, int y)
        {
            if (x < 0 || y < 0 || x > SizeX || y > SizeY)
                return false;
            return true;
        }

        /// <summary>
        /// This method removes the board of all the <c>pieces</c> on it
        /// </summary>
        public void ClearBoard()
        {
            Array.Clear(grid, 0, grid.Length);
        }

    }
}
