using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gauge : NavitasGeneralPage //, INotifyPropertyChanged  put this back if you dont' succeed moving the context. binding context was not seen for custom image user tabbed view. 
    {
        System.Threading.CancellationTokenSource cancellationTokenSource;
        FormattedString formattedString;
        string oldStateString = null;
        public bool bBlockTimeout;
        // For new error flashing 

        public Gauge()
        {
            try
            {
                App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue = DisplaySettings.GetStandardorMetric() ? 1 : 0;
                InitializeComponent();
                Boolean ExternalDashboardFound = false;
                string[] fileList = FileManager.GetNavitasDirectoryFiles();
                foreach (string filePath in fileList)
                {
                    if(filePath.Contains("DashboardScripting.html"))
                    {
                        ExternalDashboardFound = true;
                        var a = this;
                        VisualElement b = this.FindByName<VisualElement>("syncFusionElement1");
                        this.grid_gauge.Children.Remove(MTEMPGaugeC);
                        this.grid_gauge.Children.Remove(MTEMPGaugeF);
                        this.grid_gauge.Children.Remove(VBATGauge);
                        this.grid_gauge.Children.Remove(ROTORRPM);
                        this.grid_gauge.Children.Remove(NEUTRAL);
                        this.grid_gauge.Children.Remove(FORWARD);
                        this.grid_gauge.Children.Remove(REVERSE);
                        this.grid_gauge.Children.Remove(KEYENABLE);
                        this.grid_gauge.BackgroundColor = Color.Transparent;
                        hybridWebView = LoadHTMLPage(filePath);
                        hybridWebView.BackgroundColor = Color.Black;
                        hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
                        hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;
                        AbsoluteLayout.SetLayoutBounds(hybridWebView, new Xamarin.Forms.Rectangle(0, 0, 1, 1));
                        AbsoluteLayout.SetLayoutFlags(hybridWebView, AbsoluteLayoutFlags.All);
                        this.AbsoluteLayout.Children.Insert(0, hybridWebView);
                        break;
                    }

                }
                if (!ExternalDashboardFound)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.UWP:
                            var a = this;
                            VisualElement b = this.FindByName<VisualElement>("syncFusionElement1");
                            this.grid_gauge.Children.Remove(MTEMPGaugeC);
                            this.grid_gauge.Children.Remove(MTEMPGaugeF);
                            this.grid_gauge.Children.Remove(VBATGauge);
                            this.grid_gauge.Children.Remove(ROTORRPM);
                            this.grid_gauge.Children.Remove(NEUTRAL);
                            this.grid_gauge.Children.Remove(FORWARD);
                            this.grid_gauge.Children.Remove(REVERSE);
                            this.grid_gauge.Children.Remove(KEYENABLE);
                            this.grid_gauge.BackgroundColor = Color.Transparent;
                            hybridWebView = LoadHTMLPage("DashboardScripting.html");
                            hybridWebView.BackgroundColor = Color.Black;
                            hybridWebView.HorizontalOptions = LayoutOptions.FillAndExpand;
                            hybridWebView.VerticalOptions = LayoutOptions.FillAndExpand;
                            AbsoluteLayout.SetLayoutBounds(hybridWebView, new Xamarin.Forms.Rectangle(0, 0, 1, 1));
                            AbsoluteLayout.SetLayoutFlags(hybridWebView, AbsoluteLayoutFlags.All);
                            this.AbsoluteLayout.Children.Insert(0, hybridWebView);
                            break;
                        case Device.iOS:
                        case Device.Android:
                        case Device.macOS:
                        default:
                            break;
                    }
                }
                LoadCommunicationItemsTAC();
                AddActivityPopUp();
                BuildErrorIcon();
                BuildDemoFlashing();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: GaugePage.xaml.cs" + ex.Message);
            }
            BindingContext = App.ViewModelLocator.MainViewModel;
            CONTROLLERERROR.ParentAction += TACDisplayFaultMessage;
            LockImage.ParentAction += VehicleUnlock;
            UnlockImage.ParentAction += VehicleLock;
            //#endif

            MessagingCenter.Subscribe<Gauge>(this, "AnimateTapWord", (sender) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    await TapWord.ScaleTo(1.3, 500, Easing.CubicInOut);
                    TapWord.Scale = 1;
                });
            });
        }

        private const int defaultTimespan = 1;

        private double width;
        private double height;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                grid_gauge.RowDefinitions.Clear();
                grid_gauge.ColumnDefinitions.Clear();
                grid_gauge.RowDefinitions.Add(new RowDefinition { Height = new GridLength(90, GridUnitType.Absolute) });
                grid_gauge.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8.5, GridUnitType.Star) });
                grid_gauge.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) });
                grid_gauge.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });

                grid_gauge.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                grid_gauge.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.0, GridUnitType.Star) });
                grid_gauge.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                //if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                //{
                //    StatusLabel.FontSize = 20;
                //}
                ForceLayout();
            }
        }

        void VehicleLock(object sender, EventArgs e)
        {
            SetParameterEventArgs SetParameterEventArgs = new SetParameterEventArgs(164, 1.0f, null);
            QueParameter(SetParameterEventArgs);

            if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue < 2.700)   // 
            {
                SetParameterEventArgs = new SetParameterEventArgs(50, 1.0f, null);
                QueParameter(SetParameterEventArgs);
            }
        }

        void VehicleUnlock(object sender, EventArgs e)
        {
            SetParameterEventArgs SetParameterEventArgs = new SetParameterEventArgs(164, 0.0f, null);
            QueParameter(SetParameterEventArgs);

            if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue < 2.700)   // 
            {
                SetParameterEventArgs = new SetParameterEventArgs(50, 1.0f, null);
                QueParameter(SetParameterEventArgs);
            }
        }


        private bool AlreadyAppeared = false;
        public bool Disappeared = true;

        protected async Task WaitAndExecute(int millisec, Action actionToExecute)
        {
            await Task.Delay(millisec);
            actionToExecute();
        }

        protected async override void OnAppearing()
        {
            System.Diagnostics.Debug.WriteLine("Gauge screen appearing");
            App.ViewModelLocator.GetParameter("SPEED").parameterValue = 0.0f;

            System.Diagnostics.Debug.WriteLine("Gauge screen appearing done");
            bBlockTimeout = false;


            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            formattedString = new FormattedString();
            oldStateString = null;
            oldString = null;
            Task.Factory.StartNew(ShowStatus, cancellationTokenSource.Token);
            if (hybridWebView != null)
                hybridWebView.EvaluateJavascript("try{ App.NavitasMotorController.HtmlAppCommand('Resume');}catch(exeception){console.log('Do not worry for some reason this has not loaded yet' + exception.toString());}");
            base.OnAppearing();

            Disappeared = false;
        }

        float previousGroupOneFaultsVal = 0;
        float previousGroupTwoFaultsVal = 0;
        float previousGroupThreeFaultsVal = 0;
        float previousGroupFourFaultsVal = 0;

        float previousGroupOneWarningsVal = 0;

        bool FaultFlag = false;
        async Task CheckTACFaultMessage()
        {
            float GroupOneFaultsVal = App.ViewModelLocator.GetParameter("GROUPONEFAULTS").parameterValue;
            float GroupTwoFaultsVal = App.ViewModelLocator.GetParameter("GROUPTWOFAULTS").parameterValue;
            float GroupThreeFaultsVal = App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS").parameterValue;
            float GroupFourFaultsVal = App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").parameterValue;

            float GroupOneWarningVal = App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue;

            if (App.ViewModelLocator.GetParameter("CONTROLLERERROR").parameterValue == 1)
            {
                if (GroupOneFaultsVal != previousGroupOneFaultsVal ||
                    GroupTwoFaultsVal != previousGroupTwoFaultsVal ||
                    GroupThreeFaultsVal != previousGroupThreeFaultsVal ||
                    GroupFourFaultsVal != previousGroupFourFaultsVal)
                {
                    MessagingCenter.Send<Gauge>(this, "AnimateTapWord");
                    if ((GroupOneFaultsVal > previousGroupOneFaultsVal ||
                        GroupTwoFaultsVal > previousGroupTwoFaultsVal ||
                        GroupThreeFaultsVal > previousGroupThreeFaultsVal ||
                        GroupFourFaultsVal > previousGroupFourFaultsVal) &&
                         !App.ViewModelLocator.GetParameter("DEMOMODE").parameterBoolean)
                    {
                        await TACFaultMessageProcess();
                    }
                }

                if ((GroupOneWarningVal != previousGroupOneWarningsVal))
                {
                    TACDisplayWarningMessage();
                }
            }
            FaultFlag = false;
            if (GroupOneFaultsVal != 0 || GroupTwoFaultsVal != 0 || GroupThreeFaultsVal != 0 || GroupFourFaultsVal != 0) FaultFlag = true;

           previousGroupOneFaultsVal = GroupOneFaultsVal;
            previousGroupTwoFaultsVal = GroupTwoFaultsVal;
            previousGroupThreeFaultsVal = GroupThreeFaultsVal;
            previousGroupFourFaultsVal = GroupFourFaultsVal;

            previousGroupOneWarningsVal = GroupOneWarningVal;
        }

        protected override void OnDisappearing()
        {
            System.Diagnostics.Debug.WriteLine("Gauge screen for TAC Disappearing");
            cancellationTokenSource.Cancel();
            Disappeared = true;
            base.OnDisappearing();
            if (hybridWebView != null)
                hybridWebView.EvaluateJavascript("try{ App.NavitasMotorController.HtmlAppCommand('Pause');}catch(exeception){console.log('Do not worry for some reason this has not loaded yet' + exception.toString());}");
        }
        //#if SMART_DIAG
        void AddStateString(string strAdd)
        {
            if ((strAdd != oldStateString))
            {
                Span textline = new Span();
                oldStateString = strAdd;
                oldString = null;
                textline.Text = strAdd;
                formattedString.Spans.Add(textline);
                StatusLabel.Text = strAdd;  // change strAdd to formatted string for the history  and set the formattedText property of the label.
            }
        }

        string oldString = null;
        void AddString(string strAdd)
        {
            if ((strAdd != oldString))
            {
                Span textline = new Span();
                oldString = strAdd;
                StatusLabel.Scale = 1;
                //     strAdd = Environment.NewLine + strAdd;
                textline.Text = strAdd;
                formattedString.Spans.Add(textline);
                //         Device.BeginInvokeOnMainThread(() =>
                //        {
                
                StatusLabel.Text = strAdd;  
                StatusLabel.ScaleTo(1.3, 500, Easing.CubicInOut);

                // change strAdd to formatted string for the history and set the formattedText property of the label.
                //            var lastChild = StatusStackLayout.Children.LastOrDefault();
                //       if (lastChild != null)
                //          StatusScrollview.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, true);
                //         StatusScrollview.ScrollToAsync(0,StatusStackLayout.Height, true);
                //      Task t =  StatusScrollview.ScrollToAsync(StatusLabel, ScrollToPosition.End, true);
                //       });
                //         t.Wait();
            }
            //  StatusScrollview.ScrollToAsync(StatusLabel, ScrollToPosition.End, true);
        }
        int State17Counter = 0;
    
        void  ShowStatus()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    while (true)
                    {
                        cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        if (cancellationTokenSource.Token.IsCancellationRequested)
                        {
                                //                 System.Diagnostics.Debug.WriteLine("Cancellation seen");
                                throw (new Exception());
                        }

                        await CheckTACFaultMessage();

                        if ((System.Int16)App.ViewModelLocator.GetParameter("STATE").parameterValue != 17)
                        {
                             State17Counter = 0;
                        }
                        else State17Counter++;
                        //      AddStateString("Vehicle is in the " + GetVehicleStateFromInt((System.Int16)App.ViewModelLocator.GetParameter("STATE) + " state.");
                        switch ((System.Int16)App.ViewModelLocator.GetParameter("STATE").parameterValue)
                        {
                            case 0:    // Reset
                                if (App.ViewModelLocator.GetParameter("VEHICLELOCKED").parameterBoolean)
                                {
                                    AddString("Waiting to be unlocked from the App");
                                }
                                else
                                {
                                    if ((System.Int16)App.ViewModelLocator.GetParameter("KEYIN").parameterValue == 0)
                                    {
                                        AddString("Waiting For Key");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue == 1) && (App.ViewModelLocator.GetParameter("FOOTSW").parameterValue == 0))
                                    {
                                        AddString("Waiting for seat switch");
                                    }

                                    //else
                                    //{
                                    //    AddString("0");
                                    //}
                                }
                                break;
                            case 1:  // ChargerOrTowConnectionCheck
                                if ((System.Int16)App.ViewModelLocator.GetParameter("CHARGERINTERLOCKIN").parameterValue == 0)
                                {
                                    AddString("Waiting for charger to be removed");
                                }
                                else
                                {
                                    if ((System.Int16)App.ViewModelLocator.GetParameter("TOWIN").parameterValue == 1)
                                    {
                                        AddString("Waiting for tow switch");
                                    }
                                    //else
                                    //    AddString("1");
                                }
                                break;
                            case 2:    // ThrottleZeroStartupCheck  We don't wait for throttle to be zero anymore.
                                //AddString("2");
                                break;
                            case 3:   // ThrottleSaturationStartupCheck
                                //AddString("3");
                                break;
                            case 4: // ReverseBuzzerCheck  What to do?
                                //AddString("4");
                                break;
                            case 5: // DCBusCheck
                                if (App.ViewModelLocator.GetParameter("VBATVDC").parameterValue <= 18)
                                {
                                    AddString("Battery is too low");
                                }
                                //else
                                //    AddString("5");
                                // AddString("DC Bus is " + App.ViewModelLocator.GetParameter("VBATVDC.ToString() + "V and must be greater than 18V to drive");
                                break;
                            case 6: //ParkingBrakeCheck What to do?
                                //AddString("6");
                                break;
                            case 7: // Startup
                                //AddString("7");
                                break;
                            case 8: // Neutral
                                if ((App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue == 1) && (App.ViewModelLocator.GetParameter("FOOTSW").parameterValue == 0))
                                {
                                    AddString("Waiting for seat switch");
                                }
                                else
                                {
                                    AddString("Waiting for forward or reverse switch");
                                    //AddString("Mark testing UI here");
                                }

                                break;
                            case 9: // NeutralTransition
                                AddString("Waiting for throttle or foot switch to release");
                                // AddString("Throttle voltage is " + App.ViewModelLocator.GetParameter("VTHROTTLEV + "V and must be less than the Throttle Min setting of " + App.ViewModelLocator.GetParameter("THROTTLEMIN.ToString() + "V when transitioning from neutral to forward or reverse.");
                                break;
                            case 10: // Forward
                            case 11:  // Reverse
                                if ((App.ViewModelLocator.GetParameter("FORWARDSW").parameterValue == 0) && (App.ViewModelLocator.GetParameter("REVERSESW").parameterValue == 0))
                                {
                                    if (App.ViewModelLocator.GetParameter("SPEED").parameterValue != 0.0)
                                    {
                                        AddString("Waiting to stop");
                                    }
                                }
                                else
                                {
                                    if ((App.ViewModelLocator.GetParameter("DISABLEBRAKESWITCH").parameterValue == 0) && (App.ViewModelLocator.GetParameter("BRAKESW").parameterValue == 0))
                                    {
                                        AddString("Waiting for brake switch to be released");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("DISABLEANALOGBRAKE").parameterValue == 0) && (App.ViewModelLocator.GetParameter("VBRAKEV").parameterValue > App.ViewModelLocator.GetParameter("BRAKEMIN").parameterValue) && (App.ViewModelLocator.GetParameter("VBRAKEV").parameterValue < App.ViewModelLocator.GetParameter("BRAKEFULL").parameterValue)) //BRAKEMAX
                                    {
                                        AddString("Waiting for brake pedal to be released");
                                        //AddString("Analog brake is on.  Analog brake voltage is " + App.ViewModelLocator.GetParameter("VBRAKEV.ToString() + "V and is in between the Brake Min " + App.ViewModelLocator.GetParameter("BRAKEMIN + "V and the Brake Max " + App.ViewModelLocator.GetParameter("BRAKEMAX + "V Setttings.");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue == 1) && (App.ViewModelLocator.GetParameter("FOOTSW").parameterValue == 0))
                                    {
                                        AddString("Waiting for seat switch");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("DISABLEFOOTSWITCH").parameterValue == 0) && (App.ViewModelLocator.GetParameter("FOOTSW").parameterValue == 0))
                                    {
                                        AddString("Waiting for foot switch");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue > App.ViewModelLocator.GetParameter("THROTTLEMIN").parameterValue) && (App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue < App.ViewModelLocator.GetParameter("THROTTLEMAX").parameterValue))
                                    {
                                        AddString("Driving");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue > App.ViewModelLocator.GetParameter("THROTTLEMAX").parameterValue) && (App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue < App.ViewModelLocator.GetParameter("THROTTLEFULL").parameterValue))
                                    {
                                        AddString("Driving - Full Throttle");
                                    }
                                    else if ((App.ViewModelLocator.GetParameter("VTHROTTLEV").parameterValue > App.ViewModelLocator.GetParameter("THROTTLEFULL").parameterValue))
                                    {
                                        AddString("Throttle out of range");
                                    }
                                    else
                                    {
                                        AddString("Waiting for throttle");//.  Throttle Voltage is " + App.ViewModelLocator.GetParameter("VTHROTTLEV.ToString() + "V Throttle Min is " + App.ViewModelLocator.GetParameter("THROTTLEMIN.ToString() + "V Throttle Max is " + App.ViewModelLocator.GetParameter("THROTTLEMAX.ToString() + "V.");
                                    }
                                }

                                break;
                            case 12:  // DropBrake
                                //AddString("12");
                                break;
                            case 13:  // Error
                                //AddString("13");
                                break;
                            case 14:  // NeutralCoastThenReset
                                //AddString("14");
                                break;
                            case 15:  // SafetyInterlockStartupChecks
                            case 16:  // SafetyInterlockStartupChecks
                            case 17:  // SafetyInterlockStartupChecks
                            // If faluts is cleared by Initialize, the state machine will run in a loop of some states but stay in state 17 longer (>200ms) ,
                            // since this task is excuted every 50ms, we maynot able to catch the other states, so we check if it is cotinueous stay in state 17 more than more than 1 seconds, the vehiclestate stays in a start up loop.
                            // power cycle the controller to get error message back.
                                if (!FaultFlag)
                                {
                                    if (State17Counter >20)
                                    {
                                        AddString("Power Cycle the controller");
                                    }
                                }
                                else AddString("Tap Caution Icon For Error Details");
                                break;

                            case 18:  // SafetyInterlockStartupChecks
                                AddString("Vehicle must start in Neutral");
                                break;
                            default:
                                //AddString("unknown");
                                if (FaultFlag)
                                    AddString("Tap Caution Icon For Error Details");
                                break;
                        }
                            //System.Diagnostics.Debug.WriteLine("time this 1 " + DateTime.Now.Millisecond.ToString());
                            await Task.Delay(50);
                            //System.Diagnostics.Debug.WriteLine("time this 2 " + DateTime.Now.Millisecond.ToString());
                        }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Gauge show status cancelled");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            });
        }
        //#endif
    }
}
