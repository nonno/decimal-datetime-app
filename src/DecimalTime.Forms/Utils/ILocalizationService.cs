using System;
using System.Globalization;

// https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/
namespace DecimalTime.Forms.Utils
{
    public interface ILocalizationService
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
