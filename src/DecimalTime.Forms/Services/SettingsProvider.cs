using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DecimalTime.Forms.Services
{
    public class SettingsProvider
    {
        private IDictionary<string, object> Props { get => Application.Current.Properties; }

        private string shortFormat;
        private string longFormat;

        private string backgroundColor;
        private string dateLabelColor;

        private string tickMarksColor;
        private string hoursHandColor;
        private string minutesHandColor;
        private string secondsHandColor;

        public SettingsProvider()
        {
            shortFormat = ShortFormat ?? "d MMMM yyy";
            longFormat = LongFormat ?? "ddd d MMMM MMM M yyy, hh:mm:ss";

            backgroundColor = BackgroundColor ?? "#00000000";
            dateLabelColor = DateLabelColor ?? "#FFFFFFFF";

            tickMarksColor = TickMarksColor ?? "#FFFFFFFF";
            hoursHandColor = HoursHandColor ?? "#FF83CBC4";
            minutesHandColor = MinutesHandColor ?? "#FF9CCBC4";
            secondsHandColor = SecondsHandColor ?? "#FFB4CBC4";
        }

        #region DataFormat
        public string ShortFormat
        {
            get {
                if (!string.IsNullOrEmpty(shortFormat)) { return shortFormat; }

                if (Props.ContainsKey(nameof(ShortFormat))) {
                    return Props[nameof(ShortFormat)].ToString();
                }
                return shortFormat;
            }
            set {
                shortFormat = value;
                if (Props.ContainsKey(nameof(ShortFormat))) {
                    Props[nameof(ShortFormat)] = value;
                } else {
                    Props.Add(nameof(ShortFormat), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public string LongFormat
        {
            get {
                if (!string.IsNullOrEmpty(longFormat)) { return longFormat; }

                if (Props.ContainsKey(nameof(LongFormat))) {
                    return Props[nameof(LongFormat)].ToString();
                }
                return longFormat;
            }
            set {
                longFormat = value;
                if (Props.ContainsKey(nameof(LongFormat))) {
                    Props[nameof(LongFormat)] = value;
                } else {
                    Props.Add(nameof(LongFormat), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }
        #endregion

        public string BackgroundColor {
            get {
                if (!string.IsNullOrEmpty(backgroundColor)) { return backgroundColor; }

                if (Props.ContainsKey(nameof(BackgroundColor))) {
                    return Props[nameof(BackgroundColor)].ToString();
                }
                return backgroundColor;
            }
            set {
                backgroundColor = value;
                if (Props.ContainsKey(nameof(BackgroundColor))) {
                    Props[nameof(BackgroundColor)] = value;
                } else {
                    Props.Add(nameof(BackgroundColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public string DateLabelColor
        {
            get {
                if (!string.IsNullOrEmpty(dateLabelColor)) { return dateLabelColor; }

                if (Props.ContainsKey(nameof(DateLabelColor))) {
                    return Props[nameof(DateLabelColor)].ToString();
                }
                return dateLabelColor;
            }
            set {
                dateLabelColor = value;
                if (Props.ContainsKey(nameof(DateLabelColor))) {
                    Props[nameof(DateLabelColor)] = value;
                } else {
                    Props.Add(nameof(DateLabelColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        #region ClockView
        public string TickMarksColor
        {
            get {
                if (!string.IsNullOrEmpty(tickMarksColor)) { return tickMarksColor; }

                if (Props.ContainsKey(nameof(TickMarksColor))) {
                    return Props[nameof(TickMarksColor)].ToString();
                }
                return tickMarksColor;
            }
            set {
                tickMarksColor = value;
                if (Props.ContainsKey(nameof(TickMarksColor))) {
                    Props[nameof(TickMarksColor)] = value;
                } else {
                    Props.Add(nameof(TickMarksColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public string HoursHandColor
        {
            get {
                if (!string.IsNullOrEmpty(hoursHandColor)) { return hoursHandColor; }

                if (Props.ContainsKey(nameof(HoursHandColor))) {
                    return Props[nameof(HoursHandColor)].ToString();
                }
                return hoursHandColor;
            }
            set {
                hoursHandColor = value;
                if (Props.ContainsKey(nameof(HoursHandColor))) {
                    Props[nameof(HoursHandColor)] = value;
                } else {
                    Props.Add(nameof(HoursHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public string MinutesHandColor
        {
            get {
                if (!string.IsNullOrEmpty(minutesHandColor)) { return minutesHandColor; }

                if (Props.ContainsKey(nameof(MinutesHandColor))) {
                    return Props[nameof(MinutesHandColor)].ToString();
                }
                return minutesHandColor;
            }
            set {
                minutesHandColor = value;
                if (Props.ContainsKey(nameof(MinutesHandColor))) {
                    Props[nameof(MinutesHandColor)] = value;
                } else {
                    Props.Add(nameof(MinutesHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public string SecondsHandColor
        {
            get {
                if (!string.IsNullOrEmpty(secondsHandColor)) { return secondsHandColor; }

                if (Props.ContainsKey(nameof(SecondsHandColor))) {
                    return Props[nameof(SecondsHandColor)].ToString();
                }
                return secondsHandColor;
            }
            set {
                secondsHandColor = value;
                if (Props.ContainsKey(nameof(SecondsHandColor))) {
                    Props[nameof(SecondsHandColor)] = value;
                } else {
                    Props.Add(nameof(SecondsHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }
        #endregion
    }

}
