using System;
using System.Globalization;

// https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/
namespace DecimalTime.Core.Utils
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
