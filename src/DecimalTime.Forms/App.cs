﻿using System;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms
{
    public class App : Application
    {
        public App()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android) {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                i18n.AppStrings.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new MainPage();
        }
    }
}
