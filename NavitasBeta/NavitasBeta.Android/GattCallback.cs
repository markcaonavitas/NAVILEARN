using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Bluetooth;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NavitasBeta.Droid
{
    /// <summary>
    /// Gatt callback to handle Gatt events.
    /// </summary>
    public class GattCallback : BluetoothGattCallback
    {

        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        public event EventHandler<EventArgs> CharacteristicFound = delegate { };

        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        public event EventHandler<EventArgs> CharacteristicNotFound = delegate { };


        /// <summary>
        /// Occurs when device connected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceConnected = delegate { };

        /// <summary>
        /// Occurs when device disconnected.
        /// </summary>
        public event EventHandler<EventArgs> DeviceDisconnected = delegate { };

        /// <summary>
        /// Occurs when characteristic value updated.
        /// </summary>
        public event EventHandler<CharacteristicReadEventArgs> CharacteristicValueUpdated = delegate { };

        public bool bFound;
        public GattStatus _status;
        /// <Docs>GATT client</Docs>
        /// <summary>
        /// Raises the connection state change event.
        /// </summary>
        /// <param name="gatt">Gatt.</param>
        /// <param name="status">Status.</param>
        /// <param name="newState">New state.</param>
        public ProfileState _newState;
        public override void OnConnectionStateChange(BluetoothGatt gatt, GattStatus status, ProfileState newState)
        {

            try
            {
                base.OnConnectionStateChange(gatt, status, newState);
                _status = status;
                bFound = true;
                System.Diagnostics.Debug.WriteLine("OnConnectionStateChange status = " + status.ToString() + " NewState = " + newState.ToString() + " OldState = " + _newState.ToString());
                if (status != GattStatus.Success)
                {
                    System.Diagnostics.Debug.WriteLine("OnConnectionStateChange not successfull, it is " + status.ToString());
                    //          gatt.Connect();

                    //
                    if (_newState == ProfileState.Connected)
                        DeviceDisconnected(this, null);
                    if (status.ToString() == "133") //Well known GattStatus does not enumerate this
                    {
                        System.Diagnostics.Debug.WriteLine("OnConnectionStateChange is 133!!!");
                        //this crashes app//DeviceDisconnected(this, null); //let the app know
                        //gatt.Close does not work
                        //gatt.Dispose does not work
                    }
                    return;
                }
                if (_newState != newState)
                {
                    _newState = newState;
                    System.Diagnostics.Debug.WriteLine("_newState = " + _newState.ToString());
                    switch (newState)
                    {
                        case ProfileState.Disconnected:
                            System.Diagnostics.Debug.WriteLine("OnConnectionStateChange = ProfileState.Disconnected");
                            DeviceDisconnected(this, null);

                            break;
                        case ProfileState.Connected:
                            //             bFound = true;
                            System.Diagnostics.Debug.WriteLine("OnConnectionStateChange = ProfileState.Connected");

                            DeviceConnected(this, null);

                            break;
                        case ProfileState.Disconnecting:
                            System.Diagnostics.Debug.WriteLine("OnConnectionStateChange = ProfileState.Disconnecting");
                            break;

                    }


                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("gatt.Services exception =  " + ex.Message);
            }
        }
        BluetoothGattCharacteristic ReadCharacteristic;
        BluetoothGattCharacteristic WriteCharacteristic;
        BluetoothGatt _gatt;
        protected static Java.Util.UUID CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID = Java.Util.UUID.FromString("00002902-0000-1000-8000-00805f9b34fb");
        /// <summary>
        /// Raises the services discovered event.
        /// </summary>
        /// <param name="gatt">Gatt.</param>
        /// <param name="status">Status.</param>
        Java.Util.UUID writeserviceuuid;
        Java.Util.UUID writechacteristicid;

        public override void OnServicesDiscovered(BluetoothGatt gatt, GattStatus status)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("OnServicesDiscovered status = " + status);
                base.OnServicesDiscovered(gatt, status);
                bool bCharacteristicfound = false;
                System.Diagnostics.Debug.WriteLine("gatt.Services = " + gatt.Services.Count.ToString());
                foreach (var s in gatt.Services)
                {
                    System.Diagnostics.Debug.WriteLine("s.Characteristics.Count = " + s.Characteristics.Count.ToString());
                    foreach (BluetoothGattCharacteristic c in s.Characteristics)
                    {
                        System.Diagnostics.Debug.WriteLine("c.Uuid.ToString()  = " + c.Uuid.ToString());
                        if (c.Uuid.ToString() == "0000ffb1-0000-1000-8000-00805f9b34fb" || c.Uuid.ToString() == "0000fff1-0000-1000-8000-00805f9b34fb")
                        {
                            bCharacteristicfound = true;
                            System.Diagnostics.Debug.WriteLine("gatt WriteCharacteristic found");
                            System.Diagnostics.Debug.WriteLine("UUID = " + c.Uuid.ToString());
                            writeserviceuuid = s.Uuid;
                            writechacteristicid = c.Uuid;
                            WriteCharacteristic = c;
                        }
                        if (c.Uuid.ToString() == "0000ffb2-0000-1000-8000-00805f9b34fb" || c.Uuid.ToString() == "0000fff2-0000-1000-8000-00805f9b34fb")
                        {
                            gatt.SetCharacteristicNotification(c, true);
                            BluetoothGattDescriptor descriptor = c.GetDescriptor(CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID);
                            descriptor.SetValue(BluetoothGattDescriptor.EnableNotificationValue.ToArray());
                            gatt.WriteDescriptor(descriptor); //descriptor write operation successfully started? 
                            System.Diagnostics.Debug.WriteLine("gatt ReadCharacteristic found");
                            System.Diagnostics.Debug.WriteLine("UUID = " + c.Uuid.ToString());
                            ReadCharacteristic = c;
                        }
                    }
                }

                if (bCharacteristicfound)
                {
                    _gatt = gatt;
                    //   gatt.Connect();
                    //   System.Threading.Tasks.Task.Delay(2000).Wait();
                    System.Diagnostics.Debug.WriteLine("Calling CharacteristicFound");
                    CharacteristicFound(null, null);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Calling CharacteristicNotFound");
                    CharacteristicNotFound(null, null);
                }



                //  if (status != GattStatus.Success)
                //    return;



            }
            catch (Exception)
            {

                System.Diagnostics.Debug.WriteLine("OnServicesDiscovered exception " + status.ToString());
                //throw;
            }
        }

        public BluetoothGatt GetGatt()
        {
            return _gatt;
        }


        public BluetoothGattCharacteristic GetWriteCharacteristic()
        {
            return WriteCharacteristic;
        }

        public Java.Util.UUID GetWriteServiceUUID()
        {
            return writeserviceuuid;
        }

        public Java.Util.UUID GetWriteCharacteristicUUID()
        {
            return this.writechacteristicid;
        }



        /// <summary>
        /// Raises the characteristic read event.
        /// </summary>
        /// <param name="gatt">Gatt.</param>
        /// <param name="characteristic">Characteristic.</param>
        /// <param name="status">Status.</param>
        public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            base.OnCharacteristicRead(gatt, characteristic, status);

            if (status != GattStatus.Success)
                return;

        }
        volatile public bool bWriteFinished;
        /// <summary>
        /// Raises the characteristic write event.
        /// </summary>
        /// <param name="gatt">Gatt.</param>
        /// <param name="characteristic">Characteristic.</param>
        /// <param name="status">Status.</param>
        public override void OnCharacteristicWrite(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, GattStatus status)
        {
            //    System.Diagnostics.Debug.WriteLine("OnCharacteristicWrite");
            base.OnCharacteristicWrite(gatt, characteristic, status);
            //    System.Diagnostics.Debug.WriteLine("OnCharacteristicWrite status =  " +  status.ToString());
            if (status != GattStatus.Success)
            {
                System.Diagnostics.Debug.WriteLine("OnCharacteristicWrite not succussful status =  " + status.ToString());
                return;

            }
            //     System.Diagnostics.Debug.WriteLine("bWriteFinished = true");            

            bWriteFinished = true;
        }

        /// <Docs>GATT client the characteristic is associated with</Docs>
        /// <summary>
        /// Callback triggered as a result of a remote characteristic notification.
        /// </summary>
        /// <para tool="javadoc-to-mdoc">Callback triggered as a result of a remote characteristic notification.</para>
        /// <format type="text/html">[Android Documentation]</format>
        /// <since version="Added in API level 18"></since>
        /// <param name="gatt">Gatt.</param>
        /// <param name="characteristic">Characteristic.</param>
        public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
        {
            base.OnCharacteristicChanged(gatt, characteristic);
            byte[] Value = characteristic.GetValue();
            CharacteristicValueUpdated(this, new CharacteristicReadEventArgs(Value));

        }
    }
}
