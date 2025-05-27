using System.Collections.ObjectModel;
using Model;
using Stub;

namespace QuartoApp.Pages;

public partial class ScorePage : ContentPage
{
    public ObservableCollection<PlayerScore> Scores { get; set; }

    public ScorePage()
    {
        InitializeComponent();

        var stubScores = new StubPlayerScores();
        var scoreManager = new ScoreManager();

        scoreManager.LoadScores();

        Scores = [.. scoreManager.GetAllScores().Select(static kvp => new PlayerScore(kvp.Key, kvp.Value))];

        BindingContext = this;
    }
}