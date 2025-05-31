using QuartoApp.Resources.Localization;

namespace QuartoApp.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
	{
		InitializeComponent();
        InitializeCulturePicker();
	}

    private void InitializeCulturePicker()
    {
        string savedCulture = Preferences.Default.Get("Culture", "fr-FR");

        CulturePicker.SelectedItem = savedCulture;
    }
}