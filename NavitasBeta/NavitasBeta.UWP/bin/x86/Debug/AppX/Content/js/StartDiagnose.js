App = { VehicleDiagnostics: new VehicleDiagnostics() }

if (typeof HasBrowserCommandInterface != 'undefined') {
    App.NavitasMotorController = new NavitasMotorControllerModel(new CommunicationsInterface(new NavitasModbus())) //just so it looks like the c# NavitasBeta App.
}
else {
    App.NavitasMotorController = new NavitasMotorControllerModel(new CommunicationsInterface({})) //just so it looks like the c# NavitasBeta App.
}

window.addEventListener("unload", async function () {
    App.VehicleDiagnostics.StopTest = false;
    App.NavitasMotorController.Communications.bEnableCommunicationTransmissions = false;
    console.log("Trying to Close ");
    if (typeof HasBrowserCommandInterface != 'undefined') App.NavitasMotorController.Communications.DeviceSocket.websocket.close();
});

// Without jQuery
// Define a convenience method and use it
let startDiagnoseHtmlready = (callback) => {
    //screen.orientation.addEventListener("change", callback);
    if (document.readyState != "loading") callback();
    else document.addEventListener("DOMContentLoaded", callback);
}
startDiagnoseHtmlready(async () => {
    console.log("DOM loaded");
    var golfCartScanner = new RadarScan(document.getElementById("myCanvas"));
    golfCartScanner.startRadarScan(1000 / 60); //frame rate = 60/s
    setTimeout(() => golfCartScanner.stopRadarScan(), 5000); //just do a couple scans
    await WaitForPhoneAppToStartThis(); //uncomment to run App0
});

async function InitializeScreen() {
    if (document.querySelector(".stationary-diagnostics").style.display != "") {
        for (const [test, subtests] of Object.entries(StationaryTestSequenceDictionary)) {
            for (const [subtest, value] of Object.entries(subtests)) {
                let testNameResult = document.querySelector(`.test-name-result.${subtest}`);
                let testNameResultContext = testNameResult.getContext('2d');
                testNameResultContext.clearRect(0, 0, testNameResult.width, testNameResult.height);
                let testDetails = document.querySelector(`.test-details.${subtest}`);
                let testInfo = testDetails.querySelectorAll(`.test-info`);
                testInfo.forEach(
                    function (child, index, list) {
                        let testInfoResult = child.querySelector(`.test-info-result`);
                        let testInfoResultContext = testInfoResult.getContext('2d');
                        testInfoResultContext.clearRect(0, 0, testInfoResult.width, testInfoResult.height);
                    }
                );
            }
        }
    }
    else {

    }
}

let controlButtonFn = null;
function ConfigButton(text, color = '#ffff00') {
    let controlButton = document.querySelector(".control-button");
    controlButton.innerHTML = text;
    if (text == 'START') {
        controlButton.style.background = "#27ae60";
        controlButton.removeEventListener('click', controlButtonFn);
        if (document.querySelector(".stationary-diagnostics").style.display != "") {
            controlButtonFn = async function () {
                App.VehicleDiagnostics.StopTest = false;
                RunCompleteStationaryTest();
            }
        }
        else {
            controlButtonFn = async function () {
                App.VehicleDiagnostics.StopTest = false;
                RunCompleteDrivingTest();
                TrackLocationContinuously();
            }
        }
        controlButton.addEventListener("click", controlButtonFn);
    }
    else if (text == 'STOP') {
        controlButton.style.background = "#ae2727ff";
        controlButton.removeEventListener('click', controlButtonFn);
        controlButtonFn = async function () {
            App.VehicleDiagnostics.StopTest = true;
            ConfigButton('STOPPING');
        }
        controlButton.addEventListener("click", controlButtonFn);
    }
    else if (text == 'STOPPING') {
        controlButton.style.background = "#ae6f27";
    }
    else {
        controlButton.style.background = color;
    }
}

// function VehicleLock() {
//     App.NavitasMotorController.JsonCommands({
//         Read: {
//             "VEHICLELOCKED": 0
//         }
//     });
//     let vehicleLocked = App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue;
//     let popupLockButton = document.querySelector(`.popup-lock-button`);
//     if (vehicleLocked == 0) {
//         popupLockButton.src = "./images/ionlocked.png";
//         popupLockButton.setAttribute("alt", "tap to unlock");
//     }
//     else {
//         popupLockButton.src = "./images/ionunlocked.png";
//         popupLockButton.setAttribute("alt", "tap to lock");
//     }
// }

