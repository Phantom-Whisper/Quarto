namespace QuartoApp.Pages;

using System.Data.Common;
using System.Text.Json;
using Manager;
using Model;
using QuartoApp.MyLayouts;

public partial class GamePage : ContentPage
{
    public Board board { get; } = new Board(4, 4);
    public GamePage()
    {
        InitializeComponent();
        BindingContext = this;
    }
    public IEnumerable<IPiece> FlatMatrix2d
    {
        get
        {
            List<IPiece> flatMatrix = new();

            if (board.Grid == null) return flatMatrix;

            IPiece[,] mat = new IPiece[board.SizeX, board.SizeY];
            for (int numRow = 0; numRow < board.SizeX; numRow++)
            {
                for (int numCol = 0; numCol < board.SizeY; numCol++)
                {
                    flatMatrix.Add(board.Grid[numRow, numCol]);
                }
            }
            return flatMatrix;
        }
    }
}