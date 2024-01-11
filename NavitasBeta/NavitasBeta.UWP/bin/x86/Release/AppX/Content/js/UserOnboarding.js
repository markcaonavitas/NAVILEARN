const outputWindow = document.querySelector('#MyConsoleLogOutputArea');

if (navigator.userAgent.match(/iPhone|iPad/i))
{
    document.body.style.fontSize = "3rem";
}
else if (navigator.userAgent.match(/Android/i)) {
    document.body.style.fontSize = "1rem";
}

if (typeof HasBrowserCommandInterface != 'undefined') {
    App = { //just so it looks like the c# NavitasBeta App.
        NavitasMotorController: new NavitasMotorControllerModel(new CommunicationsInterface(new NavitasModbus())),
    }
}
else {
    App = { //just so it looks like the c# NavitasBeta App.
        NavitasMotorController: new NavitasMotorControllerModel(new CommunicationsInterface({})),
    }
}

window.addEventListener("unload", async function ()
{
    App.NavitasMotorController.Communications.bEnableCommunicationTransmissions = false;
    console.log("Trying to Close ");
    if (typeof HasBrowserCommandInterface != 'undefined') App.NavitasMotorController.Communications.DeviceSocket.websocket.close();
});

// Without jQuery
// Define a convenience method and use it
let ready = (callback) => {
    //screen.orientation.addEventListener("change", callback);
    if (document.readyState != "loading") callback();
    else document.addEventListener("DOMContentLoaded", callback);
}
ready(async () => {
    console.log("DOM loaded");
    await WaitForPhoneAppToStartThis(); //uncomment to run App
});

let setupWizard = {
    index: 0,
    nextScreen: function () {
        if (this.index < this.indexMax()) {
            this.index += 1;
            console.log("nextScreen - this.index: " + this.index);
            this.updateScreen();
        }
    },
    prevScreen: function() {
        if (this.index > 0) {
            this.index -= 1;
            console.log("prevScreen - this.index: " + this.index);
            this.updateScreen();
        }
    },
    updateScreen: function() {
        this.reset();
        this.goTo(this.index);
        this.setBtns();
    },
    setBtns: function() {
        if (this.index === this.indexMax()) {
            document.querySelector('.next-screen').setAttribute('disabled', '');
            document.querySelector('.prev-screen').removeAttribute('disabled');
        } else if (this.index === 0) {
            document.querySelector('.next-screen').removeAttribute('disabled');
            document.querySelector('.prev-screen').setAttribute('disabled', '');
        } else {
            document.querySelector('.next-screen').removeAttribute('disabled');
            document.querySelector('.prev-screen').removeAttribute('disabled');
        }
        console.log("setBtns - this.index: " + this.index);
    },
    goTo: function(index) {
        console.log("goTo - index: " + index);
        $('.screen').eq(index).addClass('active');
        $('.dot').eq(index).addClass('active');
    },
    reset: function() {
        $('.screen, .dot').removeClass('active');
    },
    indexMax: function() {
        return $('.screen').length - 1;
    },
    closeModal: function() {
        document.querySelector('.walkthrough').classList.remove('reveal', 'show');
        this.index = 0;
        this.updateScreen();
    },
    openModal: function() {
        document.querySelector('.walkthrough').classList.add('show', 'reveal');
        this.updateScreen();
    }
}

