using System;
using DecimalTime.Core.Pages;
using DecimalTime.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainPage), typeof(MainPageRenderer))]
namespace DecimalTime.iOS.Renderers
{
    public class MainPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            WantsFullScreenLayout = true;
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackTranslucent;
        }
    }
}
