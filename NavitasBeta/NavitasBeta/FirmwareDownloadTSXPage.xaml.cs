using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

using System.IO;
using Xamarin.Essentials;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirmwareDownloadTSXPage : NavitasGeneralPage
    {
        public event EventHandler<WritePacketEventArgs> WritePacket = delegate { };
        public event EventHandler<EventArgs> ProgrammingDone = delegate { };
        public event EventHandler<EventArgs> ClearQueue = delegate { };
        uint calculatedchecksum = 0;

        public FirmwareDownloadTSXPage(bool alreadyTalkingToBootloader = false)
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTSX();
                AlreadyTalkingToBootloader = alreadyTalkingToBootloader;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GetSupplierChainObject(AlreadyTalkingToBootloader);
                    InsertFileList();
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: FirmwareDownloadTSXPage.xaml.cs" + ex.Message);
            }
        }

        public bool AlreadyTalkingToBootloader = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                DeviceDisplay.KeepScreenOn = true;
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.KeepScreenOn = false;
        }

        bool bUnlockJumpToBoot = false;
        async Task<bool> EnableJumpToBoot()
        {
            System.Diagnostics.Debug.WriteLine("EnableJumpToBoot");
            QueParameter(new SetParameterEventArgs(0xff, 1.0f, "Bootload"));
            bResponseHandShake = false;

            int seconds = 0;
            while (!bResponseHandShake)
            {
                await Task.Delay(100);
                seconds++;
                if (seconds > 10)
                {
                    return false;
                }
            }
            return true;
        }
        bool bJumpToBoot = false;
        async Task<bool> JumpToBoot()
        {
            System.Diagnostics.Debug.WriteLine("JumpToBoot");
            bJumpToBoot = true;
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x00, 0x01, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];
            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int seconds = 0;
            while (!bResponseHandShake)
            {
                await Task.Delay(100);
                seconds++;
                if (seconds > 10)
                {


                    return false;
                }
            }
            return true;

        }

        public void ResponseFromBootLoader(byte[] b)
        {
            if (b.Length < 6)
                return;
            if (b[0] == 0xaa)
                return;
#if CONSOLE_WRITE
            int iReturnbyteCount = 0;
            foreach (byte mybyte in b)
            {
                System.Diagnostics.Debug.WriteLine("byte " + iReturnbyteCount.ToString("X") + " mybyte = " + mybyte.ToString("X"));
                iReturnbyteCount++;
            }
#endif
            //     Task.Delay(50).Wait();


            switch (b[5])
            {
                case 1:
                    if (bUnlockJumpToBoot)
                    {
                        if ((b[4] == 0x80) && (b[8] == 0x01))
                        {
                            System.Diagnostics.Debug.WriteLine("Unlock Jump To Boot Success");
                            bResponseHandShake = true;
                        }
                        else
                            return;
                    }
                    else if (bJumpToBoot)
                    {
                        if (b[4] == 0x80)
                            return;
                        bResponseHandShake = true;

                    }
                    else
                    {
                        if (b[9] != 6) // boot loader version 
                        {
                            WriteProgressLabel("Boot loader version not found");
                            return;
                        }
                        UInt16 DeviceID = (UInt16)(b[12] << 8);
                        DeviceID |= (UInt16)b[11];
                        if (DeviceID != 0x281)
                        {
                            WriteProgressLabel("Boot loader Device ID not found");
                            return;
                        }
                        UInt16 ProcessID = (UInt16)(b[14] << 8);
                        ProcessID |= (UInt16)b[13];
                        if (ProcessID != 0x0001)
                        {
                            WriteProgressLabel("Boot loader Process ID not found");
                            return;

                        }
                        WriteProgressLabel("Boot loader Init Success");
                        bResponseHandShake = true;
                    }
                    break;
                case 0x11:
                    bResponseHandShake = true;
                    break;
                case 0x04:
                    bResponseHandShake = true;
                    break;
                case 0x05:
                    if ((b[4] & 0x80) == 0x80)
                    {
                        UInt16 Error = (UInt16)(b[14] << 8);
                        Error |= b[13];
                        if ((Error & 0x0001) == 0x0001)
                        {
                            System.Diagnostics.Debug.WriteLine("Burn Address In Boot Space");
                            WriteProgressLabel("Burn Address In Boot Space");
                        }
                        if ((Error & 0x0002) == 0x0002)
                        {
                            System.Diagnostics.Debug.WriteLine("PM Burn Address Out Of Range");
                            WriteProgressLabel("PM Burn Address Out Of Range");
                        }
                        if ((Error & 0x0004) == 0x0004)
                        {
                            System.Diagnostics.Debug.WriteLine("Burn Address Not On Boundary");
                            WriteProgressLabel("Burn Address Not On Boundary");
                        }
                        if ((Error & 0x0020) == 0x0020)
                        {
                            System.Diagnostics.Debug.WriteLine("Burn Checksum Error");
                            WriteProgressLabel("Burn Checksum Error");
                        }
                        return;
                    }

                    bResponseHandShake = true;
                    break;
                case 0x0C:
                    if ((b[4] & 0x80) == 0x80)
                    {
                        UInt16 Error = (UInt16)(b[14] << 8);
                        Error |= b[13];
                        if ((Error & 0x0100) == 0x0100)
                        {
                            System.Diagnostics.Debug.WriteLine("Build CM Page Attempted To Write Protected Bits");
                            WriteProgressLabel("Build CM Page Attempted To Write Protected Bits");
                        }
                        if ((Error & 0x0080) == 0x0080)
                        {
                            System.Diagnostics.Debug.WriteLine("Build CM Page Attempted To Write A Non-XT Mode FPR Bits To FOSC Config Register");
                            WriteProgressLabel("Build CM Page Attempted To Write A Non-XT Mode FPR Bits To FOSC Config Register");
                        }
                        return;
                    }
                    bResponseHandShake = true;

                    break;
                case 0x0D:
                    if ((b[4] & 0x80) == 0x80)
                    {
                        UInt16 Error = (UInt16)(b[14] << 8);
                        Error |= b[13];
                        if ((Error & 0x0002) == 0x0002)
                        {
                            System.Diagnostics.Debug.WriteLine("CM Burn Address Out Of Range");
                            WriteProgressLabel("CM Burn Address Out Of Range");
                        }
                        if ((Error & 0x0020) == 0x0020)
                        {
                            System.Diagnostics.Debug.WriteLine("CM Burn Bad Checksum");
                            WriteProgressLabel("CM Burn Bad Checksum");
                        }
                        return;
                    }
                    bResponseHandShake = true;
                    break;
                case 0x14:
                    if ((b[4] & 0x80) == 0x80)
                        if ((b[4] & 0x80) == 0x80)
                        {
                            UInt16 Error = (UInt16)(b[14] << 8);
                            Error |= b[13];
                            if ((Error & 0x0001) == 0x0001)
                            {
                                System.Diagnostics.Debug.WriteLine("Burn Address In Boot Space");
                                WriteProgressLabel("Burn Address In Boot Space");
                            }
                            if ((Error & 0x0002) == 0x0002)
                            {
                                System.Diagnostics.Debug.WriteLine("PM Burn Address Out Of Range");
                                WriteProgressLabel("PM Burn Address Out Of Range");
                            }
                            if ((Error & 0x0004) == 0x0004)
                            {
                                System.Diagnostics.Debug.WriteLine("Burn Address Not On Boundary");
                                WriteProgressLabel("Burn Address Not On Boundary");
                            }
                            if ((Error & 0x0020) == 0x0020)
                            {
                                System.Diagnostics.Debug.WriteLine("Burn Checksum Error");
                                WriteProgressLabel("Burn Checksum Error");
                            }
                            return;
                        }

                    bResponseHandShake = true;
                    break;

            }
            //#endif
        }
        bool BlockFromEntering = false;

        string strModelNameAndVersion;
        
        public void OnButtonClicked(object sender, EventArgs args)
        {
            string strwarning = null;
            string strModelNameAndVersion = "";
            //string[] FileNameItems;
            if (!BlockFromEntering)
            {
                BlockFromEntering = true;
                Button button = (Button)sender;
                
                if ((args as EventArgs<string[]>) != null)
                {
                    //FileNameItems = (args as EventArgs<string[]>).Value;
                    strFileName = (args as EventArgs<string[]>).Value[0];
                    strModelNameAndVersion = (args as EventArgs<string[]>).Value[1];
                }

                strwarning = "Are you sure you want to download " + strModelNameAndVersion;
            }
            // if it is in forward or reverse driving state, cannot do firmware update
            var temp = (System.Int16)App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue;

            if (((temp  == 1) || (temp == 2) || (temp == 9)) || AlreadyTalkingToBootloader)//((SVAR_MACHINE_STATE == STATE_NEUTRAL) || (SVAR_MACHINE_STATE == STATE_RESET) || (SVAR_MACHINE_STATE == STATE_CART_PULL_IN_MAIN)) allowed toupdate firmware
            {
                Task<bool> task = DisplayAlert("Firmware Download", strwarning, "Yes", "Cancel");

                task.ContinueWith(AlertDismissedCallback);
            }
            else
            {
                DisplayAlert("VEHICLE IS ENABLED", "Ensure vehicle is stopped, place in neutral and set parking brake before continuing", "Back to Downloads");
            }
        }

        string[] fileList = FileManager.GetNavitasDirectoryFiles();
        int childPositionCounter = 1;
        void InsertFileList()
        {
            try
            {
                //checked for public NavitasBeta directory xml file
                StackLayout latestUpdatesStackLayout = new StackLayout();
                bool isLatestUpdatesStackLayoutCreated = false;
                bool isFirmwareScreenExtensionFound = false;
                InitialSC.CurrentRating filterController;
                string extensionFilter = "";
                FirmwaresGroups releaseFirmwareFileNames = null;
                Stream streamToRead = null;

                //TSX firmware download filter
                if ((App.AppConfigurationLevel == "DEALER" || App.AppConfigurationLevel == "ADVANCED_USER") && !(App._MainFlyoutPage._DeviceListPage._device is DemoDevice) && !AlreadyTalkingToBootloader)
                {
                    if (App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue == 10.0) // TSX600
                    {
                        extensionFilter = "600";
                        filterController = InitialSC.CurrentRating.TSX600A;
                    }
                    else if (App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue == 12.0) // TSX440
                    {
                        extensionFilter = "440";
                        filterController = InitialSC.CurrentRating.TSX440A;
                    }

                }
                else
                    extensionFilter = "hex";


                //FileButtonList.Children[1].IsVisible = false; //this is the "Latest Updates" label, it will be made visible if there are any
                if (fileList != null)
                {
                    bool fileNeedsToBeAdded = true;
                    //find .TSXFirmwareScreen only
                    foreach (var externalFile in fileList)
                    {
                        if (fileNeedsToBeAdded && externalFile.Contains(".TSXFirmwareScreen"))
                        {
                            var firmwareFilename = Regex.Match(externalFile, @"([^\/]+$)", RegexOptions.IgnoreCase).Value;
                            streamToRead = FileManager.GetExternalFileStream(firmwareFilename);
                            isFirmwareScreenExtensionFound = true;
                            break;
                            break;
                        }
                    }

                    if (!isFirmwareScreenExtensionFound)
                    {
                        foreach (var publicFilePath in fileList)
                        {
                            //foreach (var button in listOfFrames.Where(x => (x as VisualElement).GetType() == typeof(Button)))
                            //{
                            //    if ((button as Button).Text.Contains(fileName))
                            //        fileNeedsToBeAdded = false;
                            //}

                            if (fileNeedsToBeAdded && publicFilePath.Contains("TSX") && (publicFilePath.Contains("." + extensionFilter) ||
                                publicFilePath.Contains(".TSX" + extensionFilter)) && (publicFilePath.Contains(".hex") ||
                                publicFilePath.Contains(".TSXhex")))
                            {
                                //Create Latest Updates Stacklayout
                                if (!isLatestUpdatesStackLayoutCreated)
                                {
                                    isLatestUpdatesStackLayoutCreated = true;
                                    latestUpdatesStackLayout.Children.Add(BuildGroupTitle("Latest Updates", groupTitleHexColor));


                                    if (streamToRead == null)
                                    {
                                        streamToRead = FileManager.GetInternalFileStream($"NavitasBeta.firmware.TSX.Original.TSXFirmwareScreen");
                                        releaseFirmwareFileNames = (FirmwaresGroups)(new XmlSerializer(typeof(FirmwaresGroups))).Deserialize(streamToRead);
                                        streamToRead.Dispose();
                                    }
                                }

                                string publicFileName = Path.GetFileName(publicFilePath);

                                // TODO: RegEx shoud be gathered in one place
                                Match match = Regex.Match(publicFileName, @"(?![P.\d_]+).+(?=_Rev)", RegexOptions.IgnoreCase);
                                Regex PriorityNumberExtractor = new Regex(@"^(P\d+(\.\d+)?)_");
                                
                                if (match.Success)
                                {
                                    //Remove letter "A" if it match regex
                                    //due to new naming convention does not include letter "A"
                                    var fileNameWithUnderscore = Regex.Replace(match.Value, @"([0-9]{3})[A]", "$1");

                                    string friendlyFileName = "";
                                    for (int i = 0; i < releaseFirmwareFileNames.FirmwareGroups.Count; i++)
                                    {
                                        friendlyFileName = releaseFirmwareFileNames.FirmwareGroups[i]
                                                                                   .FileItems
                                                                                   .FirstOrDefault(fileItem => fileItem.ActualFileName.Contains(fileNameWithUnderscore))
                                                                                   ?.FriendlyFileName ?? "404";
                                        //404 is a not found code
                                        if (friendlyFileName != "404")
                                            break;
                                    }

                                    if (friendlyFileName != "404")
                                    {
                                        var friendlyFileNameAndVersion = friendlyFileName + " " +
                                                                        Regex.Match(publicFileName, @"(?<=Rev_).+(?=\.\d+\.(hex|TSXhex))", RegexOptions.IgnoreCase).Value;

                                        latestUpdatesStackLayout.Children.Add(BuildFrameAndButton(friendlyFileNameAndVersion, friendlyFileName, publicFilePath));
                                    }
                                    else
                                    {
                                        publicFileName = PriorityNumberExtractor.Replace(publicFileName, "");
                                        latestUpdatesStackLayout.Children.Add(BuildFrameAndButton(publicFileName, publicFileName, publicFilePath));
                                    }
                                }
                            }
                        }
                        FileButtonList.Children.Insert(childPositionCounter, latestUpdatesStackLayout);
                        childPositionCounter++;
                    }
                }

                //if there is no public xml file, go check the embedded one (internal)
                if (streamToRead == null)
                    streamToRead = FileManager.GetInternalFileStream($"NavitasBeta.firmware.TSX.Original.TSXFirmwareScreen");

                //if first value returns null, then it will instantiate a new object
                releaseFirmwareFileNames = releaseFirmwareFileNames ?? (FirmwaresGroups)(new XmlSerializer(typeof(FirmwaresGroups))).Deserialize(streamToRead);

                streamToRead.Dispose();

                bool isFullListRequired = false;

                if (!isFirmwareScreenExtensionFound)
                {
                    //Show the entire list except OEM's firmware when there is a mismatch issue or firmware not found
                    if (App._MainFlyoutPage._DeviceListPage._device is DemoDevice || AlreadyTalkingToBootloader || currentSupplierChain == null)
                        isFullListRequired = true;
                    ContinueBuildingFileButtonList(releaseFirmwareFileNames.FirmwareGroups,
                                                        FileButtonList,
                                                        childPositionCounter,
                                                        isFullListRequired);
                }
                else //Neb stuff goes here
                {
                    ContinueBuildingFileButtonList(releaseFirmwareFileNames.FirmwareGroups,
                                                    FileButtonList,
                                                    childPositionCounter,
                                                    isFullListRequired,
                                                    true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Insert Files TSX Exception: " + ex.Message);
            }
        }

        async void AlertDismissedCallback(Task<bool> task)
        {
            System.Diagnostics.Debug.WriteLine("task.Result = " + task.Result);
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                BlockFromEntering = true;
                await Task.Factory.StartNew(Download);
                BlockFromEntering = false;
            }
            else
            {
                BlockFromEntering = false;
            }
        }

        bool bResponseHandShake = false;
        async Task<bool> InitBootLoaderMode()
        {
            System.Diagnostics.Debug.WriteLine("InitBootLoaderMode");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x01, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];
            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int seconds = 0;
            while (!bResponseHandShake)
            {
                await Task.Delay(100);
                seconds++;
                if (seconds > 10)
                {


                    return false;
                }
            }
            return true;

        }

        struct PMBURN
        {
            public uint DestinationAddress;
            public byte[] bytes;
        }
        string strFileName;// = "NavitasBeta.firmware.SEM.hex";
        float SavedArmatureCurrentScaleFactor;
        float SavedFieldCurrentScaleFactor;
        float SavedCalibrationDateYear;
        float SavedCalibrationDateDayMonth;
        float SavedSerialNumberUnit;
        float SaveSerialNumberDate;
        float SavedAbsoluteArmatureCurrent;
        float SavedControllerModelNumber;
        float SavedMaxSpeedForward;
        float SavedMaxSpeedReverse;
        
        async void Download()
        {
            // To Do 
            // Get the calib parameters we want to restore after updating firmware. 
            System.Diagnostics.Debug.WriteLine("Inside Dowload " + AlreadyTalkingToBootloader.ToString());
            if (!AlreadyTalkingToBootloader)
            {
                SavedArmatureCurrentScaleFactor = App.ViewModelLocator.GetParameterTSX("PARARMATURECURRENTSCALEFACTOR").parameterValue;
                SavedFieldCurrentScaleFactor = App.ViewModelLocator.GetParameterTSX("PARFIELDCURRENTSCALEFACTOR").parameterValue;
                SavedCalibrationDateYear = App.ViewModelLocator.GetParameterTSX("PARCALIBRATIONDATEYEAR").parameterValue;
                SavedCalibrationDateDayMonth = (float)App.ViewModelLocator.GetParameterTSX("PARCALIBRATIONDATEDAYMONTH").parameterValue;
                SavedSerialNumberUnit = (float)App.ViewModelLocator.GetParameterTSX("SERIALNUMBERLOW").parameterValue;
                SaveSerialNumberDate = (float)App.ViewModelLocator.GetParameterTSX("PARSERIALNUMBERDATE").parameterValue;
                SavedAbsoluteArmatureCurrent = App.ViewModelLocator.GetParameterTSX("PARABSOLUTEARMATURECURRENT").parameterValue;
                SavedControllerModelNumber = (float)App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue;
                SavedMaxSpeedForward = App.ViewModelLocator.GetParameterTSX("PARMAXSPEEDFORWARD").parameterValue;
                SavedMaxSpeedReverse = App.ViewModelLocator.GetParameterTSX("PARMAXSPEEDREVERSE").parameterValue;
            }
#if OLD_SAVE
            float SavedTireRadius = (float)App.ViewModelLocator.GetParameterTSX("TIREDIAMETER;
            float SavedAccessoryEnable = (float)App.ViewModelLocator.GetParameterTSX("PAR_ACCESSORY_ENABLE;
            float SavedRearAxleratio = (float)App.ViewModelLocator.GetParameterTSX("REARAXLERATIO;
            float SavedThrottleMin = App.ViewModelLocator.GetParameterTSX("PAR_PRIMARY_THROTTLE_FORWARD_MIN;
            float SavedThrottleAccelerationX = (float)App.ViewModelLocator.GetParameterTSX("PAR_PRIMARY_THROTTLE_FORWARD_ACCELERATIO_X;
            float SavedThrottleAccelerationY = (float)App.ViewModelLocator.GetParameterTSX("PAR_PRIMARY_THROTTLE_FORWARD_ACCELERATIO_Y;
            float SavedThrottleMax = (float)App.ViewModelLocator.GetParameterTSX("PAR_PRIMARY_THROTTLE_FORWARD_MAX;
            float SavedArmatureCurrentMin = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_ARMATURE_CURRENT_MIN;
            float SavedArmatureCurrentMax = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_ARMATURE_CURRENT_MAX;
            float SavedFieldCurrentMin = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_FIELD_CURRENT_MIN;
            float SavedFieldCurrentMax = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_FIELD_CURRENT_MAX;
            float SavedOverdriveArmatureCurrent = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_ARMATURE_CURRENT_MID;
            float SavedOverdriveFieldCurrent = (float)App.ViewModelLocator.GetParameterTSX("PAR_FORWARD_FIELD_CURRENT_MID;
#endif
            System.Diagnostics.Debug.WriteLine("Enabling Jump To Boot");
            WriteProgressLabel("Enabling Jump To Boot");
            Task<bool> boottask = EnableJumpToBoot();
            if (boottask.Result == false)
            {
                ClearQueue(null, null);  // if we are in boot already then we don't want this to execute when we wake up after a successful download 
                bUnlockJumpToBoot = false;
                System.Diagnostics.Debug.WriteLine("Can't Unlock Jump to Boot");
                WriteProgressLabel("Can't Unlock Jump to Boot");
            }
            bUnlockJumpToBoot = false;
            System.Diagnostics.Debug.WriteLine("Jumping to Boot");
            WriteProgressLabel("Jumping to Boot");
            boottask = JumpToBoot();
            if (boottask.Result == false)
            {
                bJumpToBoot = false;
                System.Diagnostics.Debug.WriteLine("Can't Jump to Boot");
                WriteProgressLabel("Can't Jump to Boot");
            }
            bJumpToBoot = false;
            System.Diagnostics.Debug.WriteLine("Initializing Bootloader");
            WriteProgressLabel("Initializing Bootloader");
            boottask = InitBootLoaderMode();
            if (boottask.Result == false)
            {
                System.Diagnostics.Debug.WriteLine("Can't Initialize Bootloader");
                WriteProgressLabel("Can't Initialize Bootloader");
                AlreadyTalkingToBootloader = false;
                ProgrammingDone(null, null);
                return;
            }


            System.Diagnostics.Debug.WriteLine("Download");
            List<PMBURN> pmburns = new List<PMBURN>();
            char[] LineConvertedToCharArray;
            bool bFirstTime = true;
            uint DestAddr = 0;
            uint[] BlockBeginningAddress = new uint[10];
            uint[] BlockEndingAddress = new uint[10];
            byte[] ConfigReg0 = new byte[2];
            byte[] ConfigReg1 = new byte[2];
            byte[] ConfigReg2 = new byte[2];
            uint BeginningAddress = 0;
            uint EndingAddress = 0;
            bool bNewHexRecordAddress = false;
            bool bIsAddressValid = false;
            PMBURN pmburn = new PMBURN();
            pmburn.bytes = new byte[96];
            int DataIndex = 0;
            uint AddressToLookFor = 0;
            uint NumberOfPasses = 0;
            uint UpperAddress = 0;
            bool bDoneProcessingFile = true;
            // byte bsubpage = 0;

            

            Stream stream = FileManager.GetInternalFirstOrExternalFileStream(strFileName);
            StreamReader reader = new System.IO.StreamReader(stream);
            float dFileLength = (float)reader.BaseStream.Length;
            WriteProgressLabel("Processing file Please Wait ");

            string strWholeFile = reader.ReadToEnd();
            string[] filelines = strWholeFile.Split(':');
            float Progress = 0;
            float ProgressOld = 0;
            UpdateProgressBar(0.0f);
            for (int Index = 1; Index < filelines.Length; Index++)
            {
                //         Progress = ((float)((float)Index / (float)(filelines.Length)));            
                //             if (Progress - ProgressOld >= 0.01)
                //            {
                //      System.Diagnostics.Debug.WriteLine("Progress = " + Progress.ToString());
                //               UpdateProgressBar(Progress);
                //                ProgressOld = Progress;
                //              }
                LineConvertedToCharArray = filelines[Index].ToCharArray();
                if (LineConvertedToCharArray[7] == '4')
                {

                    UpperAddress = (UInt32)Convert.ToUInt16(new string(LineConvertedToCharArray, 8, 4), 16) << 16;
                    //      bNewHexRecordAddress = true;
                }
                if (LineConvertedToCharArray[7] == '1')
                {
                    if (DataIndex != 0)  // if the block doesn't end on and even boundary. 
                    {
                        DataIndex = 0;
                        pmburns.Add(pmburn);
                        bNewHexRecordAddress = true;
                    }
                    BlockBeginningAddress[NumberOfPasses] = BeginningAddress;
                    BlockEndingAddress[NumberOfPasses] = EndingAddress;
                    NumberOfPasses++;
                    Index = 0;
                    bFirstTime = true;
                    if (bDoneProcessingFile == true)  // This means no more blocks. 
                        break;

                    bDoneProcessingFile = true;
                }
                if (LineConvertedToCharArray[7] == '0')
                {
                    DestAddr = UpperAddress;
                    DestAddr |= Convert.ToUInt16(new string(LineConvertedToCharArray, 2, 4), 16);
                    DestAddr /= 2;
                    for (int i = 0; i < NumberOfPasses; i++)
                    {
                        if ((DestAddr >= BlockBeginningAddress[i]) && (DestAddr <= BlockEndingAddress[i]))
                        {
                            DestAddr = 0xFFFFFF;
                        }

                    }
                    switch (DestAddr)
                    {
                        case 0xf80000:
                            ConfigReg0[0] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 0 * 2, 2), 16);
                            ConfigReg0[1] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 1 * 2, 2), 16);
                            break;
                        case 0xf80002:
                            ConfigReg1[0] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 0 * 2, 2), 16);
                            ConfigReg1[1] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 1 * 2, 2), 16);
                            break;
                        case 0xf80004:
                            ConfigReg2[0] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 0 * 2, 2), 16);
                            ConfigReg2[1] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + 1 * 2, 2), 16);
                            break;
                        case 0xF8000A:
                            break;
                        case 0x7FF000:
                            break;
                        case 0xFFFFFF:
                            break;
                        default:
                            bDoneProcessingFile = false;
                            if (bFirstTime == true)
                            {
                                AddressToLookFor = DestAddr;
                                //  bNewHexRecordAddress = true;
                            }
                            if (DestAddr == AddressToLookFor)
                            {
                                if (bFirstTime)
                                {
                                    BeginningAddress = DestAddr;
                                    bFirstTime = false;
                                }
                                else
                                {
                                    EndingAddress = DestAddr;
                                }

                                bIsAddressValid = true;
                            }

                            if (bIsAddressValid)
                            {
                                if (bNewHexRecordAddress == true)
                                {
                                    if ((DestAddr % (32 * 2)) == 0)
                                    {
                                        pmburn = new PMBURN();
                                        pmburn.DestinationAddress = DestAddr;
                                        pmburn.bytes = new byte[96];
                                        for (int h = 0; h < pmburn.bytes.Length; h++)
                                        {
                                            pmburn.bytes[h] = 0xff;
                                        }
                                        bIsAddressValid = true;
                                        DataIndex = 0;
                                        bNewHexRecordAddress = false;
                                    }
                                }

                                for (int i = 1; i < (Convert.ToByte(new string(LineConvertedToCharArray, 0, 2), 16) + 1); i++)
                                {
                                    //      System.Diagnostics.Debug.WriteLine("i = " + i.ToString());
                                    if (DataIndex == 96)
                                    {
                                        DataIndex = 0;
                                        pmburns.Add(pmburn);
                                        //    bNewHexRecordAddress = true;
                                        DestAddr += (uint)i / 2;
                                        if ((DestAddr % (32 * 2)) == 0)
                                        {
                                            pmburn = new PMBURN();
                                            pmburn.DestinationAddress = DestAddr;
                                            pmburn.bytes = new byte[96];
                                            for (int h = 0; h < pmburn.bytes.Length; h++)
                                            {
                                                pmburn.bytes[h] = 0xff;
                                            }
                                            bNewHexRecordAddress = false;
                                        }

                                    }
                                    if (!((i % 4) == 0))
                                    {
                                        if (pmburn.DestinationAddress == 0x00000000 && (DataIndex == 0))  // Always jump to the bootloader and it will load the app
                                            pmburn.bytes[DataIndex] = 0X00;
                                        else if (pmburn.DestinationAddress == 0x00000000 && (DataIndex == 1))
                                            pmburn.bytes[DataIndex] = 0X01;
                                        else
                                            pmburn.bytes[DataIndex] = Convert.ToByte(new string(LineConvertedToCharArray, 8 + (i - 1) * 2, 2), 16);
                                        DataIndex++;
                                    }
                                }
                                bIsAddressValid = false;
                                AddressToLookFor = pmburn.DestinationAddress;
                                AddressToLookFor += (uint)(DataIndex / 3) * 2;
                                if (DataIndex == 96)
                                {
                                    DataIndex = 0;
                                    pmburns.Add(pmburn);
                                    bNewHexRecordAddress = true;
                                }

                            }
                            break;
                    }
                }
            }
