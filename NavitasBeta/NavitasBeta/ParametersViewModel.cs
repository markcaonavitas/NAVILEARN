using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NavitasBeta
{
    public class ParametersViewModel : ViewModelBase
    {

        private ObservableCollection<GoiParameter> _goiParameterList;
        public ObservableCollection<GoiParameter> GoiParameterList
        {
            set { SetProperty(ref _goiParameterList, value); }
            get { return _goiParameterList; }
        }
        public const int ONLY_IN_APP = -1;

        private bool _userLevelEqualOrGreaterThanAdvancedUserProperty;
        public bool UserLevelEqualOrGreaterThanAdvancedUserProperty
        {
            get { return (_userLevelEqualOrGreaterThanAdvancedUserProperty); }
            set
            {
                SetProperty(ref _userLevelEqualOrGreaterThanAdvancedUserProperty, value);
            }
        }

        private bool _userLevelEqualOrGreaterThanDealerProperty;
        public bool UserLevelEqualOrGreaterThanDealerProperty
        {
            get { return (_userLevelEqualOrGreaterThanDealerProperty); }
            set
            {
                SetProperty(ref _userLevelEqualOrGreaterThanDealerProperty, value);
            }
        }


        private bool _userLevelEqualOrGreaterThanEngProperty;
        public bool UserLevelEqualOrGreaterThanEngProperty
        {
            get { return (_userLevelEqualOrGreaterThanEngProperty); }
            set
            {
                SetProperty(ref _userLevelEqualOrGreaterThanEngProperty, value);
            }
        }

        private bool _isRequestAdvancedUserEnable;
        public bool IsRequestAdvancedUserEnable
        {
            get { return (_isRequestAdvancedUserEnable); }
            set
            {
                SetProperty(ref _isRequestAdvancedUserEnable, value);
            }
        }

        public GoiParameter SOFTWAREREVISION;
        private bool _isDemoMode = false;
        public bool IsDemoMode
        {
            get { return (_isDemoMode); }
            set
            {
                if(value != _isDemoMode)
                {
                    _isDemoMode = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<SupplierChain> SupplierChains { get; set; }

        public ParametersViewModel()
        {

            // Initialize

            //System.Diagnostics.Debug.WriteLine("ParametersViewModel TAC");

            GoiParameterList = FileManager.DeserializeExternalFirstOrInternalXml<ObservableCollection<GoiParameter>>("TACDictionary.xml", "NavitasBeta.Dictionaries.TACDictionary.xml");

            var parameterSuperSet = FileManager.DeserializeExternalFirstOrInternalXml<List<ParameterFile>>("BatteryGroupedParameterFile.xml", "NavitasBeta.GroupedParameterFiles.BatteryGroupedParameterFile.xml");
            ConstructBatteryGroupedParameterFile("Initilize", parameterSuperSet);

            SOFTWAREREVISION = GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SOFTWAREREVISION"));

            //GoiParameter objects were constructed this way,because of .xml desiralization's constraint
            foreach (GoiParameter parameter in GoiParameterList)
            {
                if (parameter.SubsetOfAddress)
                    GoiParameterList.FirstOrDefault(x => (x.Address == parameter.Address) && (x.SubsetOfAddress == false)).SubsetList.Add(parameter);

                if (parameter.EnumPointer != null)
                {
                    parameter.EnumPointerObj = GoiParameterList.FirstOrDefault(x => x.PropertyName == parameter.EnumPointer);
                    parameter.EnumPointerObj.ParametersPointToThis.Add(parameter);
                }
            }

            SupplierChains = FileManager.DeserializeExternalFirstOrInternalXml<List<SupplierChain>>("SupplierChain.xml", "NavitasBeta.SupplierChain.SupplierChain.xml");

            List<ParameterFile> parameterSuperSetThermistor = FileManager.GetDeserializedObject<List<ParameterFile>>("ThermistorGroupedParameterFile.xml");
            List<string> thermistorTypeList = new List<string>();
            foreach (var parameterFile in parameterSuperSetThermistor)
            {
                thermistorTypeList.Add(parameterFile.FriendlyFileName);
            }
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Thermistor Type", "THERMISTOR_TYPE", thermistorTypeList, parameterSuperSetThermistor));
            foreach (var parameterFileItem in parameterSuperSetThermistor[0].ParameterFileItemList)
            {
                GoiParameterList.FirstOrDefault(x => (x.PropertyName == parameterFileItem.PropertyName)).ReCalculate += CalculateThermistorType;
            }

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Vehicle Speed", "SPEED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SPEED")).ReCalculate += fixSyncFusionSpeedometerException;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Kilometers or Miles", "KILOMETERSORMILES", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            //Gauge logic bits
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "FORWARD", "FORWARD", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "REVERSE", "REVERSE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "NEUTRAL", "NEUTRAL", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "KEYENABLE", "KEYENABLE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            //Gauge fault bits
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "CONTROLLERERROR", "CONTROLLERERROR", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "COMMERROR", "COMMERROR", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "LOCKED", "LOCKED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "UNLOCKED", "UNLOCKED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Forward Speed Limit (MPH)", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Reverse Speed Limit (MPH)", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Forward Speed Limit (KPH)", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Reverse Speed Limit (KPH)", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "OTFSPEED", "OTFSPEED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "OTFDECEL", "OTFDECEL", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "OTFACCEL", "OTFACCEL", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "CONTROLLERWARNING", "CONTROLLERWARNING", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "GROUPONEWARNINGS", "GROUPONEWARNINGS", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Throttle %", "THROTTLEPERCENTAGE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Torque Commanded after Limiters ", "TORQUECOMMANDEDMODIFIED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //

            //GoiParameterList.FirstOrDefault(x => (x.PropertyName == "InvertDigitalInput")).ReCalculate += CalculateToggleDigitialInputBits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "Options")).ReCalculate += CalculateOptionBits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "FORWARDSPEED")).ReCalculate += CalculateOldOTFSettings;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "ROTORRPM")).ReCalculate += CalculateSpeed;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SWITCHBITS")).ReCalculate += CalculateForwardNeutralReverseBits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPONEFAULTS")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPTWOFAULTS")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPTHREEFAULTS")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPFOURFAULTS")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "VEHICLELOCKED")).ReCalculate += CalculateLock;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "FWDLMTRPM")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "RVSLMTRPM")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "TIREDIAMETER")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "REARAXLERATIO")).ReCalculate += CalculateMPHLimits;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "APPVERSION", "APPVERSION", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "APPVERSION")).parameterValueString = App.InfoService.Version;

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SPEEDOMETERMAXSPEED")).parameterValue = 35;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SPEEDOMETERMAXSPEED")).ReCalculate += CalculateSpeedOmeterMaxSpeed; //35;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Dashboard Display Speedometer Range KPH", "SPEEDOMETERMAXSPEEDKPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Dashboard Display Speedometer Range", "SPEEDOMETERMAXSPEEDMKPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "MILESORKILOMETERS")).ReCalculate += UpdateDisplayUnit; //35;
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Speed Uint", "SPEEDUINT", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Threshold one", "THRESHOLDONE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Threshold Two", "THRESHOLDTWO", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SOFTWAREREVISION")).ReCalculate += FillInProfileNumberFromSoftwareRev; //future implement/depricate tags needed
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "NEWPROFILENUMBER")).ReCalculate += FillInNewProfileNumberFromSoftwareRev; //future implement/depricate tags needed
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "DEMOMODE", "DEMOMODE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = false;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Motor Temperature (F)", "MTTEMPF", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Motor Temperature", "MTTEMPCF", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Controller Temperature (F)", "PBTEMPF", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
             GoiParameterList.FirstOrDefault(x => (x.PropertyName == "MTTEMPC")).ReCalculate += CalculateControllerWarning;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PBTEMPC")).ReCalculate += CalculateControllerWarning;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DischargeBatteryCurrentLimit_Aq4")).ReCalculate += CalculateControllerWarning;

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "VTHROTTLEV")).ReCalculate += CalculateThrottlePercentage;

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "MAXCURRENT")).ReCalculate += CalculateTorqueCommanded;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SLIPREFSNAP2")).ReCalculate += CalculateTorqueCommanded;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "HigherOfLogicPowerOrBatteryPostVoltage", "HigherOfLogicPowerOrBatteryPostVoltage", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "VBATVDC")).ReCalculate += CalculateBatteryVoltage;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "LOGICSIDEBATTERYVOLTAGEV")).ReCalculate += CalculateBatteryVoltage;
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Calibrate Voltage input", "INPUT_VOLTAGE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Latitude", "LATITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Longitude", "LONGITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Altitude", "ALTITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Acceleration (m/s^2)", "ACCELERATION", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Torque (Nm)", "TORQUE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation X", "ORIENTATIONX", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation Y", "ORIENTATIONY", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation Z", "ORIENTATIONZ", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation W", "ORIENTATIONW", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
        }
        public static void CalculateSpeedOmeterMaxSpeed(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDKPH").parameterValue = (Int16)App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue * 1.609344f;

            if (App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue != 0)
            {
                App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDKPH").parameterValue;
            }
            else
            {
                App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue;
            }
        }
        public void ConstructBatteryGroupedParameterFile(string state, List<ParameterFile> parameterSuperSet)
        {
            // Get first element's ParameterFileItemList from the supper set
            // Make use of its children as a template due to they repeat across all ParameterFile (Profile)
            var parameterFileItemList = parameterSuperSet[0].ParameterFileItemList;

            List<string> batteryTypeList = new List<string>();

            foreach (var parameterFile in parameterSuperSet)
            {
                batteryTypeList.Add(parameterFile.FriendlyFileName);
            }

            if (state == "Initilize")
            {
                GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Battery Type", "BATTERY_TYPE", batteryTypeList, parameterSuperSet));

                // Register CalculateBatteryType to each parameter's delegate recalculate
                foreach (var parameterFileItem in parameterFileItemList)
                {
                    GoiParameterList.FirstOrDefault(x => (x.PropertyName == parameterFileItem.PropertyName)).ReCalculate += CalculateBatteryType;
                }
            }
            else // state == Modified
            {
                var parameterTarget = App.ViewModelLocator.GetParameter("BATTERY_TYPE");
                var prevParameterFileItemList = parameterTarget.GroupedParameters[0].ParameterFileItemList;

                var prevPropertyNames = prevParameterFileItemList.Select(p => p.PropertyName).ToList();
                var recentPropertyNames = parameterFileItemList.Select(p => p.PropertyName).ToList();

                //how differentitate the newer battery group from the older one
                var propertyNamesOnlyInNewList = recentPropertyNames.Except(prevPropertyNames).ToList();

                // overwrite the old list
                parameterTarget.GroupedParameters = parameterSuperSet;
                parameterTarget.enumListName = batteryTypeList;

                // remove delegate if it necessary

                // Assign delegate to new parameters, (there should be a parameter that exists in a dictionary as well
                // because it is required to recalculate when theirs value change
                if (propertyNamesOnlyInNewList != null)
                {
                    foreach (var propertyName in propertyNamesOnlyInNewList)
                    {
                        GoiParameterList.FirstOrDefault(x => (x.PropertyName == propertyName)).ReCalculate += CalculateBatteryType;
                    } 
                }
            }

        }

        public static void UpdateDisplayUnit(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDKPH").parameterValue = App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue * 1.609344f;
            if (App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue != 0)
            {
                App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDKPH").parameterValue;
                App.ViewModelLocator.GetParameter("MTTEMPCF").parameterValue = App.ViewModelLocator.GetParameter("MTTEMPC").parameterValue;
                App.ViewModelLocator.GetParameter("SPEEDUINT").parameterValueString = "KPH";
                App.ViewModelLocator.GetParameter("THRESHOLDONE").parameterValue = 33;
                App.ViewModelLocator.GetParameter("THRESHOLDTWO").parameterValue = 42;
                App.ViewModelLocator.GetParameter("SPEED").Name = "Vehicle Speed (KPH)";
            }
            else
            {
                App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue;
                App.ViewModelLocator.GetParameter("MTTEMPCF").parameterValue = App.ViewModelLocator.GetParameter("MTTEMPF").parameterValue;
                App.ViewModelLocator.GetParameter("SPEEDUINT").parameterValueString = "MPH";
                App.ViewModelLocator.GetParameter("THRESHOLDONE").parameterValue = 20;
                App.ViewModelLocator.GetParameter("THRESHOLDTWO").parameterValue = 25;
                App.ViewModelLocator.GetParameter("SPEED").Name = "Vehicle Speed (MPH)";
            }
        }

        public static void CalculateBatteryType(GoiParameter unUsed)
        {
            //MessagingCenter.Send<NavitasGeneralPage>(MainFlyoutlPage.userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage), "ShowActivity");

            foreach (var item in App.ViewModelLocator.GetParameter("BATTERY_TYPE").GroupedParameters)
            {
                bool matchFound = true;
                bool matchBatteryConfigurationProfileNumberFound = false;
                var debug = App.ViewModelLocator.GetParameter("BATTERY_TYPE");
                var indexOfItem = App.ViewModelLocator.GetParameter("BATTERY_TYPE").GroupedParameters.IndexOf(item);
                var indexOfModifiedString = App.ViewModelLocator.GetParameter("BATTERY_TYPE").Name.IndexOf(" (modified");
                if(indexOfModifiedString != -1)
                    App.ViewModelLocator.GetParameter("BATTERY_TYPE").Name = App.ViewModelLocator.GetParameter("BATTERY_TYPE").Name.Remove(indexOfModifiedString);

                foreach (var parameterFileItem in item.ParameterFileItemList)
                {
                    if (App.ViewModelLocator.MainViewModel.SOFTWAREREVISION.parameterValue >= App.ViewModelLocator.GetParameter(parameterFileItem.PropertyName).ImplementedFirmwareVersion)
                    {
                        //it's hard to have equal floats so just see if they are close enough, .01%
                        if ((Math.Abs(App.ViewModelLocator.GetParameter(parameterFileItem.PropertyName).parameterValue / parameterFileItem.ParameterValue) > 1.0001
                             || Math.Abs(App.ViewModelLocator.GetParameter(parameterFileItem.PropertyName).parameterValue / parameterFileItem.ParameterValue) < 0.9999))
                        {
                            matchFound = false;
                        }
                        else if (parameterFileItem.PropertyName == "BatteryConfigurationProfileNumber")
                        {//BatteryConfigurationProfileNumber must have been implemented and the value must have matched
                            matchBatteryConfigurationProfileNumberFound = true;
                        }
                    }

                }
                if (matchFound || matchBatteryConfigurationProfileNumberFound)
                {//the first match is used hopefully there is only one match
                 //and magically since the last list is empty (matchFound = false is never set) so it must be custom
                    if (!matchFound)
                    {//as of Firmware 8.9  BatteryConfigurationProfileNumber can be used and no matchFound simply indicates was the base battery was
                        App.ViewModelLocator.GetParameter("BATTERY_TYPE").Name += " (modified " + App.ViewModelLocator.GetParameter("BATTERY_TYPE").enumListName[indexOfItem] + ")";
                        //now make custom in drop down so that when the user selects the unmodified version (or a different one) the on selected item event fires in the picker
                        //otherwise the selected item never changes and these modified values will not be overwritten
                        App.ViewModelLocator.GetParameter("BATTERY_TYPE").parameterValue = App.ViewModelLocator.GetParameter("BATTERY_TYPE").GroupedParameters.Count - 1;
                    }
                    else
                    {// if matchBatteryConfigurationProfileNumberFound is not used then only custom shows up in the drop down and nobody know what type was customized
                        App.ViewModelLocator.GetParameter("BATTERY_TYPE").parameterValue = App.ViewModelLocator.GetParameter("BATTERY_TYPE").GroupedParameters.IndexOf(item);
                    }
                    break;
                }
            }
            //MessagingCenter.Send<NavitasGeneralPage>(this, "StopActivity");
        }
        public static void CalculateThermistorType(GoiParameter unUsed)
        {
            foreach (var item in App.ViewModelLocator.GetParameter("THERMISTOR_TYPE").GroupedParameters)
            {
                bool matchFound = true;
                foreach (var parameterFileItem in item.ParameterFileItemList)
                {
                    //it's hard to have equal floats so just see if they are close enough, .01%
                    if (Math.Abs(App.ViewModelLocator.GetParameter(parameterFileItem.PropertyName).parameterValue / parameterFileItem.ParameterValue) > 1.0001
                        ||
                        Math.Abs(App.ViewModelLocator.GetParameter(parameterFileItem.PropertyName).parameterValue / parameterFileItem.ParameterValue) < 0.9999)
                    {
                        matchFound = false;
                    }
                }
                if (matchFound)
                {//the first match is used hopefully there is only one match
                 //and magically since the last list is empty (matchFound = false is never set) so it must be custom 
                    App.ViewModelLocator.GetParameter("THERMISTOR_TYPE").parameterValue = App.ViewModelLocator.GetParameter("THERMISTOR_TYPE").GroupedParameters.IndexOf(item);
                    break;
                }
            }
        }

        public static void fixSyncFusionSpeedometerException(GoiParameter unUsed)
        {
            if (App.ViewModelLocator.GetParameter("SPEED").parameterValue > App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue)
                App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").parameterValue = App.ViewModelLocator.GetParameter("SPEED").parameterValue;
        }

        public static void CalculateOldOTFSettings(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameter("OTFSPEED").parameterValue = App.ViewModelLocator.GetParameter("FORWARDSPEED").parameterValue / App.ViewModelLocator.GetParameter("FWDLMTRPM").parameterValue * 100;
            App.ViewModelLocator.GetParameter("OTFDECEL").parameterValue = App.ViewModelLocator.GetParameter("ScalerForMuchFasterNonVehicleAccelAndDecel_PUq0").parameterValue / App.ViewModelLocator.GetParameter("ScalerForMuchFasterNonVehicleAccelAndDecel_PUq0").parameterValue * 100;
            App.ViewModelLocator.GetParameter("OTFACCEL").parameterValue = App.ViewModelLocator.GetParameter("RISERPMSEC").parameterValue / App.ViewModelLocator.GetParameter("RISEHZSEC").parameterValue * 100;
        }

        public static void CalculateForwardNeutralReverseBits(GoiParameter switchbits)
        {
            if (((int)switchbits.parameterValue & 0x0006) == 0x0006) //both direction switches on
            {
                App.ViewModelLocator.GetParameter("FORWARD").parameterValue = 0;
                App.ViewModelLocator.GetParameter("REVERSE").parameterValue = 0;
                App.ViewModelLocator.GetParameter("NEUTRAL").parameterValue = 1;
            }
            else if (((int)switchbits.parameterValue & 0x0002) == 0x0002) // forward
            {
                App.ViewModelLocator.GetParameter("FORWARD").parameterValue = 1;
                App.ViewModelLocator.GetParameter("REVERSE").parameterValue = 0;
                App.ViewModelLocator.GetParameter("NEUTRAL").parameterValue = 0;
            }
            else if (((int)switchbits.parameterValue & 0x0004) == 0x0004) // reverse
            {
                App.ViewModelLocator.GetParameter("FORWARD").parameterValue = 0;
                App.ViewModelLocator.GetParameter("REVERSE").parameterValue = 1;
                App.ViewModelLocator.GetParameter("NEUTRAL").parameterValue = 0;

            }
            else
            {
                App.ViewModelLocator.GetParameter("FORWARD").parameterValue = 0;
                App.ViewModelLocator.GetParameter("REVERSE").parameterValue = 0;
                App.ViewModelLocator.GetParameter("NEUTRAL").parameterValue = 1;

            }
            if (((int)switchbits.parameterValue & 0x0010) == 0x0010)
            {
                App.ViewModelLocator.GetParameter("KEYENABLE").parameterValue = 1;
            }
            else
            {
                App.ViewModelLocator.GetParameter("KEYENABLE").parameterValue = 0;

            }
        }

        private static DateTime _previousTimeAtSpeedUpdated = DateTime.MinValue;
        private static float _previousSpeedValue = 0;
        public static void CalculateSpeed(GoiParameter unUsed)
        {
            float inchesPerMinuteToMPH = 60 * 2 * (float)Math.PI * 1.57828e-5f;
            float speed = inchesPerMinuteToMPH * ((float)Math.Abs(App.ViewModelLocator.GetParameter("ROTORRPM").parameterValue) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2) / App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue;
            float velocityMeterPerSecond = speed * 0.447040f;
            DateTime timeAtSpeedUpdated = DateTime.Now;

            if (speed >= 0.0)
            {
                //      System.Diagnostics.Debug.WriteLine("SPEED");
                if (App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue != 0)
                {
                    App.ViewModelLocator.GetParameter("SPEED").parameterValue = speed * 1.609344f;
                }
                else
                {
                    App.ViewModelLocator.GetParameter("SPEED").parameterValue = speed;
                };
                
                if (_previousTimeAtSpeedUpdated != DateTime.MinValue)
                {
                    App.ViewModelLocator.GetParameter("ACCELERATION").parameterValue = (float)((velocityMeterPerSecond - _previousSpeedValue) / (((TimeSpan)(timeAtSpeedUpdated - _previousTimeAtSpeedUpdated)).TotalMilliseconds / 1000));
                }
                _previousTimeAtSpeedUpdated = timeAtSpeedUpdated;
                _previousSpeedValue = velocityMeterPerSecond;
            }
        }

        public static void CalculateOptionBits(GoiParameter OptionBits)
        {
            if (((int)OptionBits.parameterValue & 0x0001) == 0x0001)
            {
                App.ViewModelLocator.GetParameter("REVERSEENCODER").parameterValue = 1;
                //System.Diagnostics.Debug.WriteLine("REVERSEENCODER written 1");

            }
            else
            {
                App.ViewModelLocator.GetParameter("REVERSEENCODER").parameterValue = 0;
                //System.Diagnostics.Debug.WriteLine("REVERSEENCODER written 0");
            }

            if (((int)OptionBits.parameterValue & 0x0002) == 0x0002)
                App.ViewModelLocator.GetParameter("DISABLEANALOGBRAKE").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("DISABLEANALOGBRAKE").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0004) == 0x0004)
                App.ViewModelLocator.GetParameter("DISABLEBRAKESWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("DISABLEBRAKESWITCH").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0008) == 0x0008)
                App.ViewModelLocator.GetParameter("MFG_TESTING_ANALOG_BRAKE_AND_THROTTLE_OPTION").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("MFG_TESTING_ANALOG_BRAKE_AND_THROTTLE_OPTION").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0010) == 0x0010)
                App.ViewModelLocator.GetParameter("MANUFACTURINGTESTOPTION").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("MANUFACTURINGTESTOPTION").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0020) == 0x0020)
                App.ViewModelLocator.GetParameter("DISABLEOFFTHROTTLEREGENTOSTOP").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("DISABLEOFFTHROTTLEREGENTOSTOP").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0040) == 0x0040)
                App.ViewModelLocator.GetParameter("REVERSEREARAXLEDIRECTION").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("REVERSEREARAXLEDIRECTION").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0080) == 0x0080)
                App.ViewModelLocator.GetParameter("ANTIROLLBACK").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("ANTIROLLBACK").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0100) == 0x0100)
                App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue = 0;

            if (((int)OptionBits.parameterValue & 0x0200) == 0x0200)
                App.ViewModelLocator.GetParameter("ENABLENEUTRALSTARTUPINTERLOCK").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("ENABLENEUTRALSTARTUPINTERLOCK").parameterValue = 0;
        }

        public static void CalculateToggleDigitialInputBits(GoiParameter ToggleDigitalInputs)
        {
            if (((int)ToggleDigitalInputs.parameterValue & 0x0001) == 0x0001)
                App.ViewModelLocator.GetParameter("TOGGLEKEYSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEKEYSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0002) == 0x0002)
                App.ViewModelLocator.GetParameter("TOGGLETOWSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLETOWSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0004) == 0x0004)
                App.ViewModelLocator.GetParameter("TOGGLECHARGERSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLECHARGERSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0008) == 0x0008)
                App.ViewModelLocator.GetParameter("TOGGLEFORWARDSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEFORWARDSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0010) == 0x0010)
                App.ViewModelLocator.GetParameter("TOGGLEREVERSESWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEREVERSESWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0020) == 0x0020)
                App.ViewModelLocator.GetParameter("TOGGLEFOOTSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEFOOTSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0040) == 0x0040)
                App.ViewModelLocator.GetParameter("TOGGLEBRAKESWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEBRAKESWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0080) == 0x0080)
                App.ViewModelLocator.GetParameter("TOGGLEOTFSWITCH").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEOTFSWITCH").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0100) == 0x0100)
                App.ViewModelLocator.GetParameter("TOGGLESOLENOIDFAULT").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLESOLENOIDFAULT").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0200) == 0x0200)
                App.ViewModelLocator.GetParameter("TOGGLEBRAKEFAULT").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEBRAKEFAULT").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0400) == 0x0400)
                App.ViewModelLocator.GetParameter("TOGGLEENCODERA").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEENCODERA").parameterValue = 0;

            if (((int)ToggleDigitalInputs.parameterValue & 0x0800) == 0x0800)
                App.ViewModelLocator.GetParameter("TOGGLEENCODERB").parameterValue = 1;
            else
                App.ViewModelLocator.GetParameter("TOGGLEENCODERB").parameterValue = 0;
        }


        public static void CalculateControllerError(GoiParameter unUsed)
        {
            float fControllerError = 0.1f;
            if (App.ViewModelLocator.GetParameter("GROUPONEFAULTS").parameterValue != 0 ||
                App.ViewModelLocator.GetParameter("GROUPTWOFAULTS").parameterValue != 0 ||
                App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS").parameterValue != 0 ||
                App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").parameterValue != 0 ||
                App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue != 0)
            {
                fControllerError = 1.0f;
            }
            App.ViewModelLocator.GetParameter("CONTROLLERERROR").parameterValue = fControllerError;
        }

        public static void CalculateLock(GoiParameter unUsed)
        {

            if (App.ViewModelLocator.GetParameter("VEHICLELOCKED").parameterBoolean)
            {
                App.ViewModelLocator.GetParameter("LOCKED").parameterValue = 1;
                App.ViewModelLocator.GetParameter("UNLOCKED").parameterValue = 0;
            }
            else
            {
                App.ViewModelLocator.GetParameter("LOCKED").parameterValue = 0;
                App.ViewModelLocator.GetParameter("UNLOCKED").parameterValue = 1;
            }
        }

        public static void CalculateMPHLimits(GoiParameter unUsed) //just do both here
        {
            App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue = App.ViewModelLocator.GetParameter("FWDLMTRPM").parameterValue / ((App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2));
            App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue = App.ViewModelLocator.GetParameter("RVSLMTRPM").parameterValue / ((App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2));
            App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH").parameterValue = App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue * 1.609344f;
            App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH").parameterValue = App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue * 1.609344f;
        }
        public static void CalculateTorqueCommanded(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameter("TORQUECOMMANDEDMODIFIED").parameterValue = App.ViewModelLocator.GetParameter("SLIPREFSNAP2").parameterValue / App.ViewModelLocator.GetParameter("MAXCURRENT").parameterValue;
            App.ViewModelLocator.GetParameter("TORQUE").parameterValue = 9550 * (((App.ViewModelLocator.GetParameter("VBATVDC").parameterValue / 2) * App.ViewModelLocator.GetParameter("VQV").parameterValue) * App.ViewModelLocator.GetParameter("VECTORCURRENT").parameterValue / 1000) / App.ViewModelLocator.GetParameter("ROTORRPM").parameterValue;
        }
        public static void CalculateControllerWarning(GoiParameter unUsed)
        {
            float fControllerWarning = 0.0f;
            Int32 finalValue = 0;
            if(!unUsed.couldBeDirtyBecauseKBDoneWasNotUsed)
            {
                App.ViewModelLocator.GetParameter("MTTEMPF").parameterValue = (App.ViewModelLocator.GetParameter("MTTEMPC").parameterValue * 9) / 5 + 32;
                App.ViewModelLocator.GetParameter("PBTEMPF").parameterValue = (App.ViewModelLocator.GetParameter("PBTEMPC").parameterValue * 9) / 5 + 32;

                if (App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue != 0)
                {
                    App.ViewModelLocator.GetParameter("MTTEMPCF").parameterValue = App.ViewModelLocator.GetParameter("MTTEMPC").parameterValue;
                }
                else
                {
                    App.ViewModelLocator.GetParameter("MTTEMPCF").parameterValue = App.ViewModelLocator.GetParameter("MTTEMPF").parameterValue;
                }

                if (App.ViewModelLocator.GetParameter("MTTEMPC").parameterValue > App.ViewModelLocator.GetParameter("MOTTRIPC").parameterValue &&
                    App.ViewModelLocator.GetParameter("MOTTRIPC").parameterValue != 0)
				{
                    finalValue += (1<<3);
                    fControllerWarning = 1;
				}
                else if (App.ViewModelLocator.GetParameter("MTTEMPC").parameterValue > App.ViewModelLocator.GetParameter("NONFATALOTTRIPC").parameterValue &&
                    App.ViewModelLocator.GetParameter("NONFATALOTTRIPC").parameterValue != 0)
                {
                    finalValue += (1 << 0);
                    fControllerWarning = 1;
                }

                if ((App.ViewModelLocator.GetParameter("PBTEMPC").parameterValue > App.ViewModelLocator.GetParameter("OTWARNINGC").parameterValue) &&
                    App.ViewModelLocator.GetParameter("OTWARNINGC").parameterValue != 0)
                {
                    finalValue += (1 << 1);
                    fControllerWarning = 1;
                }

                if ((App.ViewModelLocator.GetParameter("DischargeBatteryCurrentLimit_Aq4").parameterValue <= App.ViewModelLocator.GetParameter("BatteryDischargePulseCapacity").parameterValue) &&
                    App.ViewModelLocator.GetParameter("BatteryDischargePulseCapacity").parameterValue != 0)
                {
                    //finalValue += (1 << 4);
                    //fControllerWarning = 1;
                }

                if ((App.ViewModelLocator.GetParameter("HigherOfLogicPowerOrBatteryPostVoltage").parameterValue < App.ViewModelLocator.GetParameter("UNDERVOLTAGETHRESHOLD").parameterValue) &&
                    App.ViewModelLocator.GetParameter("UNDERVOLTAGETHRESHOLD").parameterValue != 0)
                {
                    //finalValue += (1 << 2);
                    //fControllerWarning = 1;
                }
            }
            App.ViewModelLocator.GetParameter("CONTROLLERWARNING").parameterValue = fControllerWarning;
            App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue = finalValue;

            CalculateControllerError(unUsed);
        }

        public static void CalculateThrottlePercentage(GoiParameter unUsed)
        {
            var throttleVoltage = App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue;
            var throttleMax = App.ViewModelLocator.GetParameter("THROTTLEMAX").parameterValue;
            var throttleMin = App.ViewModelLocator.GetParameter("THROTTLEMIN").parameterValue;

            if (throttleVoltage < throttleMin)
                App.ViewModelLocator.GetParameter("THROTTLEPERCENTAGE").parameterValue = 0;
            else if (throttleVoltage >= throttleMin && throttleVoltage <= throttleMax)
                App.ViewModelLocator.GetParameter("THROTTLEPERCENTAGE").parameterValue = (throttleVoltage - throttleMin) / ((throttleMax - throttleMin) / 100);
            else
                App.ViewModelLocator.GetParameter("THROTTLEPERCENTAGE").parameterValue = 100;
        }

        public static void FillInProfileNumberFromSoftwareRev(GoiParameter SOFTWAREREVISION) //just do both here
        {
            float profileNumber;

            if (SOFTWAREREVISION.parameterValue < App.ViewModelLocator.GetParameter("NEWPROFILENUMBER").ImplementedFirmwareVersion)
            {
                if (SOFTWAREREVISION.parameterValue < 4.7f)
                    profileNumber = 32767f;
                else
                    profileNumber = (float)Math.Round((SOFTWAREREVISION.parameterValue % 0.100f) * 1000f);
                App.ViewModelLocator.GetParameter("PARPROFILENUMBER").couldBeDirtyBecauseKBDoneWasNotUsed = true;
                if (profileNumber != 100)
                    App.ViewModelLocator.GetParameter("PARPROFILENUMBER").parameterValue = profileNumber;
                else
                    App.ViewModelLocator.GetParameter("PARPROFILENUMBER").parameterValue = 0;
            }
        }
        public static void FillInNewProfileNumberFromSoftwareRev(GoiParameter unsued) //just do both here
        {
            if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= App.ViewModelLocator.GetParameter("NEWPROFILENUMBER").ImplementedFirmwareVersion)
            {
                App.ViewModelLocator.GetParameter("PARPROFILENUMBER").parameterValue = App.ViewModelLocator.GetParameter("NEWPROFILENUMBER").parameterValue;
            }
        }
        
        public static void CalculateBatteryVoltage(GoiParameter unsued)
        {
             App.ViewModelLocator.GetParameter("HigherOfLogicPowerOrBatteryPostVoltage").parameterValue = (App.ViewModelLocator.GetParameter("VBATVDC").parameterValue > 
                                                                                                            App.ViewModelLocator.GetParameter("LOGICSIDEBATTERYVOLTAGEV").parameterValue) ?
                                                                                                            App.ViewModelLocator.GetParameter("VBATVDC").parameterValue :
                                                                                                            App.ViewModelLocator.GetParameter("LOGICSIDEBATTERYVOLTAGEV").parameterValue;
        }
    }
}
