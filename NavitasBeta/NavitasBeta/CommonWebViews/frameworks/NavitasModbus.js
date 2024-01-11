// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.Threading.Tasks;
// using Xamarin.Forms;
// using System.Linq;

// using Xamarin.Essentials;

// namespace NavitasBeta
// {
let DEBUG_NAVITASMODBUS = false;
class NavitasModbus extends EventTarget //this might be temporaty but EventTarget is used to access the delegate stucture used in an event
{
    //why doesn't this work if in constructor? 
    PacketStates = { startUp: 0, standardCommunications: 1, saveOrInit: 2, bootLoader: 3, getBlock: 4, gotoBoot: 5, fastDatalog: 6, getFlashBlock: 7 };

    constructor()// public DeviceComunication()
    {
        super();
        const _this = this;
        this.ModelReference = {};
        this.ModelType = ""; //gets set to TAC or TSX after communications is started and gets a reply
        this.DeviceTypeDeterminedDelegate = new CustomEvent("DeviceTypeDetermined", // public event EventHandler<System.EventArgs> DeviceTypeDetermined = delegate { };
            {
                detail:
                {
                    ModelType: "", //this.DeviceHasReplied,
                    time: new Date(),
                },
                bubbles: true,
                cancelable: true
            });
        // this.ScopeGetBlockRespondedDelegate = new CustomEvent("ScopeGetBlockResponded",
        //     {
        //         detail:
        //             {
        //                 time: new Date(),
        //             },
        //         bubbles: true,
        //         cancelable: true
        //     });
        this.ReceiveDelegate = new CustomEvent("ReceiveResponded",
            {
                detail:
                    {
                        time: new Date(),
                    },
                bubbles: true,
                cancelable: true
            });
        this.DeviceHasReplied = true;
        this.recievedResponseTimer = 0;
        this.WaitingForReceiveResponse = false;
        this.EnableReceiveEvent = false;

        this.PacketSchedule = "";
        this.ScopeBlockSchedule = Number.NaN;
        this.pageUniqueId = 0;

        this.DeviceCommunicationsParameterAndPacketList = new List();

        this.bEnableCommunicationTransmissions = false;
        this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = false;

        this.bJustToSendResponsesStraightThrough = false;
        this.blockDeviceTypeDetermined = false;
        this.ReceivedPacket = [];
        // this.ListOfBytesFromScopeBlockResponse = [[], [], [], [], [], [], [], [], [], [], [], [], [], [], [], []];  // 4 channels of scope block
        this.NumberOfBytesExpected = 0;
        this.Retries = 3;
        this.ReceivedPacketCount = 0;
        this.transmittedPageIndex = 0;
        this.transmittedPacketIndex = 0;
        this.TransmitPayload = function (dataArray) { }; //filled in with delegate

    }

