using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Bluetooth;
using Windows.Devices.WiFi;
using Windows.Devices.Enumeration;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;

using Xamarin.Essentials;
using Windows.Devices.Bluetooth.Advertisement;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;

namespace NavitasBeta.UWP
{

    public class GattCharacteristicWithValue
    {
        public GattCharacteristicWithValue() { }

        public GattCharacteristicWithValue(GattCharacteristic gattCharacteristic)
        {
            NativeCharacteristic = gattCharacteristic;
        }

        public GattCharacteristic NativeCharacteristic { get; set; }

        public byte[] Value { get; set; }

        public Guid ID { get { return NativeCharacteristic.Uuid; } }

        public string Uuid { get { return NativeCharacteristic.Uuid.ToString(); } }
    }

    public class Adapter : IAdapter
    {
        protected GattCharacteristicWithValue writeGattCharacteristicWithValue;
        protected GattCharacteristicWithValue readGattCharacteristicWithValue;

        public Adapter()
        {
            this.writeGattCharacteristicWithValue = new GattCharacteristicWithValue();
            this.readGattCharacteristicWithValue = new GattCharacteristicWithValue();
        }
        //events
        /// <summary>
        /// Occurs when a receive packet is validated
        /// </summary>
        public event EventHandler<PacketReceivedEventArgs> PacketResponseReceived = delegate { };

        public event EventHandler<EventArgs> DeviceDiscovered;
        //public event EventHandler<DeviceConnectionEventArgs> DeviceConnected;
        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceConnected = delegate { };
        //        public event EventHandler<DeviceConnectionEventArgs> DeviceDisconnected;
        /// <summary>
        /// Occurs when device disconnected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceDisconnected = delegate { };
        public event EventHandler ScanTimeoutElapsed;

        BluetoothLEDevice bleDevice;
        //  BluetoothGattCharacteristic ReadCharacteristic;
        // GattCharacteristicWithValue WriteCharacteristic;
        //BluetoothDevice remoteDevice;

        public bool IsScanning
        {
            get { return isScanning; }
        }
        protected bool isScanning;

        //public IList<IDevice> DiscoveredDevices
        //{
        //    get { return discoveredDevices; }
        //} protected IList<IDevice> discoveredDevices = new List<IDevice>(); 
        /// <summary>
        /// Gets the discovered devices.
        /// </summary>
        /// <value>The discovered devices.</value>
        public ObservableCollection<IDevice> DiscoveredDevices { get; set; }
        public IDictionary<string, string> _UserCredentials;


        public IList<IDevice> ConnectedDevices
        {
            get { return connectedDevices; }
        }
        protected IList<IDevice> connectedDevices = new List<IDevice>();

        public void StartScanningForDevices()
        {
            StartScanningForDevices(serviceUuid: Guid.Empty);
        }

        public void StartScanningForDevices(Guid serviceUuid)
        {
            if (isScanning == true)
                return;

            //DiscoveredDevices = new ObservableCollection<IDevice>();

            Debug.WriteLine("Adapter: Starting a scan for devices.");

            //clear the list
            //discoveredDevices = new List<IDevice>();

            isScanning = true;
            //string junk =BluetoothLEDevice.GetDeviceSelector();
            //DeviceInformationCollection junk1 = await DeviceInformation.FindAllAsync(BluetoothLEDevice.GetDeviceSelector());
            //DeviceInformationCollection junk2 = await DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(false));
            //DeviceInformationCollection junk3 = await DeviceInformation.FindAllAsync(BluetoothDevice.GetDeviceSelectorFromPairingState(true));
            //string BleSelector = "System.Devices.DevObjectType:=5 AND (System.Devices.Aep.ProtocolId:=\"{BB7BB05E-5972-42B5-94FC-76EAA7084D49}\" OR System.Devices.Aep.ProtocolId:=\"{E0CBF06C-CD8B-4647-BB8A-263B43F0F974}\")";
            //string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected", "System.Devices.Aep.ProtocolId" };
            //DeviceWatcher deviceWatcher = DeviceInformation.CreateWatcher(BleSelector, requestedProperties, DeviceInformationKind.AssociationEndpoint);

            //deviceWatcher.Added += async (watcher, args) =>
            //{
            //    Debug.WriteLine($"Added {args.Name}");
            //    await OnScanResult(args);
            //};

            //deviceWatcher.Updated += (watcher, args) =>
            //{
            //    Debug.WriteLine($"Updated {args.Id}");
            //};
            //deviceWatcher.Removed += (watcher, args) =>
            //{
            //    Debug.WriteLine($"Removed {args.Id}");
            //};
            //deviceWatcher.EnumerationCompleted += (watcher, args) =>
            //{
            //    Debug.WriteLine("No more devices found");
            //};
            //deviceWatcher.Start();

            //isScanning = false;

            WatchForRssi();
        }

