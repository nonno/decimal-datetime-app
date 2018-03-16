using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DecimalTime.Forms.Utils
{
    public class SettingsProvider
    {
        private IDictionary<string, object> Props { get => Application.Current.Properties; }

        private string shortFormat;
        private string longFormat;

        public SettingsProvider()
        {
            shortFormat = ShortFormat ?? "d MMMM yyy";
            longFormat = LongFormat ?? "ddd d MMMM MMM M yyy, hh:mm:ss";
        }

        public string ShortFormat {
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

        public string LongFormat {
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
    }

}
