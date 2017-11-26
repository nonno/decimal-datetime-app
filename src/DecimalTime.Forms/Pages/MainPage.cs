using System;
using DecimalTime.Core.Converters;
using DecimalTime.Core.i18n;
using DecimalTime.Core.Utils;
using DecimalTime.Core.ViewModels;
using DecimalTime.Core.Views;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace DecimalTime.Core.Pages
{
    public class MainPage : MvxContentPage<MainViewModel>
    {
        private AbsoluteLayout contentContainer;

        private Label dateLabel;
        private Label dateNameLabel;

        private ClockView clockView;
        private Image backgroundImage;
        private Button settingsButton;

        private readonly TapGestureRecognizer pageDoubleTapRecognizer;

        public MainPage()
        {
            SetupControls();

            SetupBindings();

            pageDoubleTapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            pageDoubleTapRecognizer.Tapped += Page_DoubleTapped;

            SizeChanged += OnPageSizeChanged;
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

        private void SetupBindings()
        {
            clockView.SetBinding(ClockView.DecimalDateTimeProperty, nameof(MainViewModel.DecimalDateTime), BindingMode.TwoWay);
            backgroundImage.SetBinding(Image.SourceProperty, nameof(MainViewModel.CalendarImageFile));
            dateLabel.SetBinding(Label.TextProperty, nameof(MainViewModel.DecimalDateTime), BindingMode.OneWay, new DecimalDateTimeToShortFormatConverter());
            dateNameLabel.SetBinding(Label.TextProperty, nameof(MainViewModel.DecimalDateTime), BindingMode.OneWay, new DecimalDateTimeToDayNameConverter());

            settingsButton.SetBinding(Button.CommandProperty, nameof(MainViewModel.ShowSettingsCommand));
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

            this.contentContainer.GestureRecognizers.Add(pageDoubleTapRecognizer);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.contentContainer.GestureRecognizers.Remove(pageDoubleTapRecognizer);
        }

        private void Page_DoubleTapped(object sender, EventArgs e)
        {
            var viewModel = (BindingContext.DataContext as MainViewModel);

            viewModel?.AlertCurrentDateTimeCommand?.Execute();
            viewModel?.SpeakCommand?.Execute();
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            SetupControlsPositions();

            this.clockView.OnPageSizeChanged(sender, args);
        }
    }
}