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
	public partial class UserDiagnosticsPageTSX : NavitasGeneralPage
    {
        public UserDiagnosticsPageTSX()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTSX();
                AddActivityPopUp();
                BuildErrorIcon();
                CONTROLLERERROR.ParentAction += TSXDisplayFaultMessage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: UserDiagnosticsPageTSX.xaml.cs" + ex.Message);
            }
        }

        //public void ParameterCompleted(object sender, EventArgs e)
        //{
        //    App.ViewModelLocator.GetParameterTSX("FIRMWARETEST").parameterValueString = ModelNumberEntry.Text;
        //}
    }
}