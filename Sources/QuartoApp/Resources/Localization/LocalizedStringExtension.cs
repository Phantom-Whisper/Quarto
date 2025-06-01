using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using QuartoApp.Resources.Localization;

namespace QuartoApp.Resources.Localization;

public class LocalizationApp
{
    public string Culture
    {
        get => culture;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            culture = value;
            LocalizedStringExtension.Culture = culture;
        }
    }
    private string culture = "fr-FR";
}

public class LocalizedStringExtension : IMarkupExtension<string>
{
    public required string Key { get; set; }
    public static string Culture
    {
        get => culture;
        set
        {
            if (culture == value) return;

            try
            {
                var newCulture = new CultureInfo(value);
                culture = value;
                AppRes.Culture = newCulture;
                CultureChanged?.Invoke(null, EventArgs.Empty);
            }
            catch (CultureNotFoundException)
            {
                Debug.WriteLine($"Invalid culture identifier: {value}");
                AppRes.Culture = new CultureInfo("fr-FR");
            }
        }
    }
    private static string culture = "fr-FR";

    public static AppResourcesVM AppRes { get; set; } = new AppResourcesVM();

    public static event EventHandler? CultureChanged;

    public string ProvideValue(IServiceProvider serviceProvider)
    {
        IProvideValueTarget? provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
        CultureChanged += (src, args) =>
        {
            (provideValueTarget?.TargetObject as BindableObject)?.SetValue((provideValueTarget?.TargetProperty as BindableProperty), typeof(AppResources).GetProperty(Key, BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null) as string ?? "");
        };
        return typeof(AppResources).GetProperty(Key, BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null) as string ?? "";
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
    }
}
