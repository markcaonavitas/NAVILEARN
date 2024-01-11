using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreBluetooth;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Threading;
using CoreFoundation;
using System.Text.RegularExpressions;

namespace NavitasBeta.iOS
{
    public class CentralManagerDelegate : CBCentralManagerDelegate
    {
        public event EventHandler<EventArgs> FindServices = delegate { };
        public event EventHandler<EventArgs> Disconnect = delegate { };

        ObservableCollection<IDevice> _DiscoveredDevices;
        public IDictionary<string, string> _UserCredentials;
        public List<string> RegisterControllers;
        public List<string> ControllersNickName;
        public AutoResetEvent _stateChanged;
        public bool bFound = false;
        public CentralManagerDelegate()
        {
            _stateChanged = new AutoResetEvent(false);
        }

        public void SetObservatbleDeviceList(ObservableCollection<IDevice> DiscoveredDevices, IDictionary<string, string> userCredentials)
        {
            _DiscoveredDevices = DiscoveredDevices;
            _UserCredentials = userCredentials;
        }

        public override void UpdatedState(CBCentralManager cbCentralManager)
        {
            _stateChanged.Set();
        }

        public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber receivedSignalStrengthIndication)
        {
            YesNoPopupLoader popup = new YesNoPopupLoader();
            //System.Diagnostics.Debug.WriteLine("New DiscoveredPeripheral Calling ConnectPeripheral " + peripheral.Name);

            //        CBPeripheral c = sender as CBPeripheral;

            //    System.Diagnostics.Debug.WriteLine("e.Rssi = " + e.Rssi);

            //     System.Diagnostics.Debug.WriteLine("c.Name = " + c.Name.ToString());
            ///       _central.CancelPeripheralConnection(c);

            for (int i = 0; i < _DiscoveredDevices.Count; i++)
            {
                if (_DiscoveredDevices[i].Name == peripheral.Name)
                {
                    //System.Diagnostics.Debug.WriteLine($"Device = {peripheral.Name}");
                    //System.Diagnostics.Debug.WriteLine($"result.Rssi = {receivedSignalStrengthIndication.Int16Value} and previous {_DiscoveredDevices[i].Rssi} ");
                    var newRssi = (receivedSignalStrengthIndication.Int16Value == 127) ? -127 : receivedSignalStrengthIndication.Int16Value;
                    var curBLEDevice = (_DiscoveredDevices[i] as BLEDevice);
                    curBLEDevice.FilteredRssi = (newRssi - curBLEDevice.FilteredRssi) * 0.1f + curBLEDevice.FilteredRssi;
                    _DiscoveredDevices[i].Rssi = (int)curBLEDevice.FilteredRssi;

                    var RssiDifference = Math.Abs(curBLEDevice.FilteredRssiStoredForSorting - curBLEDevice.FilteredRssi);
                    if (RssiDifference > 10)
                    {
                        //System.Diagnostics.Debug.WriteLine($"prev stored Rssi = {curBLEDevice.FilteredRssiStoredForSorting} recent filtered: {curBLEDevice.FilteredRssi}");
                        //System.Diagnostics.Debug.WriteLine($"{_DiscoveredDevices[i].Name} changed");
                        curBLEDevice.FilteredRssiStoredForSorting = (int)curBLEDevice.FilteredRssi;
                        if (i == 0 && curBLEDevice.FilteredRssi < (_DiscoveredDevices[i + 1] as BLEDevice).FilteredRssi)
                        { // Step down
                            //System.Diagnostics.Debug.WriteLine($"Current [{i}] Rssi = {curBLEDevice.FilteredRssi} steps down [{i + 1}] Rssi = {(_DiscoveredDevices[i + 1] as BLEDevice).FilteredRssi}");
                            TransitDevice(i + 1, i);
                        }
                        else if (i > 0 && i < _DiscoveredDevices.Count - 1)
                        {
                            if (curBLEDevice.FilteredRssi > (_DiscoveredDevices[i - 1] as BLEDevice).FilteredRssi)
                            { // Step up
                                //System.Diagnostics.Debug.WriteLine($"Current [{i}] Rssi = {curBLEDevice.FilteredRssi} steps up [{i - 1}] Rssi = {(_DiscoveredDevices[i - 1] as BLEDevice).FilteredRssi}");
                                TransitDevice(i, i - 1);
                            }

                            if (curBLEDevice.FilteredRssi < (_DiscoveredDevices[i + 1] as BLEDevice).FilteredRssi)
                            { // Step down
                                //System.Diagnostics.Debug.WriteLine($"Current [{i}] Rssi = {curBLEDevice.FilteredRssi} steps down [{i + 1}] Rssi = {(_DiscoveredDevices[i + 1] as BLEDevice).FilteredRssi}");
                                TransitDevice(i + 1, i);
                            }
                        }
                        else
                        {
                            // Device transition is not needed
                        }
                    }
                    return;
                }
            }

            BLEDevice device = new BLEDevice(peripheral)
            {
                Rssi = receivedSignalStrengthIndication.Int16Value
            };
            bool isNonNavitasDevice = !device.Name.Contains("TAC_") && !device.Name.Contains("TSX_") && (device.Name != "SENSSUN FAT");
            if (device.Rssi == 127 || isNonNavitasDevice) //Dec 2018, looks like it is a weak signal or is non Navitas BLE device, some come in like this so put it at the bottom
                device.Rssi = -127;
            System.Diagnostics.Debug.WriteLine("RssiRead DiscoveredDevices.Count = " + _DiscoveredDevices.Count + " rssi = " + receivedSignalStrengthIndication.ToString());
            if (_DiscoveredDevices.Count == 0)
            {
                device.FilteredRssi = device.Rssi;
                device.FilteredRssiStoredForSorting = device.Rssi;
                _DiscoveredDevices.Add(device);

                if (isNonNavitasDevice)
                    device.IsClickable = false;
                else //Doing this won't block the UI (Main) thread
                    Task.Factory.StartNew(async () => await VerifyControllerOwnership(device));
            }
            else
            {
                int iIndex = 0;

                for (int i = 0; i < _DiscoveredDevices.Count; i++)
                {
                    //-78 < -75
                    if ((device.Rssi < ((BLEDevice)_DiscoveredDevices[i]).Rssi) &&
                            (((BLEDevice)_DiscoveredDevices[i]).Name.Contains("TAC_") || ((BLEDevice)_DiscoveredDevices[i]).Name.Contains("TSX_")))
                    {
                        iIndex++;
                    }
                }
                if (_DiscoveredDevices.Count >= iIndex)
                {
                    if (isNonNavitasDevice)
                        device.IsClickable = false;
                    device.FilteredRssi = device.Rssi;
                    device.FilteredRssiStoredForSorting = device.Rssi;
                    _DiscoveredDevices.Insert(iIndex, device);
                    //Doing this won't block the UI (Main) thread
                    Task.Factory.StartNew(async () => await VerifyControllerOwnership(device));
                }
            }
        }

