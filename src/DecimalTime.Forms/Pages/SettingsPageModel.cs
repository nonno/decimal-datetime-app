using System;
using System.ComponentModel;
using System.Windows.Input;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public class SettingsPageModel : INotifyPropertyChanged
    {
        private INavigation _navigation;

        private SettingsProvider _settingsProvider;

        public SettingsPageModel(INavigation navigation, SettingsProvider settingsProvider)
        {
            _navigation = navigation;
            _settingsProvider = settingsProvider;

            _shortFormatText = _settingsProvider.ShortFormat;
            _longFormatText = _settingsProvider.LongFormat;
        }

        private string _shortFormatText;
        public string ShortFormatText
        {
            get => _shortFormatText;
            set
            {
                if (_shortFormatText == value) return;

                _shortFormatText = value;
                _settingsProvider.ShortFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShortFormatText)));
            }
        }

        private string _longFormatText;
        public string LongFormatText
        {
            get => _longFormatText;
            set
            {
                if (_longFormatText == value) return;

                _longFormatText = value;
                _settingsProvider.LongFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LongFormatText)));
            }
        }

        public ICommand CloseCommand => new Command(Close);
        private void Close()
        {
            _navigation.PopModalAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
