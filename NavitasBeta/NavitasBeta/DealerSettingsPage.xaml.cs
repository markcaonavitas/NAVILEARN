using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Essentials;
namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealerSettingsPage : NavitasGeneralPage
    {
        public DealerSettingsPage()
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
                System.Diagnostics.Debug.WriteLine("Debug this exception: DealerSettingsPage.xaml.cs" + ex.Message);

            }
        }
        async void OnRequestAdvancedAccessButtonClicked(object sender, EventArgs e)
        {
            BuildWarningPopUp("Disclaimer");
        }
    }
}