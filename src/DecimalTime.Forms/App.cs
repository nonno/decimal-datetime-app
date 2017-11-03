using System;
using DecimalTime.Forms.Pages;
using Xamarin.Forms;

namespace DecimalTime.Forms
{
    public class App : Application
    {
        public App()
        {
            MainPage = new DecimalTimePage();
        }
    }
}
