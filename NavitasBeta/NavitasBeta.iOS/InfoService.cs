using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace NavitasBeta.iOS
{
    public class InfoService : IInfoService
    {
        public string Version
        {
            get
            {

                return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
            }
        }

        //public string Build
        //{
        //    get
        //    {

        //        return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        //    }
        //}
    }

}