    GroupParameterListTo64bytesAndAddressRange(pageParameterList, oddPacketSizeHack = true)
    {
        var pageCommunicationsList = new PageCommunicationsList(pageParameterList, new List(new List()), new List(new List()));

        //parse the list (which is all parameters on the page calling this
        //create new lists by breaking it into 64 byte chunks (limited by communication protocal)
        //also creating separate chunked lists based on address range (TAC is growing and needs further parsing when it 
        //goes past 512 PRESENTLY ANYTHING OVER 256 ie, even greater than 512 goes into 256-512 range which is wrong
        var list = pageParameterList.parameterList.Where(i => i.Address != this.ModelReference.Model.ONLY_IN_APP && i.Address < 256).ToList();
        if (list.Count != 0)
        {
            while (list.Count > 64)
            {
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                list.DeleteRange(list.Count - 64, 64);
            }
            pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
            var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
            if (lastItem.Count % 2 == 1 && oddPacketSizeHack) //odd count
            {//something doesn't like odd packet sizes so duplicate last member
                lastItem.Add(lastItem[lastItem.Count - 1]);
            }
        }
        list = pageParameterList.parameterList.Where(i => i.Address != this.ModelReference.Model.ONLY_IN_APP && i.Address >= 256 && i.Address < 512).ToList();
        if (list.Count != 0)
        {
            while (list.Count > 64)
            {
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                list.DeleteRange(list.Count - 64, 64);
            }
            pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
            var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
            if (lastItem.Count % 2 == 1 && oddPacketSizeHack) //odd count
            {//something doesn't like odd packet sizes so duplicate last member
                lastItem.Add(lastItem[lastItem.Count - 1]);
            }
        }
        list = pageParameterList.parameterList.Where(i => i.Address != this.ModelReference.Model.ONLY_IN_APP && i.Address >= 512 && i.Address < 768).ToList();
        if (list.Count != 0)
        {
            while (list.Count > 64)
            {
                pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list.GetRange(list.Count - 64, 64));
                list.DeleteRange(list.Count - 64, 64);
            }
            pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Add(list);
            var lastItem = pageCommunicationsList.parametersGroupedto64bytesAndAddressRange[pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.Count - 1];
            if (lastItem.Count % 2 == 1 && oddPacketSizeHack) //odd count
            {//something doesn't like odd packet sizes so duplicate last member
                lastItem.Add(lastItem[lastItem.Count - 1]);
            }
        }
        return pageCommunicationsList;
    }

    BuildAReadCommandPageList(pageParameterList) //public long
    {
        var pageCommunicationsList = this.GroupParameterListTo64bytesAndAddressRange(pageParameterList);
        pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.forEach((parameterList, index, array) =>
        {
            //Back to javascript array because I don't totally understand array of objects
            //In javascript array of objects (List.js) the index of an object is a bit odd and
            //might not stay in order? like after an insert?
            //modbus like protocal: 0x02, 'T', 'A', 'C', ??, length, command, address 1, address 2,..., crcA, crcB, ETX(0x03)
            //command: 0x20 = read parameter < 256
            //dataArray of data [0x02, 0x54, 0x41, 0x43, 0x00, 0x20, 2, 177, 32, 38, 193, 0x03];
            var transmitPacket = [0x02, 0x54, 0x53, 0x58, 0x00, 0x20, 0, 0xa3, 0x63, 0x03];
            var addressModifier = 0;
            //each group is already separated into address ranges so just check first first address and modify
            //the rest according to the first

            if (parameterList[0].Address >= 512)
            {
                transmitPacket[5] = 0x26;
                addressModifier = 512;
            }
            else if (parameterList[0].Address >= 256)
            {
                transmitPacket[5] = 0x23;
                addressModifier = 256;
            }

            parameterList.forEach((parameter, index, array) =>
            {
                transmitPacket.splice((transmitPacket.length - 3), 0, parameter.Address - addressModifier) //transmitPacket.Insert((transmitPacket.Count - 3), parseInt(parameter.Address));
            });

            let checksum = [0, 0];
            transmitPacket[6] = (transmitPacket.length - 10);
            this.fletcher16(checksum, transmitPacket, transmitPacket.length - 3);
            transmitPacket[transmitPacket.length - 3] = checksum[0];
            transmitPacket[transmitPacket.length - 2] = checksum[1];

            pageCommunicationsList.matchingPackets.Add(transmitPacket);
        });
        return (pageCommunicationsList); //index into this list used by actual page to enable and disable comms.
    }

    BuildAWriteCommandPageList(pageParameterList) //public long
    {
        var pageCommunicationsList = this.GroupParameterListTo64bytesAndAddressRange(pageParameterList, false);
        pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.forEach((parameterList, index, array) =>
        {
            //Back to javascript array because I don't totally understand array of objects
            //In javascript array of objects (List.js) the index of an object is a bit odd and
            //my not stay in order? like after an insert?
            //modbus like protocal: STX(0x02), 'T', 'A', 'C', ??, command, length, address 1, value 1 (MSBs), value 2 (LSBs), address 2,..., crcA, crcB, ETX(0x03)
            //command: 0x20 = read parameter < 256
            //dataArray of data [0x02, 0x54, 0x41, 0x43, 0x00, 0x20, 2, 177, 32, 38, 193, 0x03];
            var transmitPacket = [0x02, 0x54, 65, 67, 0x00, 0x21, 0x00, 0xa3, 0x63, 0x03]; //Nov 2021, just found that controller echos pachet back only changing to TAC without checksum calc
            var addressModifier = 0;
            //each group is already separated into address ranges so just check first first address and modify
            //the rest according to the first
            if (parameterList[0].Address >= 512)
            {
                transmitPacket[5] = 0x27;
                addressModifier = 512;
            }
            else if (parameterList[0].Address >= 256)
            {
                transmitPacket[5] = 0x24;
                addressModifier = 256;
            }
            parameterList.forEach((parameter, index, array) =>
            {
                transmitPacket.splice((transmitPacket.length - 3), 0, parameter.Address - addressModifier) //transmitPacket.Insert((transmitPacket.Count - 3), parseInt(parameter.Address));
                transmitPacket.splice((transmitPacket.length - 3), 0, (parameter.rawValueToBeWritten >> 8) & 0xFF) //transmitPacket.Insert((transmitPacket.Count - 3), parseInt(parameter.Address));
                transmitPacket.splice((transmitPacket.length - 3), 0, parameter.rawValueToBeWritten & 0xFF); //transmitPacket.Insert((transmitPacket.Count - 3), parseInt(parameter.Address));
            });

            let checksum = [0, 0];
            transmitPacket[6] = (transmitPacket.length - 10);
            this.fletcher16(checksum, transmitPacket, transmitPacket.length - 3);
            transmitPacket[transmitPacket.length - 3] = checksum[0];
            transmitPacket[transmitPacket.length - 2] = checksum[1];

            pageCommunicationsList.matchingPackets.Add(transmitPacket);
        });
        return pageCommunicationsList;
    }

    BuildAReadScopeBlockCommandPageList(pageParameterList) //public long
    {
        var pageCommunicationsList = this.GroupParameterListTo64bytesAndAddressRange(pageParameterList);
        pageCommunicationsList.parametersGroupedto64bytesAndAddressRange.forEach((parameterList, index, array) =>
        {
            //Back to javascript array because I don't totally understand array of objects
            //In javascript array of objects (List.js) the index of an object is a bit odd and
            //might not stay in order? like after an insert?
            //modbus like protocal: 0x02, 'T', 'A', 'C', ??, length, command, address 1, address 2,..., crcA, crcB, ETX(0x03)
            //command: 0x20 = read parameter < 256
            //dataArray of data [0x02, 0x54, 0x41, 0x43, 0x00, 0x20, 2, 177, 32, 38, 193, 0x03];
            var transmitPacket = [0x02, 0x54, 0x41, 0x43, 0x00, 0x20, 0, 0, 0xa3, 0x63, 0x03];

            transmitPacket[5] = parameterList[0].Address;  // cmd
            transmitPacket[6] = 1;

            parameterList.forEach((parameter, index, array) =>
            {
                if (parameter.Address != 0x25)    // cmd to read FLASH sector
                {
                    transmitPacket[7] = parameter.rawValueToBeWritten;
                }
                else
                {
                    transmitPacket[4] = parameter.rawValueToBeWritten / 256;  // Bits 10:8 contain sector number from 0 to 7.
                    transmitPacket[7] = parameter.rawValueToBeWritten & 0x7F;          // Low byte contains offset into sector.
                }
            });

            let checksum = [0, 0];
            transmitPacket[6] = (transmitPacket.length - 10);
            this.fletcher16(checksum, transmitPacket, transmitPacket.length - 3);
            transmitPacket[transmitPacket.length - 3] = checksum[0];
            transmitPacket[transmitPacket.length - 2] = checksum[1];

            pageCommunicationsList.matchingPackets.Add(transmitPacket);
        });
        return (pageCommunicationsList); //index into this list used by actual page to enable and disable comms.
    }

    // public void SetScopeBockResponseHandler(EventHandler<ScopeGetBlockResponseEventArgs> handler)
    // {
    //     ScopeBockResponseHandler = handler;
    // }

    fletcher16(twoByteCheckBufferRef, charbuffer, len)
    {
        let sum1 = 0xff, sum2 = 0xff;
        let i = 0;
        while (len)
        {
            let tlen = len > 21 ? 21 : len;
            len -= tlen;
            do
            {
                sum1 += charbuffer[i];
                sum2 += sum1;
                i++;
            } while (--tlen);
            sum1 = (sum1 & 0xff) + (sum1 >> 8);
            sum2 = (sum2 & 0xff) + (sum2 >> 8);
        }
        //Second reduction step to reduce sums to 8 bits
        sum1 = (sum1 & 0xff) + (sum1 >> 8);
        sum2 = (sum2 & 0xff) + (sum2 >> 8);
        twoByteCheckBufferRef[0] = sum1;
        twoByteCheckBufferRef[1] = sum2;
    }
    PacketResponseReceived(receivedEvent) //(object sender, this.WaitingForReceiveResponseEventArgs receivedEvent)
    {
        try
        {
            this.recievedResponseTimer = 0; //we recieved something so reset timer for timeout
            let receivedEventReceivedData = Array.from(new Uint8Array(receivedEvent.data));
            let ProcessedReceivedPacket = [];
            if (receivedEventReceivedData == null)
            {
                console.log("ReceivedPacket null should never happen?????????????????????");
                return;
            }
            if (this.ReceivedPacket == null)
            {
                this.ReceivedPacket = [];
            }
            if (this.ReceivedPacket.length > 200)
            {
                console.log("ReceivedPacket.length > 200should never happen?????????????????????");
            }
            this.ReceivedPacket = this.ReceivedPacket.concat(receivedEventReceivedData);
            if (this.ReceivedPacket.length >= 7)
            {
                this.NumberOfBytesExpected = this.ReceivedPacket[6] + 10;
                //console.log("Building ReceivedPacket, added " + receivedEventReceivedData.length.toString() + " for " + this.ReceivedPacket.length.toString() + " Number of data bytes expected " + this.NumberOfBytesExpected);
            }
            else
            {
                //console.log("Building ReceivedPacket, added " + receivedEventReceivedData.length.toString() + " for " + this.ReceivedPacket.length.toString() + " Number of data bytes expected  " + "unknown, only " + this.ReceivedPacket.length.toString() + " received");
            }

            //ReceivedPacket == null means the start of a new packet
            if (this.bJustToSendResponsesStraightThrough)
            {
                ProcessedReceivedPacket = this.ReceivedPacket;
                //console.log("Hope it is a boot load packet");
                this.ReceivedPacket = null;
            }
            else if (receivedEventReceivedData[0] == 'A'.charCodeAt(0) && receivedEventReceivedData.length == 1)
            {
                ProcessedReceivedPacket = ['A'.charCodeAt(0)];
                console.log("Detected Boot Mode for TAC");
                this.ReceivedPacket = null;
            }
            else if (this.ReceivedPacket != null)
            {
                if (this.ReceivedPacket[0] != 0x02)
                {
                    let x = this.ReceivedPacket.indexOf(0x02);
                    let y = this.ReceivedPacket.indexOf('T'.charCodeAt(0));
                    if (x != -1 && y == (x + 1))
                    {
                        this.ReceivedPacket = this.ReceivedPacket.slice(x, this.ReceivedPacket.length);
                        console.log("ReceivedPacket, Resynced");
                    }
                    else
                    {
                        ProcessedReceivedPacket = [0xaa];
                        console.log("ReceivedPacket, ReceivedPacket[0] != 0x02");
                        this.ReceivedPacket = null;
                    }
                }
                if (this.ReceivedPacket != null && this.ReceivedPacket.length >= 7) //RG Oct '21 above can set ReceivedPacket = null which is dumb so fix it
                {
                    this.NumberOfBytesExpected = this.ReceivedPacket[6] + 10; // data bytes plus there are 10 bytes of overhead

                    if (this.ReceivedPacket.length == this.NumberOfBytesExpected)
                    {
                        let checksum = [0, 0];
                        this.fletcher16(checksum, this.ReceivedPacket, this.NumberOfBytesExpected - 3);
                        if ((this.ReceivedPacket[this.NumberOfBytesExpected - 3] == checksum[0]) && (this.ReceivedPacket[this.NumberOfBytesExpected - 2] == checksum[1]))
                        {
                            ProcessedReceivedPacket = this.ReceivedPacket.slice(0, this.NumberOfBytesExpected);
                            this.ReceivedPacket = null;
                            if (DEBUG_NAVITASMODBUS) console.log("ReceivedPacket, Successfull"); //don't fill the screen with this
                        }
                        else
                        {
                            console.log("ReceivedPacket, Checksum Failed");
                            this.ReceivedPacket = null;
                            return;
                        }
                    }
                    else if (this.ReceivedPacket.length > this.NumberOfBytesExpected)
                    {// Navitas Modbus protocal always expects you to wait for a valid reply
                        console.log("ReceivedPacket, ReceivedPacket.length " + this.ReceivedPacket.length + " > NumberOfBytesExpected " + this.NumberOfBytesExpected);
                        // ProcessedReceivedPacket = [0xaa];
                        ProcessedReceivedPacket = this.ReceivedPacket.splice(0, this.NumberOfBytesExpected)
                        console.log("But... why did you receive more than expected CHECK YOUR TRANSMIT LOGIC AND TIMING");
                        // this.ReceivedPacket = null; //get rid of invalid receive
                    }
                    else
                    {
                        console.log("ReceivedPacket, ReceivedPacket.length " + this.ReceivedPacket.length + " < NumberOfBytesExpected " + this.NumberOfBytesExpected);
                        return;
                    }
                }
                else
                {
                    console.log("ReceivedPacket, ReceivedPacket.length < 7");
                    return;
                }
            }
            else
            {
                console.log("RG: Dec 2019, Not sure why but ReceivedPacket == null");
                return;
            }

            if (ProcessedReceivedPacket == null)
            {
                console.log("Processed ReceivedPacket null should also never happen?????????????????????");
                return;
            }

            if (this.PacketSchedule == this.PacketStates.bootLoader)
            {
                if (this.ModelType == "TAC")
                {
                    this.bJustToSendResponsesStraightThrough = true;
                    this.UpdateModelWithPacketResponse(this.pageUniqueId, this.transmittedPacketIndex, []);
                    App.FirmwareDownload.ResponseFromBootLoader(ProcessedReceivedPacket);
                }
                if (this.ModelType == "TSX")
                {
                    this._FirmwareDownloadTSXPage.ResponseFromBootLoader(ProcessedReceivedPacket);
                }
            }
            else if (this.PacketSchedule == this.PacketStates.gotoBoot)
            {
                if (this.ModelType == "TSX")
                {
                    if (ProcessedReceivedPacket[9] == 6) // bootloader version
                    {
                        this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = true;
                    }
                }
                else if (ProcessedReceivedPacket[0] == 'A'.charCodeAt(0)) //TAC TI internal bootloader autobaud echo responses (it found this to get here but why not do it again)
                {
                    this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode = true;
                    this.bJustToSendResponsesStraightThrough = true;
                    this.PacketSchedule = this.PacketStates.bootLoader;
                    this.ModelType = "TAC"; //was not determined by initial packet because the initial packet will screw up this bootloader
                }
                else
                {  // just to remove the boot command response
                    this.UpdateModelWithPacketResponse(this.pageUniqueId, this.transmittedPacketIndex, []);
                    this.bJustToSendResponsesStraightThrough = true;
                    this.PacketSchedule = this.PacketStates.bootLoader;
                }

                console.log("case 7 this.recievedResponseTimer = " + this.recievedResponseTimer.toString());
                this.WaitingForReceiveResponse = false;
            }
            else
            {
                if (ProcessedReceivedPacket[0] != 0xaa && ProcessedReceivedPacket.length > 4 && (ProcessedReceivedPacket[4] & 0x80) != 0x80)
                {// ProcessedReceivedPacket[4] & 0x80 is the error bit that happens if the ac  firmware has a checksum error.
                    switch (this.PacketSchedule)
                    {
                        case this.PacketStates.startUp:

                            if (DEBUG_NAVITASMODBUS) console.log("Received startUp packet");

                            if (ProcessedReceivedPacket[1] == 'T'.charCodeAt(0) && ProcessedReceivedPacket[2] == 'A'.charCodeAt(0) && ProcessedReceivedPacket[3] == 'C'.charCodeAt(0))
                            {
                                // console.log("TAC Seen");
                                this.ModelType = "TAC";
                                this.DeviceHasReplied = true;
                            }
                            if (ProcessedReceivedPacket[1] == 'T'.charCodeAt(0) && ProcessedReceivedPacket[2] == 'S'.charCodeAt(0) && ProcessedReceivedPacket[3] == 'X'.charCodeAt(0))
                            {
                                // console.log("TSX Seen");
                                this.ModelType = "TSX";
                                this.DeviceHasReplied = true;
                            }

                            if ((this.DeviceHasReplied == true) && (this.blockDeviceTypeDetermined == false))
                            {
                                // this.DeviceTypeDeterminedDelegate.detail.ModelType = this.ModelType;
                                // this.dispatchEvent(this.DeviceTypeDeterminedDelegate);
                                this.ModelReference.Model = this.ModelType;
                                this.blockDeviceTypeDetermined = true;
                            }

                            break;
                        case this.PacketStates.standardCommunications:

                            if (DEBUG_NAVITASMODBUS) console.log("Received standardCommunications (page, packet) " + (this.DeviceCommunicationsParameterAndPacketList.FirstOrDefault(x => (x.parentPage.uniqueID == this.pageUniqueId))).parentPage.ParentTitle + "," + this.transmittedPacketIndex.toString() + " Rx Length = " + ProcessedReceivedPacket.length.toString() + " Received Count = " + this.ReceivedPacketCount.toString());

                            var ProcessedReceivedData = [];
                            if ((this.DeviceCommunicationsParameterAndPacketList.FirstOrDefault(x => (x.parentPage.uniqueID == this.pageUniqueId))).parentPage.ProtocalCommandType == PageParameterList.ProtocalCommandTypes.WRITE)
                            {
                                for (let i = 8; i < (this.NumberOfBytesExpected - 3); i += 3)
                                {
                                    ProcessedReceivedData.push(ProcessedReceivedPacket[i] << 8 | ProcessedReceivedPacket[i + 1]);
                                }
                            }
                            else if ((this.DeviceCommunicationsParameterAndPacketList.FirstOrDefault(x => (x.parentPage.uniqueID == this.pageUniqueId))).parentPage.ProtocalCommandType == PageParameterList.ProtocalCommandTypes.READ)
                            {
                                for (let i = 7; i < (this.NumberOfBytesExpected - 3); i += 2)
                                {
                                    ProcessedReceivedData.push(ProcessedReceivedPacket[i] << 8 | ProcessedReceivedPacket[i + 1]);
                                }
                            }
                            //if (ProcessedReceivedPacket.length == (packetParentPointer.matchingPackets[packetIndex].length * 2) - 10) //check for standardCommunications response? why bother?
                            this.UpdateModelWithPacketResponse(this.pageUniqueId, this.transmittedPacketIndex, ProcessedReceivedData);
                            this.ReceivedPacketCount++;
                            //console.log("PacketStates.standardCommunications");

                            // if (App.AppConfigurationLevel == "DEALER" || App.AppConfigurationLevel == "ENG" || App.AppConfigurationLevel == "ADVANCED_USER" || App.AppConfigurationLevel == "ENG_USER")
                            // {
                            //     if (MainFlyoutlPage.DealerSettingsPage != null)
                            //         MainFlyoutlPage.DealerSettingsPage.AddPointsToGraphForPacket();
                            //     if (MainFlyoutlPage.DealerSettingsTSXPage != null)
                            //         MainFlyoutlPage.DealerSettingsTSXPage.AddPointsToGraphForPacket();
                            // }
                            // this.Retries = 3;
                            break;
                        case this.PacketStates.saveOrInit:

                            if (DEBUG_NAVITASMODBUS) console.log("Received saveOrInit packet");

                            var ProcessedReceivedData = [];
                            for (let i = 7; i < (this.NumberOfBytesExpected - 3); i += 2)
                            {
                                ProcessedReceivedData.push(ProcessedReceivedPacket[i] << 8 | ProcessedReceivedPacket[i + 1]);
                            }
                            this.UpdateModelWithPacketResponse(this.pageUniqueId, this.transmittedPacketIndex, ProcessedReceivedData);
                            this.ReceivedPacketCount++;
                            break;
                        case this.PacketStates.getBlock:

                            if (DEBUG_NAVITASMODBUS) console.log("Received getBlock packet");

                            // this.ListOfBytesFromScopeBlockResponse[this.ScopeBlockSchedule].length = 0;
                            let timeBase = 0
                            var ProcessedReceivedData = []
                            for (let i = 7; i < (this.NumberOfBytesExpected - 3); i += 2)
                            {
                                let blockData = ProcessedReceivedPacket[i] << 8 | ProcessedReceivedPacket[i + 1];
                                ProcessedReceivedData.push(blockData);
                                let timeAndValuePairObject = { time: timeBase, value: blockData };
                                this.ModelReference.Model.ScopeDataArray[0].parameterTimeAndValuePairList.push(timeAndValuePairObject);
                                timeBase += 1;
                            }
                            this.UpdateModelWithPacketResponse(this.pageUniqueId, this.transmittedPacketIndex, ProcessedReceivedData);
                            this.ReceivedPacketCount++;
                            // List<byte> bytelist = new List<byte>(ProcessedReceivedPacket);
                            // //console.log("ResponseFromBlock 1 " + DateTime.Now.Millisecond.toString());
                            // ScopeBockResponseHandler(this, new ScopeGetBlockResponseEventArgs(bytelist.GetRange(7, ProcessedReceivedPacket[6])));
                            // //console.log("ResponseFromBlock 2 " + DateTime.Now.Millisecond.toString());
                            break;
                        case this.PacketStates.getFlashBlock:

                            if (DEBUG_NAVITASMODBUS) console.log("Received getFlashBlock packet");

                            // List<byte> flashbytelist = new List<byte>(ProcessedReceivedPacket);
                            // if (App.AppConfigurationLevel == "ENG" || App.AppConfigurationLevel == "ENG_USER")
                            // {
                            //     App._MainFlyoutPage.ParameterFilePage.ResponseFromBlock(flashbytelist.GetRange(7, ProcessedReceivedPacket[6]));
                            // }
                            break;
                    }
                    if (this.EnableReceiveEvent) this.dispatchEvent(this.ReceiveDelegate);
                    this.WaitingForReceiveResponse = false;
                }
            }
        }
        catch (e)
        {
            console.log("PacketResponseReceived exception: " + e);
        }
        //console.log("PacketResponseReceived exiting");
    } //end of PacketResponseReceived()
    async InitialPacket()
    {
        //initial startup response check of TAC bootmode.Both TAC and TSX will timeout if TAC is not in bootmode, TSX handles it differently
        console.log("NavitasModbus first test byte sent");
        this.TransmitPayload([0x41]); //Add delegate or injection or something _Mode.TransmitPayload(new byte[] { (byte)'A' }); //only the TAC bootloader would respond to a single byte containing'A'
        while (this.WaitingForReceiveResponse)
        {
            let nowtime = new Date().getTime() / 1000; //DateTime.Now.Second;
            if (this.recievedResponseTimer > 1)  // if 1 second goes by then there was no response to the one byte bootmode check for TAC
            {
                //so try the regular communication protocal that works for both TAC and TSX
                this.WaitingForReceiveResponse = true;
                this.PacketSchedule = this.PacketStates.startUp;

                //response to this basic packet will determin vehicle type (TAC or TSX)
                let packetGetConrollerType = [0x02, 0x54, 0x53, 0x58, 0x00, 0x20, 2, 1, 40, 0xa3, 0x63, 0x03];
                let checksum = [0, 0];
                this.fletcher16(checksum, packetGetConrollerType, packetGetConrollerType.length - 3);
                packetGetConrollerType[packetGetConrollerType.length - 3] = checksum[0];
                packetGetConrollerType[packetGetConrollerType.length - 2] = checksum[1];
                this.recievedResponseTimer = 0;
                console.log("Transmitting initial start up packet to discover Vehicle type ");
                await Task.Delay(1000);  // wait for controller to power up
                this.TransmitPayload(packetGetConrollerType);
                while (this.WaitingForReceiveResponse)
                {//wait for vehicle type response
                    // nowtime = new Date().getTime() / 1000;
                    if (this.recievedResponseTimer > 4) // if 6 seconds go buy without a response set the flag to try again.
                    {
                        console.log("BeginThread Initial not in boot Timeout = {0} ", this.recievedResponseTimer);
                        this.TransmitPayload(packetGetConrollerType);
                        // cancellationTokenSource.Cancel(); //notify the thread about stopping
                        // cancellationTokenSource.Token.ThrowIfCancellationRequested(); //stop the thread
                    }
                    await Task.Delay(1000);
                    this.recievedResponseTimer += 1;
                }
            }
            await Task.Delay(50); //Just being nice + wait for save parameters to finish
            this.recievedResponseTimer += 0.05;
        }

        if (this.ModelType == "TSX") //set elsewhere from above response
        {
            let packetinitBoot = [0x02, 0x54, 0x53, 0x58, 0x01, 0x01, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03];
            let checksum = [0, 0];
            this.fletcher16(checksum, packetinitBoot, packetinitBoot.length - 3);
            packetinitBoot[packetinitBoot.length - 3] = checksum[0];
            packetinitBoot[packetinitBoot.length - 2] = checksum[1];
            this.PacketSchedule = this.PacketStates.gotoBoot;
            this.recievedResponseTimer = 0;
            this.WaitingForReceiveResponse = true;
            this.TransmitPayload(packetinitBoot);
            while (this.WaitingForReceiveResponse)
            {
                let nowtime = new Date().getTime() / 1000;
                if (this.recievedResponseTimer > 4)  // if 4 seconds go buy without a respons set the flag to try again.
                {
                    console.log("BeginThread Initial TSX Timeout = {0} ", this.recievedResponseTimer);
                    // cancellationTokenSource.Cancel();
                    // cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
                await Task.Delay(50); //Just being nice + wait for save parameters to finish
                this.recievedResponseTimer += 0.05;
            }
            if (this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode)
            {
                // App._MainFlyoutPage.BootloaderDetermined(true);
            }
            console.log("Bootloader only check done");

        }
        if (this.ModelType == "TAC" && this.PacketSchedule == this.PacketStates.gotoBoot)
        {//above responded in less than a second to a one byte bootmode check for TAC and was not set to startup
            this.bJustToSendResponsesStraightThrough = true;
            this.PacketSchedule = this.PacketStates.bootLoader;
            if (this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode)
            {
                App.FirmwareDownload.AlreadyTalkingToBootloader = true;
                await App.FirmwareDownload.Download();
            }
        }
    }
    async BeginThread()
    {
        //try
        {
            this.ReceivedPacket = null; //clear any old stuff left in here from previous timeouts
            this.DeviceHasReplied = false;
            this.blockDeviceTypeDetermined = false;
            this.WaitingForReceiveResponse = true;
            this.recievedResponseTimer = 0;

            this.PacketSchedule = this.PacketStates.gotoBoot;
            await this.InitialPacket();

            this.recievedResponseTimer = 0;
            let timeoutmax = 3;
            let timeToDelayingMoreTransmissions = 0;
            let TransmittedPacketCount = 0;
            let pageIndex = 0;
            let packetIndex = 0;
            console.log("NavitasModbus thread started");

            while (true)
            {
                //             var debug = MainThread.IsMainThread;
                //             cancellationTokenSource.Token.ThrowIfCancellationRequested();
                if (this.bCommunicationsStartedWithControllerAlreadyInBootLoaderMode == false && this.bEnableCommunicationTransmissions)
                {//nobody is in boot mode
                    //for clarity, this.WaitingForReceiveResponse == false indicates time to send another one
                    if (!this.WaitingForReceiveResponse)
                    {
                        if (this.DeviceCommunicationsParameterAndPacketList.Count != 0) //no code has filled it in yet
                        {
                            try //get next packet from list, only spin through once in case there are no packets to send
                            {
                                do {
                                    if (pageIndex >= this.DeviceCommunicationsParameterAndPacketList.Count) //only happens when a page specific packet was last and removed by the page
                                    {
                                        pageIndex = 0;
                                        packetIndex = 0;
                                    }
                                    else if (++packetIndex >= this.DeviceCommunicationsParameterAndPacketList[pageIndex].matchingPackets.Count)
                                    {
                                        packetIndex = 0;
                                        if (++pageIndex >= this.DeviceCommunicationsParameterAndPacketList.Count)
                                        {
                                            pageIndex = 0;
                                        }
                                    }
                                }
                                while (!(pageIndex == 0 && packetIndex == 0) && ((!this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.Active)
                                    || (this.ModelReference.GetParameter("SOFTWAREREVISION").parameterValue <= 1.280 && this.DeviceCommunicationsParameterAndPacketList[pageIndex].matchingPackets[packetIndex][5] == 0x23)
                                    || (this.ModelReference.GetParameter("SOFTWAREREVISION").parameterValue <= 5.000 && this.DeviceCommunicationsParameterAndPacketList[pageIndex].matchingPackets[packetIndex][5] == 0x26)
                                )); //skip if not Active (gauge is usually active plus the visible page) and 1.280 or less does not support reading parameters above 255
                            } catch (ex) {
                                console.log("Loop through pointers exception: " + ex);
                            }
                            // if ((Date.now() - this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.timeCreated) > 3000) {
                            //     this.ModelReference.Communications.RemovePacketList(this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.uniqueID);
                            // }
                            // else
                            // {
                            try //send the command
                            {
                                if (this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.Active) {
                                    let packet = this.DeviceCommunicationsParameterAndPacketList[pageIndex].matchingPackets[packetIndex];
                                    if (DEBUG_NAVITASMODBUS) console.log(new Date().getTime() + ": About to transmit (page, packet) " + this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.ParentTitle + "," + packetIndex.toString() + " length = " + packet.length.toString() + " Transmitted Count = " + TransmittedPacketCount.toString());
                                    if (this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.ProtocalCommandType == PageParameterList.ProtocalCommandTypes.WRITE) {
                                        if (DEBUG_NAVITASMODBUS) console.log("WRITE command sent");

                                        if (this.ModelType == "TAC") {
                                            if (packet[5] == 0x22) // Get Block
                                            {
                                                this.PacketSchedule = this.PacketStates.getBlock;
                                                //console.log("_Mode.GetBlock(packet[7], e.fParameterValue)");
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                            } else if (packet[5] == 0x25) // Read Flash Sector
                                            {
                                                // Get Block
                                                this.PacketSchedule = this.PacketStates.getFlashBlock;
                                                //console.log("_Mode.GetBlock(packet[7], e.fParameterValue)");
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                            } else {
                                                if (packet[7] == 49)  // init parameters
                                                {
                                                    timeoutmax = 9;
                                                    this.PacketSchedule = this.PacketStates.saveOrInit;
                                                } else if (packet[7] == 50)  // save parameters
                                                {
                                                    timeoutmax = 9;
                                                    this.PacketSchedule = this.PacketStates.saveOrInit;
                                                    timeToDelayingMoreTransmissions = 5; //saving takes around 4 seconds
                                                } else if (packet[7] == 109)  // bootloader
                                                {
                                                    this.PacketSchedule = this.PacketStates.bootLoader;
                                                    this.bJustToSendResponsesStraightThrough = true;
                                                } else {
                                                    //RG: Dec 2021 remove queuing concept so use this so packet is removed once recieved
                                                    this.PacketSchedule = this.PacketStates.standardCommunications;
                                                    timeoutmax = 3;
                                                }
                                            }
                                        } else if (this.ModelType == "TSX") {
                                            if (packet[7] == 199)  // save  parameters
                                            {
                                                timeoutmax = 9;
                                                this.PacketSchedule = this.PacketStates.saveOrInit;
                                                timeToDelayingMoreTransmissions = 5;
                                            } else if (packet[7] == 0xff && e.strParameter == "Bootload") {
                                                // INIT BOOT.
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.InitBootLoaderMode(e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.InitBootLoaderMode(e.fParameterValue);
                                                // }
                                                this.PacketSchedule = this.PacketStates.bootLoader;
                                                timeoutmax = 120;
                                            } else if (packet[7] == 0xff && e.strParameter == "FastDatalog") {
                                                console.log("FastDatalog");
                                            } else {
                                                this.PacketSchedule = this.PacketStates.standardCommunications;
                                                timeoutmax = 3;
                                            }
                                        }
                                    } else {
                                        if (DEBUG_NAVITASMODBUS) console.log("Read command sent");

                                        if (this.ModelType == "TAC") {
                                            if (packet[5] == 0x22) // Get Block
                                            {
                                                this.PacketSchedule = this.PacketStates.getBlock;
                                                this.ScopeBlockSchedule = packet[7];
                                                //console.log("_Mode.GetBlock(packet[7], e.fParameterValue)");
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                            } else if (packet[5] == 0x25) // Read Flash Sector
                                            {
                                                // Get Block
                                                this.PacketSchedule = this.PacketStates.getFlashBlock;
                                                //console.log("_Mode.GetBlock(packet[7], e.fParameterValue)");
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.GetBlock(packet[7], e.fParameterValue);
                                                // }
                                            } else {
                                                //RG: Dec 2021 remove queuing concept so use this so packet is removed once recieved
                                                this.PacketSchedule = this.PacketStates.standardCommunications;
                                                timeoutmax = 3;
                                            }
                                        } else if (this.ModelType == "TSX") {
                                            if (packet[7] == 199)  // save  parameters
                                            {
                                                timeoutmax = 9;
                                                this.PacketSchedule = this.PacketStates.saveOrInit;
                                                timeToDelayingMoreTransmissions = 5;
                                            } else if (packet[7] == 0xff && e.strParameter == "Bootload") {
                                                // INIT BOOT.
                                                // if (strMode == "Demo")
                                                // {
                                                //     _Mode.InitBootLoaderMode(e.fParameterValue);
                                                // }
                                                // if (strMode == "Physical")
                                                // {
                                                //     _Mode.InitBootLoaderMode(e.fParameterValue);
                                                // }
                                                this.PacketSchedule = this.PacketStates.bootLoader;
                                                timeoutmax = 120;
                                            } else if (packet[7] == 0xff && e.strParameter == "FastDatalog") {
                                                console.log("FastDatalog");
                                            } else {
                                                this.PacketSchedule = this.PacketStates.standardCommunications;
                                                timeoutmax = 3;
                                            }
                                        }
                                    }
                                    // this.transmittedPageIndex = pageIndex;
                                    this.transmittedPacketIndex = packetIndex;
                                    this.pageUniqueId = this.DeviceCommunicationsParameterAndPacketList[pageIndex].parentPage.uniqueID;
                                    this.WaitingForReceiveResponse = true;
                                    this.TransmitPayload(packet);//.ToArray()); //_Mode.
                                    TransmittedPacketCount++;
                                    // packetIndex++;
                                    // pageIndex++;
                                }
                            }
                            catch (ex)
                            {
                                console.log("TransmitPayload 1 Exception exited: " + ex);
                            }
                            // }
                        }
                        this.recievedResponseTimer = 0;
                    }
                    else if (this.recievedResponseTimer > timeoutmax)
                    {
                        this.ModelReference.GetParameter("COMMERROR").parameterValue = 1.0;
                        this.ReceivedPacket = null;
                        this.WaitingForReceiveResponse = false; //wait for someone else to retry
                    }
                    else
                    {
                        this.ModelReference.GetParameter("COMMERROR").parameterValue = 0.1;
                    }
                    this.recievedResponseTimer += 0.05;
                }
                else if (this.bEnableCommunicationTransmissions)//somebody is in boot mode
                {
                    this.PacketSchedule = this.PacketStates.bootLoader;
                    timeoutmax = 240; //two minutes seems short until we kick back into comms
                }
                if (timeToDelayingMoreTransmissions != 0)
                {
                    //MessagingCenter.Send<DeviceComunication>(this, "ShowActivity");
                }
                await Task.Delay(50 + timeToDelayingMoreTransmissions * 1000); //Just being nice + wait for save parameters to finish
                if (timeToDelayingMoreTransmissions != 0)
                {
                    //MessagingCenter.Send<DeviceComunication>(this, "StopActivity");
                    timeToDelayingMoreTransmissions = 0;
                }
            }
        }
        //catch (OperationCanceledException e)
        //{
        //    console.log("OperationCanceledException and exited " + e.Message);
        //    Timeout(null, null);
        //    ClosePhysicalMode();
        //}
    //    catch (e)
    //    {
    //        console.log("BeginThread Exception exited" + e);
    //    }
    } // End of BeginThread()

}