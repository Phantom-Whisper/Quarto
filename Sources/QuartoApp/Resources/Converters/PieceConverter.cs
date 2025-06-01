using System.Globalization;

namespace QuartoApp.Resources.Converters
{
    public class PieceConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            // Supposons que tu récupères un nom de fichier selon la pièce
            string? filename = value.ToString(); // exemple

            // Retourne un ImageSource, pas une simple string
            return ImageSource.FromFile(filename);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
