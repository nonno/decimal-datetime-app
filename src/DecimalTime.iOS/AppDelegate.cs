using System;
using Autofac;
using DecimalTime.Forms;
using DecimalTime.Forms.Utils;
using DecimalTime.iOS.Services;
using Foundation;
using UIKit;

namespace DecimalTime.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            try {
                Firebase.Core.App.Configure();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }

            IoCSetup();

            Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        private void IoCSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FirebaseAnalyticsService>().As<AnalyticsService>().SingleInstance();
            builder.RegisterType<TextToSpeechService>().As<ITextToSpeech>().SingleInstance();
            IoC.Container = builder.Build();
        }
    }
}
