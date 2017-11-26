using System;
using Acr.UserDialogs;
using DecimalTime.Core.Pages;
using DecimalTime.Core.Utils;
using MvvmCross.Forms.Platform;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace DecimalTime.Core
{
    public class App : MvxFormsApplication
    {
        public App()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android) {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                i18n.AppStrings.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            MainPage = new MainPage();
        }
    }
}
