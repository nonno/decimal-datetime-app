using System;
using DecimalTime.Core.Utils;
using Xamarin.Forms;

namespace DecimalTime.Core.Views
{
    public class ClockView : AbsoluteLayout
    {
        public static readonly BindableProperty DecimalDateTimeProperty = BindableProperty.Create(nameof(DecimalDateTime), typeof(Pallettaro.Revo.DateTime), typeof(ClockView), propertyChanged: DecimalDateTimePropertyChanged);
        public Pallettaro.Revo.DateTime DecimalDateTime
        {
            get => (Pallettaro.Revo.DateTime)GetValue(DecimalDateTimeProperty);
            set => SetValue(DecimalDateTimeProperty, value);
        }

        private BoxView secondHand;
        private BoxView minuteHand;
        private BoxView hourHand;

        private BoxView[] tickMarks = new BoxView[100];

        protected override void OnParentSet()
        {
            CreateElements();

            base.OnParentSet();
        }

        private void CreateElements()
        {
            var tickMarksColor = Color.FromHex(Styles.TickMarksColor);
            for (int i = 0; i < tickMarks.Length; i++) {
                tickMarks[i] = new BoxView { Color = tickMarksColor };
                this.Children.Add(tickMarks[i]);
            }

            this.Children.Add(hourHand = new BoxView { Color = Color.FromHex(Styles.HoursHandColor) });
            this.Children.Add(minuteHand = new BoxView { Color = Color.FromHex(Styles.MinutesHandColor) });
            this.Children.Add(secondHand = new BoxView { Color = Color.FromHex(Styles.SecondsHandColor) });
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

            hourHand.Rotation = 36 * dateTime.RepublicanHours + 0.36 * dateTime.RepublicanMinutes;
            minuteHand.Rotation = 3.6 * dateTime.RepublicanMinutes + 0.036 * dateTime.RepublicanSeconds;
            secondHand.Rotation = 3.6 * (dateTime.RepublicanSeconds);
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

            HandLayout(secondHand, new HandParams(0.02, 1.1, 0.85));
            HandLayout(minuteHand, new HandParams(0.05, 0.8, 0.9));
            HandLayout(hourHand, new HandParams(0.125, 0.65, 0.9));
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
