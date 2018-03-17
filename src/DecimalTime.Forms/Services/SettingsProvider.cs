using System;
using System.Collections.Generic;
using System.ComponentModel;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace DecimalTime.Forms.Services
{
    public class SettingsProvider : PropertyChangedNotifier
    {
        private ISettings AppSettings { get => CrossSettings.Current; }

        private Dictionary<string, object> Defaults = new Dictionary<string, object> {
            {nameof(BackgroundColor), "#00000000"},
            {nameof(ShowBackgroundImage), true},
            {nameof(DateLabelColor), "#FFFFFFFF"},
            {nameof(TickMarksColor), "#FFFFFFFF"},
            {nameof(HoursHandColor), "#FF83CBC4"},
            {nameof(MinutesHandColor), "#FF9CCBC4"},
            {nameof(SecondsHandColor), "#FFB4CBC4"},
            {nameof(EnableReaderOnDoubleTap), true},
            {nameof(ShortFormat), "d MMMM yyy"},
            {nameof(LongFormat), "ddd d MMMM MMM M yyy, hh:mm:ss"}
        };

        public string BackgroundColor {
            get {
                return AppSettings.GetValueOrDefault(nameof(BackgroundColor), (string)Defaults[nameof(BackgroundColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(BackgroundColor), value);
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public bool ShowBackgroundImage {
            get {
                return AppSettings.GetValueOrDefault(nameof(ShowBackgroundImage), (bool)Defaults[nameof(ShowBackgroundImage)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(ShowBackgroundImage), value);
                OnPropertyChanged(nameof(ShowBackgroundImage));
            }
        }

        public string DateLabelColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(DateLabelColor), (string)Defaults[nameof(DateLabelColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(DateLabelColor), value);
                OnPropertyChanged(nameof(DateLabelColor));
            }
        }

        #region ClockView
        public string TickMarksColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(TickMarksColor), (string)Defaults[nameof(TickMarksColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(TickMarksColor), value);
                OnPropertyChanged(nameof(TickMarksColor));
            }
        }

        public string HoursHandColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(HoursHandColor), (string)Defaults[nameof(HoursHandColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(HoursHandColor), value);
                OnPropertyChanged(nameof(HoursHandColor));
            }
        }

        public string MinutesHandColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(MinutesHandColor), (string)Defaults[nameof(MinutesHandColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(MinutesHandColor), value);
                OnPropertyChanged(nameof(MinutesHandColor));
            }
        }

        public string SecondsHandColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(SecondsHandColor), (string)Defaults[nameof(SecondsHandColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(SecondsHandColor), value);
                OnPropertyChanged(nameof(SecondsHandColor));
            }
        }
        #endregion

        public bool EnableReaderOnDoubleTap {
            get {
                return AppSettings.GetValueOrDefault(nameof(EnableReaderOnDoubleTap), (bool)Defaults[nameof(EnableReaderOnDoubleTap)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(EnableReaderOnDoubleTap), value);
                OnPropertyChanged(nameof(EnableReaderOnDoubleTap));
            }
        }

        public string ShortFormat {
            get {
                return AppSettings.GetValueOrDefault(nameof(ShortFormat), (string)Defaults[nameof(ShortFormat)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(ShortFormat), value);
                OnPropertyChanged(nameof(ShortFormat));
            }
        }

        public string LongFormat {
            get {
                return AppSettings.GetValueOrDefault(nameof(LongFormat), (string)Defaults[nameof(LongFormat)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(LongFormat), value);
                OnPropertyChanged(nameof(LongFormat));
            }
        }
    }

}
