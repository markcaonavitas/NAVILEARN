using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NavitasBeta
{
    public class PhysicalMode : IMode
    {


        public PhysicalMode()
        {

        }

        public void SetPacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate)
        {
            App.BluetoothAdapter.PacketResponseReceived += PacketResponseDelegate;
        }

        public void RemovePacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate)
        {
            App.BluetoothAdapter.PacketResponseReceived -= PacketResponseDelegate;
        }

        public void WritePacket(byte[] packetfull)
        {
            //       System.Diagnostics.Debug.WriteLine("WritePacket started");
            var packettosend = new List<byte>();
            byte[] checksum = new byte[2];
            int iNumberOfBlocks = packetfull.Length / 20;
            int iByteIndex2 = 0;
            int iByteIndex1 = 0;
            for (int iBlockIndex = 0; iBlockIndex < iNumberOfBlocks; iBlockIndex++)
            {
                packettosend = new List<byte>();
                for (iByteIndex1 = 0; iByteIndex1 < 20; iByteIndex1++)
                {
                    packettosend.Add(packetfull[iByteIndex2++]);

                }
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling first write");
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    System.Diagnostics.Debug.WriteLine("First write failed");
                }
           //     Task.Delay(20).Wait();
       //         await Task.Delay(20);

            }
            //    System.Diagnostics.Debug.WriteLine("WritePacket Middle");
            packettosend = new List<byte>();
            for (int iByteIndex3 = iByteIndex2; iByteIndex3 < packetfull.Length; iByteIndex3++)
            {
                packettosend.Add(packetfull[iByteIndex3]);
            }
            if (packettosend.Count > 0)
            {
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling Second write");
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    System.Diagnostics.Debug.WriteLine("Second write failed");
                }

           //     Task.Delay(20).Wait();
            }
            //        System.Diagnostics.Debug.WriteLine("WritePacket ended");
        }


        public void SetParameter(SetParameterEventArgs e)
        {
           
            int ParameterValue = 0;

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                GoiParameter parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(i => i.Address == e.bParameterNumber && i.SubsetOfAddress == false);

                //redundant since it is caught in general page parameterComplete functions
                //but may not be done properly in html functions
                if (e.fParameterValue > parameter.MaximumParameterValue)
                    e.fParameterValue = parameter.MaximumParameterValue;
                if (e.fParameterValue < parameter.MinimumParameterValue)
                    e.fParameterValue = parameter.MinimumParameterValue;

                if (e.parameterTypeIsFloat)
                    ParameterValue = (ushort)parameter.GetRawVal(e.fParameterValue);
                else
                    ParameterValue = (ushort)e.rawParameterValue;   //  GetRawVal(e.fParameterValue);

                parameter.bupdate = true; //general method to stop visual parameters from updating before value is written
                System.Diagnostics.Debug.WriteLine("SetParameter " + parameter.Address.ToString());
            }

            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                GoiParameter parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.Address == e.bParameterNumber && i.SubsetOfAddress == false);
                //redundant since it is caught in general page parameterComplete functions
                //but may not be done properly in html functions
                if (e.fParameterValue > parameter.MaximumParameterValue)
                    e.fParameterValue = parameter.MaximumParameterValue;
                if (e.fParameterValue < parameter.MinimumParameterValue)
                    e.fParameterValue = parameter.MinimumParameterValue;

                if (e.parameterTypeIsFloat)
                    ParameterValue = (int)(e.fParameterValue * parameter.Scale);
                else
                    ParameterValue = (int)e.rawParameterValue;   //  GetRawVal(e.fParameterValue);
                //if (parameter.PropertyName == "TESTERTHROTTLEANALOGVALUE")
                //    System.Diagnostics.Debug.WriteLine("TESTERTHROTTLEANALOGVALUE written " + ParameterValue.ToString());

                parameter.bupdate = true; //general method to stop visual parameters from updating before value is written
                System.Diagnostics.Debug.WriteLine("SetParameter " + parameter.Address.ToString());
            }

            //  [0x02][0x54][0x53][0x58][0x00][0x20][0x01][0xCE][chkA][chkB][etx]

            int iBytesToTransmit = 0;
            byte[] buffer = new byte[3 + 7 + 3];


            buffer[0] = 0x02;  // stx
            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                buffer[1] = 0x54;  // 'T'
                buffer[2] = 0x53;  // 'A'
                buffer[3] = 0x58;  // 'C'
            }
            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                buffer[1] = 0x54;  // 'T'
                buffer[2] = 0x41;  // 'A'
                buffer[3] = 0x43;  // 'C'

            }
            buffer[4] = 0x00;  // flags
            buffer[5] = 0x21;  // cmd
            if (e.bParameterNumber > 511)
            {
                buffer[5] = 0x27;  // cmd
                e.bParameterNumber -= 512;
            }
            else if (e.bParameterNumber > 255)
            {
                buffer[5] = 0x24;  // cmd
                e.bParameterNumber -= 256;
            }

            buffer[6] = 3;  // Pln
            buffer[7] = (byte) e.bParameterNumber;
            buffer[8] = (byte)(ParameterValue >> 8);
            buffer[9] = (byte)ParameterValue;

            byte[] checksum = new byte[2];
            iBytesToTransmit = 10;
            fletcher16(checksum, buffer, iBytesToTransmit);
            buffer[iBytesToTransmit] = checksum[0]; // chksum 1
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = checksum[1]; // chksum 2
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = 0x03; // etx
            iBytesToTransmit++;
            //   System.Diagnostics.Debug.WriteLine("Calling SetParameter write ServiceUuid = {0} CharacteristicUuid = {0} ", _ServiceUuid, _CharacteristicUuid);

            //var debug = MainThread.IsMainThread;
            //System.Diagnostics.Debug.WriteLine("Wite Timer 1 " + DateTime.Now.Millisecond.ToString());
            App.BluetoothAdapter.Write(buffer);
            //System.Diagnostics.Debug.WriteLine("Wite Timer 2 " + DateTime.Now.Millisecond.ToString());
            Task.Delay(20).Wait();
            //System.Diagnostics.Debug.WriteLine("Wite Timer 3 "+ DateTime.Now.Millisecond.ToString());

        }

        public void InitBootLoaderMode(byte BootLoaderBit)
        {
            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                int iBytesToTransmit = 0;
                System.Diagnostics.Debug.WriteLine("InitBootLoaderMode(byte BootLoaderBit)");
                byte[] buffer = new byte[18];
                buffer[0] = 0x02;
                buffer[1] = 0x54;
                buffer[2] = 0x53;
                buffer[3] = 0x58;
                buffer[4] = BootLoaderBit;  // flags
                buffer[5] = 0x01;  // Bootloader command 
                buffer[6] = 0x08;
                buffer[7] = 0x00;
                buffer[8] = 0x00;
                buffer[9] = 0x00;
                buffer[10] = 0x00;
                buffer[11] = 0x00;
                buffer[12] = 0x00;
                buffer[13] = 0x00;
                buffer[14] = 0x00;
                byte[] checksum = new byte[2];
                iBytesToTransmit = 15;
                fletcher16(checksum, buffer, iBytesToTransmit);
                buffer[iBytesToTransmit] = checksum[0]; // chksum 1
                iBytesToTransmit++;
                buffer[iBytesToTransmit] = checksum[1]; // chksum 2
                iBytesToTransmit++;
                buffer[iBytesToTransmit] = 0x03; // etx
               

                App.BluetoothAdapter.Write(buffer);
            }

        }


        public void GetBlock(int bParameterNumber, float fBlock)
        {
            int iBytesToTransmit = 0;
            byte[] buffer = new byte[11];
          //  byte[] replybuffer = new byte[138];

            buffer[0] = 0x02;  // stx
            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                buffer[1] = 0x54;  // 'T'
                buffer[2] = 0x53;  // 'S'
                buffer[3] = 0x58;  // 'X'
            }
            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                buffer[1] = 0x54;  // 'T'
                buffer[2] = 0x41;  // 'A'
                buffer[3] = 0x43;  // 'C'

            }

            buffer[4] = 0x00;  // flags
            buffer[5] = (byte) bParameterNumber;  // cmd
            buffer[6] = (byte)1;

            if (bParameterNumber != 0x25)    // cmd to read FLASH sector
            {
                buffer[7] = (byte)fBlock;
            }
            else
            {
                buffer[4] = (byte)(fBlock / 256);  // Bits 10:8 contain sector number from 0 to 7.
                buffer[7] = ((byte)((ushort)fBlock & 0x7F));          // Low byte contains offset into sector.
            }

            byte[] checksum = new byte[2];
            iBytesToTransmit = 8;
            fletcher16(checksum, buffer, iBytesToTransmit);
            buffer[iBytesToTransmit] = checksum[0]; // chksum 1
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = checksum[1]; // chksum 2
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = 0x03; // etx
            iBytesToTransmit++;


            App.BluetoothAdapter.Write(buffer);
            Task.Delay(20).Wait();
        }

        public void Close()
        {
            App.BluetoothAdapter.Close();
        }

        public void fletcher16(byte[] checksum, byte[] data, int len)
        {
            uint sum1 = 0xff, sum2 = 0xff;
            int i = 0;
            while (len > 0)
            {
                int tlen = len > 21 ? 21 : len;
                len -= tlen;
                do
                {
                    sum1 += data[i];
                    sum2 += sum1;
                    i++;
                } while (--tlen > 0);
                sum1 = (sum1 & 0xff) + (sum1 >> 8);
                sum2 = (sum2 & 0xff) + (sum2 >> 8);
            }
            /* Second reduction step to reduce sums to 8 bits */
            sum1 = (sum1 & 0xff) + (sum1 >> 8);
            sum2 = (sum2 & 0xff) + (sum2 >> 8);
            checksum[0] = (byte)sum1;
            checksum[1] = (byte)sum2;
            return;
        }

    }
}
