namespace QuartoApp.Pages;
using Model;
using QuartoApp.MyLayouts;

public partial class GamePage : ContentPage
{
    public Matrix2d Matrix { get; } = new Matrix2d(4, 4);
    public GamePage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}