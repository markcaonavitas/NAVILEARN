using System;

using System.Collections.Generic;
using Android.Bluetooth;
using System.Linq;
using Java.Util;
using System.Threading.Tasks;
using Xamarin.Forms;
using Java.Nio;
using Android.OS;
using System.Collections.ObjectModel;
using Android.Bluetooth.LE;
using NavitasBeta;
using Xamarin.Essentials;
using static Android.Provider.ContactsContract.CommonDataKinds;
using System.Text.RegularExpressions;
using Javax.Security.Auth;
using static Android.Bluetooth.BluetoothClass;

namespace NavitasBeta.Droid
{

    public class MyAndroidScanCallback : ScanCallback
    {
        public ObservableCollection<IDevice> _DiscoveredDevices;
        public ObservableCollection<IDevice> _BackUpDiscoveredDevices;
        public IDictionary<string, string> _UserCredentials;
        public List<string> RegisterControllers;
        public List<string> ControllersNickName;
        public MyAndroidScanCallback()
        {
            System.Diagnostics.Debug.WriteLine("Scan called back!!!");
        }


        public override void OnScanResult(ScanCallbackType callbackType, ScanResult result)
        {
            base.OnScanResult(callbackType, result);

            //System.Diagnostics.Debug.WriteLine("New Device = " + result.Device.Name.ToString());

            //System.Diagnostics.Debug.WriteLine("result.Rssi = " + result.Rssi.ToString());


            for (int i = 0; i < _DiscoveredDevices.Count; i++)
            {
                if (_DiscoveredDevices[i].Name == result.Device.Name)
                {
                    //System.Diagnostics.Debug.WriteLine("Device = " + result.Device.Name.ToString());
                    //System.Diagnostics.Debug.WriteLine($"result.Rssi = {result.Rssi} and previous {_DiscoveredDevices[i].Rssi} ");
                    var newRssi = (result.Rssi == 127) ? -127 : result.Rssi;
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
                            //Device transition is not needed
                        }
                    }
                    return;
                }
            }

            BLEDevice device = new BLEDevice(result.Device);
            device.Rssi = (Int16)result.Rssi;
            bool isNonNavitasDevice = !device.Name.Contains("TAC_") && !device.Name.Contains("TSX_") && (device.Name != "SENSSUN FAT");
            if (device.Rssi == 127 || isNonNavitasDevice) //Dec 2018, looks like it is a weak signal or is non Navitas BLE device, some come in like this so put it at the bottom
                device.Rssi = -127;
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
                    if ((device.Rssi < (_DiscoveredDevices[i]).Rssi) &&
                            (((BLEDevice)_DiscoveredDevices[i]).Name.Contains("TAC_") || ((BLEDevice)_DiscoveredDevices[i]).Name.Contains("TSX_")) || ((BLEDevice)_DiscoveredDevices[i]).Name.Contains("Ble"))
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

