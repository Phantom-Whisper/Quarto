namespace QuartoApp.Pages;

public partial class RulesPage : ContentPage
{
    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage
        => CurrentApp?.GlobalBackgroundImage as ImageSource;
    public RulesPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	public async void Return_Tapped(Object sender, TappedEventArgs e)
	{
		await Navigation.PopAsync();
	}
}