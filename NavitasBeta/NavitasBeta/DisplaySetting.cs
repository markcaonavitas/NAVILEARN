using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace NavitasBeta
{
    public static class DisplaySettings
    {
        private static IDictionary<string, object> properties = Application.Current.Properties;
  

        public static IDictionary<string, string> GetDisplayStuff()
        {
            IDictionary<string, string> DisplayStuff = new Dictionary<string, string>();

    
            if (!properties.ContainsKey("StandardorMetric"))
                properties.Add("StandardorMetric", "False");
            DisplayStuff["StandardorMetric"] = properties["StandardorMetric"] as string;

            if (!properties.ContainsKey("SpeedOmeterMaxSpeed"))
                properties.Add("SpeedOmeterMaxSpeed", "35");
            DisplayStuff["SpeedOmeterMaxSpeed"] = properties["SpeedOmeterMaxSpeed"] as string;

            return DisplayStuff;
        }
        public static bool GetStandardorMetric()
        {
            return (string)properties["StandardorMetric"] == "True" ? true : false;
        }

        public static void SaveStandardorMetric(bool value)
        {
            properties["StandardorMetric"] = value ? "True" : "False";
            Application.Current.SavePropertiesAsync();
        }
        public static string GetSpeedOmeterMaxSpeed()
        {
            return (string)properties["SpeedOmeterMaxSpeed"];
        }

        public static void SaveSpeedOmeterMaxSpeed(string Speedvalue)
        {
            properties["SpeedOmeterMaxSpeed"] = Speedvalue;
            Application.Current.SavePropertiesAsync();
        }
    }
}

