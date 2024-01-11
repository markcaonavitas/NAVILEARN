using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class DemoMode : IMode
    {

        const int CMD_PACKET_INDEX = 5;

        const int COMMAND_SET_PARAM = 0x21;
        const int COMMAND_GET_PARAM = 0x20;

        const int COMMAND_GET_PAGE1_PARAM  = 0x23;
        const int COMMAND_SET_PAGE1_PARAM  = 0x24;

        const int COMMAND_GET_PAGE2_PARAM  = 0x26;
        const int COMMAND_SET_PAGE2_PARAM  = 0x27;

        const int PACKET_DATA_LENGHT_INDEX = 6;
        const int PACKET_DATA_0_INDEX = 7;
        const int PARAMETER_BUFFER_SIZE = 1024; //way mo
        const int SCOPE_BUFFER_SIZE = 1024;
        const int PARAMETER_VALUES_BUFFER_SIZE = 128;
        Int16[] Parameters;
        UInt16[] ScopeBuffer;
        byte[] ParamValues;

        System.Threading.CancellationTokenSource cancellationTokenSource;

        public event EventHandler<PacketReceivedEventArgs> PacketResponseReceived = delegate { };

        public event EventHandler<EventArgs> DeviceDisconnected = delegate { };

        char[] identifierBytes;
        Boolean demoIsForTACisTrueTSXisFalse = true;

        public DemoMode(string typeIdentifier)
        {
            identifierBytes = new char[3];
            if (typeIdentifier == "TAC") demoIsForTACisTrueTSXisFalse = true;
            else demoIsForTACisTrueTSXisFalse = false;

            ScopeBuffer = new UInt16[SCOPE_BUFFER_SIZE];
            for (UInt16 i = 0; i < SCOPE_BUFFER_SIZE; i++)
            {
                ScopeBuffer[i] = i;
            }

            Parameters = new Int16[PARAMETER_BUFFER_SIZE];
            ParamValues = new byte[PARAMETER_VALUES_BUFFER_SIZE];

            //One time setup so it can be over written later
            Parameters[App.ViewModelLocator.GetParameter("SOFTWAREREVISION").Address] = 8003;
            Parameters[App.ViewModelLocator.GetParameter("NONFATALOTTRIPC").Address] = 100;
            Parameters[App.ViewModelLocator.GetParameter("MILESORKILOMETERS").Address] = 0;

            // It's a bit tricky to set default Display Unit for TSX, due to its address = -1
            App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue = 0;

            StartThread();
        }
        private void StartThread()
        {
            cancellationTokenSource = new System.Threading.CancellationTokenSource();


            Task.Factory.StartNew(ThreadMethod, cancellationTokenSource.Token);
            // Device.StartTimer()

        }

        private void StopThread()
        {
            System.Diagnostics.Debug.WriteLine("StopThread");
            if (cancellationTokenSource != null)
            {
                System.Diagnostics.Debug.WriteLine("Calling ThreadMethod Cancel token");
                cancellationTokenSource.Cancel();
            }
        }


        void ThreadMethod()
        {
            System.Diagnostics.Debug.WriteLine("ThreadMethod");
            UInt32 loopcounter = 1;
            Int16 rpm = 0;
            Int16 motortemp = 0;
            Int16 batteryvoltage = 0;
            Int16 vehiclelock = 0;
            Int16 switches = 0;
            Int16 error = 0;

            bool toggle = false;
            try
            {
                //double previousTime = 0.0f;
                while (true)
                {
                    //System.Diagnostics.Debug.WriteLine("Demo Timer" + ((double)DateTime.Now.Ticks - previousTime).ToString());
                    //previousTime = (double)DateTime.Now.Ticks;
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        System.Diagnostics.Debug.WriteLine("ThreadMethod Cancellation seen");
                        throw (new Exception());
                    }
                    if (!demoIsForTACisTrueTSXisFalse)
                    {
                        Parameters[App.ViewModelLocator.GetParameterTSX("PARMOTORRPM").Address] = (short)(32768 + rpm); //its a simple user from to so do it like this

                        GoiParameter temperature = App.ViewModelLocator.GetParameterTSX("PARTEMPERATURE");
                        Parameters[temperature.Address] = (short)temperature.GetRawVal(motortemp); //not so simple

                        GoiParameter voltage = App.ViewModelLocator.GetParameterTSX("PARBATTERYVOLTS");
                        Parameters[voltage.Address] = (short)voltage.GetRawVal((short)(motortemp / 2)); //why not, it's going from 0 100

                        Parameters[App.ViewModelLocator.GetParameterTSX("PARPARAMETERMAPVERSION").Address] = 1300;
                        Parameters[App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").Address] = 1152;
                        Parameters[App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").Address] = 2176;
                        //rethink this, the address is -1 and crash with bad index Parameters[App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").Address] = 35;
                        Parameters[App.ViewModelLocator.GetParameterTSX("PARSWITCHSTATES").Address] = switches;
                        Parameters[App.ViewModelLocator.GetParameterTSX("VEHICLELOCKED").Address] = vehiclelock;
                        Parameters[App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").Address] = error;
                    }
                    else
                    {
                        Parameters[App.ViewModelLocator.GetParameter("ROTORRPM").Address] = rpm;
                        Parameters[App.ViewModelLocator.GetParameter("MTTEMPC").Address] = motortemp;
                        Parameters[App.ViewModelLocator.GetParameter("VBATVDC").Address] = (short)(motortemp/4); //why not, it's going from 0 100                        
                        Parameters[App.ViewModelLocator.GetParameter("TIREDIAMETER").Address] = 1152/2;
                        Parameters[App.ViewModelLocator.GetParameter("REARAXLERATIO").Address] = 2176;
                        Parameters[App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").Address] = 35;
                        Parameters[App.ViewModelLocator.GetParameter("SWITCHBITS").Address] = switches;
                        Parameters[App.ViewModelLocator.GetParameter("VEHICLELOCKED").Address] = vehiclelock;
                        Parameters[App.ViewModelLocator.GetParameter("GROUPONEFAULTS").Address] = error;
                        Parameters[App.ViewModelLocator.GetParameter("SLIPREFSNAP2").Address] = (short)(0.8 * 256);
                        Parameters[App.ViewModelLocator.GetParameter("MAXCURRENT").Address] = (short)(0.9 * 4096);
                    }
                    rpm += 20;
                    if (rpm >= 7000)
                    {
                        rpm = 0;

                    }

                    if (motortemp++ >= 200)
                    {
                        motortemp = 0;
                    }
                    batteryvoltage += 20;
                    if (batteryvoltage >= 10240)
                    {
                        batteryvoltage = 0;
                    }
                    //#if REMOVED
                    if (loopcounter++ >= 4000000000)
                    {
                        loopcounter = 1;
                    }

                    if (((loopcounter % 100) == 0))
                    {
                        toggle ^= true;
                        if (toggle)
                        {
                            switches = 0x0010;
                            vehiclelock = 0;
                            error = 1;
                        }
                        else
                        {
                            switches = 0x0002;
                            vehiclelock = 1;
                            error = 0;
                        }
                    }
                    if ((loopcounter % 300) == 0)
                    {
                        switches = 0x0004;
                    }
                    Task.Delay(50).Wait();
                }
            }
            catch (Exception e)
            {
                //#if PUT_THIS_BACK
                DeviceDisconnected(this, null);
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine("ThreadMethod exit");
            }

        }




        public void SetPacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate)
        {
            this.PacketResponseReceived += PacketResponseDelegate;
        }

        public void RemovePacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate)
        {
            this.PacketResponseReceived -= PacketResponseDelegate;
        }

        public void WritePacket(byte[] packetfull)
        {
            if (packetfull.Length != 1)
            { //skip boot loader check
                byte[] buffer = new byte[10 + (packetfull.Length - 10) * 2];
                byte[] checksum = new byte[2];
                for (int i = 0; i < packetfull.Length; i++)
                {
                    buffer[i] = packetfull[i];
                }
                if (demoIsForTACisTrueTSXisFalse)
                {
                    buffer[1] = (byte)'T';    // Controller model
                    buffer[2] = (byte)'A';
                    buffer[3] = (byte)'C';
                }
                else
                {
                    buffer[1] = (byte)'T';    // Controller model
                    buffer[2] = (byte)'S';
                    buffer[3] = (byte)'X';
                }

                switch (packetfull[CMD_PACKET_INDEX])
                {

                    case 0x01:
                    case COMMAND_GET_PARAM:
                    case COMMAND_GET_PAGE1_PARAM:
                    case COMMAND_GET_PAGE2_PARAM:
                        if ((packetfull[PACKET_DATA_LENGHT_INDEX] >= 1) && (packetfull[PACKET_DATA_LENGHT_INDEX] <= 64))
                        {
                            for (int iIndex = 0; iIndex < packetfull[PACKET_DATA_LENGHT_INDEX]; iIndex++)
                            {
                                int iParamNumToGet = packetfull[PACKET_DATA_0_INDEX + iIndex];
                                if ((iParamNumToGet >= 0) && (iParamNumToGet <= 255))
                                {
                                    if (packetfull[CMD_PACKET_INDEX] == 0x23)
                                        iParamNumToGet += 256;
                                    if (packetfull[CMD_PACKET_INDEX] == 0x26)
                                        iParamNumToGet += 512;
                                    // little endian
                                    ParamValues[iIndex * 2] = (byte)(Parameters[iParamNumToGet] >> 8);
                                    ParamValues[iIndex * 2 + 1] = (byte)(Parameters[iParamNumToGet] & 0x00ff);
                                }
                            }
                            // The payload will alway float.
                            buffer[PACKET_DATA_LENGHT_INDEX] *= 2;
                            for (int iIndex = 0; iIndex < buffer[PACKET_DATA_LENGHT_INDEX]; iIndex++)
                            {
                                buffer[PACKET_DATA_0_INDEX + iIndex] = ParamValues[iIndex];
                            }
                            int iNumberOfBytesToTrasnmit = PACKET_DATA_0_INDEX + buffer[PACKET_DATA_LENGHT_INDEX];


                            //				AddIntervalIfSet();

                            fletcher16(checksum, buffer, iNumberOfBytesToTrasnmit);

                            buffer[iNumberOfBytesToTrasnmit] = checksum[0];
                            iNumberOfBytesToTrasnmit++;
                            buffer[iNumberOfBytesToTrasnmit] = checksum[1];
                            iNumberOfBytesToTrasnmit++;
                            buffer[iNumberOfBytesToTrasnmit] = 0x03;
                            iNumberOfBytesToTrasnmit++;
                            PacketResponseReceived(this, new PacketReceivedEventArgs(buffer));
                        }
                        break;
                }
            }
        }

        public void InitBootLoaderMode(byte BootLoaderBit)
        {
        }


        public void GetBlock(int bParameterNumber, float fBlock)
        {
            int iBytesToTransmit = 0;
            byte[] buffer = new byte[11];
            byte[] replybuffer = new byte[138];

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
            buffer[5] = (byte)bParameterNumber;  // cmd
            buffer[6] = (byte)1;
            buffer[7] = (byte)fBlock;



            byte[] checksum = new byte[2];
            iBytesToTransmit = 8;
            fletcher16(checksum, buffer, iBytesToTransmit);
            buffer[iBytesToTransmit] = checksum[0]; // chksum 1
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = checksum[1]; // chksum 2
            iBytesToTransmit++;
            buffer[iBytesToTransmit] = 0x03; // etx
            iBytesToTransmit++;


            replybuffer[0] = buffer[0];
            replybuffer[1] = buffer[1];
            replybuffer[2] = buffer[2];
            replybuffer[3] = buffer[3];
            replybuffer[4] = buffer[4];
            replybuffer[5] = buffer[5];
            replybuffer[6] = 128;
            System.Diagnostics.Debug.WriteLine("buffer[7] =  " + buffer[7].ToString());
            int iReplyIndex = 7;
            for (int iIndex = 0; iIndex < 64; iIndex++)
            {
                int BlockIndex = (int)buffer[7] * (int)64;
                replybuffer[iReplyIndex] = (byte)(ScopeBuffer[BlockIndex + iIndex] >> 8);
                replybuffer[iReplyIndex + 1] = (byte)ScopeBuffer[BlockIndex + iIndex];
                iReplyIndex += 2;
            }

            checksum = new byte[2];
            iBytesToTransmit = 135;
            fletcher16(checksum, replybuffer, iBytesToTransmit);
            replybuffer[iBytesToTransmit] = checksum[0]; // chksum 1
            iBytesToTransmit++;
            replybuffer[iBytesToTransmit] = checksum[1]; // chksum 2
            iBytesToTransmit++;
            replybuffer[iBytesToTransmit] = 0x03; // etx
            iBytesToTransmit++;

            PacketResponseReceived(this, new PacketReceivedEventArgs(replybuffer));
        }



        public void SetParameter(SetParameterEventArgs e)
        {

            int ParameterValue = 0;

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                //switch (e.bParameterNumber)
                //{
                //    case 150:
                //        ParameterValue = (int)(e.fParameterValue / 0.0078125);
                //        break;
                //    case 151:
                //        ParameterValue = (int)(e.fParameterValue / 0.0078125);
                //        break;
                //    default:
                //        ParameterValue = (int)e.fParameterValue;
                //        break;

                //}
                GoiParameter parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(i => i.Address == e.bParameterNumber && i.SubsetOfAddress == false);
                ParameterValue = (int)(e.fParameterValue * parameter.Scale);
                parameter.bupdate = true; //general method to stop visual parameters from updating before value is written
                System.Diagnostics.Debug.WriteLine("Demo:SetParameter " + parameter.Address.ToString());
            }

            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                GoiParameter parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.Address == e.bParameterNumber && i.SubsetOfAddress == false);
                ParameterValue = (int)(e.fParameterValue * parameter.Scale);
                parameter.bupdate = true; //general method to stop visual parameters from updating before value is written
                System.Diagnostics.Debug.WriteLine("Demo:SetParameter " + parameter.Address.ToString());
                if(parameter.Address == 32)
                    System.Diagnostics.Debug.WriteLine("Demo:SetParameter " + parameter.Address.ToString());
            }


            //  [0x02][0x54][0x53][0x58][0x00][0x20][0x01][0xCE][chkA][chkB][etx]

            int iBytesToTransmit = 0;
            byte[] buffer = new byte[3 + 7 + 3];


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

            Parameters[e.bParameterNumber] = (Int16)ParameterValue;

            buffer[4] = 0x00;  // flags
            buffer[5] = 0x21;  // cmd
            if (e.bParameterNumber > 256)
            {
                buffer[5] = 0x24;  // cmd
                e.bParameterNumber -= 256;
            }

            buffer[6] = 3;  // Pln
            buffer[7] = (byte)e.bParameterNumber;
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

            PacketResponseReceived(this, new PacketReceivedEventArgs(buffer));
            //   System.Diagnostics.Debug.WriteLine("Calling SetParameter write ServiceUuid = {0} CharacteristicUuid = {0} ", _ServiceUuid, _CharacteristicUuid);



        }

        public void Close()
        {
            this.StopThread();

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
