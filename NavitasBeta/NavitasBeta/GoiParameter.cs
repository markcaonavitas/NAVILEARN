using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Linq;

namespace NavitasBeta
{
    public class GoiParameter : ViewModelBase
    {
        public enum MemoryTypeAndAccess : int { ReadOrWrite, Flash, FlashReadOnly, LoadableOTP };

        public string PropertyName;
        public int Address;
        public bool SubsetOfAddress; //used for bits and enums
        public float Scale;
        public MemoryTypeAndAccess MemoryType;
        public float UserFrom = float.NaN;
        public float UserTo;
        public float DigitalFrom;
        public float DigitalTo;
        public int BitRangeStart;
        public int BitRangeEnd;
        public float MinimumParameterValue;
        public float MaximumParameterValue;
        public float ImplementedFirmwareVersion = 0;
        public int FlashOffset = -1;
        public List<ParameterFile> GroupedParameters;
        public string EnumPointer;
        public List<RootEnumItem> RootEnumList;

        // The XmlSerializer ignores this field.
        public delegate void ReCalculateAppOnlyParamemters(GoiParameter parameter);

        public GoiParameter() { }
        void initBaseTSX(int address, float userFrom, float userTo, float digitalFrom, float digitalTo, string name, string propertyName, MemoryTypeAndAccess memoryType)
        {
            PropertyName = propertyName;
            Address = address;
            Name = name;
            //Scale = scale;
            UserFrom = userFrom;
            UserTo = userTo;
            DigitalFrom = digitalFrom;
            DigitalTo = digitalTo;
            MemoryType = memoryType;
            SubsetOfAddress = false;
            BitRangeStart = 0;
            BitRangeEnd = 15;
            MinimumParameterValue = -16383.0f / 1; //I think one of the from to's decide this
            MaximumParameterValue = 16383.0f / 1;
            if (UserFrom != DigitalFrom)
                System.Diagnostics.Debug.WriteLine("not just a scale");
        }

        void initBaseTAC(int address, float scale, string name, string propertyName, MemoryTypeAndAccess memoryType)
        {
            PropertyName = propertyName;
            Address = address;
            Name = name;
            Scale = scale;
            MemoryType = memoryType;
            SubsetOfAddress = false;
            BitRangeStart = 0;
            BitRangeEnd = 15;
            MinimumParameterValue = -16383.0f / scale;
            MaximumParameterValue = 16383.0f / scale;
            ImplementedFirmwareVersion = 0.0f;
        }

        public GoiParameter(int address, float userFrom, float userTo, float digitalFrom, float digitalTo, string name, string propertyName, MemoryTypeAndAccess memoryType)
        {
            initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName, memoryType);
        }

        [XmlIgnore]
        public ReCalculateAppOnlyParamemters ReCalculate;

        [XmlIgnore]
        public List<GoiParameter> SubsetList = new List<GoiParameter>();

        [XmlIgnore]
        public List<GoiParameter> ParametersPointToThis = new List<GoiParameter>();

        [XmlIgnore]
        public GoiParameter EnumPointerObj;

        public List<string> enumListValue; //public just to get it serializable for now

        private List<string> _enumListName;
        public List<string> enumListName //public just to get it serializable for now
        {
            get { return _enumListName; }
            set { _enumListName = value; OnPropertyChanged(); }
        }

        public GoiParameter(int address, float scale, string name, string propertyName, List<string> enums, List<ParameterFile> listOfParameters)
        {
            initBaseTAC(address, scale, name, propertyName, MemoryTypeAndAccess.ReadOrWrite);
            enumListName = enums;
            GroupedParameters = listOfParameters;
        }

        public GoiParameter(int address, float scale, string name, string propertyName, List<string> enums, MemoryTypeAndAccess memoryType)
        {
            initBaseTAC(address, scale, name, propertyName, memoryType);
            enumListName = enums;
        }
        public GoiParameter(int address, float scale, string name, string propertyName, MemoryTypeAndAccess memoryType)
        {
            initBaseTAC(address, scale, name, propertyName, memoryType);
        }
        float GetScaledValue(float userFrom, float userTo, float digitalFrom, float digitalTo, float RawValue)
        {
            float percentage = (RawValue - digitalFrom) / (digitalTo - digitalFrom);
            return userFrom + (userTo - userFrom) * percentage;
        }
        public float GetRawVal(float value)
        {
            return GetScaledValue(DigitalFrom, DigitalTo, UserFrom, UserTo, value);
        }

