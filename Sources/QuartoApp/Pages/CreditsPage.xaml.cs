namespace QuartoApp.Pages;

public partial class CreditsPage : ContentPage
{
    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;

    public CreditsPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

    private async void Return_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
    }
}