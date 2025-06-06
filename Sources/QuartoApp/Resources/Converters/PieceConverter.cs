using System.Globalization;

namespace QuartoApp.Resources.Converters
{
    public class PieceConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string? filename;
            if (value == null) return null;
            if (value is Model.Board.Cell cell)
                filename = cell.Piece!.ToString() + ".png";
            else
                filename = value.ToString() + ".png"; 
            return ImageSource.FromFile(filename);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
