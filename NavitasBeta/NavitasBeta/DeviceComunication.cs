

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

using Xamarin.Essentials;
using System.Xml;

namespace NavitasBeta
{

    public class DeviceComunication
    {
        /// <summary>
        /// Occurs when communication times out 
        /// </summary>
        public event EventHandler<EventArgs> Timeout = delegate { };
        public event EventHandler<System.EventArgs> VehicleTypeDetermined = delegate { };
        //public event EventHandler<System.EventArgs> BootloaderDetermined = delegate { };
        public event EventHandler<ScopeGetBlockResponseEventArgs> ScopeBlockResponseHandler = delegate { };
        public event EventHandler<FlashGetBlockResponseEventArgs> FlashBlockResponseHandler = delegate { };


        string strMode;

        System.Threading.CancellationTokenSource cancellationTokenSource;
        public double recievedResponseTimer;
        public volatile bool PacketReceived;
        bool pauseTimeoutTimer = false;

        Queue<SetParameterEventArgs> Q;
        enum PacketStates : int { startUp, standardCommunications, saveOrInit, bootLoader, getBlock, gotoBoot, fastDatalog, getFlashBlock };
        PacketStates PacketSchedule;
        int pageIndex = 0, packetIndex = 0, packetToSendIndex = 0;
        long pageUniqueId = 0;

        public FirmwareDownloadPage _FirmwareDownloadPage;
        public FirmwareDownloadTSXPage _FirmwareDownloadTSXPage;
        long uniqueIdToBeRemoved = 0;

        List<PageCommunicationsList> DeviceCommunicationsParameterAndPacketList;
        List<PageCommunicationsList> DeviceCommunicationsParameterAndPacketListTSX;
        public static List<PageCommunicationsList> PageCommunicationsListPointer;

        public DeviceComunication()
        {
            DeviceCommunicationsParameterAndPacketList = new List<PageCommunicationsList>();
            DeviceCommunicationsParameterAndPacketListTSX = new List<PageCommunicationsList>();
            PageCommunicationsListPointer = DeviceCommunicationsParameterAndPacketList; //initialize it to something to avoid null exceptions during early access
        }

        public class PageCommunicationsList
        {
            public PageParameterList parentPage; //for reference to Active comm var
            public List<List<GoiParameter>> parametersGroupedto64bytesAndAddressRange;
            public List<List<byte>> matchingPackets;
        };

        //These two lists are separate because presently the App opens initial TSX and TAC screens before
        //deciding with controller it is talking two
        //it would be faster to maintain only one controller type after the decision is made

        public long AddToPacketList(PageParameterList pageParameterList)
        {
            //var starttime = DateTime.Now;
            PageCommunicationsList pageCommunicationsList = new PageCommunicationsList
            {
                parentPage = pageParameterList,
                parametersGroupedto64bytesAndAddressRange = new List<List<GoiParameter>>(),
                matchingPackets = new List<List<byte>>()
            };

            //parse the list (which is all parameters on the page calling this
            //create new lists by breaking it into 64 byte chunks (limited by communication protocal)
            //also creating separate chunked lists based on address range (TAC is growing and needs further parsing when it 
            //goes past 512 PRESENTLY ANYTHING OVER 256 ie, even greater than 512 goes into 256-512 range which is wrong
            var list = pageParameterList.parameterList.Where(i => i.Address != ParametersViewModel.ONLY_IN_APP && i.Address < 256).ToList();
            if (list.Count != 0)
            {
                while (list.Count > 64)
                {
                    pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                    list.RemoveRange(list.Count - 64, 64);
                }
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
                var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
                if (lastItem.Count % 2 == 1) //odd count
                {//something doesn't like odd packet sizes so duplicate last member
                    lastItem.Add(lastItem[lastItem.Count - 1]);
                }
            }
            list = pageParameterList.parameterList.Where(i => i.Address != ParametersViewModel.ONLY_IN_APP && i.Address >= 256 && i.Address < 512).ToList();
            if (list.Count != 0)
            {
                while (list.Count > 64)
                {
                    pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                    list.RemoveRange(list.Count - 64, 64);
                }
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
                var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
                if (lastItem.Count % 2 == 1) //odd count
                {//something doesn't like odd packet sizes so duplicate last member
                    lastItem.Add(lastItem[lastItem.Count - 1]);
                }
            }

            list = pageParameterList.parameterList.Where(i => i.Address != ParametersViewModel.ONLY_IN_APP && i.Address >= 512 && i.Address < 768).ToList();
            if (list.Count != 0)
            {
                while (list.Count > 64)
                {
                    pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                    list.RemoveRange(list.Count - 64, 64);
                }
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
                var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
                if (lastItem.Count % 2 == 1) //odd count
                {//something doesn't like odd packet sizes so duplicate last member
                    lastItem.Add(lastItem[lastItem.Count - 1]);
                }
            }

            foreach (var parameterList in pageCommunicationsList.parametersGroupedto64bytesAndAddressRange)
            {
                List<byte> transmitPacket = new List<byte>() { 0x02, 0x54, 0x53, 0x58, 0x00, 0x20, 0, 0xa3, 0x63, 0x03 };

                foreach (var parameter in parameterList)
                {
                    if (parameter.Address >= 512)
                        transmitPacket[5] = 0x26;
                    else if (parameter.Address >= 256)
                        transmitPacket[5] = 0x23;
                    transmitPacket.Insert((transmitPacket.Count - 3), (byte)parameter.Address);
                }

                byte[] checksum = new byte[2];
                transmitPacket[6] = (byte)(transmitPacket.Count - 10);
                fletcher16(checksum, transmitPacket.ToArray(), transmitPacket.Count - 3);
                transmitPacket[transmitPacket.Count - 3] = checksum[0];
                transmitPacket[transmitPacket.Count - 2] = checksum[1];

                pageCommunicationsList.matchingPackets.Add(transmitPacket);
            }
            int listSize;
            if (pageParameterList.parameterListType == PageParameterList.ParameterListType.TAC && pageCommunicationsList.matchingPackets.Count > 0)
            {
                DeviceCommunicationsParameterAndPacketList.Add(pageCommunicationsList);
                listSize = DeviceCommunicationsParameterAndPacketList.Count - 1;
            }
            else if (pageParameterList.parameterListType == PageParameterList.ParameterListType.TSX && pageCommunicationsList.matchingPackets.Count > 0)
            {
                DeviceCommunicationsParameterAndPacketListTSX.Add(pageCommunicationsList);
                listSize = DeviceCommunicationsParameterAndPacketListTSX.Count - 1;
            }
            else
            {
                return 0;
            }
            //System.Diagnostics.Debug.WriteLine($"addtopacketlist timer : {(DateTime.Now - starttime).TotalMilliseconds} ms ");
            return (pageCommunicationsList.parentPage.uniqueID); //index into this list used by actual page to enable and disable comms.
        }
        public void SetUniqueIdToBeRemoved(long uniqueId)
        {
            uniqueIdToBeRemoved = uniqueId;
        }
        public long GetUniqueIdToBeRemoved()
        {
            return uniqueIdToBeRemoved;
        }

