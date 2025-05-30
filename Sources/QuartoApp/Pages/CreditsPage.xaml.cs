namespace QuartoApp.Pages;

public partial class CreditsPage : ContentPage
{
	public CreditsPage()
	{
		InitializeComponent();
	}

    private async void Return_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}