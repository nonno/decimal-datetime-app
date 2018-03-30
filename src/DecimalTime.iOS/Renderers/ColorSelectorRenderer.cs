using System.ComponentModel;
using DecimalTime.Forms.Controls;
using DecimalTime.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ColorSelector), typeof(ColorSelectorRenderer))]
namespace DecimalTime.iOS.Renderers
{
    public class ColorSelectorRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {
                var view = (Element as ColorSelector);
                if (view != null) {
                    view.Focused += (object sender, FocusEventArgs e2) => {
                        var ev = e2;
                    };
                }
            }
        }
    }
}