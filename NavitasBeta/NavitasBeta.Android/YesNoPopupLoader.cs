using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using NavitasBeta.Droid;

[assembly: Dependency(typeof(YesNoPopupLoader))]

namespace NavitasBeta.Droid
{
    public class YesNoPopupLoader : IYesNoPopupLoader
    {
        public void ShowPopup(CustomYesNoBox popup)
        {
            var alert = new AlertDialog.Builder(MainActivity.Instance);

            var textView = new TextView(MainActivity.Instance) { Text = popup.Text };

            var textViewTitle = new TextView(MainActivity.Instance) { Text = popup.Title };
            textViewTitle.Gravity = GravityFlags.Center;
            textViewTitle.SetTypeface(null,Android.Graphics.TypefaceStyle.Bold);
            textView.Gravity = GravityFlags.Center;
           
            alert.SetView(textView);

            //   alert.SetTitle(popup.Title);
            alert.SetCustomTitle(textViewTitle);
            var buttons = popup.Buttons;

            alert.SetPositiveButton(buttons[0], (senderAlert, args) =>
            {
                popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                {
                    Button = buttons[0]
                });
            });

            alert.SetNegativeButton(buttons[1], (senderAlert, args) =>
            {
                popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                {
                    Button = buttons[1]
                });
            });

            if(buttons.Count == 3)
            {
                alert.SetNeutralButton(buttons[2], (senderAlert, args) =>
                {
                    popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                    {
                        Button = buttons[2]
                    });
                });
            }
            

            alert.SetCancelable(false);
            alert.Show();
        }
    }
}