#if WRITE_FLASH_OLD_SLOW_WAY_WAS_WORKING
            WriteProgressLabel("Writing Flash Please Wait ");
            for (int Index = 0; Index < pmburns.Count; Index++)
            {
                UpdateProgressBar(((float)((float)Index / (float)(pmburns.Count))));
                calculatedchecksum = 0;
                for (int i = 0; i < pmburns[Index].bytes.Length; i+=6)
                {
                    if (PopulatePMSubPage(bsubpage, pmburns[Index].bytes[i], pmburns[Index].bytes[i + 1], pmburns[Index].bytes[i + 2], pmburns[Index].bytes[i + 3], pmburns[Index].bytes[i + 4], pmburns[Index].bytes[i + 5]) == false)
                        return;
                    bsubpage++;
                }
                if (bsubpage == 16)
                {
                    bsubpage = 0;
                    calculatedchecksum &= 0x0000ffff;
                    calculatedchecksum = ~calculatedchecksum + 1;
                    BurnPM((byte)(pmburns[Index].DestinationAddress), (byte)(pmburns[Index].DestinationAddress >> 8), (byte)(pmburns[Index].DestinationAddress >> 16), (byte)(calculatedchecksum), (byte)(calculatedchecksum >> 8));
                }

            }
#endif
            WriteProgressLabel("Writing Flash Please Wait ");
            Progress = 0;
            ProgressOld = 0;
            int Retries = 20;


            //int IndexOfAddressZero = 0;
            for (int Index = 0; Index < pmburns.Count; Index++)
            {
                Progress = (float)((float)Index / (float)(pmburns.Count));
                //System.Diagnostics.Debug.WriteLine("Progress = " + Progress.ToString());
                if (Progress - ProgressOld >= 0.01)
                {
                    //      System.Diagnostics.Debug.WriteLine("Progress = " + Progress.ToString());
                    UpdateProgressBar(Progress);
                    ProgressOld = Progress;
                }



                //       if((pmburns[Index].DestinationAddress >= 0x002180) && (pmburns[Index].DestinationAddress <= 0x17FFE))
                //      {
                Task<bool> task = PopulatePMPageAndBurn((byte)(pmburns[Index].DestinationAddress), (byte)(pmburns[Index].DestinationAddress >> 8), (byte)(pmburns[Index].DestinationAddress >> 16), pmburns[Index].bytes);
                if (task.Result == false)
                {
                    if (Retries == 0)
                    {
                        return;
                    }
                    else
                    {
                        WriteProgressLabel("Retries = " + (20 - Retries).ToString());
                        System.Diagnostics.Debug.WriteLine("Retries = " + Retries.ToString());
                        Retries--;
                        Index--;
                        await Task.Delay(4000);
                    }

                }
                else
                {
                    if (Retries != 20)
                        WriteProgressLabel("Writing Flash Please Wait ");

                    Retries = 20;
                }
                //      }
                //         if (PopulatePMPageAndBurn((byte)(pmburns[Index].DestinationAddress), (byte)(pmburns[Index].DestinationAddress >> 8), (byte)(pmburns[Index].DestinationAddress >> 16), pmburns[Index].bytes) == false)
                //          return;
                //      Task.Delay(50).Wait();
            }
