using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParseManager;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NavitasBeta
{
    public partial class App : Application
    {
        private static IAdapter _bluetoothAdapter;
        public static IAdapter BluetoothAdapter { get { return _bluetoothAdapter; } }
        private static IParseClientManager _parsemanagerAdapter;
        public static IParseClientManager ParseManagerAdapter { get { return _parsemanagerAdapter; } }
 
        private static IParseClientManager _parsemanagerAdapterTest;
        public static IParseClientManager ParseManagerAdapterTest { get { return _parsemanagerAdapterTest; } }
        public static IInfoService _infoservice;
        public static IInfoService InfoService { get { return _infoservice; } }
        public static AutomaticLogin _AutomaticLogin;
        public static DeviceComunication _devicecommunication;
        public static MainFlyoutPage _MainFlyoutPage;

        public static ViewModelLocator ViewModelLocator;
        public static GeolocationRequest _geolocationRequest;

        static string _AppConfigurationLevel = "USER";
        public static string AppConfigurationLevel
        {
            get
            {
                return _AppConfigurationLevel;
            }
            set
            {
                _AppConfigurationLevel = value;
                if (_AppConfigurationLevel == "ADVANCED_USER" || _AppConfigurationLevel == "ENG_USER" || _AppConfigurationLevel == "DEALER" || _AppConfigurationLevel == "ENG"|| _AppConfigurationLevel == "DEV")
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanAdvancedUserProperty = true;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanAdvancedUserProperty = true;
                    ViewModelLocator.MainViewModel.IsRequestAdvancedUserEnable = false;
                    ViewModelLocator.MainViewModelTSX.IsRequestAdvancedUserEnable = false;
                }
                else if(Authentication.GetUserCredentials()["UserName"] == "testc5a3nFD43M")
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanAdvancedUserProperty = false;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanAdvancedUserProperty = false;
                    ViewModelLocator.MainViewModel.IsRequestAdvancedUserEnable = false;
                    ViewModelLocator.MainViewModelTSX.IsRequestAdvancedUserEnable = false;
                }
                else
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanAdvancedUserProperty = false;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanAdvancedUserProperty = false;
                    ViewModelLocator.MainViewModel.IsRequestAdvancedUserEnable = true;
                    ViewModelLocator.MainViewModelTSX.IsRequestAdvancedUserEnable = true;
                }
                if ( _AppConfigurationLevel == "ENG_USER" || _AppConfigurationLevel == "DEALER" || _AppConfigurationLevel == "ENG" || _AppConfigurationLevel == "DEV")
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanDealerProperty = true;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanDealerProperty = true;
                }
                else
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanDealerProperty = false;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanDealerProperty = false;
                }
                    

                if (_AppConfigurationLevel == "ENG_USER" || _AppConfigurationLevel == "ENG" || _AppConfigurationLevel == "DEV")
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanEngProperty = true;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanEngProperty = true;
                }
                else
                {
                    ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanEngProperty = false;
                    ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanEngProperty = false;
                }
                    
            }
        }

        public static DateTime liveReloadTime;
        public static string PresentConnectedController;
        public static Dictionary<string, string> MemberOfLists;
        public static bool isSkipAuthorization = false;
        public static DateTime ReleasedDateTime;
        public static DateTime BatteryGroupLastModified;

        public App()
        {
            //Register Syncfusion license from https://www.syncfusion.com/account/downloads press get Unlock key with logged in with Ransom's linkedIn
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY3NDUwQDMxMzcyZTMzMmUzMFl4dEQ2SXRIL0JMVVNBaWdiTkR3ck5RbTJrMEdxQVpTQXlHZEtkN1YycUE9");
            _infoservice = DependencyService.Get<IInfoService>();
            _bluetoothAdapter = DependencyService.Get<IAdapter>();
            _parsemanagerAdapter = DependencyService.Get<IParseClientManager>();
            _parsemanagerAdapterTest = DependencyService.Get<IParseClientManager>(DependencyFetchTarget.NewInstance);
            _devicecommunication = new DeviceComunication();
            ConnectivityService.Initilize();
            BatteryGroupLastModified = ReleasedDateTime = FileManager.GetAssemblyLastModifiedTimeUTC();
            InitializeComponent();
            FileManager.CreatePublicDirectory("Navitas");
            string[] fileList = FileManager.GetNavitasDirectoryFiles();  //Not used here but this will pop up the file access permission screen to be used later
            ViewModelLocator = new ViewModelLocator();

            App.ViewModelLocator.GetParameter("KILOMETERSORMILES").parameterValueString = "Miles";
            App.ViewModelLocator.GetParameterTSX("KILOMETERSORMILES").parameterValueString = "Miles";
            DisplaySettings.GetDisplayStuff();
            MainPage = _MainFlyoutPage = new MainFlyoutPage(); //still not getting why I need to make a static reference available.
            liveReloadTime = DateTime.Now;

            _geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
        }


        //protected override void OnStart()
        //{
        //    System.Diagnostics.Debug.WriteLine("OnStart");
        //    // Handle when your app starts
        //}

        protected override void OnSleep()
        {
            System.Diagnostics.Debug.WriteLine("OnSleep");
            // Handle when your app sleeps
        }

        //protected override void OnResume()
        //{

        //    System.Diagnostics.Debug.WriteLine("OnResume");
        //    // Handle when your app resumes
        //}
    }


    /// <summary>
    /// Required for Custom Renderer to target just this Type
    /// </summary>
    public class DoneEntry : Entry
    {
    }
}
