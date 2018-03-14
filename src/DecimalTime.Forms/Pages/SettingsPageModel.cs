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

        public SettingsPageModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private string _shortFormatText = FormatSettings.ShortFormat;
        public string ShortFormatText
        {
            get => _shortFormatText;
            set
            {
                if (_shortFormatText == value) return;

                _shortFormatText = value;
                FormatSettings.ShortFormat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShortFormatText)));
            }
        }

        private string _longFormatText = FormatSettings.LongFormat;
        public string LongFormatText
        {
            get => _longFormatText;
            set
            {
                if (_longFormatText == value) return;

                _longFormatText = value;
                FormatSettings.LongFormat = value;
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
