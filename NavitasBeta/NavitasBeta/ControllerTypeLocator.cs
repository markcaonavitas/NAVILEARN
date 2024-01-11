using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{ 
    public static class ControllerTypeLocator
    {
        private static string strControllerType = "TAC";
        public static string ControllerType
        {
            get
            {
                return strControllerType;
            }
            set
            {
                strControllerType = value;
            }
        }

    }
}
