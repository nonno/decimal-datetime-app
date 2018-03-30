using System;
using Autofac;
using DecimalTime.Forms.Pages;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Ooui;
using Xamarin.Forms;

namespace DecimalTime.Wasm
{
    class Program
    {
        static void Main(string[] args)
        {
            Xamarin.Forms.Forms.Init();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Services.SettingsProvider>().As<ISettingsProvider>().SingleInstance();
            IoC.Container = containerBuilder.Build();

            var page = new MainPage(IoC.Settings);

            UI.Publish("/", page.GetOouiElement());
        }
    }
}
