using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace NavitasBeta
{
    public class ParameterFileHandler
    {

        UInt32[] crc32_table = new UInt32[] 
        {
                0x00000000, 0x04C11DB7, 0x09823B6E, 0x0D4326D9,
                0x130476DC, 0x17C56B6B, 0x1A864DB2, 0x1E475005,
                0x2608EDB8, 0x22C9F00F, 0x2F8AD6D6, 0x2B4BCB61,
                0x350C9B64, 0x31CD86D3, 0x3C8EA00A, 0x384FBDBD,
                0x4C11DB70, 0x48D0C6C7, 0x4593E01E, 0x4152FDA9,
                0x5F15ADAC, 0x5BD4B01B, 0x569796C2, 0x52568B75,
                0x6A1936C8, 0x6ED82B7F, 0x639B0DA6, 0x675A1011,
                0x791D4014, 0x7DDC5DA3, 0x709F7B7A, 0x745E66CD,
                0x9823B6E0, 0x9CE2AB57, 0x91A18D8E, 0x95609039,
                0x8B27C03C, 0x8FE6DD8B, 0x82A5FB52, 0x8664E6E5,
                0xBE2B5B58, 0xBAEA46EF, 0xB7A96036, 0xB3687D81,
                0xAD2F2D84, 0xA9EE3033, 0xA4AD16EA, 0xA06C0B5D,
                0xD4326D90, 0xD0F37027, 0xDDB056FE, 0xD9714B49,
                0xC7361B4C, 0xC3F706FB, 0xCEB42022, 0xCA753D95,
                0xF23A8028, 0xF6FB9D9F, 0xFBB8BB46, 0xFF79A6F1,
                0xE13EF6F4, 0xE5FFEB43, 0xE8BCCD9A, 0xEC7DD02D,
                0x34867077, 0x30476DC0, 0x3D044B19, 0x39C556AE,
                0x278206AB, 0x23431B1C, 0x2E003DC5, 0x2AC12072,
                0x128E9DCF, 0x164F8078, 0x1B0CA6A1, 0x1FCDBB16,
                0x018AEB13, 0x054BF6A4, 0x0808D07D, 0x0CC9CDCA,
                0x7897AB07, 0x7C56B6B0, 0x71159069, 0x75D48DDE,
                0x6B93DDDB, 0x6F52C06C, 0x6211E6B5, 0x66D0FB02,
                0x5E9F46BF, 0x5A5E5B08, 0x571D7DD1, 0x53DC6066,
                0x4D9B3063, 0x495A2DD4, 0x44190B0D, 0x40D816BA,
                0xACA5C697, 0xA864DB20, 0xA527FDF9, 0xA1E6E04E,
                0xBFA1B04B, 0xBB60ADFC, 0xB6238B25, 0xB2E29692,
                0x8AAD2B2F, 0x8E6C3698, 0x832F1041, 0x87EE0DF6,
                0x99A95DF3, 0x9D684044, 0x902B669D, 0x94EA7B2A,
                0xE0B41DE7, 0xE4750050, 0xE9362689, 0xEDF73B3E,
                0xF3B06B3B, 0xF771768C, 0xFA325055, 0xFEF34DE2,
                0xC6BCF05F, 0xC27DEDE8, 0xCF3ECB31, 0xCBFFD686,
                0xD5B88683, 0xD1799B34, 0xDC3ABDED, 0xD8FBA05A,
                0x690CE0EE, 0x6DCDFD59, 0x608EDB80, 0x644FC637,
                0x7A089632, 0x7EC98B85, 0x738AAD5C, 0x774BB0EB,
                0x4F040D56, 0x4BC510E1, 0x46863638, 0x42472B8F,
                0x5C007B8A, 0x58C1663D, 0x558240E4, 0x51435D53,
                0x251D3B9E, 0x21DC2629, 0x2C9F00F0, 0x285E1D47,
                0x36194D42, 0x32D850F5, 0x3F9B762C, 0x3B5A6B9B,
                0x0315D626, 0x07D4CB91, 0x0A97ED48, 0x0E56F0FF,
                0x1011A0FA, 0x14D0BD4D, 0x19939B94, 0x1D528623,
                0xF12F560E, 0xF5EE4BB9, 0xF8AD6D60, 0xFC6C70D7,
                0xE22B20D2, 0xE6EA3D65, 0xEBA91BBC, 0xEF68060B,
                0xD727BBB6, 0xD3E6A601, 0xDEA580D8, 0xDA649D6F,
                0xC423CD6A, 0xC0E2D0DD, 0xCDA1F604, 0xC960EBB3,
                0xBD3E8D7E, 0xB9FF90C9, 0xB4BCB610, 0xB07DABA7,
                0xAE3AFBA2, 0xAAFBE615, 0xA7B8C0CC, 0xA379DD7B,
                0x9B3660C6, 0x9FF77D71, 0x92B45BA8, 0x9675461F,
                0x8832161A, 0x8CF30BAD, 0x81B02D74, 0x857130C3,
                0x5D8A9099, 0x594B8D2E, 0x5408ABF7, 0x50C9B640,
                0x4E8EE645, 0x4A4FFBF2, 0x470CDD2B, 0x43CDC09C,
                0x7B827D21, 0x7F436096, 0x7200464F, 0x76C15BF8,
                0x68860BFD, 0x6C47164A, 0x61043093, 0x65C52D24,
                0x119B4BE9, 0x155A565E, 0x18197087, 0x1CD86D30,
                0x029F3D35, 0x065E2082, 0x0B1D065B, 0x0FDC1BEC,
                0x3793A651, 0x3352BBE6, 0x3E119D3F, 0x3AD08088,
                0x2497D08D, 0x2056CD3A, 0x2D15EBE3, 0x29D4F654,
                0xC5A92679, 0xC1683BCE, 0xCC2B1D17, 0xC8EA00A0,
                0xD6AD50A5, 0xD26C4D12, 0xDF2F6BCB, 0xDBEE767C,
                0xE3A1CBC1, 0xE760D676, 0xEA23F0AF, 0xEEE2ED18,
                0xF0A5BD1D, 0xF464A0AA, 0xF9278673, 0xFDE69BC4,
                0x89B8FD09, 0x8D79E0BE, 0x803AC667, 0x84FBDBD0,
                0x9ABC8BD5, 0x9E7D9662, 0x933EB0BB, 0x97FFAD0C,
                0xAFB010B1, 0xAB710D06, 0xA6322BDF, 0xA2F33668,
                0xBCB4666D, 0xB8757BDA, 0xB5365D03, 0xB1F740B4,
            };

        static UInt32 crc32_accum = 0;
        NavitasGeneralPage ParentPage;
        ObservableCollection<ParameterFileItem> FileParameters;
        void getCRC32_cpu(uint msg)
        {
            uint i = (crc32_accum >> 24) ^ msg;
            crc32_accum = (crc32_accum << 8) ^ crc32_table[i];
        }
        PageParameterList PageParameters;
        public ParameterFileHandler(NavitasGeneralPage parentPage)//string fileName)
        {
            ParentPage = parentPage;
            try
            {
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    PageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, ParentPage);
                    foreach (var v in App.ViewModelLocator.MainViewModel.GoiParameterList)
                    {
                         //start reading all flash parameters
                        if ((v.MemoryType == GoiParameter.MemoryTypeAndAccess.Flash) || (v.MemoryType == GoiParameter.MemoryTypeAndAccess.FlashReadOnly) || (v.MemoryType == GoiParameter.MemoryTypeAndAccess.LoadableOTP))
                        {
                            if (v.SubsetOfAddress == false && v.Address != ParametersViewModel.ONLY_IN_APP
                                && !(App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue <= 1.280 && v.Address >= 256)
                                && !(App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue <= 5.000 && v.Address >= 512)
                                )
                            {
                                //just whole values not partial bits or enums or App variables
                                PageParameters.parameterList.Add(v);
                            }
                        }
                    }
                }
                else
                {
                    PageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, ParentPage);
                    foreach (var v in App.ViewModelLocator.MainViewModelTSX.GoiParameterList)
                    {
                        //start reading all flash parameters
                        if ((v.MemoryType == GoiParameter.MemoryTypeAndAccess.Flash) || (v.MemoryType == GoiParameter.MemoryTypeAndAccess.FlashReadOnly) || (v.MemoryType == GoiParameter.MemoryTypeAndAccess.LoadableOTP))
                        {
                            if (v.SubsetOfAddress == false && v.Address != ParametersViewModelTSX.ONLY_IN_APP
                                && !(App.ViewModelLocator.GetParameterTSX("PAREMBEDDEDSOFTWAREVER").parameterValue <= 9.4 && v.Address >= 256)
                                )
                            {
                                //just whole values not partial bits or enums or App variables
                                PageParameters.parameterList.Add(v);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ParentPage.DisplayAlert("XML error: ", e.ToString(), "Close");
            }
        }

        public void SAVECHANGES_Clicked(object sender, EventArgs e)
        {  
            Task<bool> task = ParentPage.DisplayAlert("Save Changes", "Settings will saved to the Navitas directory of your device", "Yes", "Cancel");

            task.ContinueWith(SaveDismissedCallback);
        }
        public async void SaveDismissedCallback(Task<bool> task)
        {
            await ParentPage.communicationsReadWithWait(PageParameters);
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                //Keep commented out writer below incase you would like to know how to
                //create a new XML format while developing
                FileParameters = new ObservableCollection<ParameterFileItem>();
                foreach (var v in PageParameters.parameterList)
                {

                    if (((ControllerTypeLocator.ControllerType == "TAC") && App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= v.ImplementedFirmwareVersion)
                        || ((ControllerTypeLocator.ControllerType == "TSX") && App.ViewModelLocator.GetParameterTSX("PAREMBEDDEDSOFTWAREVER").parameterValue >= v.ImplementedFirmwareVersion))
                    {
                        ParameterFileItem fileParameter = new ParameterFileItem
                        {
                            Address = v.Address,
                            PropertyName = v.PropertyName,
                            ParameterValue = v.parameterValue,
                            ParameterValueRaw = v.rawValue
                        };

                        FileParameters.Add(fileParameter);
                        if ((v.MemoryType != GoiParameter.MemoryTypeAndAccess.FlashReadOnly) && (v.MemoryType != GoiParameter.MemoryTypeAndAccess.LoadableOTP)) // Leave Flash read only values out of CRC
                        {
                            getCRC32_cpu((uint)fileParameter.ParameterValueRaw & 0x00FF);   // Push low byte through CRC algorithm
                            getCRC32_cpu((uint)fileParameter.ParameterValueRaw >> 8 & 0x00FF);// Push high byte through CRC algorithm
                        }
                    }
                }
                string fileNamingRevision = "";
                if (ControllerTypeLocator.ControllerType == "TAC")
                    fileNamingRevision = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == "SOFTWAREREVISION").parameterValue.ToString("0.000");
                else
                    fileNamingRevision = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.PropertyName == "PAREMBEDDEDSOFTWAREVER").parameterValue.ToString("0.000");

                StreamWriter streamToWrite = new StreamWriter(FileManager.CreateStream(Path.Combine(FileManager.GetNavitasDirectoryPath(), "ParameterFile_FwRev_" + fileNamingRevision + "AppRev_" + App.InfoService.Version + "_saved_" + DateTime.Now.ToString(("dd-MM-yyyy")) + "_time_" + DateTime.Now.ToString("s").Replace(":", ".") + "_CRC=_" + crc32_accum + ".xml")));
                new XmlSerializer(typeof(ObservableCollection<ParameterFileItem>)).Serialize(streamToWrite, FileParameters);//Writes to the file
                streamToWrite.Dispose();
                DateTime starttime = DateTime.Now;

                Device.BeginInvokeOnMainThread(() =>
                {
                    ParentPage.activityMessage.Text = "Saving...";
                });

                MessagingCenter.Send<NavitasGeneralPage>(ParentPage, "ShowActivity");

                while ((DateTime.Now - starttime) < TimeSpan.FromSeconds(2))
                {
                    Task.Delay(10).Wait();
                }

                MessagingCenter.Send<NavitasGeneralPage>(ParentPage, "StopActivity");
                crc32_accum = 0;    // Reset for the next time the parameter CRC is calculated.
            }
        }
        private void INITIALIZESETTINGS_Clicked(object sender, EventArgs e)
        {
            Task<bool> task = ParentPage.DisplayAlert("Initialize", "Settings will be over written to factory defaults. Click save to save these settings", "Yes", "Cancel");

            task.ContinueWith(InitializeDismissedCallback);

        }


        void InitializeDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                ParentPage.QueParameter(new SetParameterEventArgs(49, 1.0f, null));
            }
        }

        public void LoadParametersFromFile_Clicked(object sender, EventArgs e)
        {
            AddExternalFilesAsButtons();
        }

        public class EventArgs<T> : EventArgs
        {
            public T Value { get; private set; }

            public EventArgs(T val)
            {
                Value = val;
            }
        }

        void AddExternalFilesAsButtons()
        {
            string[] fileList = FileManager.GetNavitasDirectoryFiles();
            ParentPage.BuildfileListPopUp();
            ParentPage.fileListPopUp.IsVisible = true;
            //clear old list otherwise it just keeps adding same file items
            while (ParentPage.FileButtonList.Children.Count > 2)
                ParentPage.FileButtonList.Children.RemoveAt(1);
            foreach (var fileName in fileList)
            {
                bool fileNeedsToBeAdded = true;

                if (fileNeedsToBeAdded && fileName.Contains("ParameterFile") && fileName.Contains(".xml"))
                {
                    Frame publicFile = new Frame
                    {
                        BorderColor = Color.Black,
                        Padding = new Thickness(5, 0),
                        Margin = new Thickness(5, 0),
                        IsVisible = true,
                        BackgroundColor = Color.FromRgb(215, 215, 215),
                        CornerRadius = 5,
                    };
                    StackLayout textAndTapContainer = new StackLayout
                    {
                        Padding = new Thickness(10),
                        Children =
                    {
                            new Label{ Text = Path.GetFileName(fileName)},
                    }
                    };
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1
                    };
                    tapGestureRecognizer.Tapped += (sender, args) => OnButtonClicked(this, new EventArgs<string>(fileName));

                    publicFile.Content = textAndTapContainer;
                    publicFile.GestureRecognizers.Add(tapGestureRecognizer);

                    ParentPage.FileButtonList.Children.Insert(1, publicFile); //skip first labels in xaml
                }
            }
            Button btnCancel = new Button()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Cancel"
            };
            btnCancel.Clicked += Load_Cancel_Clicked;
            ParentPage.FileButtonList.Children.Add(btnCancel);
        }
        string loadFileName = ""; //didn't have time to figure out how to pass it to two level click

        async void OnButtonClicked(object sender, EventArgs args)
        {
            loadFileName = (args as EventArgs<string>).Value;
            Task<bool> task = ParentPage.DisplayAlert("Load setting from file", "Settings will be over written with this file. When completed click Save to Flash to permanently retain these settings after a power cycle", "Yes", "Cancel");
            ParentPage.fileListPopUp.IsVisible = false;
            await task.ContinueWith(LoadFileDismissedCallback);
        }
        async void LoadFileDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                var index = 0;
                foreach (var x in DeviceComunication.PageCommunicationsListPointer)
                {
                    if (index++ != 0)
                        x.parentPage.Active = false;
                }
                string strFileName = loadFileName;
                FileParameters = FileManager.GetDeserializedObject<ObservableCollection<ParameterFileItem>>(strFileName);
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    ParentPage.activityMessage.Text = "0%";
                });

                MessagingCenter.Send<NavitasGeneralPage>(ParentPage, "ShowActivity");
                foreach (var parameterFileItem in FileParameters)
                {
                    var parameter = new GoiParameter();
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.Address == parameterFileItem.Address);

                        //if ((parameter != null) 
                        if ((parameter != null) && (parameter.Address != 196)) //HL Added Jan5th,2021 to get rid of valid parameter confilicting problem
                        {
                            if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= parameter.ImplementedFirmwareVersion)
                            {
                                ParentPage.SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, parameterFileItem.ParameterValueRaw, "never used so get rid of this");
                                if (parameter.MemoryType != GoiParameter.MemoryTypeAndAccess.FlashReadOnly) // Don't send Read Only parameters (like sensor calibration values)
                                {
                                    ParentPage.QueParameter(ParentPage.SetParameterEventArgs);
                                }
                            }
                            else
                                System.Diagnostics.Debug.WriteLine("Navitas Parameter PropertyName " + parameterFileItem.PropertyName + " not found");
                        }
                        if (parameter.Address == 109)
                            System.Diagnostics.Debug.WriteLine("whaaa");
                    }
                    else
                    {
                        parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(i => i.Address == parameterFileItem.Address);

                        //if ((parameter != null) 
                        if ((parameter != null))
                        {
                            if (App.ViewModelLocator.GetParameterTSX("PAREMBEDDEDSOFTWAREVER").parameterValue >= parameter.ImplementedFirmwareVersion)
                            {
                                ParentPage.SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, parameterFileItem.ParameterValueRaw, "never used so get rid of this");
                                if (parameter.MemoryType != GoiParameter.MemoryTypeAndAccess.FlashReadOnly) // Don't send Read Only parameters (like sensor calibration values)
                                {
                                    ParentPage.QueParameter(ParentPage.SetParameterEventArgs);
                                }
                            }
                            else
                                System.Diagnostics.Debug.WriteLine("Navitas Parameter PropertyName " + parameterFileItem.PropertyName + " not found");
                        }
                    }
                }
                while (await App._devicecommunication.IsQueueNotEmptyAsync())
                {
                    await MainFlyoutPage.BeginInvokeOnMainThreadAsync(async () =>
                    {
                        ParentPage.activityMessage.Text = ((int)((float)(FileParameters.Count() - (await App._devicecommunication.ItemsLeftInQueueAsync())) / FileParameters.Count() * 100)).ToString() + " %";
                        await Task.Delay(100); //tracking the controller writes
                    });
                }

                MessagingCenter.Send<NavitasGeneralPage>(ParentPage, "StopActivity");
            }
            ParentPage.PageParameters.Active = true;
        }

        async void Load_Cancel_Clicked(object sender, EventArgs args)
        {
            ParentPage.fileListPopUp.IsVisible = false;
        }

        private void READ_Clicked(object sender, EventArgs e)
        {
            ParentPage.ReadVEEPROM(false);
        }
    }
}
