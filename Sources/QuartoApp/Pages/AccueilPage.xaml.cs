using System.Windows.Input;

namespace QuartoApp.Pages;

public partial class AccueilPage : ContentPage
{
    public AccueilPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public async void Exit_Tapped(object sender, EventArgs e)
    {
        bool confirmation = await DisplayAlert("Alert", "Are you sure you want to exit the game ?", "Yes", "No");
        if (confirmation)
        {
            App.Current?.Quit();
        }
    }

    public async void Rules_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RulesPage());
    }

    public async void Settings_Clicked(Object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    public async void Play_Tapped(Object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}