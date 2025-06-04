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
    public App? CurrentApp 
        => App.Current as App;

    public ImageSource? BackgroundImage 
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager
        => CurrentApp?.GameManager as GameManager;

    public Board hand { get; } = new Board(4,4);

    private readonly ObservableCollection<IPiece?> _flatMatrix = [];

    public GamePage()
    {
        InitializeComponent();
        FillBagMatrix();
        BindingContext = this;
    }

    public IEnumerable<IPiece> FlatMatrix2d
    {
        get
        {
            List<IPiece> flatMatrix = new();

            if (GameManager!.Board!.Grid == null) return flatMatrix;

            IPiece[,] mat = new IPiece[GameManager.Board.SizeX, GameManager.Board.SizeY];
            for (int numRow = 0; numRow < GameManager.Board.SizeX; numRow++)
            {
                for (int numCol = 0; numCol < GameManager.Board.SizeY; numCol++)
                {
                    flatMatrix.Add(GameManager.Board.Grid[numRow, numCol]);
                }
            }
            return flatMatrix;
        }
    }

    public IEnumerable<IPiece> FlatMatrixBag
    {
        get
        {
            List<IPiece> matrix = new();

            if (GameManager!.Bag!.Baglist == null) return matrix;

            IPiece[,] mat = new IPiece[hand.SizeX, hand.SizeY];
            for (int numRow = 0; numRow < hand.SizeX; numRow++)
            {
                for(int numCol = 0; numCol < hand.SizeY; numCol++)
                    matrix.Add(hand.Grid[numRow, numCol]);
            }
            return matrix;
        }
    }

    public void FillBagMatrix()
    {
        int index = 0;
        for (int i = 0; i < hand.SizeX; i++)
        {
            for (int j = 0; j < hand.SizeY; j++)
            {
                if (index < GameManager!.Bag!.Baglist.Count)
                {
                    hand.InsertPiece(GameManager!.Bag!.Baglist[index], i, j);
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