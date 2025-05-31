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
}