using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealerTabbedPageTSX : TabbedPage
    {
        public DealerTabbedPageTSX()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            BindingContext = ViewModelLocator.MainViewModelTSX;
            if (Device.OS == TargetPlatform.iOS)
            {
                MessagingCenter.Subscribe<DeviceComunication>(this, "Hi", (sender) =>
                {
                    // do something whenever the "Hi" message is sent
                    DisplayMessage();
                });
            }

        }

        void DisplayMessage()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                this.DisplayAlert("Error", "Device Communication timeout", "OK");
            });
        }
    }
}