let uiWalkthrough = {
    index: 0,
    highlightFn: null,
    maxStep: 4,
    nextScreen: function () {
        if (this.index < this.indexMax()) {
            this.index++;
            this.updateScreen();
        }
    },
    prevScreen: function() {
        if (this.index > 0) {
            this.index--;
            this.updateScreen();
        }
    },
    updateScreen: function() {
        this.reset();
        this.goTo(this.index);
        this.setBtns();
    },
    setBtns: function() {
        if (this.index === this.indexMax()) {
            document.querySelector('.page-next').setAttribute('disabled', '');
            document.querySelector('.page-previous').removeAttribute('disabled');
        } else if (this.index === 0) {
            document.querySelector('.page-next').removeAttribute('disabled');
            document.querySelector('.page-previous').setAttribute('disabled', '');
        } else {
            document.querySelector('.page-next').removeAttribute('disabled');
            document.querySelector('.page-previous').removeAttribute('disabled');
        }
    },
    goTo: function(index) {
        if (index === 0) {
            // focus on 'hamburger menu'
            document.querySelector(".highlight-top").style.flex = 'none';
            document.querySelector(".highlight-top").style.height = 0;
            document.querySelector(".highlight-left").style.flex = 'none';
            document.querySelector(".highlight-left").style.width = 0;
            document.querySelector(".highlight").style.width = '10%';
            document.querySelector(".highlight").style.height = '80px';

            // write description
            document.querySelector(".page-description").innerHTML = 'Click here to show the menu options';
        }
        else if (index === 1) {
            // focus on 'diagnostics'
            document.querySelector(".highlight-bottom").style.flex = 'none';
            document.querySelector(".highlight-bottom").style.height = 0;
            document.querySelector(".highlight").style.width = '20%';
            document.querySelector(".highlight").style.height = '110px';

            // add click event to highlight
            document.querySelector('.highlight').removeEventListener('click', this.highlightFn);
            this.highlightFn = () => {
                App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 1 } });
                this.nextScreen();
            }
            document.querySelector('.highlight').addEventListener('click', this.highlightFn);

            // write description
            document.querySelector(".page-description").innerHTML = 'Click here to go to diagnostics tab';
        }
        else if (index === 2) {
            // focus on 'settings'
            document.querySelector(".highlight-bottom").style.flex = 'none';
            document.querySelector(".highlight-bottom").style.height = 0;
            document.querySelector(".highlight-right").style.flex = 'none';
            document.querySelector(".highlight-right").style.width = '6%';
            document.querySelector(".highlight").style.width = '20%';
            document.querySelector(".highlight").style.height = '110px';

            // add click event to highlight
            document.querySelector('.highlight').removeEventListener('click', this.highlightFn);
            this.highlightFn = () => {
                App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 2 } });
                this.nextScreen();
            }
            document.querySelector('.highlight').addEventListener('click', this.highlightFn);

            // write description
            document.querySelector(".page-description").innerHTML = 'Click here to go to settings tab';
        }
        else if (index === 3) {
            // focus on 'Forward Speed Limit (MPH)' entry
            document.querySelector(".highlight-top").style.flex = 'none';
            document.querySelector(".highlight-top").style.height = '14%';
            document.querySelector(".highlight-right").style.flex = 'none';
            document.querySelector(".highlight-right").style.width = 0;
            document.querySelector(".highlight").style.width = '30%';
            document.querySelector(".highlight").style.height = '30px';

            // add click event to highlight
            document.querySelector('.highlight').removeEventListener('click', this.highlightFn);
            this.highlightFn = () => {
                App.NavitasMotorController.JsonCommands({ FocusOnElement: { 'PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH': 0 } });
            }
            document.querySelector('.highlight').addEventListener('click', this.highlightFn);

            // write description
            document.querySelector(".page-description").innerHTML = 'Click here to change max forward speed limit (MPH)';
        }
    },
    reset: function() {
        document.querySelector(".highlight-top").style.removeProperty('flex');
        document.querySelector(".highlight-top").style.removeProperty('height');
        document.querySelector(".highlight-bottom").style.removeProperty('flex');
        document.querySelector(".highlight-bottom").style.removeProperty('height');
        document.querySelector(".highlight-left").style.removeProperty('flex');
        document.querySelector(".highlight-left").style.removeProperty('width');
        document.querySelector(".highlight-right").style.removeProperty('flex');
        document.querySelector(".highlight-right").style.removeProperty('width');
        document.querySelector(".highlight").style.removeProperty('width');
        document.querySelector(".highlight").style.removeProperty('height');
    },
    indexMax: function() {
        return this.maxStep;
    },
    closeModal: function() {
        return setTimeout((() => {
            this.index = 0;
            this.updateScreen();
        }), 200);
    },
    openModal: function() {
        this.updateScreen();
    }
}

