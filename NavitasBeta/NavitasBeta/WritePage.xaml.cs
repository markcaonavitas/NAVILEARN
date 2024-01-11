using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Xml.Serialization;

using System.ComponentModel;


namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WritePage : NavitasGeneralPage
    {
        public WritePage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTAC();
                //AddActivityPopUp();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: WritePage.xaml.cs" + ex.Message);
            }
        }
    }
}