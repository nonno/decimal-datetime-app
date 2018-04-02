using System;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Services;
using Xamarin.Forms;

namespace DecimalTime.Web.Pages
{
    public sealed class CustomMainPage : MainPage
    {
        public CustomMainPage(ISettingsProvider settingsProvider) : base(settingsProvider)
        {
            backgroundImage.RemoveBinding(Image.SourceProperty);
            backgroundImage.Source = ImageSource.FromResource("DecimalTime.Web.Images.m13.jpg", System.Reflection.Assembly.GetCallingAssembly());

            settingsButton.IsVisible = false;
        }
    }
}