        private void TransitDevice(int replacedByPos, int moveToPos)
        {
            var tempDevice = _DiscoveredDevices[moveToPos];
            _DiscoveredDevices.Move(replacedByPos, moveToPos);
            _DiscoveredDevices.RemoveAt(replacedByPos);
            //_DiscoveredDevices.Select((d, i) => new { d, i }).ForEach(o => System.Diagnostics.Debug.WriteLine($"{o.i} - Name: {o.d.Name}"));
            _DiscoveredDevices.Insert(replacedByPos, tempDevice);
            //_DiscoveredDevices.Select((d, i) => new { d, i }).ForEach(o => System.Diagnostics.Debug.WriteLine($"{o.i} - Name: {o.d.Name}"));
        }

        private async Task VerifyControllerOwnership(BLEDevice device)
        {
            //Console.WriteLine("{0} thread ID: {1}", device.Name, Thread.CurrentThread.ManagedThreadId);
            try
            {
                int index = RegisterControllers.Select((serialNumber, i) => new { serialNumber, i }).
                    FirstOrDefault(obj => obj.serialNumber == device.Name)?.i ?? -1;

                if (index != -1)
                {
                    await UpdateRegisteredControllerView(device, index);
                }
                else
                {
                    if (await App.ParseManagerAdapter.IsControllerRegistered(device.Name))
                    {
                        var isRegisteredUser = await App.ParseManagerAdapter.IsRegisteredUser(device.Name);
                        if (isRegisteredUser)
                        {
                            await UpdateRegisteredControllerView(device, index);
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(() => device.HasRegisteredUsersButNotYou = !isRegisteredUser);
                            await FetchNickName(device);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("VerifyControllerOwnership exception: " + ex.Message);
            }
        }

        private async Task UpdateRegisteredControllerView(BLEDevice device, int pos)
        {
            MainThread.BeginInvokeOnMainThread(() => device.IsRegisteredUser = true);
            if (pos < ControllersNickName.Count && pos != -1)
            {
                // Nickname is existing in persistent property
                MainThread.BeginInvokeOnMainThread(() => device.NickName = ControllersNickName[pos]);
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var nickName = await App.ParseManagerAdapter.GetNickName(device.Name);
                    //Replace existing nickname from the cloud if there are any changes from the persistent property
                    if (nickName == null || ControllersNickName[pos] != nickName)
                    {
                        var isNickNameSaved = await App.ParseManagerAdapter.SaveNickName(device.Name, ControllersNickName[pos]);
                        //if (isNickNameSaved)
                        //    System.Diagnostics.Debug.WriteLine("Nickname saved!");
                        //else
                        //    System.Diagnostics.Debug.WriteLine("Nickname failed to save");
                    }
                }
            }
            else
            {
                // Serial Number already added to persistent prop, except the nickname
                await FetchNickName(device);
            }
        }

        private async Task FetchNickName(BLEDevice device)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                var nickName = await App.ParseManagerAdapter.GetNickName(device.Name);
                if (nickName != null)
                {
                    MainThread.BeginInvokeOnMainThread(() => device.NickName = nickName);
                }
            }
        }

