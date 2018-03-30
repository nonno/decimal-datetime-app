using System;

namespace DecimalTime.Wasm.Services
{
    public class SettingsProvider : Forms.Services.SettingsProvider
    {
        public new string BackgroundColor {
            get {
                return (string)Defaults[nameof(BackgroundColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new bool ShowBackgroundImage {
            get {
                return (bool)Defaults[nameof(ShowBackgroundImage)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string DateLabelColor
        {
            get {
                return (string)Defaults[nameof(DateLabelColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        #region ClockView
        public new string TickMarksColor
        {
            get {
                return (string)Defaults[nameof(TickMarksColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string HoursHandColor
        {
            get {
                return (string)Defaults[nameof(HoursHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string MinutesHandColor
        {
            get {
                return (string)Defaults[nameof(MinutesHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string SecondsHandColor
        {
            get {
                return (string)Defaults[nameof(SecondsHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }
        #endregion

        public new bool EnableReaderOnDoubleTap {
            get {
                return (bool)Defaults[nameof(EnableReaderOnDoubleTap)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string ShortFormat {
            get {
                return (string)Defaults[nameof(ShortFormat)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new string LongFormat {
            get {
                return (string)Defaults[nameof(LongFormat)];
            }
            set {
                throw new NotImplementedException();
            }
        }
    }

}
