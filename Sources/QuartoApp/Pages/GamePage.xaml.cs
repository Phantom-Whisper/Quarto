namespace QuartoApp.Pages;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;
using Manager;
using Manager.CustomEventArgs;
using Model;
using QuartoApp.MyLayouts;
using QuartoApp.Views;

public partial class GamePage : ContentPage, INotifyPropertyChanged
{
    public App? CurrentApp 
        => App.Current as App;

    public ImageSource? BackgroundImage 
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager
        => CurrentApp?.GameManager as GameManager;

    public Board Hand { get; } = new Board(4,4); // Pour afficher le bag correctement

    public GamePage()
    {
        InitializeComponent();
        FillBagMatrix();
        BindingContext = this;

        //GameManager!.AskCoordinate += OnAskCoordinate;
    }

    public void FillBagMatrix()
    {
        int index = 0;
        for (int i = 0; i < Hand.SizeX; i++)
        {
            for (int j = 0; j < Hand.SizeY; j++)
            {
                if (index < GameManager!.Bag!.Baglist.Count)
                {
                    Hand.InsertPiece(GameManager!.Bag!.Baglist[index], i, j);
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

    private void Piece_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is IPiece clickedPiece)
        {
            Debug.WriteLine($"Pièce sélectionnée : {clickedPiece}");
            GameManager!.PieceToPlay = clickedPiece;
            GameManager!.Bag!.Remove(clickedPiece);
        }
    }

    private void Pose_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is Model.Cell cell && GameManager!.PieceToPlay != null)
        {
            GameManager!.Board!.InsertPiece(GameManager!.PieceToPlay!, cell.X, cell.Y);
            GameManager!.PieceToPlay = null;
        }
    }

    private async void Return_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//Accueil");
    }
}