using System;
using Autofac;

namespace DecimalTime.Forms.Utils
{
    public static class IoC
    {
        public static IContainer Container { private get; set; }

        public static AnalyticsService Analytics {
            get { return ResolveObject<AnalyticsService>(); }
        }

        public static ILocalizationService Localization {
            get { return ResolveObject<ILocalizationService>(); }
        }

        public static SettingsProvider Settings {
            get { return ResolveObject<SettingsProvider>(); }
        }

        public static ITextToSpeechService TTS {
            get { return ResolveObject<ITextToSpeechService>(); }
        }

        private static T ResolveObject<T>()
        {
            if (Container != null) {
                if (Container.IsRegistered<T>()) {
                    return Container.Resolve<T>();
                }
                return default(T);
            } else {
                throw new Exception("IoC not initialized");
            }
        }
    }
}
