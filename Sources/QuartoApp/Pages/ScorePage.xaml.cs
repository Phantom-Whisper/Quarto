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

        Scores = [.. stubScores.GetAllScores()
                .Select(kvp => new PlayerScore(kvp.Key.Name, kvp.Value))];

        BindingContext = this;
    }
}