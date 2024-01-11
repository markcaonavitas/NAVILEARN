using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace NavitasBeta
{
    public class CustomImage : Xamarin.Forms.Image
    {
        public event EventHandler<EventArgs> ParentAction = delegate { };
#if OLD
        public static Xamarin.Forms.BindableProperty OnClickProperty =
            BindableProperty.Create("OnClick", typeof(Command), typeof(CustomImage));


        public static Xamarin.Forms.BindableProperty CustomNameProperty =
     BindableProperty.Create("CustomName", typeof(String), typeof(CustomImage));


        public Command OnClick
        {
            get { return (Command)GetValue(OnClickProperty); }
            set { SetValue(OnClickProperty, value); }
        }
#endif
        public CustomImage()
        {
            System.Diagnostics.Debug.WriteLine("CustomImage()");

            GestureRecognizers.Add(new TapGestureRecognizer()
            {
               
                Command = new Command(DisTap),
                //    CommandParameter = GetValue(NameProperty)
            });
        }

        private void DisTap(object sender)
        {
#if OLD
            string str = (string)GetValue(CustomNameProperty);
            CustomEventArgs e = new CustomEventArgs();
            e.strImageName = str;
#endif
            System.Diagnostics.Debug.WriteLine("DisTap(object sender)" );
            ParentAction(this, null);
#if OLD
            if (OnClick != null)
            {
                OnClick.Execute(str);
            }
        }
#endif

        }
    }
#if OLD
    public class CustomEventArgs : EventArgs
    {
        public string strImageName;
    }
#endif

}
