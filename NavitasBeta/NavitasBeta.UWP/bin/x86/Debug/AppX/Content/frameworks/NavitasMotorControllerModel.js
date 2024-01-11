//Good lesson on vanilla javascript which now handles things only jquery could do
//Clarity on javascript recent updates instead of jQuery; https://tobiasahlin.com/blog/move-from-jquery-to-vanilla-javascript/
//Best Practice for speed?
// var x1 = {};           // new object
// var x2 = "";           // new primitive string
// var x3 = 0;            // new primitive number
// var x4 = false;        // new primitive boolean
// var x5 = [];           // new array object
// var x6 = /()/;         // new regexp object
// var x7 = function(){}; // new function object

//******************************************************** Note: *****************************************************************************//
//This is a model.
//The model contains/uses a dictionary to describe all Motor Controller I/O and internal variables and features/functions
//Models are good in general and are needed for more things than MVVM/MVC/MVAnything
//But... just in case, it contains a hook for ViewModels.
//There are two hooks:
// 1). A PropertyChanged event which allows view things to bind to changes in the Model
//      Usage like ...this.GetParameter("ROTORRPM").addEventListener("PropertyChanged", UpdateRPM, false);
//                    if ROTORRPM changes then UpdateRPM will be automatically called instead of some RPM thing polling the value
//
// 2). Remove the below ability because it is the wrong concept and writing parmaterValue is wrong.
//      Usage like ...this.GetParameter("THROTTLEMIN").addBindingToThisSource(document.getElementById("Rec1PresentValue"), "value", "keyup");
//                    goes the other way if an external event like "keyup" happens the element value is sent to parameterValue WHICH IS WRONG it should call
//                    a write parameter function an then that will update his display through usage 1)
class NavitasMotorControllerModel //public class NavitasMotorControllerModels
{//TODO: this is called a model but has a .Model field just to confuse people

    constructor(communicationsInterface)
    {
        const _this = this;
        this.Communications = communicationsInterface;
        if (typeof HasBrowserCommandInterface != 'undefined') this.Communications.protocal.ModelReference = this;
        this._Tac = null;
        this._Tsx = null;
        this._Model = null;
        this.ModelType = "";
        this.ScopeBlockSchedule = Number.NaN;
        Object.defineProperty(this, "Model",
            {
                set: function (value)
                {
                    if (value.includes("TAC"))
                    {
                        this._Tac = new Tac(this);
                        this._Model = this._Tac;
                    }
                    else if (value.includes("TSX"))
                    {
                        this._Tsx = new Tsx(this);
                        this._Model = this._Tsx;
                    }
                    console.log(this.ModelType + " Type Determined");
                },
                get: function () //get { return _parameterValue; }
                {
                    return this._Model;
                }
            });
        this.CommandHandler = {}
        //maybe this can use some events in the future?
        //like: _this.dispatchEvent(_this.DeviceTypeDeterminedDelegate); //is this the best way of bubbling up?
        //this.DeviceTypeDeterminedDelegate = new CustomEvent("DeviceTypeDetermined", // public event EventHandler<System.EventArgs> DeviceTypeDetermined = delegate { };
        //    {
        //        detail:
        //        {
        //            ModelType: _this.ModelType, //_this.DeviceHasReplied,
        //            time: new Date(),
        //        },
        //        bubbles: true,
        //        cancelable: true
        //    });
        // this.Communications.protocal.addEventListener("DeviceTypeDetermined", function (event)
        // { //events are used like a c# delegate where code would look like: this.Communications.protocal.DeviceTypeDetermined += a function
        //     _this.ModelType = this.ModelType; //this is the object that called the event
        //     console.log(this.ModelType + " Type Determined");
        //    });
            // this.Communications.protocal.addEventListener("ScopeGetBlockResponded", function (event)
            // { //events are used like a c# delegate where code would look like: this.Communications.protocal.ScopeGetBlockResponse += a function
            //     console.log(this.ModelType + " Scope Block " + this.ScopeBlockSchedule + " Received");
        // }
        this.JsonCommandCompleted = true;
    }
    GetParameter(value) //(string name)
    {
        var parameter = null;
        try
        {
            if (typeof value === 'string')
                parameter = this.Model.ParameterList.FirstOrDefault(x => (x.PropertyName === value));
            else if (typeof value === 'number') //hope it is an address, subsets in dictionary have the same address so search for the parent
                parameter = this.Model.ParameterList.FirstOrDefault(x => (x.Address === value && x.SubsetOfAddress === false));

            if (parameter == null)
                throw new InvalidOperationException("GetParameter can't find " + value);
        }
        catch (ex) //(Exception ex)
        {
            //System.Diagnostics.Debug.WriteLine(ex.ToString());
            console.log(ex.toString());
        }
        return parameter;
    }
    async JsonCommands(data, timeout_ms = 3000, enableReceiveEvent = false, interval = 100, test = () =>
    {
        if (this.JsonCommandCompleted) return true;
    })
    {
        //Json Commands are executed by
        // 1)Sending it straight to the mobile App through the xamarin interface invokeCSharpAction() routine
        // 2)Sending the command to the simulator invokeSimulatorAction for easier UI development
        // 3)Sending through the Javascript Interface invokeJavascriptAction() which connects a browser to serial or ble ports through a websocket
        //Commands are:
        // Read Contiuously:
        // Read:
        // Write:
        // the ones below are not yet supported by invokeJavascriptAction yet
        // ReadScopeDataBlock:
        // ControlVerified:
        // GetAppParameters:
        // Close
        return new Promise(async (resolve, reject) =>
        {
            //just showing a check and a reject method for future knowledge and good practice to help newer developers
            if (typeof (timeout_ms) != "number") reject("this.timeOut argument not a number in this.JsonCommands(selector, timeout_ms)");

            this.JsonCommandCompleted = false;

            //MyConsoleLog("time into invokeCSharpAction" + (new Date().getTime()/1000%100).toFixed(3));
            if (typeof HasBrowserCommandInterface != 'undefined')
            {
                this.Communications.protocal.EnableReceiveEvent = enableReceiveEvent;
                this.CommandHandler.invokeJavascriptAppAction(data, interval).then((value) => {
                    if(value) {
                        resolve(true);
                    }
                    else {
                        console.log('%c' + 'this.timeOut, failed waiting for: ' + JSON.stringify(data), 'color:#cc2900');
                        resolve(false);
                        //or uncomment above a below line to generate reject
                        // reject("Reading from Navitas App timed out");
                    }
                });
            }
            else if (typeof invokeCSharpAction == 'function')
            {
                console.log("invokeCSharpAction data: " + JSON.stringify(data))
                //Navitas Mobile App call which is inserted globally by the xamarin App
                invokeCSharpAction(JSON.stringify(data)); //also responds with CompleteTheJsonCommand(some stuff) which sets JsonCommandCompleted = true

                //invokeCSharpActionForVisualResponseTestingOnly(data);
                //above timed at around 10-20 ms
                //leave time stamp methods here so we remember how
                //MyConsoleLog("time out of invokeCSharpAction" + (new Date().getTime()/1000%100).toFixed(3));

                let pollingPeriod_ms = 10;
                let result;
                // wait until the result is truthy, or this.timeOut
                while (!result || result.length === 0) { // for non arrays, length is undefined, so != 0
                    //if (timeout_ms % 1000 < pollingPeriod_ms) console.log('%c'+'waiting for: '+ test,'color:#809fff' );
                    if ((timeout_ms -= pollingPeriod_ms) < 0) {
                        console.log('%c' + 'this.timeOut, failed waiting for: ' + JSON.stringify(data), 'color:#cc2900');
                        //resolve(false);
                        //or uncomment above a below line to generate reject
                        reject("Reading from Navitas App timed out");
                    }
                    await sleep(pollingPeriod_ms);
                    result = test(); // run the test and update result variable
                }
                // return result if test passed
                //console.log('Passed: ', test);
                resolve(result);
            }
            else
            {
                //invokeCSharpAction was not inserted (probably because this is not running in the App) so run this for testing
                this.CommandHandler.invokeSimulatedAction(data, interval)

                //invokeCSharpActionForVisualResponseTestingOnly(data);
                //above timed at around 10-20 ms
                //leave time stamp methods here so we remember how
                //MyConsoleLog("time out of invokeCSharpAction" + (new Date().getTime()/1000%100).toFixed(3));

                let pollingPeriod_ms = 10;
                let result;
                // wait until the result is truthy, or this.timeOut
                while (!result || result.length === 0)
                { // for non arrays, length is undefined, so != 0
                    //if (timeout_ms % 1000 < pollingPeriod_ms) console.log('%c'+'waiting for: '+ test,'color:#809fff' );
                    if ((timeout_ms -= pollingPeriod_ms) < 0)
                    {
                        console.log('%c' + 'this.timeOut, failed waiting for: ' + JSON.stringify(data), 'color:#cc2900');
                        //resolve(false);
                        //or uncomment above a below line to generate reject
                        reject("Reading from Navitas App timed out");
                    }
                    await sleep(pollingPeriod_ms);
                    result = test(); // run the test and update result variable
                }
                // return result if test passed
                //console.log('Passed: ', test);
                resolve(result);
            }
        }).catch((error) =>
        {
            console.log(`JsonCommands error: ${error}`);
        });
    }
    CompleteTheJsonCommand(parameterAndValueList)
    {//an empty parameterAndValueList means it was call from the Javascript interface not C#
        parameterAndValueList.forEach((element, index, array) =>
        {
            if (element.parameterTimeAndValuePairList == null)
            {
                this.GetParameter(element.PropertyName).parameterValue = element.parameterValue;
            }
            else
            {
                this.Model.ScopeDataArray[0] = {
                    ProperName: element.PropertyName,
                    parameterTimeAndValuePairList: element.parameterTimeAndValuePairList
                };
            }
            //MyConsoleLog(element.PropertyName + " " + element.parameterValue);
            //console,log(element.PropertyName + " " + element.parameterValue);
        });
        this.JsonCommandCompleted = true;
        //MyConsoleLog("time out of CompleteTheJsonCommand " + (new Date().getTime()/1000%100).toFixed(3));
        return ("Function Called");
    }
    UnsolicitedCompleteTheJsonCommand(parameterAndValueList)
    {
        //MyConsoleLog("time into CompleteTheJsonCommand " + (new Date().getTime()/1000%100).toFixed(3));
        parameterAndValueList.forEach((element, index, array) =>
        {
            if (element.parameterTimeAndValuePairList == null)
            {
                this.GetParameter(element.PropertyName).parameterValue = element.parameterValue;
            }
            else
            {
                this.Model.ScopeDataArray[0] = {
                    ProperName: element.PropertyName,
                    parameterTimeAndValuePairList: element.parameterTimeAndValuePairList
                };
            }
            //MyConsoleLog(element.PropertyName + " " + element.parameterValue);
            //console,log(element.PropertyName + " " + element.parameterValue);
        });
        //JsonCommandCompleted = true; ie Unsolicited means continuous update from App
        //MyConsoleLog("time out of CompleteTheJsonCommand " + (new Date().getTime()/1000%100).toFixed(3));
        return ("Function Called");
    }

