using System;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Services;
using Xamarin.Forms;

namespace DecimalTime.Wasm.Pages
{
    public sealed class CustomMainPage : MainPage
    {
        public CustomMainPage(ISettingsProvider settingsProvider) : base(settingsProvider)
        {
            backgroundImage.RemoveBinding(Image.SourceProperty);
            backgroundImage.IsVisible = false;
            settingsButton.IsVisible = false;
        }
    }
}
