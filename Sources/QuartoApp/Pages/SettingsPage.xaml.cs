using QuartoApp.Resources.Localization;
using QuartoApp.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace QuartoApp.Pages;

public partial class SettingsPage : ContentPage, INotifyPropertyChanged
{
    public App? CurrentApp
        => App.Current as App;

    public ImageSource? BackgroundImage
    {  
        get => CurrentApp?.GlobalBackgroundImage as ImageSource;
        set
        {
                if (CurrentApp != null && value != null)
                    CurrentApp.GlobalBackgroundImage = value;
        }
    }

    public SettingsPage()
	{
		InitializeComponent();
        InitializeCulturePicker();

        BindingContext = this;
        if (CurrentApp?.BackgroundPlayer != null)
            MySlider.Value = CurrentApp.BackgroundPlayer.Volume * 100;

        if (CurrentApp != null)
            CurrentApp.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CurrentApp.GlobalBackgroundImage))
                    OnPropertyChanged(nameof(BackgroundImage));
            };
    }

    private void OnVolumeChanged(object sender, ValueChangedEventArgs e)
    {
        // Le volume de IAudioPlayer est entre 0.0 et 1.0
        var app = Application.Current as App;
        if (app?.BackgroundPlayer != null)
        {
            app.BackgroundPlayer.Volume = (float)(e.NewValue / 100.0);
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;
    protected new void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnThemeButtonPressed(object sender, TappedEventArgs e)
    {
        // put background in transparent
        Simple.BackgroundColor = Colors.Transparent;
        Modern.BackgroundColor = Colors.Transparent;
        Colored.BackgroundColor = Colors.Transparent;

        var button = sender as CustomButton2;
        if (button != null)
        {
            button.BackgroundColor = Colors.Gray;
        }

        // attribute the difficukty to BackgroundImage
        if (button == Simple)
        {
            BackgroundImage = "simple.png";
        }
        else if (button == Modern)
        {
            BackgroundImage = "modern.png";
        }
        else if (button == Colored)
        {
            BackgroundImage = "colored.png";
        }
    }

    private void InitializeCulturePicker()
    {
        string savedCulture = Preferences.Default.Get("Culture", "fr-FR");

        CulturePicker.SelectedItem = savedCulture;
    }

    private void CulturePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedCulture = CulturePicker.SelectedItem?.ToString() ?? "fr-FR";
        Preferences.Default.Set("Culture", selectedCulture);

        // Met à jour la culture du thread
        var culture = new CultureInfo(selectedCulture);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }


    public async void Rules_Tapped(object sender, TappedEventArgs e)
    {
        string savedCulture = Preferences.Default.Get("Culture", "fr-FR");
        if (savedCulture.StartsWith("en"))
            await Navigation.PushAsync(new RulesPageEn());
        else
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