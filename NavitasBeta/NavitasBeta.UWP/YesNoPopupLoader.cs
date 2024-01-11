using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Controls;
using NavitasBeta.UWP;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(YesNoPopupLoader))]

namespace NavitasBeta.UWP
{
    class YesNoPopupLoader : IYesNoPopupLoader
    {
        public void ShowPopup(CustomYesNoBox popup)
        {
            //ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            //XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            //XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            //toastTextElements[0].AppendChild(toastXml.CreateTextNode("hi there"));

            //XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/Logo.scale-240.png");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "logo");

            //IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            //((XmlElement)toastNode).SetAttribute("duration", "short");

            //var toastNavigationUriString = "#/MainPage.xaml?param1=12345";
            //var toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
            //toastElement.SetAttribute("launch", toastNavigationUriString);

            //ToastNotification toast = new ToastNotification(toastXml);

            //ToastNotificationManager.CreateToastNotifier().Show(toast);
            
            var alert = new ContentDialog
            {
                Title = popup.Title,
                Content = popup.Text,
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };
            //foreach (var b in popup.Buttons)
            //    alert.AddHandler(b.);

            alert.CloseButtonClick += (s, args) =>
            {
                popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                {
                    Button = "No"
                });
            };
            alert.PrimaryButtonClick += (s, args) =>
            {
                popup.OnPopupClosed(new CustomYesNoBoxClosedArgs
                {
                    Button = "Yes"
                });
            };
            alert.ShowAsync();

        }
    } 
}