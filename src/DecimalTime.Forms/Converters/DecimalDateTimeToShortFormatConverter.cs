using System;
using System.Globalization;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Converters
{
    public class DecimalDateTimeToShortFormatConverter : IValueConverter
    {
        private SettingsProvider _settingsProvider;

        public DecimalDateTimeToShortFormatConverter(SettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalDateTime = value as Pallettaro.Revo.DateTime;

            return decimalDateTime?.ToString(_settingsProvider.ShortFormat) ?? null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
