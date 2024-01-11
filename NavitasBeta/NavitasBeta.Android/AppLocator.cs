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
using Xamarin.Forms;
namespace NavitasBeta.Droid
{
    public static class AppLocator
    {
        public static App _App;
        public static App InitializedApp
        {
            get
            {
                return _App;
            }
        }
        public static void Init()
        {
            DependencyService.Register<IInfoService, InfoService>();
            DependencyService.Register<IAdapter, Adapter>();
            DependencyService.Register<ParseManager.IParseClientManager, ParseManager.Droid.ParseClientManager>();
          

            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
        }
    }
}