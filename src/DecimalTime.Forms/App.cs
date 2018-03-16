﻿using System;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms
{
    public class App : Application
    {
        public App()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android) {
                var ci = IoC.Localization.GetCurrentCultureInfo();
                i18n.AppStrings.Culture = ci; // set the RESX for resource localization
                IoC.Localization.SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new MainPage(IoC.Settings);
        }

        protected override void OnStart()
        {
            Resources = CreateGlobalStyles(new StyleProvider());
        }

        protected ResourceDictionary CreateGlobalStyles(StyleProvider styleProvider)
        {
            var resources = new ResourceDictionary {
                styleProvider.GetStyleForButtons(),
                styleProvider.GetStyleForLabels(),
                styleProvider.GetStyleForEntries(),
                styleProvider.GetStyleForEditors(),
                styleProvider.GetStyleForPages()
            };
            return resources;
        }
    }
}
