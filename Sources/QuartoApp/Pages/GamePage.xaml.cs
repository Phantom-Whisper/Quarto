namespace QuartoApp.Pages;
using System.ComponentModel;
using System.Diagnostics;
using Manager;
using Manager.CustomEventArgs;
using Model;
using Plugin.Maui.Audio;
using Serialize;

public partial class GamePage : ContentPage, INotifyPropertyChanged
{
    public GamePage()
    {
        InitializeComponent();
        BindingContext = this;

        _serializer = new GameStateSerializer();

        if (CurrentApp != null)
            CurrentApp.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CurrentApp.GlobalBackgroundImage))
                    OnPropertyChanged(nameof(BackgroundImage));
            };
    }

    public new event PropertyChangedEventHandler? PropertyChanged;
    protected new void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage 
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager
        => CurrentApp?.GameManager as GameManager;

    public IAudioManager AudioManager
        => CurrentApp!._audioManager as IAudioManager;

    public Board Hand { get; } = new Board(4,4); // Pour afficher le bag correctement

    private AskPieceToPlayEventArgs? _pendingAskPieceArgs;

    private IAudioPlayer? _highPlayer;
    private IAudioPlayer? _lowPlayer;

    private readonly GameStateSerializer _serializer;

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _highPlayer ??= AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("high.mp3"));
        _lowPlayer ??= AudioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("low.mp3"));

        if (GameManager != null)
        {
            GameManager.AskPieceToPlay += OnAskPieceToPlay;
            GameManager.BoardChanged += OnBoardChanged;
            GameManager.BagChanged += OnBagChanged;
            GameManager.Quarto += OnQuarto;
            GameManager.GameEnd += OnGameEnd;

            FillBagMatrix();

            await GameManager.Run();
        }

    }

    private async void OnAskPieceToPlay(object? sender, AskPieceToPlayEventArgs e)
    {
        if (GameManager?.CurrentPlayer is DumbAIPlayer)
        {

            var piece = DumbAIPlayer.ChoosePiece(GameManager.Bag!);
            e.PieceToPlay = piece;

            GameManager.Bag!.Remove(piece);
            
            _highPlayer?.Play();
            GameManager.PieceToPlay = piece;

            if (GameManager.PieceSelectionTcs is { Task.IsCompleted: false } tcs)
            {
                tcs.SetResult(piece);
            }
        }
        else
        {
            _pendingAskPieceArgs = e;
        }
    }

    private void OnBoardChanged(object? sender, BoardChangedEventArgs e)
    {
        OnPropertyChanged(nameof(GameManager));
    }

    private void OnBagChanged(object? sender, BagChangedEventArgs e)
    {
        OnPropertyChanged(nameof(GameManager));
    }

    private async void OnQuarto(object? sender, QuartoEventArgs e)
    {
        await DisplayAlert("Quarto !", $"{e.CurrentPlayer.Name} a gagn� avec un Quarto.", "OK");
    }

    private async void OnGameEnd(object? sender, GameEndEventArgs e)
    {
        await DisplayAlert("Fin de la partie", $"Le gagnant est : {e.Winner.Name}", "OK");
        if (GameManager!.HasWinner && GameManager!.CurrentPlayer is HumanPlayer)
            await Shell.Current.GoToAsync("//Win");
        else
            await Shell.Current.GoToAsync("//Defeat");
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
                    return;
                }
            }
        }
    }

    private async void Bag_Clicked(object sender, EventArgs e)
    {
        _highPlayer?.Play();
        if (sender is ImageButton button && button.BindingContext is IPiece clickedPiece && GameManager!.PieceToPlay == null)
        {
            
            GameManager.PieceToPlay = clickedPiece;
            GameManager.Bag?.Remove(clickedPiece);

            if (_pendingAskPieceArgs is not null)
            {
                _pendingAskPieceArgs.PieceToPlay = clickedPiece;
                _pendingAskPieceArgs = null;
            }

            if (GameManager.PieceSelectionTcs is { Task.IsCompleted: false } tcs)
            {
                tcs.SetResult(clickedPiece);
            }
        }
    }

    private async void Board_Clicked(object sender, EventArgs e)
    {
        _lowPlayer?.Play();
        if (sender is ImageButton button && button.BindingContext is Model.Cell cell && GameManager!.PieceToPlay != null)
        {
            
            await GameManager.ExecuteTurn(cell.X, cell.Y);
        }
    }

    public async void Settings_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    public static Piece[][] ConvertToJagged(Piece[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var result = new Piece[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new Piece[cols];
            for (int j = 0; j < cols; j++)
            {
                result[i][j] = grid[i, j];
            }
        }
        return result;
    }

    private GameState CreateGameStateFromManager()
    {
        return new GameState
        {
            Board = ConvertToJagged((Piece[,])(GameManager?.Board?.GetPiecesGrid())!),
            CurrentPlayerName = GameManager?.CurrentPlayer?.Name,
            Players = GameManager?.Players.Select(p => new PlayerData
            {
                Name = p.Name,
                Type = p is DumbAIPlayer ? "Dumb AI" : "Human"
            }).ToList(),
            Bag = GameManager?.Bag as Bag,
            Turn = GameManager?.TurnNumber ?? 0,
            CurrentPiece = GameManager?.PieceToPlay as Piece,
            Rules = GameManager?.RulesManager as Rules
        };
    }

    private async void Exit_Tapped(object sender, TappedEventArgs e)
    {
        bool confirmation = await DisplayAlert("Alert", "Are you sure you want to exit the game ?", "Yes", "No");
        if (!confirmation)
            return;

        try
        {
            var gameState = CreateGameStateFromManager();
            _serializer.Save(gameState);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la sauvegarde : {ex.Message}");
        }

        App.Current?.Quit();
    }
}