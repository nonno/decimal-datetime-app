using System;
using System.Collections.Generic;
using System.ComponentModel;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace DecimalTime.Forms.Services
{
    public class SettingsProvider : PropertyChangedNotifier, ISettingsProvider
    {
        private ISettings AppSettings { get => CrossSettings.Current; }

        protected Dictionary<string, object> Defaults = new Dictionary<string, object> {
            {nameof(BackgroundColor), "black"},
            {nameof(ShowBackgroundImage), true},
            {nameof(DateLabelColor), "white"},
            {nameof(TickMarksColor), "white"},
            {nameof(HoursHandColor), "#FF83CBC4"},
            {nameof(MinutesHandColor), "#FF9CCBC4"},
            {nameof(SecondsHandColor), "#FFB4CBC4"},
            {nameof(EnableReaderOnDoubleTap), true},
            {nameof(ShortFormat), "d MMMM yyy"},
            {nameof(LongFormat), "ddd d MMMM MMM M yyy, hh:mm:ss"}
        };

        public virtual void Reset()
        {
            BackgroundColor = (string)Defaults[nameof(BackgroundColor)];
            ShowBackgroundImage = (bool)Defaults[nameof(ShowBackgroundImage)];
            DateLabelColor = (string)Defaults[nameof(DateLabelColor)];
            TickMarksColor = (string)Defaults[nameof(TickMarksColor)];
            HoursHandColor = (string)Defaults[nameof(HoursHandColor)];
            MinutesHandColor = (string)Defaults[nameof(MinutesHandColor)];
            SecondsHandColor = (string)Defaults[nameof(SecondsHandColor)];
            EnableReaderOnDoubleTap = (bool)Defaults[nameof(EnableReaderOnDoubleTap)];
            ShortFormat = (string)Defaults[nameof(ShortFormat)];
            LongFormat = (string)Defaults[nameof(LongFormat)];
        }

        public virtual string BackgroundColor {
            get {
                return AppSettings.GetValueOrDefault(nameof(BackgroundColor), (string)Defaults[nameof(BackgroundColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(BackgroundColor), value);
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public virtual bool ShowBackgroundImage {
            get {
                return AppSettings.GetValueOrDefault(nameof(ShowBackgroundImage), (bool)Defaults[nameof(ShowBackgroundImage)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(ShowBackgroundImage), value);
                OnPropertyChanged(nameof(ShowBackgroundImage));
            }
        }

        public virtual string DateLabelColor
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
        public virtual string TickMarksColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(TickMarksColor), (string)Defaults[nameof(TickMarksColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(TickMarksColor), value);
                OnPropertyChanged(nameof(TickMarksColor));
            }
        }

        public virtual string HoursHandColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(HoursHandColor), (string)Defaults[nameof(HoursHandColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(HoursHandColor), value);
                OnPropertyChanged(nameof(HoursHandColor));
            }
        }

        public virtual string MinutesHandColor
        {
            get {
                return AppSettings.GetValueOrDefault(nameof(MinutesHandColor), (string)Defaults[nameof(MinutesHandColor)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(MinutesHandColor), value);
                OnPropertyChanged(nameof(MinutesHandColor));
            }
        }

        public virtual string SecondsHandColor
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

        public virtual bool EnableReaderOnDoubleTap {
            get {
                return AppSettings.GetValueOrDefault(nameof(EnableReaderOnDoubleTap), (bool)Defaults[nameof(EnableReaderOnDoubleTap)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(EnableReaderOnDoubleTap), value);
                OnPropertyChanged(nameof(EnableReaderOnDoubleTap));
            }
        }

        public virtual string ShortFormat {
            get {
                return AppSettings.GetValueOrDefault(nameof(ShortFormat), (string)Defaults[nameof(ShortFormat)]);
            }
            set {
                AppSettings.AddOrUpdateValue(nameof(ShortFormat), value);
                OnPropertyChanged(nameof(ShortFormat));
            }
        }

        public virtual string LongFormat {
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
