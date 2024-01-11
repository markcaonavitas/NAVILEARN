using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WriteTSXPage : NavitasGeneralPage
    {
        public WriteTSXPage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTSX();
                AddActivityPopUp();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: WriteTSXPAge.xaml.cs" + ex.Message);
            }
        }
        private void SAVECHANGES_Clicked(object sender, EventArgs e)
        {

            Task<bool> task = DisplayAlert("Save Changes", "Settings will be permanently saved to controller flash", "Yes", "Cancel");

            task.ContinueWith(SaveDismissedCallback);
        }
        void SaveDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                QueParameter(new SetParameterEventArgs(199, 1.0f, null));

            }
        }
    }
}