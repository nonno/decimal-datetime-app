using System;

namespace DecimalTime.Core.Utils
{
    public abstract class AnalyticsService
    {
        public struct Action
        {
            public static String OpenSettings { get { return nameof(OpenSettings); } }

            public static String ShowExtendedDate { get { return nameof(ShowExtendedDate); } }
        };

        public abstract void LogPageChange(String pageName);

        public abstract void LogEvent(String eventAction);

        public abstract void LogException(Exception exception, Boolean isFatal);
    }
}
