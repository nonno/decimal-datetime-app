using System;
using System.ComponentModel;
using System.Windows.Input;
using DecimalTime.Forms.i18n;
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
            try {
                MessagingCenter.Send(this, MainPage.RefreshUiEvent);

                _navigation.PopModalAsync();
            } catch(Exception ex){
                Application.Current.MainPage.DisplayAlert(string.Empty, ex.Message, AppStrings.ok);
            }
        }
    }
}