        private BluetoothLEAdvertisementWatcher rssiWatcher;
        public void WatchForRssi()
        {
            Guid[] serviceUuids = new Guid[2];
            //      serviceUuids[0] = "FFF0";
            //  serviceUuids[0] = "FFB0";
            serviceUuids[0] = Guid.Parse("0000ffb0-0000-1000-8000-00805f9b34fb");
            serviceUuids[1] = Guid.Parse("0000fff0-0000-1000-8000-00805f9b34fb");

            rssiWatcher = new BluetoothLEAdvertisementWatcher();
            rssiWatcher.ScanningMode = BluetoothLEScanningMode.Passive;
            rssiWatcher.AdvertisementFilter.Advertisement.ServiceUuids.Add(serviceUuids[0]);
            //why ???
            //rssiWatcher.AdvertisementFilter.Advertisement.ServiceUuids.Add(serviceUuids[1]);

            //rssiWatcher.SignalStrengthFilter.InRangeThresholdInDBm = -80;
            //rssiWatcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -100;
            //rssiWatcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(2500);
            //rssiWatcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(500);
            rssiWatcher.Received += OnScanResult;
            rssiWatcher.Start();
        }

        protected bool DeviceExistsInDiscoveredList(BluetoothLEDevice device)
        {
            //foreach (var d in discoveredDevices)
            //{
            //    if (device.BluetoothAddress == ((BluetoothLEDevice) d.NativeDevice).BluetoothAddress)
            //        return true;
            //}
            return false;
        }

