using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Board
    {
        public Board(int x, int y) 
        {
            SizeX = x;
            SizeY = y;
            grid = new Piece[SizeX, SizeY];
        }

        public int SizeX
        {
            get;
        }

        public int SizeY
        {
            get;
        }

        public readonly Piece[,] grid;

        public void InsertPiece(Piece piece, int x, int y)
        {
            if (IsEmpty(x, y) && IsOnBoard(x, y))
                grid[x, y] = piece;
            else
                throw new InvalidOperationException("The piece cannot be placed in this position.");
        }

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

        public bool IsEmpty(int x, int y)
        {
            if (grid[x, y] == null)
                return true;
            return false;
        }

        public Piece GetPiece(int x, int y) => grid[x, y];

        public bool IsOnBoard(int x, int y)
        {
            if (x < 0 || y < 0 || x > SizeX || y > SizeY)
                return false;
            return true;
        }

        public void ClearBoard()
        {
            Array.Clear(grid, 0, grid.Length);
        }

    }
}