        [XmlIgnore]
        public bool bupdate = true;
        [XmlIgnore]
        public bool couldBeDirtyBecauseKBDoneWasNotUsed = true;
        float _rawValue = float.NaN; //force first rawValue != value
        public System.UInt16 rawValue
        {
            set
            {
                //System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                //stopWatch.Start();
                //GoiParameter a = ;
                //stopWatch.Stop();
                //// Get the elapsed time as a TimeSpan value.
                //TimeSpan ts = stopWatch.Elapsed;
                //System.Diagnostics.Debug.WriteLine("bupdatetime = " + ts.ToString());
                if (bupdate) //don't let serial communications update this, it's busy
                {
                    if (float.IsNaN(UserFrom)) //must be a TAC parameter
                    {
                        //check if the current software revision reach the minimum of implemented firmware version
                        if(App.ViewModelLocator.MainViewModel.SOFTWAREREVISION.parameterValue >= ImplementedFirmwareVersion)
                        {
                            _rawValue = (float)(System.Int16)value;
                            parameterValue = (float)_rawValue / Scale;
                        }
                    }
                    else
                    {
                        _rawValue = (float)(System.UInt16)value;

                        if (PropertyName == "OTFSPEED" && _rawValue > DigitalTo)
                            _rawValue = DigitalTo;
                        if (PropertyName == "OTFBRAKE" && _rawValue > DigitalTo)
                            _rawValue = DigitalTo;
                        if (PropertyName == "PARBATTERYCURRENT" && _rawValue > 32767) //we think this is the only one
                            _rawValue -= 65535;

                        parameterValue = GetScaledValue(UserFrom, UserTo, DigitalFrom, DigitalTo, (float)_rawValue);
                    }

                    if (SubsetList.Count != 0 && SubsetOfAddress == false)
                    {
                        foreach (GoiParameter subsetParameter in SubsetList)
                        {
                            int bitValues = (UInt16)Math.Round(parameterValue, 0) >> subsetParameter.BitRangeStart; //math on float could need rounding
                            int bitValuesMask = ~(0xFFFF << (subsetParameter.BitRangeEnd - subsetParameter.BitRangeStart + 1));
                            int rangedValue = (bitValues & bitValuesMask);
                            subsetParameter.rawValue = (System.UInt16)rangedValue;
                        }
                    }
                }
            }
            get { return (System.UInt16)_rawValue; }
        }