    HtmlAppCommand(command)
    {
        if(command == "Pause")
            App.Pause = true;
        else if (command == "Resume")
            App.Pause = false;
        console.log(command + " Executed");
        return ("Function Called");
    }
}

MemoryTypeAndAccess = {
    ReadOrWrite: 0,
    Flash: 1,
    FlashReadOnly: 2
} //static for some reason exposes this, C# was public enum MemoryTypeAndAccess : int { ReadOrWrite, Flash };

class GoiParameter extends ViewModelHook //ObservableObject //public class GoiParameter : ViewModelBase
{
    constructor(parentModel, address, scale, userFrom, userTo, digitalFrom, digitalTo, name, propertyName, memoryType, subsetOfAddress, bitRangeStart, bitRangeEnd, minParamValue, maxParamValue, implementedVersion, enumListValue, enumListName, description)
    {
        super(); //propertyName, callBack);
        const _this = this;
        this.observers = [];
        this.parentModel = parentModel;
        this.elementBindings = [];

        //iNotify type callback on property
        //if(callBack != null)
        // _this.observers.push(callBack);

        //additional callbacks can be added by calling the object.Observe()
        this.Observe = function (notifyCallback)
        {
            _this.observers.push(notifyCallback);
        }

        //html element specific addEventListener *************Read ViewModelHook notes and fix then remove this are fix concept
        this.addBindingToThisSource = function (element, attribute, event)
        {
            var binding = {
                element: element,
                attribute: attribute,
            }
            if (event)
            {
                element.addEventListener(event, function (event)
                {
                    _this.parameterValue = element[attribute];
                })
                binding.event = event
            }
            _this.elementBindings.push(binding)
            element[attribute] = _this.parameterValue;

            return _this
        }


        //void initBaseTSX(int address, float userFrom, float userTo, float digitalFrom, float digitalTo, string name, string propertyName, MemoryTypeAndAccess memoryType)
        function initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName, memoryType, subsetOfAddress, bitRangeStart, bitRangeEnd, minParamValue, maxParamValue, implementedVersion, enumListValue, enumListName, description)
        {
            //if (propertyName == "Parameter257")
                //console.log("breakpoint spot 1" + propertyName);
            _this.PropertyName = propertyName;
            _this.Address = parseInt(address);
            _this.Name = name;
            _this.UserFrom = parseInt(userFrom);
            _this.UserTo = parseInt(userTo);
            _this.DigitalFrom = parseInt(digitalFrom);
            _this.DigitalTo = parseInt(digitalTo);
            _this.MemoryType = typeof memoryType == "number" ? memoryType : MemoryTypeAndAccess[memoryType];
            _this.SubsetOfAddress = (subsetOfAddress === "true");
            _this.BitRangeStart = parseInt(bitRangeStart);
            _this.BitRangeEnd = parseInt(bitRangeEnd);
            _this.MinimumParameterValue = parseFloat(minParamValue);
            _this.MaximumParameterValue = parseFloat(maxParamValue);
            _this.ImplementedFirmwareVersion = parseFloat(implementedVersion);
            _this.enumListValue = parseInt(enumListValue);
            _this.enumListName = enumListName;
            _this.Description = description;
        }

