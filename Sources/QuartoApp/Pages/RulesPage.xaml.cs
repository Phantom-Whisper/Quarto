namespace QuartoApp.Pages;

public partial class RulesPage : ContentPage
{
	public RulesPage()
	{
		InitializeComponent();
	}

	public async void Return_Tapped(Object sender, TappedEventArgs e)
	{
		await Navigation.PopAsync();
	}
}