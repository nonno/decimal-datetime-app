using System;
using Xamarin.Forms;

namespace DecimalTime.Forms.Utils
{
    public static class ContentPageUtils
    {
        public static bool IsSquare(this ContentPage page){
            return Math.Abs(page.Height - page.Width) <= Double.MinValue;
        }
    }
}