        public void StopScanningForDevices()
        {
            this.isScanning = false;
            rssiWatcher.Stop();
        }
        public List<string> RegisterControllers;
        public List<string> ControllersNickName;
        public async void OnScanResult(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            //       System.Diagnostics.Debug.WriteLine("New Device = " +  result.Device.Name.ToString());

            //   System.Diagnostics.Debug.WriteLine("result.Rssi = " + result.Rssi.ToString());
            //            if (result.AdvertisementType.ToString().Contains("toothLE"))
            BluetoothLEDevice nativeDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(eventArgs.BluetoothAddress);
            if (nativeDevice != null)
            {
                for (int i = 0; i < DiscoveredDevices.Count; i++)
                {
                    if (DiscoveredDevices[i].Name == eventArgs.Advertisement.LocalName)
                    {
                        return;
                    }
                }

                BLEDevice device = new BLEDevice(nativeDevice); //= new Device(await BluetoothLEDevice.FromIdAsync(result.Id));
                device.Rssi = (Int16)eventArgs.RawSignalStrengthInDBm;
                bool isNonNavitasDevice = !device.Name.Contains("TAC_") && !device.Name.Contains("TSX_") && (device.Name != "SENSSUN FAT");
                if (device.Rssi == 127 || isNonNavitasDevice) //Dec 2018, looks like it is a weak signal, some come in like this so put it at the bottom
                    device.Rssi = -127;
                if (DiscoveredDevices.Count == 0)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        DiscoveredDevices.Add(device);
                        if (isNonNavitasDevice)
                            device.IsClickable = false;
                        else
                        {
                            int index = RegisterControllers.Select((serialNumber, i) => new { serialNumber, i }).
                    FirstOrDefault(obj => obj.serialNumber == device.Name)?.i ?? -1;

                            if (index != -1)
                            {
                                await UpdateRegisteredControllerView(device, index);
                            }
                            else
                            {
                                try
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
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine("Pull to refresh exception: " + ex.Message);
                                }
                            }
                        }
                    });
                }
                else
                {
                    int iIndex = 0;

                    for (int i = 0; i < DiscoveredDevices.Count; i++)
                    {
                        //-78 < -75
                        if ((device.Rssi < ((BLEDevice)DiscoveredDevices[i]).Rssi) &&
                        ((BLEDevice)DiscoveredDevices[i]).Name.Contains("TAC_"))
                        {
                            iIndex++;
                        }
                    }
                    if (DiscoveredDevices.Count >= iIndex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            if (isNonNavitasDevice)
                                device.IsClickable = false;
                            DiscoveredDevices.Insert(iIndex, device);
                            int index = RegisterControllers.Select((serialNumber, i) => new { serialNumber, i }).
                    FirstOrDefault(obj => obj.serialNumber == device.Name)?.i ?? -1;
                            if (index != -1)
                            {
                                await UpdateRegisteredControllerView(device, index);
                            }
                            else
                            {
                                try
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
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine("Pull to refresh exception: " + ex.Message);
                                }
                            }
                        });
                    }
                }
            }
        }

        private async Task UpdateRegisteredControllerView(BLEDevice device, int pos)
        {
            MainThread.BeginInvokeOnMainThread(() => device.IsRegisteredUser = true);
            if (pos < ControllersNickName.Count && pos != -1)
            {
                // Nickname is existing in persistent property
                MainThread.BeginInvokeOnMainThread(() => device.NickName = ControllersNickName[pos]);
                try
                {
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
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"UpdateRegisteredControllerView exception:  {e.Message}");
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

        byte[] ReceivePacket;
        int NumberOfBytesExpected;
        byte[] checksum;
        public async Task<bool> ConnectToDevice(IDevice device)
        {
            StopScanningForDevices();

            bool bReturn = false;
            ReceivePacket = null;
            Task<bool> task = Connect(device);
            bReturn = await task;

            return bReturn;
            //TODO ConectToDevice
            this.connectedDevices.Add(device);
            //            DeviceConnected(this, new DeviceConnectionEventArgs() {Device = device, ErrorMessage = "error"});
            return true;
        }

        public void Close()
        {

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                rssiWatcher.Received -= OnScanResult;
                readGattCharacteristicWithValue.NativeCharacteristic.ValueChanged -= ValueChanged;
                bleDevice.ConnectionStatusChanged -= OnDeviceConnectionStatusChanged;

                ////foreach (DeviceInformation d in devices)
                ////{
                var result = await bleDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);

                //if (result.Status == GattCommunicationStatus.Success)
                //{
                //    foreach (var service in result.Services)
                //    {
                //        if (service.Session.SessionStatus == GattSessionStatus.Active)
                //            service.Session.Dispose();

                //        service.Dispose();
                //    }
                //    //bleDevice.Dispose();
                //    //}
                //}
                System.Diagnostics.Debug.WriteLine("gatt Close");
                DeviceDisconnected(null, null);
            });
        }

        public void InitializeDeviceList(IDictionary<string, string> userCredentials)
        {

            DiscoveredDevices = new ObservableCollection<IDevice>();
            _UserCredentials = userCredentials;
            RegisterControllers = Regex.Replace(Authentication.GetUserCredentials()["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",").Split(',').ToList();
            ControllersNickName = Authentication.GetUserCredentials().ContainsKey("NickName") ? Authentication.GetUserCredentials()["NickName"].Split(',').ToList() : new List<string>();
            //_centraldelegate.SetObservatbleDeviceList(this.DiscoveredDevices, UserCredentials);
            System.Diagnostics.Debug.WriteLine("DiscoveredDevices.Count = " + DiscoveredDevices.Count.ToString());

#if OLD
            bleDevice = null;
#endif

        }


        public bool Write(byte[] data)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine(DateTime.Now.Millisecond + ": Write received, Count = " + data.Length.ToString());

                var dataWriter = new DataWriter();

                dataWriter.WriteBytes(data);

                var buffer = dataWriter.DetachBuffer();

                GattCommunicationStatus status = GattCommunicationStatus.Unreachable;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    status = await writeGattCharacteristicWithValue.NativeCharacteristic.WriteValueAsync(buffer, GattWriteOption.WriteWithoutResponse);
                });
                //while (status == GattCommunicationStatus.Unreachable) ;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Write unsuccessful " + ex.Message);

            }
            // var status = await _gattCharacteristicWithValue.NativeCharacteristic.WriteValueAsync(buffer, GattWriteOption.WriteWithoutResponse);

            //  Debug.WriteLine("Write status: " + status.ToString());
            return true;

        }

        void ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            //so here's the data
            Debug.WriteLine("Characteristic Value Changed");

            var count = args.CharacteristicValue.Length;
            byte[] buffer = new byte[count];
            var data = String.Empty;

            DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(buffer);

            PacketResponseReceived(sender, new PacketReceivedEventArgs(buffer));

            ////and notify
            //ValueUpdated(this, new CharacteristicReadEventArgs()
            //{
            //    Characteristic = this,
            //});
        }


        public void DisconnectDevice(IDevice device)
        {
            //TODO DisconnetDevice
            this.connectedDevices.Remove(device);
        }

        public async Task<bool> IsEnabled()
        {
            return true;// _adapter.IsEnabled;
        }
        public enum GattNativeServiceUuid : ushort
        {
            None = 0,
            AlertNotification = 0x1811,
            Battery = 0x180F,
            BloodPressure = 0x1810,
            CurrentTimeService = 0x1805,
            CyclingSpeedandCadence = 0x1816,
            DeviceInformation = 0x180A,
            GenericAccess = 0x1800,
            GenericAttribute = 0x1801,
            Glucose = 0x1808,
            HealthThermometer = 0x1809,
            HeartRate = 0x180D,
            HumanInterfaceDevice = 0x1812,
            ImmediateAlert = 0x1802,
            LinkLoss = 0x1803,
            NextDSTChange = 0x1807,
            PhoneAlertStatus = 0x180E,
            ReferenceTimeUpdateService = 0x1806,
            RunningSpeedandCadence = 0x1814,
            ScanParameters = 0x1813,
            TxPower = 0x1804,
            SimpleKeyService = 0xFFE0
        }
        public enum GattNativeCharacteristicUuid : ushort
        {
            None = 0,
            AlertCategoryID = 0x2A43,
            AlertCategoryIDBitMask = 0x2A42,
            AlertLevel = 0x2A06,
            AlertNotificationControlPoint = 0x2A44,
            AlertStatus = 0x2A3F,
            Appearance = 0x2A01,
            BatteryLevel = 0x2A19,
            BloodPressureFeature = 0x2A49,
            BloodPressureMeasurement = 0x2A35,
            BodySensorLocation = 0x2A38,
            BootKeyboardInputReport = 0x2A22,
            BootKeyboardOutputReport = 0x2A32,
            BootMouseInputReport = 0x2A33,
            CSCFeature = 0x2A5C,
            CSCMeasurement = 0x2A5B,
            CurrentTime = 0x2A2B,
            DateTime = 0x2A08,
            DayDateTime = 0x2A0A,
            DayofWeek = 0x2A09,
            DeviceName = 0x2A00,
            DSTOffset = 0x2A0D,
            ExactTime256 = 0x2A0C,
            FirmwareRevisionString = 0x2A26,
            GlucoseFeature = 0x2A51,
            GlucoseMeasurement = 0x2A18,
            GlucoseMeasurementContext = 0x2A34,
            HardwareRevisionString = 0x2A27,
            HeartRateControlPoint = 0x2A39,
            HeartRateMeasurement = 0x2A37,
            HIDControlPoint = 0x2A4C,
            HIDInformation = 0x2A4A,
            IEEE11073_20601RegulatoryCertificationDataList = 0x2A2A,
            IntermediateCuffPressure = 0x2A36,
            IntermediateTemperature = 0x2A1E,
            LocalTimeInformation = 0x2A0F,
            ManufacturerNameString = 0x2A29,
            MeasurementInterval = 0x2A21,
            ModelNumberString = 0x2A24,
            NewAlert = 0x2A46,
            PeripheralPreferredConnectionParameters = 0x2A04,
            PeripheralPrivacyFlag = 0x2A02,
            PnPID = 0x2A50,
            ProtocolMode = 0x2A4E,
            ReconnectionAddress = 0x2A03,
            RecordAccessControlPoint = 0x2A52,
            ReferenceTimeInformation = 0x2A14,
            Report = 0x2A4D,
            ReportMap = 0x2A4B,
            RingerControlPoint = 0x2A40,
            RingerSetting = 0x2A41,
            RSCFeature = 0x2A54,
            RSCMeasurement = 0x2A53,
            SCControlPoint = 0x2A55,
            ScanIntervalWindow = 0x2A4F,
            ScanRefresh = 0x2A31,
            SensorLocation = 0x2A5D,
            SerialNumberString = 0x2A25,
            ServiceChanged = 0x2A05,
            SoftwareRevisionString = 0x2A28,
            SupportedNewAlertCategory = 0x2A47,
            SupportedUnreadAlertCategory = 0x2A48,
            SystemID = 0x2A23,
            TemperatureMeasurement = 0x2A1C,
            TemperatureType = 0x2A1D,
            TimeAccuracy = 0x2A12,
            TimeSource = 0x2A13,
            TimeUpdateControlPoint = 0x2A16,
            TimeUpdateState = 0x2A17,
            TimewithDST = 0x2A11,
            TimeZone = 0x2A0E,
            TxPowerLevel = 0x2A07,
            UnreadAlertStatus = 0x2A45,
            AggregateInput = 0x2A5A,
            AnalogInput = 0x2A58,
            AnalogOutput = 0x2A59,
            CyclingPowerControlPoint = 0x2A66,
            CyclingPowerFeature = 0x2A65,
            CyclingPowerMeasurement = 0x2A63,
            CyclingPowerVector = 0x2A64,
            DigitalInput = 0x2A56,
            DigitalOutput = 0x2A57,
            ExactTime100 = 0x2A0B,
            LNControlPoint = 0x2A6B,
            LNFeature = 0x2A6A,
            LocationandSpeed = 0x2A67,
            Navigation = 0x2A68,
            NetworkAvailability = 0x2A3E,
            PositionQuality = 0x2A69,
            ScientificTemperatureinCelsius = 0x2A3C,
            SecondaryTimeZone = 0x2A10,
            String = 0x2A3D,
            TemperatureinCelsius = 0x2A1F,
            TemperatureinFahrenheit = 0x2A20,
            TimeBroadcast = 0x2A15,
            BatteryLevelState = 0x2A1B,
            BatteryPowerState = 0x2A1A,
            PulseOximetryContinuousMeasurement = 0x2A5F,
            PulseOximetryControlPoint = 0x2A62,
            PulseOximetryFeatures = 0x2A61,
            PulseOximetryPulsatileEvent = 0x2A60,
            SimpleKeyState = 0xFFE1
        }
        private static bool IsSigDefinedUuid(Guid uuid)
        {
            var bluetoothBaseUuid = new Guid("00000000-0000-1000-8000-00805F9B34FB");

            var bytes = uuid.ToByteArray();
            // Zero out the first and second bytes
            // Note how each byte gets flipped in a section - 1234 becomes 34 12
            // Example Guid: 35918bc9-1234-40ea-9779-889d79b753f0
            //                   ^^^^
            // bytes output = C9 8B 91 35 34 12 EA 40 97 79 88 9D 79 B7 53 F0
            //                ^^ ^^
            bytes[0] = 0;
            bytes[1] = 0;
            var baseUuid = new Guid(bytes);
            return baseUuid == bluetoothBaseUuid;
        }
        public static ushort ConvertUuidToShortId(Guid uuid)
        {
            // Get the short Uuid
            var bytes = uuid.ToByteArray();
            var shortUuid = (ushort)(bytes[0] | (bytes[1] << 8));
            return shortUuid;
        }
        private async Task<bool> Connect(IDevice device)
        {
            //TODO ConectToDevice
            //this.connectedDevices.Add(device);
            bleDevice = (BluetoothLEDevice)device.GetBLEDevice();
            bleDevice.ConnectionStatusChanged += OnDeviceConnectionStatusChanged;
            GattDeviceServicesResult result = await bleDevice.GetGattServicesAsync(BluetoothCacheMode.Uncached);

            if (result.Status == GattCommunicationStatus.Success)
            {
                var services = result.Services;
                //rootPage.NotifyUser(String.Format("Found {0} services", services.Count), NotifyType.StatusMessage);
                foreach (var service in services)
                {
                    //ServiceList.Items.Add(new ComboBoxItem { Content = DisplayHelpers.GetServiceName(service), Tag = service });
                    if (IsSigDefinedUuid(service.Uuid))
                    {
                        GattNativeServiceUuid serviceName;
                        if (Enum.TryParse(ConvertUuidToShortId(service.Uuid).ToString(), out serviceName))
                        {
                            //return serviceName.ToString();
                        }
                        IReadOnlyList<GattCharacteristic> characteristics = null;
                        var gattResult = await service.GetCharacteristicsAsync(BluetoothCacheMode.Uncached);
                        if (gattResult.Status == GattCommunicationStatus.Success)
                        {
                            characteristics = gattResult.Characteristics;
                            foreach (GattCharacteristic c in characteristics)
                            {
                                GattNativeCharacteristicUuid characteristicName;
                                if (Enum.TryParse(ConvertUuidToShortId(c.Uuid).ToString(),
                                    out characteristicName))
                                {
                                    var characteristic = new GattCharacteristicWithValue(c);
                                    if (characteristic.NativeCharacteristic.CharacteristicProperties == GattCharacteristicProperties.Notify)
                                    {
                                        readGattCharacteristicWithValue = characteristic;
                                        GattCommunicationStatus status = GattCommunicationStatus.Unreachable;
                                        status = await c.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                                        readGattCharacteristicWithValue.NativeCharacteristic.ValueChanged += ValueChanged;
                                        DeviceConnected(null, null);
                                    }
                                    if (c.Uuid.ToString() == "0000ffb1-0000-1000-8000-00805f9b34fb" || c.Uuid.ToString() == "0000fff1-0000-1000-8000-00805f9b34fb")
                                    {
                                        writeGattCharacteristicWithValue = characteristic;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //rootPage.NotifyUser("Error accessing service.", NotifyType.ErrorMessage);

                            // On error, act as if there are no characteristics.
                            characteristics = new List<GattCharacteristic>();
                        }
                    }

                }
            }
            else
            {
                //rootPage.NotifyUser("Device unreachable", NotifyType.ErrorMessage);
            }
            return true;
        }

        private void OnDeviceConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {
            if (sender.ConnectionStatus == BluetoothConnectionStatus.Connected)
            {
                System.Diagnostics.Debug.WriteLine("Connected");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Disconnected");
                DeviceDisconnected(null, null);
            }

        }
    }
}
