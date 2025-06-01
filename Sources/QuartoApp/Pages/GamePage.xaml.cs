namespace QuartoApp.Pages;

public partial class GamePage : ContentPage
{
    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public GamePage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    public async void Settings_Clicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}