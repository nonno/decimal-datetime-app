using System;
using System.Collections.Generic;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            BindingContext = new SettingsPageModel(Navigation);

            InitializeComponent();
        }
    }
}
