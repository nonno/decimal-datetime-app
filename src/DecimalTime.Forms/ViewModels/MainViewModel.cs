using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DecimalTime.Core.i18n;
using DecimalTime.Core.Pages;
using DecimalTime.Core.Utils;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace DecimalTime.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task Initialize()
        {
            int timerPeriod = Convert.ToInt32(Pallettaro.Revo.DateTime.SECONDS_RATIO * 500);
            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
		    
            return base.Initialize();
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set => SetProperty(ref _dateTime, value);
        }

        private Pallettaro.Revo.DateTime _decimalDateTime;
        public Pallettaro.Revo.DateTime DecimalDateTime
        {
            get => _decimalDateTime;
            set => SetProperty(ref _decimalDateTime, value);
        }

        private string _calendarImageFile;
        public string CalendarImageFile
        {
            get => _calendarImageFile;
            set => SetProperty(ref _calendarImageFile, value);
        }

        public IMvxCommand SpeakCommand => new MvxCommand(Speak);
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

        public IMvxCommand AlertCurrentDateTimeCommand => new MvxCommand(AlertCurrentDateTime);
        private void AlertCurrentDateTime()
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.ShowExtendedDate);

            var now = Pallettaro.Revo.DateTime.Now;
            var nowString = $"{now.ToString("hh:mm:ss - dd/MM/yyy")} - {now.DayName}, {now.MonthName}";

            Mvx.Resolve<IUserDialogs>().Alert(nowString, okText: AppStrings.ok);
        }

        public IMvxCommand ShowSettingsCommand => new MvxCommand(ShowSettings);
        private void ShowSettings()
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.OpenSettings);

            _navigationService.Navigate<SettingsViewModel>();
        }

        private bool OnTimerTick()
        {
            DateTime = DateTime.Now;
            DecimalDateTime = Pallettaro.Revo.DateTime.Now;

            CalendarImageFile = $"m{DecimalDateTime.RepublicanMonth.ToString("00")}.jpg";

            return true;
        }
    }
}