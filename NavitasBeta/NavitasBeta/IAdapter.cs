using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NavitasBeta
{
	/// <summary>
	/// Adapter interface that handles device discovery and connection.
	/// </summary>
	public interface IAdapter
	{
        /// <summary>
        /// Occurs when receive packet checksum is validated.
        /// </summary>
        event EventHandler<PacketReceivedEventArgs> PacketResponseReceived;
        /// <summary>
        /// Occurs when device diconnected.
        /// </summary>
        event EventHandler<EventArgs> DeviceDisconnected;

        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        event EventHandler<EventArgs> DeviceConnected;

        /// <summary>
        /// Start scanning for devices.
        /// </summary>
        void StartScanningForDevices();

        void StopScanningForDevices();

        /// <summary>
        /// Connects to the user selected device 
        /// </summary>
        Task<bool> ConnectToDevice(IDevice device);

        /// <summary>
        /// Closes the bluetooth adapter
        /// </summary>
        void Close();

        /// <summary>
        /// Init the list that holds the scanned devices
        /// </summary>
        void InitializeDeviceList(IDictionary<string,string> userCredentials);

        /// <summary>
        /// Writes data to write characteristic
        /// </summary>
        bool Write(byte[] data);

        /// <summary>
        /// Gets the discovered devices.
        /// </summary>
        /// <value>The discovered devices.</value>
        ObservableCollection<IDevice> DiscoveredDevices { get; set; }

        /// <summary>
        /// Checks if smartphone has bluetooth turned on 
        /// </summary>
        Task<bool> IsEnabled();
    }
}

