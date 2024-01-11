using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : CustomTabbedPage
    {
        public bool TabsInitialized = false;
        public bool EngTabsInitialized = false;
        public bool SettingsTabsInitialized = false;
        public UserTabbedPage()
        {
            try
            {
                InitializeComponent();
                On<Android>().SetOffscreenPageLimit(8); //fixes slow tab changes on Android (ENGINEERING has more then for tabs)
                                                        //.SetIsSwipePagingEnabled(true)
                                                        //.SetIsSmoothScrollEnabled(false)
                                                        //.SetToolbarPlacement(ToolbarPlacement.Bottom);
                                                        //On<Android>().SetIsSwipePagingEnabled(false);
                On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                //.SetBarItemColor(Color.Black)
                //.SetBarSelectedItemColor(Color.Red);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: UserTabbedPage.xaml.cs" + ex.Message);
            }
        }
    }
}