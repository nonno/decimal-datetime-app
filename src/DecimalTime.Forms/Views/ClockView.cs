using System;
using System.Linq;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Views
{
    public class ClockView : AbsoluteLayout
    {
        public static readonly BindableProperty DecimalDateTimeProperty = BindableProperty.Create(nameof(DecimalDateTime), typeof(Pallettaro.Revo.DateTime), typeof(ClockView), propertyChanged: DecimalDateTimePropertyChanged);
        public Pallettaro.Revo.DateTime DecimalDateTime
        {
            get => (Pallettaro.Revo.DateTime)GetValue(DecimalDateTimeProperty);
            set => SetValue(DecimalDateTimeProperty, value);
        }

        private BoxView secondsHand;
        private BoxView minutesHand;
        private BoxView hoursHand;

        private BoxView[] tickMarks = new BoxView[100];

        private ISettingsProvider _settingsProvider;

        public ClockView(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        protected override void OnParentSet()
        {
            CreateElements();
            SetupBindings();

            base.OnParentSet();
        }

        private void CreateElements()
        {
            for (int i = 0; i < tickMarks.Length; i++) {
                tickMarks[i] = new BoxView { BindingContext = _settingsProvider };
                this.Children.Add(tickMarks[i]);
            }

            this.Children.Add(hoursHand = new BoxView { BindingContext = _settingsProvider });
            this.Children.Add(minutesHand = new BoxView { BindingContext = _settingsProvider });
            this.Children.Add(secondsHand = new BoxView { BindingContext = _settingsProvider });
        }

        public void SetupBindings()
        {
            foreach (var tickMark in tickMarks) {
                tickMark.SetBinding(BoxView.ColorProperty, nameof(SettingsProvider.TickMarksColor));
            }
            hoursHand.SetBinding(BoxView.ColorProperty, nameof(SettingsProvider.HoursHandColor));
            minutesHand.SetBinding(BoxView.ColorProperty, nameof(SettingsProvider.MinutesHandColor));
            secondsHand.SetBinding(BoxView.ColorProperty, nameof(SettingsProvider.SecondsHandColor));
        }

        public void OnPageSizeChanged(object sender, EventArgs args)
        {
            var center = new Point(this.Width / 2, this.Height / 2);
            var radius = 0.45 * Math.Min(this.Width, this.Height);

            SetupTickMarks(center, radius);
            SetupHands(center, radius);
        }

        private static void DecimalDateTimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as ClockView).OnTimerTick();
        }
        public void OnTimerTick()
        {
            var dateTime = DecimalDateTime;

            hoursHand.Rotation = 36 * dateTime.RepublicanHours + 0.36 * dateTime.RepublicanMinutes;
            minutesHand.Rotation = 3.6 * dateTime.RepublicanMinutes + 0.036 * dateTime.RepublicanSeconds;
            secondsHand.Rotation = 3.6 * (dateTime.RepublicanSeconds);
        }

        private void SetupTickMarks(Point center, double radius)
        {
            for (int i = 0; i < tickMarks.Length; i++) {
                double size = radius / (i % 10 == 0 ? 16 : 40);
                double radians = i * 2 * Math.PI / tickMarks.Length;
                double x = center.X + radius * Math.Sin(radians) - size / 2;
                double y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(tickMarks[i], new Rectangle(x, y, size, size));

                tickMarks[i].AnchorX = 0.51;        // Anchor settings necessary for Android
                tickMarks[i].AnchorY = 0.51;
                tickMarks[i].Rotation = 180 * radians / Math.PI;
            }
        }

        private void SetupHands(Point center, double radius)
        {
            Action<BoxView, HandParams> HandLayout = (boxView, handParams) => {
                double width = handParams.Width * radius;
                double height = handParams.Height * radius;
                double offset = handParams.Offset;

                var bounds = new Rectangle(center.X - 0.5 * width, center.Y - offset * height, width, height);
                AbsoluteLayout.SetLayoutBounds(boxView, bounds);

                boxView.AnchorX = 0.51;
                boxView.AnchorY = handParams.Offset;
            };

            HandLayout(secondsHand, new HandParams(0.02, 1.1, 0.85));
            HandLayout(minutesHand, new HandParams(0.05, 0.8, 0.9));
            HandLayout(hoursHand, new HandParams(0.125, 0.65, 0.9));
        }

        private struct HandParams
        {
            public HandParams(double width, double height, double offset) : this()
            {
                Width = width;
                Height = height;
                Offset = offset;
            }

            public double Width { private set; get; }   // fraction of radius
            public double Height { private set; get; }  // ditto
            public double Offset { private set; get; }  // relative to center pivot
        }
    }
}