async function TestStateMachine() {
    let diagHome = document.querySelector(".diagnostics-home");
    let diagBody = document.querySelector(".diagnostics-body");
    let stationaryDiagBody = document.querySelector(".stationary-diagnostics");
    let drivingDiagBody = document.querySelector(".driving-diagnostics");
    let stationaryDiagButton = document.querySelector("#stationary-diag-button");
    let drivingDiagButton = document.querySelector("#driving-diag-button");
    stationaryDiagButton.addEventListener('click', async () => {
        diagHome.style.display = "none";
        diagBody.style.display = "block";
        stationaryDiagBody.style.display = "flex";
        await StationaryDiagnostics();
    });
    drivingDiagButton.addEventListener('click', async () => {
        diagHome.style.display = "none";
        diagBody.style.display = "block";
        drivingDiagBody.style.display = "block";
        await DrivingDiagnostics();
    });
    document.querySelector("#ready").style.display = "block";
}

async function DrivingDiagnostics() {
    if (await App.NavitasMotorController.JsonCommands({ GetCurrentLocation: { "continuously": 0 } })) {
        console.log(`Latitude: ${App.NavitasMotorController.GetParameter("LATITUDE").parameterValue}. Longitude: ${App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue}`);
        App.VehicleDiagnostics.Map = L.map('map').setView([
            App.NavitasMotorController.GetParameter("LATITUDE").parameterValue,
            App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue
        ], 13);
        L.tileLayer('https://{s}.google.com/vt/lyrs=s&x={x}&y={y}&z={z}',{
            maxZoom: 20,
            subdomains:['mt0','mt1','mt2','mt3']
        }).addTo(App.VehicleDiagnostics.Map);
        // L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        //     maxZoom: 19,
        //     attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        // }).addTo(map);
    }

    let drivingDiagnostics = document.querySelector(`.driving-diagnostics`);
    for (const [test, params] of Object.entries(DrivingTestSequenceDictionary)) {
        for (const param of params.paramsToMonitor) {
            let chart = document.querySelector(`.resources .driving-chart`);
            drivingDiagnostics.appendChild(chart.cloneNode(true));
            chart = drivingDiagnostics.querySelector(`.driving-chart:last-child`);
            chart.id = `line-chart-${param}`;
            App.VehicleDiagnostics.BuildChart({
                param: `#line-chart-${param}`,
                name: `${App.NavitasMotorController.GetParameter(param).Name}`,
                type: "line"
            });
        }
    }
    ConfigButton('START');
}

async function StationaryDiagnostics() {
    await App.NavitasMotorController.JsonCommands({
        Read: {
            "SOFTWAREREVISION": 0
        }
    })
    let firmwareRev = await App.NavitasMotorController.GetParameter("SOFTWAREREVISION").parameterValue;
    for (const [test, subtests] of Object.entries(StationaryTestSequenceDictionary)) {
        App.VehicleDiagnostics.BuildHTMLTable({
            Header: {
                name: test
            }
        });
        for (const [subtest, value] of Object.entries(subtests)) {
            if (value.isImplemented && (value.vehicleType.includes(firmwareRev) || !value.vehicleType.length)) {
                App.VehicleDiagnostics.BuildHTMLTable({
                    Test: {
                        name: subtest,
                        description: value.description,
                        fn: value.run,
                        mcontrol: value.manualControl
                    }
                });
            }
        }
    }
    ConfigButton('START');
}

async function RunCompleteDrivingTest() {
    await InitializeScreen();
    ConfigButton("STOP");

    let listOfParams = {};
    let listOfCharts = [];
    for (let [param, chart] of Object.entries(App.VehicleDiagnostics.ChartDataDict)) {
        param = param.split("-").pop();
        listOfParams[param] = 0;
        listOfCharts.push(async () => {
            await App.VehicleDiagnostics.UpdateChart(param);
        });
    }

    while (!App.VehicleDiagnostics.StopTest) {
        await App.NavitasMotorController.JsonCommands({
            Read: listOfParams
        });
        await Promise.allSettled(listOfCharts.map(f => f()));
        await Task.Delay(50); // be nice in this loop
    }
    // latlngs = kFilter.filterAll(latlngs);
    // await drawRoute();
    ConfigButton("START");
}

async function RunCompleteStationaryTest() {
    await InitializeScreen();
    ConfigButton("STOP");
    let firmwareRev = await App.NavitasMotorController.GetParameter("SOFTWAREREVISION").parameterValue;
    for (const [test, subtests] of Object.entries(StationaryTestSequenceDictionary)) {
        if (App.VehicleDiagnostics.StopTest) break;
        for (const [subtest, value] of Object.entries(subtests)) {
            if (value.isImplemented && (value.vehicleType.includes(firmwareRev) || !value.vehicleType.length)) {
                if (App.VehicleDiagnostics.StopTest) break;
                await App.VehicleDiagnostics.TestFunctionsWrapper(subtest, value.run);
            }
        }
    }
    ConfigButton("START");
}

