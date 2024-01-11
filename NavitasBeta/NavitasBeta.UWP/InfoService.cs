using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Windows.ApplicationModel;
using Xamarin.Forms;

namespace NavitasBeta.UWP
{
    public class InfoService : IInfoService
    {
        public string Version
        {
            get
            {              
                var p = Package.Current.Id.Version;
                return $"{p.Major}.{p.Minor}.{p.Build}.{p.Revision}";
            }
        }

        //public string Build
        //{
        //    get
        //    {
        //        return "0";
        //    }
        //}
    }
}
