using System;
using Autofac;
using DecimalTime.Core.Utils;
using DecimalTime.iOS.Services;
using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using UIKit;

namespace DecimalTime.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            try {
                Firebase.Core.App.Configure();
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }

            IoCSetup();

            Window = new UIWindow();
            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            LoadApplication(setup.FormsApplication);

            Window.MakeKeyAndVisible();

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
