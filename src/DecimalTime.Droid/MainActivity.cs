using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Util;
using Autofac;
using DecimalTime.Droid.Services;
using DecimalTime.Core.Utils;
using Firebase;
using MvvmCross.Forms.Droid.Views;
using Xamarin.Forms.Platform.Android;

namespace DecimalTime.Droid
{
    [Activity(ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity
    {
        readonly string TAG = nameof(MainActivity);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            try {
                FirebaseApp.InitializeApp(this);
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }

            IoCSetup();

            IsPlayServicesAvailable();

            if (Intent.Extras != null) {
                foreach (var key in Intent.Extras.KeySet()) {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                }
            }
        }

        private void IoCSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new FirebaseAnalyticsService(this)).As<AnalyticsService>().SingleInstance();
            builder.RegisterType<TextToSpeechService>().As<ITextToSpeech>().SingleInstance();
            IoC.Container = builder.Build();
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

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e);

            IoC.Analytics.LogException(e.ExceptionObject as Exception, true);
        }
    }
}

