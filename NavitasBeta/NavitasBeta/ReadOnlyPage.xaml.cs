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
    public partial class ReadOnlyPage : NavitasGeneralPage
    {
        public ReadOnlyPage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTAC();
                AddActivityPopUp();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: ReadOnlyPage.xaml.cs" + ex.Message);
            }
        }
    }
}