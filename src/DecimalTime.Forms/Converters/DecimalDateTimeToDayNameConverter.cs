using System;
using System.Globalization;
using Xamarin.Forms;

namespace DecimalTime.Core.Converters
{
    public class DecimalDateTimeToDayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalDateTime = value as Pallettaro.Revo.DateTime;

            return decimalDateTime?.DayName ?? null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
