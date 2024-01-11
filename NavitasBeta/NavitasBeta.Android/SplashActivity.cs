using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android.Util;

namespace NavitasBeta.Droid
{
    //  [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
        {
            static readonly string TAG = "X:" + typeof(SplashActivity).Name;

            public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
            {
                base.OnCreate(savedInstanceState, persistentState);
        //        Log.Debug(TAG, "SplashActivity.OnCreate");
            }
        protected override void OnStart()
        {
            base.OnStart();
        }


        // Launches the startup task
        protected override void OnResume()
            {
                base.OnResume();
                Task startupWork = new Task(() => { SimulateStartup(); });
                startupWork.Start();
    //        AppLocator._App = new App();
            }

            // Simulates background work that happens behind the splash screen
            void SimulateStartup()
            {
              //  Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
       //         await Task.Delay(8000); // Simulate a bit of startup work.
              //  Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
        }
}