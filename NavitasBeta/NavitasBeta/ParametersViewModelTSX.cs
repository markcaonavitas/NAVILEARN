using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class ParametersViewModelTSX : ViewModelBase
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

        private bool _isDemoMode = false;
        public bool IsDemoMode
        {
            get { return (_isDemoMode); }
            set
            {
                if (value != _isDemoMode)
                {
                    _isDemoMode = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<SupplierChain> SupplierChains { get; set; }
        public ParametersViewModelTSX()
        {
            //System.Diagnostics.Debug.WriteLine("ParametersViewModel TSX");

            GoiParameterList = FileManager.DeserializeExternalFirstOrInternalXml<ObservableCollection<GoiParameter>>("TSXDictionary.xml", "NavitasBeta.Dictionaries.TSXDictionary.xml");

            SupplierChains = FileManager.DeserializeExternalFirstOrInternalXml<List<SupplierChain>>("TSXSupplierChain.xml", "NavitasBeta.SupplierChain.TSXSupplierChain.xml");

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Units", "MILESORKILOMETERS", new List<string> { "Standard", "Metric" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Speedometer max speed MPH", "SPEEDOMETERMAXSPEED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Speedometer max speed KPH", "SPEEDOMETERMAXSPEEDKPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Dashboard Display Speedometer Range", "SPEEDOMETERMAXSPEEDMKPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "MILESORKILOMETERS")).ReCalculate += UpdateDisplayUnit; //35;
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Speed Uint", "SPEEDUINT", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Threshold one", "THRESHOLDONE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Threshold Two", "THRESHOLDTWO", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Vehicle Speed (MPH)", "SPEED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(-1, 0, 1, 0, 1, "Generic Parameter 254", "KILOMETERSORMILES", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Battery Current (A)", "TSXBATTERYCURRENT", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));

            //Gauge fault bits
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "CONTROLLERERROR", "CONTROLLERERROR", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "COMMERROR", "COMMERROR", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); ////	

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Lock Switch", "LOCKSWITCH", new List<string> { "Locked", "Unlocked" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Key Switch", "KEY", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Foot Switch", "AUX1SWITCH", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Reverse Switch", "REVERSESWITCH", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Forward Switch", "FORWARDSWITCH", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Interlock", "SROSWITCH", new List<string> { "Off", "Connected" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Neutral", "NEUTRAL", new List<string> { "Off", "Connected" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "LOCKED", "LOCKED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "UNLOCKED", "UNLOCKED", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Main Solenoid Output", "MAINSOLENOID", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Buzzer Output", "BUZZEROUTPUT", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            //GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Enable Main Solenoid", "MAINSOLENOIDENABLE", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            //GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Enable Speed Sensor", "SPEEDSENSORENABLE", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            //GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Enable Speed Limits", "SPEEDLIMITENABLE", new List<string> { "Off", "On" }, GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Forward MPH Speed Limit", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Reverse MPH Speed Limit", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Forward KPH Speed Limit", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Reverse KPH Speed Limit", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARSTARTUPERRORS")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARRUNTIMEERRORSLOW")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARRUNTIMEERRORSHIGH")).ReCalculate += CalculateControllerError;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "VEHICLELOCKED")).ReCalculate += CalculateLock;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARMAXMOTORFWDSPEEDLIMIT")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARMAXMOTORREVSPEEDLIMIT")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "TIREDIAMETER")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "REARAXLERATIO")).ReCalculate += CalculateMPHLimits;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARSWITCHSTATES")).ReCalculate += CalculateVehicleSwitches;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARDIGOUT")).ReCalculate += CalculateDigitalOutputs;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARACCESSORYENABLE")).ReCalculate += CalculateAccessoryOptions;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARMOTORRPM")).ReCalculate += CalculateSpeed;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARBATTERYCURRENT")).ReCalculate += CalculateBatteryCurrent;

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "APPVERSION", "APPVERSION", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "APPVERSION")).parameterValueString = App.InfoService.Version;

            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "SPEEDOMETERMAXSPEED")).ReCalculate = CalculateSpeedOmeterMaxSpeed; // 35;
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "DEMOMODE", "DEMOMODE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //	
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "DEMOMODE")).parameterBoolean = false;
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARGENERALOPTIONS")).ReCalculate += CalculateGeneralOptions;

            //GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Model Name Input", "FIRMWARETEST", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Latitude", "LATITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Longitude", "LONGTITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Altitude", "ALTITUDE", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation X", "ORIENTATIONX", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation Y", "ORIENTATIONY", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation Z", "ORIENTATIONZ", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Orientation W", "ORIENTATIONW", GoiParameter.MemoryTypeAndAccess.ReadOrWrite));

            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Controller Temperature (F)", "PARTEMPERATUREF", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.Add(new GoiParameter(ONLY_IN_APP, 1, "Controller Temperature", "PARTEMPERATURECF", GoiParameter.MemoryTypeAndAccess.ReadOrWrite)); //
            GoiParameterList.FirstOrDefault(x => (x.PropertyName == "PARTEMPERATURE")).ReCalculate += CalculateTemperature;
                    }
        public static void CalculateTemperature(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameterTSX("PARTEMPERATUREF").parameterValue = (App.ViewModelLocator.GetParameterTSX("PARTEMPERATURE").parameterValue * 9) / 5 + 32;

            if (App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue != 0)
            {
                  App.ViewModelLocator.GetParameterTSX("PARTEMPERATURECF").parameterValue = App.ViewModelLocator.GetParameterTSX("PARTEMPERATURE").parameterValue;
            }
            else
            {
                App.ViewModelLocator.GetParameterTSX("PARTEMPERATURECF").parameterValue = App.ViewModelLocator.GetParameterTSX("PARTEMPERATUREF").parameterValue;
            }
        }
        public static void UpdateDisplayUnit(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue = float.Parse (DisplaySettings.GetSpeedOmeterMaxSpeed());

            App.ViewModelLocator.GetParameterTSX("PARTEMPERATUREF").parameterValue = (App.ViewModelLocator.GetParameterTSX("PARTEMPERATURE").parameterValue * 9) / 5 + 32;
            App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDKPH").parameterValue = App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue * 1.60934f;

            if (App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue != 0)
            {
                App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDKPH").parameterValue;
                App.ViewModelLocator.GetParameterTSX("PARTEMPERATURECF").parameterValue = App.ViewModelLocator.GetParameterTSX("PARTEMPERATURE").parameterValue;
                App.ViewModelLocator.GetParameterTSX("SPEEDUINT").parameterValueString = "KPH";
                App.ViewModelLocator.GetParameterTSX("THRESHOLDONE").parameterValue = 33;
                App.ViewModelLocator.GetParameterTSX("THRESHOLDTWO").parameterValue = 42;
                App.ViewModelLocator.GetParameterTSX("KILOMETERSORMILES").parameterValueString = "Kilometers";
                App.ViewModelLocator.GetParameterTSX("SPEED").Name = "Vehicle Speed (KPH)";
            }
            else
            {
                App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue;
                App.ViewModelLocator.GetParameterTSX("PARTEMPERATURECF").parameterValue = App.ViewModelLocator.GetParameterTSX("PARTEMPERATUREF").parameterValue;
                App.ViewModelLocator.GetParameterTSX("SPEEDUINT").parameterValueString = "MPH";
                App.ViewModelLocator.GetParameterTSX("THRESHOLDONE").parameterValue = 20;
                App.ViewModelLocator.GetParameterTSX("THRESHOLDTWO").parameterValue = 25;
                App.ViewModelLocator.GetParameterTSX("KILOMETERSORMILES").parameterValueString = "Miles";
                App.ViewModelLocator.GetParameterTSX("SPEED").Name = "Vehicle Speed (MPH)";
            }
        }
        public static void CalculateSpeedOmeterMaxSpeed(GoiParameter unUsed)
        {
            App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDKPH").parameterValue = (Int16)App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue * 1.609344f;

            if (App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue != 0)
            {
                App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDKPH").parameterValue;
            }
            else
            {
                App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEEDMKPH").parameterValue = App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue;
            }
        }
        public static void CalculateMPHLimits(GoiParameter unUsed) //just do both here
        {
            App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue = App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").parameterValue / ((App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2));
            App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue = App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").parameterValue / ((App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2));
            App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH").parameterValue = App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue * 1.609344f;
            App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH").parameterValue = App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue * 1.609344f;
        }

        public static void CalculateSpeed(GoiParameter PARMOTORRPM)
        {
            float inchesPerMinuteToMPH = 60 * 2 * (float)Math.PI * 1.57828e-5f;
            float speed = inchesPerMinuteToMPH * ((float)Math.Abs(PARMOTORRPM.parameterValue) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue/2) / App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue;

            if (speed >= 0.0)
            {
                //      System.Diagnostics.Debug.WriteLine("SPEED");
                if (App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue != 0)
                {
                    App.ViewModelLocator.GetParameterTSX("SPEED").parameterValue = speed * 1.609344f;
                }
                else
                {
                    App.ViewModelLocator.GetParameterTSX("SPEED").parameterValue = speed;
                }
            }
        }

        public static void CalculateBatteryCurrent(GoiParameter TSXBATTERYCURRENT)
        {
            if (App.ViewModelLocator.GetParameterTSX("PARBATTERYCURRENT").parameterValue > 32767)
            {
                //      System.Diagnostics.Debug.WriteLine("SPEED");
                App.ViewModelLocator.GetParameterTSX("TSXBATTERYCURRENT").parameterValue = App.ViewModelLocator.GetParameterTSX("PARBATTERYCURRENT").parameterValue - 65535;
            }
            else
            {
                App.ViewModelLocator.GetParameterTSX("TSXBATTERYCURRENT").parameterValue = App.ViewModelLocator.GetParameterTSX("PARBATTERYCURRENT").parameterValue;
            }
        }

        public static void CalculateControllerError(GoiParameter unUsed)
        {
            float fControllerError = 0.1f;
            if (App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").parameterValue != 0 || App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW").parameterValue != 0 || App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH").parameterValue != 0)
            {
                fControllerError = 1.0f;
            }
            App.ViewModelLocator.GetParameterTSX("CONTROLLERERROR").parameterValue = fControllerError;
        }

        public static void CalculateVehicleSwitches(GoiParameter PARSWITCHSTATES)
        {
            App.ViewModelLocator.GetParameterTSX("LOCKSWITCH").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0080) == 0x0080 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("AUX1SWITCH").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0020) == 0x0020 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("KEY").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0010) == 0x0010 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("REVERSESWITCH").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0004) == 0x0004 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("FORWARDSWITCH").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("SROSWITCH").parameterValue = ((int)PARSWITCHSTATES.parameterValue & 0x0001) == 0x0001 ? 0 : 1;
            App.ViewModelLocator.GetParameterTSX("NEUTRAL").parameterValue = ((int)App.ViewModelLocator.GetParameterTSX("FORWARDSWITCH").parameterValue == (int)App.ViewModelLocator.GetParameterTSX("REVERSESWITCH").parameterValue) ? 1 : 0;
        }


        public static void CalculateDigitalOutputs(GoiParameter digitalOutputs)
        {

            App.ViewModelLocator.GetParameterTSX("MAINSOLENOID").parameterValue = ((int)digitalOutputs.parameterValue & 0x004) == 0x0004 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("BUZZEROUTPUT").parameterValue = ((int)digitalOutputs.parameterValue & 0x0001) == 0x0001 ? 1 : 0;
        }

        public static void CalculateAccessoryOptions(GoiParameter accessoryOptions)
        {
            App.ViewModelLocator.GetParameterTSX("SOLENOIDENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0001) == 0x0001 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("AUX4ENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("DRAGRACEENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0004) == 0x0004 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("BUZZERENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0008) == 0x0008 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("AUX2ENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0010) == 0x0010 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("AUX3ENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0020) == 0x0020 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("SPEEDSENSORENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0040) == 0x0040 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("SPEEDLIMITENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0080) == 0x0080 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("ANTIROLLAWAYENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0100) == 0x0100 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("ANTIROLLAWAYRESET").parameterValue = ((int)accessoryOptions.parameterValue & 0x0200) == 0x0200 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("ROLLAWAYALARMENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0400) == 0x0400 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("REVERSEALARMENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x0800) == 0x0800 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("OVERDRIVEENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x1000) == 0x1000 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("STALLPROTECTIONENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x2000) == 0x2000 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("VARIABLEBRAKINGENABLE").parameterValue = ((int)accessoryOptions.parameterValue & 0x4000) == 0x4000 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("OVERDRIVEALLOWED").parameterValue = ((int)accessoryOptions.parameterValue & 0x8000) == 0x8000 ? 1 : 0;
       }

       public static void CalculateGeneralOptions(GoiParameter GeneralOptions)
        {
            App.ViewModelLocator.GetParameterTSX("CHARGERINTERLOCKDISABLE").parameterValue = ((int)GeneralOptions.parameterValue & 0x0001) == 0x0001 ? 1 : 0;
            App.ViewModelLocator.GetParameterTSX("INMOTIONBUZZER").parameterValue = ((int)GeneralOptions.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
       }

        public static void CalculateLock(GoiParameter unUsed)
        {

            if (App.ViewModelLocator.GetParameterTSX("VEHICLELOCKED").parameterBoolean)
            {
                App.ViewModelLocator.GetParameterTSX("LOCKED").parameterValue = 1;
                App.ViewModelLocator.GetParameterTSX("UNLOCKED").parameterValue = 0;
            }
            else
            {
                App.ViewModelLocator.GetParameterTSX("LOCKED").parameterValue = 0;
                App.ViewModelLocator.GetParameterTSX("UNLOCKED").parameterValue = 1;
            }
        }
        public static void ForceDeadbandChange(GoiParameter PARPRIMARYTHROTTLEFORWARDMIN)
        {
            //QueParameter(new SetParameterEventArgs(131, (float)GetRawValue(0, 5, 0, 4095, PARPRIMARYTHROTTLEFORWARDMIN.parameterValue), null));
            //QueParameter(new SetParameterEventArgs(1, 0);
        }
    }
}