        void RemovePacketList(long uniqueId) //carefull that this index can't change (from insertion etc....
        {
            PageCommunicationsList list = PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == uniqueId));
            if (list != null)
            {
                PageCommunicationsListPointer.Remove(list);
                pageIndex = 0;
                packetIndex = 0;
            }

        }
        public bool bEnableCommunicationTransmissions = false;
        bool bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = false;

        public void Init()
        {
            bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = false;
        }
        public void WritePacket(object sender, WritePacketEventArgs e)
        {
            //       System.Diagnostics.Debug.WriteLine("WritePacket started");
            var packettosend = new List<byte>();
            byte[] checksum = new byte[2];
            int iNumberOfBlocks = e._packetfull.Length / 20;
            int iByteIndex2 = 0;
            int iByteIndex1 = 0;
            for (int iBlockIndex = 0; iBlockIndex < iNumberOfBlocks; iBlockIndex++)
            {
                packettosend = new List<byte>();
                for (iByteIndex1 = 0; iByteIndex1 < 20; iByteIndex1++)
                {
                    packettosend.Add(e._packetfull[iByteIndex2++]);

                }
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling first write");

                int iReturnbyteCount = 0;
                foreach (byte mybyte in packettosend)
                {
                    System.Diagnostics.Debug.WriteLine("byte " + iReturnbyteCount.ToString("X") + " mybyte = " + mybyte.ToString("X"));
                    iReturnbyteCount++;
                }
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    System.Diagnostics.Debug.WriteLine("First write failed");
                }


            }
            //    System.Diagnostics.Debug.WriteLine("WritePacket Middle");
            packettosend = new List<byte>();
            for (int iByteIndex3 = iByteIndex2; iByteIndex3 < e._packetfull.Length; iByteIndex3++)
            {
                packettosend.Add(e._packetfull[iByteIndex3]);
            }
            if (packettosend.Count > 0)
            {
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling Second write");

                int iReturnbyteCount = 0;
                foreach (byte mybyte in packettosend)
                {
                    System.Diagnostics.Debug.WriteLine("byte " + iReturnbyteCount.ToString("X") + " mybyte = " + mybyte.ToString("X"));
                    iReturnbyteCount++;
                }
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    System.Diagnostics.Debug.WriteLine("Second write failed");
                }


            }
            //        System.Diagnostics.Debug.WriteLine("WritePacket ended");
        }

        public void AddParamValuesToQueue(object sender, SetParameterEventArgs e)
        {
            if (App._MainFlyoutPage.UserHasWriteCredentials())
            {
                System.Diagnostics.Debug.WriteLine("Q.Enqueue(e)");
                Q.Enqueue(e);
            }
            else
            {
                //bit of a hack but things like maximum speed could be set but when the write is skipped like this
                //recalculate will not be called because no real value was changed
                //so nothing tells the display (binding) to change it back
                GoiParameter parameter;
                System.Diagnostics.Debug.WriteLine("No credentials for Q.Enqueue(e)");
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == e.bParameterNumber && (x.SubsetOfAddress == false));

                }
                else //TSX
                {
                    parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == e.bParameterNumber && (x.SubsetOfAddress == false));
                }

                parameter.couldBeDirtyBecauseKBDoneWasNotUsed = true;

                //TODO: Dec 2020 its time to refactor ViewModelLocator to automatically pick the correct view model and stop check TAC and TSX everywhere
            }
        }

        IMode _Mode;
        Task CommunicationMethodThread;
        public async Task StartCommunciationThread(IMode Mode, string strMode)
        {
            if (CommunicationMethodThread != null && !CommunicationMethodThread.IsCompleted)
            {
                System.Diagnostics.Debug.WriteLine("someone miss stopping com thread");
                cancellationTokenSource.Cancel(); //did someone miss stopping com thread
                await Task.FromResult<bool>(CommunicationMethodThread.IsCompleted);
            }

            this.strMode = strMode;
            _Mode = Mode;

            // App.BluetoothAdapter.PacketResponseReceived += this.PacketResponseReceived;
            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            cancellationTokenSource.Token.Register(() =>
            {
                Timeout(null, null);
                CloseMode();
            });
            PacketReceived = false;

            Q = new Queue<SetParameterEventArgs>();

            recievedResponseTimer = 0;

            _Mode.SetPacketResponseDelegate(this.PacketResponseReceived);

            CommunicationMethodThread = Task.Factory.StartNew(CommunicationMethod, cancellationTokenSource.Token);
        }

        public void SetFirmwareDownloadPageRef(FirmwareDownloadTSXPage FirmwareDownloadTSXPage)
        {
            _FirmwareDownloadTSXPage = FirmwareDownloadTSXPage;
        }
        public void SetFirmwareDownloadPageRef(FirmwareDownloadPage FirmwareDownloadTACPage)
        {
            _FirmwareDownloadPage = FirmwareDownloadTACPage;
        }

        public void ProgrammingDoneTSX(object sender, EventArgs e)
        {
            PacketReceived = true;
            bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = false;
        }

        bool bJustForTacSendBootmodeResponsesStraightThrough = false;
        public void ProgrammingDone(object sender, WriteEventArgs e)
        {
            PacketReceived = true;
            bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = false;
            bJustForTacSendBootmodeResponsesStraightThrough = false; ;
        }

        bool VehicleHasReplied = false;
        bool blockVehicleTypeDetermined = false;

        byte[] ReceivedPacket;
        int NumberOfBytesExpected;
        byte[] checksum;
        void PacketResponseReceived(object sender, PacketReceivedEventArgs receivedEvent)
        {
            try
            {
                //System.Diagnostics.Debug.WriteLine("PacketResponseReceived entered");
                var debug = MainThread.IsMainThread;
                recievedResponseTimer = 0; //we recieved something so reset timer for timeout

                byte[] ProcessedReceivedPacket = null;
                if (receivedEvent.ReceivedPacket == null)
                {
                    System.Diagnostics.Debug.WriteLine("ReceivedPacket null should never happen?????????????????????");
                    return;
                }
                if (ReceivedPacket == null)
                {
                    ReceivedPacket = new byte[0];
                    //System.Diagnostics.Debug.WriteLine("New ReceivedPacket started");
                }
                if (ReceivedPacket.Length > 200)
                    System.Diagnostics.Debug.WriteLine("ReceivedPacket.Length > 200should never happen?????????????????????");
                ReceivedPacket = ReceivedPacket.Concat(receivedEvent.ReceivedPacket).ToArray();
                //System.Diagnostics.Debug.WriteLine("Building ReceivedPacket, added " + receivedEvent.ReceivedPacket.Length.ToString() + " for " + ReceivedPacket.Length.ToString() + " NumberOfBytesExpected " + NumberOfBytesExpected);

                //ReceivedPacket == null means the start of a new packet
                if (bJustForTacSendBootmodeResponsesStraightThrough)
                {
                    ProcessedReceivedPacket = ReceivedPacket;
                    //System.Diagnostics.Debug.WriteLine("Hope it is a boot load packet");
                    ReceivedPacket = null;
                }
                else if (receivedEvent.ReceivedPacket[0] == (byte)'A' && receivedEvent.ReceivedPacket.Length == 1)
                {
                    ProcessedReceivedPacket = new byte[1] { (byte)'A' };
                    System.Diagnostics.Debug.WriteLine("Detected Boot Mode for TAC");
                    ReceivedPacket = null;
                }
                else if (ReceivedPacket != null)
                {
                    if (ReceivedPacket[0] != 0x02)
                    {
                        var x = Array.IndexOf(new byte[] { 0x02, (byte)'T' }, ReceivedPacket);
                        if (x != -1)
                        {
                            ReceivedPacket = (byte[])ReceivedPacket.Skip(x).ToArray();
                            System.Diagnostics.Debug.WriteLine("ReceivedPacket, Resynced");
                            return; //might be able to resync
                        }

                        ProcessedReceivedPacket = new byte[1] { 0xaa };
                        System.Diagnostics.Debug.WriteLine("ReceivedPacket, ReceivedPacket[0] != 0x02");
                        ReceivedPacket = null;
                    }
                    else if (ReceivedPacket.Length >= 7)
                    {
                        NumberOfBytesExpected = ReceivedPacket[6];
                        // there are 10 bytes overhead
                        NumberOfBytesExpected += 10;
                        if (ReceivedPacket.Length == NumberOfBytesExpected)
                        {
                            checksum = new byte[2];
                            fletcher16(checksum, ReceivedPacket, ReceivedPacket.Length - 3);
                            if ((ReceivedPacket[ReceivedPacket.Length - 3] == checksum[0]) && (ReceivedPacket[ReceivedPacket.Length - 2] == checksum[1]))
                            {
                                ProcessedReceivedPacket = ReceivedPacket;
                                //System.Diagnostics.Debug.WriteLine("ReceivedPacket, Successfull"); //don't fill the screen with this
                                ReceivedPacket = null;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("ReceivedPacket, Checksum Failed");
                                ReceivedPacket = null;
                                return;
                            }
                        }
                        else if (ReceivedPacket.Length > NumberOfBytesExpected)
                        {
                            System.Diagnostics.Debug.WriteLine("ReceivedPacket, ReceivedPacket.Length " + ReceivedPacket.Length + " > NumberOfBytesExpected " + NumberOfBytesExpected);
                            ProcessedReceivedPacket = new byte[1] { 0xaa };
                            ReceivedPacket = null;
                        }
                        else
                        {
                            //System.Diagnostics.Debug.WriteLine("PacketResponseReceived exiting 2");
                            return; //just building a multi-packet (ble 20 byte packet)
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("ReceivedPacket, ReceivedPacket.Length < 7");
                        return;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("RG: Dec 2019, Not sure why but ReceivedPacket == null");
                    return;
                }

                if (ProcessedReceivedPacket == null)
                {
                    System.Diagnostics.Debug.WriteLine("Processed ReceivedPacket null should also never happen?????????????????????");
                    return;
                }

                if (PacketSchedule == PacketStates.bootLoader)
                {
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        System.Diagnostics.Debug.WriteLine("ResponseFromBootLoader");
                        _FirmwareDownloadPage.ResponseFromBootLoader(ProcessedReceivedPacket);
                    }
                    if (ControllerTypeLocator.ControllerType == "TSX")
                    {
                        //       System.Diagnostics.Debug.WriteLine("ResponseFromBootLoader");
                        _FirmwareDownloadTSXPage.ResponseFromBootLoader(ProcessedReceivedPacket);
                    }
                }
                else if (PacketSchedule == PacketStates.gotoBoot)
                {
                    System.Diagnostics.Debug.WriteLine("case 7");
                    if (ControllerTypeLocator.ControllerType == "TSX")
                    {
                        System.Diagnostics.Debug.WriteLine("case 7");
                        if (ProcessedReceivedPacket[9] == 6) // boot loader version 
                        {
                            bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = true;
                        }
                        PageCommunicationsListPointer = DeviceCommunicationsParameterAndPacketListTSX;
                    }
                    else if (ProcessedReceivedPacket[0] == (byte)'A') //TAC TI internal bootloader autobaud echo responses (it found this to get here but why not do it again)
                    {
                        bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = true;
                        bJustForTacSendBootmodeResponsesStraightThrough = true;
                        ControllerTypeLocator.ControllerType = "TAC"; //was not determined by initial packet because the initial packet will screw up this boot loader
                        PageCommunicationsListPointer = DeviceCommunicationsParameterAndPacketList;
                    }

                    System.Diagnostics.Debug.WriteLine("case 7 recievedResponseTimer = " + recievedResponseTimer.ToString());
                    PacketReceived = true;
                }
                else
                {
                    if (ProcessedReceivedPacket[0] != 0xaa && ProcessedReceivedPacket.Length > 4)
                    {
                        if ((ProcessedReceivedPacket[4] & 0x80) != 0x80)  // this is the error bit that happens if the ac  firmware has a checksum error.
                        {

                            switch (PacketSchedule)
                            {
                                case PacketStates.startUp:
                                    //#if CONSOLE_WRITE
                                    System.Diagnostics.Debug.WriteLine("case 0");
                                    //#endif 
                                    if (ProcessedReceivedPacket[1] == 'T' && ProcessedReceivedPacket[2] == 'A' && ProcessedReceivedPacket[3] == 'C')
                                    {
                                        //            System.Diagnostics.Debug.WriteLine("TAC Seen");
                                        ControllerTypeLocator.ControllerType = "TAC";
                                        PageCommunicationsListPointer = DeviceCommunicationsParameterAndPacketList;
                                        VehicleHasReplied = true;
                                    }
                                    if (ProcessedReceivedPacket[1] == 'T' && ProcessedReceivedPacket[2] == 'S' && ProcessedReceivedPacket[3] == 'X')
                                    {
                                        //                   System.Diagnostics.Debug.WriteLine("TSX Seen");

                                        ControllerTypeLocator.ControllerType = "TSX";
                                        PageCommunicationsListPointer = DeviceCommunicationsParameterAndPacketListTSX;
                                        VehicleHasReplied = true;
                                    }

                                    //if (bCommunicationsStartedWithControllerAlreadyInBootLoaderMode == false)
                                    //{
                                    if ((VehicleHasReplied == true) && (blockVehicleTypeDetermined == false))
                                    {
                                        VehicleTypeDetermined(null, null);
                                        blockVehicleTypeDetermined = true;
                                    }
                                    //}

                                    PacketReceived = true;
                                    break;
                                case PacketStates.standardCommunications:
                                    var receivingPage = PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == pageUniqueId));
                                    //System.Diagnostics.Debug.WriteLine("Received (page, packet) " + receivingPage.parentPage.ParentTitle + "," + packetToSendIndex.ToString() + " Rx Length = " + ProcessedReceivedPacket.Length.ToString() + " Received Count = " + ReceivedPacketCount.ToString());
                                    if (App.AppConfigurationLevel == "DEALER" || App.AppConfigurationLevel == "ENG" || App.AppConfigurationLevel == "ADVANCED_USER" || App.AppConfigurationLevel == "ENG_USER" || App.AppConfigurationLevel == "DEV")
                                    {
                                        if (receivingPage.parentPage.pagePointer.OxyPlotPage != null)
                                        {
                                            receivingPage.parentPage.pagePointer.AddPointsToGraphForPacket();
                                        }
                                    }
                                    //var starttime = DateTime.Now;
                                    pauseTimeoutTimer = true;
                                    UpdateParameterViewModelWithPacketResponse(pageUniqueId, packetToSendIndex, ProcessedReceivedPacket);
                                    //System.Diagnostics.Debug.WriteLine($"UpdateParameterViewModelWithPacketResponse timer : {(DateTime.Now - starttime).TotalMilliseconds} ms ");
                                    //if ((DateTime.Now - starttime).TotalMilliseconds > 3000)
                                    //    System.Diagnostics.Debug.WriteLine("Why did this take so long");
                                    ReceivedPacketCount++;
                                    Retries = 3;
                                    PacketReceived = true;
                                    pauseTimeoutTimer = false;
                                break;
                                //#if PACKET_2
                                case PacketStates.saveOrInit:

                                    System.Diagnostics.Debug.WriteLine("case 4");
                                    PacketReceived = true;
                                    break;
                                case PacketStates.getBlock:

                                    // write the data to the scope. 
                                    System.Diagnostics.Debug.WriteLine("case 6");
                                    List<byte> bytelist = new List<byte>(ProcessedReceivedPacket);
                                    //System.Diagnostics.Debug.WriteLine("ResponseFromScopeBlock 1 " + DateTime.Now.Millisecond.ToString());
                                    ScopeBlockResponseHandler(this, new ScopeGetBlockResponseEventArgs(bytelist.GetRange(7, ProcessedReceivedPacket[6])));
                                    //System.Diagnostics.Debug.WriteLine("ResponseFromScopeBlock 2 " + DateTime.Now.Millisecond.ToString());
                                    PacketReceived = true;
                                    break;

                                case PacketStates.getFlashBlock:

                                    // write the flash data to the screen. 
                                    System.Diagnostics.Debug.WriteLine("case 7");
                                    List<byte> flashbytelist = new List<byte>(ProcessedReceivedPacket);
                                    FlashBlockResponseHandler(this, new FlashGetBlockResponseEventArgs(flashbytelist.GetRange(7, ProcessedReceivedPacket[6])));
                                    PacketReceived = true;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("PacketResponseReceived exception " + e.Message);
            }
            //System.Diagnostics.Debug.WriteLine("PacketResponseReceived exiting");
        } //end of PacketResponseReceived()

        int timeoutmax = 0;
        uint ucounter = 0;
        uint Retries = 3;
        uint TransmittedPacketCount = 0;
        uint ReceivedPacketCount = 0;
        async void CommunicationMethod()
        {
            System.Diagnostics.Debug.WriteLine("CommunicationMethod started");
            ReceivedPacket = null; //clear any old stuff left in here from previous timeouts
            VehicleHasReplied = false;
            blockVehicleTypeDetermined = false;


            //       System.Diagnostics.Debug.WriteLine("CommunicationMethod");
            byte[] checksum = new byte[2];

            //      System.Diagnostics.Debug.WriteLine("Write Get Controller type packet");
            PacketSchedule = PacketStates.gotoBoot;
            PacketReceived = false;
            _Mode.WritePacket(new byte[] { (byte)'A' }); //only the TAC bootloader would respond to a single byte containing'A'
            recievedResponseTimer = 0;
            try
            {   //initial startup response check of TAC bootmode check TSX will timeout
                while (!PacketReceived)
                {
                    int nowtime = DateTime.Now.Second;
                    if (recievedResponseTimer > 1)  // if 1 second goes by then there was no response to the one byte bootmode check for TAC
                    {
                        //so try the regular communication protocal that works for both TAC and TSX
                        PacketReceived = false;
                        PacketSchedule = PacketStates.startUp;

                        byte[] packetGetConrollerType = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x00, 0x20, 2, 1, 40, 0xa3, 0x63, 0x03 };
                        fletcher16(checksum, packetGetConrollerType, packetGetConrollerType.Length - 3);
                        packetGetConrollerType[packetGetConrollerType.Length - 3] = checksum[0];
                        packetGetConrollerType[packetGetConrollerType.Length - 2] = checksum[1];

                        _Mode.WritePacket(packetGetConrollerType);
                        recievedResponseTimer = 0;
                        while (!PacketReceived)
                        {
                            nowtime = DateTime.Now.Second;
                            if (recievedResponseTimer > 4) // if 4 seconds go buy without a respons set the flag to try again.
                            {
                                System.Diagnostics.Debug.WriteLine("CommunicationMethod Initial not in boot Timeout = {0} ", recievedResponseTimer);
                                cancellationTokenSource.Cancel(); //notify the thread about stopping
                            }
                            await Task.Delay(50); //Just being nice + wait for save parameters to finish
                            recievedResponseTimer += 0.05;
                        }
                    }
                    await Task.Delay(50); //Just being nice + wait for save parameters to finish
                    recievedResponseTimer += 0.05;
                }

                if (ControllerTypeLocator.ControllerType == "TSX") //set elsewhere from above response
                {
                    byte[] packetinitBoot = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x01, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
                    checksum = new byte[2];
                    fletcher16(checksum, packetinitBoot, packetinitBoot.Length - 3);
                    packetinitBoot[packetinitBoot.Length - 3] = checksum[0];
                    packetinitBoot[packetinitBoot.Length - 2] = checksum[1];
                    PacketSchedule = PacketStates.gotoBoot;
                    recievedResponseTimer = 0;
                    PacketReceived = false;
                    _Mode.WritePacket(packetinitBoot);
                    while (!PacketReceived)
                    {
                        int nowtime = DateTime.Now.Second;
                        if (recievedResponseTimer > 4)  // if 4 seconds go buy without a respons set the flag to try again.
                        {
                            System.Diagnostics.Debug.WriteLine("CommunicationMethod Initial TSX Timeout = {0} ", recievedResponseTimer);
                            cancellationTokenSource.Cancel();
                        }
                        await Task.Delay(50); //Just being nice + wait for save parameters to finish
                        recievedResponseTimer += 0.05;
                    }
                    if (bCommunicationsStartedWithControllerAlreadyInBootLoaderMode)
                    {
                        App._MainFlyoutPage.BootloaderDetermined(true);
                    }
                    System.Diagnostics.Debug.WriteLine("Bootloader only check done");

                }
                if (ControllerTypeLocator.ControllerType == "TAC") //set elsewhere from above response
                {
                    if (bCommunicationsStartedWithControllerAlreadyInBootLoaderMode)
                    {
                        App._MainFlyoutPage.BootloaderDetermined(true);
                    }
                }

                bool nothingTransmitted = true;
                recievedResponseTimer = 0;
                System.Diagnostics.Debug.WriteLine("before while(true) recievedResponseTimer = " + recievedResponseTimer.ToString());
                int timeToDelayingMoreTransmissions = 0;
                pageIndex = 0;
                packetIndex = 0;
                pageUniqueId = PageCommunicationsListPointer[pageIndex].parentPage.uniqueID; //hopefully there is one page before we get here
                PageCommunicationsList presentPageCommunicaitonsList = (PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == pageUniqueId)));

                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var debug = MainThread.IsMainThread;
                    if (bCommunicationsStartedWithControllerAlreadyInBootLoaderMode == false && bEnableCommunicationTransmissions)
                    {//nobody is in boot mode
                        //for clarity, PacketReceived == true means time to send another one
                        //but if nothing is Active nothing was sent and we can spin in this while(true) from above waiting for active pages
                        if (PacketReceived == true || nothingTransmitted)
                        {
                            PacketReceived = false;
                            nothingTransmitted = false;
                            if (IsQueueNotEmpty())
                            { //Single Parameter must havew been written
                                System.Diagnostics.Debug.WriteLine("Packet from q");
                                SetParameterEventArgs e = GetParamValuesFromQueue();
                                if (ControllerTypeLocator.ControllerType == "TAC")
                                {
                                    if (e.bParameterNumber == 49)  // init parameters  
                                    {
                                        timeoutmax = 9;
                                        PacketSchedule = PacketStates.saveOrInit;
                                        _Mode.SetParameter(e);
                                    }
                                    else if (e.bParameterNumber == 50)  // save parameters  
                                    {
                                        timeoutmax = 9;
                                        PacketSchedule = PacketStates.saveOrInit;
                                        _Mode.SetParameter(e);
                                        timeToDelayingMoreTransmissions = 5; //saving takes around 4 seconds 
                                    }
                                    else if (e.bParameterNumber == 0x22)
                                    {
                                        // Get Block
                                        PacketSchedule = PacketStates.getBlock;
                                        System.Diagnostics.Debug.WriteLine("_Mode.GetBlock(e.bParameterNumber, e.fParameterValue)");
                                        if (strMode == "Demo")
                                        {
                                            _Mode.GetBlock(e.bParameterNumber, e.fParameterValue);
                                        }
                                        if (strMode == "Physical")
                                        {
                                            _Mode.GetBlock(e.bParameterNumber, e.fParameterValue);
                                        }
                                    }
                                    else if (e.bParameterNumber == 0x25) // Read Flash Sector
                                    {
                                        // Get Block
                                        PacketSchedule = PacketStates.getFlashBlock;
                                        System.Diagnostics.Debug.WriteLine("_Mode.GetBlock(e.bParameterNumber, e.fParameterValue)");
                                        if (strMode == "Demo")
                                        {
                                            _Mode.GetBlock(e.bParameterNumber, e.fParameterValue);
                                        }
                                        if (strMode == "Physical")
                                        {
                                            _Mode.GetBlock(e.bParameterNumber, e.fParameterValue);
                                        }
                                    }
                                    else if (e.bParameterNumber == 109)
                                    {
                                        _Mode.SetParameter(e);
                                        bJustForTacSendBootmodeResponsesStraightThrough = true;
                                        PacketSchedule = PacketStates.bootLoader;
                                        timeoutmax = 120;
                                    }
                                    else
                                    {
                                        //RG: June 2020, just realized simple write parameter just skips the response
                                        //because PacketStates.saveOrInit does nothing,...not a problem yet
                                        //but might be when we need block writes
                                        PacketSchedule = PacketStates.saveOrInit;
                                        _Mode.SetParameter(e);
                                    }
                                }

                                if (ControllerTypeLocator.ControllerType == "TSX")
                                {
                                    if (e.bParameterNumber == 199)  // save  parameters  
                                    {
                                        timeoutmax = 9;
                                        PacketSchedule = PacketStates.saveOrInit;
                                        _Mode.SetParameter(e);
                                        timeToDelayingMoreTransmissions = 5;
                                    }
                                    else if (e.bParameterNumber == 0xff && e.strParameter == "Bootload")
                                    {
                                        // INIT BOOT.
                                        if (strMode == "Demo")
                                        {
                                            _Mode.InitBootLoaderMode((byte)e.fParameterValue);
                                        }
                                        if (strMode == "Physical")
                                        {
                                            _Mode.InitBootLoaderMode((byte)e.fParameterValue);
                                        }
                                    }
                                    else if (e.bParameterNumber == 0xff && e.strParameter == "FastDatalog")
                                    {
                                        System.Diagnostics.Debug.WriteLine("FastDatalog");
                                    }
                                    else
                                    {
                                        PacketSchedule = PacketStates.saveOrInit;
                                        _Mode.SetParameter(e);
                                    }
                                    if (e.bParameterNumber == 0xff && e.strParameter == "Bootload")
                                    {
                                        PacketSchedule = PacketStates.bootLoader;
                                        timeoutmax = 120;
                                    }
                                }
                            }
                            else
                            {
                                if (Retries != 0)
                                    Retries--;
                                if (ControllerTypeLocator.ControllerType == "TAC")
                                    timeoutmax = 3;
                                if (ControllerTypeLocator.ControllerType == "TSX")
                                    timeoutmax = 3;

                                if ((PacketSchedule == PacketStates.startUp) || (PacketSchedule == PacketStates.standardCommunications) || (PacketSchedule == PacketStates.saveOrInit) || (PacketSchedule == PacketStates.bootLoader) || (PacketSchedule == PacketStates.getBlock) || (PacketSchedule == PacketStates.gotoBoot) || (PacketSchedule == PacketStates.fastDatalog) || (PacketSchedule == PacketStates.getFlashBlock))
                                {
                                    if (PageCommunicationsListPointer.Count != 0) //no pages have not filled it in yet
                                    {
                                        try
                                        {
                                            do //get packet to be sent
                                            {
                                                if (pageIndex >= PageCommunicationsListPointer.Count)
                                                { //only happens when a page specific packet was last and removed by the page
                                                    pageIndex = 0;
                                                    packetIndex = 0;
                                                }

                                                packetToSendIndex = packetIndex;
                                                presentPageCommunicaitonsList = PageCommunicationsListPointer[pageIndex];
                                                pageUniqueId = presentPageCommunicaitonsList.parentPage.uniqueID;
                                                presentPageCommunicaitonsList.parentPage.PacketsEvaluatedForSending++; //track sent and SOFTWAREREVISION skipped packets

                                                if (uniqueIdToBeRemoved == pageUniqueId)
                                                    presentPageCommunicaitonsList.parentPage.Active = true; //Just a catch all incase someone made it inactive

                                                if (!presentPageCommunicaitonsList.parentPage.Active) //it's possible only some were sent and then it was made inactive
                                                    presentPageCommunicaitonsList.parentPage.PacketsEvaluatedForSending = 0; //so start over

                                                //get next packet from list
                                                if (++packetIndex >= presentPageCommunicaitonsList.matchingPackets.Count)
                                                {
                                                    packetIndex = 0;
                                                    if (++pageIndex >= PageCommunicationsListPointer.Count)
                                                    {
                                                        pageIndex = 0;
                                                    }
                                                }
                                            } //only spin through once incase there are no packets to send
                                            while (!(pageIndex == 0 && packetToSendIndex == 0) && ((!presentPageCommunicaitonsList.parentPage.Active)
                                                || (ControllerTypeLocator.ControllerType == "TAC") && ((App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue <= 1.280 && presentPageCommunicaitonsList.matchingPackets[packetToSendIndex][5] == 0x23)
                                                || (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue <= 5.000 && presentPageCommunicaitonsList.matchingPackets[packetToSendIndex][5] == 0x26)))
                                                || ((ControllerTypeLocator.ControllerType == "TSX") && (App.ViewModelLocator.GetParameterTSX("PAREMBEDDEDSOFTWAREVER").parameterValue < 9.5 && presentPageCommunicaitonsList.matchingPackets[packetIndex][5] == 0x23)
                                            ));                                        
                                        }

                                        catch (Exception ex)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Loop through pointers exception" + ex.Message);
                                        }

                                        try
                                        {
                                            if (presentPageCommunicaitonsList.parentPage.Active)
                                            {
                                                PacketSchedule = PacketStates.standardCommunications;
                                                //System.Diagnostics.Debug.WriteLine(DateTime.Now.Millisecond + ": About to transmit (page, packet) " + presentPageCommunicaitonsList.parentPage.ParentTitle + "," + packetToSendIndex.ToString() + " length = " + presentPageCommunicaitonsList.matchingPackets[packetToSendIndex].Count.ToString() + " Transmitted Count = " + TransmittedPacketCount.ToString());
                                                _Mode.WritePacket(presentPageCommunicaitonsList.matchingPackets[packetToSendIndex].ToArray());
                                                TransmittedPacketCount++;
                                                Task.Delay(50).Wait(); //limit transmission rate, in case we actually are able to send and receive faster?
                                            }
                                            else
                                                nothingTransmitted = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            System.Diagnostics.Debug.WriteLine("WritePacket 1 Exception exited" + ex.Message);
                                        }
                                    }
                                    else
                                        nothingTransmitted = true;
                                }
                            }
                            recievedResponseTimer = 0;
                        }
                        int nowtime = DateTime.Now.Second;
                        if (recievedResponseTimer > timeoutmax)
                        {
                            if (ControllerTypeLocator.ControllerType == "TSX")
                            {
                                App.ViewModelLocator.GetParameterTSX("COMMERROR").parameterValue = 1.0f;
                            }
                            if (ControllerTypeLocator.ControllerType == "TAC")
                            {
                                App.ViewModelLocator.GetParameter("COMMERROR").parameterValue = 1.0f;
                            }

                            if (strMode != "Demo")
                            {
                                System.Diagnostics.Debug.WriteLine("CommunicationMethod running, timeout=" + recievedResponseTimer.ToString() + ", timeoutmax = " + timeoutmax.ToString() + ", time now = " + nowtime.ToString() + ", packet sent time= " + recievedResponseTimer.ToString());
                                cancellationTokenSource.Cancel();
                                nothingTransmitted = true;
                            }
                        }
                        else
                        {

                            if (strMode == "Demo")
                            {
                                if ((ucounter++ % 100) == 0)
                                {
                                    if (ControllerTypeLocator.ControllerType == "TSX")
                                    {
                                        App.ViewModelLocator.GetParameterTSX("COMMERROR").parameterValue = 1.0f;
                                    }
                                    if (ControllerTypeLocator.ControllerType == "TAC")
                                    {
                                        App.ViewModelLocator.GetParameter("COMMERROR").parameterValue = 1.0f;
                                    }
                                }
                            }
                            else
                            {

                                if (ControllerTypeLocator.ControllerType == "TSX")
                                {
                                    App.ViewModelLocator.GetParameterTSX("COMMERROR").parameterValue = 0.1f;
                                }
                                if (ControllerTypeLocator.ControllerType == "TAC")
                                {
                                    App.ViewModelLocator.GetParameter("COMMERROR").parameterValue = 0.1f;

                                }
                            }
                        }
                        if (timeToDelayingMoreTransmissions != 0)
                        {
                            MessagingCenter.Send<DeviceComunication>(this, "ShowActivity");
                        }

                        await Task.Delay(50 + timeToDelayingMoreTransmissions * 1000); //Just being nice + wait for save parameters to finish
                        if (!pauseTimeoutTimer)
                        {
                            recievedResponseTimer += 0.05;
                        }
                        if (timeToDelayingMoreTransmissions != 0)
                        {
                            MessagingCenter.Send<DeviceComunication>(this, "StopActivity");
                            if (ControllerTypeLocator.ControllerType == "TSX")
                                MessagingCenter.Send<DeviceComunication>(this, "SettingsSaved");
                        }
                            
                        timeToDelayingMoreTransmissions = 0;
                    }
                    else if (bEnableCommunicationTransmissions)//somebody is in boot mode
                    {
                        PacketSchedule = PacketStates.bootLoader;
                        timeoutmax = 120;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("CommunicationMethod Exception exited" + e.Message);
            }
        } // End of CommunicationMethod()

        void CloseMode()
        {
            _Mode.Close();
            _Mode.RemovePacketResponseDelegate(this.PacketResponseReceived);

        }
        SetParameterEventArgs GetParamValuesFromQueue()
        {
            return Q.Dequeue();
        }

        public void ClearQueue(object sender, EventArgs e)
        {
            Q.Clear();
        }

        public bool IsQueueNotEmpty()
        {
            return Q.Count > 0;
        }

        public Task<bool> IsQueueNotEmptyAsync()
        {
            return Task.FromResult<bool>(Q.Count > 0);
        }

        public Task<int> ItemsLeftInQueueAsync()
        {
            return Task.FromResult<int>(Q.Count);
        }
        //public void InitializeGaugePageReference(Gauge Gauge, GaugeTSX GaugeTSX)
        //{
        //    _Gauge = Gauge;
        //    _GaugeTSX = GaugeTSX;
        //}

        public void StopCommunicationThread()
        {
            //         System.Diagnostics.Debug.WriteLine("StopCommunicationThread");
            if (cancellationTokenSource != null)
            {
                //          System.Diagnostics.Debug.WriteLine("Calling Cancel token");
                cancellationTokenSource.Cancel();
            }
            //else if(CommunicationMethodThread.IsCompleted) //cancel would not be seen
            //{
            //    ClosePhysicalMode();
            //}

        }

        public void SetScopeBlockResponseHandler(EventHandler<ScopeGetBlockResponseEventArgs> handler)
        {
            ScopeBlockResponseHandler = handler;
        }
        
        public void SetFlashBlockResponseHandler(EventHandler<FlashGetBlockResponseEventArgs> handler)
        {
            FlashBlockResponseHandler = handler;
        }

        private static void fletcher16(byte[] checksum, byte[] data, int len)
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

        void UpdateParameterViewModelWithPacketResponse(long uniqueId, int sentPacketIndex, byte[] ProcessedReceivedPacket)
        {
            try
            {
                var i = 0;
                var packetParentPointer = PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == uniqueId));
                if (ProcessedReceivedPacket.Length == (packetParentPointer.matchingPackets[sentPacketIndex].Count * 2) - 10)
                {
                    foreach (var parameter in packetParentPointer.parametersGroupedto64bytesAndAddressRange[sentPacketIndex])
                    {
                        var debug = MainThread.IsMainThread;
                        //if (parameter.PropertyName == "Options") 
                        //    System.Diagnostics.Debug.WriteLine("Options reading " + parameter.parameterValue.ToString());
                        parameter.rawValue = (System.UInt16)((System.UInt16)ProcessedReceivedPacket[7 + i] << 8 | (System.UInt16)ProcessedReceivedPacket[8 + i]);
                        i += 2;

                    }
                    if (uniqueIdToBeRemoved == uniqueId && packetParentPointer.parentPage.PacketsEvaluatedForSending >= packetParentPointer.matchingPackets.Count) //Active pages that were either skipped or sent
                    {
                            RemovePacketList(uniqueId);
                            uniqueIdToBeRemoved = 0; //asynchronous indicator to other threads that packet has been removed
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateParameterViewModelWithPacketResponse Exception exited" + ex.Message);
            }

        }


        string GetVehicleStateFromInt(System.Int16 state)
        {
            string ret = null;
            switch (state)
            {
                case 0:
                    ret = "Reset";
                    break;
                case 1:
                    ret = "Charger Or Tow Connection Check";
                    break;
                case 2:
                    ret = "Throttle Zero Startup Check";
                    break;
                case 3:
                    ret = "Throttle Saturation Startup Check";
                    break;
                case 4:
                    ret = "Reverse Buzzer Check";
                    break;
                case 5:
                    ret = "DC Bus Check";
                    break;
                case 6:
                    ret = "Start up Parking Brake Check";
                    break;
                case 7:
                    ret = "Startup";
                    break;
                case 8:
                    ret = "Neutral";
                    break;
                case 9:
                    ret = "Neutral Transition";
                    break;
                case 10:
                    ret = "Forward";
                    break;
                case 11:
                    ret = "Reverse";
                    break;
                case 12:
                    ret = "DropBrake";
                    break;
                case 13:
                    ret = "Error";
                    break;


            }
            return ret;
        }

        public void Write20Bytes(object sender, WriteBlockEventArgs e)
        {
            if (App.BluetoothAdapter.Write(e._list.ToArray()) == false)
            {
                System.Diagnostics.Debug.WriteLine("Write20Bytes failed");
            }

        }

        async public void WriteBlock(object sender, WriteBlockEventArgs e)
        {
            // e._list.Count
            var packettosend = new List<byte>();
            byte[] checksum = new byte[2];
            int iNumberOfBlocks = e._list.Count / 20;
            int iByteIndex2 = 0;
            int iByteIndex1 = 0;
            for (int iBlockIndex = 0; iBlockIndex < iNumberOfBlocks; iBlockIndex++)
            {
                packettosend = new List<byte>();
                for (iByteIndex1 = 0; iByteIndex1 < 20; iByteIndex1++)
                {
                    packettosend.Add(e._list[iByteIndex2++]);

                }
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling first write");
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    //                 System.Diagnostics.Debug.WriteLine("First write failed");
                }

                await Task.Delay(20);

            }
            packettosend = new List<byte>();
            for (int iByteIndex3 = iByteIndex2; iByteIndex3 < e._list.Count; iByteIndex3++)
            {
                packettosend.Add(e._list[iByteIndex3]);
            }
            if (packettosend.Count > 0)
            {
#if CONSOLE_WRITE
                    System.Diagnostics.Debug.WriteLine("Calling Second write");
#endif
                if (App.BluetoothAdapter.Write(packettosend.ToArray()) == false)
                {
                    //            System.Diagnostics.Debug.WriteLine("Second write failed");
                }

                await Task.Delay(20);
            }


        }

        void Write2Bytes(object sender, Write2ByteEventArgs e)
        {
            byte[] buffer = new byte[2];
            buffer = e.bSendBytes;
            System.Diagnostics.Debug.WriteLine(" Write2Bytes(object sender, Write2ByteEventArgs e)");
            if (App.BluetoothAdapter.Write(buffer) == false)
            {



            }
        }

        public void Write(object sender, WriteEventArgs e)
        {
            byte[] buffer = new byte[1];
            buffer[0] = e.bSendByte;
            //     System.Diagnostics.Debug.WriteLine("e.bSendByte = " + e.bSendByte.ToString());
            if (App.BluetoothAdapter.Write(buffer) == false)
            {



            }
            //        Task.Delay(20).Wait();  // This statement is very nescessary.  
            // Without this wait the firmware downloader doesn't work
            // when the app is installed via and apk file.
            // Keep this in mind when testing on other platforms
            // A first step to debugging may be to increase this wait.


        }
    }
}

