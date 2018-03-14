using System;
using System.Collections.Generic;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            var pageModel = new SettingsPageModel(Navigation);
            BindingContext = pageModel;

            InitializeComponent();
        }
    }
}
