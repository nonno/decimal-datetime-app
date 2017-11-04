using System;
using DecimalTime.Forms.Utils;
using DecimalTime.Forms.Views;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    class DecimalTimePage : ContentPage
    {
        static readonly Color dateColor = Color.White;

        private int timerPeriod = Convert.ToInt32(Pallettaro.Revo.DateTime.SECONDS_RATIO * 500);

        private Label dayNameLabel;
        private Label dayLabel;
        private Button settingsButton;
        private ClockView clockView;
        private Image backgroundImage;

        public DecimalTimePage()
        {
            var repTime = Pallettaro.Revo.DateTime.Now;
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            backgroundImage = new Image {
                Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg",
                Aspect = Aspect.AspectFill
            };
            absoluteLayout.Children.Add(backgroundImage);

            dayNameLabel = new Label();
            dayNameLabel.FontSize = 30;
            dayNameLabel.TextColor = dateColor;
            dayNameLabel.VerticalTextAlignment = TextAlignment.Center;
            dayNameLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayNameLabel.Text = repTime.DayName;
            absoluteLayout.Children.Add(dayNameLabel);

            clockView = new ClockView();
            absoluteLayout.Children.Add(clockView);

            settingsButton = new Button();
            settingsButton.BackgroundColor = Color.Transparent;
            settingsButton.BorderColor = Color.Transparent;
            settingsButton.Image = "Icon.png";

            absoluteLayout.Children.Add(settingsButton);
            settingsButton.Clicked += SettingsButton_Clicked;

            dayLabel = new Label();
            dayLabel.FontSize = 30;
            dayLabel.TextColor = dateColor;
            dayLabel.VerticalTextAlignment = TextAlignment.Center;
            dayLabel.HorizontalTextAlignment = TextAlignment.Center;
            dayLabel.Text = repTime.ToString(FormatSettings.ShortFormat);

            absoluteLayout.Children.Add(dayLabel);

            Content = absoluteLayout;

            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
            SizeChanged += OnPageSizeChanged;

            var doubleTapGestureRecognizer = new TapGestureRecognizer();
            doubleTapGestureRecognizer.NumberOfTapsRequired = 2;
            doubleTapGestureRecognizer.Tapped += Page_DoubleTapped;
            absoluteLayout.GestureRecognizers.Add(doubleTapGestureRecognizer);
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.OpenSettings);

            Action refresh = () => {
                dayLabel.Text = Pallettaro.Revo.DateTime.Now.ToString(FormatSettings.ShortFormat);
            };
            var settingsPage = new SettingsPage(refresh);
            await Navigation.PushModalAsync(settingsPage);

        }

        private void Page_DoubleTapped(object sender, EventArgs e)
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.ShowExtendedDate);

            var repTime = Pallettaro.Revo.DateTime.Now;
            DisplayAlert(String.Empty, repTime.ToString(FormatSettings.LongFormat), "ok");
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            if (Height > Width) {
                AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, Width, Height));
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(0, Width / 2, Width, Width));
                AbsoluteLayout.SetLayoutBounds(dayLabel, new Rectangle(0, 15, Width, 40));
                AbsoluteLayout.SetLayoutBounds(dayNameLabel, new Rectangle(0, 55, Width, 40));
                AbsoluteLayout.SetLayoutBounds(settingsButton, new Rectangle(0, Height - 70, 70, 70));
            } else {
                AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, Width, 2 * Height));
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(Width - Height, 0, Height, Height));
                AbsoluteLayout.SetLayoutBounds(dayLabel, new Rectangle(0, 5, Width / 2, 40));
                AbsoluteLayout.SetLayoutBounds(dayNameLabel, new Rectangle(0, 45, Width / 2, 40));
                AbsoluteLayout.SetLayoutBounds(settingsButton, new Rectangle(0, Height - 70, 70, 70));
            }
            this.clockView.OnPageSizeChanged(sender, args);
        }

        private bool OnTimerTick()
        {
            var repTime = Pallettaro.Revo.DateTime.Now;
            var changeDay = repTime.RepublicanHours.Equals(0) && repTime.RepublicanMinutes.Equals(0) && repTime.RepublicanSeconds.Equals(0);
            var dayLabelContainsHour = FormatSettings.ShortFormat.Contains("h") || FormatSettings.ShortFormat.Contains("m") || FormatSettings.ShortFormat.Contains("s");
            if (changeDay || dayLabelContainsHour) {
                dayLabel.Text = repTime.ToString(FormatSettings.ShortFormat);
                dayNameLabel.Text = repTime.DayName;
                if (repTime.RepublicanDay.Equals(1)) {
                    this.backgroundImage.Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
                }
            }
            return clockView.OnTimerTick();
        }
    }

}
