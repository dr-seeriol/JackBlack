using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace JackBlack
{
    public class CardToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Card card)
                return Binding.DoNothing;   

            string imageFilename = GetImageFilename(card);
            string imagePath =
                $"pack://application:,,,/JackBlack;component/CardImages/{imageFilename}";
            return new BitmapImage(new Uri(imagePath));
        }

        private static string GetImageFilename(Card card) =>
            $"p{((int)card.Suit * 13 + (int)card.Rank) % 52}.png"; // Had to get help on this one because we didn't learn modulo

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
