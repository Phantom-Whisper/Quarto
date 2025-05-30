namespace QuartoApp.Pages;

public partial class DefeatPage : ContentPage
{
	public DefeatPage()
	{
		InitializeComponent();
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