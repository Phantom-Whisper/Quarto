namespace QuartoApp.Pages;
using Model;

public partial class GamePage : ContentPage
{
    public Board Matrix { get; } = new Board(4, 4);
    public GamePage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}