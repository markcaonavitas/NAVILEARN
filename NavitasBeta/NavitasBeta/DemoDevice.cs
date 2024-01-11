using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    class DemoDevice : IDevice
    {

        /// <summary>
        /// Gets the device name
        /// </summary>
        /// <value>The device name</value>
        public string Name { get; set; }

        public bool IsRegisteredUser { set; get; }

        public bool HasRegisteredUsersButNotYou { set; get; }

        public int Rssi { set; get; }

        public bool IsClickable { set; get; }
        public string NickName { get; set; }

        public DemoDevice()
        {
            Rssi = 0x0000ffff;         
        }

        public object GetBLEDevice()
        {
            return null;
        }
    }
}
