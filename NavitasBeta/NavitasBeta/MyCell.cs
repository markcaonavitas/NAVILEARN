using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NavitasBeta
{
    public class NativeCell : ViewCell 
    {

        public static readonly BindableProperty TextProperty =
        BindableProperty.Create("Text", typeof(string), typeof(NativeCell), "");

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value);}
        }

        public static readonly BindableProperty DetailProperty =
        BindableProperty.Create("Detail", typeof(string), typeof(NativeCell), "");

        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }
        public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create("BackgroundColor", typeof(Color), typeof(NativeCell), new Color());
        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        public NativeCell() : base()
        {


            var minLabel = new Label()
            {
              //  FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
                //       YAlign = TextAlignment.Center,
                //         XAlign = TextAlignment.End,
            };
            var maxLabel = new Label()
            {
              //  FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                //          YAlign = TextAlignment.Center
            };

            minLabel.BindingContext = this;
            maxLabel.BindingContext = this;
            minLabel.SetBinding(Label.TextProperty,"Text");
       //     minLabel.SetBinding(Label.BackgroundColorProperty, "BackgroundColor");
            maxLabel.SetBinding(Label.TextProperty,"Detail");
        //    maxLabel.SetBinding(Label.BackgroundColorProperty, "BackgroundColor");

            View = new StackLayout
            {
               // HeightRequest = 120,
               // Spacing = 0,

                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
         //       Padding = new Thickness(15, 5, 5, 30),
                Children =
                {
                    new StackLayout
                    {
              //          HeightRequest = 120,
                //        Spacing = 0,
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            minLabel, maxLabel
                        }
                    },
                }
            };
            View.BindingContext = this;

         //   View.BackgroundColor = Color.Green;
            View.SetBinding(View.BackgroundColorProperty, "BackgroundColor");
        }
    }
}
