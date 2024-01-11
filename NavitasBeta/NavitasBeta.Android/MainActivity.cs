using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using OxyPlot;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android;
using Android.Support.V4.App;
using Android.Support.V4.Content;
//using Plugin.FilePicker; //I don't like it... Too much information and ability for user


namespace NavitasBeta.Droid
{

    //   [Activity(Label = "NavitasBeta")]
    // [Activity(Label = "NavitasBeta", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    [Activity(Label = "Navitas", Icon = "@drawable/icon", Theme = "@style/MainTheme",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.FullSensor)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static MainActivity Instance { get; private set; }

        protected override void OnStart()
        {
            base.OnStart();
        }

        private async Task CheckAppPermissions()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted ||
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted ||
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted ||
                ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothScan) != Permission.Granted) && (int)Build.VERSION.SdkInt >= 31) ||
                ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.BluetoothConnect) != Permission.Granted) && (int)Build.VERSION.SdkInt >= 31) ||
                ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                await RequestLocationWithDisclousure();
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage,
                                                                        Manifest.Permission.ReadExternalStorage,
                                                                        Manifest.Permission.AccessCoarseLocation,
                                                                        Manifest.Permission.AccessFineLocation, 
                                                                        Manifest.Permission.BluetoothScan,
                                                                        Manifest.Permission.BluetoothConnect}, 0);
            }
            else
            {
                permissionsDone = true;
            }
        }
        public async Task<bool> RequestLocationWithDisclousure()
        {
            var tcs = new TaskCompletionSource<bool>();

            using (var alert = new AlertDialog.Builder(this))
            {
                alert.SetTitle("Location Disclosure");
                alert.SetMessage("Navitas Dashboard uses location services to allow Bluetooth communication with our products.\n\nGeographic location is not identified, collected, or stored on your device or our servers when the app is closed or not in use.");
                alert.SetPositiveButton("Next", (sender, args) => {
                    tcs.TrySetResult(true);
                });
                //alert.SetNegativeButton("Cancel", (sender, args) => { tcs.TrySetResult(false); });
                alert.Show();
            }

            return await tcs.Task;
        }

        bool permissionsDone = false;
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            permissionsDone = true;
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Instance = this;

            if ((int)Build.VERSION.SdkInt < 23)
            {
                return;
            }
            else
            {
                await CheckAppPermissions();
                while (!permissionsDone)
                {
                    await Task.Delay(1);
                }

            }
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY3NDUwQDMxMzcyZTMzMmUzMFl4dEQ2SXRIL0JMVVNBaWdiTkR3ck5RbTJrMEdxQVpTQXlHZEtkN1YycUE9");
//#if EXCEPTION_HANDLERS
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Task exception from {0}: {1}", s.GetType(), e.Exception.ToString());
                if (!e.Observed)
                    e.SetObserved();
            };

            //Test1
            AppDomain.CurrentDomain.UnhandledException += HandleExceptions;

            //Test2
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;


            //#endif 
            DependencyService.Register<IInfoService, InfoService>();
            DependencyService.Register<IAdapter, Adapter>();
            DependencyService.Register<ParseManager.IParseClientManager, ParseManager.Droid.ParseClientManager>();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();


                LoadApplication(new App());
            //LoadApplication(AppLocator.InitializedApp);
            Xamarin.Essentials.Platform.Init(this, bundle);
        }
//#if EXCEPTION_HANDLERS
        static void HandleExceptions(object sender, UnhandledExceptionEventArgs e)
        {
            //Exception d = (Exception)e.ExceptionObject;
            Console.WriteLine("TEST");
        }

        void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
            Console.Write("HANDLED EXCEPTION");
        }
        public override void OnBackPressed()
        {
            return;
        }//#endif 

    }
}

