using Manager;
using Model;
using QuartoApp.Views;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
namespace QuartoApp.Pages;

public partial class LoginPage : ContentPage
{
    public App? CurrentApp 
        => App.Current as App;

    public string? BackupFile
        => CurrentApp?.BackupFileName as string;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager 
        => CurrentApp?.GameManager as GameManager;

	public string? Entry_name1 { get; set; }
    public string? Entry_name2 { get; set; }
    private IPlayer[] players = new IPlayer[2];
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
        if (string.IsNullOrEmpty(BackupFile))
        {
            await DisplayAlert("Erreur", "Aucune sauvegarde trouvée.", "OK");
            return;
        }

        try
        {
            //récupère le fichier
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Impossible de charger la partie ", "OK");
        }
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