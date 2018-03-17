using Xamarin.Forms;

namespace DecimalTime.Forms.Services
{
	public class StyleProvider
	{
		public TextStyle Accent
		{
			get => new TextStyle
			{
                BackgroundColor = Color.FromHex("#ff2f2f2f"),
                TextColor = Color.FromHex("#ff7cc7bc"),
				TextSize = 18.0,
				TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5, 0, 5, 0)
			};
		}

		public string FontFamily
		{
			get {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        return "HelveticaNeue-Light";
                    case Device.Android:
                        return "sans-serif-light";
                    default:
                        return "sans-serif";
                }
            }
		}

		public TextStyle Regular
		{
            get => new TextStyle {
                BackgroundColor = Color.White,
                TextColor = Color.FromRgb(55, 55, 55),
                TextSize = 18.0,
                TextAlignment = TextAlignment.Start,
                Margin = new Thickness(5, 5, 5, 5)
            };
		}

        public TextStyle Editor
        {
            get {
                double textSize;
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        textSize = 16;
                        break;
                    default:
                        textSize = 14;
                        break;
                }

                return new TextStyle {
                    BackgroundColor = Color.White,
                    TextColor = Color.FromRgb(33, 33, 33),
                    TextSize = textSize,
                    TextAlignment = TextAlignment.Start,
                    Margin = Regular.Margin
                };
            }
        }

        public Style GetStyleForButtons()
		{
			return new Style(typeof(Button)) {
				Setters = {
					new Setter { Property = Button.FontFamilyProperty, Value = FontFamily },
					new Setter { Property = Button.BackgroundColorProperty, Value = Accent.BackgroundColor },
					new Setter { Property = Button.TextColorProperty, Value = Accent.TextColor },
					new Setter { Property = Button.FontSizeProperty, Value = Accent.TextSize },
					new Setter { Property = Button.CornerRadiusProperty, Value = 0 },
                    new Setter { Property = Button.MarginProperty, Value = Regular.Margin }
                }
			};
		}

		public Style GetStyleForLabels()
		{
			return new Style(typeof(Label)) {
				Setters = {
					new Setter { Property = Label.FontFamilyProperty, Value = FontFamily },
					new Setter { Property = Label.BackgroundColorProperty, Value = Color.Transparent },
					new Setter { Property = Label.TextColorProperty, Value = Regular.TextColor },
					new Setter { Property = Label.FontSizeProperty, Value = Regular.TextSize },
                    new Setter { Property = Label.MarginProperty, Value = Regular.Margin }
				}
			};
		}

        public Style GetStyleForPages()
		{
			return new Style(typeof(Page)) {
				Setters = {
					new Setter { Property = Page.BackgroundColorProperty, Value = Regular.BackgroundColor },
				}
			};
		}

		public Style GetStyleForEntries()
		{
			return new Style(typeof(Entry)) {
				Setters = {
					new Setter { Property = Entry.BackgroundColorProperty, Value = Editor.BackgroundColor },
					new Setter { Property = Entry.TextColorProperty, Value = Editor.TextColor },
                    new Setter { Property = Entry.FontSizeProperty, Value = Editor.TextSize },
                    new Setter { Property = Entry.MarginProperty, Value = Editor.Margin }
				}
			};
		}

        public Style GetStyleForEditors()
        {
            return new Style(typeof(Editor)) {
                Setters = {
                    new Setter { Property = Xamarin.Forms.Editor.BackgroundColorProperty, Value = Editor.BackgroundColor },
                    new Setter { Property = Xamarin.Forms.Editor.TextColorProperty, Value = Editor.TextColor },
                    new Setter { Property = Xamarin.Forms.Editor.FontSizeProperty, Value = Editor.TextSize },
                    new Setter { Property = Xamarin.Forms.Editor.MarginProperty, Value = Editor.Margin }
                }
            };
        }

        public Style GetStyleForSwitches()
        {
            return new Style(typeof(Switch)) {
                Setters = {
                    new Setter { Property = Xamarin.Forms.Editor.MarginProperty, Value = Editor.Margin }
                }
            };
        }
	}

    public class TextStyle
    {
        public Color TextColor { get; set; }
        public double TextSize { get; set; }
        public Color BackgroundColor { get; set; }
        public TextAlignment TextAlignment { get; set; }
        public Thickness Margin { get; set; }
    }
}
