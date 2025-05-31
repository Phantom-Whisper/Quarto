using System.Collections.ObjectModel;
using Model;
using Serialize;
using Stub;

namespace QuartoApp.Pages;

public partial class ScorePage : ContentPage
{
    public ObservableCollection<PlayerScore> Scores { get; set; }

    public ScorePage()
    {
        InitializeComponent();

        var scoreManager = new ScoreManager();        
        scoreManager.LoadScores();
        Scores = scoreManager.GetAllScores();

        BindingContext = this;
    }

    public async void General_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void Rules_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RulesPage());
    }

    public async void Credit_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CreditsPage());
    }

    public async void Quit_Tapped(Object sender, TappedEventArgs e)
    {
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        await Navigation.PopAsync();
    }
}