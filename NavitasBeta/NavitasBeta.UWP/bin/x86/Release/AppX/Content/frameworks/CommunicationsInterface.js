// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using System.Threading.Tasks;
// using Xamarin.Forms;
// using System.Linq;

// using Xamarin.Essentials;

// namespace NavitasBeta
// {
let DEBUG_VEHICLE_COMMS = false;
class CommunicationsInterface extends EventTarget //this might be temporaty but EventTarget is used to access the delegate stucture used in an event
{
    constructor(protocalHandler, deviceURL = "ws://127.0.0.1:8283/")// public DeviceComunication()
    {
        super();
        const _this = this; //so constructor functions can access this instead of the function this
        this.deviceURL = deviceURL;
        this.protocal = protocalHandler;
        this.protocal.TransmitPayload = function (dataArray)
        {
            _this.TransmitPayload(dataArray);
        }
        this.PacketResponseReceived = function (e)
        {
            _this.protocal.PacketResponseReceived(e);
        }
        this.protocal.UpdateModelWithPacketResponse = function (uniqueID, packetIndex, processedReceivedData)
        {
            _this.UpdateModelWithPacketResponse(uniqueID, packetIndex, processedReceivedData)
        }

        this.uniqueIdToBeRemoved = [];

        this.PageCommunicationsListPointer = this.protocal.DeviceCommunicationsParameterAndPacketList; //initialize it to something to avoid null exceptions during early access

        this.packetIndex = 0;

        this.DeviceSocket = {
            websocket: {},
            websocketState: ""
        };
        //0 – “CONNECTING”: the connection has not yet been established,
        //1 – “OPEN”: communicating,
        //2 – “CLOSING”: the connection is closing,
        //3 – “CLOSED”: the connection is closed.
        this.ws = { CONNECTING: 0, OPEN: 1, CLOSING: 2, CLOSED: 3};
        //Note: I had to make an object DeviceSocket to be able to pass it as a reference below
        //blank or empty or null objects we passed by value? and never filled in the object.. a little odd.
        //wsUri = "ws://echo.websocket.org/" for testing
        if ((typeof HasBrowserCommandInterface != 'undefined')) this.OpenAWebsocket(this.DeviceSocket, this.deviceURL);
        // this.testerCommSocket = {
        //     websocket: {},
        //     websocketState: ""
        // };
        //this.OpenAWebsocket(this.testerCommSocket, "ws://127.0.0.1:8284/");
    }

    OpenAWebsocket(socketPointer, wsUri)
    {
        const _this = this;
        console.log("Trying to Open " + wsUri);
        socketPointer.websocket = new WebSocket(wsUri);
        socketPointer.websocket.binaryType = "arraybuffer";
        socketPointer.websocketState = "CONNECTING"

        socketPointer.websocket.onopen = function (e)
        {
            if (socketPointer.websocket == undefined)
            {
                console.log("Odd open error in " + wsUri);
                setTimeout(() =>
                {
                    _this.OpenAWebsocket(socketPointer, wsUri); //keep trying
                }, 1000
                );
                return;
            }

            if ((typeof writeToScreen == 'function')) writeToScreen("CONNECTED");
            socketPointer.websocketState = "CONNECTED"
            console.log("Opened " + wsUri);
        };

        socketPointer.websocket.onclose = function (e)
        {
            if ((typeof writeToScreen == 'function')) writeToScreen("DISCONNECTED");
            socketPointer.websocketState = "CLOSED"
            console.log("Closed " + wsUri);
        };

        socketPointer.websocket.onmessage = function (e)
        {
            let view = new Uint8Array(e.data)
            console.log(_this.protocal.ModelType + " message: " + view);
            if (e.data instanceof Blob)
            {
                var reader = new FileReader();

                reader.onload = () =>
                {
                    if ((typeof writeToScreen == 'function')) writeToScreen("<span>Binary RESPONSE: " + toHexString(reader.result) + "</span>");
                };

                reader.readAsText(e.data)
            }
            else
            {
                if ((typeof writeToScreen == 'function')) writeToScreen("<span>RESPONSE: " + toHexString(e.data) + "</span>");
                _this.PacketResponseReceived(e);
            }
        };

        socketPointer.websocket.onerror = function (e)
        {
            socketPointer.websocketState = "ERRORED"
            console.log("Error in " + _this.protocal.ModelType + "(" + wsUri + "): " + e.data);
            setTimeout(() =>
                {
                    _this.OpenAWebsocket(socketPointer, wsUri); //keep trying
                }, 1000
            );
        };
    }