        if (scale == null)
            initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName, memoryType, subsetOfAddress, bitRangeStart, bitRangeEnd, minParamValue, maxParamValue, implementedVersion, enumListValue, enumListName, description);

        function initBaseTAC(address, scale, name, propertyName, memoryType, subsetOfAddress, bitRangeStart, bitRangeEnd, minParamValue, maxParamValue, implementedVersion, enumListValue, enumListName, description)
        {
            // if (propertyName == "Parameter257")
            //     console.log("breakpoint spot 1" + propertyName);
            _this.PropertyName = propertyName; //why do i have to you this, will the .bind function fix this
            _this.Address = parseInt(address);
            _this.Name = name;
            _this.Scale = parseInt(scale);
            _this.MemoryType = typeof memoryType == "number" ? memoryType : MemoryTypeAndAccess[memoryType];
            _this.SubsetOfAddress = (subsetOfAddress === "true");
            _this.BitRangeStart = parseInt(bitRangeStart);
            _this.BitRangeEnd = parseInt(bitRangeEnd);
            _this.MinimumParameterValue = parseFloat(minParamValue);
            _this.MaximumParameterValue = parseFloat(maxParamValue);
            _this.ImplementedFirmwareVersion = parseFloat(implementedVersion);
            _this.enumListValue = parseInt(enumListValue);
            _this.enumListName = enumListName;
            _this.Description = description;
        }

        if (scale != null)
            initBaseTAC(address, scale, name, propertyName, memoryType, subsetOfAddress, bitRangeStart, bitRangeEnd, minParamValue, maxParamValue, implementedVersion, enumListValue, enumListName, description);

        // public GoiParameter(int address, double userFrom, double userTo, double digitalFrom, double digitalTo, string name, string propertyName, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName,memoryType);
        // }
        // [XmlIgnore]
        // public ReCalculateAppOnlyParamemters ReCalculate;
        // public GoiParameter(int address, double scale, string name, string propertyName, ReCalculateAppOnlyParamemters method, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTAC(address, scale, name, propertyName,memoryType);
        //     ReCalculate = method;
        // }
        // public GoiParameter(int address, double userFrom, double userTo, double digitalFrom, double digitalTo, string name, string propertyName, ReCalculateAppOnlyParamemters method, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName,memoryType);
        //     ReCalculate = method;
        // }

        // public List<string> enumListValue; //public just to get it serializable for now
        // public List<string> enumListName; //public just to get it serializable for now
        // public GoiParameter(int address, double userFrom, double userTo, double digitalFrom, double digitalTo, string name, string propertyName, List<string> enums, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTSX(address, userFrom, userTo, digitalFrom, digitalTo, name, propertyName,memoryType);
        //     enumListName = enums;
        // }

        // public GoiParameter(int address, double scale, string name, string propertyName, List<string> enums, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTAC(address, scale, name, propertyName,memoryType);
        //     enumListName = enums;
        // }

        // public GoiParameter(int address, double scale, string name, string propertyName, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTAC(address, scale, name, propertyName, memoryType);
        // }

        // public GoiParameter(int address, bool subsetOfAddress, int bitRangeStart, int bitRangeEnd, double scale, string name, string propertyName, MemoryTypeAndAccess memoryType)
        // {
        //     initBaseTAC(address, scale, name, propertyName, memoryType);
        //     SubsetOfAddress = subsetOfAddress;
        //     BitRangeStart = bitRangeStart;
        //     BitRangeEnd = bitRangeEnd;
        //     MinimumParameterValue = 0;
        //     MaximumParameterValue = (Math.Pow(2, (bitRangeEnd - bitRangeStart + 1)) - 1) / scale;
        // }

        function GetScaledValue(userFrom, userTo, digitalFrom, digitalTo, RawValue)
        {
            let percentage = (RawValue - digitalFrom) / (digitalTo - digitalFrom);
            return userFrom + (userTo - userFrom) * ((RawValue - digitalFrom) / (digitalTo - digitalFrom));
        }

        //function GetScaledValue(userFrom, userTo, digitalFrom, digitalTo, RawValue, digits)
        //{
        //    let percentage = (RawValue - digitalFrom) / (digitalTo - digitalFrom);
        //    return Math.round(userFrom + (userTo - userFrom) * percentage, digits);
        //}

        //function GetRawVal(value)
        //{
        //    return GetScaledValue(DigitalFrom, DigitalTo, UserFrom, UserTo, value);
        //}

        // [XmlIgnore]
        this.bupdate = true;
        this.couldBeDirtyBecauseKBDoneWasNotUsed = true;
        this.rawValueToBeWritten = Number.NaN; //force first rawValue != value
        this._rawValue = Number.NaN; //force first rawValue != value
        Object.defineProperty(this, "rawValue", //just to get setter and getter the "=" overload ability
            {
                set: function (value)
                {
                    if (this.bupdate && (this.MemoryType == MemoryTypeAndAccess.ReadOrWrite || this.MemoryType == MemoryTypeAndAccess.Flash)) //don't let serial communications update this, it's busy
                    {
                        //if (this.PropertyName == "Parameter257")
                        //    console.log("breakpoint spot 1" + this.propertyName);
                        if (isNaN(this.UserFrom)) //must be a TAC parameter
                        {
                            this._rawValue = value;
                            this.parameterValue = this._rawValue / this.Scale;
                        }
                        else
                        {
                            this._rawValue = value;
                            if (this.PropertyName == "Parameter257")
                                console.log("breakpoint spot 1" + this.propertyName);

                            //if (PropertyName == "OTFSPEED" && _rawValue > DigitalTo)
                            //    _rawValue = DigitalTo;
                            //if (PropertyName == "OTFBRAKE" && _rawValue > DigitalTo)
                            //    _rawValue = DigitalTo;
                            //if (PropertyName == "PARBATTERYCURRENT" && _rawValue > 32767) //we think this is the only one
                            //    _rawValue -= 65535;

                            this.parameterValue = GetScaledValue(this.UserFrom, this.UserTo, this.DigitalFrom, this.DigitalTo, this._rawValue);
                        }
                    }
                },
                get: function (value) { return this._rawValue; }
            });
        this._parameterValue = null; //double _parameterValue = 0;
        Object.defineProperty(this, "parameterValue", //just to get SetProperty for view model notifications
            {
                set: function (value)
                {
                    //if (this.PropertyName == "Parameter257")
                    //    console.log("breakpoint spot 1" + this.propertyName);
                    this._parameterValue = value;
                    //Here is the all important ViewModel hook
                    this.SetProperty(this, value, "parameterValue");
                    //     //             else
                    //     //                 SetProperty(ref _parameterValue, GetScaledValue(UserFrom, UserTo, DigitalFrom, DigitalTo, (double)value));

                    //     //             parameterValueString = Math.Round(_parameterValue, 2).ToString();

                    //     //             //specific debugging stuff
                    //     //             //if (PropertyName == "ENCODERA")
                    //     //             //    System.Diagnostics.Debug.WriteLine("REVERSEENCODER " + value.ToString());
                    // if (enumListName != null && enumListName.Count > 0 && value < enumListName.Count)
                    // {
                    //     if (enumListValue != null && enumListValue.Count == enumListName.Count)
                    //     {
                    //         int enumerationValue = (Int16) value >> BitRangeStart;
                    //         int rangeValueMask = ~(0xFFFF << (BitRangeEnd - BitRangeStart));
                    //         enumerationValue = (enumerationValue & rangeValueMask);
                    //         var enumListValueIndex = enumListValue.IndexOf(enumerationValue.ToString());
                    //         if (enumListValueIndex == -1)
                    //             parameterEnumString = "Mistake in Dictionary";
                    //         else
                    //             parameterEnumString = enumListName[enumListValueIndex];
                    //     }
                    //     else //origninal continuous (0,1,2,3...) enumerations
                    //         parameterEnumString = enumListName[(int) value];

                    // }
                    //     //             if (value == 0)
                    //     //             {
                    //     //                 //specific debugging stuff
                    //     //                 //if (PropertyName == "REVERSEENCODER")
                    //     //                 //    System.Diagnostics.Debug.WriteLine("REVERSEENCODER " + value.ToString());
                    //     //                 parameterBoolean = false;

                    //     //             }
                    //     //             else
                    //     //             {
                    //     //                 //specific debugging stuff
                    //     //                 //if (PropertyName == "REVERSEENCODER")
                    //     //                 //    System.Diagnostics.Debug.WriteLine("REVERSEENCODER " + value.ToString());
                    //     //                 parameterBoolean = true;
                    //     //             }

                    //ReCalculate?.Invoke(this);
                    //Notify(call) callback list that the variable has changed
                    for (var i = 0; i < this.observers.length; i++)
                        this.observers[i](_this.parentModel, _this);

                    for (var i = 0; i < this.elementBindings.length; i++)
                    {
                        this.elementBindings[i].element[this.elementBindings[i].attribute] = value;
                    }
                },
                get: function () //get { return _parameterValue; }
                {
                    return this._parameterValue;
                }
                //     // }
                //     // string _parameterValueString;
                //     // [XmlIgnore]
                //     // public string parameterValueString
                //     // {
                //     //     set { if (SetProperty(ref _parameterValueString, value)) { } }
                //     //     get { return _parameterValueString; }
                //     // }
                //     // bool _parameterBoolean = false;
                //     // [XmlIgnore]
                //     // public bool parameterBoolean
                //     // {
                //     //     set
                //     //     {
                //     //         if (bupdate)
                //     //         {
                //     //             SetProperty(ref _parameterBoolean, value);
                //     //         }
                //     //     }
                //     //     get { return _parameterBoolean; }
                //     // }
                //     // string _parameterEnumString;
                //     // [XmlIgnore]
                //     // public string parameterEnumString
                //     // {
                //     //     set { SetProperty(ref _parameterEnumString, value); }
                //     //     get { return _parameterEnumString; }
                //     // }
                //     // string _name;
                //     // public string Name
                //     // {
                //     //     set { if (SetProperty(ref _name, value)) { } }
                //     //     get { return _name; }
                //     // }
                //     // bool _checked = false;
                //     // [XmlIgnore]
                //     // public bool CHECKED
                //     // {
                //     //     set { if (SetProperty(ref _checked, value)) { } }
                //     //     get { return _checked; }
                //     // }
                // //}
            }
        );
    }
}

