using System;
using DecimalTime.Forms.i18n;
using DecimalTime.Forms.Utils;
using DecimalTime.Forms.Views;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    class MainPage : ContentPage
    {
        private readonly int timerPeriod = Convert.ToInt32(Pallettaro.Revo.DateTime.SECONDS_RATIO * 500);

        private AbsoluteLayout contentContainer;

        private Label dateLabel;
        private Label dateNameLabel;

        private ClockView clockView;
        private Image backgroundImage;
        private Button settingsButton;

        private bool firstTick = true;

        private readonly TapGestureRecognizer pageDoubleTapRecognizer;

        public MainPage()
        {
            SetupControls();

            pageDoubleTapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            pageDoubleTapRecognizer.Tapped += Page_DoubleTapped;

            SizeChanged += OnPageSizeChanged;

            Device.StartTimer(TimeSpan.FromMilliseconds(timerPeriod), OnTimerTick);
        }

        private void SetupControls()
        {
            contentContainer = new AbsoluteLayout();

            clockView = new ClockView();

            backgroundImage = new Image {
                Aspect = Aspect.AspectFill
            };

            dateLabel = new Label() {
                FontSize = 24,
                TextColor = Color.FromHex(Styles.DateLabelColor),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            dateNameLabel = new Label {
                FontSize = 24,
                TextColor = Color.FromHex(Styles.DateLabelColor),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
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

            if (this.IsSquare()) {
                dateNameLabel.IsVisible = false;
                dateLabel.IsVisible = false;
            }
            if (true) { // TODO settings temporary disabled (not ready for production)
                settingsButton.IsVisible = false;
            }

            this.Content = contentContainer;
        }

        private void SetupControlsPositions()
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

            OnTimerTick();

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

            var now = Pallettaro.Revo.DateTime.Now;
            var nowString = $"{now.ToString("hh:mm:ss - dd/MM/yyy")} - {now.DayName}, {now.MonthName}";

            DisplayAlert(String.Empty, nowString, AppStrings.ok);

            var nowSpeak = 
                $"{now.ToString("hh:mm")}; " +
                $"{now.RepublicanDay}, " +
                $"{now.RepublicanMonth}, " +
                $"{now.RepublicanYear}; " +
                $"{now.DayName}, " +
                $"{now.MonthName}";

            IoC.TTS.Speak(nowSpeak);
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            SetupControlsPositions();

            this.clockView.OnPageSizeChanged(sender, args);
        }

        private bool OnTimerTick()
        {
            var now = Pallettaro.Revo.DateTime.Now;

            var changeDay = now.RepublicanHours.Equals(0) && now.RepublicanMinutes.Equals(0) && now.RepublicanSeconds.Equals(0);
            var dayLabelContainsHour = FormatSettings.ShortFormat.Contains("h") || FormatSettings.ShortFormat.Contains("m") || FormatSettings.ShortFormat.Contains("s");

            if (changeDay || dayLabelContainsHour || firstTick) {
                dateLabel.Text = now.ToString(FormatSettings.ShortFormat);
                dateNameLabel.Text = now.DayName;
                backgroundImage.Source = $"m{now.RepublicanMonth.ToString("00")}.jpg";

                firstTick = false;
            }

            clockView.OnTimerTick(now);

            return true;
        }
    }

}
