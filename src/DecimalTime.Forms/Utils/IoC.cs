﻿using System;
using Autofac;
using DecimalTime.Forms.Services;

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

        public static ISettingsProvider Settings {
            get { return ResolveObject<ISettingsProvider>(); }
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
