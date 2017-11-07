using System;
using DecimalTime.Forms.i18n;
using DecimalTime.Forms.Utils;
using DecimalTime.Forms.Views;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    class DecimalTimePage : ContentPage
    {
        private readonly int timerPeriod = Convert.ToInt32(Pallettaro.Revo.DateTime.SECONDS_RATIO * 500);

        private readonly AbsoluteLayout contentContainer;

        private readonly Label dateLabel;
        private readonly Label dateNameLabel;

        private readonly ClockView clockView;
        private readonly Image backgroundImage;
        private readonly Button settingsButton;

        private readonly TapGestureRecognizer pageDoubleTapRecognizer;

        public DecimalTimePage()
        {
            var repTime = Pallettaro.Revo.DateTime.Now;

            contentContainer = new AbsoluteLayout();

            clockView = new ClockView();

            backgroundImage = new Image {
                Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg",
                Aspect = Aspect.AspectFill
            };

            dateLabel = new Label() {
                FontSize = 25,
                TextColor = Color.FromHex(Styles.DateLabelColor),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = repTime.ToString(FormatSettings.ShortFormat)
            };
            dateNameLabel = new Label {
                FontSize = 25,
                TextColor = Color.FromHex(Styles.DateLabelColor),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = repTime.DayName
            };

            settingsButton = new Button {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                Image = AppAssets.settingsIco
            };
            settingsButton.Clicked += SettingsButton_Clicked;

            contentContainer.Children.Add(backgroundImage);
            contentContainer.Children.Add(clockView);
            contentContainer.Children.Add(dateNameLabel);
            contentContainer.Children.Add(dateLabel);
            contentContainer.Children.Add(settingsButton);

            if (this.IsSquare()){
                dateNameLabel.IsVisible = false;
                dateLabel.IsVisible = false;
            }

            this.Content = contentContainer;

            this.SizeChanged += OnPageSizeChanged;

            pageDoubleTapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            pageDoubleTapRecognizer.Tapped += Page_DoubleTapped;

            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
        }

        private void SetControlsPositions()
        {
            int labelsHeight = 30;
            int labelsYOffset = 15;
            int settingsSize = 50;
            int settingsMargin = 5;

            if (Height > Width) {
                double clockSize = Width;
                double clockViewY = Height / 2 - clockSize / 2;

                AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, Width, Height));
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(0, clockViewY, clockSize, clockSize));
                AbsoluteLayout.SetLayoutBounds(dateLabel, new Rectangle(0, labelsYOffset, Width, labelsHeight));
                AbsoluteLayout.SetLayoutBounds(dateNameLabel, new Rectangle(0, labelsYOffset + labelsHeight, Width, labelsHeight));
            } else {
                double clockSize = Height;
                double clockViewX = Width - clockSize;

                AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, Width, 2 * Height));
                AbsoluteLayout.SetLayoutBounds(clockView, new Rectangle(clockViewX, 0, clockSize, clockSize));
                AbsoluteLayout.SetLayoutBounds(dateLabel, new Rectangle(0, labelsYOffset, Width / 2, labelsHeight));
                AbsoluteLayout.SetLayoutBounds(dateNameLabel, new Rectangle(0, labelsYOffset + labelsHeight, Width / 2, labelsHeight));
            }
            AbsoluteLayout.SetLayoutBounds(settingsButton, new Rectangle(settingsMargin, Height - settingsSize - settingsMargin, settingsSize, settingsSize));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.contentContainer.GestureRecognizers.Add(pageDoubleTapRecognizer);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.contentContainer.GestureRecognizers.Remove(pageDoubleTapRecognizer);
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.OpenSettings);

            Action refresh = () => {
                dateLabel.Text = Pallettaro.Revo.DateTime.Now.ToString(FormatSettings.ShortFormat);
            };
            var settingsPage = new SettingsPage(refresh);
            await Navigation.PushModalAsync(settingsPage);

        }

        private void Page_DoubleTapped(object sender, EventArgs e)
        {
            IoC.Analytics.LogEvent(AnalyticsService.Action.ShowExtendedDate);

            var repTime = Pallettaro.Revo.DateTime.Now;
            DisplayAlert(String.Empty, repTime.ToString(FormatSettings.LongFormat), AppStrings.ok);
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            SetControlsPositions();

            this.clockView.OnPageSizeChanged(sender, args);
        }

        private bool OnTimerTick()
        {
            var repTime = Pallettaro.Revo.DateTime.Now;
            var changeDay = repTime.RepublicanHours.Equals(0) && repTime.RepublicanMinutes.Equals(0) && repTime.RepublicanSeconds.Equals(0);
            var dayLabelContainsHour = FormatSettings.ShortFormat.Contains("h") || FormatSettings.ShortFormat.Contains("m") || FormatSettings.ShortFormat.Contains("s");
            if (changeDay || dayLabelContainsHour) {
                dateLabel.Text = repTime.ToString(FormatSettings.ShortFormat);
                dateNameLabel.Text = repTime.DayName;
                if (repTime.RepublicanDay.Equals(1)) {
                    this.backgroundImage.Source = $"m{repTime.RepublicanMonth.ToString("00")}.jpg";
                }
            }
            return clockView.OnTimerTick();
        }
    }

}
