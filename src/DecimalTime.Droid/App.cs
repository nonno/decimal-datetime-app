using System;
using Android.App;
using Android.Runtime;
using Autofac;
using DecimalTime.Droid.Services;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Firebase;

namespace DecimalTime.Droid
{
    #if DEBUG
    [Application(Debuggable = true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class App : Application
    {
        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho) { }

        public override void OnCreate()
        {
            base.OnCreate();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            IoCSetup();

            FirebaseSetup();
        }

        private void IoCSetup()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(new FirebaseAnalyticsService(this)).As<AnalyticsService>().SingleInstance();
            containerBuilder.RegisterType<TextToSpeechService>().As<ITextToSpeechService>().SingleInstance();
            containerBuilder.RegisterType<LocalizationService>().As<ILocalizationService>().SingleInstance();
            containerBuilder.RegisterType<SettingsProvider>().As<SettingsProvider>().SingleInstance();
            IoC.Container = containerBuilder.Build();
        }

        private void FirebaseSetup()
        {
            try {
                FirebaseApp.InitializeApp(this);
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e);

            IoC.Analytics.LogException(e.ExceptionObject as Exception, true);
        }
    }
}