class Tac //: ViewModelBase
{

    constructor(parentModel) //public Tac()
    {
        const _this = this;

        this.ONLY_IN_APP = -1;
        this.ParameterList = new List();
        if (typeof HasBrowserCommandInterface != 'undefined') {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', '../Dictionaries/TACDictionary.xml', false);
            xhr.send();
            if (xhr.readyState === 4 && xhr.status === 200) {
                TACDictionaryAsXmlString = xhr.responseText;
            }
            else {
                console.log(`Attempt read dictionary failed. XMLHttpRequest.readyState = ${xhr.readyState}, XMLHttpRequest.status = ${xhr.status}`)
            }
        }
        let dictionary = $.xml2json(TACDictionaryAsXmlString);
        dictionary.GoiParameter.forEach(function (item, index)
        {
            _this.ParameterList.push(new GoiParameter(parentModel, item.Address, item.Scale, null, null, null, null, item.Name, item.PropertyName, item.MemoryType, item.SubsetOfAddress, item.BitRangeStart, item.BitRangeEnd, item.MinimumParameterValue, item.MaximumParameterValue, item.ImplementedFirmwareVersion, item.enumListValue, item.enumListName, item.Description));
        });

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Vehicle Speed (MPH)", "SPEED", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        //this.ParameterList.FirstOrDefault(x => (x.PropertyName == "SPEED")).ReCalculate = fixSyncFusionSpeedometerException;//+= fixSyncFusionSpeedometerException;

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Kilometers or Miles", "KILOMETERSORMILES", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        //Gauge logic bits
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "FORWARD", "FORWARD", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "REVERSE", "REVERSE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "NEUTRAL", "NEUTRAL", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "KEYENABLE", "KEYENABLE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        //Gauge fault bits
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "CONTROLLERERROR", "CONTROLLERERROR", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "COMMERROR", "COMMERROR", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "LOCKED", "LOCKED", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "UNLOCKED", "UNLOCKED", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Forward MPH Speed Limit", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Reverse MPH Speed Limit", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "OTFSPEED", "OTFSPEED", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "OTFDECEL", "OTFDECEL", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "OTFACCEL", "OTFACCEL", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Calibrate Voltage input", "INPUT_VOLTAGE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "InvertDigitalInput")).ReCalculate += CalculateToggleDigitialInputBits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "Options")).ReCalculate += CalculateOptionBits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "FORWARDSPEED")).ReCalculate += CalculateOldOTFSettings;
        this.ParameterList.FirstOrDefault(x => (x.PropertyName == "ROTORRPM")).Observe(this.CalculateSpeed); //.ReCalculate += CalculateSpeed;
        this.ParameterList.FirstOrDefault(x => (x.PropertyName == "SWITCHBITS")).Observe(this.CalculateForwardNeutralReverseBits); //.ReCalculate += CalculateForwardNeutralReverseBits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPONEFAULTS")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPTWOFAULTS")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPTHREEFAULTS")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "GROUPFOURFAULTS")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "VEHICLELOCKED")).ReCalculate += CalculateLock;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "FWDLMTRPM")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "RVSLMTRPM")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "TIREDIAMETER")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "REARAXLERATIO")).ReCalculate += CalculateMPHLimits;

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "APPVERSION", "APPVERSION", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "APPBUILD", "APPBUILD", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        //            this.ParameterList.FirstOrDefault(x => (x.PropertyName == "APPVERSION")).parameterValueString = Appp.InfoService.Version;
        //            this.ParameterList.FirstOrDefault(x => (x.PropertyName == "APPBUILD")).parameterValueString = Appp.InfoService.Build;
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Latitude", "LATITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Longitude", "LONGITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Altitude", "ALTITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Acceleration (m/s^2)", "ACCELERATION", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Torque (Nm)", "TORQUE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Throttle (%)", "THROTTLEPERCENTAGE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation X", "ORIENTATIONX", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation Y", "ORIENTATIONY", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation Z", "ORIENTATIONZ", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation W", "ORIENTATIONW", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ScopeDataArray = new Array(
            {
                PropertyName: "",
                parameterTimeAndValuePairList: []
            },
            {},
            {},
            {}); //model scope buffer

        parentModel.ModelType = "TAC";
    }

    fixSyncFusionSpeedometerException(unUsed) //public static void fixSyncFusionSpeedometerException(GoiParameter unUsed)
    {
        // if (this..GetParameter("SPEED").parameterValue > this..GetParameter("SPEEDOMETERMAXSPEED").parameterValue)
        //     this..GetParameter("SPEEDOMETERMAXSPEED").parameterValue = this..GetParameter("SPEED").parameterValue;
    }

    // public static void CalculateOldOTFSettings(GoiParameter unUsed)
    // {
    //     this..GetParameter("OTFSPEED").parameterValue = this..GetParameter("FORWARDSPEED").parameterValue / this..GetParameter("FWDLMTRPM").parameterValue * 100;
    //     this..GetParameter("OTFDECEL").parameterValue = this..GetParameter("ScalerForMuchFasterNonVehicleAccelAndDecel_PUq0").parameterValue / this..GetParameter("ScalerForMuchFasterNonVehicleAccelAndDecel_PUq0").parameterValue * 100;
    //     this..GetParameter("OTFACCEL").parameterValue = this..GetParameter("RISERPMSEC").parameterValue / this..GetParameter("RISEHZSEC").parameterValue * 100;
    // }

    //public static void
    CalculateForwardNeutralReverseBits(parentModel, switchbits)
    {
        if ((switchbits.parameterValue & 0x0006) == 0x0006) //both direction switches on
        {
            parentModel.GetParameter("FORWARD").parameterValue = 0;
            parentModel.GetParameter("REVERSE").parameterValue = 0;
            parentModel.GetParameter("NEUTRAL").parameterValue = 1;
        }
        else if ((switchbits.parameterValue & 0x0002) == 0x0002) // forward
        {
            parentModel.GetParameter("FORWARD").parameterValue = 1;
            parentModel.GetParameter("REVERSE").parameterValue = 0;
            parentModel.GetParameter("NEUTRAL").parameterValue = 0;
        }
        else if ((switchbits.parameterValue & 0x0004) == 0x0004) // reverse
        {
            parentModel.GetParameter("FORWARD").parameterValue = 0;
            parentModel.GetParameter("REVERSE").parameterValue = 1;
            parentModel.GetParameter("NEUTRAL").parameterValue = 0;

        }
        else
        {
            parentModel.GetParameter("FORWARD").parameterValue = 0;
            parentModel.GetParameter("REVERSE").parameterValue = 0;
            parentModel.GetParameter("NEUTRAL").parameterValue = 1;

        }
        if ((switchbits.parameterValue & 0x0010) == 0x0010)
        {
            parentModel.GetParameter("KEYENABLE").parameterValue = 1;
        }
        else
        {
            parentModel.GetParameter("KEYENABLE").parameterValue = 0;

        }
    }

    CalculateSpeed(parentModel, unUsed) //public static void CalculateSpeed(GoiParameter unUsed)
    {
        var inchesPerMinuteToMPH = 60 * 2 * Math.PI * 1.57828e-5;
        var speed = inchesPerMinuteToMPH * (Math.abs(parentModel.GetParameter("ROTORRPM").parameterValue) * parentModel.GetParameter("TIREDIAMETER").parameterValue / 2) / parentModel.GetParameter("REARAXLERATIO").parameterValue;

        if (speed >= 0.0)
        {
            //      System.Diagnostics.Debug.WriteLine("SPEED");
            if (parentModel.GetParameter("MILESORKILOMETERS").parameterValue != 0)
            {
                parentModel.GetParameter("SPEED").parameterValue = speed * 1.609344;
            }
            else
            {
                parentModel.GetParameter("SPEED").parameterValue = speed;
            }
        }
    }

    // public static void CalculateOptionBits(GoiParameter OptionBits)
    // {
    //     if (((int)OptionBits.parameterValue & 0x0001) == 0x0001)
    //     {
    //         this..GetParameter("REVERSEENCODER").parameterValue = 1;
    //         //System.Diagnostics.Debug.WriteLine("REVERSEENCODER written 1");

    //     }
    //     else
    //     {
    //         this..GetParameter("REVERSEENCODER").parameterValue = 0;
    //         //System.Diagnostics.Debug.WriteLine("REVERSEENCODER written 0");
    //     }

    //     if (((int)OptionBits.parameterValue & 0x0002) == 0x0002)
    //         this..GetParameter("DISABLEANALOGBRAKE").parameterValue = 1;
    //     else
    //         this..GetParameter("DISABLEANALOGBRAKE").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0004) == 0x0004)
    //         this..GetParameter("DISABLEBRAKESWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("DISABLEBRAKESWITCH").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0008) == 0x0008)
    //         this..GetParameter("MFG_TESTING_ANALOG_BRAKE_AND_THROTTLE_OPTION").parameterValue = 1;
    //     else
    //         this..GetParameter("MFG_TESTING_ANALOG_BRAKE_AND_THROTTLE_OPTION").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0010) == 0x0010)
    //         this..GetParameter("MANUFACTURINGTESTOPTION").parameterValue = 1;
    //     else
    //         this..GetParameter("MANUFACTURINGTESTOPTION").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0020) == 0x0020)
    //         this..GetParameter("DISABLEOFFTHROTTLEREGENTOSTOP").parameterValue = 1;
    //     else
    //         this..GetParameter("DISABLEOFFTHROTTLEREGENTOSTOP").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0040) == 0x0040)
    //         this..GetParameter("REVERSEREARAXLEDIRECTION").parameterValue = 1;
    //     else
    //         this..GetParameter("REVERSEREARAXLEDIRECTION").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0080) == 0x0080)
    //         this..GetParameter("ANTIROLLBACK").parameterValue = 1;
    //     else
    //         this..GetParameter("ANTIROLLBACK").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0100) == 0x0100)
    //         this..GetParameter("DISABLEFOOTSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("DISABLEFOOTSWITCH").parameterValue = 0;

    //     if (((int)OptionBits.parameterValue & 0x0200) == 0x0200)
    //         this..GetParameter("ENABLENEUTRALSTARTUPINTERLOCK").parameterValue = 1;
    //     else
    //         this..GetParameter("ENABLENEUTRALSTARTUPINTERLOCK").parameterValue = 0;
    // }

    // public static void CalculateToggleDigitialInputBits(GoiParameter ToggleDigitalInputs)
    // {
    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0001) == 0x0001)
    //         this..GetParameter("TOGGLEKEYSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEKEYSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0002) == 0x0002)
    //         this..GetParameter("TOGGLETOWSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLETOWSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0004) == 0x0004)
    //         this..GetParameter("TOGGLECHARGERSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLECHARGERSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0008) == 0x0008)
    //         this..GetParameter("TOGGLEFORWARDSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEFORWARDSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0010) == 0x0010)
    //         this..GetParameter("TOGGLEREVERSESWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEREVERSESWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0020) == 0x0020)
    //         this..GetParameter("TOGGLEFOOTSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEFOOTSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0040) == 0x0040)
    //         this..GetParameter("TOGGLEBRAKESWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEBRAKESWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0080) == 0x0080)
    //         this..GetParameter("TOGGLEOTFSWITCH").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEOTFSWITCH").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0100) == 0x0100)
    //         this..GetParameter("TOGGLESOLENOIDFAULT").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLESOLENOIDFAULT").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0200) == 0x0200)
    //         this..GetParameter("TOGGLEBRAKEFAULT").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEBRAKEFAULT").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0400) == 0x0400)
    //         this..GetParameter("TOGGLEENCODERA").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEENCODERA").parameterValue = 0;

    //     if (((int)ToggleDigitalInputs.parameterValue & 0x0800) == 0x0800)
    //         this..GetParameter("TOGGLEENCODERB").parameterValue = 1;
    //     else
    //         this..GetParameter("TOGGLEENCODERB").parameterValue = 0;
    // }


    // public static void CalculateControllerError(GoiParameter unUsed)
    // {
    //     float fControllerError = 0.1f;
    //     if (this..GetParameter("GROUPONEFAULTS").parameterValue != 0 || this..GetParameter("GROUPTWOFAULTS").parameterValue != 0 || this..GetParameter("GROUPTHREEFAULTS").parameterValue != 0 || this..GetParameter("GROUPFOURFAULTS").parameterValue != 0)
    //     {
    //         fControllerError = 1.0f;
    //     }
    //     this..GetParameter("CONTROLLERERROR").parameterValue = fControllerError;
    // }

    // public static void CalculateLock(GoiParameter unUsed)
    // {

    //     if (this..GetParameter("VEHICLELOCKED").parameterBoolean)
    //     {
    //         this..GetParameter("LOCKED").parameterValue = 1;
    //         this..GetParameter("UNLOCKED").parameterValue = 0;
    //     }
    //     else
    //     {
    //         this..GetParameter("LOCKED").parameterValue = 0;
    //         this..GetParameter("UNLOCKED").parameterValue = 1;
    //     }
    // }

    // public static void CalculateMPHLimits(GoiParameter unUsed) //just do both here
    // {
    //     this..GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue = this..GetParameter("FWDLMTRPM").parameterValue / ((this..GetParameter("REARAXLERATIO").parameterValue) / ((2 * Math.PI * 60 / 5280 / 12) * this..GetParameter("TIREDIAMETER").parameterValue / 2));
    //     this..GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue = this..GetParameter("RVSLMTRPM").parameterValue / ((this..GetParameter("REARAXLERATIO").parameterValue) / ((2 * Math.PI * 60 / 5280 / 12) * this..GetParameter("TIREDIAMETER").parameterValue / 2));
    // }
}

