using QuartoApp.Resources.Localization;
using System.Globalization;

namespace QuartoApp.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    void Picker_SelectedIndexChanged(object source, EventArgs args)
    {
        string? chosenCultureString = picker.SelectedItem as string;

        CultureInfo chosenCulture = chosenCultureString switch
        {
            "en" => new CultureInfo("en"),
            _ => new CultureInfo("fr")
        };

        AppResources.Culture = chosenCulture;
    }
}