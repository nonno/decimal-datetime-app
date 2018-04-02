using System;
using Autofac;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using DecimalTime.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Ooui.AspNetCore;
using Xamarin.Forms;

namespace DecimalTime.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Services.SettingsProvider>().As<ISettingsProvider>().SingleInstance();
            IoC.Container = containerBuilder.Build();

            var page = new CustomMainPage(IoC.Settings);
            var element = page.GetOouiElement();

            return new ElementResult(element);
        }
    }
}
