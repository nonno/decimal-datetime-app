using System;
using DecimalTime.Forms.Utils;
using Xamarin.Forms;

namespace DecimalTime.Forms.Views
{
    public class ClockView : AbsoluteLayout
    {
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

        static readonly HandParams secondParams = new HandParams(0.02, 1.1, 0.85);
        static readonly HandParams minuteParams = new HandParams(0.05, 0.8, 0.9);
        static readonly HandParams hourParams = new HandParams(0.125, 0.65, 0.9);

        private BoxView[] tickMarks = new BoxView[100];
        private BoxView secondHand, minuteHand, hourHand;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        protected override void OnParentSet()
        {
            // Create the tick marks (to be sized and positioned later)
            var tickMarksColor = Color.FromHex(Styles.TickMarksColor);
            for (int i = 0; i < tickMarks.Length; i++) {
                tickMarks[i] = new BoxView { Color = tickMarksColor };
                this.Children.Add(tickMarks[i]);
            }

            // Create the three hands.
            this.Children.Add(hourHand = new BoxView { Color = Color.FromHex(Styles.HoursHandColor) });
            this.Children.Add(minuteHand = new BoxView { Color = Color.FromHex(Styles.MinutesHandColor) });
            this.Children.Add(secondHand = new BoxView { Color = Color.FromHex(Styles.SecondsHandColor) });

            base.OnParentSet();
        }

        public void OnPageSizeChanged(object sender, EventArgs args)
        {
            // Size and position the 12 tick marks.
            Point center = new Point(this.Width / 2, this.Height / 2);
            double radius = 0.45 * Math.Min(this.Width, this.Height);

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

            // Function for positioning and sizing hands.
            Action<BoxView, HandParams> HandLayout = (boxView, handParams) => {
                double width = handParams.Width * radius;
                double height = handParams.Height * radius;
                double offset = handParams.Offset;

                AbsoluteLayout.SetLayoutBounds(boxView,
                    new Rectangle(center.X - 0.5 * width,
                                  center.Y - offset * height,
                                  width, height));

                boxView.AnchorX = 0.51;
                boxView.AnchorY = handParams.Offset;
            };

            HandLayout(secondHand, secondParams);
            HandLayout(minuteHand, minuteParams);
            HandLayout(hourHand, hourParams);
        }
        public bool OnTimerTick()
        {
            // Set rotation angles for hour and minute hands.
            Pallettaro.Revo.DateTime repTime = Pallettaro.Revo.DateTime.Now;
            hourHand.Rotation = 36 * repTime.RepublicanHours + 0.36 * repTime.RepublicanMinutes;
            minuteHand.Rotation = 3.6 * repTime.RepublicanMinutes + 0.036 * repTime.RepublicanSeconds;
            secondHand.Rotation = 3.6 * (repTime.RepublicanSeconds);// + t);
            return true;
        }
    }
}