async function TrackLocationContinuously() {
    await App.NavitasMotorController.JsonCommands({ GetOrientation: { "on": 0 } });  // turn on gyroscope monitoring
    while (!App.VehicleDiagnostics.StopTest) {
        await App.NavitasMotorController.JsonCommands({ GetCurrentLocation: { "continuously": 0 } });
        await drawRoute();
        await Task.Delay(5000);
    }
    await App.NavitasMotorController.JsonCommands({ GetOrientation: { "off": 0 } });  // turn off gyroscope monitoring
}

let latlngs = [];
let circleMap;
async function drawRoute() {
    if (circleMap) App.VehicleDiagnostics.Map.removeLayer(circleMap);  // remove the old blue circle
    // create a blue circle indicating current position
    circleMap = L.circle([
        App.NavitasMotorController.GetParameter("LATITUDE").parameterValue,
        App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue
    ],{radius: 1, color: 'white', weight: 0.5, opacity: 1, fillColor: '#00d9ff', fillOpacity: 1}).addTo(App.VehicleDiagnostics.Map);

    // create a red polyline from an array of LatLng points
    if (latlngs.length != 0) {
        if (latlngs[latlngs.length - 1][0] != App.NavitasMotorController.GetParameter("LATITUDE").parameterValue || latlngs[latlngs.length - 1][1] != App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue) {
            latlngs.push([
                App.NavitasMotorController.GetParameter("LATITUDE").parameterValue,
                App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue
            ]);
        }
    }
    else {
        latlngs.push([
            App.NavitasMotorController.GetParameter("LATITUDE").parameterValue,
            App.NavitasMotorController.GetParameter("LONGITUDE").parameterValue
        ]);
    }
    // latlngs = kFilter.filterAll(latlngs);
    L.polyline(latlngs, {color: 'red'}).addTo(App.VehicleDiagnostics.Map);

    App.VehicleDiagnostics.Map.fitBounds(circleMap.getBounds());
}

let startDiagnoseHtmlSimulationCheckTimeout = 80; //a global var because static var is not a thing
async function WaitForPhoneAppToStartThis() {
    //if this is running in the actual Phone App then the App will inject a function called invokeCSharpAction()
    //so give it a couple seconds to do that (should only take 100ms)
    //startDiagnoseHtmlSimulationCheckTimeout = 0;
    if (typeof HasBrowserCommandInterface != 'undefined') { //This must be running from a browser
        //RG Nov 27, Something has to be done here if simulating
        invokeSimulatedActionIsEnabled = false;
        console.log("Browser detected");
        App.NavitasMotorController.Communications.protocal.bEnableCommunicationTransmissions = true;
        await App.NavitasMotorController.Communications.BeginThread();
        while (App.NavitasMotorController.ModelType == "")// || App.RegressionTestBoxModelA.ModelType == "" || App.firmwareDownload.AlreadyTalkingToBootloader == true) //wait for communications to start
            await Task.Delay(500);
        App.NavitasMotorController.CommandHandler = new BrowserCommandInterface(App.NavitasMotorController);
        TestStateMachine();
    }
    // else if (typeof invokeCSharpAction == 'function') { //This must be running from the Phone App
    else if (typeof invokeCSharpAction == 'function') {
        invokeSimulatedActionIsEnabled = false;
        console.log("Phone app detected");
        await App.NavitasMotorController.JsonCommands({ GetAppParameters: {} }); //will update things like App.NavitasMotorController.ModelType
        await App.NavitasMotorController.JsonCommands({ LoadAllTestScripts: {} }); //will update things like App.NavitasMotorController.ModelType
        TestStateMachine();
    }
    else {
        if (startDiagnoseHtmlSimulationCheckTimeout == 0) {
            invokeSimulatedActionIsEnabled = true;
            console.log("Simulation detected");
            App.NavitasMotorController.Model = "TAC";
            App.SimulatedMotorController = new NavitasMotorControllerModel(new CommunicationsInterface({}));
            App.SimulatedMotorController.Model = "TAC";
            App.NavitasMotorController.CommandHandler = new SimulatedCommandInterface(App.NavitasMotorController, App.SimulatedMotorController);
            // InitializeSimulatorModel();
            // setTimeout(() => App.NavitasMotorController.CommandHandler.StartNavitasControllerSimulator(10, () => { DiagnosticsThrottleCalibrationSimulator() }), 101); //to run your specific Unit Test simulator
            TestStateMachine();
        }
        else {
            startDiagnoseHtmlSimulationCheckTimeout -= 1;
            setTimeout(WaitForPhoneAppToStartThis, 100);
        }
    }
}
function callBackToTrapAndSetBreakpointsForExternallyLoadedJavascript() {
    console.log("Set Breakpoint Here"); //set breakpoint here then step out when you get it to get to external scripts
}