class Tsx //: ViewModelBase
{

    //     // ObservableCollection<GoiParameter> _ParameterList;
    //    ParameterList;// ObservableCollection<GoiParameter> ParameterList
    //     // {
    //     //     set { SetProperty(ref _ParameterList, value); }
    //     //     get { return _ParameterList; }
    //     // }

    constructor(parentModel) //public Tsx()
    {
        const _this = this;

        this.ONLY_IN_APP = -1;
        this.ParameterList = new List();
        if (typeof HasBrowserCommandInterface != 'undefined') {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', '../Dictionaries/TSXDictionary.xml', false);
            xhr.send();
            if (xhr.readyState === 4 && xhr.status === 200) {
                TACDictionaryAsXmlString = xhr.responseText;
            }
            else {
                console.log(`Attempt read dictionary failed. XMLHttpRequest.readyState = ${xhr.readyState}, XMLHttpRequest.status = ${xhr.status}`)
            }
        }
        let dictionary = $.xml2json(TSXDictionaryAsXmlString);
        dictionary.GoiParameter.forEach(function (item, index)
        {
            _this.ParameterList.push(new GoiParameter(parentModel, item.Address, item.Scale, item.UserFrom, item.UserTo, item.DigitalFrom, item.DigitalTo, item.Name, item.PropertyName, item.MemoryType, item.SubsetOfAddress, item.BitRangeStart, item.BitRangeEnd, item.MinimumParameterValue, item.MaximumParameterValue, item.ImplementedFirmwareVersion, item.enumListValue, item.enumListName, item.Description)); //, _ParameterList)
        });

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Speedometer max speed", "SPEEDOMETERMAXSPEED", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Vehicle Speed (MPH)", "SPEED", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Generic Parameter 254", "KILOMETERSORMILES", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Battery Current (A)", "TSXBATTERYCURRENT", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null));

        //Gauge fault bits
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "CONTROLLERERROR", "CONTROLLERERROR", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "COMMERROR", "COMMERROR", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); ////

        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Lock Switch", "LOCKSWITCH", new List < string >
        // {
        //     "Locked",
        //     "Unlocked"
        // }, MemoryTypeAndAccess.ReadOrWrite)); //    
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Key Switch", "KEY", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); // 
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Foot Switch", "AUX1SWITCH", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); // 
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Reverse Switch", "REVERSESWITCH", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); //   
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Forward Switch", "FORWARDSWITCH", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); //   
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Interlock", "SROSWITCH", new List < string >
        // {
        //     "Off",
        //     "Connected"
        // }, MemoryTypeAndAccess.ReadOrWrite)); // 
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Neutral", "NEUTRAL", new List < string >
        // {
        //     "Off",
        //     "Connected"
        // }, MemoryTypeAndAccess.ReadOrWrite)); // 

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "LOCKED", "LOCKED", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "UNLOCKED", "UNLOCKED", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //

        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Main Solenoid Output", "MAINSOLENOID", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); //  
        // this.ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Buzzer Output", "BUZZEROUTPUT", new List < string >
        // {
        //     "Off",
        //     "On"
        // }, MemoryTypeAndAccess.ReadOrWrite)); // 
        //ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Enable Main Solenoid", "MAINSOLENOIDENABLE", new List<string> { "Off", "On" }, MemoryTypeAndAccess.ReadOrWrite));
        //ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Enable Speed Sensor", "SPEEDSENSORENABLE", new List<string> { "Off", "On" }, MemoryTypeAndAccess.ReadOrWrite));
        //ParameterList.push(new GoiParameter(parentModel, ONLY_IN_APP, null, 0, 1, 0, 1, "Enable Speed Limits", "SPEEDLIMITENABLE", new List<string> { "Off", "On" }, MemoryTypeAndAccess.ReadOrWrite));

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Forward MPH Speed Limit", "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "Reverse MPH Speed Limit", "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //

        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARSTARTUPERRORS")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARRUNTIMEERRORSLOW")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARRUNTIMEERRORSHIGH")).ReCalculate += CalculateControllerError;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "VEHICLELOCKED")).ReCalculate += CalculateLock;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARMAXMOTORFWDSPEEDLIMIT")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARMAXMOTORREVSPEEDLIMIT")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "TIREDIAMETER")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "REARAXLERATIO")).ReCalculate += CalculateMPHLimits;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARSWITCHSTATES")).ReCalculate += CalculateVehicleSwitches;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARDIGOUT")).ReCalculate += CalculateDigitalOutputs;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARACCESSORYENABLE")).ReCalculate += CalculateAccessoryOptions;
        this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARMOTORRPM")).Observe(this.CalculateSpeed);//.ReCalculate += CalculateSpeed;
        //this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARBATTERYCURRENT")).ReCalculate += CalculateBatteryCurrent;

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "App Revision", "APPVERSION", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "App Build", "APPBUILD", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "APPVERSION")).parameterValueString = Appp.InfoService.Version;
        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "APPBUILD")).parameterValueString = Appp.InfoService.Build;

        // this.ParameterList.FirstOrDefault(x => (x.PropertyName == "SPEEDOMETERMAXSPEED")).parameterValue = 35;

        //Here is how to create TAC compatible parameters
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "SWITCHBITS", "SWITCHBITS", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARSWITCHSTATES")).Observe(this.CalculateSWITCHBITS); //.ReCalculate += CalculateForwardNeutralReverseBits;

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "FOOTSW", "FOOTSW", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, null, 0, 1, 0, 1, "VTHROTTLEV", "VTHROTTLEV", MemoryTypeAndAccess.ReadOrWrite, null, null, null, null, null, null, null, null, null)); //
        this.ParameterList.FirstOrDefault(x => (x.PropertyName == "PARPRIMTHROTVOLTS")).Observe(this.CalculateVTHROTTLEV); //.ReCalculate += CalculateForwardNeutralReverseBits;

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Latitude", "LATITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Longitude", "LONGITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Altitude", "ALTITUDE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Acceleration (m/s^2)", "ACCELERATION", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Torque (Nm)", "TORQUE", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation X", "ORIENTATIONX", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation Y", "ORIENTATIONY", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation Z", "ORIENTATIONZ", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //
        this.ParameterList.push(new GoiParameter(parentModel, this.ONLY_IN_APP, 1, 0, 1, 0, 1, "Orientation W", "ORIENTATIONW", MemoryTypeAndAccess.ReadOrWrite, false, 0, 15, null, null, null, null, null, null)); //

        parentModel.ModelType = "TSX";
    }


    // public static void CalculateMPHLimits(GoiParameter unUsed) //just do both here
    // {
    //     this..GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue = this..GetParameter("PARMAXMOTORFWDSPEEDLIMIT").parameterValue / ((this..GetParameter("REARAXLERATIO").parameterValue) / ((2 * (float) Math.PI * 60 / 5280 / 12) * this..GetParameter("TIREDIAMETER").parameterValue / 2));
    //     this..GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue = this..GetParameter("PARMAXMOTORREVSPEEDLIMIT").parameterValue / ((this..GetParameter("REARAXLERATIO").parameterValue) / ((2 * (float) Math.PI * 60 / 5280 / 12) * this..GetParameter("TIREDIAMETER").parameterValue / 2));
    // }

    CalculateSpeed(parentModel, unUsed) //public static void CalculateSpeed(GoiParameter PARMOTORRPM)
    {
        var inchesPerMinuteToMPH = 60 * 2 * Math.PI * 1.57828e-5;
        var speed = inchesPerMinuteToMPH * (Math.abs(parentModel.GetParameter("PARMOTORRPM").parameterValue) * parentModel.GetParameter("TIREDIAMETER").parameterValue / 2) / parentModel.GetParameter("REARAXLERATIO").parameterValue;

        if (speed >= 0.0)
        {
            //      System.Diagnostics.Debug.WriteLine("SPEED");
            if (parentModel.GetParameter("MILESORKILOMETERS").parameterValue != 0)
            {
                parentModel.GetParameter("SPEED").parameterValue = speed * 1.609344;
            }
            else
            {
                parentModel.GetParameter("SPEED").parameterValue = speed;
            }
        }
    }

    // public static void CalculateBatteryCurrent(GoiParameter TSXBATTERYCURRENT)
    // {
    //     if (this..GetParameter("PARBATTERYCURRENT").parameterValue > 32767)
    //     {
    //         //      System.Diagnostics.Debug.WriteLine("SPEED");
    //         this..GetParameter("TSXBATTERYCURRENT").parameterValue = this..GetParameter("PARBATTERYCURRENT").parameterValue - 65535;
    //     }
    //     else
    //     {
    //         this..GetParameter("TSXBATTERYCURRENT").parameterValue = this..GetParameter("PARBATTERYCURRENT").parameterValue;
    //     }
    // }

    // public static void CalculateControllerError(GoiParameter unUsed)
    // {
    //     float fControllerError = 0.1 f;
    //     if (this..GetParameter("PARSTARTUPERRORS").parameterValue != 0 || this..GetParameter("PARRUNTIMEERRORSLOW").parameterValue != 0 || this..GetParameter("PARRUNTIMEERRORSHIGH").parameterValue != 0)
    //     {
    //         fControllerError = 1.0 f;
    //     }
    //     this..GetParameter("CONTROLLERERROR").parameterValue = fControllerError;
    // }

    CalculateSWITCHBITS(parentModel)
    { //SWITCHBITS is compatible with TAC params
       if ((parentModel.GetParameter("PARSWITCHSTATES").parameterValue & 0x0002) == 0x0002)
            parentModel.GetParameter("SWITCHBITS").parameterValue |= 2;
        else
            parentModel.GetParameter("SWITCHBITS").parameterValue &= 0xFFFD;

        if ((parentModel.GetParameter("PARSWITCHSTATES").parameterValue & 0x0004) == 0x0004)
            parentModel.GetParameter("SWITCHBITS").parameterValue |= 4;
        else
            parentModel.GetParameter("SWITCHBITS").parameterValue &= 0xFFFB;

        if ((parentModel.GetParameter("PARSWITCHSTATES").parameterValue & 0x0020) == 0x0020)
            parentModel.GetParameter("FOOTSW").parameterValue = 1;
        else
            parentModel.GetParameter("FOOTSW").parameterValue = 0;


        // this..GetParameter("LOCKSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0080) == 0x0080 ? 1 : 0;
        // this..GetParameter("AUX1SWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0020) == 0x0020 ? 1 : 0;
        // this..GetParameter("KEY").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0010) == 0x0010 ? 1 : 0;
        // this..GetParameter("REVERSESWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0004) == 0x0004 ? 1 : 0;
        // this..GetParameter("FORWARDSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
        // this..GetParameter("SROSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0001) == 0x0001 ? 0 : 1;
        // this..GetParameter("NEUTRAL").parameterValue = ((int) this..GetParameter("FORWARDSWITCH").parameterValue == (int) this..GetParameter("REVERSESWITCH").parameterValue) ? 1 : 0;
    }

    CalculateVTHROTTLEV(parentModel)
    {
        parentModel.GetParameter("VTHROTTLEV").parameterValue = parentModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue;
    }
    // public static void CalculateVehicleSwitches(GoiParameter PARSWITCHSTATES)
    // {
    //     this..GetParameter("LOCKSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0080) == 0x0080 ? 1 : 0;
    //     this..GetParameter("AUX1SWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0020) == 0x0020 ? 1 : 0;
    //     this..GetParameter("KEY").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0010) == 0x0010 ? 1 : 0;
    //     this..GetParameter("REVERSESWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0004) == 0x0004 ? 1 : 0;
    //     this..GetParameter("FORWARDSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
    //     this..GetParameter("SROSWITCH").parameterValue = ((int) PARSWITCHSTATES.parameterValue & 0x0001) == 0x0001 ? 0 : 1;
    //     this..GetParameter("NEUTRAL").parameterValue = ((int) this..GetParameter("FORWARDSWITCH").parameterValue == (int) this..GetParameter("REVERSESWITCH").parameterValue) ? 1 : 0;
    // }


    // public static void CalculateDigitalOutputs(GoiParameter digitalOutputs)
    // {

    //     this..GetParameter("MAINSOLENOID").parameterValue = ((int) digitalOutputs.parameterValue & 0x004) == 0x0004 ? 1 : 0;
    //     this..GetParameter("BUZZEROUTPUT").parameterValue = ((int) digitalOutputs.parameterValue & 0x0001) == 0x0001 ? 1 : 0;
    // }

    // public static void CalculateAccessoryOptions(GoiParameter accessoryOptions)
    // {
    //     this..GetParameter("SOLENOIDENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0001) == 0x0001 ? 1 : 0;
    //     this..GetParameter("AUX4ENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0002) == 0x0002 ? 1 : 0;
    //     this..GetParameter("DRAGRACEENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0004) == 0x0004 ? 1 : 0;
    //     this..GetParameter("BUZZERENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0008) == 0x0008 ? 1 : 0;
    //     this..GetParameter("AUX2ENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0010) == 0x0010 ? 1 : 0;
    //     this..GetParameter("AUX3ENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0020) == 0x0020 ? 1 : 0;
    //     this..GetParameter("SPEEDSENSORENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0040) == 0x0040 ? 1 : 0;
    //     this..GetParameter("SPEEDLIMITENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0080) == 0x0080 ? 1 : 0;
    //     this..GetParameter("ANTIROLLAWAYENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0100) == 0x0100 ? 1 : 0;
    //     this..GetParameter("ANTIROLLAWAYRESET").parameterValue = ((int) accessoryOptions.parameterValue & 0x0200) == 0x0200 ? 1 : 0;
    //     this..GetParameter("ROLLAWAYALARMENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0400) == 0x0400 ? 1 : 0;
    //     this..GetParameter("REVERSEALARMENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x0800) == 0x0800 ? 1 : 0;
    //     this..GetParameter("OVERDRIVEENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x1000) == 0x1000 ? 1 : 0;
    //     this..GetParameter("STALLPROTECTIONENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x2000) == 0x2000 ? 1 : 0;
    //     this..GetParameter("VARIABLEBRAKINGENABLE").parameterValue = ((int) accessoryOptions.parameterValue & 0x4000) == 0x4000 ? 1 : 0;
    //     this..GetParameter("OVERDRIVEALLOWED").parameterValue = ((int) accessoryOptions.parameterValue & 0x8000) == 0x8000 ? 1 : 0;
    // }

    // public static void CalculateLock(GoiParameter unUsed)
    // {

    //     if (this..GetParameter("VEHICLELOCKED").parameterBoolean)
    //     {
    //         this..GetParameter("LOCKED").parameterValue = 1;
    //         this..GetParameter("UNLOCKED").parameterValue = 0;
    //     }
    //     else
    //     {
    //         this..GetParameter("LOCKED").parameterValue = 0;
    //         this..GetParameter("UNLOCKED").parameterValue = 1;
    //     }
    // }
}

//});

//more like c#
class Task //make this a static reference elsewhere
{
    static Delay(ms)
    {
        return new Promise(resolve =>
        {
            setTimeout(resolve, ms);
        });
    }
}
function sleep(ms) //free up the thread for async/await stuff
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

//definitely not the place for this next line but...
//
let App = {};