document.querySelector('.button.walkthrough-ui').addEventListener('click', () => {
    App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 0 } });  // switch to 'Dashboard'
    document.querySelector(".walkthrough-popup").style.display = 'none';  // hide walkthrough popup
    // display UI walkthrough
    document.querySelector(".ui-walkthrough").style.display = 'flex';
    document.querySelector(".page").style.display = 'flex';
    uiWalkthrough.openModal();
});
document.querySelector('.page-next').addEventListener('click', () => {
    uiWalkthrough.nextScreen();
});
document.querySelector('.page-previous').addEventListener('click', () => {
    uiWalkthrough.prevScreen();
});
document.querySelector('.page-close').addEventListener('click', () => {
    uiWalkthrough.closeModal();
    document.querySelector(".walkthrough-popup").style.display = 'flex';
    document.querySelector(".ui-walkthrough").style.display = 'none';
    document.querySelector(".page").style.display = 'none';
});


document.querySelector('.button.walkthrough-setup').addEventListener('click', () => {
    setupWizard.openModal();
});
document.querySelector('.next-screen').addEventListener('click', () => {
    setupWizard.nextScreen();
});
document.querySelector('.prev-screen').addEventListener('click', () => {
    setupWizard.prevScreen();
});
document.querySelector('.hide-screen').addEventListener('click', () => {
    setupWizard.closeModal();
});

document.querySelector('.button.walkthrough-close').addEventListener('click', () => {
    App.NavitasMotorController.JsonCommands({ Close: null });
});
document.querySelector('.button.troubleshooting-stationary').addEventListener('click', () => {

});
document.querySelector('.button.troubleshooting-driving').addEventListener('click', () => {

});

let SimulationCheckTimeout = 80; //a global var because static var is not a thing
async function WaitForPhoneAppToStartThis() {
    //if this is running in the actual Phone App then the App will inject a function called invokeCSharpAction()
    //so give it a couple seconds to do that (should only take 100ms)
    //SimulationCheckTimeout = 0;

    // $('.dashboard-tab').click(function() {
    //     App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 0 } });
    //     document.querySelectorAll(".page").forEach(element => {
    //         element.style.display = "none";
    //     });
    //     document.querySelector(".page.dashboard").style.display = 'flex';
    // });
    // $('.diagnostics-tab').click(function() {
    //     App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 1 } });
    //     document.querySelectorAll(".page").forEach(element => {
    //         element.style.display = "none";
    //     });
    //     document.querySelector(".page.diagnostics").style.display = 'flex';
    // });
    // $('.settings-tab').click(function() {
    //     App.NavitasMotorController.JsonCommands({ ChangeCurrentPage: { 'pageIndex': 2 } });
    //     document.querySelectorAll(".page").forEach(element => {
    //         element.style.display = "none";
    //     });
    //     document.querySelector(".page.settings").style.display = 'flex';
    // });
    // $('.tire-size').click(function() {
    //     App.NavitasMotorController.JsonCommands({ FocusOnElement: { 'TIREDIAMETER': 0 } });
    //     // App.NavitasMotorController.JsonCommands({ Write: { 'SAVEPARAMS': 1 } });
    // });
    if (typeof HasBrowserCommandInterface != 'undefined') { //This must be running from a browser
        //RG Nov 27, Something has to be done here if simulating
        invokeSimulatedActionIsEnabled = false;
        console.log("Browser detected");
        App.NavitasMotorController.Communications.protocal.bEnableCommunicationTransmissions = true;
        await App.NavitasMotorController.Communications.BeginThread();
        while (App.NavitasMotorController.ModelType == "")// || App.RegressionTestBoxModelA.ModelType == "" || App.firmwareDownload.AlreadyTalkingToBootloader == true) //wait for communications to start
            await Task.Delay(500);
        App.NavitasMotorController.CommandHandler = new BrowserCommandInterface(App.NavitasMotorController);
        // TestStateMachine();
    }
    // else if (typeof invokeCSharpAction == 'function') { //This must be running from the Phone App
    else if (typeof invokeCSharpAction == 'function') {
        invokeSimulatedActionIsEnabled = false;
        console.log("Phone app detected");
        await App.NavitasMotorController.JsonCommands({ GetAppParameters: {} }); //will update things like App.NavitasMotorController.ModelType
    }
    else {
        if (SimulationCheckTimeout == 0) {
            invokeSimulatedActionIsEnabled = true;
            console.log("Simulation detected");
            App.NavitasMotorController.Model = "TAC";
            App.SimulatedMotorController = new NavitasMotorControllerModel(new CommunicationsInterface({}));
            App.SimulatedMotorController.Model = "TAC";
            App.NavitasMotorController.CommandHandler = new SimulatedCommandInterface(App.NavitasMotorController, App.SimulatedMotorController);
            // InitializeSimulatorModel();
            // setTimeout(() => App.NavitasMotorController.CommandHandler.StartNavitasControllerSimulator(10, () => { DiagnosticsThrottleCalibrationSimulator() }), 101); //to run your specific Unit Test simulator
            // TestStateMachine();
        }
        else {
            SimulationCheckTimeout -= 1;
            setTimeout(WaitForPhoneAppToStartThis, 100);
        }
    }
}

