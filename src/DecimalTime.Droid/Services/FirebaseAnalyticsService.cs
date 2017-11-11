using System;
using Android.Content;
using Android.OS;
using Firebase.Analytics;
using Firebase.Crash;

namespace DecimalTime.Droid.Services
{
    public class FirebaseAnalyticsService : DecimalTime.Forms.Utils.AnalyticsService
	{
		private FirebaseAnalytics firebaseAnalytics;

        public FirebaseAnalyticsService(Context AppContext)
		{
			firebaseAnalytics = FirebaseAnalytics.GetInstance(AppContext);
		}

		public override void LogPageChange(string pageName)
		{
			var bundle = new Bundle();
			bundle.PutString("screen_name", pageName);

			firebaseAnalytics.LogEvent("screenview", bundle);
		}

		public override void LogEvent(String eventAction)
		{
			var bundle = new Bundle();

			firebaseAnalytics.LogEvent(eventAction, bundle);
		}

		public override void LogException(Exception exception, bool isFatal)
		{
            if (exception != null) {
                FirebaseCrash.Report(exception);
            }
		}
	}
}
