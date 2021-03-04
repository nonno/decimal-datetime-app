using System;
using DecimalTime.Forms.Controls;
using DecimalTime.Forms.Converters;
using DecimalTime.Forms.i18n;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using DecimalTime.Forms.Views;
using Xamarin.Forms;

namespace DecimalTime.Forms.Pages
{
    public class MainPage : ContentPage
    {
        public const string RefreshUiEvent = nameof(RefreshUiEvent);

        protected ISettingsProvider settingsProvider;

        protected AbsoluteLayout contentContainer;

        protected Label dateLabel;
        protected Label dateNameLabel;

        protected ClockView clockView;
        protected Image backgroundImage;
        protected Button settingsButton;

        private readonly TapGestureRecognizer pageDoubleTapRecognizer;

        public MainPage(ISettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;

            var pageModel = new MainPageModel(Navigation, IoC.Analytics, IoC.Settings, IoC.TTS);
            BindingContext = pageModel;
            pageModel.Initialize();

            SetupControls();

            SetupBindings();

            MessagingCenter.Subscribe<SettingsPageModel>(this, RefreshUiEvent, (sender) => {
                // TODO temporary fix, downgrade clockView.SetupBindings to private
                SetupBindings();
                clockView.SetupBindings();
            });

            pageDoubleTapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            pageDoubleTapRecognizer.Tapped += Page_DoubleTapped;

            SizeChanged += OnPageSizeChanged;
        }

        private void SetupControls()
        {
            contentContainer = new AbsoluteLayout();

            clockView = new ClockView(settingsProvider);

            backgroundImage = new Image {
                Aspect = Aspect.AspectFill
            };

            dateLabel = new DateLabel {
                FontSize = 24,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            dateNameLabel = new DateLabel {
                FontSize = 24,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            settingsButton = new SettingsButton {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                ImageSource = AppAssets.settingsIco
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

            this.Content = contentContainer;
        }

        private void SetupBindings()
        {
            try {
                clockView.SetBinding(ClockView.DecimalDateTimeProperty, nameof(MainPageModel.DecimalDateTime));
                backgroundImage.SetBinding(Image.SourceProperty, nameof(MainPageModel.CalendarImageFile));
                dateLabel.SetBinding(Label.TextProperty, nameof(MainPageModel.DecimalDateTime), converter: new DecimalDateTimeToShortFormatConverter(settingsProvider));
                settingsButton.SetBinding(Button.CommandProperty, nameof(MainPageModel.ShowSettingsCommand));

                this.SetBinding(VisualElement.BackgroundColorProperty, nameof(MainPageModel.BackgroundColor));
                backgroundImage.SetBinding(VisualElement.IsVisibleProperty, nameof(MainPageModel.BackgroundImageVisible));
                dateLabel.SetBinding(Label.TextColorProperty, nameof(MainPageModel.DateLabelColor));
                dateNameLabel.SetBinding(Label.TextProperty, nameof(MainPageModel.DecimalDateTime), converter: new DecimalDateTimeToDayNameConverter());
                dateNameLabel.SetBinding(Label.TextColorProperty, nameof(MainPageModel.DateLabelColor));
            } catch {
                settingsProvider.Reset();
                SetupBindings();
                // TODO risk of loop?
            }
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
            var pageModel = (BindingContext as MainPageModel);

            pageModel?.AlertCurrentDateTimeCommand?.Execute(this);
            pageModel?.SpeakCommand?.Execute(this);
        }

        private void OnPageSizeChanged(object sender, EventArgs args)
        {
            SetupControlsPositions();

            this.clockView.OnPageSizeChanged(sender, args);
        }
    }
}
