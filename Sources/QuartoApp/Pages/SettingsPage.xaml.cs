namespace QuartoApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    public async void Rules_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RulesPage());
    }

    public async void Score_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new ScorePage());
    }

    public async void Credit_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new CreditsPage());
    }

    public async void Quit_Tapped(Object sender,  TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}