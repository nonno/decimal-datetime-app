using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Xamarin.Forms.Platform.Android;

namespace DecimalTime.Droid
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsAppCompatActivity
    {
        readonly string TAG = nameof(MainActivity);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IsPlayServicesAvailable();

            if (Intent.Extras != null) {
                foreach (var key in Intent.Extras.KeySet()) {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }

            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new Forms.App());
        }

        private bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode == ConnectionResult.Success) {
                Console.WriteLine("Google Play Services is available.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)) {
                Console.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
            } else {
                Console.WriteLine("Google Play Services is not available.");
            }
            return false;
        }
    }
}

