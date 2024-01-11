using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomModal : ContentView
    {
        public CustomModal()
        {
            InitializeComponent();
        }

        private void DeviceTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            App._MainFlyoutPage._DeviceListPage.DemoDeviceTapped(null, e);
        }
        public void Show()
        {
            this.IsVisible = true;
        }

        public void Hide()
        {
            this.IsVisible = false;
        }

        private void btnCancelClicked(object sender, EventArgs e)
        {
            Hide();
        }
    }

    public class CustomModalViewModel : ViewModelBase
    {
        public ObservableCollection<IDevice> AppDemos { get; }

        public CustomModalViewModel()
        {
            AppDemos = new ObservableCollection<IDevice>()
            {
                new DemoDevice() { Name = "TAC AC Controller DEMO" },
                new DemoDevice() { Name = "TSX DC Controller DEMO" },
            };
        }
    }

}