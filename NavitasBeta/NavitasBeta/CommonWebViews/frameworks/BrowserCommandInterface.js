HasBrowserCommandInterface = true; //defined in all main apps and set if this file exists
class BrowserCommandInterface
{
    constructor(model)
    {
        this.Model = model;
        // this.timeOut = 0;
        // this.communicationSimulatortimeOutCanceller;
        this.ReadContinuouslyCancellationToken;
        this.htmlPageUniqueId = 0;
    }

    async invokeJavascriptAppAction(data, interval) //This gets overwritten if running from Navitas App
    { //so a good place to start a simulation if not running in the App
        //if (Appp.NavitasMotorController.Communications.PageCommunicationsListPointer.Count > 1)
        //    console.log("Why was this not removed 2");

        if (this.htmlPageUniqueId != 0)
        {
            clearTimeout(this.ReadContinuouslyCancellationToken); //stop continuous just like App would
            this.htmlPageUniqueId = 0;
        }

        //Read Contiuously: just hacked in, it constantly builds and removes packets
        //TODO: Make it like the app when you have time
        if (data.ReadContinuously)
        {
            let pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, "A Title");

            Object.entries(data.ReadContinuously).forEach(([key, keyValue]) =>
            {
                pageParameters.parameterList.Add(this.Model.GetParameter(key));
            });

            if (pageParameters.parameterList.Count != 0)
            {
                return this.ReadWithoutWait(pageParameters, interval); //blocking communications read packet sent
            }
            //SendJsCommandToWebview(pageParameters); Voila, commented out because we are in Javascript
        }
        else if (data.Read)
        {
            let pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, "A Title");

            Object.entries(data.Read).forEach(([key, keyValue]) =>
            {
                pageParameters.parameterList.Add(this.Model.GetParameter(key));
            });