        //if setproperty is never called because this value is equal to what the controller is setting
        //the the view value stays blank, setting to .NAN causes the app to crash,to big crash...
        //definitley is a better way....
        //...July 23, initializing at 1234567 is a bad idea, broke things like bootloader see the cart is enabled
        //if it starts in boot mode and never reads the actual values...0 seems bad too, took out the setproperty change check like the old way
        float _parameterValue = 0;
        public float parameterValue
        {
            //TODO: try catch to warn developers of dictionary exceptions/mistakes
            set
            {
                //specific debugging stuff
                //if (this.PropertyName == "PAREMBEDDEDSOFTWAREVER")
                //    System.Diagnostics.Debug.WriteLine("PAREMBEDDEDSOFTWAREVER read " + value.ToString());
                //if (PropertyName == "VEHICLELOCKED" && value == 0)
                //    System.Diagnostics.Debug.WriteLine("Unlocked ");
                //if (PropertyName == "LOCKED")
                //    System.Diagnostics.Debug.WriteLine(" ");
                //if (PropertyName == "UNLOCKED")
                //    System.Diagnostics.Debug.WriteLine(" ");

                if (bupdate) //don't let serial communications update or GUI this until done, it's busy
                {
                    if (SetProperty(ref _parameterValue, value, couldBeDirtyBecauseKBDoneWasNotUsed))
                    {
                        //if (this.PropertyName == "InvertDigitalInput")
                        //    System.Diagnostics.Debug.WriteLine("InvertDigitalInput read " + value.ToString());
                        couldBeDirtyBecauseKBDoneWasNotUsed = false;
                        parameterBoolean = System.Convert.ToBoolean(value);
                        parameterValueString = Math.Round(value, 2).ToString();
                        parameterValueInt = (int)value;
                        
                        //BE CAREFUL:
                        //If we forget to do this way, all of the paramters which contain a variable points to this parameter, will not get update
                        if (ParametersPointToThis.Count != 0)
                        {
                            foreach(var p in ParametersPointToThis)
                            {
                                p.couldBeDirtyBecauseKBDoneWasNotUsed = true;
                            }
                        }

                        if (EnumPointerObj != null)
                        {
                            enumListName = EnumPointerObj.RootEnumList[(int)EnumPointerObj.parameterValue].NestedEnumListName;
                            Name = EnumPointerObj.RootEnumList[(int)EnumPointerObj.parameterValue].name;
                            Description = EnumPointerObj.RootEnumList[(int)EnumPointerObj.parameterValue].description;

                            if (EnumPointerObj.RootEnumList[(int)EnumPointerObj.parameterValue].NestedEnumListValue != null)
                                enumListValue = EnumPointerObj.RootEnumList[(int)EnumPointerObj.parameterValue].NestedEnumListValue;
                        }

                        if (enumListName != null && enumListName.Count > 0)
                        {
                            if (enumListValue != null && enumListValue.Count > 0)
                            {
                                int enumerationValue = (UInt16)Math.Round(value, 0) >> BitRangeStart; //math on float could need rounding
                                int rangeValueMask = ~(0xFFFF << (BitRangeEnd - BitRangeStart));
                                enumerationValue = (enumerationValue & rangeValueMask);
                                var enumListValueIndex = enumListValue.IndexOf(enumerationValue.ToString());
                                if (enumListValueIndex == -1)
                                    parameterEnumString = "Unknown";
                                else
                                    parameterEnumString = enumListName[enumListValueIndex];
                            }
                            else //origninal continuous (0,1,2,3...) enumerations so you don't need enumListValues
                                parameterEnumString = enumListName[(int)value];

                        }
                        ReCalculate?.Invoke(this);
                    }
                }
            }
            get { return _parameterValue; }
        }
        string _parameterValueString;
        [XmlIgnore]
        public string parameterValueString
        {
            set { if (SetProperty(ref _parameterValueString, value)) { } }
            get { return _parameterValueString; }
        }
        int _parameterValueInt;
        [XmlIgnore]
        public int parameterValueInt
        {
            //            set { if (SetProperty(ref _parameterValueHex, value.ToString("X")) { } }
            set { if (SetProperty(ref _parameterValueInt, value)) { } }
            get { return _parameterValueInt; }
        }
        bool _parameterBoolean;
        [XmlIgnore]
        public bool parameterBoolean
        {
            set
            {
                if (bupdate)
                {
                    SetProperty(ref _parameterBoolean, value);
                }
            }
            get { return _parameterBoolean; }
        }
        string _parameterEnumString;
        [XmlIgnore]
        public string parameterEnumString
        {
            set { SetProperty(ref _parameterEnumString, value); }
            get { return _parameterEnumString; }
        }
        string _name;
        public string Name
        {
            set { if (SetProperty(ref _name, value)) { } }
            get { return _name; }
        }

        string _description;
        public string Description
        {
            set { if (SetProperty(ref _description, value)) { } }
            get { return _description; }
        }

        string _troubleshooting;
        public string Troubleshooting
        {
            set { if (SetProperty(ref _troubleshooting, value)) { } }
            get { return _troubleshooting; }
        }
        bool _selectedForDatalogging = false;
        [XmlIgnore]
        public bool SelectedForDatalogging
        {
            set { if (SetProperty(ref _selectedForDatalogging, value)) { } }
            get { return _selectedForDatalogging; }
        }

        bool _selectedForInvisibleDatalogging = false;
        [XmlIgnore]
        public bool SelectedForInvisibleDatalogging
        {
            set { if (SetProperty(ref _selectedForInvisibleDatalogging, value)) { } }
            get { return _selectedForInvisibleDatalogging; }
        }
    }

    public class RootEnumItem
    {
        public List<string> NestedEnumListName { get; set; }
        public List<string> NestedEnumListValue { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
