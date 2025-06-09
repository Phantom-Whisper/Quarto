using Manager;
using Model;
using QuartoApp.Views;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text.Json;
namespace QuartoApp.Pages;

public partial class LoginPage : ContentPage
{
    public App? CurrentApp 
        => App.Current as App;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager 
        => CurrentApp?.GameManager as GameManager;

	public string? Entry_name1 { get; set; }
    public string? Entry_name2 { get; set; }
    private readonly IPlayer[] players = new IPlayer[2];
    private string? selectedDifficulty;

    public LoginPage()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public async void Return_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnDiffButtonPressed(object sender, TappedEventArgs e)
    {
        // put background in transparent
        Easy.BackgroundColor = Colors.Transparent;
        Normal.BackgroundColor = Colors.Transparent;
        Advanced.BackgroundColor = Colors.Transparent;

        // put background in Gray
        var button = sender as CustomButton2;
        if (button != null)
        {
            button.BackgroundColor = Colors.Gray;
        }

        // attribute the difficukty to selectedDifficulty
        if (button == Easy)
        {
            selectedDifficulty = "Easy";
        }
        else if (button == Normal)
        {
            selectedDifficulty = "Normal";
        }
        else if (button == Advanced)
        {
            selectedDifficulty = "Advanced";
        }
    }

    public async void OnLoadGameClicked(object sender, TappedEventArgs e)
    {
        try
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Quarto", "Data", "SaveGame.json");

            if (!File.Exists(path))
            {
                await DisplayAlert("Erreur", "Aucune sauvegarde trouvée.", "OK");
                return;
            }

            string json = File.ReadAllText(path);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            GameState loadedState = JsonSerializer.Deserialize<GameState>(json, options);

            if (loadedState == null)
            {
                await DisplayAlert("Erreur", "La sauvegarde est invalide.", "OK");
                return;
            }

            // Crée un GameManager simple depuis le GameState chargé
            var gm = CreateGameManagerFromGameState(loadedState);

            // Assigne au CurrentApp
            if (CurrentApp != null)
            {
                CurrentApp.GameManager = gm;
            }

            // Navigue vers la page de jeu
            await Shell.Current.GoToAsync("//Game");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Impossible de charger la partie : {ex.Message}", "OK");
        }
    }

    private GameManager CreateGameManagerFromGameState(GameState state)
    {
        var board = new Board(4, 4);

        if (state.Board != null)
        {
            for (int i = 0; i < state.Board.Length; i++)
            {
                for (int j = 0; j < state.Board[i].Length; j++)
                {
                    var piece = state.Board[i][j];
                    if (piece != null)
                        board.InsertPiece(piece, i, j);
                }
            }
        }

        IPlayer[] players = state.Players?.Select(p =>
        {
            if (p.Type == "Dumb AI")
                return (IPlayer)new DumbAIPlayer();
            else
                return new HumanPlayer(p.Name);
        }).ToArray() ?? Array.Empty<IPlayer>();

        var bag = state.Bag ?? new Bag();
        var rules = state.Rules ?? new Rules();
        var scoreManager = new ScoreManager();

        var gm = new GameManager(rules, scoreManager, board, bag, players);
        gm.TurnNumber = state.Turn;
        gm.PieceToPlay = state.CurrentPiece;

        gm.SetCurrentPlayerByName(state.CurrentPlayerName ?? players.FirstOrDefault()?.Name ?? "");

        return gm;
    }



    public async void OnTappedStart(object sender, TappedEventArgs e)
    {
        IBoard board = new Board();
        IBag bag = new Bag();
        IScoreManager scoreManager = new ScoreManager();

        if (string.IsNullOrWhiteSpace(Entry_name2))
        {
            if (string.IsNullOrWhiteSpace(Entry_name1))
            {
                Entry_name1 = "Player1";
            }
            players[0] = new HumanPlayer(Entry_name1!);
            players[1] = new DumbAIPlayer();
        }
        else
        {
            if(Entry_name1 == Entry_name2)
            {
                _ = DisplayAlert("Erreur", "Veuillez entrer des pseudos différents.", "OK");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                if (i == 0 && string.IsNullOrWhiteSpace(Entry_name1))
                {
                    Entry_name1 = $"Player{i + 1}";
                }

                if (i == 1 && string.IsNullOrWhiteSpace(Entry_name2))
                {
                    Entry_name2 = $"Player{i + 1}";
                }
            }
            players[0] = new HumanPlayer(Entry_name1!);
            players[1] = new HumanPlayer(Entry_name2!);
        }

        if (selectedDifficulty == "Easy")
        {
            if (CurrentApp != null)
            {
                CurrentApp.GameManager = new GameManager(new RulesBeginner(), scoreManager, board, bag, players);
            }
        }
        else if (selectedDifficulty == "Normal")
        {
            if (CurrentApp != null)
            {
                CurrentApp.GameManager = new GameManager(new Rules(), scoreManager, board, bag, players);
            }
        }
        else if (selectedDifficulty == "Advanced")
        {
            if (CurrentApp != null)
            {
                CurrentApp.GameManager = new GameManager(new RulesAdvanced(), scoreManager, board, bag, players);
            }
        }
        else
        {
            _ = DisplayAlert("Erreur", "Veuillez entrer la difficulté.", "OK");
            return;
        }

        await Shell.Current.GoToAsync("//Game");
    }
}