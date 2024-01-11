////let DEBUG_SimulatedCommandInterface = false;
class SimulatedCommandInterface {
    constructor(model, simulatedModel) {
        this.Model = model;
        this.SimulatedModel = simulatedModel;
        this.timeOut = 0;
        this.communicationSimulatortimeOutCanceller;
        this.ReadContinuouslyCancellationToken;
        this.htmlPageUniqueId = 0;

        if (simulatedModel.ModelType == "TAC") {
            this.SimulatedModel.GetParameter("ROTORRPM").parameterValue = 0;
            this.decrementSimulatedROTORRPM = false;

            this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue = 0.3;
            this.decrementSimulatedVTHROTTLEV = false;

            this.SimulatedModel.GetParameter("MTTEMPC").parameterValue = 6;
            this.decrementSimulatedMTTEMPC = false;

            this.simulatedScopeData = "[{\"PropertyName\":\"VTHROTTLEV\",\"parameterTimeAndValuePairList\":[{\"time\":\"0\",\"value\":0.116455078},{\"time\":\"1\",\"value\":0.117675781},{\"time\":\"2\",\"value\":0.110839844},{\"time\":\"3\",\"value\":0.116455078},{\"time\":\"4\",\"value\":0.120361328},{\"time\":\"5\",\"value\":0.116455078},{\"time\":\"6\",\"value\":0.116455078},{\"time\":\"7\",\"value\":0.110839844},{\"time\":\"8\",\"value\":0.07519531},{\"time\":\"9\",\"value\":0.123291016},{\"time\":\"10\",\"value\":0.113525391},{\"time\":\"11\",\"value\":0.131347656},{\"time\":\"12\",\"value\":0.128662109},{\"time\":\"13\",\"value\":0.116455078},{\"time\":\"14\",\"value\":0.116455078},{\"time\":\"15\",\"value\":0.117675781},{\"time\":\"16\",\"value\":0.116455078},{\"time\":\"17\",\"value\":0.116455078},{\"time\":\"18\",\"value\":0.113525391},{\"time\":\"19\",\"value\":0.116455078},{\"time\":\"20\",\"value\":0.120361328},{\"time\":\"21\",\"value\":0.117675781},{\"time\":\"22\",\"value\":0.113525391},{\"time\":\"23\",\"value\":0.110839844},{\"time\":\"24\",\"value\":0.116455078},{\"time\":\"25\",\"value\":0.116455078},{\"time\":\"26\",\"value\":0.120361328},{\"time\":\"27\",\"value\":0.120361328},{\"time\":\"28\",\"value\":0.116455078},{\"time\":\"29\",\"value\":0.113525391},{\"time\":\"30\",\"value\":0.117675781},{\"time\":\"31\",\"value\":0.116455078},{\"time\":\"32\",\"value\":0.116455078},{\"time\":\"33\",\"value\":0.123291016},{\"time\":\"34\",\"value\":0.120361328},{\"time\":\"35\",\"value\":0.116455078},{\"time\":\"36\",\"value\":0.116455078},{\"time\":\"37\",\"value\":0.110839844},{\"time\":\"38\",\"value\":0.09716797},{\"time\":\"39\",\"value\":0.120361328},{\"time\":\"40\",\"value\":0.117675781},{\"time\":\"41\",\"value\":0.116455078},{\"time\":\"42\",\"value\":0.110839844},{\"time\":\"43\",\"value\":0.113525391},{\"time\":\"44\",\"value\":0.116455078},{\"time\":\"45\",\"value\":0.116455078},{\"time\":\"46\",\"value\":0.116455078},{\"time\":\"47\",\"value\":0.117675781},{\"time\":\"48\",\"value\":0.113525391},{\"time\":\"49\",\"value\":0.110839844},{\"time\":\"50\",\"value\":0.110839844},{\"time\":\"51\",\"value\":0.110839844},{\"time\":\"52\",\"value\":0.113525391},{\"time\":\"53\",\"value\":0.117675781},{\"time\":\"54\",\"value\":0.116455078},{\"time\":\"55\",\"value\":0.116455078},{\"time\":\"56\",\"value\":0.113525391},{\"time\":\"57\",\"value\":0.1081543},{\"time\":\"58\",\"value\":0.113525391},{\"time\":\"59\",\"value\":0.113525391},{\"time\":\"60\",\"value\":0.116455078},{\"time\":\"61\",\"value\":0.120361328},{\"time\":\"62\",\"value\":0.116455078},{\"time\":\"63\",\"value\":0.116455078}]}]";

            this.SimulatedModel.GetParameter("SWITCHBITS").parameterValue = 0x00;

            this.NavitasTacControllerSimulatorCanceller;
            this.slowLogDown2 = 0;
            this.SimulatedModel.GetParameter("THROTTLEMIN").parameterValue = 1.611;
            this.SimulatedModel.GetParameter("THROTTLEMAX").parameterValue = 4.0;
            this.SimulatedModel.GetParameter("THROTTLEKNEEGAIN238").parameterValue = 1365 / 4096;
        }
        else if (simulatedModel.ModelType == "TSX") {
            this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue = 0; //PARMOTORRPM
            this.decrementSimulatedPARMOTORRPM = false;

            this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue = 0.3; //PARPRIMTHROTVOLTS
            this.decrementSimulatedPARPRIMTHROTVOLTS = false;

            this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue = 6; //PARTEMPERATURE
            this.decrementSimulatedPARTEMPERATURE = false;

            this.simulatedScopeData = "[{\"PropertyName\":\"PARPRIMTHROTVOLTS\",\"parameterTimeAndValuePairList\":[{\"time\":\"0\",\"value\":0.116455078},{\"time\":\"1\",\"value\":0.117675781},{\"time\":\"2\",\"value\":0.110839844},{\"time\":\"3\",\"value\":0.116455078},{\"time\":\"4\",\"value\":0.120361328},{\"time\":\"5\",\"value\":0.116455078},{\"time\":\"6\",\"value\":0.116455078},{\"time\":\"7\",\"value\":0.110839844},{\"time\":\"8\",\"value\":0.07519531},{\"time\":\"9\",\"value\":0.123291016},{\"time\":\"10\",\"value\":0.113525391},{\"time\":\"11\",\"value\":0.131347656},{\"time\":\"12\",\"value\":0.128662109},{\"time\":\"13\",\"value\":0.116455078},{\"time\":\"14\",\"value\":0.116455078},{\"time\":\"15\",\"value\":0.117675781},{\"time\":\"16\",\"value\":0.116455078},{\"time\":\"17\",\"value\":0.116455078},{\"time\":\"18\",\"value\":0.113525391},{\"time\":\"19\",\"value\":0.116455078},{\"time\":\"20\",\"value\":0.120361328},{\"time\":\"21\",\"value\":0.117675781},{\"time\":\"22\",\"value\":0.113525391},{\"time\":\"23\",\"value\":0.110839844},{\"time\":\"24\",\"value\":0.116455078},{\"time\":\"25\",\"value\":0.116455078},{\"time\":\"26\",\"value\":0.120361328},{\"time\":\"27\",\"value\":0.120361328},{\"time\":\"28\",\"value\":0.116455078},{\"time\":\"29\",\"value\":0.113525391},{\"time\":\"30\",\"value\":0.117675781},{\"time\":\"31\",\"value\":0.116455078},{\"time\":\"32\",\"value\":0.116455078},{\"time\":\"33\",\"value\":0.123291016},{\"time\":\"34\",\"value\":0.120361328},{\"time\":\"35\",\"value\":0.116455078},{\"time\":\"36\",\"value\":0.116455078},{\"time\":\"37\",\"value\":0.110839844},{\"time\":\"38\",\"value\":0.09716797},{\"time\":\"39\",\"value\":0.120361328},{\"time\":\"40\",\"value\":0.117675781},{\"time\":\"41\",\"value\":0.116455078},{\"time\":\"42\",\"value\":0.110839844},{\"time\":\"43\",\"value\":0.113525391},{\"time\":\"44\",\"value\":0.116455078},{\"time\":\"45\",\"value\":0.116455078},{\"time\":\"46\",\"value\":0.116455078},{\"time\":\"47\",\"value\":0.117675781},{\"time\":\"48\",\"value\":0.113525391},{\"time\":\"49\",\"value\":0.110839844},{\"time\":\"50\",\"value\":0.110839844},{\"time\":\"51\",\"value\":0.110839844},{\"time\":\"52\",\"value\":0.113525391},{\"time\":\"53\",\"value\":0.117675781},{\"time\":\"54\",\"value\":0.116455078},{\"time\":\"55\",\"value\":0.116455078},{\"time\":\"56\",\"value\":0.113525391},{\"time\":\"57\",\"value\":0.1081543},{\"time\":\"58\",\"value\":0.113525391},{\"time\":\"59\",\"value\":0.113525391},{\"time\":\"60\",\"value\":0.116455078},{\"time\":\"61\",\"value\":0.120361328},{\"time\":\"62\",\"value\":0.116455078},{\"time\":\"63\",\"value\":0.116455078}]}]";

            this.SimulatedModel.GetParameter("SWITCHBITS").parameterValue = 0x00;

            this.NavitasTacControllerSimulatorCanceller;
            this.slowLogDown2 = 0;
            this.SimulatedModel.GetParameter("PARPRIMARYTHROTTLEFORWARDMIN").parameterValue = 1.611; //PARPRIMARYTHROTTLEFORWARDMIN
            this.SimulatedModel.GetParameter("PARPRIMARYTHROTTLEFORWARDMAX").parameterValue = 4.0; //PARPRIMARYTHROTTLEFORWARDMAX
            this.SimulatedModel.GetParameter("Parameter257").parameterValue = 0.1;
            this.SimulatedModel.GetParameter("Parameter258").parameterValue = 0.05;
            //this.SimulatedModel.GetParameter("THROTTLEKNEEGAIN238").parameterValue = 1365 / 4096;
        }
    }

