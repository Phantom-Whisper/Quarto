using Model;

namespace QuartoApp.Pages;

public partial class WinPage : ContentPage
{
    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GameManager? GameManager
        => CurrentApp?.GameManager as GameManager;

    public WinPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    public async void Replay_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    public async void Credit_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CreditsPage());
    }

    public async void Exit_Tapped(object sender, TappedEventArgs e)
    {
        bool confirmation = await DisplayAlert("Alert", "Are you sure you want to exit the game ?", "Yes", "No");
        if (confirmation)
        {
            App.Current?.Quit();
        }
    }
}