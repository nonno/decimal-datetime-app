using System;
using System.ComponentModel;
using System.Windows.Input;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public class SettingsPageModel : PropertyChangedNotifier
    {
        private INavigation _navigation;

        public SettingsPageModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public ICommand CloseCommand => new Command(Close);
        private void Close()
        {
            _navigation.PopModalAsync();
        }
    }
}
