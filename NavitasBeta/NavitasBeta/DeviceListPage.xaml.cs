using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms.Xaml;

using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace NavitasBeta
{
    public class RssiToHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float height = (127 + ((Int16)value < -27 ? (Int16)value : -27));
            return (float)0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (0);
        }

    }
    public partial class DeviceListPage : ContentPage
    {

        public ICommand Refresh { private set; get; }
        public ICommand SelectAppDemoCommand { private set; get; }

        CustomModal modal = new CustomModal();
        public DeviceListPage()
        {
            try
            {
                InitializeComponent();
                // WT Sep 2023, iOS's abosulute layout design required to have every visual element constructed at a first place
                if (!(this.Content as AbsoluteLayout).Children.Any((view) => view is ContentView))
                {
                    AbsoluteLayout.SetLayoutFlags(modal, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(modal, new Rectangle(0, 0, 1, 1));
                    modal.IsVisible = false;
                    (this.Content as AbsoluteLayout).Children.Add(modal);
                }
                SelectAppDemoCommand = new Command(SelectAppDemo);
                System.Diagnostics.Debug.WriteLine("ScanForDevices");
                
                NavigationPage.SetHasBackButton(this, false); //menu still shows up 
                //NavigationPage.SetBackButtonTitle(this, "Done");
                NavigationPage.SetHasNavigationBar(this, false); //so don't show anything

                App._devicecommunication.Timeout += Timeout;
                App.BluetoothAdapter.DeviceConnected += DeviceConnected;
                App.BluetoothAdapter.DeviceDisconnected += DeviceDisconnected;

                IsDeviceConnected = false;
                UserChangedDevices = false;
                UserUnselectedDevice = false;

                deviceCollectionView.SelectionChanged += OnCollectionViewSelectionChanged;
                App._devicecommunication.VehicleTypeDetermined += VehicleTypeDetermined;

                this.BindingContext = this;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: DeviceListPage.xaml.cs" + ex.Message);
            }
        }

        int popUpCount = 0;
        public void popUpConnectionRetryMessage()
        {
            if (popUpCount <= 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    popUpCount += 1;

                    CustomYesNoBox popup = null;
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        popup = new CustomYesNoBox("Error", "  \nDevice Communication timeout.  Reconnect ? ");
                    }
                    else
                        popup = new CustomYesNoBox("Error", "  \nDevice Communication timeout.  Reconnect ?\n\nYou may need to cycle the key switch off and on to resume communication");

                    popup.PopupClosed += (o, closedArgs) =>
                    {
                        if (closedArgs.Button == "Yes")
                        {
                            // Do something on positive response
                            popUpCount -= 1;
                            this.ReconnectToSelectedDevice(null, null);
                        }
                        else if (closedArgs.Button == "No")
                        {
                            popUpCount -= 1;
                            this.CancelReconnectToSelectedDevice(null, null);
                        }
                        else
                        {
                            // Unknown response. Do nothing?
                        }
                    };

                    popup.Show();
                });
            }
        }
        private void InitializePullRequest()
        {
            Refresh = new Command<string>(ExecuteSelection, CanExecuteSelection);
            refreshView.Command = Refresh;
            VerifyRegisteredUserChanged();
        }
        Task ScanForDeviceTask;
        CancellationTokenSource scanningCTS;
        public async void PopulateDeviceList()
        {
            System.Diagnostics.Debug.WriteLine("PopulateDeviceList");
            System.Diagnostics.Debug.WriteLine("Calling InitializeDeviceList");
            InitializeDeviceList();
            var androidVersion = int.Parse(DeviceInfo.VersionString.Split('.')[0]);
            if (DeviceInfo.Platform == DevicePlatform.Android && androidVersion > 9)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(0.1));
                    var location = await Geolocation.GetLocationAsync(request);
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    await DisplayAlert("Location Not Enabled", "Android 10 and 11 require that location is enabled to show Bluetooth devices", "OK");
                }
                catch (Exception ex)
                {
                    //Just in case, other exceptions happen
                }
            }

            scanningCTS = new CancellationTokenSource();

            // keep a reference to the task object and perform further operations on it,
            // such as awaiting its completion, checking its status, or canceling it
            System.Diagnostics.Debug.WriteLine("Calling ScanForDevices");
            ScanForDeviceTask = Task.Run(() => ScanForDevices(), scanningCTS.Token);

            System.Diagnostics.Debug.WriteLine("ScanForDevices returned");
        }
        protected override async void OnAppearing()
        {
            var nowtime = DateTime.Now.Millisecond;
            App.AppConfigurationLevel = await App._MainFlyoutPage.GetAppConfigurationLevel(); //automatically logs in with new credentials

            InitializePullRequest();
            System.Diagnostics.Debug.WriteLine("time = " + (nowtime - DateTime.Now.Millisecond).ToString());
            System.Diagnostics.Debug.WriteLine("OnAppearing");

            if (DeterminingVehicleType == false)
            {
                if (!this.IsDeviceConnected)
                {
                    System.Diagnostics.Debug.WriteLine("Calling PopulateDeviceList");

                    PopulateDeviceList();
                }
                else
                {
                    InstructUsersToRestartBTScanning();
                    CreateAdditionalTimer();
                }
            }


            base.OnAppearing();
            App._MainFlyoutPage.IsGestureEnabled = false;
        }


        void ExecuteSelection(string selection)
        {
            System.Diagnostics.Debug.WriteLine("ExecuteSelection");
            // Check if it is still running
            // var isCompleted = ScanForDeviceTask.IsCompleted;

            // force the thread to complete
            StopBTScanning();
            scanningCTS.Cancel();
            System.Diagnostics.Debug.WriteLine("ExecuteSelection -- ScanForDevices was canceled");

            if (stoppableTimer != null)
            {
                stoppableTimer.Stop();
            }

            if (additionalStoppableTimer != null)
            {
                additionalStoppableTimer.Stop();
            }

            if (!DeterminingVehicleType)
            {
                if (IsDeviceConnected)
                {
                    UserUnselectedDevice = true;
                    StopComThread();
                }
                else
                {
                    PopulateDeviceList();
                    VerifyRegisteredUserChanged();
                }
            }
            // Changing can execute even it always return true ??
            //((Command)Refresh).ChangeCanExecute();
            refreshView.IsRefreshing = false;
        }

        bool CanExecuteSelection(string selection)
        {
            // revert it back
            lblSerialNumber.Text = "Serial Number";
            lblSerialNumber.Margin = new Thickness(0);
            lblSerialNumber.HorizontalOptions = LayoutOptions.Start;
            lblSignal.Opacity = 1;
            return true;
        }

        public void ResetConnectedItems()
        {
            System.Diagnostics.Debug.WriteLine("ResetConnectedItems");
            deviceCollectionView.SelectedItem = null;
        }


        public bool IsDeviceConnected;
        public bool DeterminingVehicleType;
        public void StopComThread()
        {
            System.Diagnostics.Debug.WriteLine("StopComThread");
            App._devicecommunication.StopCommunicationThread();
        }

        public void InitializeDeviceList()
        {
            System.Diagnostics.Debug.WriteLine("Calling App.BluetoothAdapter.InitializeDeviceList");
            App.BluetoothAdapter.InitializeDeviceList(Authentication.GetUserCredentials());
            Device.BeginInvokeOnMainThread(() =>
           {
               deviceCollectionView.ItemsSource = App.BluetoothAdapter.DiscoveredDevices;
           });
        }
        private async void ScanForDevices()
        {
            try
            {
                //TODO: WT Apr 2023, this while loop runs forever, consider cancellation token to be added if necessary.
                // _DeviceListPage is a static variable, so it will not reconstruct when we come back to this page
                bool hasErrorViewBeenSet = false;
                IsIndicatorRunning = true;
                //Reset bottom message
                SetErrorView("bottom", "", "");

                
                while (!await App.BluetoothAdapter.IsEnabled())
                {
                    // Take 1 second to get an accurate result
                    await Task.Delay(1000);
                    if (!hasErrorViewBeenSet)
                    {
                        SetErrorView("center",
                            (String)Application.Current.Resources["BluetoothNotEnabledTitle"],
                            (String)Application.Current.Resources["BluetoothNotEnabledMessage"]);
                        hasErrorViewBeenSet = true;
                    }
                }

                App.BluetoothAdapter.StartScanningForDevices();
                // From Bluetooth scanning first started successfully, then timing it out after 10 min
                DelayStopScanningDevices();
                SetErrorView("center", "Searching for Navitas product", "");
                await Task.Delay(5000);
                if (App.BluetoothAdapter.DiscoveredDevices.Count == 0)
                {
                    SetErrorView("bottom",
                        (String)Application.Current.Resources["VehicleNotFoundTitle"],
                        (String)Application.Current.Resources["VehicleNotFoundMessage"]);
                }
                else
                    IsIndicatorRunning = false;
            }
            catch (OperationCanceledException oce)
            {
                // Handle the cancellation exception here...
                System.Diagnostics.Debug.WriteLine("Task was cancelled: " + oce.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ScanForDevices Exception: {ex.Message}");
            }
        }
        CustomOverlay customOverlay = new CustomOverlay();
        private StoppableTimer stoppableTimer;
        private StoppableTimer additionalStoppableTimer;
        public void DelayStopScanningDevices()
        {
            if(stoppableTimer == null )
            {
                stoppableTimer = new StoppableTimer(TimeSpan.FromMinutes(2), () => {
                    StopBTScanning();
                    InstructUsersToRestartBTScanning();
                    if (Navigation.NavigationStack.Count > 0 &&
                    Navigation.NavigationStack.Last() is DeviceListPage)
                    {
                        // Users might do not know what to do next
                        CreateAdditionalTimer();
                    }
                });
                stoppableTimer.Start();
            }
            else
            {
                stoppableTimer.Start();
            }

        }

        private void InstructUsersToRestartBTScanning()
        {
            // Clean up screen
            SetErrorView("center", "", "");
            IsIndicatorRunning = false;

            lblSerialNumber.Text = "Pull to restart scanning";
            lblSerialNumber.Margin = new Thickness(0, 0, -25, 0);
            lblSerialNumber.HorizontalOptions = LayoutOptions.Center;
            lblSignal.Opacity = 0;

            customOverlay.StartAnimation(lblSerialNumber, "ScaleAnimation");
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                customOverlay.StopAnimation("ScaleAnimation");
                lblSerialNumber.Scale = 1;
                return false;
            });
        }

        private void CreateAdditionalTimer()
        {
            if (additionalStoppableTimer == null)
            {
                additionalStoppableTimer = new StoppableTimer(TimeSpan.FromSeconds(7), InstructUsersToRestartBTScanning);
                additionalStoppableTimer.StartWithLoop();
            }
            else
            {
                additionalStoppableTimer.StartWithLoop();
            }
        }

        private void StopBTScanning()
        {
            App.BluetoothAdapter.StopScanningForDevices();
        }

        private void SetErrorView(string errorPos,string title, string message)
        {
            switch (errorPos)
            {
                case "center":
                    CenterError = new ErrorInfo() { Title = title, Message = message };
                    break;
                case "bottom":
                    BottomError = new ErrorInfo() { Title = title, Message = message };
                    break;
                default:
                    break;
            }
        }

        public IDevice _device;
        public bool bReconnecting = false;
        public async void ReconnectToSelectedDevice(object sender, EventArgs e)
        {
            bReconnecting = true;
            await Connect(_device);
        }

        public async void CancelReconnectToSelectedDevice(object sender, EventArgs e)
        {
            //        if (ControllerTypeLocator.ControllerType == "TAC")
            //       {
            await MainFlyoutPage.BeginInvokeOnMainThreadAsync(() =>
            {
                bReconnecting = false;
                MessagingCenter.Send<DeviceListPage>(this, "CloseDeviceList");
                MessagingCenter.Send<DeviceListPage>(this, "ShowDeviceList");
                DeterminingVehicleType = false;
                IsDeviceConnected = false;
                ResetConnectedItems();
                PopulateDeviceList();
            });

        }

        public void VehicleTypeDetermined(object sender, EventArgs e)
        {
            MessagingCenter.Send<DeviceListPage>(this, "ShowTabPage");
            System.Diagnostics.Debug.WriteLine("VehicleTypeDetermined");
            IsDeviceConnected = true;
            DeterminingVehicleType = false;
        }
        /// <summary>
        /// Connection Stuff
        /// </summary>
        IMode Mode;
        string strMode;
        public async Task Connect(IDevice device)
        {
            System.Diagnostics.Debug.WriteLine("Connect");
            if(device is DemoDevice)
            {
                System.Diagnostics.Debug.WriteLine("Mode = new DemoMode();");
                StopBTScanning();
                scanningCTS.Cancel();
                if (device.Name.Contains("TAC"))
                {
                    Mode = new DemoMode("TAC");
                    //below is used for visual bindable stuff related to DEMO mode
                    App.ViewModelLocator.MainViewModel.IsDemoMode = true;
                    App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = true;
                    
                }
                else if (device.Name.Contains("TSX"))
                {
                    Mode = new DemoMode("TSX");
                    //below is used for visual bindable stuff related to DEMO mode
                    App.ViewModelLocator.MainViewModelTSX.IsDemoMode = true;
                    App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = true;
                   
                }
                strMode = "Demo";
                (Mode as DemoMode).DeviceDisconnected += DeviceDisconnected;
                this.DeviceConnected(null, null);
                MessagingCenter.Send<DeviceListPage>(this, "DisplayDemoAlert");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Mode = new PhysicalMode();");
                Mode = new PhysicalMode();
                strMode = "Physical";
                App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = false;
                App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = false;
                App.ViewModelLocator.MainViewModel.IsDemoMode = false;
                App.ViewModelLocator.MainViewModelTSX.IsDemoMode = false;
                Task<bool> task = App.BluetoothAdapter.ConnectToDevice(device);

                bool result = await task;

                if (result == false)
                {
                    Mode.Close();
                    Mode = null;
                    popUpConnectionRetryMessage(); //A failed connection seems to call disconnect which also pops up but I can't guarantee it.
                }

                System.Diagnostics.Debug.WriteLine("ConnectToDevice returned  = " + result.ToString());
            }
        }

        public bool UserChangedDevices;
        public bool UserUnselectedDevice;
        //bool bTimeout;
        void Timeout(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Device List Timeout");
            //bTimeout = true;
        }

        async void DeviceConnected(object sender, EventArgs e)
        {

            //bTimeout = false;
            System.Diagnostics.Debug.WriteLine("DeviceConnected");

            await App._devicecommunication.StartCommunciationThread(Mode, strMode);
        }

        public void DeviceDisconnected(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                System.Diagnostics.Debug.WriteLine("DeviceDisconnected");
                if (UserChangedDevices)
                {
                    DeterminingVehicleType = false;
                    IsDeviceConnected = false;
                    await Connect(_device);

                }
                else if (UserUnselectedDevice)
                {
                    DeterminingVehicleType = false;
                    IsDeviceConnected = false;
                    ResetConnectedItems();
                    PopulateDeviceList();
                }
                else
                {
                    //if (bTimeout == false) //Android times out before disconnecting, ios times out quicker and bTimeout is not set
                    //{
                    //    //await BeginInvokeOnMainThreadAsync(async () => //Jan '20 leave this here until I figure out why I was trying it here
                    //    //{
                    //    //    bReconnecting = false;
                    //    //    MessagingCenter.Send<DeviceListPage>(this, "CloseDeviceList");
                    //    //});

                    //    ResetConnectedItems();
                    //    PopulateDeviceList();
                    //}
                    //else
                    {
                        StopComThread();
                        popUpConnectionRetryMessage();
                    }
                }
                UserChangedDevices = false;
                UserUnselectedDevice = false;
            });
        }

        public async Task VerifyRegisteredUserChanged()
        {
            try
            {
                var serialize = JsonConvert.SerializeObject(await App.ParseManagerAdapter.GetListOfRegisterControllers());
                var vehiclesFromDB = JsonConvert.DeserializeObject<List<Vehicle>>(serialize);

                vehiclesFromDB = vehiclesFromDB.OrderBy(v => v.Name).ToList();

                var persistentRegisterControllers = Regex.Replace(Authentication.GetUserCredentials()["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",").Split(',').ToList();
                if (persistentRegisterControllers[0] == "") {
                    persistentRegisterControllers = new List<string>();
                }
                var persistentControllerNicknames = Authentication.GetUserCredentials().ContainsKey("NickName") ? Authentication.GetUserCredentials()["NickName"].Split(',').ToList() : new List<string>();

                if (vehiclesFromDB.Count != 0 &&
                    persistentRegisterControllers.Count == 0) //Registered User with re-downloaded app to a new phone
                {
                    FileManager.AddVehicleInfoToPersistentProperty(vehiclesFromDB);
                }
                else
                {
                    List<Vehicle> vehiclesFromPersistentProp = new List<Vehicle>();
                    for (int i = 0; i < persistentRegisterControllers.Count; i++)
                    {
                        var nickName = "";
                        if (i < persistentControllerNicknames.Count)
                            nickName = persistentControllerNicknames[i];

                        vehiclesFromPersistentProp.Add(new Vehicle { Name = persistentRegisterControllers[i], NickName = nickName });
                    }

                    vehiclesFromPersistentProp = vehiclesFromPersistentProp.OrderBy(v => v.Name).ToList();

                    //Compare two list if they are different or not
                    if (!Enumerable.SequenceEqual(vehiclesFromPersistentProp, vehiclesFromDB, new VehicleComparer()))
                    {
                        if (vehiclesFromPersistentProp.Count >= vehiclesFromDB.Count)
                        {
                            var serialNumberDifferentFromDBRecords = vehiclesFromPersistentProp.Except(vehiclesFromDB, new VehicleComparer());
                            //Remove from persistent prop, the cloud takes a priority
                            foreach (var vehicle in serialNumberDifferentFromDBRecords)
                            {
                                vehiclesFromPersistentProp.Remove(vehicle);
                            }
                        }

                        var serialNumberDifferentFromPersistentProp = vehiclesFromDB.Except(vehiclesFromPersistentProp, new VehicleComparer());
                        //Let the local decice which vehicles should be added to persistent prop
                        foreach (var vehicle in serialNumberDifferentFromPersistentProp)
                        {
                            int index = vehiclesFromPersistentProp.IndexOf(vehiclesFromPersistentProp.FirstOrDefault(v => v.Name == vehicle.Name));
                            //Adding vehicles to the persistent prop only when there is no such an identical vehicle existed
                            if (index == -1)
                            {
                                vehiclesFromPersistentProp.Add(vehicle);
                            }
                        }
                        vehiclesFromPersistentProp = vehiclesFromPersistentProp.OrderBy(v => v.Name).ToList();
                    }
                    //Sorting serial number and nickname alphabetical
                    FileManager.AddVehicleInfoToPersistentProperty(vehiclesFromPersistentProp);
                }
            }
            catch (NullReferenceException ne)
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    System.Diagnostics.Debug.WriteLine("No Internet Connection from client side");
                else
                    System.Diagnostics.Debug.WriteLine("NullReferenceException ListOfRegisterContrllersChanged with Internet Connected" + ne.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception ListOfRegisterContrllersChanged: " + ex.Message);
            }
        }

        public void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("OnCollectionViewSelectionChanged");
            var device = e.CurrentSelection.FirstOrDefault() as IDevice;
            if(device == null || !(device.Name.Contains("TAC_") || device.Name.Contains("TSX_")))
            {
                // Prevent the app from attempting connect to a strange ble device
                deviceCollectionView.SelectedItem = null;
                return;
            }

            CommunicationDetermined(device);
        }
        
        public void SelectAppDemo(object o)
        {
            modal.Show();
        }
        // WT June 23, I chose ItemTapped event over ItemSelected
        // because it does not trigger event handler twice like ItemSelected does
        public void DemoDeviceTapped(object sender, ItemTappedEventArgs e)
        {
            var device = e.Item as IDevice;
            if (device != null)
            {
                //System.Diagnostics.Debug.WriteLine($"device name : {device.Name} selected");
                //Un-highlight the controller from the list
                deviceCollectionView.SelectedItem = null;
                CommunicationDetermined(device);
                // New user might get lost in Demo Mode,
                // so modifying menu item could help them to navigate back to the device list page at ease
                App._MainFlyoutPage.ModifyMasterPageItems("Communications", "Exit Demo");
            }
            modal.Hide();
        }

        private async Task CommunicationDetermined(IDevice device)
        {
            try
            {
                _device = device;
                App.PresentConnectedController = _device.Name; //do this early here so it is available for connect callbacks like bootmode
                DeterminingVehicleType = true;
                App._MainFlyoutPage.SetDirtyBitToSoftwareRevision();
                System.Diagnostics.Debug.WriteLine($"{nameof(IsDeviceConnected)} == {IsDeviceConnected}");
                if (IsDeviceConnected)
                {
                    UserChangedDevices = true;
                    StopComThread();
                }
                else
                {
                    await Connect(_device);
                }

                MessagingCenter.Send<DeviceListPage>(this, "CloseDeviceList");
                if (stoppableTimer != null)
                {
                    stoppableTimer.Stop();
                }

                if (additionalStoppableTimer != null)
                {
                    additionalStoppableTimer.Stop();
                }

                System.Diagnostics.Debug.WriteLine($"Device {_device.Name} selected returning");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"CommunicationDetermined exception {e.Message}");
            }
        }

        public ICommand DeviceTappedCommand => new Command(DeviceTapped);

        private void DeviceTapped(object sender)
        {
            var device = sender as IDevice;
            System.Diagnostics.Debug.WriteLine($"Device {device.Name} Tapped");
            var prevDevice = deviceCollectionView.SelectedItem as IDevice;
            if (prevDevice != null && device.Name == prevDevice.Name)
            {
                if (!DeterminingVehicleType && IsDeviceConnected)
                {
                    MessagingCenter.Send<DeviceListPage>(this, "CloseDeviceList");
                    if (stoppableTimer != null)
                    {
                        stoppableTimer.Stop();
                    }

                    if (additionalStoppableTimer != null)
                    {
                        additionalStoppableTimer.Stop();
                    }
                }
            }
            else
            {
                // Work around solution to trigger the SelectionChanged event handler
                deviceCollectionView.SelectedItem = device;
            }

         }

        private ErrorInfo _centerError = new ErrorInfo();
        public ErrorInfo CenterError
        {
            get => _centerError;
            set
            {
                if (_centerError != value)
                {
                    _centerError = value;
                    OnPropertyChanged();
                }
            }
        }

        private ErrorInfo _bottomError = new ErrorInfo();
        public ErrorInfo BottomError
        {
            get => _bottomError;
            set
            {
                if (_bottomError != value)
                {
                    _bottomError = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isIndicatorRunning = true;
        public bool IsIndicatorRunning
        {
            get => _isIndicatorRunning;
            set
            {
                if (_isIndicatorRunning != value)
                {
                    _isIndicatorRunning = value;
                    OnPropertyChanged();
                }
            }
        }

    }

    public class ErrorInfo
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class StoppableTimer
    {
        private readonly TimeSpan timespan;
        private readonly Action callback;

        private CancellationTokenSource cancellation;

        public StoppableTimer(TimeSpan timespan, Action callback)
        {
            this.timespan = timespan;
            this.callback = callback;
            cancellation = new CancellationTokenSource();
        }

        public void Start()
        {
            CancellationTokenSource cts = cancellation; // safe copy
            Device.StartTimer(timespan,
                () => {
                    if (cts.IsCancellationRequested) return false;
                    callback.Invoke();
                    return false; // or true for periodic behavior
                });

        }
        
        public void StartWithLoop()
        {
            CancellationTokenSource cts = cancellation; // safe copy
            Device.StartTimer(timespan,
                () => {
                    if (cts.IsCancellationRequested) return false;
                    callback.Invoke();
                    return true; // or true for periodic behavior
                });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref cancellation, new CancellationTokenSource()).Cancel();
        }

        public void Dispose()
        {

        }
    }
}

