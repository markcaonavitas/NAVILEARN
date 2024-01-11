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

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealerSettingsTSXPage : NavitasGeneralPage
    {
        public DealerSettingsTSXPage()
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
                System.Diagnostics.Debug.WriteLine("Debug this exception: DealerSettingsTSXPage.xaml.cs" + ex.Message);
            }
        }

        async void OnRequestAdvancedAccessButtonClicked(object sender, EventArgs e)
        {
            BuildWarningPopUp("Disclaimer");
        }
    }
}