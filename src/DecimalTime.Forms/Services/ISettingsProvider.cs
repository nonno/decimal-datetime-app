using System;
namespace DecimalTime.Forms.Services
{
    public interface ISettingsProvider
    {
        string BackgroundColor { get; set; }

        bool ShowBackgroundImage { get; set; }

        string DateLabelColor { get; set; }

        #region ClockView
        string TickMarksColor { get; set; }

        string HoursHandColor { get; set; }

        string MinutesHandColor { get; set; }

        string SecondsHandColor { get; set; }
        #endregion

        bool EnableReaderOnDoubleTap { get; set; }

        string ShortFormat { get; set; }

        string LongFormat { get; set; }

        void Reset();
    }
}
