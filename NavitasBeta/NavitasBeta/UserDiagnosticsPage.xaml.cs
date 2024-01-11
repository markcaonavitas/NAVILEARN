using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDiagnosticsPage : NavitasGeneralPage
    {
        public UserDiagnosticsPage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTAC();
                AddActivityPopUp();
                BuildErrorIcon();
                CONTROLLERERROR.ParentAction += TACDisplayFaultMessage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: UserDiagnosticsPage.xaml.cs" + ex.Message);
            }
        }
    }
}
