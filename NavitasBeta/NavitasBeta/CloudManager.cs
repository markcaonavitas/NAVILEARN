using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace NavitasBeta
{
    public static class CloudManager
    {
    }
    public static class ConnectivityService
    {
        private static StoppableTimer stoppableTimer;
        public static void Initilize()
        {
            Connectivity.ConnectivityChanged += ConnectivityChanged;
            stoppableTimer = new StoppableTimer(TimeSpan.FromSeconds(3), EnsureInternetIsStable);
            stoppableTimer.StartWithLoop();
        }

        private static void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"NetworkAccess state: {e.NetworkAccess} ");
        }

        private static int stabilityCounter = 0;
        public static void CheckInternetStabilityAndCleanUpRemaningFileUpload()
        {
            stabilityCounter = 0;
            stoppableTimer.StartWithLoop();
        }

        private static void EnsureInternetIsStable()
        {
            if (stabilityCounter >= 3)
            {
                // We have a stable internet connection, upload the saved data
                //System.Diagnostics.Debug.WriteLine($"Calling UploadRemainingFiles");
                // There is a case that file has been uploaded successfully, while internet stability checking still occurring
                stoppableTimer.Stop();
                FileManager.UploadRemainingCSVFiles();
            }

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                //System.Diagnostics.Debug.WriteLine($"Check Internet stability for {stabilityCounter} time(s)");
                stabilityCounter++;
            }
            else stabilityCounter = 0;
        }
    }
}
