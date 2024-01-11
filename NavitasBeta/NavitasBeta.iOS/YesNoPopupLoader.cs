using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;

using NavitasBeta.iOS;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(YesNoPopupLoader))]
namespace NavitasBeta.iOS
{
    public class YesNoPopupLoader : IYesNoPopupLoader
    {
        public void ShowPopup(CustomYesNoBox popup)
        {
            var alert = new UIAlertView
            {
                Title = popup.Title,
                Message = popup.Text
               
            };
            foreach (var b in popup.Buttons)
                alert.AddButton(b);
         
            alert.Clicked += (s, args) =>
            {
                popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                {
                    Button = popup.Buttons.ElementAt(Convert.ToInt32(args.ButtonIndex)),
                });
            };
            alert.Show();
        }
    }
}