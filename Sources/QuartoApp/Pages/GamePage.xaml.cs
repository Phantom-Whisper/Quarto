namespace QuartoApp.Pages;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;
using Manager;
using Model;
using QuartoApp.MyLayouts;

public partial class GamePage : ContentPage, INotifyPropertyChanged
{
    public App? CurrentApp => App.Current as App;
    public ImageSource? BackgroundImage => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public Board board { get; } = new Board(4, 4);
    public Bag bag { get; } = new Bag();

    private readonly ObservableCollection<IPiece?> _flatMatrix = [];

    public GamePage()
    {
        InitializeComponent();
        TestFillBoard();
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

    public void TestFillBoard()
    {
        int index = 0;
        for (int i = 0; i < board.SizeX; i++)
        {
            for (int j = 0; j < board.SizeY; j++)
            {
                if (index < bag.Baglist.Count)
                {
                    board.InsertPiece(bag.Baglist[index], i, j);
                    index++;
                }
                else
                {
                    // No more pieces left in bag
                    return;
                }
            }
        }
    }

    public async void Settings_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}