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
    public class InfoService : IInfoService
    {
        public string Version
        {
            get
            {              
                var context = MainActivity.Instance;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName.ToString();
            }
        }

        //public string Build
        //{
        //    get
        //    {
        //        var context = MainActivity.Instance;
        //        return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode.ToString();
        //    }
        //}
    }
}