            if (pageParameters.parameterList.Count != 0)
            {
                return this.ReadWithWait(pageParameters); //blocking communications read packet sent
            }
            //SendJsCommandToWebview(pageParameters); Voila, commented out because we are in Javascript
        }
        else if (data.GetCurrentLocation)
        {
            return await this.GetCurrentLocation(data.GetCurrentLocation.continuously == 1);
        }
        else if (data.GetOrientation)
        {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
        else if (data.Write)
        {
            let pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, "A Title");

            Object.entries(data.Write).forEach(([key, keyValue])  =>
            {
                let parameter = this.Model.GetParameter(key);

                if (parameter.SubsetOfAddress)
                {//this sets single or multiple bits
                    let parentParameter = this.Model.GetParameter(parameter.Address); //returns Parent parameter (ie same address but SubsetOfAddress is false)

                    var temp;
                    if (parentParameter.bupdate)
                        temp = parentParameter.rawValue; //starting value for bit manipulation
                    else //someone else is currently manipulating these bits (hopefully from this list of parameters)
                        temp = parentParameter.rawValueToBeWritten

                    let clearMask = 0;
                    for (var i = 15; i >= 0; i--)
                    {
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
                    parameter.rawValueToBeWritten = parseInt(parseFloat(keyValue) * parameter.Scale);
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

            if (pageParameters.parameterList.Count != 0)
            {
                return this.WriteWithWait(pageParameters); //blocking communications read packet sent
            }
            //SendJsCommandToWebview(pageParameters); Voila, commented out because we are in Javascript
        }
        else if (data.ReadScopeDataBlock)
        {
            let pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, "A Title");

            Object.entries(data.ReadScopeDataBlock).forEach(([key, keyValue]) =>
            {
                let parameter = this.Model.GetParameter(key);
                parameter.rawValueToBeWritten = parseInt(keyValue);

                if (!pageParameters.parameterList.some(e => e.PropertyName === parameter.PropertyName))
                    pageParameters.parameterList.Add(parameter);
            });

            if (pageParameters.parameterList.Count != 0)
            {
                return this.ReadScopeBlockWithWait(pageParameters); //blocking communications read packet sent
            }
        }
        else if (data.ControlVerified)
        {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
        else if (data.Close)
        {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }
        else if (data.GetAppParameters)
        {
            setTimeout(() => this.Model.CompleteTheJsonCommand([]), 200);
        }

        return true;
    }
    async ReadWithWait(pageParameters, interval = 0, unsolicited = false)
    {
        // //wait if a previous continuous read is still executing (ie in the queue, ie TODO:comms still need a bit more thought)
        // this.timeOut = 3000;
        // while (this.timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().length > 0)
        // {
        //     await Task.Delay(10);
        //     this.timeOut -= 10;
        // }
        // if (this.timeOut <= 0)
        // {
        //     //if we are here then in the future retry somewhere around here if you really need this packet to be sent
        //     this.Model.Communications.RemovePacketList(this.Model.Communications.GetUniqueIdToBeRemoved());
        //     console.log("I'm guessing we lost communications while ReadWithWait from " + this.Model.ModelType);
        // }

        let multiPacketList = this.Model.Communications.protocal.BuildAReadCommandPageList(pageParameters);
        let uniqueID = multiPacketList.parentPage.uniqueID
        multiPacketList.parentPage.ProtocalCommandType = PageParameterList.ProtocalCommandTypes.READ;

        this.Model.Communications.PageCommunicationsListPointer.Add(multiPacketList);
        this.Model.Communications.SetUniqueIdToBeRemoved(uniqueID); //send once
        pageParameters.Active = true; //starts as disabled

        //wait for  it to be sent and removed
        let timeOut = 3000;
        let result = true;
        while (timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().includes(uniqueID))
        {
            await Task.Delay(10);
            timeOut -= 10;
        }
        if (timeOut <= 0)
        {
            //if we are here then in the future retry somewhere around here if you really need this packet to be sent
            result = false;
            this.Model.Communications.RemovePacketList(uniqueID);
            console.log("I'm guessing we lost communications while ReadWithWait from " + this.Model.ModelType + ". Page uniqueID: " + uniqueID);
        }
        if (!unsolicited) this.Model.CompleteTheJsonCommand([]);
        else this.Model.UnsolicitedCompleteTheJsonCommand([]);
        if (interval != 0 && this.htmlPageUniqueId != 0)
            this.ReadContinuouslyCancellationToken = setTimeout(async () => await this.ReadWithWait(pageParameters, interval, true), interval);
        return result;
    }
    async WriteWithWait(pageParameters)
    {
        // //wait for  it to be sent and removed
        // this.timeOut = 3000;
        // while (this.timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().length > 0)
        // {
        //     await Task.Delay(10);
        //     this.timeOut -= 10;
        // }
        // if (this.timeOut <= 0)
        // {
        //     //if we are here then in the future retry somewhere around here if you really need this packet to be sent
        //     this.Model.Communications.RemovePacketList(this.Model.Communications.GetUniqueIdToBeRemoved());
        //     console.log("I'm guessing we lost communications while WriteWithWait from " + this.Model.ModelType);
        // }

        let multiPacketList = this.Model.Communications.protocal.BuildAWriteCommandPageList(pageParameters);
        let uniqueID = multiPacketList.parentPage.uniqueID
        multiPacketList.parentPage.ProtocalCommandType = PageParameterList.ProtocalCommandTypes.WRITE;

        this.Model.Communications.PageCommunicationsListPointer.Add(multiPacketList);
        this.Model.Communications.SetUniqueIdToBeRemoved(uniqueID);  //send once
        pageParameters.Active = true; //starts as disabled

        //wait for  it to be sent and removed
        let timeOut = 3000;
        let result = true;
        while (timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().includes(uniqueID))
        {
            await Task.Delay(10);
            timeOut -= 10;
        }
        if (timeOut <= 0)
        {
            //if we are here then in the future retry somewhere around here if you really need this packet to be sent
            result = false;
            this.Model.Communications.RemovePacketList(uniqueID);
            console.log("I'm guessing we lost communications while WriteWithWait from " + this.Model.ModelType + ". Page uniqueID: " + uniqueID);
        }

        pageParameters.parameterList.forEach((parameter, index, array) =>
        {
            parameter.bupdate = true; //so that reads can update this variable
            // parameter.rawValue = parameter.rawValueToBeWritten
        });
        this.Model.CompleteTheJsonCommand([]);
        return result;
    }
    async ReadWithoutWait(pageParameters, interval)
    {
        this.htmlPageUniqueId = 1; //not used like c# for now
        return this.ReadWithWait(pageParameters, interval);
    }
    async GetCurrentLocation(continuously = false)
    {
        return new Promise((resolve) => {
            if (continuously) {
                navigator.geolocation.watchPosition(
                    (position) => {
                        // coords = position.coords;
                        this.Model.GetParameter("LATITUDE").parameterValue = position.coords.latitude;
                        this.Model.GetParameter("LONGITUDE").parameterValue = position.coords.longitude;
                        this.Model.GetParameter("ALTITUDE").parameterValue = position.coords.altitude;
                        this.Model.CompleteTheJsonCommand([]);
                        resolve(true)
                    },
                    (err) => {
                        console.warn(`ERROR(${err.code}): ${err.message}`);
                        this.Model.CompleteTheJsonCommand([]);
                        resolve(false);
                    },
                    {
                        enableHighAccuracy: true
                    }
                );
            }
            else {
                navigator.geolocation.getCurrentPosition(
                    (position) => {
                        // coords = position.coords;
                        this.Model.GetParameter("LATITUDE").parameterValue = position.coords.latitude;
                        this.Model.GetParameter("LONGITUDE").parameterValue = position.coords.longitude;
                        this.Model.GetParameter("ALTITUDE").parameterValue = position.coords.altitude;
                        this.Model.CompleteTheJsonCommand([]);
                        resolve(true)
                    },
                    (err) => {
                        console.warn(`ERROR(${err.code}): ${err.message}`);
                        this.Model.CompleteTheJsonCommand([]);
                        resolve(false);
                    },
                    {
                        enableHighAccuracy: true
                    }
                );
            }
        });
    }
    async ReadScopeBlockWithWait(pageParameters)
    {
        // //wait if a previous continuous read is still executing (ie in the queue, ie TODO:comms still need a bit more thought)
        // this.timeOut = 3000;
        // while (this.timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().length > 0)
        // {
        //     await Task.Delay(10);
        //     this.timeOut -= 10;
        // }
        // if (this.timeOut <= 0)
        // {
        //     //if we are here then in the future retry somewhere around here if you really need this packet to be sent
        //     this.Model.Communications.RemovePacketList(this.Model.Communications.GetUniqueIdToBeRemoved());
        //     console.log("I'm guessing we lost communications while ReadScopeBlockWithWait from " + this.Model.ModelType);
        // }

        let multiPacketList = this.Model.Communications.protocal.BuildAReadScopeBlockCommandPageList(pageParameters);
        let uniqueID = multiPacketList.parentPage.uniqueID
        multiPacketList.parentPage.ProtocalCommandType = PageParameterList.ProtocalCommandTypes.READ;

        this.Model.Communications.PageCommunicationsListPointer.Add(multiPacketList);
        this.Model.Communications.SetUniqueIdToBeRemoved(uniqueID); //send once
        pageParameters.Active = true; //starts as disabled

        //wait for  it to be sent and removed
        let timeOut = 3000;
        let result = true;
        while (timeOut > 0 && this.Model.Communications.GetUniqueIdToBeRemoved().includes(uniqueID))
        {
            await Task.Delay(10);
            timeOut -= 10;
        }
        if (timeOut <= 0)
        {
            //if we are here then in the future retry somewhere around here if you really need this packet to be sent
            result = false;
            this.Model.Communications.RemovePacketList(uniqueID);
            console.log("I'm guessing we lost communications while ReadScopeBlockWithWait from " + this.Model.ModelType + ". Page uniqueID: " + uniqueID);
        }

        pageParameters.parameterList.forEach((parameter, index, array) =>
        {
            let scopeChannel = this.Model.GetParameter(parameter.PropertyName).rawValueToBeWritten;
            let scopeSelect = this.Model.GetParameter(scopeChannel + 217).parameterValue;
            let paramSelect = this.Model.GetParameter(scopeSelect);
            this.Model.Model.ScopeDataArray[0].parameterTimeAndValuePairList.forEach((data) => { data.value = data.value / paramSelect.Scale });
            this.Model.CompleteTheJsonCommand([{
                PropertyName: scopeSelect.PropertyName,
                parameterTimeAndValuePairList: this.Model.Model.ScopeDataArray[0].parameterTimeAndValuePairList
            }]);
        });
        return result;
    }
}