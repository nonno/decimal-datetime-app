using System;
using Xamarin.Forms;

namespace DecimalTime.Forms.Utils
{
    public static class Styles
    {
        public static string DateLabelColor {
            get {
                if (Application.Current.Properties.ContainsKey(nameof(DateLabelColor))) {
                    return Application.Current.Properties[nameof(DateLabelColor)].ToString();
                }
                return "#FFFFFFFF";
            }
            set {
                if (Application.Current.Properties.ContainsKey(nameof(DateLabelColor))) {
                    Application.Current.Properties[nameof(DateLabelColor)] = value;
                } else {
                    Application.Current.Properties.Add(nameof(DateLabelColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        #region ClockView
        public static string TickMarksColor {
            get {
                if (Application.Current.Properties.ContainsKey(nameof(TickMarksColor))) {
                    return Application.Current.Properties[nameof(TickMarksColor)].ToString();
                }
                return "#FFFFFFFF";
            }
            set {
                if (Application.Current.Properties.ContainsKey(nameof(TickMarksColor))) {
                    Application.Current.Properties[nameof(TickMarksColor)] = value;
                } else {
                    Application.Current.Properties.Add(nameof(TickMarksColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public static string HoursHandColor {
            get {
                if (Application.Current.Properties.ContainsKey(nameof(HoursHandColor))) {
                    return Application.Current.Properties[nameof(HoursHandColor)].ToString();
                }
                return "#FF83CBC4";
            }
            set {
                if (Application.Current.Properties.ContainsKey(nameof(HoursHandColor))) {
                    Application.Current.Properties[nameof(HoursHandColor)] = value;
                } else {
                    Application.Current.Properties.Add(nameof(HoursHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public static string MinutesHandColor {
            get {
                if (Application.Current.Properties.ContainsKey(nameof(MinutesHandColor))) {
                    return Application.Current.Properties[nameof(MinutesHandColor)].ToString();
                }
                return "#FF9CCBC4";
            }
            set {
                if (Application.Current.Properties.ContainsKey(nameof(MinutesHandColor))) {
                    Application.Current.Properties[nameof(MinutesHandColor)] = value;
                } else {
                    Application.Current.Properties.Add(nameof(MinutesHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }

        public static string SecondsHandColor {
            get {
                if (Application.Current.Properties.ContainsKey(nameof(SecondsHandColor))) {
                    return Application.Current.Properties[nameof(SecondsHandColor)].ToString();
                }
                return "#FFB4CBC4";
            }
            set {
                if (Application.Current.Properties.ContainsKey(nameof(SecondsHandColor))) {
                    Application.Current.Properties[nameof(SecondsHandColor)] = value;
                } else {
                    Application.Current.Properties.Add(nameof(SecondsHandColor), value);
                }
                Application.Current.SavePropertiesAsync();
            }
        }
        #endregion
    }
}
