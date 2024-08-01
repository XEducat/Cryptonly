using System.Globalization;
using System.Windows.Data;

namespace Cryptonly
{
    /// <summary>
    /// Конвертор ваоби
    /// </summary>
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            string symbol = parameter as string;
            return string.Format("{0}{1}", symbol, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
