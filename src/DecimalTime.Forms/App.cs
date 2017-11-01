using System;
using DecimalTime.Forms.Pages;
using Xamarin.Forms;

namespace DecimalTime.Forms
{
    public class App : Application
    {
        public App()
        {
            if (Device.RuntimePlatform == Device.Android) {
                MainPage = new DecimalTimePage(); // eventuale splash
            } else {
                MainPage = new DecimalTimePage();
            }
        }
    }
}
