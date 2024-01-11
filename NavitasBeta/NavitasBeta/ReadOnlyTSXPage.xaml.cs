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
    public partial class ReadOnlyTSXPage : NavitasGeneralPage
    {
        public ReadOnlyTSXPage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTSX();
                AddActivityPopUp();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: ReadOnlyTSXPage.xaml.cs" + ex.Message);
            }
        }
    }
}