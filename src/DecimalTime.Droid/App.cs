using System;
using Android.App;
using Android.Runtime;
using Autofac;
using DecimalTime.Droid.Services;
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

            try {
                FirebaseApp.InitializeApp(this);
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
            }

            IoCSetup();
        }

        private void IoCSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new FirebaseAnalyticsService(this)).As<AnalyticsService>().SingleInstance();
            builder.RegisterType<TextToSpeechService>().As<ITextToSpeech>().SingleInstance();
            IoC.Container = builder.Build();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e);

            IoC.Analytics.LogException(e.ExceptionObject as Exception, true);
        }
    }
}
