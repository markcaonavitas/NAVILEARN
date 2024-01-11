using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.ComponentModel;
namespace NavitasBeta
{
	/// <summary>
	/// The device interface.
	/// </summary>
	public interface IDevice
	{
		/// <summary>
		/// Gets the device name
		/// </summary>
		/// <value>The device name</value>
		string Name { set; get; }

		bool IsRegisteredUser { set; get; }

		bool HasRegisteredUsersButNotYou { set; get; }

        int Rssi { get; set; }

        bool IsClickable { set; get; }

		string NickName { get; set; }

		object GetBLEDevice();

    }
}