#if ADDRESS_ZERO
            Retries = 20;
            while(Retries != 0)
            { 
                Task<bool> bootaddresstask = PopulatePMPageAndBurn((byte)(pmburns[IndexOfAddressZero].DestinationAddress), (byte)(pmburns[IndexOfAddressZero].DestinationAddress >> 8), (byte)(pmburns[IndexOfAddressZero].DestinationAddress >> 16), pmburns[IndexOfAddressZero].bytes);
                if (bootaddresstask.Result == false)
                {
                        WriteProgressLabel("Boot Address Retries = " + (20 - Retries).ToString());
                        System.Diagnostics.Debug.WriteLine("Boot Address Retries = " + Retries.ToString());
                        Retries--;
                   
                        await Task.Delay(4000);

                }
                else
                {
                        WriteProgressLabel("Writing Flash Please Wait ");
                    break;

                }
            }
#endif
            calculatedchecksum = 0;
            Task<bool> t = this.BuildCMSubPage(ConfigReg0[0], ConfigReg0[1], ConfigReg1[0], ConfigReg1[1], ConfigReg2[0], ConfigReg2[1]);
            if (t.Result == false)
                return;
            calculatedchecksum &= 0x0000ffff;
            calculatedchecksum = ~calculatedchecksum + 1;
            BurnCM((byte)(calculatedchecksum), (byte)(calculatedchecksum >> 8));
            //          if (t.Result == false)
            //           return;
            WriteProgressLabel("Rebooting, Please Wait...");
            await Task.Delay(1000);
            AlreadyTalkingToBootloader = false;
            ProgrammingDone(null, null);

            await Task.Delay(4000);
            WriteProgressLabel("Restoring Calibration Data, Please Wait...(" + SavedArmatureCurrentScaleFactor.ToString() + ")");
            //Device.BeginInvokeOnMainThread(() => popup is not working
            //{
            //	activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            //	activityMessage.Text = "Saving...";
            //});
            if (!AlreadyTalkingToBootloader)
            {
                if (SavedArmatureCurrentScaleFactor != 0)
                    QueParameter(new SetParameterEventArgs(188, (float)GetRawValue(0, 65535, 0, 65535, SavedArmatureCurrentScaleFactor), null));
                if (SavedFieldCurrentScaleFactor != 0)
                    QueParameter(new SetParameterEventArgs(189, (float)GetRawValue(0, 65535, 0, 65535, SavedFieldCurrentScaleFactor), null));
                if (SavedAbsoluteArmatureCurrent != 0)
                    QueParameter(new SetParameterEventArgs(187, (float)GetRawValue(0, 660, 0, 2047, SavedAbsoluteArmatureCurrent), null));
                QueParameter(new SetParameterEventArgs(192, (float)GetRawValue(2000, 9999, 2000, 9999, SavedCalibrationDateYear), null));
                QueParameter(new SetParameterEventArgs(191, SavedCalibrationDateDayMonth, null));
                QueParameter(new SetParameterEventArgs(194, SavedSerialNumberUnit, null));
                QueParameter(new SetParameterEventArgs(193, SaveSerialNumberDate, null));
                if (SavedMaxSpeedForward != 0)
                    QueParameter(new SetParameterEventArgs(46, SavedMaxSpeedForward, null));
                if (SavedMaxSpeedReverse != 0) 
                    QueParameter(new SetParameterEventArgs(45, SavedMaxSpeedReverse, null));
                QueParameter(new SetParameterEventArgs(190, (float)GetRawValue(1, 12, 1, 12, SavedControllerModelNumber), null));
                QueParameter(new SetParameterEventArgs(199, 1.0f, null));
                System.Diagnostics.Debug.WriteLine("Save finishished");
                await Task.Delay(3000); //give the save command time to start executing then watch pageisbusy
                                        //A little crazy but the device communications sets PageIsBusy when save command is issued
                                        //and only whatever tab page was active (this navigated to page does not seem to register its own PageIsBusy)
                                        //only way to know that we are still saving
                while (((App._MainFlyoutPage.userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy))
                    await Task.Delay(100);
            }

            WriteProgressLabel("Programming Done");
            RemoveProgressLabelPopUp();
            if (!AlreadyTalkingToBootloader)
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.DisplayAlert("Update", "Programming Done", "OK");
                });
        }


        float GetRawValue(float userFrom, float userTo, float digitalFrom, float digitalTo, float DisplayValue)
        {
            float percentage = (DisplayValue - userFrom) / (userTo - userFrom);
            return (float)Math.Round(percentage * (digitalTo - digitalFrom) + digitalFrom, 0);
        }

        async Task<bool> BuildCMSubPage(byte byte0, byte byte1, byte byte2, byte byte3, byte byte4, byte byte5)
        {
            System.Diagnostics.Debug.WriteLine("BuildCMSubPage");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x0c, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];

            calculatedchecksum += byte0;
            calculatedchecksum += byte1;
            calculatedchecksum += byte2;
            calculatedchecksum += byte3;
            calculatedchecksum += byte4;
            calculatedchecksum += byte5;


            packetfull[9] = byte0;
            packetfull[10] = byte1;
            packetfull[11] = byte2;
            packetfull[12] = byte3;
            packetfull[13] = byte4;
            packetfull[14] = byte5;


            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int seconds = 0;
            while (!bResponseHandShake)
            {
                await Task.Delay(100);
                seconds++;

                if (seconds > 10)
                {

                    System.Diagnostics.Debug.WriteLine("BuildCMSubPage Timeout");
                    WriteProgressLabel("BuildCMSubPage Timeout");
                    return false;
                }
            }
            return true;
        }


        bool PopulatePMSubPage(byte subpage, byte byte0, byte byte1, byte byte2, byte byte3, byte byte4, byte byte5)
        {
            System.Diagnostics.Debug.WriteLine("PopulatePMSubPage");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x11, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];

            calculatedchecksum += byte0;
            calculatedchecksum += byte1;
            calculatedchecksum += byte2;
            calculatedchecksum += byte3;
            calculatedchecksum += byte4;
            calculatedchecksum += byte5;

            packetfull[8] = subpage;
            packetfull[9] = byte0;
            packetfull[10] = byte1;
            packetfull[11] = byte2;
            packetfull[12] = byte3;
            packetfull[13] = byte4;
            packetfull[14] = byte5;


            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int previoustime = DateTime.Now.Second;

            while (!bResponseHandShake)
            {
                int timeout = DateTime.Now.Second - previoustime;
                if (timeout < 0)
                {
                    timeout += 60;
                }
                if (timeout > 20)
                {

                    System.Diagnostics.Debug.WriteLine("PopulatePMSubPage timeout ");
                    WriteProgressLabel("PopulatePMSubPage timeout");
                    return false;
                }
            }
            return true;
        }

        void BurnCM(byte CheckSumLow, byte CheckSumHigh)
        {
            System.Diagnostics.Debug.WriteLine("BurnCM");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x0d, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];



            packetfull[9] = 0x00;
            packetfull[10] = 0x00;
            packetfull[11] = 0xF8;

            packetfull[13] = CheckSumLow;
            packetfull[14] = CheckSumHigh;

            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));





        }


        async Task<bool> PopulatePMPageAndBurn(byte SourceAddressLow, byte SourceAddressMid, byte SourceAddressHigh, byte[] Data)
        {
            //     byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x05, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
#if CONSOLE_WRITE
            System.Diagnostics.Debug.WriteLine("PopulatePMPageAndBurn");
#endif
            byte[] checksum = new byte[2];
            byte[] packetfull = new byte[7 + 3 + 5 + 96];
            packetfull[0] = 0x02;
            packetfull[1] = 0x54;
            packetfull[2] = 0x53;
            packetfull[3] = 0x58;
            packetfull[4] = 0x01;
            packetfull[5] = 0x14;
            packetfull[6] = 101;
            packetfull[7] = SourceAddressLow;
            packetfull[8] = SourceAddressMid;
            packetfull[9] = SourceAddressHigh;

            calculatedchecksum = 0;

            for (int i = 0; i < Data.Length; i++)
            {
                packetfull[12 + i] = Data[i];
                calculatedchecksum += Data[i];
            }

            calculatedchecksum &= 0x0000ffff;
            calculatedchecksum = ~calculatedchecksum + 1;


            packetfull[10] = (byte)calculatedchecksum;
            packetfull[11] = (byte)(calculatedchecksum >> 8);



            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];
            packetfull[packetfull.Length - 1] = 0x03;



            WritePacket(this, new WritePacketEventArgs(packetfull));
            bResponseHandShake = false;

            int seconds = 0;
            while (!bResponseHandShake)
            {
                await Task.Delay(100);
                seconds++;

                if (seconds > 10)
                {

                    System.Diagnostics.Debug.WriteLine("Write Flash Timeout");
                    WriteProgressLabel("Write Flash Timeout");
                    return false;
                }
            }
            return true;

        }


        bool BurnPM(byte SourceAddressLow, byte SourceAddressMid, byte SourceAddressHigh, byte CheckSumLow, byte CheckSumHigh)
        {
            System.Diagnostics.Debug.WriteLine("BurnPM");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x05, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];



            packetfull[9] = SourceAddressLow;
            packetfull[10] = SourceAddressMid;
            packetfull[11] = SourceAddressHigh;

            packetfull[13] = CheckSumLow;
            packetfull[14] = CheckSumHigh;

            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int previoustime = DateTime.Now.Second;

            while (!bResponseHandShake)
            {

                int timeout = DateTime.Now.Second - previoustime;
                if (timeout < 0)
                {
                    timeout += 60;
                }
                if (timeout > 20)
                {

                    //     System.Diagnostics.Debug.WriteLine("BurnPM timeout ");
                    //   WriteProgressLabel("BurnPM timeout");
                    return false;
                }
            }
            return true;

        }

        bool BuildPMPage(byte SourceAddressLow, byte SourceAddressMid, byte SourceAddressHigh)
        {
            System.Diagnostics.Debug.WriteLine("BuildPMPage");
            byte[] packetfull = new byte[] { 0x02, 0x54, 0x53, 0x58, 0x01, 0x04, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xa3, 0x63, 0x03 };
            byte[] checksum = new byte[2];



            packetfull[9] = SourceAddressLow;
            packetfull[10] = SourceAddressMid;
            packetfull[11] = SourceAddressHigh;



            fletcher16(checksum, packetfull, packetfull.Length - 3);
            packetfull[packetfull.Length - 3] = checksum[0];
            packetfull[packetfull.Length - 2] = checksum[1];

            WritePacket(this, new WritePacketEventArgs(packetfull));

            bResponseHandShake = false;

            int previoustime = DateTime.Now.Second;

            while (!bResponseHandShake)
            {

                int timeout = DateTime.Now.Second - previoustime;
                if (timeout < 0)
                {
                    timeout += 60;
                }
                if (timeout > 20)
                {

                    System.Diagnostics.Debug.WriteLine("BuildPMPage timeout ");
                    WriteProgressLabel("BuildPMPage timeout");
                    return false;
                }
            }
            return true;

        }







        public void WriteProgressLabel(string message)
        {
#if CONSOLE_WRITE
            System.Diagnostics.Debug.WriteLine(message);
#endif
            Device.BeginInvokeOnMainThread(() =>
            {

                popupBootLoader.IsVisible = true;
                progressLabel.Text = message;

            });

        }

        public void RemoveProgressLabelPopUp()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                popupBootLoader.IsVisible = false;
                progressLabel.Text = "";

            });
        }
        void UpdateProgressBar(float PercentComplete)
        {
#if CONSOLE_WRITE
            System.Diagnostics.Debug.WriteLine("PercentComplete = {0}", PercentComplete.ToString());
#endif
            Device.BeginInvokeOnMainThread(() =>
            {


                progressBar.Progress = PercentComplete;

            });

        }

        private void fletcher16(byte[] checksum, byte[] data, int len)
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

        private async void BackbuttonClicked(object sender, EventArgs e)
        {
            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
            await Navigation.PopAsync();
        }
    }
}