        public override void DisconnectedPeripheral(CBCentralManager central, CBPeripheral peripheral, NSError error)
        {
            Disconnect(null,null);   
        }

        public override void ConnectedPeripheral(CBCentralManager central, CBPeripheral peripheral)
        {
            bFound = true;
            FindServices(null, null);
        }

    
       
    }



class Adapter : IAdapter
    {
        /// <summary>
        /// Gets or sets the amount of time to scan for devices.
        /// </summary>
        /// <value>The scan timeout.</value>
        public TimeSpan ScanTimeout { get; set; }

        /// <summary>
        /// Gets or sets the amount of time to attempt to connect to a device.
        /// </summary>
        /// <value>The connection timeout.</value>
        public TimeSpan ConnectionTimeout { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is scanning.
        /// </summary>
        /// <value>true</value>
        /// <c>false</c>
        public bool IsScanning { get; set; }
        /// <summary>
        /// Occurs when a receive packet is validated
        /// </summary>
        public event EventHandler<PacketReceivedEventArgs> PacketResponseReceived = delegate { };

        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceConnected = delegate { };

        /// <summary>
        /// Occurs when device disconnected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceDisconnected = delegate { };
        /// <summary>
        /// Gets the discovered devices.
        /// </summary>
        /// <value>The discovered devices.</value>
        public ObservableCollection<IDevice> DiscoveredDevices { get; set; }
        public IDictionary<string, string> UserCredentials;

        private readonly CBCentralManager _central;
        private CentralManagerDelegate _centraldelegate;
   //     private readonly AutoResetEvent _stateChanged;
        CBPeripheral bleDevice;
        //      bool DidConnectionSucceed;
        CBCharacteristic WriteCharacteristic;
        CBCharacteristic ReadCharacteristic;

        int Count = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="BluetoothLE.iOS.Adapter"/> class.
        /// </summary>
        public Adapter()
        {
            _centraldelegate = new CentralManagerDelegate();
            _centraldelegate.FindServices += FindServices;
            _centraldelegate.Disconnect += Disconnect;
            _central = new CBCentralManager(_centraldelegate, DispatchQueue.MainQueue);


            //       _central.DiscoveredPeripheral += DiscoveredPeripheral;
            //    _central.UpdatedState += UpdatedState;


            //        _central.ConnectedPeripheral += ConnectedPeripheral;

            //         _central.ConnectedPeripheral += ConnectedPeripheral;
            //        _central.DisconnectedPeripheral += DisconnectedPeripheral;
            //      _central.FailedToConnectPeripheral += FailedToConnectPeripheral;

            //        _stateChanged = new AutoResetEvent(false);
            Count = 0;
        }
       

        async public Task<bool> IsEnabled()
        {
            await Task.Run(() => _centraldelegate._stateChanged.WaitOne(1000));
 
            if (_central.State == CBCentralManagerState.PoweredOn)
            {
                return true;
            }
            return false;
        }
#if OLD_CODE
        private void ConnectedPeripheral(object sender, CBPeripheralEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ConnectedPeripheral IsScanning = " + IsScanning.ToString());
            if (IsScanning)
            {

          //      e.Peripheral.RssiRead += RssiRead;
           //     e.Peripheral.ReadRSSI();


            }
            else
            {
                bleDevice.DiscoveredService += DiscoveredService;
                bleDevice.DiscoveredCharacteristic += DiscoveredCharacteristic;
                bleDevice.DiscoverServices();
            }

      //      DidConnectionSucceed = true;


        }
#endif
        public void FindServices(object sender, EventArgs e)
        {
            bleDevice.DiscoveredService += DiscoveredService;
            bleDevice.DiscoveredCharacteristic += DiscoveredCharacteristic;
            bleDevice.DiscoverServices();
        }

#if OLD_CODE

        private void RssiRead(object sender, CBRssiEventArgs e)
        {
           
            CBPeripheral c = sender as CBPeripheral;
           
            System.Diagnostics.Debug.WriteLine("e.Rssi = " + e.Rssi);

            System.Diagnostics.Debug.WriteLine("c.Name = " + c.Name.ToString());
     ///       _central.CancelPeripheralConnection(c);

            Device device = new Device(c);
            device.Rssi = e.Rssi;

            System.Diagnostics.Debug.WriteLine("RssiRead DiscoveredDevices.Count = " + DiscoveredDevices.Count);
            if (DiscoveredDevices.Count == 0)
            {
                DiscoveredDevices.Add(device);
               
            }
            else
            {
                int iIndex = 0;
                foreach (Device d in DiscoveredDevices)
                {

                    if(e.Rssi.Int16Value < d.Rssi.Int16Value)
                    {
                        iIndex++;                     
                    }                   

                }

                DiscoveredDevices.Insert(iIndex, device);

            }




        }


        private void DisconnectedPeripheral(object sender, CBPeripheralErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DisconnectedPeripheral = " + IsScanning.ToString());
            if (IsScanning)
            {
        //        e.Peripheral.RssiRead -= RssiRead;
            }
            else
            {
                

                bleDevice.UpdatedCharacterteristicValue -= UpdatedCharacteristicValue;
                bleDevice.DiscoveredService -= DiscoveredService;
                bleDevice.DiscoveredCharacteristic -= DiscoveredCharacteristic;


                DeviceDisconnected(null, null);
 
            }
        }
#endif 
        private void Disconnect(object sender, EventArgs e)
        {
            bleDevice.UpdatedCharacterteristicValue -= UpdatedCharacteristicValue;
            bleDevice.DiscoveredService -= DiscoveredService;
            bleDevice.DiscoveredCharacteristic -= DiscoveredCharacteristic;


            DeviceDisconnected(null, null);


        }


        private void DiscoveredService(object sender, NSErrorEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Service Discovered");
           
            if (bleDevice.Services != null)
            {
               bleDevice.DiscoverCharacteristics(bleDevice.Services[0]);
            }
     
        }

        private void DiscoveredCharacteristic(object sender, CBServiceEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Characteristic Discovered");

            bool bCharacteristicfound = false;
            foreach (var s in bleDevice.Services)
            {
                foreach (CBCharacteristic c in s.Characteristics)
                {
                    if (c.UUID.ToString() == "ffb1" || c.UUID.ToString() == "fff1")
                    {
                        bCharacteristicfound = true;
                        System.Diagnostics.Debug.WriteLine("gatt WriteCharacteristic found");
                        WriteCharacteristic = c;
                    }
                    if (c.UUID.ToString() == "ffb2" || c.UUID.ToString() == "fff2")
                    {
                        System.Diagnostics.Debug.WriteLine("gatt ReadCharacteristic found");
                        ReadCharacteristic = c;
                    }

                }

            }
            if (bCharacteristicfound)
            {
                bleDevice.UpdatedCharacterteristicValue += UpdatedCharacteristicValue;
                bleDevice.SetNotifyValue(true, ReadCharacteristic);
                DeviceConnected(null, null);
            }

        }

#if OLD_CODE
        private void FailedToConnectPeripheral(object sender, CBPeripheralErrorEventArgs e)
        {


            System.Diagnostics.Debug.WriteLine("Error" + e.Error.Description);
            
         
        }

       

        private void DiscoveredPeripheral(object sender, CBDiscoveredPeripheralEventArgs e)
        {

            if (IsScanning)
            {
                System.Diagnostics.Debug.WriteLine("DiscoveredPeripheral Calling ConnectPeripheral " + e.Peripheral.Name);
                      e.Peripheral.RssiRead += RssiRead;
                     e.Peripheral.ReadRSSI();

              
              //  _central.ConnectPeripheral(e.Peripheral);
               
             //   NSDictionary ns = e.AdvertisementData;
                    
            }
            else
            {
             //   var device = new Device(e.Peripheral);
              //  DiscoveredDevices.Add(device);
            }
            //Count++;   
        }

        private void UpdatedState(object sender, EventArgs e)
        {
            _stateChanged.Set();
        }
#endif 
        /// <summary>
        /// Start scanning for devices.
        /// </summary>
        /// <param name="serviceUuids">White-listed service UUIDs</param>
        /// 

        public async void StartScanningForDevices()
        {
            
            IsScanning = true;
            string[] serviceUuids = new string[2];
            //      serviceUuids[0] = "FFF0";
            //  serviceUuids[0] = "FFB0";
            serviceUuids[0] = "0000FFB0-0000-1000-8000-00805f9b34fb";
            serviceUuids[1] = "0000FFF0-0000-1000-8000-00805f9b34fb";

            //     await WaitForState(CBCentralManagerState.PoweredOn);

            var options = new NSDictionary(CBCentralManager.ScanOptionAllowDuplicatesKey, NSNumber.FromBoolean(true));

            var uuids = new List<CBUUID>();
            foreach (var guid in serviceUuids)
            {
                uuids.Add(CBUUID.FromString(guid));
            }
 
        //    DiscoveredDevices = new List<IDevice>(); 


            _central.ScanForPeripherals(uuids.ToArray(), options);

     //       ScanTimeout = TimeSpan.FromSeconds(10);

            //       await Task.Delay(ScanTimeout);

            //          if (IsScanning)
            //        {
            //            StopScanningForDevices();
            //       ScanTimeoutElapsed(this, EventArgs.Empty);
            //       }
        }

        /// <summary>
        /// Stop scanning for devices.
        /// </summary>
        public void StopScanningForDevices()
        {

            _central.StopScan();
        //    IsScanning = false;
        }

        /// <summary>
        /// Connect to a device.
        /// </summary>
        /// <param name="device">The device.</param>
        public async Task<bool> ConnectToDevice(IDevice device)
        {
            StopScanningForDevices();

            bool bReturn = false;
            ReceivePacket = null;
            Task<bool> task = Connect(device);
            bReturn = await task;

            return bReturn;
        }

        private async Task<bool> Connect(IDevice device)
        {
            bleDevice = (CBPeripheral)device.GetBLEDevice();

            //    bleDevice.DiscoveredService += DiscoveredService;
            //   bleDevice.DiscoveredCharacteristic += DiscoveredCharacteristic;
            _centraldelegate.bFound = false;

            _central.ConnectPeripheral(bleDevice);
            await Task.Delay(1000);
            if(_centraldelegate.bFound == false)
            {
                _central.CancelPeripheralConnection(bleDevice);
            }
            return _centraldelegate.bFound; 

        }

        byte[] ReceivePacket;
        int NumberOfBytesExpected;
        byte[] checksum;

        private void UpdatedCharacteristicValue(object sender, CBCharacteristicEventArgs e)
        {
    //          System.Diagnostics.Debug.WriteLine("UpdatedCharacteristicValue");
            NSData mydata = ReadCharacteristic.Value;
            byte[] Value = mydata.ToArray();
            PacketResponseReceived(sender, new PacketReceivedEventArgs(Value));
         }

        public void Close()
        {
    //        bleDevice.UpdatedCharacterteristicValue -= UpdatedCharacteristicValue;
     //       bleDevice.DiscoveredService -= DiscoveredService;
  //          bleDevice.DiscoveredCharacteristic -= DiscoveredCharacteristic;
      //      _central.DisconnectedPeripheral

            _central.CancelPeripheralConnection(bleDevice);
#if OLD
            bleDevice = null;
            HasComThreadStopped = true;
#endif
       
            System.Diagnostics.Debug.WriteLine("gatt Close");
        }

        public void InitializeDeviceList(IDictionary<string, string> userCredentials)
        {
            
            DiscoveredDevices = new ObservableCollection<IDevice>();
            UserCredentials = userCredentials;
            _centraldelegate.SetObservatbleDeviceList(this.DiscoveredDevices, UserCredentials);
            _centraldelegate.RegisterControllers = Regex.Replace(UserCredentials["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",").Split(',').ToList();
            _centraldelegate.ControllersNickName = UserCredentials.ContainsKey("NickName") ? UserCredentials["NickName"].Split(',').ToList() : new List<string>();
            System.Diagnostics.Debug.WriteLine("DiscoveredDevices.Count = " + DiscoveredDevices.Count.ToString());
            
#if OLD
            bleDevice = null;
#endif

        }


        public bool Write(byte[] data)
        {
            var nsData = NSData.FromArray(data);
            bleDevice.WriteValue(nsData, WriteCharacteristic, CBCharacteristicWriteType.WithoutResponse);
            return true;
        }
    }
}