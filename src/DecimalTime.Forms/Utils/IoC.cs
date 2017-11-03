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
