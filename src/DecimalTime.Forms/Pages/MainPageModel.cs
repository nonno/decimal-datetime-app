using System;
using System.ComponentModel;
using System.Windows.Input;
using DecimalTime.Forms.i18n;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private INavigation _navigation;

        public MainPageModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void Initialize()
        {
            int timerPeriod = Convert.ToInt32(Pallettaro.Revo.DateTime.SECONDS_RATIO * 500);
            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set {
                if (_dateTime == value) return;

                _dateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTime)));
            }
        }

        private Pallettaro.Revo.DateTime _decimalDateTime;
        public Pallettaro.Revo.DateTime DecimalDateTime
        {
            get => _decimalDateTime;
            set {
                if (_decimalDateTime == value) return;

                _decimalDateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DecimalDateTime)));
            }
        }

        private string _calendarImageFile;
        public string CalendarImageFile
        {
            get => _calendarImageFile;
            set {
                if (_calendarImageFile == value) return;

                _calendarImageFile = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CalendarImageFile)));
            }
        }

        public ICommand SpeakCommand => new Command(Speak);
        private void Speak()
        {
            var now = DecimalDateTime;
            var nowSpeak =
                $"{now.ToString("hh:mm")}; " +
                $"{now.RepublicanDay}, " +
                $"{now.RepublicanMonth}, " +
                $"{now.RepublicanYear}; " +
                $"{now.DayName}, " +
                $"{now.MonthName}";

            IoC.TTS.Speak(nowSpeak);
        }

        public ICommand AlertCurrentDateTimeCommand => new Command(AlertCurrentDateTime);
        private void AlertCurrentDateTime()
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.ShowExtendedDate);

            var now = Pallettaro.Revo.DateTime.Now;
            var nowString = $"{now.ToString("hh:mm:ss - dd/MM/yyy")} - {now.DayName}, {now.MonthName}";

            Application.Current.MainPage.DisplayAlert(string.Empty, nowString, AppStrings.ok);
        }

        public ICommand ShowSettingsCommand => new Command(ShowSettings);
        private void ShowSettings()
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.OpenSettings);

            _navigation.PushModalAsync(new SettingsPage());
        }

        private bool OnTimerTick()
        {
            DateTime = DateTime.Now;
            DecimalDateTime = Pallettaro.Revo.DateTime.Now;

            CalendarImageFile = $"m{DecimalDateTime.RepublicanMonth.ToString("00")}.jpg";

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
