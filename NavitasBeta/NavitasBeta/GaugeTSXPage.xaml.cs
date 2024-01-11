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

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GaugeTSX : NavitasGeneralPage  // , INotifyPropertyChanged
    {
        System.Threading.CancellationTokenSource cancellationTokenSource;
        FormattedString formattedString;
        public bool bBlockTimeout;
#if OLD
        public ICommand ImgTapCommand { private set; get; }
#endif 


        public GaugeTSX()
        {
            try
            {
                App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue = DisplaySettings.GetStandardorMetric() ? 1 : 0;
                InitializeComponent();
                switch (Device.RuntimePlatform)
                {
                    case Device.UWP:
                        var a = this;
                        VisualElement b = this.FindByName<VisualElement>("syncFusionElement1");
                        this.grid_gauge.Children.Remove(MTEMPGauge);
                        this.grid_gauge.Children.Remove(VBATGauge);
                        this.grid_gauge.Children.Remove(PARMOTORRPM);
                        this.grid_gauge.Children.Remove(NEUTRAL);
                        this.grid_gauge.Children.Remove(FORWARDSWITCH);
                        this.grid_gauge.Children.Remove(REVERSESWITCH);
                        this.grid_gauge.Children.Remove(KEY);
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
                LoadCommunicationItemsTSX();
                AddActivityPopUp();
                BuildErrorIcon();
                BuildDemoFlashing();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: GaugeTSXPage.xaml.cs" + ex.Message);
            }
            BindingContext = App.ViewModelLocator.MainViewModelTSX;
            System.Diagnostics.Debug.WriteLine("MainViewModelTSX Set for gauge page");
            CONTROLLERERROR.ParentAction += TSXDisplayFaultMessage;
            LockImage.ParentAction += VehicleLock;
            UnlockImage.ParentAction += VehicleUnlock;

            MessagingCenter.Subscribe<GaugeTSX>(this, "AnimateTapWord", (sender) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    await TapWord.ScaleTo(1.3, 500, Easing.CubicInOut);
                    TapWord.Scale = 1;
                });
            });
#if OLD
            ErrorImage.BindingContext = this;
            UnlockImage.BindingContext = this;
            LockImage.BindingContext = this;
