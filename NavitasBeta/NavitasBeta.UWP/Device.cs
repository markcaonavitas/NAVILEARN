using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Xamarin.Forms;

//http://stackoverflow.com/questions/35420940/windows-uwp-connect-to-ble-device-after-discovery

namespace NavitasBeta.UWP
{
    class BLEDevice : ViewModelBase, IDevice
    {
        public BLEDevice(BluetoothLEDevice bleDevice)
        {
            this.bleDevice = bleDevice;
            Name = bleDevice.Name;
        }

        /// <summary>
        /// Gets the device name
        /// </summary>
        /// <value>The device name</value>
        public string Name { get; set; }

        public BluetoothLEDevice bleDevice;

        int _rssi = 0;
        public int Rssi
        {
            get => _rssi;
            set
            {
                if (_rssi != value)
                {
                    _rssi = value;
                    OnPropertyChanged(nameof(Rssi));
                }
            }
        }

        bool _IsClickable = true;

        public bool IsClickable
        {
            get { return _IsClickable; }
            set
            {
                SetProperty(ref _IsClickable, value);
            }
        }

        bool _IsRegisteredUser = false;
        public bool IsRegisteredUser
        {
            get { return _IsRegisteredUser; }
            set
            {
                if (_IsRegisteredUser != value)
                {
                    if (value)
                    {
                        FontAttribute = FontAttributes.Bold;
                        SecureText = "(Secured to this account)";
                    }
                    else
                    {
                        FontAttribute = FontAttributes.None;
                        SecureText = "";
                    }
                }
                SetProperty(ref _IsRegisteredUser, value);
            }
        }

        FontAttributes _FontAttribute;
        public FontAttributes FontAttribute
        {
            get { return _FontAttribute; }
            set { SetProperty(ref _FontAttribute, value); }
        }

        string _SecureText = "";
        public string SecureText
        {
            get { return _SecureText; }
            set { SetProperty(ref _SecureText, value); }
        }

        bool _HoldInitialHeightHack = false;
        public bool HoldInitialHeightHack //iOS listviews do not resize when height changes with binding
        {
            get { return _HoldInitialHeightHack; } //show blank space unit one of these is decided
            set { SetProperty(ref _HoldInitialHeightHack, value); }
        }

        bool _HasRegisteredUsersButNotYou = false;
        public bool HasRegisteredUsersButNotYou
        {
            get { return _HasRegisteredUsersButNotYou; }
            set
            {
                if (value)
                {
                    FontAttribute = FontAttributes.None;
                    SecureText = "(Secured)";
                }
                SetProperty(ref _HasRegisteredUsersButNotYou, value);
            }
        }

        private string _nickName = "";
        public string NickName
        {
            get => _nickName;
            set { SetProperty(ref _nickName, value); }
        }

        public object GetBLEDevice()
        {
            return bleDevice;
        }

        public float FilteredRssi = -128;

        public float FilteredRssiStoredForSorting = 0;
    }
}
