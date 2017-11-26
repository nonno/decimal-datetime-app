using System;
using Firebase.Analytics;
using Foundation;

namespace DecimalTime.iOS.Services
{
    public class FirebaseAnalyticsService : DecimalTime.Core.Utils.AnalyticsService
	{
        public override void LogPageChange(string pageName)
		{
			NSString[] keys = { new NSString ("screen_name") };
            NSObject[] values = { new NSString(pageName) };
			var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

			Analytics.LogEvent ("screenview", parameters);
		}

		public override void LogEvent(String eventAction)
		{
			var eventName = eventAction.ToLower();

			var parameters = new NSDictionary<NSString, NSObject>(); 

			Analytics.LogEvent(eventName, parameters);
		}

        public override void LogException(Exception exception, bool isFatal)
		{
            if (!String.IsNullOrEmpty(exception?.Message)) {
                Firebase.CrashReporting.CrashReporting.Log(exception.Message);
            }
		}
	}
}
