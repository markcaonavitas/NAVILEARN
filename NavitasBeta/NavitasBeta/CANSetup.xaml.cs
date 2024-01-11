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
    public partial class CANSetup : ContentPage
    {
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        SetParameterEventArgs SetParameterEventArgs;
        static public ParametersViewModel.PageParameterList PageParameters;
        int pageIndex;

        public CANSetup()
        {
            try
            {
                InitializeComponent();
                PageParameters = new ParametersViewModel.PageParameterList();
                foreach (var v in ViewModelLocator.MainViewModel.GoiParameterList)
                {
                    var b = this.FindByName<Object>(v.PropertyName);
                    if (b != null)
                    {
                        PageParameters.parameterList.Add(v);
                        (b as BindableObject).BindingContext = v;
                    }
                }
                pageIndex = DeviceComunication.AddToPacketList(PageParameters);
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            BindingContext = ViewModelLocator.MainViewModel;
        }

        private void ParameterFocused(object sender, EventArgs e)
        {
            ((sender as VisualElement).BindingContext as GoiParameter).bupdate = false;
        }

        private void ParameterUnfocused(object sender, EventArgs e)
        {
            ((sender as VisualElement).BindingContext as GoiParameter).bupdate = true;
        }

        private void ParameterCompleted(object sender, EventArgs e)
        {
            try
            {
                float fParmeterValue = ((sender as VisualElement).BindingContext as GoiParameter).parameterValue;

                SetParameterEventArgs = new SetParameterEventArgs(((sender as VisualElement).BindingContext as GoiParameter).Address, fParmeterValue, "never used so get rid of this");
                AddParamValuesToQueue(this, SetParameterEventArgs);
                (sender as VisualElement).Unfocus();

            }
            catch (ArgumentNullException)
            {

            }
            catch (FormatException f)
            {
                DisplayAlert("Input Error", f.Message, "OK");
            }
        }
    }
}