#endif
        }

        public Func<string, Task<string>> EvaluateJavascript { get; set; }
        public Func<string, Task<string>> InjectJavascript { get; set; }
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

        void VehicleUnlock(object sender, EventArgs e)
        {
            QueParameter(new SetParameterEventArgs(2, 1.0f, null));
            QueParameter(new SetParameterEventArgs(199, 1.0f, null));
        }


        void VehicleLock(object sender, EventArgs e)
        {
            QueParameter(new SetParameterEventArgs(2, 0.0f, null));
            QueParameter(new SetParameterEventArgs(199, 1.0f, null));
        }

        public bool Disappeared = true;
        protected async Task WaitAndExecute(int millisec, Action actionToExecute)
        {
            await Task.Delay(millisec);
            actionToExecute();
        }
        protected override void OnAppearing()
        {
            System.Diagnostics.Debug.WriteLine("Gauge screen appearing");

            var displayUnitOptionSaved = DisplaySettings.GetStandardorMetric() ? 1 : 0;
            if (!(App._MainFlyoutPage._DeviceListPage._device is DemoDevice) && displayUnitOptionSaved != App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue)
            {
                App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue = displayUnitOptionSaved; 
            }

            App.ViewModelLocator.GetParameterTSX("SPEED").parameterValue = 0.0f;

            System.Diagnostics.Debug.WriteLine("Gauge screen appearing done");
            bBlockTimeout = false;

            cancellationTokenSource = new System.Threading.CancellationTokenSource();
            formattedString = new FormattedString();
            //TSX does not have green words, it should.
            Task.Factory.StartNew(ShowStatus, cancellationTokenSource.Token);
            if (hybridWebView != null)
                hybridWebView.EvaluateJavascript("try{ App.NavitasMotorController.HtmlAppCommand('Resume');}catch(exeception){console.log('Do not worry for some reason this has not loaded yet' + exception.toString());}");

            base.OnAppearing();

            Disappeared = false;
        }

        float previousStartUpErrorsVal = 0;
        float previousRunTimeErrorsLowVal = 0;
        float previousRunTimeErrorsHighVal = 0;

        async Task CheckTSXFaultMessage()
        {
            float StartUpErrorsVal = App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").parameterValue;
            float RunTimeErrorsLowVal = App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW").parameterValue;
            float RunTimeErrorsHighVal = App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH").parameterValue;

            if (App.ViewModelLocator.GetParameterTSX("CONTROLLERERROR").parameterValue == 1)
            {
                if (StartUpErrorsVal != previousStartUpErrorsVal ||
                    RunTimeErrorsLowVal != previousRunTimeErrorsLowVal ||
                    RunTimeErrorsHighVal != previousRunTimeErrorsHighVal)
                {
                    MessagingCenter.Send<GaugeTSX>(this, "AnimateTapWord");
                    if ((StartUpErrorsVal > previousStartUpErrorsVal ||
                    RunTimeErrorsLowVal > previousRunTimeErrorsLowVal ||
                    RunTimeErrorsHighVal > previousRunTimeErrorsHighVal) &&
                    !App.ViewModelLocator.GetParameterTSX("DEMOMODE").parameterBoolean)
                    {
                        await TSXFaultMessageProcess();
                    }
                }
            }

            previousStartUpErrorsVal = StartUpErrorsVal;
            previousRunTimeErrorsLowVal = RunTimeErrorsLowVal;
            previousRunTimeErrorsHighVal = RunTimeErrorsHighVal;
        }

        protected override void OnDisappearing()
        {
            System.Diagnostics.Debug.WriteLine("Gauge screen for TSX Disappearing");
            cancellationTokenSource.Cancel();
            Disappeared = true;
            base.OnDisappearing();
            if (hybridWebView != null)
                hybridWebView.EvaluateJavascript("try{ App.NavitasMotorController.HtmlAppCommand('Pause');}catch(exeception){console.log('Do not worry for some reason this has not loaded yet' + exception.toString());}");
        }

        string oldString = null;
        void AddString(string strAdd)
        {
            if (strAdd != oldString)
            {
                Span textline = new Span();
                oldString = strAdd;
                StatusLabel.Scale = 1;
                textline.Text = strAdd;
                formattedString.Spans.Add(textline);

                StatusLabel.Text = strAdd;
                StatusLabel.ScaleTo(1.3, 500, Easing.CubicInOut); 
            }
        }

        //void AddWarningString(string strAdd)
        //{
        //    Span textline = new Span();
        //    textline.Text = strAdd;
        //    formattedString.Spans.Add(textline);

        //    CONTROLLERWARNING.Text = strAdd;
        //    CONTROLLERWARNING.ScaleTo(1.3, 1000, Easing.BounceOut);
        //    CONTROLLERWARNING.Scale = 1;
        //}

        void ShowStatus()
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

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if ((System.Int16)App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue != 0 &&
                            App.ViewModelLocator.GetParameterTSX("LOCKED").parameterValue == 1)
                        {
                            AddString("Waiting to be unlocked from the App");
                        }
                        else
                        {
                            await CheckTSXFaultMessage();
                            
                            switch ((System.Int16)App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue)
                            {

                                case 1:     //Reset
                                    if (App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").parameterValue != 0)
                                        AddString("Faults Preventing Drive");
                                    else
                                        if (((int)App.ViewModelLocator.GetParameterTSX("PARSWITCHSTATES").parameterValue & 0x01) == 0)
                                            AddString("Charger Connected" + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                        else
                                            AddString("Waiting for Key" + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                    break;
                                case 9:
                                    if (((int)App.ViewModelLocator.GetParameterTSX("PARSWITCHSTATES").parameterValue & 0x01) == 0)
                                        AddString("Charger Connected" + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                    else
                                        if(((int)App.ViewModelLocator.GetParameterTSX("PARSWITCHSTATES").parameterValue & 0x20) == 0)
                                            AddString("Waiting for foot switch" + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                        else
                                            AddString(" State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                    break;
                                case 3:
                                    if (App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue == 0)
                                   
                                        AddString("Waiting for throttle" + " Voltage: " + App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue.ToString() + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());

                                    else
                                        AddString("Driving" + " Voltage: " + App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue.ToString() + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                    break;
                                case 4:
                                    if (App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue == 0)

                                        AddString("Waiting for throttle" + " Voltage: " + App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue.ToString() + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());


                                    else
                                        AddString("Driving" + " Voltage: " + App.ViewModelLocator.GetParameterTSX("PARSVARTHROTTLE").parameterValue.ToString() + " State is " + App.ViewModelLocator.GetParameterTSX("PARSVARMACHINESTATE2").parameterValue.ToString());
                                    break;
                                default:
                                    AddString("");
                                    break;
                            }
                        }
                    });
                    Task.Delay(50).Wait();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GaugeTSX show status cancelled");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

    }
}
