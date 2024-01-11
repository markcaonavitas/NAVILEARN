using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
#if OLD_VEHICLE_LOCKING_FEATURE
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleLockPage : ContentPage
    {
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        bool EnableOnSwitchToggled;
        public VehicleLockPage()
        {
            InitializeComponent();

            BindingContext = ViewModelLocator.MainViewModel;
            EnableOnSwitchToggled = false;
        }

        protected override void OnAppearing()
        {

            passwordEntry.Text = null;
            LockSwitch.IsEnabled = false;
            base.OnAppearing();
        }
        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if(passwordEntry.Text == ViewModelLocator.MainViewModel.VEHICLELOCKPASSWORD.ToString())
            {
                LockSwitch.IsEnabled = true;
            }
        }

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            if(EnableOnSwitchToggled == false)
            {
                EnableOnSwitchToggled = true;
                return;
            }
            float togggled = 0.0f;
            if (LockSwitch.IsToggled)
                togggled = 1.0f;
            SetParameterEventArgs SetParameterEventArgs = new SetParameterEventArgs(164, togggled, null);

            AddParamValuesToQueue(this, SetParameterEventArgs);

            SetParameterEventArgs = new SetParameterEventArgs(50, 1.0f, null);
            AddParamValuesToQueue(this, SetParameterEventArgs);
            System.Diagnostics.Debug.WriteLine("OnSwitchToggled");

        }
    }
#endif 
}
