using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/
namespace DecimalTime.Forms.Utils
{
    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo ci;
        private const string resourceId = "DecimalTime.Forms.i18n.AppStrings";

        private static readonly Lazy<ResourceManager> ResMgr = 
            new Lazy<ResourceManager>(() => new ResourceManager(resourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android) {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null) return String.Empty;

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null) {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, resourceId, ci.Name),
                    "Text"
                );
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