                if(index != -1)
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
                //Authentication.SaveNickName(""); // Delete nick name from persistent prop -- TEMP
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
    }
    /// <summary>
    /// 
    /// </summary>
    public class Adapter : Java.Lang.Object, IAdapter
    {
        private readonly BluetoothManager _manager;
        private readonly BluetoothAdapter _adapter;
        private BluetoothGatt _gatt;
        private GattCallback _callback;
        private ScanCallback _androidScanCallback;

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

        protected static UUID CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID = UUID.FromString("00002902-0000-1000-8000-00805f9b34fb");

        BluetoothDevice bleDevice;
        //     BluetoothGattCharacteristic ReadCharacteristic;
        BluetoothGattCharacteristic WriteCharacteristic;
        BluetoothDevice remoteDevice;

        public ObservableCollection<IDevice> DiscoveredDevices { get; set; }
        public IDictionary<string, string> UserCredentials;

        public Adapter()
        {
            var appContext = MainActivity.Instance;
            _manager = (BluetoothManager)appContext.GetSystemService("bluetooth");
            _adapter = _manager.Adapter;
            _callback = new GattCallback();
            _callback.DeviceConnected += BluetoothGatt_DeviceConnected;
            _callback.DeviceDisconnected += BluetoothGatt_DeviceDisconnected;
            _callback.CharacteristicFound += BluetoothGatt_OnCharacteristicFound;
            _callback.CharacteristicNotFound += BluetoothGatt_OnCharacteristicNotFound;
            System.Diagnostics.Debug.WriteLine("_callback.CharacteristicValueUpdated += CharacteristicValueUpdated");
            _callback.CharacteristicValueUpdated += CharacteristicValueUpdated;
            _androidScanCallback = new MyAndroidScanCallback();

        }


        public async Task<bool> IsEnabled()
        {
            if (_adapter == null)
            {
                return await Task.FromResult(false); //just for emulators hopefully
            }
            else
            {
                return await Task.FromResult(_adapter.IsEnabled); //interface is task<bool> for iOS etc.. so just do this
            }
        }

        public void InitializeDeviceList(IDictionary<string, string> userCredentials)
        {
            //    _adapter.Disable();

            DiscoveredDevices = new ObservableCollection<IDevice>();
            UserCredentials = userCredentials;

            ((MyAndroidScanCallback)_androidScanCallback)._DiscoveredDevices = DiscoveredDevices;
            ((MyAndroidScanCallback)_androidScanCallback)._UserCredentials = UserCredentials;
            ((MyAndroidScanCallback)_androidScanCallback).RegisterControllers = Regex.Replace(UserCredentials["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",").Split(',').ToList();
            ((MyAndroidScanCallback)_androidScanCallback).ControllersNickName = UserCredentials.ContainsKey("NickName") ? UserCredentials["NickName"].Split(',').ToList() : new List<string>();
            bleDevice = null;
            //      _adapter.Enable();
            //      Task.Delay(1000).Wait();

        }


        public void StartScanningForDevices()
        {

            _adapter.BluetoothLeScanner.StopScan(_androidScanCallback);

            IList<ScanFilter> filters = new List<ScanFilter>();
            UUID Uuid = UUID.FromString("0000fff0-0000-1000-8000-00805f9b34fb");  //// ffb0 is the new part
            ParcelUuid puuid = new ParcelUuid(Uuid);
            ScanFilter f = new ScanFilter.Builder().SetServiceUuid(puuid).Build();

            ScanSettings settings = new ScanSettings.Builder().SetScanMode(Android.Bluetooth.LE.ScanMode.LowLatency).Build();

            filters.Add(f);

            UUID UuidOld = UUID.FromString("0000ffb0-0000-1000-8000-00805f9b34fb");
            ParcelUuid puuidold = new ParcelUuid(UuidOld);
            ScanFilter o = new ScanFilter.Builder().SetServiceUuid(puuidold).Build();

            filters.Add(o);

            System.Diagnostics.Debug.WriteLine("Start Scanning");
            _adapter.BluetoothLeScanner.StartScan(filters, settings, _androidScanCallback);


            //     Task.Delay(5000).Wait();
            //      System.Diagnostics.Debug.WriteLine("Stop Scanning");
            //   _adapter.BluetoothLeScanner.StopScan(_androidScanCallback);

        }

        public void StopScanningForDevices()
        {
            if (_androidScanCallback != null)
            {
                _adapter.BluetoothLeScanner.StopScan(_androidScanCallback); 
            }
        }

        private bool Connect(IDevice device)
        {
            System.Diagnostics.Debug.WriteLine("Stop Scanning");
            _adapter.BluetoothLeScanner.StopScan(_androidScanCallback);
            Task.Delay(100).Wait();
            //RG Apr 2020:Just a guess to FlushPendingScanResults to fix status 133 errors, seemed to improve it
            _adapter.BluetoothLeScanner.FlushPendingScanResults(_androidScanCallback);
            System.Diagnostics.Debug.WriteLine("Stopped Scanning???");
            bleDevice = (BluetoothDevice)device.GetBLEDevice();

            System.Diagnostics.Debug.WriteLine("Connect to = " + bleDevice.Name);
            remoteDevice = _adapter.GetRemoteDevice(bleDevice.Address);

            System.Diagnostics.Debug.WriteLine("_adapter.GetRemoteDevice returned = " + remoteDevice.ToString());
            bFound = false;
            _callback.bFound = false;
            _callback._newState = ProfileState.Disconnecting;
            _gatt = remoteDevice.ConnectGatt(MainActivity.Instance, false, _callback, BluetoothTransports.Le);
            // manipulate UI controls
            _gatt.RequestConnectionPriority(Android.Bluetooth.GattConnectionPriority.High);
            //    BluetoothGatt.ConnectionPriorityBalanced, BluetoothGatt.ConnectionPriorityHigh or BluetoothGatt.ConnectionPriorityLowPower.




            //     await Task.Delay(3000);
            int seconds = 20;
            while (_callback.bFound == false && (seconds > 0))
            {
                System.Diagnostics.Debug.WriteLine("Dons debug waiting for callback");
                Task.Delay(1000).Wait();
                seconds--;
            }

            if (_callback._status.ToString() != "Success")
            {

                System.Diagnostics.Debug.WriteLine("Dons Debug Return false _callback._status = " + _callback._status.ToString());
                return false;
            }


            seconds = 8;
            while (bFound == false && (seconds > 0))
            {
                System.Diagnostics.Debug.WriteLine("Dons Debug waiting for characteristics");
                Task.Delay(1000).Wait();
                seconds--;
            }

            System.Diagnostics.Debug.WriteLine("Dons Debug bFound = " + bFound + "  status = " + _callback._status.ToString());
            return this.bFound;

        }

        public void Close()
        {

            try
            {
                _gatt.Disconnect();
                //this seems like a must otherwise it is possible to close before disconnect callback is seen
                Task.Delay(1000).Wait();

                remoteDevice.UnregisterFromRuntime();
                remoteDevice.Dispose();
                _gatt.Close();
                _gatt.Dispose();
                Java.Lang.Reflect.Method mi = remoteDevice.Class.GetMethod("removeBond", null); mi.Invoke(remoteDevice, null);

                //    this.InitializeDeviceList();
                //     this.StartScanningForDevices();
                bleDevice = null;
                System.Diagnostics.Debug.WriteLine("gatt Close");

            }
            catch (Exception e)
            {

                //throw;//A silly Android invalid status will dispose this object and a close is called from the
                //project devicecommunication.cs probably when it shouldn't
                System.Diagnostics.Debug.WriteLine("silly Android dispose exception? or " + e.Message);
            }
        }

        /// <summary>
        /// Connect to a device.
        /// </summary>
        /// <param name="device">The device.</param>
        public async Task<bool> ConnectToDevice(IDevice device)
        {

            if (!Connect(device))
            {
                bleDevice = null;
                return await Task.FromResult(false); //interface is task<bool> for iOS etc.. so just do this
            }

            return await Task.FromResult(true); //interface is task<bool> for iOS etc.. so just do this
        }

        bool bFound;
        private void BluetoothGatt_OnCharacteristicFound(object sender, EventArgs e)
        {

            WriteCharacteristic = _callback.GetWriteCharacteristic();
            System.Diagnostics.Debug.WriteLine("BluetoothGatt_OnCharacteristicFound  UUID = " + WriteCharacteristic.Uuid.ToString());
            bFound = true;
            Task.Delay(300).Wait();
            DeviceConnected(null, null);
        }

        private void BluetoothGatt_OnCharacteristicNotFound(object sender, EventArgs e)
        {
            // call the timeout here. 
            _gatt.Close();
            _gatt.Dispose();

        }


        private void BluetoothGatt_DeviceConnected(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Calling Discover Services");

            _gatt.DiscoverServices();
        }

        private void BluetoothGatt_DeviceDisconnected(object sender, EventArgs e)
        {
            DeviceDisconnected(null, null);
            _gatt.Close();
            _gatt.Dispose();

            Task.Delay(200).Wait();
        }
        private void CharacteristicValueUpdated(object sender, CharacteristicReadEventArgs e)
        {
            PacketResponseReceived(sender, new PacketReceivedEventArgs(e.Value));
        }

        public bool Write(byte[] data)
        {
            //         System.Diagnostics.Debug.WriteLine("Write Entered");
            //     _callback.bWriteFinished = false;
            //    System.Diagnostics.Debug.WriteLine("Write UUID = " + WriteCharacteristic.Uuid.ToString());
            //    _gatt.Connect();

            //      public Java.Util.UUID GetWriteServiceUUID()
            //        {
            //            return writeserviceuuid;
            //        }

            //      public Java.Util.UUID GetWriteCharacteristicUUID()
            //       {
            //           return this.writechacteristicid;
            //       }

            try
            {
                //System.Diagnostics.Debug.WriteLine(DateTime.Now.Millisecond + ": Write received, Count = " + data.Length.ToString());
                BluetoothGattService mSVC = _gatt.GetService(_callback.GetWriteServiceUUID());
                BluetoothGattCharacteristic WriteCharacteristic = mSVC.GetCharacteristic(_callback.GetWriteCharacteristicUUID());
                //        mCH.setValue(data_to_write);
                //         mBG.writeCharacteristic(mCH);
                //      System.Diagnostics.Debug.WriteLine("Waiting for 20");
                //       Wait(20000);
                //      System.Diagnostics.Debug.WriteLine("Waiting done");
                int retries = 100000;
                while (retries > 0)
                {
                    WriteCharacteristic.WriteType = GattWriteType.NoResponse;
                    WriteCharacteristic.SetValue(data);

                    _callback.bWriteFinished = false;
                    if (_gatt.WriteCharacteristic(WriteCharacteristic))
                    {

                        if (WaitForCallbackToFinish())
                        {
                            //          System.Diagnostics.Debug.WriteLine("Success");
                            //Task.Delay(6).Wait();
                            return true;
                        }
                        //while (_callback.bWriteFinished == false) ;
                        //   return true;
                    }
                    Task.Delay(1000).Wait();
                    System.Diagnostics.Debug.WriteLine("retries = " + retries.ToString());
                    retries--;
                }

            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Is this the final dispose exception?");

                //throw;
            }
            System.Diagnostics.Debug.WriteLine("Write(data) fail");
            return false;
        }
#if OLD
        async void Wait(int Milliseconds)
        {
            while (Milliseconds > 0)
            {
                await Task.Delay(1);
                Milliseconds--;
            }
           
        }
#endif
        bool WaitForCallbackToFinish()
        {
            int TimeCounter = 0;
            //          if(_callback.bWriteFinished)
            //             System.Diagnostics.Debug.WriteLine("_callback.bWriteFinished already true");

            while (_callback.bWriteFinished == false)
            {
                //System.Diagnostics.Debug.WriteLine("Waiting 1ms");
                Task.Delay(1).Wait();
                TimeCounter++;
                if (TimeCounter > 2000)  // I have seen this counter reach 70.  WTF its like  700ms to aknowledge once in blue moon. 
                {
                    System.Diagnostics.Debug.WriteLine("_callback.bWriteFinished never finished");
                    return false;
                }

                if (TimeCounter > 10)
                    System.Diagnostics.Debug.WriteLine("TimeCounter = " + TimeCounter.ToString());
                //           else
                //               System.Diagnostics.Debug.WriteLine("TimeCounter = " + TimeCounter.ToString());

            }

            //   System.Diagnostics.Debug.WriteLine("Write finish");
            return true;
        }

        public Task<bool> IsServiceEnabled()
        {

            throw new NotImplementedException();
        }
    }


}

