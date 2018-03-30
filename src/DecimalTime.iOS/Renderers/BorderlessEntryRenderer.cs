using System.ComponentModel;
using DecimalTime.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(BorderedEntryRenderer))]
namespace DecimalTime.iOS.Renderers
{
    public class BorderedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {
                var view = (Element as Entry);
                if (view != null) {
                    Control.BorderRect(new CoreGraphics.CGRect(0, 0, Frame.Width, Frame.Height));
                    Control.BorderStyle = UITextBorderStyle.Line;
                }
            }
        }
    }
}