// (function() {
//     $(document).ready(function() {
//         var walkthrough;
//         walkthrough = {
//             index: 0,
//             nextScreen: function() {
//                 if (this.index < this.indexMax()) {
//                     this.index++;
//                     return this.updateScreen();
//                 }
//             },
//             prevScreen: function() {
//                 if (this.index > 0) {
//                     this.index--;
//                     return this.updateScreen();
//                 }
//             },
//             updateScreen: function() {
//                 this.reset();
//                 this.goTo(this.index);
//                 return this.setBtns();
//             },
//             setBtns: function() {
//                 var $lastBtn, $nextBtn, $prevBtn;
//                 $nextBtn = $('.next-screen');
//                 $prevBtn = $('.prev-screen');
//                 $lastBtn = $('.hide-screen');
//                 if (walkthrough.index === walkthrough.indexMax()) {
//                     $nextBtn.prop('disabled', true);
//                     $prevBtn.prop('disabled', false);
//                     // return $lastBtn.addClass('active').prop('disabled', false);
//                 } else if (walkthrough.index === 0) {
//                     $nextBtn.prop('disabled', false);
//                     $prevBtn.prop('disabled', true);
//                     // return $lastBtn.removeClass('active').prop('disabled', true);
//                 } else {
//                     $nextBtn.prop('disabled', false);
//                     $prevBtn.prop('disabled', false);
//                     // return $lastBtn.removeClass('active').prop('disabled', true);
//                 }
//             },
//             goTo: function(index) {
//                 $('.screen').eq(index).addClass('active');
//                 return $('.dot').eq(index).addClass('active');
//             },
//             reset: function() {
//                 return $('.screen, .dot').removeClass('active');
//             },
//             indexMax: function() {
//                 return $('.screen').length - 1;
//             },
//             closeModal: function() {
//                 $('.walkthrough, .shade').removeClass('reveal');
//                 return setTimeout((() => {
//                     $('.walkthrough, .shade').removeClass('show');
//                     this.index = 0;
//                     return this.updateScreen();
//                 }), 200);
//             },
//             openModal: function() {
//                 $('.walkthrough, .shade').addClass('show');
//                 setTimeout((() => {
//                     return $('.walkthrough, .shade').addClass('reveal');
//                 }), 200);
//                 return this.updateScreen();
//             }
//         };
//         $('.next-screen').click(function() {
//             return walkthrough.nextScreen();
//         });
//         $('.prev-screen').click(function() {
//             return walkthrough.prevScreen();
//         });
//         $('.hide-screen').click(function() {
//             return walkthrough.closeModal();
//         });
//         $('.button.walkthrough-open').click(function() {
//             return walkthrough.openModal();
//         });
//         $('.button.walkthrough-close').click(function() {
//             App.NavitasMotorController.JsonCommands({ Close: null });
//         });
//         // walkthrough.openModal();
//
//         // Optionally use arrow keys to navigate walkthrough
//         return $(document).keydown(function(e) {
//             switch (e.which) {
//                 case 37:
//                     // left
//                     walkthrough.prevScreen();
//                     break;
//                 case 38:
//                     // up
//                     walkthrough.openModal();
//                     break;
//                 case 39:
//                     // right
//                     walkthrough.nextScreen();
//                     break;
//                 case 40:
//                     // down
//                     walkthrough.closeModal();
//                     break;
//                 default:
//                     return;
//             }
//             e.preventDefault();
//         });
//     });
// }).call(this);