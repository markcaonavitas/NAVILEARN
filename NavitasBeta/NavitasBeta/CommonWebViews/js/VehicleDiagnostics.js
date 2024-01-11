class VehicleDiagnostics
{
    constructor()
    {
        const _this = this; //so constructor functions can access this instead of the function this
        this.SkipTest = false;
        this.StopTest = false;
        this.RequireUserInput = false;
        this.TestRunning = false;
        /*
        * Store modular test dynamically
        * Structure:
        * this.TestDict = {
        *   "MainSolenoidDiagnostics": new MainSolenoidDiagnostics()
        * }
        * */
        this.TestDict = {}
        /*
        * Store button function dynamically
        * Structure:
        * this.ButtonFunctionDict = {
        *   "#button-id": function() {}
        * }
        * */
        this.ButtonFunctionDict = {};
        /*
        * Store chart data dynamically
        * Structure:
        * this.ChartDataDict = {
        *   "#chart-id": {
        *     chart: CanvasJs.Chart
        *     chartDPS: [{
        *      xVal: x,
        *      yVal: y
        *     }]
        *    }
        * }
        * */
        this.ChartDataDict = {};
        this.Map = null;
        // let {KalmanFilter} = kalmanFilter;
        // this.kFilter = new KalmanFilter({observation: 2});

        // Get all defined class methods
        const methods = Object.getOwnPropertyNames(Object.getPrototypeOf(this));
        // Bind all methods
        methods
            .filter(method => (method !== 'constructor'))
            .forEach((method) => { this[method] = this[method].bind(this); });
    }

    InitializeDOMSelector()
    {
        this.DiagnosticsBody = document.querySelector(".stationary-diagnostics-body");
        this.DiagnosticsPopUp = document.querySelector(".diagnostics-popup");
        this.PopUpSkipButton = document.getElementById("popup-skip-button");
        this.PopUpSkipButton.addEventListener("click", () => { this.SkipTest = true; });
    }

    BuildHTMLTable(data)
    {
        if (data.Header) {
            let testTitle = document.createElement("div");
            testTitle.classList.add("test-title", `${data.Header.name}`);
            let testTitleHeader = document.createElement("div");
            testTitleHeader.innerHTML = data.Header.name.replace(/-/g, " ").split(" ").map((word) => {
                return word[0].toUpperCase() + word.substring(1);
            }).join(" ");
            testTitle.appendChild(testTitleHeader);
            this.DiagnosticsBody.appendChild(testTitle);
        }
        else if (data.Test) {
            let testName = document.createElement("div");
            testName.classList.add("test-name", `${data.Test.name}`);

            let testNameText = document.createElement("span");
            testNameText.classList.add("test-name-text", `${data.Test.name}`);
            testNameText.innerHTML = data.Test.name.replace(/-/g, " ").split(" ").map((word) => {
                return word[0].toUpperCase() + word.substring(1);
            }).join(" ");
            testName.appendChild(testNameText);

            let testDetails = document.createElement("div");
            testDetails.classList.add("test-details", `${data.Test.name}`);

            let testNameTextDetails = document.createElement("div");
            testNameTextDetails.classList.add("test-name-text-details", `${data.Test.name}`);
            testNameTextDetails.innerHTML = "Details";
            testNameTextDetails.addEventListener("click", async function (e) {
                testDetails.style.display = testDetails.style.display == "" ? "block" : "";
            }, false);
            testName.appendChild(testNameTextDetails);

            let testNameResult = document.createElement("canvas");
            testNameResult.classList.add("test-name-result", `${data.Test.name}`);
            testName.appendChild(testNameResult);

            let testDescription = document.createElement("div");
            testDescription.classList.add("test-description", `${data.Test.name}`);
            testDescription.innerHTML = data.Test.description;
            testDetails.appendChild(testDescription);

            let runTestButton = document.querySelector(`.resources #popup-custom-button-2`);
            testDetails.appendChild(runTestButton.cloneNode(true));
            runTestButton = testDetails.querySelector(`#popup-custom-button-2`);
            // add text or event listener to element
            runTestButton.innerHTML = "Run Test";
            runTestButton.addEventListener('click', async () => {
                await App.VehicleDiagnostics.TestFunctionsWrapper(data.Test.name, data.Test.fn);
            });

            if (data.Test.mcontrol) {
                let controls = document.createElement("div");
                controls.classList.add("controls", `${data.Test.name}`);

                let controlsBody = document.createElement("div");
                controlsBody.classList.add("controls-body", `${data.Test.name}`);

                let manualControlInput = document.querySelector(`.resources #popup-custom-button-2`);
                controls.appendChild(manualControlInput.cloneNode(true));
                manualControlInput = controls.querySelector(`#popup-custom-button-2`);
                // add text or event listener to element
                manualControlInput.innerHTML = "Manual Control";
                manualControlInput.addEventListener('click', () => {
                    controlsBody.style.display = controlsBody.style.display == "" ? "block" : "";
                }, false);

                for (const [index, description] of data.Test.mcontrol.description.entries()) {
                    let controlInfo = document.createElement("div");
                    controlInfo.classList.add("control-info", `${data.Test.name}-${index}`);

                    let controlInfoText = document.createElement("span");
                    controlInfoText.classList.add("control-info-text", `${data.Test.name}-${index}`);
                    controlInfoText.innerHTML = description;
                    controlInfo.appendChild(controlInfoText);

                    let controlInfoInputType = document.querySelector(`.resources .${data.Test.mcontrol.type[index]}`);
                    controlInfo.appendChild(controlInfoInputType.cloneNode(true));
                    controlInfoInputType = controlInfo.querySelector(`.${data.Test.mcontrol.type[index]}:last-child`);
                    controlInfoInputType.classList.add("control-info-input", `${data.Test.name}-${index}`);
                    if (data.Test.mcontrol.bind[index] != "") {
                        const pattern  = /(\w+)(?:\\&(\w+))?/;
                        const match = data.Test.mcontrol.bind[index].match(pattern);
                        if (match) {
                            let [, bindParam, bindParamValue] = match;
                            if (/^0x[\da-f]+$/i.test(bindParamValue)) {
                                // String is a hexadecimal number
                                bindParamValue = parseInt(bindParamValue, 16);
                            } else if (/^\d+$/.test(bindParamValue)) {
                                // String is a decimal number
                                bindParamValue = parseInt(bindParamValue, 10);
                            } else {
                                // String is not a valid number format
                                bindParamValue = null;
                            }
                            let controlInfoInput = controlInfoInputType.querySelector(`input`);
                            // App.NavitasMotorController.GetParameter(bindParam).addBindingToThisSource(controlInfoInput, "checked", "PropertyChanged"); //object binding to element
                            App.NavitasMotorController.GetParameter(bindParam).addEventListener("PropertyChanged", async (e) => {
                                if (controlInfoInput.type == "checkbox") {
                                    controlInfoInput.checked = !!(e.target.parameterValue & (bindParamValue ? bindParamValue : e.target.parameterValue));
                                }
                            }, false);
                        }
                    }
                    controlInfoInputType.addEventListener('click', async (e) => {
                        await data.Test.mcontrol.run[index]();
                    }, false);

                    controlsBody.appendChild(controlInfo);
                }

                controls.appendChild(controlsBody);
                testDetails.appendChild(controls);
            }

            this.DiagnosticsBody.appendChild(testName);
            this.DiagnosticsBody.appendChild(testDetails);
        }
        else if (data.SubTest) {
            let testInfo = document.querySelector(`.test-info.${data.SubTest.stage}`);
            if (testInfo != null) {
                let testInfoText = document.querySelector(`.test-info-text.${data.SubTest.stage}`);
                testInfoText.innerHTML = data.SubTest.text;
                let testInfoResult = document.querySelector(`.test-info-result.${data.SubTest.stage}`);
                testInfoResult.innerHTML = "";
            }
            else {
                let testInfo = document.createElement("div");
                testInfo.classList.add("test-info", `${data.SubTest.stage}`);

                let testInfoText = document.createElement("span");
                testInfoText.classList.add("test-info-text", `${data.SubTest.stage}`);
                testInfoText.innerHTML = data.SubTest.text;
                testInfo.appendChild(testInfoText);
                let testInfoResult = document.createElement("canvas");
                testInfoResult.classList.add("test-info-result", `${data.SubTest.stage}`);
                testInfoResult.innerHTML = "";
                testInfo.appendChild(testInfoResult);

                let testDetails = document.querySelector(`.test-details.${data.SubTest.name}`);
                testDetails.appendChild(testInfo);
            }
        }
        else if (data.PopUp) {
            if (data.PopUp.hasOwnProperty("graph")) {
                // remove child elements if any
                let popupGraphsArea = document.querySelector(`.popup-graphs-area`);
                while (popupGraphsArea.firstChild) {
                    popupGraphsArea.removeChild(popupGraphsArea.firstChild);
                }
                // clone element and add to desire area
                if (!Array.isArray(data.PopUp.graph.id)) {
                    let popupGraph = document.querySelector(`.resources ${data.PopUp.graph.id}`);
                    if (popupGraph) {
                        popupGraphsArea.appendChild(popupGraph.cloneNode(true));
                    }
                }
                else {
                    data.PopUp.graph.id.forEach((id, index, array) => {
                        let popupGraph = document.querySelector(`.resources ${id}`);
                        if (popupGraph) {
                            popupGraphsArea.appendChild(popupGraph.cloneNode(true));
                            popupGraph = document.querySelector(`.popup-graphs-area #${popupGraph.id}`);
                            popupGraph.id = `${popupGraph.id}-${index}`;
                        }
                    });
                }
            }

            if (data.PopUp.hasOwnProperty("text")) {
                // remove child elements if any
                let popupText = document.querySelector(".popup-text");
                popupText.innerHTML = data.PopUp.text;
            }

            if (data.PopUp.hasOwnProperty("button")) {
                // remove child elements if any
                let popupButtonsArea = document.querySelector(`.popup-buttons-area`);
                while (popupButtonsArea.firstChild) {
                    popupButtonsArea.removeChild(popupButtonsArea.firstChild);
                }
                // clone element and add to desire area
                if (!Array.isArray(data.PopUp.button.id)) {
                    let popupButton = document.querySelector(`.resources ${data.PopUp.button.id}`);
                    if (popupButton) {
                        popupButtonsArea.appendChild(popupButton.cloneNode(true));
                        popupButton = document.querySelector(`.popup-buttons-area #${popupButton.id}`);
                        // add text or event listener to element
                        if (data.PopUp.button.hasOwnProperty("text")) popupButton.querySelector(`.popup-button-front.text`).innerHTML = data.PopUp.button.text;
                        // if (data.PopUp.button.hasOwnProperty("color")) popupButton.querySelector(`.popup-button-front.text`).innerHTML = data.PopUp.button.text;
                        if (data.PopUp.button.hasOwnProperty("fn")) {
                            this.ButtonFunctionDict[`#${popupButton.id}`] = data.PopUp.button.fn;  // store reference event listener
                            popupButton.addEventListener('click', this.ButtonFunctionDict[`#${popupButton.id}`]);
                        }
                    }
                }
                else {
                    data.PopUp.button.id.forEach((id, index, array) => {
                        let popupButton = document.querySelector(`.resources ${id}`);
                        if (popupButton) {
                            popupButtonsArea.appendChild(popupButton.cloneNode(true));
                            popupButton = document.querySelector(`.popup-buttons-area #${popupButton.id}`);
                            popupButton.id = `${popupButton.id}-${index}`;
                            // add text or event listener to element
                            if (data.PopUp.button.hasOwnProperty("text")) popupButton.querySelector(`.popup-button-front.text`).innerHTML = data.PopUp.button.text[index];
                            // if (data.PopUp.button.hasOwnProperty("color")) popupButton.querySelector(`.popup-button-front.text`).innerHTML = data.PopUp.button.text;
                            if (data.PopUp.button.hasOwnProperty("fn")) {
                                this.ButtonFunctionDict[`#${popupButton.id}`] = data.PopUp.button.fn[index];  // store reference event listener
                                popupButton.addEventListener('click', this.ButtonFunctionDict[`#${popupButton.id}`]);
                            }
                        }
                    });
                }
            }
        }
    }

    BuildChart(data) {
        this.ChartDataDict[data.param.split("-").pop()] = {
            chart: null,
            chartDPS: []
        }

        this.ChartDataDict[data.param.split("-").pop()].chart = $.plot(data.param, [this.ChartDataDict[data.param.split("-").pop()].chartDPS], {
            series: {
                shadowSize: 0  // Drawing is faster without shadows
            },
            xaxis: {
                show: false,
            },
            yaxis: {
                min: data.ymin,
                max: data.ymax
            }
        });
    }

    async UpdateChart(id) {
        // let yVal = App.NavitasMotorController.GetParameter(id).parameterValue;
        // let xVal = this.ChartDataDict[id].chartDPS.length;
        // this.ChartDataDict[id].chartDPS.push([xVal, yVal]);
        this.ChartDataDict[id].chart.setData([{label: `${App.NavitasMotorController.GetParameter(id).Name}`, data: this.ChartDataDict[id].chartDPS}]);
        // this.ChartDataDict[id].chart.getAxes().xaxis.options.max = this.ChartDataDict[id].chartDPS.length;
        // this.ChartDataDict[id].chart.getAxes().yaxis.options.max = this.ChartDataDict[id].chart.getAxes().yaxis.options.max < yVal ? yVal : this.ChartDataDict[id].chart.getAxes().yaxis.options.max;
        this.ChartDataDict[id].chart.setupGrid();
        this.ChartDataDict[id].chart.draw();
    }

    async TestResult(canvas, paramName = "", paramValue = 0, filterBit, minValue, maxValue, maxTime = 1, { signal } = {})
    {
        return new Promise(async (resolve, reject) =>
        {
            progressBar(canvas, 0, "", "s", maxTime);

            let paramList = {};
            let paramValueList = {};
            let filterBitList = {};
            let minValueList = {};
            let maxValueList = {};

            if (paramName != "" || Array.isArray(paramName)) {
                if (Array.isArray(paramName)) {
                    paramName.forEach((value, index, array) => {
                       paramList[value] = 0;
                       minValueList[value] = minValue[index];
                       maxValueList[value] = maxValue[index];
                       if (Array.isArray(filterBit)) {
                           filterBitList[value] = filterBit[index];
                       }
                    });
                }
                else {
                    paramList[paramName] = 0;
                    minValueList[paramName] = minValue;
                    maxValueList[paramName] = maxValue;
                    if (filterBit) filterBitList[paramName] = filterBit;
                }
            }
            else {
                paramValueList[paramName] = paramValue;
                minValueList[paramName] = minValue;
                maxValueList[paramName] = maxValue;
                if (filterBit) filterBitList[paramName] = filterBit;
            }

            let start = 0;
            let elapsedTime = 0;
            let totalElapsedTime = 0;
            let paramListLength = Object.keys(paramList).length;
            if (paramListLength > 0) {
                await App.NavitasMotorController.JsonCommands({
                    Read: paramList
                });
                await App.NavitasMotorController.JsonCommands({
                    ReadContinuously: paramList
                });
                if (maxTime != 0) {
                    for (const param of Object.keys(paramList)) {
                        start = Date.now();
                        paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                        if (Object.keys(filterBitList).length > 0) {
                            while (!(((paramValueList[param] & filterBitList[param]) >= minValueList[param]) && ((paramValueList[param] & filterBitList[param]) <= maxValueList[param])) && (elapsedTime < maxTime)) {
                                paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                                elapsedTime = (Date.now() - start) / 1000;
                                progressBar(canvas, (elapsedTime + totalElapsedTime).toFixed(0), "", "s", maxTime * paramListLength);
                                await Task.Delay(10);  // be nice in this loop
                                // if (signal) {
                                //     if (signal.aborted) return reject("Aborted");
                                // }
                            }
                        }
                        else {
                            while (!(paramValueList[param] >= minValueList[param] && paramValueList[param] <= maxValueList[param]) && (elapsedTime < maxTime)) {
                                paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                                elapsedTime = (Date.now() - start) / 1000;
                                progressBar(canvas, (elapsedTime + totalElapsedTime).toFixed(0), "", "s", maxTime * paramListLength);
                                await Task.Delay(10);  // be nice in this loop
                                // if (signal) {
                                //     if (signal.aborted) return reject("Aborted");
                                // }
                            }
                        }
                        totalElapsedTime += maxTime;
                    }
                }
                else {
                    for (const param of Object.keys(paramList)) {
                        paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                        if (Object.keys(filterBitList).length > 0) {
                            while (!(((paramValueList[param] & filterBitList[param]) >= minValueList[param]) && ((paramValueList[param] & filterBitList[param]) <= maxValueList[param]))) {
                                paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                                await Task.Delay(10);  // be nice in this loop
                                // if (signal) {
                                //     if (signal.aborted) return reject("Aborted");
                                // }
                            }
                        }
                        else {
                            while (!(paramValueList[param] >= minValueList[param] && paramValueList[param] <= maxValueList[param])) {
                                paramValueList[param] = parseFloat(App.NavitasMotorController.GetParameter(param).parameterValue.toFixed(2));
                                await Task.Delay(10);  // be nice in this loop
                                // if (signal) {
                                //     if (signal.aborted) return reject("Aborted");
                                // }
                            }
                        }
                    }
                }
                await App.NavitasMotorController.JsonCommands({  // cancel ReadContiously
                    Read: paramList
                });
            }

            let result = {};
            let imageToDisplay = "Pass";
            if (paramListLength > 0) {
                for (const param of Object.keys(paramList)) {
                    if (Object.keys(filterBitList).length > 0) {
                        if (((paramValueList[param] & filterBitList[param]) >= minValueList[param]) && ((paramValueList[param] & filterBitList[param]) <= maxValueList[param])) {
                            result[param] = {
                                result: true,
                                value: paramValueList[param]
                            };
                        }
                        else {
                            imageToDisplay = "Fail";
                            result[param] = {
                                result: false,
                                value: paramValueList[param]
                            };
                        }

                    }
                    else {
                        if (paramValueList[param] >= minValueList[param] && paramValueList[param] <= maxValueList[param]) {
                            result[param] = {
                                result: true,
                                value: paramValueList[param]
                            };
                        } else {
                            imageToDisplay = "Fail";
                            result[param] = {
                                result: false,
                                value: paramValueList[param]
                            };
                        }
                    }
                }
            }
            else {
                if (Object.keys(filterBitList).length > 0) {
                    if (((paramValueList[paramName] & filterBitList[paramName]) >= minValueList[paramName]) && ((paramValueList[paramName] & filterBitList[paramName]) <= maxValueList[paramName])) {
                        result = {
                            result: true,
                            value: paramValueList[paramName]
                        };
                    }
                    else {
                        imageToDisplay = "Fail";
                        result = {
                            result: false,
                            value: paramValueList[paramName]
                        };
                    }
                }
                else {
                    if (paramValueList[paramName] >= minValueList[paramName] && paramValueList[paramName] <= maxValueList[paramName]) {
                        result = {
                            result: true,
                            value: paramValueList[paramName]
                        };
                    } else {
                        imageToDisplay = "Fail";
                        result = {
                            result: false,
                            value: paramValueList[paramName]
                        };
                    }
                }
            }

            progressBar(canvas, maxTime, imageToDisplay, "s", maxTime);
            // signal?.removeEventListener("abort", abortHandler);
            return resolve(result);
        });
    }

    InitializePopUp()
    {
        let popupGraphArea = document.querySelector(".popup-graphs-area");
        if (popupGraphArea.firstChild) {
            popupGraphArea.removeChild(popupGraphArea.firstChild);
        }
        let popupText = document.querySelector(".popup-text");
        popupText.innerHTML = "";
        let popupButtonsArea = document.querySelector(`.popup-buttons-area`);
        while (popupButtonsArea.firstChild) {
            popupButtonsArea.removeChild(popupButtonsArea.firstChild);
        }
        this.HidePopUp();
    }

    DisplayPopUp()
    {
        // let diagnosticsPopup = document.querySelector(`.diagnostics-popup`);
        this.DiagnosticsPopUp.style.display = "flex";
    }

    HidePopUp()
    {
        // let diagnosticsPopup = document.querySelector(`.diagnostics-popup`);
        this.DiagnosticsPopUp.style.display = "none";
        this.PopUpSkipButton.disabled = true;
        this.SkipTest = false;
    }

    async MainSolenoidSwitch(state)
    {
        await App.NavitasMotorController.JsonCommands({
            Read: {
                "Options": 0,
                "SetOutputState": 0
            }
        });
        let initialOptionsValue = App.NavitasMotorController.GetParameter("Options").parameterValue;
        let initialOutputState = App.NavitasMotorController.GetParameter("SetOutputState").parameterValue;

        // ensure only valid state is accepted
        if (state == 1) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState | (1 << 0) // set bit 0 to 1 (main solenoid)
                }
            });
        }
        else if (state == 0) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState & ~(1 << 0) // set bit 0 to 0
                }
            });
        }
        else if (state == undefined) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState ^ (1 << 0) // flip bit 0
                }
            });
        }
    }

    async ReverseBuzzerSwitch(state)
    {
        await App.NavitasMotorController.JsonCommands({
            Read: {
                "Options": 0,
                "SetOutputState": 0
            }
        });
        let initialOptionsValue = App.NavitasMotorController.GetParameter("Options").parameterValue;
        let initialOutputState = App.NavitasMotorController.GetParameter("SetOutputState").parameterValue;

        // ensure only valid state is accepted
        if (state == 1) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState | (1 << 4) // set bit 4 to 1 (reverse buzzer)
                }
            });
        }
        else if (state == 0) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState & ~(1 << 4) // set bit 4 to 0 (reverse buzzer)
                }
            });
        }
        else if (state == undefined) {
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "Options": initialOptionsValue | (1 << 4),  // set bit 4 to 1 (enable manufacturing options)
                    "SetOutputState": initialOutputState ^ (1 << 4) // flip bit 4
                }
            });
        }
    }

    async TestFunctionsWrapper(testNames, test)
    {
        this.TestRunning = true;
        let finalResultImage = document.querySelector(`.test-name-result.${testNames}`);
        progressBar(finalResultImage, 0, "");
        let result = await test(finalResultImage, testNames);
        progressBar(finalResultImage, result.percent, result.result);
        this.TestRunning = false;
    }

    async MotorPhasesTest (canvas, testName)
    {
        /*
        * Check motor phase voltages when switching MOSFETs
        */
        let finalResult = "Pass";
        let percentage = 0;

        await App.NavitasMotorController.JsonCommands({
            Read: {
                "VBATVDC": 0
            }
        });
        let batteryVoltage = parseFloat(App.NavitasMotorController.GetParameter("VBATVDC").parameterValue.toFixed(2));

        //------------ Check initial phase voltages when main solenoid is released ------------//
        let stageName = "initial-phase-voltage";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check initial phase voltages"
            }
        });
        let stageText = document.querySelector(`.test-info-text.${stageName}`);
        let stageResult = document.querySelector(`.test-info-result.${stageName}`);
        let minPhaseVoltage = (batteryVoltage / 2) * 0.95;
        let maxPhaseVoltage = (batteryVoltage / 2) * 1.05;
        let initPhases = await this.TestResult(stageResult, ["VABVRMS", "VBCVRMS", "VCAVRMS"], undefined, undefined, [minPhaseVoltage, minPhaseVoltage, minPhaseVoltage], [maxPhaseVoltage, maxPhaseVoltage, maxPhaseVoltage], 3)
        // const [initPhaseA, initPhaseB, initPhaseC] = await Promise.all([
        //     this.TestResult(stageResult, "VABVRMS", undefined, undefined, minPhaseVoltage, maxPhaseVoltage, 3).catch((error) => { return { result: false, value: NaN }; }),
        //     this.TestResult(stageResult, "VBCVRMS", undefined, undefined, minPhaseVoltage, maxPhaseVoltage, 3).catch((error) => { return { result: false, value: NaN }; }),
        //     this.TestResult(stageResult, "VCAVRMS", undefined, undefined, minPhaseVoltage, maxPhaseVoltage, 3).catch((error) => { return { result: false, value: NaN }; })
        // ]);
        if (initPhases["VABVRMS"].result && initPhases["VBCVRMS"].result && initPhases["VCAVRMS"].result) {
            stageText.innerHTML = `Initial off phase voltages are:<br>Phase A (W): ${initPhases["VABVRMS"].value}V<br>Phase B (V): ${initPhases["VBCVRMS"].value}V<br>Phase C (U): ${initPhases["VCAVRMS"].value}V`;
        }
        else {
            let text = "";
            if (initPhases["VABVRMS"].value < minPhaseVoltage) {
                text = `Initial phase A (W) ${initPhases["VABVRMS"].value}V is below minimum ${minPhaseVoltage}V`;
            }
            else if (initPhases["VABVRMS"].value > maxPhaseVoltage) {
                text = `Initial phase A (W) ${initPhases["VABVRMS"].value}V is above maximum ${maxPhaseVoltage}V`;
            }
            if (initPhases["VBCVRMS"].value < minPhaseVoltage) {
                if (text != "") text += `<br>`;
                text = `Initial phase B (V) ${initPhases["VBCVRMS"].value}V is below minimum ${minPhaseVoltage}V`;
            }
            else if (initPhases["VBCVRMS"].value < maxPhaseVoltage) {
                if (text != "") text += `<br>`;
                text = `Initial phase B (V) ${initPhases["VBCVRMS"].value}V is above maximum ${minPhaseVoltage}V`;
            }
            if (initPhases["VCAVRMS"].value < minPhaseVoltage) {
                if (text != "") text += `<br>`;
                text = `Initial phase C (V) ${initPhases["VCAVRMS"].value}V is below minimum ${minPhaseVoltage}V`;
            }
            else if (initPhases["VCAVRMS"].value < maxPhaseVoltage) {
                if (text != "") text += `<br>`;
                text = `Initial phase C (V) ${initPhases["VCAVRMS"].value}V is above maximum ${minPhaseVoltage}V`;
            }
            stageText.innerHTML = text;
            finalResult = "Fail";
        }
        percentage += 100;
        progressBar(canvas, percentage, "");

        return {
            percent: percentage,
            result: finalResult
        };
    }

     async ThrottleTest(canvas, testName)
     {
        /*
        * Check throttle voltage and foot switch at released and when pressed
        * Check brake voltage and brake switch on/off (if exist)
        */
        let finalResult = "Pass";
        let percentage = 0;

        //------------ Check throttle and foot switch ------------//
        await App.NavitasMotorController.JsonCommands({
            Read: {
                "THROTTLEMIN": 0,
                "THROTTLEMAX": 0,
                "THROTTLEFULL": 0,
                "VEHICLELOCKED": 0
            }
        });
        let throttleMin = parseFloat(App.NavitasMotorController.GetParameter("THROTTLEMIN").parameterValue.toFixed(2));
        let throttleMax = parseFloat(App.NavitasMotorController.GetParameter("THROTTLEMAX").parameterValue.toFixed(2));
        let throttleFull = parseFloat(App.NavitasMotorController.GetParameter("THROTTLEFULL").parameterValue.toFixed(2));
        let initialVehicleLocked = App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue;

        //------------ Check initial throttle voltage when throttle is released ------------//
        let stageName = "initial-throttle-state";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check initial throttle voltage and foot switch state"
            }
        });
        let stageText = document.querySelector(`.test-info-text.${stageName}`);
        let stageResult = document.querySelector(`.test-info-result.${stageName}`);
        let minThrottleOffVoltage = 0;
        let maxThrottleOffVoltage = throttleMin;
        let limitFootSwitchOffState = 0;
        let initThrottleState = await this.TestResult(stageResult, ["VTHROTTLEV", "FOOTSW"], undefined, undefined, [minThrottleOffVoltage, limitFootSwitchOffState], [maxThrottleOffVoltage, limitFootSwitchOffState], 3);
        // const [initThrottle, initFootSwitch] = await Promise.all([
        //     this.TestResult(stageResult, "VTHROTTLEV", undefined, undefined, minThrottleOffVoltage, maxThrottleOffVoltage, 3).catch((error) => { return { result: false, value: NaN }; }),
        //     this.TestResult(stageResult, "FOOTSW", undefined, undefined, limitFootSwitchOffState, limitFootSwitchOffState, 3).catch((error) => { return { result: false, value: NaN }; })
        // ]);
        if (initThrottleState["VTHROTTLEV"].result && initThrottleState["FOOTSW"].result) {
            stageText.innerHTML = `Initial off throttle voltage is ${initThrottleState["VTHROTTLEV"].value}V`;
        } else {
            let text = "";
            if (initThrottleState["VTHROTTLEV"].value < minThrottleOffVoltage) {
                text = `Initial off throttle voltage ${initThrottleState["VTHROTTLEV"].value}V is below min off throttle voltage ${minThrottleOffVoltage}V`;
            } else if (initThrottleState["VTHROTTLEV"].value > maxThrottleOffVoltage) {
                text = `Initial throttle voltage ${initThrottleState["VTHROTTLEV"].value}V is above max off throttle voltage ${maxThrottleOffVoltage}V`;
            }
            if (initThrottleState["FOOTSW"].value != limitFootSwitchOffState) {
                if (text != "") text += `<br>`
                text += `Foot switch did not turn off`;
            }
            stageText.innerHTML = text;
            finalResult = "Fail";
            return {
                percent: percentage,
                result: finalResult
            };
        }
        percentage += 25;
        progressBar(canvas, percentage, "");

        //------------ Check if the vehicle is safe for throttle test ------------//
        stageName = "vehicle-safety-state";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check state of vehicle safety parameters"
            }
        });
        stageText = document.querySelector(`.test-info-text.${stageName}`);
        stageResult = document.querySelector(`.test-info-result.${stageName}`);
        this.BuildHTMLTable({
            PopUp: {
                text: "Checking if vehicle is safe for throttle test..."
            }
        });
        this.DisplayPopUp();
        let popupText = document.querySelector(`.popup-text`);
        await App.NavitasMotorController.JsonCommands({
            Read: {
                "VEHICLELOCKED": 0,
                "SWITCHBITS": 0
            }
        });
        let safetyCheckPointCompleted = (App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue == 1) || ((App.NavitasMotorController.GetParameter("SWITCHBITS").parameterValue & 0x6) == 0);
        if (!safetyCheckPointCompleted) {
            popupText.innerHTML = "Please place the vehicle in neutral<br>OR<br>Tap button below to lock the vehicle";
            stageText.innerHTML = "Waiting for vehicle to be in neutral state or locked...";
            this.BuildHTMLTable({
                PopUp: {
                    graph: {
                        id: "#popup-stop"
                    },
                    button: {
                        id: "#popup-lock-button",
                        fn: async () => {
                            await App.NavitasMotorController.JsonCommands({Write: {"VEHICLELOCKED": 1}});
                        }
                    },
                }
            });
            // await Promise.any([
            //     this.TestResult(stageResult, "VEHICLELOCKED", undefined, undefined, 1, 1, 0),
            //     this.TestResult(stageResult, "SWITCHBITS", undefined, 0x6, 0, 0, 0)
            // ]);
            while (!safetyCheckPointCompleted) {
                await App.NavitasMotorController.JsonCommands({
                    Read: {
                        "VEHICLELOCKED": 0,
                        "SWITCHBITS": 0
                    }
                });
                safetyCheckPointCompleted = (App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue == 1) || ((App.NavitasMotorController.GetParameter("SWITCHBITS").parameterValue & 0x6) == 0);
                await Task.Delay(50);  // be nice in this loop
            }
        }
        stageText.innerHTML = "The vehicle is safe for throttle test...";
        progressBar(stageResult, 100, "Pass");
        percentage += percentage;
        progressBar(canvas, percentage, "");

        //------------ Check if foot switch on throttle voltage is within controller parameter ------------//
        //------------ ------------//
        // Ask user to press on throttle and measure min throttle voltage when foot switch turn on
        //  and max throttle voltage when throttle fully pressed
        //------------ ------------//
        stageName = "min-throttle-voltage";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check min throttle voltage when foot switch turn on"
            }
        });
        stageText = document.querySelector(`.test-info-text.${stageName}`);
        stageResult = document.querySelector(`.test-info-result.${stageName}`);
        await App.NavitasMotorController.JsonCommands({  // watching min throttle when foot switch turns on
            Write: {
                "DATASCOPECH1SELECT": "15",
                "DATASCOPETRIGGERLEVEL": 1,
                "DATASCOPETRIGGERMASK": 0xff,
                "DATASCOPETIMEBASE": 1,
                "DATASCOPETRIGGERMODE": 2,
                "DATASCOPETRIGGERADDRESS": "25",
                "DATASCOPEPRECOUNT": 0
            }
        });
        let readyForThrottleTest = false;
        this.BuildHTMLTable({
            PopUp: {
                graph: {
                    id: ["#popup-acceleration", "#popup-voltage-bar"]
                },
                button: {
                    id: "#popup-custom-button",
                    text: "START",
                    fn: () => {
                        readyForThrottleTest = true;
                    }
                },
                text: "The vehicle is safe for throttle test<br>Hit 'START' when you're ready<br>AND<br>Slowly apply throttle to full"
            }
        });
        this.PopUpSkipButton.disabled = false;
        let throttleBar = document.querySelector(".popup-graphs-area #popup-voltage-bar-1")
        let throttleBarMaxValue = throttleBar.querySelector("#progress-bar-max-value");
        throttleBarMaxValue.innerHTML = `${throttleMax}V`;
        let throttleBarCurrentValue = throttleBar.querySelector("#progress-bar-current-value");
        let throttleBarFill = throttleBar.querySelector("#progress-bar-fill");
        let throttleBarPercentage = 0;
        throttleBarFill.style.width = `0%`;
        let throttleBarIntervalId = 0;
        await App.NavitasMotorController.JsonCommands({
            ReadContinuously: {
                "VTHROTTLEV": 0,
                "VEHICLELOCKED": 0,
                "SWITCHBITS": 0
            }
        });
        let updateThrottleBarFunc = async () => {
            let throttleV = parseFloat(App.NavitasMotorController.GetParameter("VTHROTTLEV").parameterValue.toFixed(2));
            throttleBarCurrentValue.innerHTML = `${throttleV}V`;
            throttleBarPercentage = (throttleV / throttleMax) * 100;
            if (throttleBarPercentage > 100) throttleBarPercentage = 100;
            else if (throttleBarPercentage < 0) throttleBarPercentage = 0;
            throttleBarFill.style.width = `${throttleBarPercentage.toFixed(0)}%`;
            throttleBarIntervalId = setTimeout(updateThrottleBarFunc, 0);
        }
        throttleBarIntervalId = setTimeout(updateThrottleBarFunc, 0);
        safetyCheckPointCompleted = (App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue == 1) || ((App.NavitasMotorController.GetParameter("SWITCHBITS").parameterValue & 0x6) == 0);
        while ((!readyForThrottleTest || !safetyCheckPointCompleted) && !this.SkipTest) {
            if (!safetyCheckPointCompleted) {
                popupText.innerHTML = "Please place the vehicle in neutral!";
            } else if (parseFloat(App.NavitasMotorController.GetParameter("VTHROTTLEV").parameterValue.toFixed(2)) > throttleMin) {
                popupText.innerHTML = `Throttle voltage is above ${throttleMin}V<br>Release the throttle OR You might need to calibrate the throttle`;
            } else {
                popupText.innerHTML = "The vehicle is safe for throttle test<br>Hit 'START' when you're ready<br>AND<br>Slowly apply throttle to full";
            }
            safetyCheckPointCompleted = (App.NavitasMotorController.GetParameter("VEHICLELOCKED").parameterValue == 1) || ((App.NavitasMotorController.GetParameter("SWITCHBITS").parameterValue & 0x6) == 0);
            if (!safetyCheckPointCompleted) readyForThrottleTest = false;
            await Task.Delay(50);  // be nice in this loop
        }
        if (!this.SkipTest) {
            await App.NavitasMotorController.JsonCommands({  // start scope
                Write: {
                    "DATASCOPECOMMAND": 1
                }
            });
            // readyForThrottleTestButton.setAttribute('display', 'none');
            popupText.innerHTML = "Slowly apply throttle to full";
            stageText.innerHTML = "Waiting for foot switch to turns on...";
            let scopeTriggered = await this.TestResult(stageResult, ["DATASCOPECOMMAND", "VTHROTTLEV"], 0, undefined, [2, 0], [2, throttleFull], 5);
            scopeTriggered = scopeTriggered["DATASCOPECOMMAND"];
            await App.NavitasMotorController.JsonCommands({ReadScopeDataBlock: {"IQA": 0}}); //confusingly writing to 0x22 is a get scope block
            // await Task.Delay(1000);
            if (scopeTriggered.result) {
                let liveFootSwitchOnVoltageResult = App.NavitasMotorController.Model.ScopeDataArray[0].parameterTimeAndValuePairList[0].value;
                stageText.innerHTML = `Foot switch turns on at ${liveFootSwitchOnVoltageResult}V throttle voltage`;
                popupText.innerHTML = `Foot switch turns on at ${liveFootSwitchOnVoltageResult}V\nKeep applying throttle until full and hold`;
                if (liveFootSwitchOnVoltageResult > 2) {
                    stageText.innerHTML = stageText.innerHTML + `<br>Your foot switch comes on with an unsafe high throttle voltage ( > 2V ) and should be repaired`;
                    await this.TestResult(stageResult, "", liveFootSwitchOnVoltageResult, undefined, 0, 2);
                    finalResult = "Fail";
                } else if (liveFootSwitchOnVoltageResult > 1.5) {
                    stageText.innerHTML = stageText.innerHTML + `<br>Your foot switch comes on with an unusually high throttle voltage ( < 1.5V ) and should be repaired`;
                } else if (liveFootSwitchOnVoltageResult < throttleMin * 0.98 || liveFootSwitchOnVoltageResult > throttleMin * 1.02) {
                    stageText.innerHTML = stageText.innerHTML + `<br>Throttle might need calibration because the voltage is not within 2% of min throttle voltage ${throttleMin}V`;
                }
            } else {
                stageText.innerHTML = "Foot switch did not transition to on";
                finalResult = "Fail";
            }
            percentage += percentage;
            progressBar(canvas, percentage, "");

            stageName = "max-throttle-voltage";
            this.BuildHTMLTable({
                SubTest: {
                    name: testName,
                    stage: stageName,
                    text: "Check max throttle voltage at full throttle"
                }
            });
            stageText = document.querySelector(`.test-info-text.${stageName}`);
            stageResult = document.querySelector(`.test-info-result.${stageName}`);
            popupText.innerHTML = "Keep holding...";
            let minThrottleMaxValue = throttleMax * 0.9;
            let maxThrottleMaxValue = throttleFull;
            let maxThrottle = await this.TestResult(stageResult, "VTHROTTLEV", 0, undefined, minThrottleMaxValue, maxThrottleMaxValue, 5);
            maxThrottle = maxThrottle["VTHROTTLEV"];
            if (maxThrottle.result) {
                stageText.innerHTML = `Full throttle voltage at ${maxThrottle.value}V`;
            } else {
                if (maxThrottle.value < throttleMin) {
                    stageText.innerHTML = `Full throttle voltage ${maxThrottle.value}V is below min on throttle voltage ${throttleMin}V`;
                    finalResult = "Fail";
                } else if (maxThrottle.value < minThrottleMaxValue) {
                    stageText.innerHTML = `Throttle might needs calibration. Full throttle voltage ${maxThrottle.value}V is less than max throttle voltage ${minThrottleMaxValue}V`;
                } else if (maxThrottle.value > maxThrottleMaxValue) {
                    stageText.innerHTML = `Full throttle voltage ${maxThrottle.value}V is greater than throttle fault voltage ${maxThrottleMaxValue}V`;
                    finalResult = "Fail";
                }
            }
            percentage += percentage;
            progressBar(canvas, percentage, "");
            clearInterval(throttleBarIntervalId);
        }
        else {
            finalResult = "";
        }
        await App.NavitasMotorController.JsonCommands({
            Write: {
                "VEHICLELOCKED": initialVehicleLocked
            }
        });
        this.HidePopUp();

        return {
            percent: percentage,
            result: finalResult
        };
    }

    async AnalogBrakeTest(canvas, testName)
    {
        let finalResult = "Pass";
        let percentage = 0;

        //------------ Check analog brake and brake switch (if existed) ------------//
        await App.NavitasMotorController.JsonCommands({
            Read: {
                "Options": 0,
                "BRAKESW": 0,
                "VBRAKEV": 0,
                "BRAKEMIN": 0,
                "BRAKEMAX": 0,
                "BRAKEFULL": 0
            }
        });
        let manufacturingConfigOptions = App.NavitasMotorController.GetParameter("Options").parameterValue;
        let brakeMin = parseFloat(App.NavitasMotorController.GetParameter("BRAKEMIN").parameterValue.toFixed(2));
        let brakeMax = parseFloat(App.NavitasMotorController.GetParameter("BRAKEMAX").parameterValue.toFixed(2));
        let brakeFull = parseFloat(App.NavitasMotorController.GetParameter("BRAKEFULL").parameterValue.toFixed(2));
        let vehicleAnalogBrakeAvailable = {
            "BRAKESW": {
                available: !(manufacturingConfigOptions & 0x4),
                initialValue: App.NavitasMotorController.GetParameter("BRAKESW").parameterValue
            },
            "VBRAKEV": {
                available: !(manufacturingConfigOptions & 0x2),
                initialValue: parseFloat(App.NavitasMotorController.GetParameter("VBRAKEV").parameterValue.toFixed(2))
            }
        }
        this.SkipTest = false;

        //------------ Check initial brake voltage when brake is released ------------//
        let stageName = "initial-brake-state";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check initial brake voltage and brake switch state"
            }
        });
        let stageText = document.querySelector(`.test-info-text.${stageName}`);
        let stageResult = document.querySelector(`.test-info-result.${stageName}`);
        let minBrakeOffVoltage = (vehicleAnalogBrakeAvailable["VBRAKEV"].available) ? 0 : brakeMax;
        let maxBrakeeOffVoltage = (vehicleAnalogBrakeAvailable["VBRAKEV"].available) ? brakeMin : brakeFull;
        let limitBrakeSwitchOffState = 0;
        let initBrakeState = await this.TestResult(stageResult, ["VBRAKEV", "BRAKESW"], undefined, undefined, [minBrakeOffVoltage, limitBrakeSwitchOffState], [maxBrakeeOffVoltage, limitBrakeSwitchOffState], 3);
        // const [initThrottle, initFootSwitch] = await Promise.all([
        //     this.TestResult(stageResult, "VTHROTTLEV", undefined, undefined, minThrottleOffVoltage, maxThrottleOffVoltage, 3).catch((error) => { return { result: false, value: NaN }; }),
        //     this.TestResult(stageResult, "FOOTSW", undefined, undefined, limitFootSwitchOffState, limitFootSwitchOffState, 3).catch((error) => { return { result: false, value: NaN }; })
        // ]);
        if (initBrakeState["VBRAKEV"].result && initBrakeState["BRAKESW"].result) {
            stageText.innerHTML = `Initial off brake voltage is ${initBrakeState["VBRAKEV"].value}V`;
        }
        else {
            let text = "";
            if (initBrakeState["VBRAKEV"].value < minBrakeOffVoltage && vehicleAnalogBrakeAvailable["VBRAKEV"].available) {
                text = `Initial off brake voltage ${initBrakeState["VBRAKEV"].value}V is below min off brake voltage ${minBrakeOffVoltage}V`;
                finalResult = "Fail";
            }
            else if (initBrakeState["VBRAKEV"].value > maxBrakeeOffVoltage && vehicleAnalogBrakeAvailable["VBRAKEV"].available) {
                text = `Initial brake voltage ${initBrakeState["VBRAKEV"].value}V is above max off brake voltage ${maxBrakeeOffVoltage}V`;
                finalResult = "Fail";
            }
            else if (!initBrakeState["VBRAKEV"].result && !vehicleAnalogBrakeAvailable["VBRAKEV"].available) {
                text = `Initial off brake voltage is ${initBrakeState["VBRAKEV"].value}V and is out of range from ${minBrakeOffVoltage}V to ${maxBrakeeOffVoltage}V.
                        But the vehicle doesn't use this input so the result is ignored`;
                progressBar(stageResult, 100, "");
            }
            if (initBrakeState["BRAKESW"].value != limitBrakeSwitchOffState && vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                if (text != "") text += `<br>`
                text += `Brake switch did not turn off`;
                finalResult = "Fail";
            }
            else if (!initBrakeState["BRAKESW"].result && !vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                if (text != "") text += `<br>`
                text += `Brake switch did not turn off. But the vehicle doesn't use this input so the result is ignored`;
                progressBar(stageResult, 100, "");
            }
            stageText.innerHTML = text;
            if (!vehicleAnalogBrakeAvailable["VBRAKEV"].available && !vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                return finalResult;
            }
        }
        percentage += 50;
        progressBar(canvas, percentage, "");

        //------------ Check if brake switch on brake voltage is within controller parameter ------------//
        //------------ ------------//
        // Ask user to press on brake and measure max brake voltage when brake switch turn on
        //------------ ------------//
        stageName = "max-brake-voltage";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check max brake voltage when brake switch turn on"
            }
        });
        stageText = document.querySelector(`.test-info-text.${stageName}`);
        stageResult = document.querySelector(`.test-info-result.${stageName}`);
        let brakeTriggerLevel = brakeMax * App.NavitasMotorController.GetParameter("BRAKEMAX").Scale * 0.9;
        brakeTriggerLevel = parseInt(brakeTriggerLevel.toFixed(0));
        await App.NavitasMotorController.JsonCommands({  // watching min throttle when foot switch turns on
            Write: {
                "DATASCOPECH1SELECT": (vehicleAnalogBrakeAvailable["VBRAKEV"].available) ? "16" : "28",
                "DATASCOPETRIGGERLEVEL": (vehicleAnalogBrakeAvailable["BRAKESW"].available) ? 0 : brakeTriggerLevel,
                "DATASCOPETRIGGERMASK": 0xffff,
                "DATASCOPETIMEBASE": 1,
                "DATASCOPETRIGGERMODE": (vehicleAnalogBrakeAvailable["BRAKESW"].available) ? 2 : 0,
                "DATASCOPETRIGGERADDRESS": (vehicleAnalogBrakeAvailable["BRAKESW"].available) ? "28" : "16",
                "DATASCOPEPRECOUNT": 0
            }
        });
        let readyForBrakeTest = false;
        this.BuildHTMLTable({
            PopUp: {
                graph: {
                    id: ["#popup-acceleration", "#popup-voltage-bar"]
                },
                button: {
                    id: "#popup-custom-button",
                    text: "START",
                    fn: () => { readyForBrakeTest = true; }
                },
                text: "The vehicle is safe for brake test<br>Hit 'START' when you're ready<br>AND<br>Slowly apply brake to full"
            }
        });
        let popupText = document.querySelector(`.popup-text`);
        this.PopUpSkipButton.disabled = false;
        this.DisplayPopUp();
        let brakeBar = document.querySelector(".popup-graphs-area #popup-voltage-bar-1")
        let brakeBarMaxValue = brakeBar.querySelector("#progress-bar-max-value");
        brakeBarMaxValue.innerHTML = `${brakeMax.toFixed(2)}V`;
        let brakeBarCurrentValue = brakeBar.querySelector("#progress-bar-current-value");
        let brakeBarFill = brakeBar.querySelector("#progress-bar-fill");
        let brakeBarPercentage = 0;
        brakeBarFill.style.width = `0%`;
        let brakeBarIntervalId = 0;
        await App.NavitasMotorController.JsonCommands({
            ReadContinuously: {
                "VBRAKEV": 0,
                "BRAKESW": 0
            }
        });
        let updateBrakeBarFunc = async () => {
            let brakeV = parseFloat(App.NavitasMotorController.GetParameter("VBRAKEV").parameterValue.toFixed(2));
            brakeBarCurrentValue.innerHTML = `${brakeV}V`;
            brakeBarPercentage = (brakeV / brakeMax) * 100;
            if (brakeBarPercentage > 100 || (App.NavitasMotorController.GetParameter("BRAKESW").parameterValue == 0 && vehicleAnalogBrakeAvailable["BRAKESW"].available && !vehicleAnalogBrakeAvailable["VBRAKEV"].available)) brakeBarPercentage = 100;
            else if (brakeBarPercentage < 0) brakeBarPercentage = 0;
            brakeBarFill.style.width = `${brakeBarPercentage.toFixed(0)}%`;
            brakeBarIntervalId = setTimeout(updateBrakeBarFunc, 0);
        }
        brakeBarIntervalId = setTimeout(updateBrakeBarFunc, 0);
        while (!readyForBrakeTest && !this.SkipTest) {
            if (parseFloat(App.NavitasMotorController.GetParameter("VBRAKEV").parameterValue.toFixed(2)) > brakeMin && vehicleAnalogBrakeAvailable["VBRAKEV"].available) {
                popupText.innerHTML = `Brake voltage is above ${brakeMin}V<br>Release the brake`;
            }
            else if (App.NavitasMotorController.GetParameter("BRAKESW").parameterValue == 0 && vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                popupText.innerHTML = `Brake switch is on<br>Release the brake`;
            }
            else {
                popupText.innerHTML = "The vehicle is safe for brake test<br>Hit 'START' when you're ready<br>AND<br>Slowly apply brake to full";
            }
            await Task.Delay(50);  // be nice in this loop
        }
        if (!this.SkipTest) {
            await App.NavitasMotorController.JsonCommands({  // start scope
                Write: {
                    "DATASCOPECOMMAND": 1
                }
            });
            // readyForThrottleTestButton.setAttribute('display', 'none');
            popupText.innerHTML = "Slowly apply brake to full";
            stageText.innerHTML = (vehicleAnalogBrakeAvailable["BRAKESW"].available) ? "Waiting for brake switch to turns on..." : "Waiting for brake to be fully pressed...";
            let brakeSwitchLimit = (vehicleAnalogBrakeAvailable["BRAKESW"].available) ? 0 : 1;
            let scopeTriggered = await this.TestResult(stageResult, ["DATASCOPECOMMAND", "VBRAKEV", "BRAKESW"], 0, undefined, [2, brakeMin, brakeSwitchLimit], [2, brakeFull, brakeSwitchLimit], 10);
            scopeTriggered = scopeTriggered["DATASCOPECOMMAND"];
            await App.NavitasMotorController.JsonCommands({ ReadScopeDataBlock: { "IQA": 0 } }); //confusingly writing to 0x22 is a get scope block
            // await Task.Delay(1000);
            if (scopeTriggered.result) {
                let liveBrakeOnVoltageResult = App.NavitasMotorController.Model.ScopeDataArray[0].parameterTimeAndValuePairList[0].value;
                if (vehicleAnalogBrakeAvailable["VBRAKEV"].available && vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                    stageText.innerHTML = `Brake switch turns on at ${liveBrakeOnVoltageResult}V brake voltage`;
                    if (liveBrakeOnVoltageResult > brakeFull) {
                        stageText.innerHTML = stageText.innerHTML + `<br>Your brake switch comes on with an unsafe high brake voltage ( > ${brakeFull}V ) and should be repaired`;
                        finalResult = "Fail";
                    }
                    else if (liveBrakeOnVoltageResult < brakeMin) {
                        stageText.innerHTML = stageText.innerHTML + `<br>Your brake switch comes on with an unusually low brake voltage ( < ${brakeMin}V ) and should be repaired`;
                        finalResult = "Fail";
                    }
                    await this.TestResult(stageResult, "", liveBrakeOnVoltageResult, undefined, brakeMin, brakeFull);
                }
                else if (vehicleAnalogBrakeAvailable["VBRAKEV"].available && !vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                    stageText.innerHTML = `Checking max brake voltage...`;
                    popupText.innerHTML = `Keep holding...`;
                    await Task.Delay(5000);
                    await App.NavitasMotorController.JsonCommands({ Read: { "VBRAKEV": 0 } });
                    let brakeV = parseFloat(App.NavitasMotorController.GetParameter("VBRAKEV").parameterValue.toFixed(2));
                    if (brakeV > brakeFull) {
                        stageText.innerHTML = `Max brake voltage is above ${brakeFull}V and should be repaired`;
                        finalResult = "Fail";
                    }
                    else {
                        stageText.innerHTML = `Max brake voltage is ${brakeV}V`;
                    }
                    await this.TestResult(stageResult, "", brakeV, undefined, brakeMin, brakeFull);
                }
                else if (!vehicleAnalogBrakeAvailable["VBRAKEV"].available && vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                    stageText.innerHTML = `Brake switch did turned on when fully pressed`;
                }
            }
            else {
                if ((vehicleAnalogBrakeAvailable["VBRAKEV"].available && vehicleAnalogBrakeAvailable["BRAKESW"].available) || vehicleAnalogBrakeAvailable["BRAKESW"].available) {
                    stageText.innerHTML = "Brake switch did not transition to on";
                }
                else {
                    stageText.innerHTML = `Brake voltage did not go above ${(brakeMax * 0.9).toFixed(2)}V when pressed and should be repaired`;
                }
                finalResult = "Fail";
            }
            percentage += percentage;
            progressBar(canvas, percentage, finalResult);
            clearInterval(brakeBarIntervalId);
        }
        else {
            finalResult = "";
        }
        this.SkipTest = false;
        this.HidePopUp();

        return {
            percent: percentage,
            result: finalResult
        };

    }

    async SwitchesTest(canvas, testName)
    {
        /*
        * Check vehicle switches
        */
        let finalResult = "";
        let percentage = 0;

        let inputsToBeTested = {
                "KEYIN": 0,
                "CHARGERINTERLOCKIN": 0,
                "FORWARDSW": 0,
                "REVERSESW": 0
        }
        await App.NavitasMotorController.JsonCommands({
            Read: inputsToBeTested
        });
        let vehicleSwitchesAvailable = {};
        Object.keys(inputsToBeTested).forEach((input) => {
            vehicleSwitchesAvailable[input] = {
                available: true,
                initialValue: App.NavitasMotorController.GetParameter(input).parameterValue
            }
        });
        //------------ Check if switches state change when user switch the input ------------//
        let switchFail = false;
        this.BuildHTMLTable({
            PopUp: {
                graph: {
                    id: "#popup-vehicle-switches"
                },
                text: "Cycling the switch above to verify its function<br>OR<br>Press the FAIL button below if the switch did not change",
                button: {
                    id: "#popup-custom-button",
                    text: "FAIL",
                    fn: () => { switchFail = true; }
                }
            }
        });
        this.PopUpSkipButton.disabled = false;
        this.DisplayPopUp();
        let popupVehicleSwitches = document.querySelector(`.popup-graphs-area > #popup-vehicle-switches`);
        Object.keys(vehicleSwitchesAvailable).forEach((key) => {
            let sw = popupVehicleSwitches.querySelector(`.button-cover`);
            popupVehicleSwitches.appendChild(sw.cloneNode(true));
            sw = popupVehicleSwitches.querySelector(`.button-cover:last-child`);
            sw.id = key;
            sw.style.setProperty('--switch-content', `"${App.NavitasMotorController.GetParameter(key).Name}"`);
            sw.querySelector(`.button-image`).src = `./images/${key}.svg`;
            if (vehicleSwitchesAvailable[key].initialValue == 1) {
                sw.querySelector(`.vehicle-switch > .checkbox`).checked = true;
            }
            else if (vehicleSwitchesAvailable[key].initialValue == 0) {
                sw.querySelector(`.vehicle-switch > .checkbox`).checked = false;
            }
            sw.style.display = "none";
        });
        let noOfSwitchesAvailable = Object.values(vehicleSwitchesAvailable).filter(({available}) => available == true).length;
        let noOfSwitchesTested = 0;
        let prevSwitchState = 0;
        let currentSwitchState = 0;
        let noOfSwitchChanges = 0;
        let result = "";
        for (const key of Object.keys(vehicleSwitchesAvailable)) {
            if (vehicleSwitchesAvailable[key].available) {
                let sw = document.querySelector(`.popup-graphs-area > #popup-vehicle-switches > #${key}`);
                sw.style.display = "block";
                // sw.scrollIntoView({behavior: "smooth", block: "center", inline: "center"});
                await App.NavitasMotorController.JsonCommands({
                    ReadContinuously: {
                        [key]: 0
                    }
                });
                switchFail = false;
                prevSwitchState = App.NavitasMotorController.GetParameter(key).parameterValue;
                currentSwitchState = App.NavitasMotorController.GetParameter(key).parameterValue;
                noOfSwitchChanges = 0;
                while (!switchFail && !this.SkipTest) {
                    if (currentSwitchState == 1 && currentSwitchState != prevSwitchState) {
                        sw.querySelector(`.vehicle-switch > .checkbox`).checked = true;
                        prevSwitchState = currentSwitchState;
                        noOfSwitchChanges++;
                    }
                    else if (currentSwitchState == 0 && currentSwitchState != prevSwitchState) {
                        sw.querySelector(`.vehicle-switch > .checkbox`).checked = false;
                        prevSwitchState = currentSwitchState;
                        noOfSwitchChanges++;
                    }
                    currentSwitchState = App.NavitasMotorController.GetParameter(key).parameterValue;
                    if (noOfSwitchChanges > 1) break;
                    await Task.Delay(50);
                }
                if (!this.SkipTest) {
                    noOfSwitchesTested++;
                    let stageName = `${key}-switching-state`;
                    this.BuildHTMLTable({
                        SubTest: {
                            name: testName,
                            stage: stageName,
                            text: `Checking ${App.NavitasMotorController.GetParameter(key).PropertyName}...`
                        }
                    });
                    let stageText = document.querySelector(`.test-info-text.${stageName}`);
                    let stageResult = document.querySelector(`.test-info-result.${stageName}`);
                    if (switchFail) {
                        stageText.innerHTML = `${App.NavitasMotorController.GetParameter(key).Name} failed to switch`;
                        sw.style.background = "#7c7c7c";
                        result += "Fail";
                    }
                    else {
                        stageText.innerHTML = `${App.NavitasMotorController.GetParameter(key).Name} passed switch test`;
                        result += "Pass";
                    }
                    await this.TestResult(stageResult, "", noOfSwitchChanges, undefined, 2, 10, 0);
                }
                this.SkipTest = false;
                sw.style.display = "none";
                percentage = (noOfSwitchesTested / noOfSwitchesAvailable) * 100;
                progressBar(canvas, percentage, "");
            }
        }
        this.HidePopUp();
        await App.NavitasMotorController.JsonCommands({  // stop continuous read
            Read: {
                "Options": 0
            }
        });
        if (result.includes("Fail")) finalResult = "Fail";
        else if (result != "") finalResult = "Pass";

        return {
            percent: percentage,
            result: finalResult
        };
    }

     async BrakeLightTest(canvas, testName)
     {
        /*
        * Check brake light on/off
        */
        let finalResult = "";
        let percentage = 0;

        await App.NavitasMotorController.JsonCommands({
            Read: {
                "Options": 0
            }
        });
        let initialOptionsValue = App.NavitasMotorController.GetParameter("Options").parameterValue;

        //------------ Check brake light on ------------//
        let stageName = "brake-light-on";
        this.BuildHTMLTable({
            SubTest: {
                name: testName,
                stage: stageName,
                text: "Check brake light on"
            }
        });
        let stageText = document.querySelector(`.test-info-text.${stageName}`);
        let stageResult = document.querySelector(`.test-info-result.${stageName}`);
        await App.NavitasMotorController.JsonCommands({
            Write: {
                "Options": 0x10,
                "SetOutputState": 0
            }
        });
        let lightOn = false;
        this.BuildHTMLTable({
            PopUp: {
                graph: {
                    id: "#BRAKERELAYOUTPUT_OFF"
                },
                text: "Press the button below to turn on brake light",
                button: {
                    id: "#popup-custom-button",
                    text: "LIGHT ON",
                    fn: () => { lightOn = true; }
                }
            }
        });
        this.PopUpSkipButton.disabled = false;
        this.DisplayPopUp();
        while (!lightOn && !this.SkipTest) {
            await Task.Delay(50);
        }
        if (!this.SkipTest) {
            this.PopUpSkipButton.disabled = true;
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "SetOutputState": 0x4
                }
            });
            let answer = -1;
            this.BuildHTMLTable({
                PopUp: {
                    graph: {
                        id: "#BRAKERELAYOUTPUT_ON"
                    },
                    text: "Do you see the brake light on?",
                    button: {
                        id: ["#popup-custom-button", "#popup-custom-button"],
                        text: ["YES", "NO"],
                        fn: [() => { answer = 1; }, () => { answer = 0; }]
                    }
                }
            });
            while (answer == -1) {
                await Task.Delay(50);
            }
            let result = await this.TestResult(stageResult, "", answer, undefined, 1, 1, 0);
            percentage += 50;
            progressBar(canvas, percentage, "");
            if (result.result) {
                stageText.innerHTML = `Brake Light turns on`;
                finalResult += "Pass";
            }
            else {
                stageText.innerHTML = `Brake Light did not turn on`;
                finalResult += "Fail";
            }

            //------------ Check brake light off ------------//
            stageName = "brake-light-off";
            this.BuildHTMLTable({
                SubTest: {
                    name: testName,
                    stage: stageName,
                    text: "Check brake light off"
                }
            });
            stageText = document.querySelector(`.test-info-text.${stageName}`);
            stageResult = document.querySelector(`.test-info-result.${stageName}`);
            await App.NavitasMotorController.JsonCommands({
                Write: {
                    "SetOutputState": 0
                }
            });
            answer = -1;
            this.BuildHTMLTable({
                PopUp: {
                    graph: {
                        id: "#BRAKERELAYOUTPUT_OFF"
                    },
                    text: "Do you still see the brake light on?",
                    button: {
                        id: ["#popup-custom-button", "#popup-custom-button"],
                        text: ["YES", "NO"],
                        fn: [() => { answer = 1; }, () => { answer = 0; }]
                    }
                }
            });
            while (answer == -1) {
                await Task.Delay(50);
            }
            result = await this.TestResult(stageResult, "", answer, undefined, 0, 0, 0);
            percentage += percentage;
            progressBar(canvas, percentage, "");
            if (result.result) {
                stageText.innerHTML = `Brake Light turns off`;
                finalResult += "Pass";
            }
            else {
                stageText.innerHTML = `Brake Light did not turn off`;
                finalResult += "Fail";
            }
        }
        this.SkipTest = false;
        this.HidePopUp();

        if (finalResult.includes("Fail")) finalResult = "Fail";
        else if (finalResult != "") finalResult = "Pass";
        await App.NavitasMotorController.JsonCommands({
            Write: {
                "Options": initialOptionsValue
            }
        });

        return {
            percent: percentage,
            result: finalResult
        };
    }
}