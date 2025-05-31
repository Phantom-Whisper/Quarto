namespace QuartoApp.Pages;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}

    public async void Settings_Clicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }
}