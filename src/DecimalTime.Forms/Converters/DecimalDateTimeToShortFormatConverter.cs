﻿using System;
using System.Globalization;
using DecimalTime.Core.Utils;
using Xamarin.Forms;

namespace DecimalTime.Core.Converters
{
    public class DecimalDateTimeToShortFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalDateTime = value as Pallettaro.Revo.DateTime;

            return decimalDateTime?.ToString(FormatSettings.ShortFormat) ?? null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