    //*****************************************types of ways to test code in Chrome or Safari browser**********************************************************//
    async invokeSimulatedAction(data, interval) //This gets overwritten if running from Navitas App
    { //so a good place to start a simulation if not running in the App
        clearTimeout(this.communicationSimulatorTimeOutCanceller); //stop continuous just like App would

        //Read Contiuously: just hacked in, it constantly builds and removes packets
        //TODO: Make it like the app when you have time
        if (data.ReadContinuously)
        {
            let pageParameters = new PageParameterList((this.SimulatedModel.ModelType == "TAC") ? PageParameterList.ParameterListType.TAC : PageParameterList.ParameterListType.TSX, "A Title");

            Object.entries(data.ReadContinuously).forEach(([key, keyValue]) => {
                pageParameters.parameterList.Add(this.Model.GetParameter(key));
            });

            //SendJsCommandToWebview(pageParameters); Voila, commented out because we are in Javascript
            this.readSimulationdata(pageParameters.parameterList);
            this.Model.CompleteTheJsonCommand(pageParameters.parameterList); //wait for resonse handshake ie JsonCommandCompleted == ture
            setTimeout(() => this.ContinuousUpdateGoiData(pageParameters.parameterList), interval); //then don't handshake
        }
        else if (data.Read) {
            let pageParameters = new PageParameterList((this.SimulatedModel.ModelType == "TAC") ? PageParameterList.ParameterListType.TAC : PageParameterList.ParameterListType.TSX, "A Title");

            Object.entries(data.Read).forEach(([key, keyValue]) => {
                pageParameters.parameterList.Add(this.Model.GetParameter(key));
            });

            if (pageParameters.parameterList.Count != 0) {
                this.readSimulationdata(pageParameters.parameterList);
                setTimeout(() => this.Model.CompleteTheJsonCommand(pageParameters.parameterList), interval);
            }
            //SendJsCommandToWebview(pageParameters); Voila, commented out because we are in Javascript
        }
        else if (data.Write) {
            let pageParameters = new PageParameterList((this.SimulatedModel.ModelType == "TAC") ? PageParameterList.ParameterListType.TAC : PageParameterList.ParameterListType.TSX, "A Title");

            Object.entries(data.Write).forEach(([key, keyValue]) => {
                let parameter = this.Model.GetParameter(key);

                if (parameter.SubsetOfAddress) {//this sets single or multiple bits
                    let parentParameter = this.Model.GetParameter(parameter.Address); //returns Parent parameter (ie same address but SubsetOfAddress is false)

                    var temp;
                    if (parentParameter.bupdate)
                        temp = parentParameter.rawValue; //starting value for bit manipulation
                    else //someone else is currently manipulating these bits (hopefully from this list of parameters)
                        temp = parentParameter.rawValueToBeWritten

                    let clearMask = 0;
                    for (var i = 15; i >= 0; i--) {
                        clearMask <<= 1;
                        if (i >= parameter.BitRangeStart && i <= parameter.BitRangeEnd)
                            clearMask &= 0xFFFE;
                        else
                            clearMask |= 0x0001;
                    }
                    temp &= clearMask;
                    temp |= (parseInt(keyValue) << parameter.BitRangeStart);
                    parentParameter.rawValueToBeWritten = temp;
                    parameter = parentParameter; //reference the parent now
                }
                else
                {
                    if (this.SimulatedModel.ModelType == "TAC")
                        parameter.rawValueToBeWritten = parseInt(parseFloat(keyValue) * parameter.Scale);
                    else
                        parameter.rawValueToBeWritten = parseInt((parameter.UserFrom + (parameter.UserTo - parameter.UserFrom) * ((parseFloat(keyValue) - parameter.DigitalFrom) / (parameter.DigitalTo - parameter.DigitalFrom))));
                }

                // if (parameter.MemoryType === "ReadOrWrite")
                // {
                //     parameter.rawValue = parameter.rawValueToBeWritten; //convienence to parameter values are kept up to date since it cannot be read
                // }
                //if we also happen to be reading this in the background then don't overwrite it before we finish
                parameter.bupdate = false; //a completed write will set this back to true
                //TODO: maybe. DONT FORGET this new method of writing may have a hole that if a read happens here the value will be overwritten
                if (!pageParameters.parameterList.some(e => e.PropertyName === parameter.PropertyName))
                    pageParameters.parameterList.Add(parameter);
            });

            if (pageParameters.parameterList.Count != 0) {
                this.writeSimulationdata(pageParameters.parameterList);
                setTimeout(() => this.Model.CompleteTheJsonCommand(pageParameters.parameterList), 200);
            }
        }
        else if (data.ReadScopeDataBlock) {
            let pageParameters = new PageParameterList((this.SimulatedModel.ModelType == "TAC") ? PageParameterList.ParameterListType.TAC : PageParameterList.ParameterListType.TSX, "A Title");

            Object.entries(data.ReadScopeDataBlock).forEach(([key, keyValue]) => {
                let parameter = this.Model.GetParameter(key);
                parameter.rawValueToBeWritten = parseInt(keyValue);

                if (!pageParameters.parameterList.some(e => e.PropertyName === parameter.PropertyName))
                    pageParameters.parameterList.Add(parameter);
            });

            if (pageParameters.parameterList.Count != 0) {
                //writeSimulationdata(objectFromJson);
                setTimeout(() => this.Model.CompleteTheJsonCommand(pageParameters.parameterList), 200);
            }
        }
        else if (data.ControlVerified) {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
        else if (data.Close) {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
        else if (data.GetAppParameters) {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
    }

    ContinuousUpdateGoiData(data) {
        this.readSimulationdata(data);
        this.Model.UnsolicitedCompleteTheJsonCommand(data);
        this.communicationSimulatorTimeOutCanceller = setTimeout(() => { this.ContinuousUpdateGoiData(data) }, 100);
    }
    StartNavitasControllerSimulator(timeout,
        simulatorToRun = () => {
            if (this.ModelType == "TAC")
                this.NavitasTacControllerGeneralSimulator();
            else if (this.ModelType == "TSX")
                this.NavitasTsxControllerGeneralSimulator();
        }) {
        this.NavitasControllerSimulatorCanceller = setTimeout(() => { this.StartNavitasControllerSimulator(timeout, simulatorToRun) }, timeout);
        simulatorToRun();
    }
    CancelNavitasControllerSimulators() {
        clearTimeout(this.NavitasControllerSimulatorCanceller);
        clearTimeout(this.communicationSimulatorTimeOutCanceller);
        //console.log("Navitas Tac Controller Javascript Simulator Cancelled")
    }

    NavitasTacControllerGeneralSimulator() {
        let nowTime = (new Date().getTime() / 1000).toFixed(3);

        if (!this.decrementSimulatedROTORRPM)
            this.SimulatedModel.GetParameter("ROTORRPM").parameterValue += 50;
        else
            this.SimulatedModel.GetParameter("ROTORRPM").parameterValue -= 50;

        if (this.SimulatedModel.GetParameter("ROTORRPM").parameterValue >= 3000)
            this.decrementSimulatedROTORRPM = true;
        if (this.SimulatedModel.GetParameter("ROTORRPM").parameterValue <= 0)
            this.decrementSimulatedROTORRPM = false;
        if (this.SimulatedModel.GetParameter("ROTORRPM").parameterValue < 0)
            this.SimulatedModel.GetParameter("ROTORRPM").parameterValue = 0;
        if (this.SimulatedModel.GetParameter("ROTORRPM").parameterValue > 3000)
            this.SimulatedModel.GetParameter("ROTORRPM").parameterValue = 3000;


        if (!this.decrementSimulatedVTHROTTLEV)
            this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue += .06;
        else
            this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue -= .06;

        if (this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue >= 5)
            this.decrementSimulatedVTHROTTLEV = true;
        if (this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue <= 0.3)
            this.decrementSimulatedVTHROTTLEV = false;
        if (this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue < 0.3)
            this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue = 0.3;
        if (this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue > 5)
            this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue = 5;

        if (!this.decrementSimulatedMTTEMPC)
            this.SimulatedModel.GetParameter("MTTEMPC").parameterValue += 10;
        else
            this.SimulatedModel.GetParameter("MTTEMPC").parameterValue -= 10;

        if (this.SimulatedModel.GetParameter("MTTEMPC").parameterValue >= 120)
            this.decrementSimulatedMTTEMPC = true;
        if (this.SimulatedModel.GetParameter("MTTEMPC").parameterValue <= 0)
            this.decrementSimulatedMTTEMPC = false;
        if (this.SimulatedModel.GetParameter("MTTEMPC").parameterValue < 0)
            this.SimulatedModel.GetParameter("MTTEMPC").parameterValue = 0;
        if (this.SimulatedModel.GetParameter("MTTEMPC").parameterValue > 120)
            this.SimulatedModel.GetParameter("MTTEMPC").parameterValue = 120;
        //Debug the executtion time of this routine
        // let nowTime1= (new Date().getTime()/1000).toFixed(3);
        // if(slowLogDown2 % 100 == 1)
        //     console.log("Time to execute = ", (nowTime1-nowTime).toString())
        // slowLogDown2++;
    }

    NavitasTsxControllerGeneralSimulator() {
        let nowTime = (new Date().getTime() / 1000).toFixed(3);

        if (!this.decrementSimulatedPARMOTORRPM)
            this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue += 50;
        else
            this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue -= 50;

        if (this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue >= 3000)
            this.decrementSimulatedPARMOTORRPM = true;
        if (this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue <= 0)
            this.decrementSimulatedPARMOTORRPM = false;
        if (this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue < 0)
            this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue = 0;
        if (this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue > 3000)
            this.SimulatedModel.GetParameter("PARMOTORRPM").parameterValue = 3000;


        if (!this.decrementSimulatedPARPRIMTHROTVOLTS)
            this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue += .06;
        else
            this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue -= .06;

        if (this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue >= 5)
            this.decrementSimulatedPARPRIMTHROTVOLTS = true;
        if (this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue <= 0.3)
            this.decrementSimulatedPARPRIMTHROTVOLTS = false;
        if (this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue < 0.3)
            this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue = 0.3;
        if (this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue > 5)
            this.SimulatedModel.GetParameter("PARPRIMTHROTVOLTS").parameterValue = 5;

        if (!this.decrementSimulatedPARTEMPERATURE)
            this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue += 10;
        else
            this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue -= 10;

        if (this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue >= 120)
            this.decrementSimulatedPARTEMPERATURE = true;
        if (this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue <= 0)
            this.decrementSimulatedPARTEMPERATURE = false;
        if (this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue < 0)
            this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue = 0;
        if (this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue > 120)
            this.SimulatedModel.GetParameter("PARTEMPERATURE").parameterValue = 120;
        //Debug the executtion time of this routine
        // let nowTime1= (new Date().getTime()/1000).toFixed(3);
        // if(slowLogDown2 % 100 == 1)
        //     console.log("Time to execute = ", (nowTime1-nowTime).toString())
        // slowLogDown2++;
    }
    readSimulationdata(objectFromJson) {
        objectFromJson.forEach((element, index, array) => {
            if (element.PropertyName == "MILESORKILOMETERS")
                element.parameterValue = 1;
            else if (element.PropertyName == "SPEEDOMETERMAXSPEED")
                element.parameterValue = 55;
            else if (element.PropertyName == "PARSWITCHSTATES")
                element.parameterValue = this.SimulatedModel.GetParameter("FOOTSW").parameterValue << 5;
            else if (element.PropertyName == "TIREDIAMETER")
                element.parameterValue = 23;
            else if (element.PropertyName == "REARAXLERATIO")
                element.parameterValue = 12;
            else
                element.parameterValue = this.SimulatedModel.GetParameter(element.PropertyName).parameterValue;
        });
    }
    writeSimulationdata(objectFromJson) {
        //generally update any parameter written
        objectFromJson.forEach((parameter, index, array) => {
            parameter.bupdate = true; //so that reads can update this variable
            parameter.rawValue = parameter.rawValueToBeWritten
            this.SimulatedModel.GetParameter(parameter.PropertyName).parameterValue = parameter.parameterValue;
            if (parameter.PropertyName == "DATASCOPECOMMAND") {
                if (parameter.parameterValue == 1) {

                    setTimeout(() => this.scopeTriggerSimulator(10), 10);

                }
            }
            if (parameter.PropertyName == "Parameter257") {
                if (parameter.parameterValue == 0) {
                    //we write a 0 so we know if it becomes non 0 a new voltage was captured
                    setTimeout(() => this.footSwitchVoltageCaptureSimulator(10), 10);

                }
            }
            if (parameter.PropertyName == "Parameter258") {
                if (parameter.parameterValue == 0) {
                    //we write a 0 so we know if it becomes non 0 a new voltage was captured
                    setTimeout(() => this.footSwitchVoltageCaptureSimulator(10), 10);

                }
            }
        });
    }
    scopeTriggerSimulator(timeout) {
        let triggerLevel = this.SimulatedModel.GetParameter("DATASCOPETRIGGERLEVEL").parameterValue;
        let triggerAddress = this.SimulatedModel.GetParameter("DATASCOPETRIGGERADDRESS").parameterValue;
        let triggerMode = this.SimulatedModel.GetParameter("DATASCOPETRIGGERMODE").parameterValue;
        let parameter = this.SimulatedModel.GetParameter(triggerAddress);
        if (parameter == null)
            throw new InvalidOperationException("GetParameter can't find " + triggerAddress);

        if (triggerMode == 2 && triggerLevel == parameter.parameterValue) {
            setTimeout(() => this.SimulatedModel.GetParameter("DATASCOPECOMMAND").parameterValue = 2, 2000);
            //fake throttle data
            let objectFromJson = JSON.parse(this.simulatedScopeData);
            objectFromJson[0].parameterTimeAndValuePairList[0].value = this.SimulatedModel.GetParameter("VTHROTTLEV").parameterValue;
            this.simulatedScopeData = JSON.stringify(objectFromJson);
            App.NavitasMotorController.Model.ScopeDataArray = objectFromJson;
        }
        else
            setTimeout(() => this.scopeTriggerSimulator(timeout), timeout); //continue watching
    }
    footSwitchVoltageCaptureSimulator(timeout) {
        if (this.SimulatedModel.GetParameter("Parameter257").parameterValue == 0)
            setTimeout(() => this.SimulatedModel.GetParameter("Parameter257").parameterValue = .5555, 2000);
        if (this.SimulatedModel.GetParameter("Parameter258").parameterValue == 0)
            setTimeout(() => this.SimulatedModel.GetParameter("Parameter258").parameterValue = .44, 2000);
    }
}