    async BeginThread()
    {
        while (this.DeviceSocket.websocketState != "CONNECTED")
            await Task.Delay(50); //be nice in this loop
        this.protocal.BeginThread();
        while (this.protocal.ModelType === "")
            await Task.Delay(50); //be nice in this loop
        console.log("Starting " + this.protocal.ModelType + " communication thread");
    }

    TransmitPayload(dataArray)
    {
        try
        {
            if (dataArray.length == 1)
            {
                let a = new Uint8Array(dataArray);
                this.DeviceSocket.websocket.send(a);
            }
            else
            {
                var d = new Uint8Array(dataArray);
                if (!(this.DeviceSocket.websocket.readyState === this.ws.OPEN))
                {
                    console.log("this is an odd error " + this.DeviceSocket.websocket.readyState);
                    if (this.DeviceSocket.websocketState == "CLOSED")
                        this.OpenAWebsocket(this.DeviceSocket, this.deviceURL);
                }
                else
                {
                    this.DeviceSocket.websocket.send(d);
                }
            }
        }
        catch(ex) {
            console.log("TransmitPayload WebSocket unexpected error " + ex);
        }
    }

    UpdateModelWithPacketResponse(uniqueID, packetIndex, processedReceivedData)
    {
        try
        {
            var packetParentPointer = this.PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == uniqueID));

            if (processedReceivedData.length != 0)
            {//is a READ responce
                packetParentPointer.parametersGroupedto64bytesAndAddressRange[packetIndex].forEach((parameter, index, array) =>
                {
                    //if (parameter.PropertyName == "Options")
                    //    console.log("Options reading " + parameter.parameterValue.toString());
                    parameter.bupdate = true;
                    parameter.rawValue = processedReceivedData[index];
                });
            }

            if (this.uniqueIdToBeRemoved.includes(uniqueID) && packetIndex == (packetParentPointer.parametersGroupedto64bytesAndAddressRange.Count - 1)) //last one in list
            {   // this is the easiest place to remove a packet knowing it has been sent
                this.RemovePacketList(uniqueID);
            }
        }
        catch (ex)
        {
            console.log("UpdateModelWithPacketResponse Exception exited " + ex);
        }

    }

    SetUniqueIdToBeRemoved(uniqueID)
    {
        this.uniqueIdToBeRemoved.push(uniqueID);
    }

    GetUniqueIdToBeRemoved()
    {
        return this.uniqueIdToBeRemoved;
    }

    RemovePacketList(uniqueID)
    {
        // if (this.PageCommunicationsListPointer.Count > 1)
        //     console.log("How did I get two packets here");
        if (typeof uniqueID == "number") {
            this.PageCommunicationsListPointer.Remove(uniqueID);
            const index = this.uniqueIdToBeRemoved.indexOf(uniqueID);
            if (index > -1) {  // only splice array when item is found
                this.uniqueIdToBeRemoved.splice(index, 1);  // 2nd parameter means remove one item only
            }
        }
        else if (Array.isArray(uniqueID)) {
            uniqueID.forEach((id, index, array) => {
                this.PageCommunicationsListPointer.Remove(id);
                const i = this.uniqueIdToBeRemoved.indexOf(id);
                if (i > -1) {  // only splice array when item is found
                    this.uniqueIdToBeRemoved.splice(i, 1);  // 2nd parameter means remove one item only
                }
            })
        }
        // else this.uniqueIdToBeRemoved = 0; //asynchronous indicator to other threads that packet has been removed
    }
}

class InvalidOperationException extends Error {
    constructor(message) {
        super(message);
        this.name = "InvalidOperationException";
    }
}