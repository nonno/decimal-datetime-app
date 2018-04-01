using System;

namespace DecimalTime.Wasm.Services
{
    public sealed class SettingsProvider : Forms.Services.SettingsProvider
    {
        public override string BackgroundColor {
            get {
                return (string)Defaults[nameof(BackgroundColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override bool ShowBackgroundImage {
            get {
                return (bool)Defaults[nameof(ShowBackgroundImage)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string DateLabelColor
        {
            get {
                return (string)Defaults[nameof(DateLabelColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        #region ClockView
        public override string TickMarksColor
        {
            get {
                return (string)Defaults[nameof(TickMarksColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string HoursHandColor
        {
            get {
                return (string)Defaults[nameof(HoursHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string MinutesHandColor
        {
            get {
                return (string)Defaults[nameof(MinutesHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string SecondsHandColor
        {
            get {
                return (string)Defaults[nameof(SecondsHandColor)];
            }
            set {
                throw new NotImplementedException();
            }
        }
        #endregion

        public override bool EnableReaderOnDoubleTap {
            get {
                return (bool)Defaults[nameof(EnableReaderOnDoubleTap)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string ShortFormat {
            get {
                return (string)Defaults[nameof(ShortFormat)];
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override string LongFormat {
            get {
                return (string)Defaults[nameof(LongFormat)];
            }
            set {
                throw new NotImplementedException();
            }
        }
    }

}
