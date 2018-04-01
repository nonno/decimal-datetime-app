using System;
using Autofac;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using DecimalTime.Wasm.Pages;
using Xamarin.Forms;

namespace DecimalTime.Wasm
{
    public class App : Application
    {
        public App()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Services.SettingsProvider>().As<ISettingsProvider>().SingleInstance();
            IoC.Container = containerBuilder.Build();

            MainPage = new CustomMainPage(IoC.Settings);
        }
    }
}
