using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;

using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

using Xamarin.Essentials;
using System.Reflection;

using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Internals;
using static Xamarin.Essentials.Permissions;
using Newtonsoft.Json.Linq;
using OxyPlot.Series;
using OxyPlot;
using System.ComponentModel;

namespace NavitasBeta
{
    public class NavitasGeneralPage : ContentPage
    {
        public ActivityIndicator PublicActivityIndicator;
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        public SetParameterEventArgs SetParameterEventArgs;
        public PageParameterList PageParameters;
        public PageParameterList PageParametersToPlot;
        public long pageUniqueId;
        public long pageUniqueIdToPlot = 0;
        public bool AlreadyAppeared = false;
        //public bool Disappeared = true;

        public OxyPlotPage OxyPlotPage;

        List<string> hiddenTACParametersDatalogging = new List<string>();
        List<string> hiddenTSXParametersDataLogging = new List<string>();

        //WebView stuff
        public HybridWebView hybridWebView;
        protected bool bBlockTimeout;
        public long htmlPageUniqueId = -1;
        public string htmlFilePathAndName = "";
        public DateTime PageliveReloadTime;

        public NavitasGeneralPage()
        {
            try
            {
                //InitializeComponent();
                AddParamValuesToQueue += App._devicecommunication.AddParamValuesToQueue;
                On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.OverFullScreen);
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    BindingContext = App.ViewModelLocator.MainViewModel;
                }
                else
                    BindingContext = App.ViewModelLocator.MainViewModelTSX;

                PageliveReloadTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: NavitasGeneralPage.xaml.cs" + ex.Message);
            }
        }

        public static Binding GetBinding(BindableObject self, BindableProperty property)
        {
            var methodInfo = typeof(BindableObject).GetTypeInfo().GetDeclaredMethod("GetContext");
            var context = methodInfo?.Invoke(self, new[] { property });

            var propertyInfo = context?.GetType().GetTypeInfo().GetDeclaredField("Binding");
            return propertyInfo?.GetValue(context) as Binding;
        }
        public void LoadCommunicationItemsTAC()
        {
            PageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
            var pageName = this.GetType().Name;
            foreach (var parameter in App.ViewModelLocator.MainViewModel.GoiParameterList)
            {
                var b = this.FindByName<Object>(parameter.PropertyName);

                if (b != null)
                {
                    var c = b.GetType().Name.ToString();
                    if (pageName == "DealerSettingsPage" && c == "StackLayout")
                    {
                        var StackLayoutElement = (b as StackLayout).Children.Where(x => x.GetType().Name.ToString() == "StackLayout").FirstOrDefault();
                        if (StackLayoutElement != null)
                        {
                            var SwitchElement = (StackLayoutElement as StackLayout).Children.Where(x => x.GetType().Name.ToString() == "Switch").FirstOrDefault();
                            if (SwitchElement != null)
                            {
                                var binding = GetBinding(SwitchElement, Switch.IsToggledProperty);
                                if (binding.Path == "SelectedForDatalogging")
                                {
                                    parameter.SelectedForDatalogging = true;
                                }
                            }
                        }
                    }
                    (b as BindableObject).BindingContext = parameter;
                    BuildCommunicationsList(PageParameters.parameterList, parameter);
                }
            }
            pageUniqueId = App._devicecommunication.AddToPacketList(PageParameters);

        }
        public void LoadCommunicationItemsTSX()
        {
            PageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, this);
            var pageName = this.GetType().Name;
            foreach (var parameter in App.ViewModelLocator.MainViewModelTSX.GoiParameterList)
            {
                var b = this.FindByName<Object>(parameter.PropertyName);
                if (b != null)
                {
                    var c = b.GetType().Name.ToString();
                    if (pageName == "DealerSettingsTSXPage" && c == "StackLayout")
                    {
                        var StackLayoutElement = (b as StackLayout).Children.Where(x => x.GetType().Name.ToString() == "StackLayout").FirstOrDefault();
                        if (StackLayoutElement != null)
                        {
                            var SwitchElement = (StackLayoutElement as StackLayout).Children.Where(x => x.GetType().Name.ToString() == "Switch").FirstOrDefault();
                            if (SwitchElement != null)
                            {
                                var binding = GetBinding(SwitchElement, Switch.IsToggledProperty);
                                if (binding.Path == "SelectedForDatalogging")
                                {
                                    parameter.SelectedForDatalogging = true;
                                }
                            }
                        }
                    }
                    (b as BindableObject).BindingContext = parameter;
                    BuildCommunicationsList(PageParameters.parameterList, parameter);
                }
            }

            if (PageParameters.parameterList.Count != 0)
                pageUniqueId = App._devicecommunication.AddToPacketList(PageParameters);

        }

        public void BuildCommunicationsList(List<GoiParameter> parameterList, GoiParameter parameter)
        {
            var parentParameter = parameter;
            if (parameter.SubsetOfAddress) //change to parent parameter
                parentParameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == parameter.Address));

            if (!parameterList.Contains(parentParameter))
                parameterList.Add(parentParameter);

            if (parameter.GroupedParameters != null && parameter.GroupedParameters.Count != 0)
            {//if not in the xaml list then add to communications list so the binding responds
                foreach (var group in parameter.GroupedParameters)
                {
                    foreach (var item in group.ParameterFileItemList)
                    {
                        var subParameter = App.ViewModelLocator.GetParameter(item.PropertyName);

                        parentParameter = subParameter;
                        if (subParameter.SubsetOfAddress) //change to parent parameter
                            parentParameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == subParameter.Address));

                        if (!parameterList.Contains(parentParameter))
                            parameterList.Add(parentParameter);
                    }
                }
            }
        }

        protected override void OnAppearing()
        {
            if (PageParameters != null) //some pages like the scripting page don't always keep this initiated
                PageParameters.Active = true;
            base.OnAppearing();
            if (PageParametersToPlot != null) //somebody is plotting
                PageParametersToPlot.Active = false;

            if ((App.ViewModelLocator.MainViewModel.IsDemoMode || App.ViewModelLocator.MainViewModelTSX.IsDemoMode) && DemoFlash != null)
            {
                var demoFlashingAnimation = new Animation(v => DemoFlash.Opacity = v, 0.75, 0);
                Device.BeginInvokeOnMainThread(() =>
                {
                    demoFlashingAnimation.Commit(this,
                        "DemoFlashingAnimation",
                        16,
                        3000,
                        Easing.CubicInOut,
                        null,
                        () => true);
                });
            }
            //OpenOrAppendOrCloseFIle("", "Close"); //Syntax to Close any open files
        }

        public void QueParameter(SetParameterEventArgs SetParameterEventArgs)
        {
            AddParamValuesToQueue(this, SetParameterEventArgs);
        }
        protected override void OnDisappearing()
        {
            if (PageParameters != null) //some pages like the scripting page don't always keep this initiated
                PageParameters.Active = false;
            base.OnDisappearing();
            //Cancel animation
            if ((App.ViewModelLocator.MainViewModel.IsDemoMode || App.ViewModelLocator.MainViewModelTSX.IsDemoMode) && DemoFlash != null)
            {
                this.AbortAnimation("DemoFlashingAnimation");
                DemoFlash.Opacity = 0.0f;
            }
        }
        public async void ParameterCompleted(object sender, EventArgs e)
        {
            try
            {
                hasDoneButtonBeenPressed = true;
                var textparameterValue = ((Xamarin.Forms.Entry)sender).Text;
                float fparameterValue;

                if (textparameterValue.Contains("0x"))
                    fparameterValue = (float)int.Parse(textparameterValue.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                else
                    fparameterValue = float.Parse(textparameterValue);

                GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                if (fparameterValue >= parameter.MinimumParameterValue && fparameterValue <= parameter.MaximumParameterValue)
                {
                    if (parameter.Address != -1)
                    {
                        if (parameter.PropertyName != "IBATLIMIT" && parameter.PropertyName != "PARMAXMOTORFWDSPEEDLIMIT" && parameter.PropertyName != "PARMAXMOTORREVSPEEDLIMIT")
                        {
                            SetParameterEventArgs = new SetParameterEventArgs(((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).Address, fparameterValue, "never used so get rid of this");
                            QueParameter(SetParameterEventArgs);
                        }
                    }
                    (sender as Xamarin.Forms.VisualElement).Unfocus();
                }
                else
                {
                    if (fparameterValue < parameter.MinimumParameterValue)
                        DisplayAlert("Input Error", "value must be greater than " + parameter.MinimumParameterValue, "OK");
                    if (fparameterValue > parameter.MaximumParameterValue)
                        DisplayAlert("Input Error", "value must be less than " + parameter.MaximumParameterValue, "OK");
                    throw (new ArithmeticException());
                }
                if (ControllerTypeLocator.ControllerType == "TSX")
                {
                    if (parameter.PropertyName == "MILESORKILOMETERS")
                    {
                        DisplaySettings.SaveStandardorMetric(fparameterValue == 0 ? false : true);

                        App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue = DisplaySettings.GetStandardorMetric() ? 1 : 0;
                    }
                    if (parameter.PropertyName == "SPEEDOMETERMAXSPEEDKPH" || parameter.PropertyName == "SPEEDOMETERMAXSPEED")
                    {
                        // convert KPH to MPH
                        if (parameter.PropertyName == "SPEEDOMETERMAXSPEEDKPH")
                        {
                            App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue = fparameterValue / 1.609344f;
                        }
                        else
                        {
                            App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue = fparameterValue;
                        }
                        DisplaySettings.SaveSpeedOmeterMaxSpeed(App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue.ToString());
                        App.ViewModelLocator.GetParameterTSX("SPEEDOMETERMAXSPEED").parameterValue = float.Parse(DisplaySettings.GetSpeedOmeterMaxSpeed());
                    }

                    if (parameter == App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDMIN"))
                    {
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDDEADBANDX").Address, fparameterValue, "PARMAXMOTORFWDSPEEDLIMIT"));
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDDEADBANDY").Address, 0.0f, "PARPRIMARYTHROTTLEFORWARDDEADBANDY"));
                    }

                    if (parameter == App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLETYPE"))
                    {
                        if (fparameterValue == 2)
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDMIN").Address, (float)1.5, "PARPRIMARYTHROTTLEFORWARDMIN"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDDEADBANDX").Address, (float)1.5, "PARPRIMARYTHROTTLEFORWARDDEADBANDX"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDACCELERATIONX").Address, (float)2, "PARPRIMARYTHROTTLEFORWARDACCELERATIONX"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDACCELERATIONY").Address, (float)10, "PARPRIMARYTHROTTLEFORWARDACCELERATIONY"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDMAX").Address, (float)3.5, "PARPRIMARYTHROTTLEFORWARDMAX"));
                        }
                        if (fparameterValue == 1)
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDMIN").Address, (float)3.5, "PARPRIMARYTHROTTLEFORWARDMIN"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDDEADBANDX").Address, (float)3.5, "PARPRIMARYTHROTTLEFORWARDDEADBANDX"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDACCELERATIONX").Address, (float)2.5, "PARPRIMARYTHROTTLEFORWARDACCELERATIONX"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDACCELERATIONY").Address, (float)10, "PARPRIMARYTHROTTLEFORWARDACCELERATIONY"));
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARPRIMARYTHROTTLEFORWARDMAX").Address, (float)1.5, "PARPRIMARYTHROTTLEFORWARDMAX"));
                        }
                    }

                    if (parameter == App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT"))
                    {
                        var mphcalc = (float)Math.PI * fparameterValue * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * 60 / 5280 / 12;
                        BuildWarningPopUp(null, App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH"), mphcalc);
                    }

                    if (parameter == App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT"))
                    {
                        var mphcalc = (float)Math.PI * fparameterValue * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * 60 / 5280 / 12;
                        BuildWarningPopUp(null, App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH"), mphcalc);
                    }

                    if ((parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH")) || (parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH")))
                    {
                        if (parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH"))
                        {
                            // convert KPH to MPH
                            fparameterValue = fparameterValue / 1.609344f;
                        }
                        BuildWarningPopUp(null, parameter, fparameterValue);
                    }

                    if ((parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH")) || (parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH")))
                    {
                        if (parameter == App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH"))
                        {
                            // convert KPH to MPH
                            fparameterValue = fparameterValue / 1.609344f;
                        }
                        BuildWarningPopUp(null, parameter, fparameterValue);
                    }
                    //GoiParameter junk = App.ViewModelLocator.GetParameterTSX("TIREDIAMETER");
                    if (parameter == App.ViewModelLocator.GetParameterTSX("TIREDIAMETER"))
                    {
                        var value = (App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * fparameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)value, "PARMAXMOTORFWDSPEEDLIMIT"));

                        value = (App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * fparameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)value, "PARMAXMOTORREVSPEEDLIMIT"));
                    }
                    if (parameter == App.ViewModelLocator.GetParameterTSX("REARAXLERATIO"))
                    {
                        var value = (fparameterValue * App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)value, "PARMAXMOTORFWDSPEEDLIMIT"));

                        value = (fparameterValue * App.ViewModelLocator.GetParameterTSX("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)value, "PARMAXMOTORREVSPEEDLIMIT"));
                    }
                }
                else if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    if (parameter == App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEEDKPH"))
                    {
                        // convert KPH to MPH
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("SPEEDOMETERMAXSPEED").Address, (float)fparameterValue / 1.609344f, "SPEEDOMETERMAXSPEED"));
                    }
                    if ((parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH")) || (parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH")))
                    {
                        if (parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH"))
                        {
                            // convert KPH to MPH
                            fparameterValue = fparameterValue / 1.609344f;
                        }

                        if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= App.ViewModelLocator.GetParameter("MAXMFGFWDSPEED").ImplementedFirmwareVersion)
                        {
                            await ReadOneParameter(App.ViewModelLocator.GetParameter("MAXMFGFWDSPEED"));  // no max speed dynamic page
                            if (fparameterValue > App.ViewModelLocator.GetParameter("MAXMFGFWDSPEED").parameterValue)
                            {
                                fparameterValue = App.ViewModelLocator.GetParameter("MAXMFGFWDSPEED").parameterValue;

                                await DisplayAlert("Vehicle Manufacturer Limit Exceeded", "The speed setting will be limited to " + $"{fparameterValue}", "OK");
                            }
                        }
                        BuildWarningPopUp(null, parameter, fparameterValue);
                    }

                    if ((parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH")) || (parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH")))
                    {
                        if (parameter == App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH"))
                        {
                            // convert KPH to MPH
                            fparameterValue = fparameterValue / 1.609344f;
                        }
                        if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= App.ViewModelLocator.GetParameter("MAXMFGREVSPEED").ImplementedFirmwareVersion)
                        {
                            await ReadOneParameter(App.ViewModelLocator.GetParameter("MAXMFGREVSPEED"));  // no max speed dynamic page
                            if (fparameterValue > App.ViewModelLocator.GetParameter("MAXMFGREVSPEED").parameterValue)
                            {
                                fparameterValue = App.ViewModelLocator.GetParameter("MAXMFGREVSPEED").parameterValue;

                                await DisplayAlert("Vehicle Manufacturer Limit Exceeded", "The speed setting will be limited to " + $"{fparameterValue}", "OK");
                            }
                        }
                        BuildWarningPopUp(null, parameter, fparameterValue);
                    }

                    if (parameter == App.ViewModelLocator.GetParameter("IBATLIMIT"))
                    {
                        if (fparameterValue > 200)
                            BuildWarningPopUp("BatteryLimit", parameter, fparameterValue);
                        else
                        {
                            SetParameterEventArgs = new SetParameterEventArgs(((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).Address, fparameterValue, "never used so get rid of this");
                            QueParameter(SetParameterEventArgs);
                        }

                    }

                    if (parameter == App.ViewModelLocator.GetParameter("TIREDIAMETER"))
                    {
                        var value = (App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue * App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * fparameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)value, "Fwd Lmt RPM"));

                        value = (App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue * App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * fparameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)value, "Rvs Lmt RPM"));
                    }
                    if (parameter == App.ViewModelLocator.GetParameter("REARAXLERATIO"))
                    {
                        var value = (fparameterValue * App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)value, "Fwd Lmt RPM"));

                        value = (fparameterValue * App.ViewModelLocator.GetParameter("PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH").parameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2);
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)value, "Rvs Lmt RPM"));
                    }
                    if (parameter == App.ViewModelLocator.GetParameter("INPUT_VOLTAGE") && (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= App.ViewModelLocator.GetParameter("Otp_dcbusScale").ImplementedFirmwareVersion))
                    {
                        var value = (float)(fparameterValue / (App.ViewModelLocator.GetParameter("VBATVDC").parameterValue / (float)App.ViewModelLocator.GetParameter("Otp_dcbusScale").parameterValue));
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("Otp_dcbusScale").Address, (float)value, null));
                        value = (float)(fparameterValue / (App.ViewModelLocator.GetParameter("LOGICSIDEBATTERYVOLTAGEV").parameterValue / (float)App.ViewModelLocator.GetParameter("Otp_logicPowerScale").parameterValue));
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("Otp_logicPowerScale").Address, (float)value, null));
                    }
                }
            }
            catch (ArgumentNullException)
            {
            }
            catch (FormatException f)
            {
                DisplayAlert("Input Error", f.Message, "OK");
            }
            catch (ArithmeticException f)
            {
            }
        }

        bool hasDoneButtonBeenPressed = false;
        bool doNotShowThisAgain = false;
        string parameterEntryValue = "";
        public void ParameterFocused(object sender, EventArgs e)
        {
            ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).bupdate = false;
            parameterEntryValue = (sender as Xamarin.Forms.Entry).Text;
            //RG Oct 2020, enabled the if property changed logic in our ViewModelBase (MVAnything) INotifyPropertyChanged implementation
            //But if someone enters a value but does not hit done then the value will just stay on the screen 
            //the actual property would not change and people would not know the value is incorrect
            ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).couldBeDirtyBecauseKBDoneWasNotUsed = true;

        }

        ContentView doneBackgroundOverlay = null;
        string altEnterBtnName = "";

        Button btnOkay;
        public void BuildDoneButtonWarningTextBox()
        {
            AbsoluteLayout overlayArea = this.Content as AbsoluteLayout;

            doneBackgroundOverlay = new ContentView
            {
                IsVisible = false,
                BackgroundColor = Color.FromHex("#C0202020"),
            };

            AbsoluteLayout.SetLayoutFlags(doneBackgroundOverlay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(doneBackgroundOverlay, new Rectangle(0, 0, 1, 1));

            StackLayout doneDisplayAlertContainer = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center
            };

            //Create a frame for the display alert
            Frame doneDisplayAlertFrame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#C2C3C6"),
                HasShadow = true,
                Content = doneDisplayAlertContainer,
                Margin = new Thickness(10, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Label lblTitle = new Label()
            {
                Text = $"Your change has been dismissed because you tapped outside of the keyboard",
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    altEnterBtnName = "Done";
                    break;
                case Device.Android:
                    altEnterBtnName = "Enter or ✅";
                    break;
                case Device.UWP:
                    altEnterBtnName = "Enter";
                    break;
                default:
                    break;
            }

            Label lblMessage = new Label()
            { Text = $"Re-enter the value and press {altEnterBtnName} on the keyboard when finished." };

            //DoNotShowAgainContainer
            StackLayout doNotShowAgainContainer = new StackLayout() { Orientation = StackOrientation.Horizontal };

            //Add checkbox
            CheckBox doNotShowAgainCheckbox = new CheckBox()
            {
                HorizontalOptions = LayoutOptions.Start
            };
            doNotShowAgainCheckbox.CheckedChanged += (sender, args) =>
            {
                //Save this value to persistent object
                Authentication.SaveDoNotShowMessage(args.Value);
            };

            // Checkbox label
            Label doNotAskAgainText = new Label()
            {
                Text = "Do not show this message again",
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

            doNotShowAgainContainer.Children.Add(doNotShowAgainCheckbox);
            doNotShowAgainContainer.Children.Add(doNotAskAgainText);

            // --Button
            btnOkay = new Button()
            {
                Text = "Ok",
                HorizontalOptions = LayoutOptions.End,
                Padding = new Thickness(20, 10)
            };

            btnOkay.Clicked += (s, args) =>
            {
                doneBackgroundOverlay.IsVisible = false;
            };

            doneDisplayAlertContainer.Children.Add(lblTitle);
            doneDisplayAlertContainer.Children.Add(lblMessage);
            doneDisplayAlertContainer.Children.Add(doNotShowAgainContainer);
            doneDisplayAlertContainer.Children.Add(btnOkay);

            doneBackgroundOverlay.Content = doneDisplayAlertFrame;
            overlayArea.Children.Add(doneBackgroundOverlay);
        }


        public void ParameterUnfocused(object sender, EventArgs e)
        {
            if (parameterEntryValue != (sender as Xamarin.Forms.Entry).Text)
            {
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    //Wipe out the uncompleted text after 1 seconds
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (!Authentication.GetDoNotShowMessage())
                        {
                            if (!hasDoneButtonBeenPressed)
                            {
                                // Display Custom Text Box
                                doneBackgroundOverlay.IsVisible = true;
                            }
                        }
                    });
                    return false;
                });
            }

            ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).bupdate = true;
            if (((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH" ||
                ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH" ||
                ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH" ||
                ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH")
            {
                //Force setting dirty bit to True on tire diameter which results in setting fwd or rev speed value properly
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    App.ViewModelLocator.GetParameter("TIREDIAMETER").couldBeDirtyBecauseKBDoneWasNotUsed = true;
                }
                else
                {
                    App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").couldBeDirtyBecauseKBDoneWasNotUsed = true;
                }
            }

            hasDoneButtonBeenPressed = false; //reset
            parameterEntryValue = "";
        }

        public void BitParameterToggled(object sender, EventArgs e)
        {
            try
            {
                //This better be a bit parameter
                GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                GoiParameter parentParameter;

                if (PageParameters.parameterListType == PageParameterList.ParameterListType.TAC)
                    parentParameter = (App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == parameter.Address) && (x.SubsetOfAddress == false)));
                else
                    parentParameter = (App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => (x.Address == parameter.Address) && (x.SubsetOfAddress == false)));

                if (parentParameter.PropertyName == "Options")
                    System.Diagnostics.Debug.WriteLine("just to debug this");


                int temp = (int)parentParameter.parameterValue;
                if ((sender as Switch).IsToggled)
                {
                    temp |= (1 << (int)parameter.BitRangeStart);
                }
                else
                {
                    temp &= ~(1 << (int)parameter.BitRangeStart);
                }

                //When the App starts up things like toggled bits being display get this BitParameterToggled if they are toggled
                //It seems it only writes the same exisiting value but I don't do it just to not confuse actual writing of changes
                //when debugging
                if (parentParameter.parameterValue != temp)
                {
                    //parentParameter.bupdate = false;
                    //parentParameter.parameterValue = temp; //This fixes a quick toggling of another bit in this parent to not overwrite this one.
                    //remember bupdate is cleared in the write method at the heart of AddParamValuesToQueue;
                    SetParameterEventArgs = new SetParameterEventArgs(parentParameter.Address, (float)temp, "never used so get rid of this");
                    AddParamValuesToQueue(this, SetParameterEventArgs);
                }
                //parameter.bupdate = true;
            }
            catch (ArgumentNullException)
            {

            }
            catch (FormatException f)
            {
                DisplayAlert("Input Error", f.Message, "OK");
            }
        }

        public async void StartDatalogging()
        {
            PageParameters.Active = false;
            try
            {
                OxyPlotPage = new OxyPlotPage(1);
                bool isUserSessionValid = await App.ParseManagerAdapter.IsUserSessionValid();
                if (!isUserSessionValid)
                {
                    App._AutomaticLogin.Login();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("StartDatalogging exception: " + e.Message);
            }
            OxyPlotPage.RemoveAllGraphs();

            ParameterVehicleItems hiddenTACParameter = FileManager.GetDeserializedObject<ParameterVehicleItems>("HiddenParameter.xml");

            if (App.AppConfigurationLevel != "DEV")
            {
                foreach (ParameterItem parameterItem in hiddenTACParameter.ParameterVehicleList)
                {
                    hiddenTACParametersDatalogging.Add(parameterItem.PropertyName);
                }
            }

            ParameterVehicleItems hiddenTSXParameter = FileManager.GetDeserializedObject<ParameterVehicleItems>("HiddenTSXParameter");

            if (App.AppConfigurationLevel != "DEV")
            {
                foreach (ParameterItem parameterItem in hiddenTSXParameter.ParameterVehicleList)
                {
                    hiddenTSXParametersDataLogging.Add(parameterItem.PropertyName);
                }
            }

            //RG Aug 2020, we seem to be cleaning up old things that should have been cleaned up another way?
            if (pageUniqueIdToPlot != 0) //non zero indicates a plot was happening
            {
                //float checking that he is not active before removing, probably a waste of time
                if ((DeviceComunication.PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == pageUniqueIdToPlot))).parentPage.Active == false)
                    DeviceComunication.PageCommunicationsListPointer.Remove((DeviceComunication.PageCommunicationsListPointer.FirstOrDefault(x => (x.parentPage.uniqueID == pageUniqueIdToPlot))));
            }
            pageUniqueIdToPlot = 0;
            if (PageParameters.parameterListType == PageParameterList.ParameterListType.TAC)
            {
                PageParametersToPlot = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
                PageParameters.Active = false;
                foreach (var v in App.ViewModelLocator.MainViewModel.GoiParameterList)
                {
                    if (hiddenTACParametersDatalogging.Contains(v.PropertyName) && !v.SelectedForDatalogging)
                    {
                        //ungraphing those paremeters when they belongs to the list above
                        //and user did not select for datalogging at the same time
                        v.SelectedForInvisibleDatalogging = true;
                        OxyPlotPage.AddGraph(v.Name, false);
                        if (v.SubsetOfAddress == false)
                            PageParametersToPlot.parameterList.Add(v);
                    }
                    if (v.SelectedForDatalogging)
                    {
                        OxyPlotPage.AddGraph(v.Name, true);
                        if (v.SubsetOfAddress == false)
                            PageParametersToPlot.parameterList.Add(v);
                    }
                }
            }
            else
            {
                PageParametersToPlot = new PageParameterList(PageParameterList.ParameterListType.TSX, this);
                PageParameters.Active = false;
                foreach (var v in App.ViewModelLocator.MainViewModelTSX.GoiParameterList)
                {
                    if (hiddenTSXParametersDataLogging.Contains(v.PropertyName) && !v.SelectedForDatalogging)
                    {
                        v.SelectedForInvisibleDatalogging = true;
                        OxyPlotPage.AddGraph(v.Name, false);
                        if (v.SubsetOfAddress == false)
                            PageParametersToPlot.parameterList.Add(v);
                    }
                    if (v.SelectedForDatalogging)
                    {
                        OxyPlotPage.AddGraph(v.Name, true);
                        if (v.SubsetOfAddress == false)
                            PageParametersToPlot.parameterList.Add(v);
                    }
                }
            }

            if (PageParametersToPlot.parameterList.Count > 0)  // Prevents communication lockup if nothing is selected prior to starting datalog
                pageUniqueIdToPlot = App._devicecommunication.AddToPacketList(PageParametersToPlot);

            //TODO: originally fast packets are accopmplished by turning off all other page communications including 0(which is always gauge page)
            //var index = 0;
            //foreach (var x in DeviceComunication.PageCommunicationsListPointer)
            //{
            //    //if (index++ != 0)
            //    //    x.parentPage.Active = false;
            //    //else
            //        x.parentPage.Active = true;
            //}


            //TODO: The oxyplot navigation packets need to be removed somewhere, not like above when a new one is started
            //App._devicecommunication.SetUniqueIdToBeRemoved(indexOfThisPacket);
            // //wait for  it to be sent and removed
            //while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
            //    await Task.Delay(10);
            VehicleData vehicleData = new VehicleData();
            //await next so that logging does not have a huge time gap at the start
            //when this call can take a few seconds
            await vehicleData.SendVehicleInformationToDatabase();

            string lineItems = "Time, ";
            foreach (var v in PageParametersToPlot.parameterList)
            {
                if (v.SelectedForDatalogging || v.SelectedForInvisibleDatalogging)
                {
                    lineItems += v.Name + ", ";
                    if (v.PropertyName == "ORIENTATIONX" && !isOrientationEnabled)
                    {
                        GetOrientation();
                    }
                }
            }
            //OpenOrAppendOrCloseFIle(AppendedStreamPath, lineItems);
            AppendedStreamStartTime = ((double)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) / 1000.0f;

            System.Diagnostics.Debug.WriteLine("Pushing oxyplot page");
            await Navigation.PushAsync(OxyPlotPage);
            OxyPlotPage.ShowAllGraphs(6000); // 60 is the number of samples. 
            OxyPlotPage.StartTime = DateTime.Now;
            PageParametersToPlot.Active = true;
        }

        double AppendedStreamStartTime;

        public void AddPointsToGraphForPacket()
        {
            if (OxyPlotPage.HasGraphingStarted)
            {
                string lineItems = ((((double)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) / 1000.0f) - AppendedStreamStartTime).ToString() + ", ";
                foreach (var v in PageParametersToPlot.parameterList)
                {
                    if (v.SelectedForDatalogging || v.SelectedForInvisibleDatalogging)
                    {
                        OxyPlotPage.AddPointToGraph(v.Name, v.parameterValue);
                        lineItems += v.parameterValue.ToString() + ",";
                    }
                }
                //OpenOrAppendOrCloseFIle(AppendedStreamPath, lineItems);
            }
        }

        public void AddStepper(object sender, EventArgs e)
        {
            if ((sender as Grid).RowDefinitions.Count <= 1)
            {//just add a stepper once
                (sender as Grid).RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StackLayout rowContent = new StackLayout { Orientation = StackOrientation.Horizontal };
                (sender as Grid).Children.Add(rowContent, 0, 1);

                Button CustomDownButton10 = new Button
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "-10%"
                };

                if (Device.Idiom == TargetIdiom.Phone)
                {
                    CustomDownButton10.WidthRequest = 60;
                }
                rowContent.Children.Add(CustomDownButton10);
                CustomDownButton10.Clicked += (object s, EventArgs anotherEvent) =>
                { //this would be nice... async (s, e) =>
                    GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                    float fparameterValue = (float)parameter.parameterValue;
                    if (parameter.Address != -1)
                    {

                        var changeSize = parameter.parameterValue / 10.0D;
                        var newValue = parameter.parameterValue - changeSize;
                        //newValue = newValue > parameter.Maximum ? parameter.Maximum : newValue;
                        //newValue = newValue < parameter.Minimum ? parameter.Minimum : newValue;
                        //await parameter..SetValue(newValue); //Zero takes care of itself
                        SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)newValue, "never used so get rid of this");
                        QueParameter(SetParameterEventArgs);
                    }

                };
                Button CustomDownButton1 = new Button
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "-1%"
                };//ImageButton("-10%", "SmallGreenGlossyLongButton.png", 1);
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    CustomDownButton1.WidthRequest = 60;
                }
                rowContent.Children.Add(CustomDownButton1);
                CustomDownButton1.Clicked += (object s, EventArgs anotherEvent) =>
                { //this would be nice... async (s, e) =>
                    GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                    float fparameterValue = (float)parameter.parameterValue;
                    if (parameter.Address != -1)
                    {

                        var changeSize = parameter.parameterValue / 100.0D;
                        var newValue = parameter.parameterValue - changeSize;
                        //newValue = newValue > parameter.Maximum ? parameter.Maximum : newValue;
                        //newValue = newValue < parameter.Minimum ? parameter.Minimum : newValue;
                        //await parameter..SetValue(newValue); //Zero takes care of itself
                        SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)newValue, "never used so get rid of this");
                        QueParameter(SetParameterEventArgs);
                    }

                };
                Button CustomUpButton1 = new Button
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "+1%"
                };//ImageButton("-10%", "SmallGreenGlossyLongButton.png", 1);
                rowContent.Children.Add(CustomUpButton1);
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    CustomUpButton1.WidthRequest = 60;
                }
                CustomUpButton1.Clicked += (object s, EventArgs anotherEvent) =>
                { //this would be nice... async (s, e) =>
                    GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                    float fparameterValue = (float)parameter.parameterValue;
                    if (parameter.Address != -1)
                    {

                        var changeSize = parameter.parameterValue / 100.0D;
                        var newValue = parameter.parameterValue + changeSize;
                        //newValue = newValue > parameter.Maximum ? parameter.Maximum : newValue;
                        //newValue = newValue < parameter.Minimum ? parameter.Minimum : newValue;
                        //await parameter..SetValue(newValue); //Zero takes care of itself
                        SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)newValue, "never used so get rid of this");
                        QueParameter(SetParameterEventArgs);
                    }

                };
                Button CustomUpButton10 = new Button
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "+10%"
                };//ImageButton("-10%", "SmallGreenGlossyLongButton.png", 1);
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    CustomUpButton10.WidthRequest = 60;
                }
                rowContent.Children.Add(CustomUpButton10);
                CustomUpButton10.Clicked += (object s, EventArgs anotherEvent) =>
                { //this would be nice... async (s, e) =>
                    GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                    float fparameterValue = (float)parameter.parameterValue;
                    if (parameter.Address != -1)
                    {

                        var changeSize = parameter.parameterValue / 10.0D;
                        var newValue = parameter.parameterValue + changeSize;
                        //newValue = newValue > parameter.Maximum ? parameter.Maximum : newValue;
                        //newValue = newValue < parameter.Minimum ? parameter.Minimum : newValue;
                        //await parameter..SetValue(newValue); //Zero takes care of itself
                        SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)newValue, "never used so get rid of this");
                        QueParameter(SetParameterEventArgs);
                    }

                };
            }
        }

        public void AddSlider(object sender, EventArgs e)
        {
            if ((sender as Grid).RowDefinitions.Count <= 1)
            {//just add a slider once
                (sender as Grid).RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                StackLayout rowContent = new StackLayout { Orientation = StackOrientation.Horizontal };
                (sender as Grid).Children.Add(rowContent, 0, (sender as Grid).Children.Count);

                Xamarin.Forms.Slider slider = new Xamarin.Forms.Slider
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Maximum = ((sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter).MaximumParameterValue,
                    Minimum = 0, //don't complicate it with negative values which are few a far between
                    VerticalOptions = LayoutOptions.End,
                };
                slider.BindingContext = (sender as Xamarin.Forms.VisualElement).BindingContext;
                slider.SetBinding(Xamarin.Forms.Slider.ValueProperty, "parameterValue", BindingMode.OneWay);

                bool inhibitWrites = false;
                slider.ValueChanged += (s, args) =>
                {
                    //slider.Value ??
                    GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;

                    //bupdate and != stop from queuing up many writes in milliseconds
                    if (parameter.Address != -1 && parameter.bupdate && ((float)Math.Abs(parameter.parameterValue - args.NewValue) > 0.009999D) && !inhibitWrites)
                    {
                        parameter.bupdate = false;
                        SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)args.NewValue, "never used so get rid of this");
                        QueParameter(SetParameterEventArgs);

                        //Allow some time for reads to happen or else it is so busy writing that the text value never gets update
                        inhibitWrites = true;
                        Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
                        {
                            inhibitWrites = false;
                            return false; // True = Repeat again, False = Stop the timer
                        });
                    }
                };

                //if (Device.Idiom == TargetIdiom.Phone)
                //{
                //    CustomDownButton10.WidthRequest = 60;
                //}
                rowContent.Children.Add(
                  new Label
                  {
                      Text = slider.Minimum.ToString(),
                      HorizontalOptions = LayoutOptions.Start,
                      FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                      TextColor = Color.Black,
                      VerticalOptions = LayoutOptions.End,
                      HorizontalTextAlignment = TextAlignment.Start,
                  });

                rowContent.Children.Add(slider);
                rowContent.Children.Add(
                  new Label
                  {
                      Text = slider.Maximum.ToString(),
                      HorizontalOptions = LayoutOptions.End,
                      FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                      TextColor = Color.Black,
                      VerticalOptions = LayoutOptions.End,
                      HorizontalTextAlignment = TextAlignment.End,
                  });
            }
        }

        public void ShowMasterMenu(object sender, EventArgs e)
        {
            App._MainFlyoutPage.IsPresented = true;
        }

        public void ChangeOpenDescriptions(object sender, EventArgs e)
        {//This code depends too much on the xaml order so fix that in the future or don't change layout structure
            StackLayout parentViewOfPicker = ((sender as Xamarin.Forms.VisualElement).Parent as StackLayout);
            StackLayout parentViewOfPickerParent = ((sender as Xamarin.Forms.VisualElement).Parent as StackLayout);
            try
            {
                //parent.parent.parent... is very specific to the fact that this is only called by software revision picker
                foreach (Xamarin.Forms.VisualElement child in (parentViewOfPickerParent.Parent.Parent.Parent.Parent as StackLayout).Children) //all groups
                {
                    //Analog brake input now has 2 stacklayout
                    if ((child as StackLayout).Children.Count < 2)
                    {
                        if ((((child as StackLayout).Children[0] as StackLayout).Children[1] as StackLayout).IsVisible)
                        {
                            FillInDescriptions(((child as StackLayout).Children[0] as StackLayout).Children[0] as StackLayout, e);
                        }
                    }
                    else if ((child is StackLayout) && ((child as StackLayout).Children[1] as StackLayout).IsVisible) //first help label is visible, skip other children types
                    {
                        //close it and reopen to change it.......... why not
                        bool reExpandMoreTroubleShooting = false;

                        if ((((child as StackLayout).Children[2] as Frame).Children[0] as StackLayout).Children.Count > 2)
                        {//Has a More Trouble Shooting Label so see if it is expanded or not
                         //check for the places where similar stuff was grouped in a frame with the last one having help
                            int subGroupCount = ((child as StackLayout).Children[2] as Frame).Children.Count;
                            Label moreTroubleShootingLabel = (((child as StackLayout).Children[2] as Frame).Children[subGroupCount - 1] as StackLayout).Children[2] as Label;
                            if (!moreTroubleShootingLabel.IsVisible) //then image and stuff is visible so toggle it too
                                reExpandMoreTroubleShooting = true;
                        }

                        FillInDescriptions(((child as StackLayout).Children[0] as StackLayout), e); //send the guy who would have the FillInDescriptions tap gesture

                        if (reExpandMoreTroubleShooting)
                        {//get the new moreTroubleShootingLabel, but this one might not have one
                            int subGroupCount = ((child as StackLayout).Children[2] as Frame).Children.Count;
                            if ((((child as StackLayout).Children[2] as Frame).Children[0] as StackLayout).Children.Count > 2)
                            {//Has a More Trouble Shooting Label so see if it is expanded or not
                             //check for the places where similar stuff was grouped in a frame with the last one having help
                                Label moreTroubleShootingLabel = (((child as StackLayout).Children[2] as Frame).Children[subGroupCount - 1] as StackLayout).Children[2] as Label;
                                ExpandCollapseTroubleshooting(moreTroubleShootingLabel, e);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        public async void FillInDescriptions(object sender, EventArgs e)
        {//This code depends too much on the xaml order so fix that in the future
            var thisElement = sender as Xamarin.Forms.VisualElement;
            var parentView = thisElement.Parent as StackLayout; //it better be
            if (parentView.Parent != null && parentView.Parent.Parent != null && parentView.Parent.Parent.GetType() == typeof(Xamarin.Forms.ScrollView))
                (parentView.Parent.Parent as Xamarin.Forms.ScrollView).ScrollToAsync(thisElement, ScrollToPosition.Start, true);
            if (thisElement.GetType() == typeof(StackLayout) && (thisElement as StackLayout).Children.Count > 1)
            {//then this element has more than just a lable, it must have information image and a down arrow image
                try
                {
                    var descriptionOfParameterGroup = parentView.Children[1];
                    descriptionOfParameterGroup.IsVisible = !descriptionOfParameterGroup.IsVisible; //toggle group description

                    var openImage = (thisElement as StackLayout).Children[2]; //toggle down arrow image
                    openImage.IsVisible = !openImage.IsVisible;

                    var collapseImage = (thisElement as StackLayout).Children[3]; //toggle down arrow image
                    collapseImage.IsVisible = !collapseImage.IsVisible;
                }
                catch (Exception ex)
                {
                    //FIX this 
                }
            }

            var b = parentView.Children;
            foreach (Xamarin.Forms.VisualElement frameOrStacklayout in b)
            {
                if (frameOrStacklayout is Frame)
                {
                    await FillInAFramedDescription(frameOrStacklayout as Frame);
                }
            }
        }

        async Task FillInAFramedDescription(Frame frame)
        {
            int countOfCoupledStackLayouts = 0;

            StackLayout frameChildStackLayout = frame.Children[0] as StackLayout;

            //More hacking to get the job done
            //I wanted one help description for two inputs, such as Forward and Reverse switches
            //looks nicer and makes a little more sense to describe them once
            //so I put to stacklayouts in the frame stacklayout
            //now I want the help to show up below the last of the group
            //so count the extra children to find the last parameter which contains a description
            //that applies to both, I call them coupled
            foreach (var element in frameChildStackLayout.Children)
            {
                if (element is StackLayout)
                    countOfCoupledStackLayouts++;
            }

            GoiParameter parameter;
            if (countOfCoupledStackLayouts > 1)
                parameter = frameChildStackLayout.Children[countOfCoupledStackLayouts - 1].BindingContext as GoiParameter;
            else
                parameter = frameChildStackLayout.BindingContext as GoiParameter;

            //First find the image matching this model
            //.....
            //then stick in description pieces
            if (frameChildStackLayout.Children.Count > countOfCoupledStackLayouts)
            {
                if (!(frameChildStackLayout.Children[0] is Stepper))
                {
                    for (var index = frameChildStackLayout.Children.Count - 1; index >= countOfCoupledStackLayouts; index--) //keep child 0
                        frameChildStackLayout.Children.Remove(frameChildStackLayout.Children[index]);
                }
            }
            else
            {
                var blockTextType = TextType.Text;

                if (parameter.Description != null && parameter.Description != "")
                {

                    if (parameter.Description.Contains("<"))
                    {//only CDATA (a way of embedding html into xml> can get "<" into a xml string
                        blockTextType = TextType.Html; //give it some formatting
                    }

                    List<string> wholeDescription = parameter.Description.Split('\n').ToList<string>(); //container for parsed parameter.Description text

                    GetDescriptionFromBinding(ref wholeDescription);

                    //firt appropriate description was found but nothing after it has been removed, so remove it
                    RemoveAfterPart(ref wholeDescription);

                    RemoveSpaceBeforeAndAfter(ref wholeDescription);

                    string parsedDescription = String.Join("\n", wholeDescription.ToArray());

                    parsedDescription = await ReplaceFormatData(parsedDescription);

                    //Testing is easier at http://regexstorm.net/tester
                    //The below regex patterns work

                    //PinchToZoomContainer harness = new PinchToZoomContainer(); //harness image stuffed below
                    Xamarin.Forms.ScrollView harness = new Xamarin.Forms.ScrollView { Orientation = ScrollOrientation.Horizontal };

                    //remove everthing from Troubleshooting and after, ignore \n's
                    var dText = Regex.Replace(parsedDescription, @"(\n)*Troubleshooting(.|\n)*", "");

                    if (dText.Contains("(Images:"))
                    {
                        ImageProcessing(ref dText, harness);
                    }

                    AddPropertyDescription(blockTextType, frameChildStackLayout, dText);
                    AddMoreTroubleshootingSection(blockTextType, frameChildStackLayout, parsedDescription, harness);
                }
            }
            ToggleHelperVisibility(frameChildStackLayout);
        }


        public void ExpandCollapseTroubleshooting(object sender, EventArgs e)
        {//This code depends too much on the xaml order so fix that in the future
            var parentView = ((sender as Xamarin.Forms.VisualElement).Parent as StackLayout);
            var parameter = parentView.BindingContext as GoiParameter;
            var b = parentView.Children;
            foreach (Xamarin.Forms.VisualElement element in b)
            {
                if ((element is Label) && (element as Label).Text.Contains("More Troubleshooting >"))
                    element.IsVisible = !element.IsVisible;
                if (element is Label && ((element as Label).Text.Contains("Troubleshooting Notes:") || (element as Label).Text.Contains("Causes:")))
                {
                    element.IsVisible = !element.IsVisible;
                }
                if (element is Xamarin.Forms.ScrollView)
                {
                    element.IsVisible = !element.IsVisible;
                }
            }
        }

        public void SelectForDataLogging(object sender, EventArgs e)
        {
            //if (savedBackGroundColour != Color.Default)
            //    savedBackGroundColour = sender.BackgroundColor;
            GoiParameter parameter = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
            if (parameter.SelectedForDatalogging)
            {
                parameter.SelectedForDatalogging = false;
                (sender as Xamarin.Forms.VisualElement).BackgroundColor = Color.Default;
            }
            else
            {
                parameter.SelectedForDatalogging = true;
                (sender as Xamarin.Forms.VisualElement).BackgroundColor = Color.Silver;
            }

        }

        public static readonly BindableProperty PageIsBusyProperty = BindableProperty.Create(nameof(PageIsBusy),
            typeof(bool),
            typeof(NavitasGeneralPage),
            false);

        /// <summary>
        ///    PageIsBusy summary. This is a bindable property.
        /// </summary>
        public bool PageIsBusy
        {
            get { return (bool)GetValue(PageIsBusyProperty); }
            //couldn't get property binding to work so....
            set
            {
                SetValue(PageIsBusyProperty, value);
                disableBackgroundOverlay.IsVisible = value;
                activityIndicator.IsVisible = value;
                activityMessage.IsVisible = value;
                activityIndicator.IsRunning = value;
                if (value)
                {
                    (this.Content as AbsoluteLayout).RaiseChild(activityIndicator);
                    (this.Content as AbsoluteLayout).RaiseChild(activityMessage);
                }
                else
                {
                    (this.Content as AbsoluteLayout).LowerChild(activityMessage);
                    (this.Content as AbsoluteLayout).LowerChild(activityIndicator);

                }
            }
        }

        ActivityIndicator activityIndicator;
        public Label activityMessage;
        public Image errorIcon;

        ContentView disableBackgroundOverlay;
        public void AddActivityPopUp()
        {
            AbsoluteLayout overlayArea = this.Content as AbsoluteLayout;

            disableBackgroundOverlay = new ContentView
            {
                IsVisible = false,
                BackgroundColor = Color.FromHex("#C0202020"),
            };

            AbsoluteLayout.SetLayoutFlags(disableBackgroundOverlay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(disableBackgroundOverlay, new Rectangle(0, 0, 1, 1));

            Size size = Device.Info.PixelScreenSize;
            var screenBoxSize = (float)Math.Min(size.Height, size.Width);
            var scaleSinceIOSDoesNotSizeWithHorWRequests = screenBoxSize / 30 / 5;

            activityIndicator = new ActivityIndicator
            {
                IsEnabled = true,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                //Use the same color as the school text color as it should contrast the background the same.
                Color = Color.DodgerBlue,
                IsVisible = false,
                //HeightRequest = screenBoxSize,
                //WidthRequest = screenBoxSize,
                Scale = scaleSinceIOSDoesNotSizeWithHorWRequests
            };

            activityMessage = new Label
            {
                IsEnabled = true,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                TextColor = Color.DodgerBlue,
                Text = "Saving...",
                IsVisible = false,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                LineBreakMode = LineBreakMode.WordWrap,
                MaxLines = 2
            };

            //I still don't have a full grip on binding
            //except I know that binding visual elements always gets down to everybody implementing INotifyPropertyChanged
            activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "PageIsBusyProperty");
            //activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "PageIsBusyProperty");
            //activityMessage.SetBinding(Label.IsVisibleProperty, "PageIsBusyProperty");

            AbsoluteLayout.SetLayoutFlags(activityMessage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(activityMessage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(activityIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(activityIndicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            overlayArea.Children.Add(disableBackgroundOverlay);
            overlayArea.Children.Add(activityMessage);
            overlayArea.Children.Add(activityIndicator);

            overlayArea.LowerChild(activityIndicator);
            overlayArea.LowerChild(activityMessage);
        }

        ContentView popupWarningGeneral;

        public void BuildWarningPopUp(string typeOfWarning, GoiParameter parameter = null, float InputParameterValue = 0)
        {
            float value = 0;

            if (!(App._MainFlyoutPage.hasUserBeenVerifiedByOEM || App._MainFlyoutPage.isMemberApprovalRequired))
            {
                AbsoluteLayout overlayArea = this.Content as AbsoluteLayout;

                Button btnDecline = new Button()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Decline"
                };

                Button btnAccept = new Button()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Accept"
                };
                string warningTitle = "";
                string warningDescription = "";

                if (typeOfWarning == "BatteryLimit")
                {
                    warningTitle = "200 Amp Battery Current Limit Exceeded";
                    warningDescription = "Increasing the battery current limit could cause damage to your batteries. \n" +
                        "By pressing accept you are accepting all risk and liabilities \n";
                    btnDecline.Clicked += OnDeclineBatteryCurrentButtonClicked;
                    btnAccept.Clicked += async (sender, agrs) =>
                    {
                        popupWarningGeneral.IsVisible = false;
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("IBATLIMIT").Address, (float)InputParameterValue, "Battery Discharge Limit (A)"));
                    };
                }
                else if (typeOfWarning == "Disclaimer")
                {
                    warningTitle = "Disclaimer";
                    warningDescription = "By changing settings in the APP you are accepting the liabilities of making any changes that can result in personal harm as wel as that can result in early failure of the Navitas controller and/or other vehicle components.\n\n" +
                        "Navitas is not liable for any damage resulting from changes to settings";

                    btnDecline.Clicked += (sender, args) => popupWarningGeneral.IsVisible = false;
                }
                else if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    value = (App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue * InputParameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2);
                    var Value20MPH = (App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue * 20.0f) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2);
                    var Value25MPH = (App.ViewModelLocator.GetParameter("REARAXLERATIO").parameterValue * 25.0f) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameter("TIREDIAMETER").parameterValue / 2);

                    if ((parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH") || (parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH"))
                    {
                        var CurrrentValue = parameter.parameterValue;
                        if (parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH") CurrrentValue /= 1.609344f;

                        if (InputParameterValue >= 25)
                        {
                            warningTitle = "25 MPH(40 KPH) Limit Exceeded";
                            warningDescription = "This product is for Golf cars and Low Speed Vehicle (LSV) applications.\n" +
                                "Any modifications (App/Mechanical) can result in speeds in excess of Manufacturers recommended limits for safe vehicle operation,  State/Province/Territory/local Laws.\n" +
                                "Please use caution when trying to obtain speeds in excess of 25MPH(40.23KPH). \n" +
                                "By pressing accept you are accepting all risk and liabilities \n";

                            if (CurrrentValue > 25)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)Value25MPH, "Fwd Lmt RPM"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else if (InputParameterValue >= 20)
                        {
                            warningTitle = "20 MPH(32 KPH) Limit Exceeded";
                            warningDescription = "This speed setting exceeds golf cart regulations.  Ensure that your vehicle meets all LSV requirements for your jurisdiction.\n";

                            if (CurrrentValue > 20)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)Value20MPH, "Fwd Lmt RPM"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)value, "Fwd Lmt RPM"));
                        }
                        btnAccept.Clicked += async (sender, agrs) =>
                        {
                            popupWarningGeneral.IsVisible = false;
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("FWDLMTRPM").Address, (float)value, "Fwd Lmt RPM"));
                        };
                    }
                    else if ((parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH") || (parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH"))
                    {
                        var CurrrentValue = parameter.parameterValue;
                        if (parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH") CurrrentValue /= 1.609344f;
                        if (InputParameterValue >= 25)
                        {
                            warningTitle = "25 MPH (40 KPH) Limit Exceeded";
                            warningDescription = "This product is for Golf cars and Low Speed Vehicle (LSV) applications.\n" +
                                "Any modifications (App/Mechanical) can result in speeds in excess of Manufacturers recommended limits for safe vehicle operation,  State/Province/Territory/local Laws.\n" +
                                "Please use caution when trying to obtain speeds in excess of 25MPH((40.23 KPH)). \n" +
                                "By pressing accept you are accepting all risk and liabilities \n";

                            if (CurrrentValue > 25)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)Value25MPH, "Rvs Lmt RPM"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else if (InputParameterValue >= 20)
                        {
                            warningTitle = "20 MPH (32 KPH) Limit Exceeded";
                            warningDescription = "This speed setting exceeds golf cart regulations.  Ensure that your vehicle meets all LSV requirements for your jurisdiction.\n";

                            if (CurrrentValue > 20)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)Value20MPH, "Rvs Lmt RPM"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)value, "Rvs Lmt RPM"));
                        }
                        btnAccept.Clicked += async (sender, agrs) =>
                        {
                            popupWarningGeneral.IsVisible = false;
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("RVSLMTRPM").Address, (float)value, "Rvs Lmt RPM"));
                        };
                    }
                }
                else if (ControllerTypeLocator.ControllerType == "TSX")
                {
                    value = (App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * InputParameterValue) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2);
                    var Value20MPH = (App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * 20.0) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2);
                    var Value25MPH = (App.ViewModelLocator.GetParameterTSX("REARAXLERATIO").parameterValue * 25.0) / ((2 * (float)Math.PI * 60 / 5280 / 12) * App.ViewModelLocator.GetParameterTSX("TIREDIAMETER").parameterValue / 2);

                    if ((parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_MPH") || (parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH"))
                    {
                        var CurrrentValue = parameter.parameterValue;
                        if (parameter.PropertyName == "PAR_MAX_MOTOR_FWD_SPEED_LIMIT_KPH") CurrrentValue /= 1.609344f;

                        if (InputParameterValue >= 25)
                        {
                            warningTitle = "25 MPH(40 KPH) Limit Exceeded";
                            warningDescription = "This product is for Golf cars and Low Speed Vehicle (LSV) applications.\n" +
                                "Any modifications (App/Mechanical) can result in speeds in excess of Manufacturers recommended limits for safe vehicle operation,  State/Province/Territory/local Laws.\n" +
                                "Please use caution when trying to obtain speeds in excess of 25MPH(40.23KPH). \n" +
                                "By pressing accept you are accepting all risk and liabilities \n";

                            if (CurrrentValue > 25)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)Value25MPH, "PARMAXMOTORFWDSPEEDLIMIT"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else if (InputParameterValue >= 20)
                        {
                            warningTitle = "20 MPH(32 KPH) Limit Exceeded";
                            warningDescription = "This speed setting exceeds golf cart regulations.  Ensure that your vehicle meets all LSV requirements for your jurisdiction.\n";

                            if (CurrrentValue > 20)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)Value20MPH, "PARMAXMOTORFWDSPEEDLIMIT"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)value, "PARMAXMOTORFWDSPEEDLIMIT"));
                        }
                        btnAccept.Clicked += async (sender, agrs) =>
                        {
                            popupWarningGeneral.IsVisible = false;
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORFWDSPEEDLIMIT").Address, (float)value, "PARMAXMOTORFWDSPEEDLIMIT"));
                        };
                    }
                    else if ((parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_MPH") || (parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH"))
                    {
                        var CurrrentValue = parameter.parameterValue;
                        if (parameter.PropertyName == "PAR_MAX_MOTOR_REV_SPEED_LIMIT_KPH") CurrrentValue /= 1.609344f;
                        if (InputParameterValue >= 25)
                        {
                            warningTitle = "25 MPH (40 KPH) Limit Exceeded";
                            warningDescription = "This product is for Golf cars and Low Speed Vehicle (LSV) applications.\n" +
                                "Any modifications (App/Mechanical) can result in speeds in excess of Manufacturers recommended limits for safe vehicle operation,  State/Province/Territory/local Laws.\n" +
                                "Please use caution when trying to obtain speeds in excess of 25MPH((40.23 KPH)). \n" +
                                "By pressing accept you are accepting all risk and liabilities \n";

                            if (CurrrentValue > 25)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)Value25MPH, "PARMAXMOTORREVSPEEDLIMIT"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else if (InputParameterValue >= 20)
                        {
                            warningTitle = "20 MPH (32 KPH) Limit Exceeded";
                            warningDescription = "This speed setting exceeds golf cart regulations.  Ensure that your vehicle meets all LSV requirements for your jurisdiction.\n";

                            if (CurrrentValue > 20)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)Value20MPH, "PARMAXMOTORREVSPEEDLIMIT"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;
                        }
                        else if (InputParameterValue >= 20)
                        {
                            warningTitle = "20 MPH (32 KPH) Limit Exceeded";
                            warningDescription = "This speed setting exceeds golf cart regulations.  Ensure that your vehicle meets all LSV requirements for your jurisdiction.\n";

                            if (CurrrentValue > 20)
                                btnDecline.Clicked += (sender, EventArgs) =>
                                {
                                    popupWarningGeneral.IsVisible = false;
                                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)Value20MPH, "PARMAXMOTORREVSPEEDLIMIT"));
                                };
                            else btnDecline.Clicked += async (sender, agrs) => popupWarningGeneral.IsVisible = false;

                        }
                        else
                        {
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)value, "PARMAXMOTORREVSPEEDLIMIT"));
                        }
                        btnAccept.Clicked += async (sender, agrs) =>
                        {
                            popupWarningGeneral.IsVisible = false;
                            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameterTSX("PARMAXMOTORREVSPEEDLIMIT").Address, (float)value, "PARMAXMOTORREVSPEEDLIMIT"));

                        };
                    }
                }

                if (warningTitle != "")
                {
                    popupWarningGeneral = new ContentView()
                    {
                        IsVisible = true,
                        BackgroundColor = Color.FromHex("#C0202020"),
                        Padding = new Thickness(10, 0)
                    };

                    AbsoluteLayout.SetLayoutFlags(popupWarningGeneral, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(popupWarningGeneral, new Rectangle(0, 0, 1, 1));

                    Xamarin.Forms.ScrollView popupScrollView = new Xamarin.Forms.ScrollView()
                    {
                        Orientation = ScrollOrientation.Vertical,
                    };

                    AbsoluteLayout.SetLayoutFlags(popupScrollView, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(popupScrollView, new Rectangle(.25, .12, .5, .76));

                    StackLayout popupStackLayout = new StackLayout()
                    {
                        BackgroundColor = Color.FromHex("#C2C3C6"),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = new Thickness(10),
                        Children = {
                                new Image()
                                {
                                    Source = "popupSpeedWarningImage.png",
                                    Scale = 0.5
                                },
                                new Label()
                                {
                                    TextColor = Color.Black,
                                    HorizontalOptions = LayoutOptions.Center,
                                    FontAttributes = FontAttributes.Bold,
                                    Text = warningTitle
                                },
                                new Label()
                                {
                                    TextColor = Color.Black,
                                    HorizontalOptions = LayoutOptions.Fill,
                                    Text = warningDescription
                                },

                        }
                    };

                    StackLayout buttonStackLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };

                    if (typeOfWarning != "Disclaimer")
                    {
                        btnAccept.Clicked += async (sender, agrs) =>
                        {
                            try
                            {
                                popupWarningGeneral.IsVisible = false;
                                if (!(App._MainFlyoutPage._DeviceListPage._device is DemoDevice))
                                {
                                    if (typeOfWarning == "BatteryLimit")
                                    {
                                        await App.ParseManagerAdapter.SaveMaxBatteryCurrentRecord(App.PresentConnectedController, Math.Round(InputParameterValue, 2));
                                    }
                                    else
                                    {
                                        await App.ParseManagerAdapter.SaveMaxSpeedRecord(App.PresentConnectedController, Math.Round(InputParameterValue, 2));
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                if (e.Message.Contains("Object reference not set to an instance of an object.") &&
                                Connectivity.NetworkAccess != NetworkAccess.Internet)
                                {
                                    await DisplayAlert("Connection Error", "Navitas requires an internet connection to allow the change.\n" +
                                        "Please check your internet connection and try again", "Ok");
                                }
                                else
                                    System.Diagnostics.Debug.WriteLine($"Accept speed limit exceeded Exception: {e.Message}");
                            }

                        };
                    }
                    else
                    {
                        (popupStackLayout.Children[0] as Image).IsVisible = false;
                        (popupStackLayout.Children[1] as Label).TextColor = Color.Red;
                        (popupStackLayout.Children[1] as Label).FontSize = 20;
                        btnAccept.Clicked += (async (sender, agrs) =>
                        {
                            popupWarningGeneral.IsVisible = false;
                            await Navigation.PushAsync(new AdvancedRequestAccessPage());
                        });
                    }

                    buttonStackLayout.Children.Add(btnAccept);
                    buttonStackLayout.Children.Add(btnDecline);

                    popupStackLayout.Children.Add(buttonStackLayout);

                    popupScrollView.Content = popupStackLayout;
                    popupWarningGeneral.Content = popupScrollView;
                    overlayArea.Children.Add(popupWarningGeneral);
                }
            }
        }

        protected void OnDeclineBatteryCurrentButtonClicked(object sender, EventArgs args)
        {
            popupWarningGeneral.IsVisible = false;
            float value = 200;
            QueParameter(new SetParameterEventArgs(43, (float)value, "Ibatlimit"));
        }
        public void BuildErrorIcon()
        {
            AbsoluteLayout pageAbsoluteLayout = this.Content as AbsoluteLayout;

            errorIcon = new Image
            {
                Source = "IconError.png",
                Margin = new Thickness(0, 0, 0, 0),
                Scale = 2,
                IsVisible = false
            };

            AbsoluteLayout.SetLayoutBounds(errorIcon, new Rectangle(0.5, 0.5, 100, 100));
            AbsoluteLayout.SetLayoutFlags(errorIcon, AbsoluteLayoutFlags.PositionProportional);

            pageAbsoluteLayout.Children.Add(errorIcon);
        }
        void AnimateErrorIcon()
        {
            double currentPageHeight = Xamarin.Forms.Application.Current.MainPage.Height;
            double currentPageWidth = Xamarin.Forms.Application.Current.MainPage.Width;
            Device.BeginInvokeOnMainThread(async () =>
            {
                errorIcon.IsVisible = true;
                if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                {
                    errorIcon.ScaleTo(0.43, 500, Easing.CubicInOut);
                    if (currentPageHeight / currentPageWidth > 1) //Potrait
                    {
                        errorIcon.TranslateTo(18, -currentPageHeight * 0.5 * 0.885, 500, Easing.CubicInOut);
                    }
                    else //Landscape
                    {
                        errorIcon.TranslateTo(18, -currentPageHeight * 0.5 * 0.8, 500, Easing.CubicInOut);
                    }
                }
                else if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
                {
                    errorIcon.ScaleTo(0.4, 500, Easing.CubicInOut);
                    if (currentPageHeight / currentPageWidth > 1) //Potrait
                    {
                        errorIcon.TranslateTo(18, -currentPageHeight * 0.5 * 0.8, 500, Easing.CubicInOut);
                    }
                    else //Landscape
                    {
                        errorIcon.TranslateTo(14, -currentPageHeight * 0.5 * 0.7, 500, Easing.CubicInOut);
                    }
                }
                await Task.Delay(500);
                errorIcon.IsVisible = false;
                errorIcon.ScaleTo(2, 50, null);
                errorIcon.TranslateTo(0, 0, 50, null);
            });
        }
        public void BuildFaultWarningMessagesBox(string controllerType, Xamarin.Forms.ScrollView faultWarningScrollView, List<Frame> frameFaults)
        {
            try
            {
                AbsoluteLayout overlayLayout = Content as AbsoluteLayout;

                StackLayout faultWarningContainer = new StackLayout();

                StackLayout errorIconAndTextContainer = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center
                };

                Image errorIcon = new Image
                {
                    Source = "IconError.png",
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 35
                };

                Label faultText = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = Color.Black,
                    FontAttributes = FontAttributes.Bold,
                    Text = "Faults"
                };

                errorIconAndTextContainer.Children.Add(errorIcon);
                errorIconAndTextContainer.Children.Add(faultText);

                Label faultWarningStatus = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    TextColor = Color.Black,
                    Padding = new Thickness(0, 10, 0, 0),
                    Text = ""
                };

                StackLayout buttonContainer = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Orientation = StackOrientation.Horizontal
                };

                Button btnCancel = BuildButton("Cancel");
                Button btnMail = BuildButton("Mail");
                Button btnIgnore = BuildButton("Ignore", false);
                Button btnRestore = BuildButton("Restore", false);

                try
                {
                    for (int i = 0; i < frameFaults.Count; i++)
                    {
                        if (frameFaults[i].Content is StackLayout) //Fault
                        {
                            if ((frameFaults[i].BindingContext as GoiParameter).Description.ToLower().Contains("press ignore"))
                            {
                                if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= 7.600)
                                {
                                    btnIgnore.IsVisible = true;
                                    ((frameFaults[i].Content as StackLayout).Children[0] as StackLayout).Children.Add(btnIgnore);
                                }
                            }
                        }
                        if ((frameFaults[i].BindingContext as GoiParameter).PropertyName == "ParameterTableInvalidGroupOneFault")
                        {
                            btnRestore.IsVisible = true;
                            ((frameFaults[i].Content as StackLayout).Children[0] as StackLayout).Children.Add(btnRestore);
                        }

                    }
                }
                catch (Exception ex)
                {
                    // Some other exception occurred
                    System.Diagnostics.Debug.WriteLine("Some other exception occurred" + ex.Message.ToString());
                }

                btnMail.Clicked += (sender, args) =>
                {
                    string faultWarningCollector,
                            faultLabel,
                            faultDescription,
                            warningDescription;

                    faultWarningCollector = faultLabel = faultDescription = warningDescription = "";

                    try
                    {
                        for (int i = 0; i < frameFaults.Count; i++)
                        {

                            if (frameFaults[i].Content is StackLayout) //Fault
                            {
                                faultLabel = (((frameFaults[i].Content as StackLayout).Children[0] as StackLayout).Children[0] as Label).Text + "<br />";
                                faultDescription = Regex.Replace(((frameFaults[i].Content as StackLayout).Children[1] as Label).Text, "<[p][^>]*>", "<p>");

                                faultWarningCollector += (faultLabel + faultDescription) + "<br />";
                            }
                            else //Warning
                            {
                                warningDescription = (frameFaults[i].Content as Label).Text;
                                faultWarningCollector += warningDescription;
                            }
                        }
                        FileManager.SendEmailWithOptionalAttachment(faultWarningCollector);
                    }
                    catch (Exception ex)
                    {
                        // Some other exception occurred
                        System.Diagnostics.Debug.WriteLine("Some other exception occurred" + ex.Message.ToString());
                    }
                };

                Frame faultWarningFrame = new Frame
                {
                    CornerRadius = 10,
                    BackgroundColor = Color.FromHex("#C2C3C6"),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HasShadow = true,
                    Content = faultWarningContainer,
                    Margin = new Thickness(10, 20, 10, 20), //so that a big one will not take up the whole page
                };

                ContentView popupFaultWarning = new ContentView
                {
                    BackgroundColor = Color.FromHex("#C0202020"),
                    Content = faultWarningFrame,
                };

                if (controllerType == "TSX")
                {
                    Button btnClearErrors = BuildButton("Clear Errors");
                    btnClearErrors.Clicked += (sender, args) =>
                    {
                        QueParameter(new SetParameterEventArgs(182, 0.0f, null));
                        QueParameter(new SetParameterEventArgs(183, 0.0f, null));
                        QueParameter(new SetParameterEventArgs(184, 0.0f, null));

                        AnimateErrorIcon();

                        MessagingCenter.Send(this, "AnimateTapWord");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            overlayLayout.Children.Remove(popupFaultWarning);
                            displayFaultWarningMessageBoxIsbusy = false;
                        });
                    };

                    buttonContainer.Children.Add(btnClearErrors);
                }

                buttonContainer.Children.Add(btnCancel);
                buttonContainer.Children.Add(btnMail);

                faultWarningContainer.Children.Add(errorIconAndTextContainer);
                faultWarningContainer.Children.Add(faultWarningScrollView);
                faultWarningContainer.Children.Add(buttonContainer);

                AbsoluteLayout.SetLayoutBounds(popupFaultWarning, new Rectangle(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(popupFaultWarning, AbsoluteLayoutFlags.All);

                overlayLayout.Children.Add(popupFaultWarning);

                btnRestore.Clicked += async (sender, args) =>
                {
                    AnimateErrorIcon();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        overlayLayout.Children.Remove(popupFaultWarning);
                        displayFaultWarningMessageBoxIsbusy = false;
                    });
                    App.isSkipAuthorization = true;

                    QueParameter(new SetParameterEventArgs(49, 1.0f, null));
                    await ReadVEEPROM(true);

                    Task.Delay(2000).Wait();
                    QueParameter(new SetParameterEventArgs(50, 1.0f, null));
                    App.isSkipAuthorization = false;
                };
                btnIgnore.Clicked += (sender, args) =>
                {

                    string faultName = "";
                    int value = 0;

                    for (int i = 0; i < frameFaults.Count; i++)
                    {
                        if (frameFaults[i].Content is StackLayout) //Fault
                        {

                            faultName = (frameFaults[i].BindingContext as GoiParameter).Name;
                            GoiParameter faultmask = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Name == (faultName + " Mask"));

                            if (faultmask != null)
                            {
                                value |= (1 << faultmask.BitRangeStart);
                            }
                        }
                    }
                    if (value != 0)
                    {
                        QueParameter(new SetParameterEventArgs(586, value, "FaultOverRideMask"));
                    }
                    AnimateErrorIcon();
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        overlayLayout.Children.Remove(popupFaultWarning);
                        displayFaultWarningMessageBoxIsbusy = false;
                    });
                };

                btnCancel.Clicked += (sender, args) =>
                {
                    AnimateErrorIcon();
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        MessagingCenter.Send(this, "AnimateTapWord");
                    }
                    else
                    {
                        MessagingCenter.Send(this, "AnimateTapWord");
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        overlayLayout.Children.Remove(popupFaultWarning);
                        displayFaultWarningMessageBoxIsbusy = false;
                    });
                };

            }
            catch (Exception e)
            {
                DisplayAlert("BuildFaultWarningMessagesBox Error: ", e.ToString(), "Close");
            }
        }
        public Image DemoFlash;
        public void BuildDemoFlashing()
        {
            DemoFlash = new Image()
            {
                Source = "DemoWM.png",
                Style = (Style)Xamarin.Forms.Application.Current.Resources["watermarkStyle"]
            };

            DemoFlash.SetBinding(IsVisibleProperty, "IsDemoMode");
            (this.Content as AbsoluteLayout).Children.Add(DemoFlash);
        }

        bool displayFaultWarningMessageBoxIsbusy = false; //only one popup at a time
        async Task DisplayFaultWarningMessageBox(string controllerType, List<Frame> frameFaults)
        {
            try
            {
                if (!displayFaultWarningMessageBoxIsbusy)
                {
                    displayFaultWarningMessageBoxIsbusy = true;
                    StackLayout stacklayout = new StackLayout();

                    for (int i = 0; i < frameFaults.Count; i++)
                    {
                        if (frameFaults[i].Content is StackLayout) //Fault
                        {
                            stacklayout.Children.Add(frameFaults[i]);
                            await FillInAFramedDescription(frameFaults[i]);

                            string faultName = (frameFaults[i].BindingContext as GoiParameter).Name.Replace(" ", "");
                            string helpFileName = faultName + $".{ControllerTypeLocator.ControllerType}help";

                            Button btnHelp = BuildButton("Help", false);
                            if (FileManager.GetExternalFirstOrInternalDirectoryFilesContaining(helpFileName).Length != 0)
                            {
                                btnHelp.IsVisible = true;
                                btnHelp.Clicked += (sender, args) =>
                                {
                                    DisplayHelper(helpFileName);
                                };

                            }

                            ((frameFaults[i].Content as StackLayout).Children[0] as StackLayout).Children.Add(btnHelp);
                        }
                        else //Warning
                        {
                            stacklayout.Children.Add(frameFaults[i]);
                        }
                    }

                    Xamarin.Forms.ScrollView scrollView = new Xamarin.Forms.ScrollView
                    {
                        Content = stacklayout, //fill in UI
                    };
                    BuildFaultWarningMessagesBox(controllerType, scrollView, frameFaults);
                }
            }
            catch (Exception e)
            {
                DisplayAlert("DisplayFaultWarningMessage Error: ", e.ToString(), "Close");
            }
        }
        void DisplayHelper(string fileName)
        {
            double currentPageHeight = Xamarin.Forms.Application.Current.MainPage.Height;
            double currentPageWidth = Xamarin.Forms.Application.Current.MainPage.Width;
            try
            {
                AbsoluteLayout overlayLayout = this.Content as AbsoluteLayout;

                StackLayout scrollViewAndButtonContainerStackLayout = new StackLayout();
                StackLayout frameParentStackLayout = new StackLayout();
                Xamarin.Forms.ScrollView faultWarningScrollView = new Xamarin.Forms.ScrollView()
                {
                    Margin = new Thickness(0)
                };
                faultWarningScrollView.Content = frameParentStackLayout;

                Frame faultWarningListFrame = new Frame()
                {
                    CornerRadius = 10,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Color.White,
                    HasShadow = true,
                    Content = scrollViewAndButtonContainerStackLayout,
                };

                ContentView helpContentView = new ContentView
                {
                    BackgroundColor = Color.FromHex("#C0202020"),
                    Content = faultWarningListFrame
                };

                if (frameParentStackLayout.Children.Count == 0)
                {
                    PageViewModel pageDescriptions = FileManager.GetDeserializedObject<PageViewModel>($"{ControllerTypeLocator.ControllerType}.{fileName}");

                    faultWarningScrollView = BuildScrollView(frameParentStackLayout, faultWarningScrollView, pageDescriptions);

                    FillInDescriptions(frameParentStackLayout.Children[0], new EventArgs());

                    StackLayout buttonContainer = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                    };

                    Button btnKeepOpenInTab = BuildButton("Keep open in tab");
                    Button btnClose = BuildButton("Close");

                    buttonContainer.Children.Add(btnKeepOpenInTab);
                    buttonContainer.Children.Add(btnClose);

                    scrollViewAndButtonContainerStackLayout.Children.Add(faultWarningScrollView);
                    scrollViewAndButtonContainerStackLayout.Children.Add(buttonContainer);

                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        helpContentView.Padding = new Thickness(currentPageWidth * 0.025, 0);
                    }
                    btnClose.Clicked += (senders, args) =>
                    {
                        overlayLayout.Children.Remove(helpContentView);
                    };
                    btnKeepOpenInTab.Clicked += (senders, args) =>
                    {
                        MessagingCenter.Send(this, "PinToTab", fileName);
                        overlayLayout.Children.Remove(helpContentView);
                    };
                }
                AbsoluteLayout.SetLayoutBounds(helpContentView, new Rectangle(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(helpContentView, AbsoluteLayoutFlags.All);

                overlayLayout.Children.Add(helpContentView);
            }
            catch (Exception e)
            {
                DisplayAlert("Display Helper: ", e.ToString(), "Close");
            }
        }

        public Frame BuildFaultsFrame(GoiParameter parameter, int bit = 0)
        {
            if (parameter != null)
            {
                StackLayout labelValueContainer = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };

                StackLayout frameStackLayout = new StackLayout();

                Label label = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                    TextColor = Color.Black,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                };
                label.SetBinding(Label.TextProperty, new Binding("Name"));

                labelValueContainer.Children.Add(label);

                frameStackLayout.Children.Add(labelValueContainer);

                Frame frame = new Frame
                {
                    Padding = new Thickness(5, 0, 5, 0),
                    Margin = new Thickness(5, 0, 5, 0),
                    BackgroundColor = Color.FromHex("#C2C3C6"),
                    HasShadow = false,
                    Content = frameStackLayout
                };
                frame.BindingContext = parameter;

                return frame;
            }
            else
            {
                return BuildFrameWithString($"Fault bit {bit} was undifined!");
            }
        }

        private Button BuildButton(string text, bool isVisible = true)
        {
            double currentPageHeight = Xamarin.Forms.Application.Current.MainPage.Height;
            double currentPageWidth = Xamarin.Forms.Application.Current.MainPage.Width;

            Button btn = new Button
            {
                Text = text,
                IsVisible = isVisible,
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(10, 5)
            };

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone && (currentPageHeight / currentPageWidth > 1))
                btn.Margin = new Thickness(0, 10);
            else
                btn.Margin = new Thickness(10);

            return btn;
        }

        public Frame BuildFrameWithString(string strMessage)
        {
            Frame warningFrame = new Frame
            {
                Padding = new Thickness(5, 0, 5, 0),
                Margin = new Thickness(5, 0, 5, 0),
                BackgroundColor = Color.FromHex("#C2C3C6"),
                HasShadow = false,
                Content = new Label
                {
                    Text = strMessage
                }
            };
            return warningFrame;
        }
        string basedTextColor = "#367F56";
        public Xamarin.Forms.ScrollView BuildScrollView(StackLayout stacklayout, Xamarin.Forms.ScrollView scrollView, PageViewModel pageDescriptions)
        {
            /////////////////////////////////////End of common page layout//////////////////////////////////////////////////////////
            //Now Fill in xaml for data and help
            //TSX not supported yet may be add a type description to the .screen file

            StackLayout tempStackLayout = null;
            StackLayout tempChildrenStackLayout = null;
            //Backward compatible with screen files without groups
            foreach (PageItem item in pageDescriptions.PageItems)
            {
                tempStackLayout = new StackLayout();
                tempStackLayout.Children.Add(BuildStandardFrameEntry(item));
                stacklayout.Children.Add(tempStackLayout);
            }

            foreach (PageGroup pageGroup in pageDescriptions.PageGroups)
            {
                //< StackLayout Orientation = "Horizontal" >
                // < Image IsVisible = "True" Source = "Information_icon.png" Margin = "0" Scale = ".6" HorizontalOptions = "Start" />
                //  < Label  Text = "General Vehicle Switches"  TextColor = "#367F56"   FontSize = "Default"  VerticalOptions = "Center" HorizontalOptions = "Start" HorizontalTextAlignment = "Start" />
                // < Image IsVisible = "False" Source = "collapseIcon.png" Margin = "0" Scale = ".6" HorizontalOptions = "Start" />
                // < StackLayout.GestureRecognizers >
                // < TapGestureRecognizer Tapped = "FillInDescriptions" NumberOfTapsRequired = "1" />
                // </ StackLayout.GestureRecognizers >
                //</ StackLayout >

                tempStackLayout = new StackLayout();

                if (pageGroup.visibilityBinding == null)
                {
                    BuildStandardStackLayout(tempStackLayout, pageGroup);

                    if (pageGroup.visibilityUserGreaterThanOrEqualLevelBinding != null)
                    {
                        SetBindingBaseOnUserLevel(pageGroup.visibilityUserGreaterThanOrEqualLevelBinding, tempStackLayout);
                    }
                }
                else
                {
                    tempChildrenStackLayout = new StackLayout();
                    BuildStandardStackLayout(tempChildrenStackLayout, pageGroup);

                    GoiParameter parameter = null;
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == pageGroup.visibilityBinding);
                    }

                    tempChildrenStackLayout.BindingContext = parameter;
                    if (pageGroup.visibilityBindingValue == "False") //When there is negative prefix from PARAMETERs such as DISABLE...
                        tempChildrenStackLayout.SetBinding(IsVisibleProperty, "parameterBoolean", BindingMode.OneWay, converter: new BoolInverterConverter(), null);
                    else //Leroy might take advantage of this system to show/hide a section without convertor
                    {
                        tempChildrenStackLayout.SetBinding(IsVisibleProperty, new Binding("parameterBoolean"));
                    }

                    tempStackLayout.Children.Add(tempChildrenStackLayout);

                    if (pageGroup.visibilityUserGreaterThanOrEqualLevelBinding != null)
                    {
                        SetBindingBaseOnUserLevel(pageGroup.visibilityUserGreaterThanOrEqualLevelBinding, tempStackLayout);
                    }
                }

                //Adding necessary buttons at the end of the page group

                if (pageGroup.PageButtons.Count > 0)
                {
                    StackLayout tempHorizontalStackLayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal
                    };

                    foreach (PageButton button in pageGroup.PageButtons)
                    {
                        tempHorizontalStackLayout.Children.Add(BuildFrameForButton(button.Text));
                        if (button.Text == "Request Advanced Access")
                            tempStackLayout.SetBinding(IsVisibleProperty, new Binding("IsRequestAdvancedUserEnable"));
                    }

                    tempStackLayout.Children.Add(tempHorizontalStackLayout);
                }

                stacklayout.Children.Add(tempStackLayout);
            }

            scrollView.Content = stacklayout; //fill in UI
            return scrollView;
        }

        private StackLayout BuildGroupTitleStackLayout(Label lblGroupTitle)
        {
            StackLayout groupTitleStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            Image infoIcon = new Image()
            {
                Source = "Information_icon.png",
                HorizontalOptions = LayoutOptions.Start,
                Scale = .6,
                Margin = new Thickness(0)
            };

            Image openIcon = new Image()
            {
                Source = "openIcon.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Scale = .5,
                Margin = new Thickness(0),
                IsVisible = true
            };

            Image collapseIcon = new Image()
            {
                Source = "collapseIcon.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Scale = .5,
                Margin = new Thickness(0),
                IsVisible = false
            };

            groupTitleStackLayout.Children.Add(infoIcon);
            groupTitleStackLayout.Children.Add(lblGroupTitle);
            groupTitleStackLayout.Children.Add(openIcon);
            groupTitleStackLayout.Children.Add(collapseIcon);

            var tapToFillInDescriptions = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
            };

            tapToFillInDescriptions.Tapped += FillInDescriptions;

            groupTitleStackLayout.GestureRecognizers.Add(tapToFillInDescriptions);

            return groupTitleStackLayout;
        }
        private StackLayout BuildGroupDescriptionStackLayout(string groupDescription, string basedTextColor, PageGroup pageGroup)
        {
            StackLayout groupDescriptionStackLayout = new StackLayout();

            Label lblGroupDescription = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.FromHex(basedTextColor),
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.Start,
                Text = groupDescription
            };

            if (groupDescription.Contains("<"))
            {
                lblGroupDescription.TextType = TextType.Html;
            }
            groupDescriptionStackLayout.Children.Add(lblGroupDescription);

            if (pageGroup.GroupDescriptionButtons.Count > 0)
            {
                StackLayout tempHorizontalStackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };

                foreach (GroupDescriptionButton button in pageGroup.GroupDescriptionButtons)
                {
                    tempHorizontalStackLayout.Children.Add(BuildFrameForButton(button.Text));
                }
                groupDescriptionStackLayout.Children.Add(tempHorizontalStackLayout);
            }

            groupDescriptionStackLayout.IsVisible = false;


            return groupDescriptionStackLayout;
        }
        private Frame BuildStandardFrameEntry(PageItem item)
        {
            StackLayout frameStackLayout = new StackLayout();

            Frame frame = new Frame
            {
                BorderColor = Color.Black,
                Padding = new Thickness(5, 0, 5, 0),
                Margin = new Thickness(5, 0, 5, 0),
            };

            if (item.TapGesutreRecognizer == "AddStepper")
            {
                //Write Page has different view layout
                //Doing this way can save us time to re-write AddStepper function
                frame.Content = BuildGridSystem(item);
            }
            else if (item.TapGesutreRecognizer == "AddSlider")
            {
                //Write Page has different view layout
                //Doing this way can save us time to re-write AddStepper function
                frame.Content = BuildGridSystem(item, "AddSlider");
            }
            else
            {
                if (item.PageSubItems.Count > 0)
                {
                    //If there are 2 or more parameters which shared last parameter's description
                    foreach (PageItem subItem in item.PageSubItems)
                    {
                        BuildFrameStackLayout(subItem, frameStackLayout, item.PageSubItems.Count);
                    }
                }
                else
                    BuildFrameStackLayout(item, frameStackLayout);

                frame.Content = frameStackLayout;

                if (item.visibilityBinding != null)
                {
                    if (ControllerTypeLocator.ControllerType == "TAC")
                        frame.BindingContext = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == item.visibilityBinding);
                    else
                        frame.BindingContext = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.PropertyName == item.visibilityBinding);

                    if (item.visibilityBindingValue == "False")
                        frame.SetBinding(IsVisibleProperty, "parameterBoolean", BindingMode.OneWay, converter: new BoolInverterConverter(), null);
                    else
                        frame.SetBinding(IsVisibleProperty, "parameterBoolean");
                }
            }

            if (item.ViewType == PageItem.ViewTypes.Hidden && item.PageSubItems.Count == 0)
            {
                frame.IsVisible = false;
            }

            //Some frames require User level > "USER" on TSX
            if (item.visibilityUserGreaterThanOrEqualLevelBinding != null)
            {
                SetBindingBaseOnUserLevel(item.visibilityUserGreaterThanOrEqualLevelBinding, null, frame);
            }

            return frame;
        }

        Stepper demoModeStepper;

        private void BuildFrameStackLayout(PageItem item, StackLayout frameStackLayout, int numberOfChildren = 0)
        {
            StackLayout labelValueContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            if (item.ViewType != PageItem.ViewTypes.Hidden)
            {
                TapGestureRecognizer tapGestureRecognizer = null;
                if (item.TapGesutreRecognizer == null)
                {
                    tapGestureRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 2
                    };

                    tapGestureRecognizer.Tapped += SelectForDataLogging;
                }
                else if (item.TapGesutreRecognizer != "None")
                {
                    tapGestureRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 2
                    };

                    WebViewStringEventArgs webViewStringEventArgs = new WebViewStringEventArgs();

                    webViewStringEventArgs.internalWebPage = item.TapGesutreRecognizer;

                    //Only Interal web view could be opened, should we need an external web view as well ?
                    tapGestureRecognizer.Tapped += (sender, e) => OpenWebView(sender, webViewStringEventArgs);
                }
                else
                {
                    // TapGestureRecognizer is not required
                }

                //Switches selected for datalogging
                if (item.IsSelectedForDatalogging != null)
                {
                    Switch switchSelectedForDataloggingEntry = new Switch
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        IsVisible = true
                    };
                    switchSelectedForDataloggingEntry.SetBinding(Switch.IsToggledProperty, "SelectedForDatalogging", BindingMode.TwoWay, null);

                    labelValueContainer.Children.Add(switchSelectedForDataloggingEntry);
                }

                //< Label Text = "{Binding Path=parameterEnumString,  StringFormat='{0}'}" TextColor = "Gray" FontSize = "Default" VerticalOptions = "Center" HorizontalOptions = "EndAndExpand" HorizontalTextAlignment = "End" />
                View valueView;
                if (item.ViewType == PageItem.ViewTypes.ReadOnlyTwoDecimalPlaces || item.ViewType == PageItem.ViewTypes.ReadOnly)
                {
                    Label numericLabel = BuildNumericLabel();
                    numericLabel.SetBinding(Label.TextProperty, "parameterValue", BindingMode.Default, null, "{0:0.##}");
                    valueView = numericLabel;
                }
                else if (item.ViewType == PageItem.ViewTypes.ReadOnlyThreeDecimalPlaces)
                {
                    Label numericLabel = BuildNumericLabel();
                    numericLabel.SetBinding(Label.TextProperty, "parameterValue", BindingMode.Default, null, "{0:F3}");
                    valueView = numericLabel;
                }
                else if (item.ViewType == PageItem.ViewTypes.ReadOnlyString)
                {
                    Label numericLabel = BuildNumericLabel();
                    numericLabel.SetBinding(Label.TextProperty, new Binding("parameterValueString"));
                    valueView = numericLabel;
                }
                else if (item.ViewType == PageItem.ViewTypes.Enum)
                {
                    //xml <Label Text="{Binding Path=parameterEnumString,  StringFormat='{0}'}" TextColor="Gray" FontSize="Default" VerticalOptions="Center" HorizontalOptions="EndAndExpand" HorizontalTextAlignment="End"/>
                    Label enumLable = new Label
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        TextColor = Color.Gray,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        VerticalOptions = LayoutOptions.Center,
                        IsVisible = true,
                        HorizontalTextAlignment = TextAlignment.Start,
                    };
                    enumLable.SetBinding(Label.TextProperty, new Binding("parameterEnumString"));

                    valueView = enumLable;
                }
                else if (item.ViewType == PageItem.ViewTypes.Hex)
                {
                    //  < local:DoneEntry Text = "{Binding Path=parameterValueString,  StringFormat='{0:F2}'}" Keyboard = "Numeric" Focused = "ParameterFocused" Unfocused = "ParameterUnfocused" Completed = "ParameterCompleted" TextColor = "Black" FontSize = "Default" VerticalOptions = "Center"  IsPassword = "False" IsVisible = "True" HorizontalOptions = "FillAndExpand" HorizontalTextAlignment = "End"   WidthRequest = "100" >
                    //      < local:DoneEntry.HorizontalOptions >
                    //      < OnPlatform  x: TypeArguments = "LayoutOptions" iOS = "FillAndExpand"  Android = "EndAndExpand" />
                    //      </ local:DoneEntry.HorizontalOptions >
                    //  </ local:DoneEntry >
                    DoneEntry doneEntry = new DoneEntry
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Keyboard = Keyboard.Text,
                        TextColor = Color.Black,
                        FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                        VerticalOptions = LayoutOptions.Center,
                        IsPassword = false,
                        IsVisible = true,
                        HorizontalTextAlignment = TextAlignment.End,
                        WidthRequest = 100
                    };
                    //slider gets automatically updated with one way from source
                    //writing the value is from Slider.ValueChanged
                    doneEntry.SetBinding(DoneEntry.TextProperty, "parameterValueInt", BindingMode.OneWay, null, "0x{0:X4}");
                    doneEntry.Focused += ParameterFocused;
                    doneEntry.Unfocused += ParameterUnfocused;
                    doneEntry.Completed += ParameterCompleted;
                    valueView = doneEntry;
                    //Ransom might take a look later !!
                    //tapGestureRecognizer.Tapped += AddStepper;
                    //tapGestureRecognizer.Tapped += AddSlider;
                }
                else if (item.ViewType == PageItem.ViewTypes.FloatTwoDecimalPlaces || item.ViewType == PageItem.ViewTypes.Float)
                {
                    DoneEntry doneEntry = DoneEntryForViewTypeFloat();
                    //Doing this way, we could have more than 1 view type float
                    doneEntry.SetBinding(DoneEntry.TextProperty, "parameterValue", BindingMode.OneWay, null, "{0:0.##}");
                    valueView = doneEntry;

                    //tapGestureRecognizer.Tapped += AddStepper;
                    //tapGestureRecognizer.Tapped += AddSlider;
                }
                else if (item.ViewType == PageItem.ViewTypes.FloatFourDecimalPlaces)
                {
                    DoneEntry doneEntry = DoneEntryForViewTypeFloat();
                    //Doing this way, we could have more than 1 view type float
                    doneEntry.SetBinding(DoneEntry.TextProperty, "parameterValue", BindingMode.OneWay, null, "{0:0.####}");
                    valueView = doneEntry;

                    //tapGestureRecognizer.Tapped += AddStepper;
                    //tapGestureRecognizer.Tapped += AddSlider;
                }
                else if (item.ViewType == PageItem.ViewTypes.DropDown)
                {
                    //<Frame OutlineColor="Black" Padding="5,1,5,1" Margin="5,1,5,1">
                    //      <StackLayout x:Name="BATTERY_TYPE" Orientation="Vertical"  >
                    //          <Label Text="Battery Type" TextColor="Black" VerticalOptions="Center" HorizontalOptions="StartAndExpand" HorizontalTextAlignment="Center"/>
                    //          <!--<Picker x:Name="ThePicker" ItemsSource="{Binding enumListName}" ItemDisplayBinding="{Binding .}" SelectedItem="{Binding parameterEnumString}" Title="Don't make you names any bigger than this" SelectedIndexChanged="PickerSelectedIndexChanged"   VerticalOptions="Center" HorizontalOptions ="End" FontSize="Small" >-->
                    //          <Picker x:Name="ThePicker" ItemsSource="{Binding enumListName}" ItemDisplayBinding="{Binding .}" SelectedItem="{Binding parameterEnumString}" Title="Battery Selection -----------------------" SelectedIndexChanged="PickerSelectedIndexChanged"   VerticalOptions="Center" HorizontalOptions ="End" FontSize="Small" >
                    //          </Picker>
                    //      </StackLayout>
                    //</Frame>
                    Xamarin.Forms.Picker picker = BuildPicker();

                    picker.SelectedIndexChanged += PickerSelectedIndexChanged;

                    valueView = picker;
                }
                else if (item.ViewType == PageItem.ViewTypes.Vertical)
                {
                    labelValueContainer.Orientation = StackOrientation.Vertical;

                    Xamarin.Forms.Picker picker = BuildPicker();

                    if (item.PropertyName == "PARPROFILENUMBER" && demoModeStepper != null)
                    {
                        picker.SelectedIndexChanged += DemoSoftwareRevisionSelectedIndexChanged;
                        picker.SetBinding(IsEnabledProperty, new Binding("Value", source: demoModeStepper));
                    }

                    if (item.PropertyName == "MILESORKILOMETERS")
                    {
                        picker.SelectedIndexChanged += PickDisplayUnit;
                    }
                    valueView = picker;
                }
                else //switch
                {
                    //< Switch IsToggled = "{Binding Path=parameterBoolean,Mode=OneWay}"   IsEnabled = "True" Toggled = "OptionsParameterToggled" Focused = "ParameterFocused"  IsVisible = "True" HorizontalOptions = "EndAndExpand" Grid.Column = "1" />
                    Switch switchEntry = new Switch
                    {
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Scale = 1
                    };
                    switchEntry.Toggled += BitParameterToggled;
                    switchEntry.SetBinding(Switch.IsToggledProperty, "parameterBoolean", BindingMode.OneWay, null, null);
                    //switchEntry.Unfocused += ParameterUnfocused;
                    valueView = switchEntry;
                }

                //< StackLayout Orientation = "Vertical" >
                //    < StackLayout Orientation = "Vertical" >
                //        < StackLayout Orientation = "Horizontal" >
                //            < Image IsVisible = "False" Source = "Information_icon.png" Margin = "0" Scale = ".6" HorizontalOptions = "Start" />
                //            < Label  Text = "Vehicle Switches"  TextColor = "#367F56"   FontSize = "Default"  VerticalOptions = "Center" HorizontalOptions = "Start" HorizontalTextAlignment = "Start" />
                //            < Image IsVisible = "False" Source = "collapseIcon.png" Margin = "0" Scale = ".6" HorizontalOptions = "Start" />
                //            < StackLayout.GestureRecognizers >
                //            < TapGestureRecognizer Tapped = "FillInDescriptions" NumberOfTapsRequired = "1" />
                //            </ StackLayout.GestureRecognizers >
                //        </ StackLayout >
                //    < StackLayout Orientation = "Vertical" IsVisible = "False" >
                //        < Label TextColor = "#367F56" FontSize = "Default"  VerticalOptions = "Center" HorizontalOptions = "FillAndExpand" HorizontalTextAlignment = "Start"
                //        Text = "Check the vehicle switch inputs are functional by changing them and watch the results here" >
                //        </ Label >
                //    </ StackLayout >
                //    < Frame OutlineColor = "Black" Padding = "5,1,5,1" Margin = "5,1,5,1" >
                //        < StackLayout Orientation = "Vertical" x: Name = "KEYIN" >
                //            < StackLayout.GestureRecognizers >
                //            < TapGestureRecognizer Tapped = "SelectForDataLogging" NumberOfTapsRequired = "2" />
                //            </ StackLayout.GestureRecognizers >
                //            < StackLayout Orientation = "Horizontal" >
                //                < Label Text = "{Binding Path=Name,  StringFormat='{0}'}" TextColor = "Black" FontSize = "Default" VerticalOptions = "Center" HorizontalTextAlignment = "Start" />
                //                < Label Text = "{Binding Path=parameterEnumString,  StringFormat='{0}'}" TextColor = "Gray" FontSize = "Default" VerticalOptions = "Center" HorizontalOptions = "EndAndExpand" HorizontalTextAlignment = "End" />
                //            </ StackLayout >
                //        </ StackLayout >
                //    </ Frame >

                if (tapGestureRecognizer != null)
                {
                    frameStackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                }

                labelValueContainer.Children.Add(SetBindingNameToLabel());
                labelValueContainer.Children.Add(valueView);
            }

            if (item.PropertyName == "DEMOMODE" && ControllerTypeLocator.ControllerType == "TAC")
            {
                demoModeStepper = new Stepper();
                demoModeStepper.IsVisible = false;
                demoModeStepper.SetBinding(Stepper.ValueProperty, new Binding("parameterBoolean"));
                frameStackLayout.Children.Add(demoModeStepper);
            }

            //Hopefully there is no null value return
            GoiParameter parameter = SetupLiveData(item);

            //if (parameter.SubsetOfAddress == false) //bits and enums are subsets, not actual full readable parameters
            //    PageParameters.parameterList.Add(parameter);

            if (item.IsSelectedForDatalogging == "True")
            {
                parameter.SelectedForDatalogging = true;
            }

            if (numberOfChildren > 0)
                labelValueContainer.BindingContext = parameter;
            else
                frameStackLayout.BindingContext = parameter;

            if (item.ViewType != PageItem.ViewTypes.Hidden)
            {
                frameStackLayout.Children.Add(labelValueContainer);
            }
        }
        async public void PickerSelectedIndexChanged(object sender, EventArgs e) //new properly hides general page one
        {
            GoiParameter a = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
            a.bupdate = false;

            //below sender as Xamarin.Forms.Picker).SelectedIndex < a.GroupedParameters.Count - 1 is there so that custom setting (last one) does not write anything
            //TODO: custom setting  also has no ParameterFileItemList so it would not queue of anything anyways?
            if ((sender as Xamarin.Forms.Picker).SelectedIndex < a.GroupedParameters.Count - 1 && (sender as Xamarin.Forms.Picker).SelectedIndex != App.ViewModelLocator.GetParameter(a.PropertyName).parameterValue && (sender as Xamarin.Forms.Picker).SelectedIndex != -1)
            {
                await MainFlyoutPage.BeginInvokeOnMainThreadAsync(async () =>
                {
                    activityMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    activityMessage.Text = "";
                    PageIsBusy = true;
                });
                //Writing all these lists and matching can take a while
                foreach (var b in a.GroupedParameters[(sender as Xamarin.Forms.Picker).SelectedIndex].ParameterFileItemList)
                    QueParameter(new SetParameterEventArgs(b.Address, (float)b.ParameterValue, null));

                await MainFlyoutPage.BeginInvokeOnMainThreadAsync(async () =>
                {
                    await Task.Delay(1000); //might take time for QueParameter to even start filling queue
                    while (App._devicecommunication.IsQueueNotEmpty())
                        await Task.Delay(100);
                    await Task.Delay(1000);
                    PageIsBusy = false;
                });
            }
            else
            {//custom
                //if (App.AppConfigurationLevel == "ENG")
                //{
                //    //do something like pop up settings for these parameters for unknown settings, ie custom?
                //    //popupBatterySettings.IsVisible = true;
                //}
            }
            int tempval = (int)a.parameterValue;
            //            EventValue.Text = string.Format("Picker Index value is {0:F1}", tempval);
            a.bupdate = true;
        }

        public async void PickDisplayUnit(object sender, EventArgs e)
        {
            if ((sender as Xamarin.Forms.Picker).SelectedIndex != -1)
            {
                var Index = (sender as Xamarin.Forms.Picker).SelectedIndex;

                GoiParameter a = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                a.bupdate = false;

                if (ControllerTypeLocator.ControllerType == "TAC")
                {

                    if (App.ViewModelLocator.GetParameter("MILESORKILOMETERS").parameterValue != (sender as Xamarin.Forms.Picker).SelectedIndex)
                    {
                        QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("MILESORKILOMETERS").Address, Index, null));
                    }
                }
                else
                {
                    if (App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue != (sender as Xamarin.Forms.Picker).SelectedIndex)
                    {
                        DisplaySettings.SaveStandardorMetric(Index == 0 ? false : true);
                        // we do not implement QueParameter to set any parameters due to non-existence TSX paramenters
                        // so set bupdate to true to let its object to invoke recalculate
                        a.bupdate = true;
                        App.ViewModelLocator.GetParameterTSX("MILESORKILOMETERS").parameterValue = DisplaySettings.GetStandardorMetric() ? 1 : 0;
                    }
                }
                a.bupdate = true;
            }
        }


        public void DemoSoftwareRevisionSelectedIndexChanged(object sender, EventArgs e)
        {//Just so tech support can change and see screens and help like the customer
            if (App._MainFlyoutPage._DeviceListPage._device is DemoDevice && (sender as Xamarin.Forms.Picker).SelectedIndex != -1)
            {
                GoiParameter a = (sender as Xamarin.Forms.VisualElement).BindingContext as GoiParameter;
                a.bupdate = false;
                float newSoftwareRevision = App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue - (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue % 0.100f);
                newSoftwareRevision = newSoftwareRevision + ((float)int.Parse(a.enumListValue[(sender as Xamarin.Forms.Picker).SelectedIndex])) / 1000f;
                if (newSoftwareRevision < 4.7f)
                    newSoftwareRevision = 4.6f;
                //Binding can force routines to be called at start up because the initial Binding has actually changed from unknown startup values
                //Even though this particular routine is only visible in demo mode it is still called because of binding
                //it is good practice not to write values to the controller that have not changed
                if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue != newSoftwareRevision)
                {
                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("SOFTWAREREVISION").Address, newSoftwareRevision, null));
                    ChangeOpenDescriptions(sender, e);
                }
                a.bupdate = true;
            }
        }
        protected HybridWebView LoadHTMLPage(string hTMLFileNameAndPath)
        {
            htmlFilePathAndName = hTMLFileNameAndPath;
            string url = htmlFilePathAndName;
            if (FileManager.isExternalFile(hTMLFileNameAndPath))
                url = "ExternalLoader.html";
            foreach (var x in DeviceComunication.PageCommunicationsListPointer)
            {
                x.parentPage.Active = false;
            }
            hybridWebView = new HybridWebView(this)
            {
                Uri = url //just converting
            };
            return hybridWebView;
        }

        async void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            var data = e.Reading;

            //System.Diagnostics.Debug.WriteLine($"Reading: X: {data.Orientation.X}, Y: {data.Orientation.Y}, Z: {data.Orientation.Z}, W: {data.Orientation.W}");

            Location location = await Geolocation.GetLocationAsync(App._geolocationRequest);
            if (location.Accuracy > 20)
            {
                if (lastKnownLocation == null) location = await Geolocation.GetLastKnownLocationAsync();
                else location = lastKnownLocation;
            }

            //App.ViewModelLocator.GetParameter("ORIENTATIONX").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Atan2((2 * (data.Orientation.W * data.Orientation.X + data.Orientation.Y * data.Orientation.Z)), (1 - 2 * (data.Orientation.X * data.Orientation.X + data.Orientation.Y * data.Orientation.Y))));
            //App.ViewModelLocator.GetParameter("ORIENTATIONY").parameterValue = (float)UnitConverters.RadiansToDegrees(2 * Math.Atan2(Math.Sqrt(1 + 2 * (data.Orientation.W * data.Orientation.Y - data.Orientation.X * data.Orientation.Z)), Math.Sqrt(1 - 2 * (data.Orientation.W * data.Orientation.Y - data.Orientation.X * data.Orientation.Z))) - Math.PI / 2);
            //App.ViewModelLocator.GetParameter("ORIENTATIONZ").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Atan2((2 * (data.Orientation.W * data.Orientation.Z + data.Orientation.X * data.Orientation.Y)), (1 - 2 * (data.Orientation.Y * data.Orientation.Y + data.Orientation.Z * data.Orientation.Z))));
            //App.ViewModelLocator.GetParameter("ORIENTATIONW").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Acos(data.Orientation.W) * 2);


            //from https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles (same results as above)
            // roll (x-axis rotation)
            double sinr_cosp = 2 * (data.Orientation.W * data.Orientation.X + data.Orientation.Y * data.Orientation.Z);
            double cosr_cosp = 1 - 2 * (data.Orientation.X * data.Orientation.X + data.Orientation.Y * data.Orientation.Y);
            App.ViewModelLocator.GetParameter("ORIENTATIONX").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Atan2(sinr_cosp, cosr_cosp));

            // pitch (y-axis rotation)
            double sinp = Math.Sqrt(1 + 2 * (data.Orientation.W * data.Orientation.Y - data.Orientation.X * data.Orientation.Z));
            double cosp = Math.Sqrt(1 - 2 * (data.Orientation.W * data.Orientation.Y - data.Orientation.X * data.Orientation.Z));
            App.ViewModelLocator.GetParameter("ORIENTATIONY").parameterValue = 2 * (float)UnitConverters.RadiansToDegrees(Math.Atan2(sinp, cosp) - Math.PI / 2 + Math.PI / 4); // + Math.PI / 4 just to correct for phone coordinate

            // yaw (z-axis rotation)
            double siny_cosp = 2 * (data.Orientation.W * data.Orientation.Z + data.Orientation.X * data.Orientation.Y);
            double cosy_cosp = 1 - 2 * (data.Orientation.Y * data.Orientation.Y + data.Orientation.Z * data.Orientation.Z);

            App.ViewModelLocator.GetParameter("ORIENTATIONZ").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Atan2(siny_cosp, cosy_cosp));

            App.ViewModelLocator.GetParameter("ORIENTATIONW").parameterValue = (float)UnitConverters.RadiansToDegrees(Math.Acos(data.Orientation.W) * 2);

            App.ViewModelLocator.GetParameter("LATITUDE").parameterValue = (float)location.Latitude;
            App.ViewModelLocator.GetParameter("LONGITUDE").parameterValue = (float)location.Longitude;
            App.ViewModelLocator.GetParameter("ALTITUDE").parameterValue = (float)location.Altitude;
        }

        public bool isOrientationEnabled = false;

        public void GetOrientation()
        {
            if (Device.RuntimePlatform != Device.UWP) //UWP crashes on this without tracable exception
            {
                // Set speed delay for monitoring changes.
                SensorSpeed speed = SensorSpeed.Default;
                isOrientationEnabled = true;
                try
                {
                    if (Accelerometer.IsMonitoring)
                    {
                        OrientationSensor.ReadingChanged -= OrientationSensor_ReadingChanged;
                        OrientationSensor.Stop();
                    }
                    else if (!Accelerometer.IsMonitoring)
                    {
                        // Register for reading changes, be sure to unsubscribe when finished
                        OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
                        OrientationSensor.Start(speed);
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    DisplayAlert("OrientationSensor", "OrientationSensor is not supported on this device", "OK");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            }
        }

        Location lastKnownLocation;

        public async Task ReadOneParameter(GoiParameter parameter)
        {
            PageParameterList pageParameters;
            if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
            else
                pageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, this);

            if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                pageParameters.parameterList.Add(App.ViewModelLocator.GetParameter(parameter.PropertyName));
            else
                pageParameters.parameterList.Add(App.ViewModelLocator.GetParameterTSX(parameter.PropertyName));

            var uniqueId = App._devicecommunication.AddToPacketList(pageParameters);
            //one time packet build, sent then removed, maybe queuing it would make more sense
            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
            {
                await Task.Delay(10); //TODO:Flag this as an issue if it happens
            }

            App._devicecommunication.SetUniqueIdToBeRemoved(uniqueId);
            pageParameters.Active = true; //starts as disabled

            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                await Task.Delay(10);
        }

        public async Task communicationsReadWithWait(PageParameterList pageParameters)
        {
            var uniqueId = App._devicecommunication.AddToPacketList(pageParameters);
            //one time packet build, sent then removed, maybe queuing it would make more sense
            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                await Task.Delay(10); //TODO:Flag this as an issue if it happens

            App._devicecommunication.SetUniqueIdToBeRemoved(uniqueId);
            pageParameters.Active = true; //starts as disabled

            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                await Task.Delay(10);
        }

        public void communicationsReadWithoutWait(PageParameterList pageParameters)
        {
            htmlPageUniqueId = App._devicecommunication.AddToPacketList(pageParameters);
            pageParameters.Active = true; //Communication thread will just constanly loop through this command
                                          //System.Diagnostics.Debug.WriteLine("Read without wait sent" + DateTime.Now.Millisecond.ToString());
        }

        public async void communicationsWriteWithWait(PageParameterList pageParameters)
        {

            foreach (var item in pageParameters.parameterList)
            {
                GoiParameter parameter;
                if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                    parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.PropertyName == item.PropertyName);
                else
                    parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(i => i.PropertyName == item.PropertyName);

                if (parameter != null)
                    SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)item.parameterValue, parameter.PropertyName);
                else
                    System.Diagnostics.Debug.WriteLine("Navitas Parameter PropertyName " + item.PropertyName + " not found");

                QueParameter(SetParameterEventArgs);
            }
            //no need to wait the read command command does not finish util read response is complete
            //while (await App._devicecommunication.IsQueueNotEmptyAsync())
            //    Task.Delay(20).Wait(); //polling time to free up threads
            if (pageParameters.parameterList.Count == 2)
                System.Diagnostics.Debug.WriteLine("Here 1");
        }

        public async Task<string> communicationsReadScopeBlockWithWait(PageParameterList pageParameters)
        {
            //System.Diagnostics.Debug.WriteLine("communicationsReadScopeBlockWithWait called");
            var item = pageParameters.parameterList[0]; //better only only be one
            GoiParameter parameter;
            if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.PropertyName == item.PropertyName);
            else
                parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(i => i.PropertyName == item.PropertyName);

            if (parameter != null)
                SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)item.parameterValue, "never used so get rid of this");
            else
                System.Diagnostics.Debug.WriteLine("Navitas Parameter PropertyName " + item.PropertyName + " not found");

            QueParameter(SetParameterEventArgs);
            if (parameter.Address == 0x22)//Wow, a scope block write actually writes to 0x22 which is IAC but does not get through, lucky IAC is not neede as a writeable
            {
                scopeBlockResponded = false;
                while (!scopeBlockResponded) //TODO: figure out better timeouts and corresponding message for these types of things
                    await Task.Delay(10);
                //just to get this done, modify whatever we averaged
                var channel1Pointer = (int)App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == "DATASCOPECH1SELECT").parameterValue;
                var channel1Parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == channel1Pointer) && (x.SubsetOfAddress == false));

                var parameterStream = new ParameterNameAndValueList[1];
                var parameterNameAndValueList = new ParameterNameAndValueList
                {
                    PropertyName = channel1Parameter.PropertyName,
                    parameterTimeAndValuePairList = new List<TimeAndValuePair>()
                };
                int i = 0;
                foreach (var point in scopeBloc)
                {
                    var timeAndValuePair = new TimeAndValuePair { time = (i++).ToString(), value = point / channel1Parameter.Scale };
                    parameterNameAndValueList.parameterTimeAndValuePairList.Add(timeAndValuePair);
                    if (i == 1) break; //just do the first one for now since the js code only uses the first
                }
                parameterStream[0] = parameterNameAndValueList;

                return JsonConvert.SerializeObject(parameterStream);

                //var result = hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand(" + jsonData + ");"); // CancelNavitasTacControllerSimulators();");


                //no need to wait the read command command does not finish util read response is complete
                //while (await App._devicecommunication.IsQueueNotEmptyAsync())
                //    Task.Delay(20).Wait(); //polling time to free up threads
            }
            return "";
        }

        bool scopeBlockResponded = false;
        List<float> scopeBloc;
        public void ResponseFromScopeBlock(object sender, ScopeGetBlockResponseEventArgs e)//List<byte> listOfBytes)
        {
            List<byte> listOfBytes = e.listOfBytesFromResponse;
            scopeBloc = new List<float>();
            for (int i = 0; i < listOfBytes.Count; i += 2)
            {
                scopeBloc.Add((((int)((sbyte)listOfBytes[i])) << 8) | (uint)listOfBytes[i + 1]);
            }
            scopeBlockResponded = true;
        }

        protected enum GroupOneFaults
        {
            ThrottleGroupOneFault,
            BrakeGroupOneFault,
            ChargerGroupOneFault,
            OverTempGroupOneFault,
            MotorOverTempGroupOneFault,
            ContactorHighResGroupOneFault,
            ParameterTableInvalidGroupOneFault,
            StartupBrakeMotionCheckGroupOneFault,
            RunningBrakeMotionCheckGroupOneFault,
            RegenResistorCheckGroupOneFault


        };
        enum GroupTwoFaults
        {
            DirectionGroupTwoFault,
            ContactorNotClosedGroupTwoFault,
            PreChargeGroupTwoFault,
            SolenoidGroupTwoFault,
            BrakeSolenoidGroupTwoFault,
            BrakeRelayGroupTwoFault,
            ReverseBuzzerGroupTwoFault,
            StartupPreChargeChangeGroupTwoFault,
        };
        enum GroupThreeFaults
        {
            BusUnderVoltageGroupThreeFault,
            BusOverVoltageGroupThreeFault,
            MotorOverCurrentGroupThreeFault,
        };
        enum GroupFourFaults
        {
            ACFirmwareOverCurrentGroupFourFault,
            ACFirmwareOverVoltageGroupFourFault,
            ACFirmwareUnderVoltageGroupFourFault,
            ACFirmwareOverTempGroupFourFault,
            ACFirmwareMotorOverCurrentGroupFourFault,
            junk,
            ACFirmwarePowerStageFailureGroupFourFault,
            ACFirmwareEncoderAGroupFourFault,
            ACFirmwareEncoderBGroupFourFault,
            ACFirmwareEncoderCGroupFourFault,
        };
        enum GroupOneWarnings
        {
            MotorOverHeatGroupOneWarning,     //0
            ControllerOverHeatGroupOneWarning,     //1
            LowVoltageGroupOneWarning,    //2
            TempSensorLostConnection,       //3
            RatedCurrentLimited                  //4
        }

        protected async void TACDisplayFaultMessage(object sender, EventArgs e)
        {
            await TACFaultMessageProcess();
        }
        public async Task TACFaultMessageProcess()
        {
            List<Frame> frameFaults = new List<Frame>();

            if (((int)(int)App.ViewModelLocator.GetParameter("GROUPONEFAULTS").parameterValue & (int)(1 << (int)GroupOneFaults.ParameterTableInvalidGroupOneFault)) != 0)
            {
                frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("GROUPONEFAULTS").Address && x.SubsetOfAddress && x.BitRangeStart == (int)GroupOneFaults.ParameterTableInvalidGroupOneFault)));
            }
            else //don't clutter the screen if the above error happens
            {
                if (App.ViewModelLocator.GetParameter("GROUPONEFAULTS").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameter("GROUPONEFAULTS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("GROUPONEFAULTS").Address && x.SubsetOfAddress && x.BitRangeStart == bit)));
                        }
                    }
                if (App.ViewModelLocator.GetParameter("GROUPTWOFAULTS").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameter("GROUPTWOFAULTS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("GROUPTWOFAULTS").Address && x.SubsetOfAddress && x.BitRangeStart == bit)));
                        }
                    }
                if (App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS").parameterValue != 0) //save some time
                {
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS").Address && x.SubsetOfAddress && x.BitRangeStart == bit)));
                        }
                    }
                }

                if (App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").parameterValue != 0) //save some time
                {
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").Address && x.SubsetOfAddress && x.BitRangeStart == bit)));
                        }
                        if (((int)App.ViewModelLocator.GetParameter("GROUPFOURFAULTS").parameterValue & (1 << bit) & (int)(1 << (int)GroupFourFaults.ACFirmwarePowerStageFailureGroupFourFault)) != 0)
                        {
                            PageParameterList pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
                            BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter("PowerStageTestVectorBits"));
                            BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter("PhaseAHalfBridgeTestResult_puq7"));
                            BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter("PhaseBHalfBridgeTestResult_puq7"));
                            BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter("PhaseCHalfBridgeTestResult_puq7"));
                            BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7"));
                            await communicationsReadWithWait(pageParameters);
                            //TODO: should these words be put in dictionary later?
                            string strFaults = "";

                            if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0001) == 0x0000))
                            {
                                strFaults += "Phase W not disabled\n";
                                strFaults += "Expected 50% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseAHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\n";

                            }
                            if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0002) == 0x0000))
                            {
                                strFaults += "Phase V not disabled on startup\n";
                                strFaults += "Expected 50% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseBHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\n";
                            }
                            if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0004) == 0x0000))
                            {
                                strFaults += "Phase U not disabled on startup\n";
                                strFaults += "Expected 50% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseCHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\n";
                            }
                            if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0007) == 0x0007)) //phase not disabled failures will not test the rest
                            {
                                if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0040) == 0x0000))
                                    strFaults += "Phase W not connected\nCheck motor cables\n\n";
                                else
                                {
                                    if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0008) == 0x0000))
                                    {
                                        strFaults += "Phase W failed switching test\n";
                                        strFaults += "Expected 25% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseAHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\nContact tech support\n\n";
                                    }
                                }
                                if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0080) == 0x0000))
                                    strFaults += "Phase V not connected\nCheck motor cables\n\n";
                                else
                                {
                                    if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0010) == 0x0000))
                                    {
                                        strFaults += "Phase V failed switching test\n";
                                        strFaults += "Expected 25% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseBHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\nContact tech support\n\n";
                                    }
                                }
                                if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0100) == 0x0000))
                                    strFaults += "Phase U not connected\nCheck motor cables\n\n";
                                else
                                {
                                    if ((((int)App.ViewModelLocator.GetParameter("PowerStageTestVectorBits").parameterValue & (int)0x0020) == 0x0000))
                                    {
                                        strFaults += "Phase U failed switching test\n";
                                        strFaults += "Expected 25% of battery voltage but measured " + String.Format("{0:0}", (App.ViewModelLocator.GetParameter("PhaseCHalfBridgeTestResult_puq7").parameterValue / App.ViewModelLocator.GetParameter("VdcBusHalfBridgeTestResult_puq7").parameterValue) * 100) + "% on startup\n\nContact tech support\n\n";
                                    }
                                }
                            }
                            strFaults += "  (4-7)\n\n";
                            frameFaults.Add(BuildFrameWithString(strFaults));
                        }
                    }
                }
            }

            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.MotorOverHeatGroupOneWarning)) != 0)
            {
                string strWarnings = "Warning - Check motor temperature\n";
                strWarnings += "The temperature sensor in the motor is above " + App.ViewModelLocator.GetParameter("NONFATALOTTRIPC").parameterValue.ToString("0") + "°C\n";
                strWarnings += "Motor currents are now being reduced until the temperature drops\n\n";
                frameFaults.Add(BuildFrameWithString(strWarnings));
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.ControllerOverHeatGroupOneWarning)) != 0)
            {
                string strWarnings = "Warning - Check controller temperature.\n";
                strWarnings += "The temperature sensor internal to the controller is above " + App.ViewModelLocator.GetParameter("OTWARNINGC").parameterValue.ToString("0") + "°C\n";
                strWarnings += "Motor currents are now being reduced until the temperature drops\n\n";
                frameFaults.Add(BuildFrameWithString(strWarnings));
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.LowVoltageGroupOneWarning)) != 0)
            {
                string strWarnings = "Warning - Low battery\n";
                strWarnings += "The battery voltage has fallen below the a very low state of charge level\n";
                strWarnings += "Motor currents are now being reduced to extend your remaining distance to empty\n\n";
                frameFaults.Add(BuildFrameWithString(strWarnings));
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.TempSensorLostConnection)) != 0)
            {
                string strWarnings = "Warning - Lost Temperature Sensor Connection\n";
                strWarnings += "Check connection to temperature sensor at motor\n\n";
                frameFaults.Add(BuildFrameWithString(strWarnings));
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.RatedCurrentLimited)) != 0)
            {
                string strWarnings = "Warning - Limited Current\n";
                strWarnings += "Limiting current to rated current of battery\n\n";
                frameFaults.Add(BuildFrameWithString(strWarnings));
            }

            if (frameFaults.Count != 0)
            {
                await DisplayFaultWarningMessageBox("TAC", frameFaults);
            }
        }
        public void TACDisplayWarningMessage()
        {
            string strWarnings = null;
            string strSubWarnings = "Performance is limited";
            var element = FindByName("CONTROLLERWARNING") as Label;

            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.MotorOverHeatGroupOneWarning)) == (int)(1 << (int)GroupOneWarnings.MotorOverHeatGroupOneWarning))
            {
                strWarnings = "Warning - Check motor temperature";//
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.ControllerOverHeatGroupOneWarning)) == (int)(1 << (int)GroupOneWarnings.ControllerOverHeatGroupOneWarning))
            {
                strWarnings = "Warning - Check controller temperature.";//
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.LowVoltageGroupOneWarning)) == (int)(1 << (int)GroupOneWarnings.LowVoltageGroupOneWarning))
            {
                strWarnings = "Warning - Low battery";//
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.TempSensorLostConnection)) == (int)(1 << (int)GroupOneWarnings.TempSensorLostConnection))
            {
                strWarnings = "Warning - Lost Temperature Sensor Connection";//
            }
            if (((int)App.ViewModelLocator.GetParameter("GROUPONEWARNINGS").parameterValue & (int)(1 << (int)GroupOneWarnings.RatedCurrentLimited)) == (int)(1 << (int)GroupOneWarnings.RatedCurrentLimited))
            {
                strWarnings = "Warning - Limited Current";//
            }


            if (strWarnings != null)
            {
                var transferAnimation = new Animation(v => element.Scale = v, 1, 1);
                Device.BeginInvokeOnMainThread(() =>
                {
                    element.IsVisible = true;
                    element.Text = strWarnings;
                    transferAnimation.Commit(this,
                        "WarningMessageAnimation",
                        16,
                        2500,
                        null,
                        async (v, c) =>
                        {
                            element.Text = strWarnings;
                            await Task.Delay(1500);
                            element.Text = strSubWarnings;
                            await Task.Delay(1000);
                        },
                        () => true);
                });
            }
        }

        // For new error flashing 
        enum StartUpFaults
        {
            InvalidThrottleType,        // 0
            MinGreaterThanMax,          // 1
            MaxGreaterThanMin,          // 2
            ThrottleRangeToSmall,       // 3        
            FieldCurrentSensorCalib,    // 4
            ArmCurrentSensorCalib,       // 5
            TempSensorCalib,            // 6
            HwShutDown,                 // 7
            WatchDogReset,              // 8

        };

        enum RunTimeFaults
        {
            AuxCoilOverCurrent,                  // 0
            LinCoilOverCurrent,                     // 1
            LiftCoilOverCurrent,                // 2
            SteerCoilOverCurrent,               // 3        
            FwdCoilOverCurrent,                 // 4
            RevCoilOverCurrent,                 // 5
            HwShutDown,                         // 6
            Pot1Fault,                         // 7
            Pot2Fault,                        // 8
            BothDirSwitches,                  // 9
            OverTemp,                            // 10
            BatteryOverVoltage,                  // 11
            BatteryUnderVoltage,                 // 12
            BaseDisCharged,                     // 13
            UnderTemp,                      // 14
            LossOfFieldCurrent              // 15


        };
        enum RunTimeFaults2
        {
            UnknownError,                   // 0
            MasterRemoteTimeout,            // 1
            MotorOverTemp,                  // 2
            BDILimiting,                    // 3
            InactivityLockout,				// 4
            WeldedSolenoid,                 // 5
            FieldMosfetShorted              // 6
        };

        enum RunTimeWarnings
        {
            ThrottleVoltageSupply,          // 0
            StallProtection,                // 1
            ControllerTempLimiting          // 2
        };

        protected async void TSXDisplayFaultMessage(object sender, EventArgs e)
        {
            await TSXFaultMessageProcess();
        }

        public async Task TSXFaultMessageProcess()
        {
            List<Frame> frameFaults = new List<Frame>();

            //if (((int)(int)App.ViewModelLocator.GetParameter("?").parameterValue & (int)(1 << (int)?.ParameterTableInvalidGroupOneFault)) != 0)
            //{
            //    frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameter("?").Address && x.SubsetOfAddress && x.BitRangeStart == (int)GroupOneFaults.ParameterTableInvalidGroupOneFault)));
            //}
            //else //don't clutter the screen if the above error happens
            {
                if (App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS").Address && x.SubsetOfAddress && x.BitRangeStart == bit), bit));
                        }
                    }
                if (App.ViewModelLocator.GetParameterTSX("PARRUNTIMEWARNINGS").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameterTSX("PARRUNTIMEWARNINGS").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameterTSX("PARRUNTIMEWARNINGS").Address && x.SubsetOfAddress && x.BitRangeStart == bit), bit));
                        }
                    }
                if (App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW").Address && x.SubsetOfAddress && x.BitRangeStart == bit), bit));
                        }
                    }
                if (App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH").parameterValue != 0) //save some time
                    for (int bit = 0; bit < 16; bit++)
                    {
                        if (((int)App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH").parameterValue & (1 << bit)) != 0)
                        {
                            frameFaults.Add(BuildFaultsFrame(App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH").Address && x.SubsetOfAddress && x.BitRangeStart == bit), bit));
                        }
                    }
            }

            if (frameFaults.Count != 0)
            {
                await DisplayFaultWarningMessageBox("TSX", frameFaults);
            }
        }

        public static SupplierChain currentSupplierChain;

        public string groupTitleHexColor = "#367F56";
        //private string groupSubTitleHexColor = "#0080C0";

        public int buttonCounter = 0;

        private double maxCurrent = 0.0;

        private string profileNumber = "";

        private string prevTitle = "";

        public async Task GetSupplierChainObject(bool AlreadyTalkingToBootloader = false, float softwareRevision = 0.0f)
        {
            switch (ControllerTypeLocator.ControllerType)
            {
                case "TAC":
                    if (!AlreadyTalkingToBootloader && !(App._MainFlyoutPage._DeviceListPage._device is DemoDevice))
                    {
                        maxCurrent = Math.Round(App.ViewModelLocator.GetParameter("MAXCURRENT").parameterValue, 1) * 10; // 440A = 7, 600A = 9
                        profileNumber = App.ViewModelLocator.GetParameter("PARPROFILENUMBER").parameterValueInt.ToString();

                        FetchSupplierChainTable(profileNumber);
                    }
                    break;
                case "TSX":
                    if (!AlreadyTalkingToBootloader && !(App._MainFlyoutPage._DeviceListPage._device is DemoDevice))
                    {
                        profileNumber = App.ViewModelLocator.GetParameterTSX("PARPROFILENUMBER").parameterValueInt.ToString();
                        //profileNumber = App.ViewModelLocator.GetParameterTSX("FIRMWARETEST").parameterValueString;
                        maxCurrent = App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue; // 440A = 12, 600A = 10

                        FetchSupplierChainTable(profileNumber);
                    }
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("There is something wrong with ControllerType");
                    break;
            }
        }
        public void FetchSupplierChainTable(string profileNumber)
        {
            currentSupplierChain = null;
            //This works for hardcoding table
            foreach (var supplierChain in InitialSC.SupplierChains)
            {
                if (profileNumber == supplierChain.ProfileNumber &&
                    maxCurrent == System.Convert.ToDouble((int)supplierChain.CurrentRating))
                {
                    currentSupplierChain = supplierChain;
                }
            }

            //if(ChainFromVersion == null)
            //    System.Diagnostics.Debug.WriteLine("firmware does not belong to the list.");
        }

        public void ContinueBuildingFileButtonList(List<FirmwareGroup> firmwareGroups, StackLayout FileButtonList, int childPositionCounter, bool isFullListRequired, bool isCustomScreen = false)
        {
            if (isCustomScreen)
            {
                //Building buttons base on custom screen
                FirmwareGroupStackLayoutCustom(firmwareGroups, FileButtonList, childPositionCounter);
            }
            else
            {
                StackLayout firmwareGroupStackLayout = FirmwareGroupStackLayoutDetermined(firmwareGroups, isFullListRequired);

                //If there is no button exists, do not mind to add a children to the list
                if (buttonCounter > 0)
                {
                    FileButtonList.Children.Insert(childPositionCounter, firmwareGroupStackLayout);
                    childPositionCounter++;
                    buttonCounter = 0;
                }
            }
        }

        public StackLayout FirmwareGroupStackLayoutDetermined(List<FirmwareGroup> firmwareGroups, bool isFullList)
        {
            StackLayout firmwareGroupStackLayout = new StackLayout();

            foreach (SupplierChain supplierChain in InitialSC.SupplierChains)
            {
                if (!isFullList)
                {
                    //filter
                    if (currentSupplierChain.Category == InitialSC.Categories.OEMs)
                    {
                        if (supplierChain.Category == InitialSC.Categories.OEMs &&
                            supplierChain.CompanyName == currentSupplierChain.CompanyName)
                            BuildFirmwareGroup(firmwareGroups, supplierChain, isFullList, ref firmwareGroupStackLayout);
                    }
                    else
                        if (supplierChain.CompanyName == currentSupplierChain.CompanyName)
                        BuildFirmwareGroup(firmwareGroups, supplierChain, isFullList, ref firmwareGroupStackLayout);
                }
                else
                    BuildFirmwareGroup(firmwareGroups, supplierChain, isFullList, ref firmwareGroupStackLayout);
            }

            return firmwareGroupStackLayout;
        }

        public void BuildFirmwareGroup(List<FirmwareGroup> firmwareGroups, SupplierChain supplierChain, bool isFullList, ref StackLayout firmwareGroupStackLayout)
        {
            foreach (FirmwareGroup firmwareGroup in firmwareGroups)
            {
                foreach (FileItem fileItem in firmwareGroup.FileItems)
                {
                    if (!isFullList)
                    {
                        //do not expose OEM's firmwares
                        if (supplierChain.ModelName == fileItem.ModelName &&
                            maxCurrent == System.Convert.ToDouble((int)supplierChain.CurrentRating))
                        {
                            int index = firmwareGroupStackLayout.Children.Select((element, i) => new { element, i }).
                                    FirstOrDefault(v =>
                                    {
                                        var buttonText = ((v.element as Frame)?.Content as Button)?.Text;
                                        if (buttonText != null)
                                        {
                                            Match match = Regex.Match(buttonText, @"\w.*(?=\sV)", RegexOptions.IgnoreCase);
                                            string expectedResult = (match.Success) ? match.Value : "";
                                            return expectedResult.Equals(fileItem.FriendlyFileName);
                                        }
                                        else
                                            return false;
                                    })?.i ?? -1;

                            if (index == -1) //Not found duplicate
                            {
                                BuildFirmwareGroupStackLayout(fileItem, supplierChain, firmwareGroup, ref firmwareGroupStackLayout);
                            }
                        }
                    }
                    else
                    {
                        if (supplierChain.ModelName == fileItem.ModelName &&
                            supplierChain.Category != InitialSC.Categories.OEMs)
                            BuildFirmwareGroupStackLayout(fileItem, supplierChain, firmwareGroup, ref firmwareGroupStackLayout);
                    }
                }
            }
        }

        public void FirmwareGroupStackLayoutCustom(List<FirmwareGroup> firmwareGroups, StackLayout FileButtonList, int childPositionCounter)
        {

            foreach (FirmwareGroup firmwareGroup in firmwareGroups)
            {
                StackLayout firmwareGroupStackLayout = new StackLayout();
                //Iterate Frame And button
                foreach (FileItem fileItem in firmwareGroup.FileItems)
                {
                    BuildFirmwareGroupStackLayout(fileItem, null, firmwareGroup, ref firmwareGroupStackLayout);
                }

                //If there is no button exists, do not mind to add a children to the list
                if (buttonCounter > 0)
                {
                    FileButtonList.Children.Insert(childPositionCounter, firmwareGroupStackLayout);
                    childPositionCounter++;
                    buttonCounter = 0;
                }
            }

        }

        public void BuildFirmwareGroupStackLayout(FileItem fileItem, SupplierChain supplierChain, FirmwareGroup firmwareGroup, ref StackLayout firmwareGroupStackLayout)
        {
            Frame buttonFrame;

            buttonFrame = GetFileButtonFrame(fileItem, supplierChain);

            //If there is multiple button, do not build the same group title over again
            //Make sure, at least one button exist before add this title
            if (firmwareGroup.Title != prevTitle)
            {
                //Create A Group Title
                firmwareGroupStackLayout.Children.Add(BuildGroupTitle(firmwareGroup.Title, groupTitleHexColor));
                prevTitle = firmwareGroup.Title;
            }

            if (buttonCounter > 0 && buttonFrame.Content != null)
            {
                firmwareGroupStackLayout.Children.Add(buttonFrame);
            }
        }
        public Label BuildGroupTitle(string txtGroupTitle, string strTextColor)
        {
            Label groupTitle = new Label()
            {
                Text = txtGroupTitle,
                TextColor = Color.FromHex(strTextColor),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            return groupTitle;
        }
        public Frame GetFileButtonFrame(FileItem fileItem, SupplierChain supplierChain)
        {
            Frame firmwareButtonFrame = new Frame();

            bool isFirmwareScreenExtensionFound = false;
            if (supplierChain == null)
                isFirmwareScreenExtensionFound = true;

            if (fileItem.ActualFileName != "Not Supported")
            {
                string[] fileList = FileManager.GetFirmwareFile(fileItem, isFirmwareScreenExtensionFound);
                if (fileList.Length != 0)
                {
                    string friendlyFileNameAndVersion = FileManager.FirmwareFileNameValidation(fileItem.FriendlyFileName, fileList[0]);
                    if (friendlyFileNameAndVersion != "")
                    {
                        if (currentSupplierChain == null || isFirmwareScreenExtensionFound || (currentSupplierChain != null && !isFirmwareScreenExtensionFound && supplierChain.ModelName == fileItem.ModelName))
                        {
                            firmwareButtonFrame = BuildFrameAndButton(friendlyFileNameAndVersion, fileItem.FriendlyFileName, fileList[0]);
                            buttonCounter++;
                        }
                    }

                }
            }
            else //Not Supported firmware
            {
                firmwareButtonFrame = BuildFrameAndButton(fileItem.FriendlyFileName + "v0.000", null, null);
                (firmwareButtonFrame.Content as Button).IsEnabled = false;
                buttonCounter++;
            }

            return firmwareButtonFrame;
        }
        public Frame BuildFrameAndButton(string friendlyFileNameAndVersion, string friendlyFileName, string filePath)
        {
            Frame publicFile = new Frame
            {
                BorderColor = Color.Black,
                Padding = new Thickness(5, 0),
                Margin = new Thickness(5, 0),
                IsVisible = true,
            };

            Button button = new Button
            {
                Text = friendlyFileNameAndVersion,
            };

            if (filePath != null && friendlyFileName != null)
            {
                string[] FileNameItems = { filePath, friendlyFileName };
                if (ControllerTypeLocator.ControllerType == "TAC")
                    button.Clicked += (sender, args) => (this as FirmwareDownloadPage).OnButtonClicked(button, new EventArgs<string[]>(FileNameItems));
                else
                    button.Clicked += (sender, args) => (this as FirmwareDownloadTSXPage).OnButtonClicked(button, new EventArgs<string[]>(FileNameItems));
            }

            publicFile.Content = button;
            return publicFile;
        }

        public void INITIALIZESETTINGS_Clicked(object sender, EventArgs e)
        {
            Task<bool> task = DisplayAlert("Initialize", "Settings will be over written to factory defaults. Click save to save these settings", "Yes", "Cancel");

            task.ContinueWith(InitializeDismissedCallback);

        }

        private void InitializeDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                QueParameter(new SetParameterEventArgs(49, 1.0f, "never used so get rid of this"));
            }
        }

        public void SAVECHANGES_Clicked(object sender, EventArgs e)
        {

            Task<bool> task = DisplayAlert("Save Changes", "Settings will be permanently saved to controller flash", "Yes", "Cancel");

            task.ContinueWith(SaveDismissedCallback);
        }

        private void SaveDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    activityMessage.Text = "Saving...";
                });

                QueParameter(new SetParameterEventArgs(50, 1.0f, "never used so get rid of this"));

            }
        }

        public void SAVECHANGESTSX_Clicked(object sender, EventArgs e)
        {

            Task<bool> task = DisplayAlert("Save Changes", "Settings will be permanently saved to controller flash", "Yes", "Cancel");

            task.ContinueWith(SaveDismissedTSXCallback);
        }

        private void SaveDismissedTSXCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                QueParameter(new SetParameterEventArgs(199, 1.0f, null));
            }
        }

        public void START_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                (sender as Button).IsEnabled = true;
                return false;
            });
            StartDatalogging();
        }

        public async void btnUploadClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                (sender as Button).IsEnabled = true;
                return false;
            });

            // Finding file with these extension .json .csv, if they exist
            var (JSONFileCounter, CSVFileCounter) = FileManager.SearchForJsonAndCSVExtensions();

            var totalFileCount = JSONFileCounter + CSVFileCounter;
            if (totalFileCount > 0)
            {
                DisplayAlert("", $"Attempting to upload {totalFileCount} remaining file(s), please try again later", "Ok");
            }
            else
            {
                DisplayAlert("", $"There are no remaining files in your external storage", "Ok");
            }
        }

        public void BuildStandardStackLayout(StackLayout tempStackLayout, PageGroup pageGroup)
        {
            StackLayout groupTitleStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };
            //xml <Label  Text="General Vehicle Switches"  TextColor="#367F56"   FontSize="Default"  VerticalOptions="Center" HorizontalOptions="Start" HorizontalTextAlignment="Start"/>
            Label lblGroupTitle = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex(basedTextColor),
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.Start,
                Text = pageGroup.GroupTitle
            };

            if (pageGroup.GroupDescription != null)
            {
                tempStackLayout.Children.Add(BuildGroupTitleStackLayout(lblGroupTitle));
                if (pageGroup.DescriptionColor != null)
                    tempStackLayout.Children.Add(BuildGroupDescriptionStackLayout(pageGroup.GroupDescription, pageGroup.DescriptionColor, pageGroup));
                else
                    tempStackLayout.Children.Add(BuildGroupDescriptionStackLayout(pageGroup.GroupDescription, basedTextColor, pageGroup));
            }
            else
            {
                //groupTitleStackLayout.Children.Add(lblGroupTitle);
                //stacklayout.Children.Add(groupTitleStackLayout);
                tempStackLayout.Children.Add(lblGroupTitle);
            }

            //integrate help to dynamic page
            //numericLabel.SetBinding(Label.TextProperty, new Binding("parameterValueString"));

            //valueView = numericLabel;
            //tapGestureRecognizer = new TapGestureRecognizer
            //{
            //    NumberOfTapsRequired = 2
            //};
            //tapGestureRecognizer.Tapped += SelectForDataLogging;

            foreach (PageItem item in pageGroup.PageItems)
            {
                tempStackLayout.Children.Add(BuildStandardFrameEntry(item));
            }
        }

        private void SetBindingBaseOnUserLevel(string AppConfigurationLevel, StackLayout tempStackLayout, Frame frame = null)
        {
            BindableObject bindableObject;
            if (frame != null)
            {
                bindableObject = frame;
            }
            else
            {
                bindableObject = tempStackLayout;
            }

            //This User Level could grow up in the future
            //So we need to create more User Property (bool) to handle this ?
            switch (AppConfigurationLevel)
            {
                case "ADVANCED_USER":
                    bindableObject.SetBinding(IsVisibleProperty, new Binding("UserLevelEqualOrGreaterThanAdvancedUserProperty"));
                    break;
                case "DEALER":
                    bindableObject.SetBinding(IsVisibleProperty, new Binding("UserLevelEqualOrGreaterThanDealerProperty"));
                    break;
                case "ENG":
                    bindableObject.SetBinding(IsVisibleProperty, new Binding("UserLevelEqualOrGreaterThanEngProperty"));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Method <c>OpenWebView</c> opens html file.
        /// </summary>
        public void OpenWebView(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                (sender as Button).IsEnabled = false;
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    (sender as Button).IsEnabled = true;
                    return false;
                });
            }

            string internalWebPage = "";
            if (App._MainFlyoutPage.UserHasWriteCredentials())
            {
                if (this is DynamicPage)
                    internalWebPage = Regex.Replace($"{(e as WebViewStringEventArgs).internalWebPage}", " ", "", RegexOptions.IgnoreCase);
                if ((e as WebViewStringEventArgs).internalWebPage.Contains("PopUp"))
                {
                    this.Navigation.PushModalAsync(new ScriptingPage(internalWebPage, true));
                }
                else
                {
                    this.Navigation.PushAsync(new ScriptingPage(internalWebPage));
                }
            }
        }

        public Frame BuildFrameForButton(string btnText)
        {
            Frame frame = new Frame()
            {
                BorderColor = Color.Black,
                Padding = new Thickness(5, 1),
                Margin = new Thickness(5, 1)
            };

            Button pageButton = new Button()
            {
                Text = btnText,
                IsEnabled = true
            };

            //Attached to the button click event properly
            if (btnText.Contains("Request Advanced"))
            {
                pageButton.Clicked += (sender, args) => BuildWarningPopUp("Disclaimer");
                frame.HorizontalOptions = LayoutOptions.CenterAndExpand;
            }
            else
            {
                if (btnText.Contains("Datalog"))
                    pageButton.Clicked += START_Clicked;
                else if (btnText.Contains("Initialize"))
                    pageButton.Clicked += INITIALIZESETTINGS_Clicked;
                else if (btnText.Contains("Upload"))
                {
                    pageButton.Clicked += btnUploadClicked;
                }
                else if (btnText.Contains("Save"))
                {
                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        if (btnText.Equals("Save"))
                        {
                            pageButton.Clicked += SAVECHANGES_Clicked;
                        }
                        else if (btnText.Contains("Parameter"))
                        {
                            pageButton.Clicked += (new ParameterFileHandler(this)).SAVECHANGES_Clicked;
                        }
                    }
                    else
                    {
                        if (btnText.Equals("Save Changes"))
                        {
                            pageButton.Clicked += SAVECHANGESTSX_Clicked;
                        }
                        else if (btnText.Contains("Parameter"))
                        {
                            pageButton.Clicked += (new ParameterFileHandler(this)).SAVECHANGES_Clicked;
                        }
                    }
                }
                else if (btnText.Contains(".html"))  // button contains link to file name
                {
                    WebViewStringEventArgs webViewStringEventArgs = new WebViewStringEventArgs();
                    webViewStringEventArgs.internalWebPage = btnText;

                    pageButton.Text = Regex.Replace(btnText, ".html", "", RegexOptions.IgnoreCase);
                    //Only Interal web view could be opened, should we need an external web view as well ?
                    pageButton.Clicked += (sender, e) => OpenWebView(sender, webViewStringEventArgs);
                }
                else if (btnText.Contains("ReadEEPROM"))  // button contains link to file name
                {
                    pageButton.Clicked += READ_Clicked;
                }
                else if (btnText.Contains("Load"))  // button contains link to file name
                {
                    pageButton.Clicked += (new ParameterFileHandler(this)).LoadParametersFromFile_Clicked;
                }
                else if (btnText.Contains("Reset"))  // button contains link to file name
                {
                    pageButton.Clicked += Reset_Clicked;
                }
                frame.HorizontalOptions = LayoutOptions.FillAndExpand;
            }

            frame.Content = pageButton;
            return frame;
        }

        public Grid BuildGridSystem(PageItem item, string AdditionalEntryType = "AddStepper")
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(7, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(3, GridUnitType.Star)}
                },
            };

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            if (AdditionalEntryType == "AddStepper")
                tapGestureRecognizer.Tapped += AddStepper;

            grid.GestureRecognizers.Add(tapGestureRecognizer);

            //Row 0, Col 0
            grid.Children.Add(SetBindingNameToLabel());
            //Row 0, Col 1
            DoneEntry doneEntry = DoneEntryForViewTypeFloat();
            doneEntry.SetBinding(DoneEntry.TextProperty, "parameterValue", BindingMode.OneWay, null, "{0:0.##}");
            grid.Children.Add(doneEntry, 1, 0);

            //Hopefully there is no null value return
            grid.BindingContext = SetupLiveData(item);
            if (AdditionalEntryType == "AddSlider")
                AddSlider(grid, null); //This always shows up without stepper type double tap

            return grid;
        }
        public GoiParameter SetupLiveData(PageItem item)
        {
            GoiParameter parameter;
            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                if (item.PropertyName != null)
                    parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == item.PropertyName);
                else //option to set address
                    parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.Address == item.Address && (x.SubsetOfAddress == false));
            }
            else
            {
                if (item.PropertyName != null)
                    parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.PropertyName == item.PropertyName);
                else
                    parameter = App.ViewModelLocator.MainViewModelTSX.GoiParameterList.FirstOrDefault(x => x.Address == item.Address && (x.SubsetOfAddress == false));
            }

            //Make sure we don't miss GroupedParameters, because they are not in the xaml
            BuildCommunicationsList(PageParameters.parameterList, parameter);

            return parameter;
        }

        public DoneEntry DoneEntryForViewTypeFloat()
        {
            //  < local:DoneEntry Text = "{Binding Path=parameterValueString,  StringFormat='{0:F2}'}" Keyboard = "Numeric" Focused = "ParameterFocused" Unfocused = "ParameterUnfocused" Completed = "ParameterCompleted" TextColor = "Black" FontSize = "Default" VerticalOptions = "Center"  IsPassword = "False" IsVisible = "True" HorizontalOptions = "FillAndExpand" HorizontalTextAlignment = "End"   WidthRequest = "100" >
            //      < local:DoneEntry.HorizontalOptions >
            //      < OnPlatform  x: TypeArguments = "LayoutOptions" iOS = "FillAndExpand"  Android = "EndAndExpand" />
            //      </ local:DoneEntry.HorizontalOptions >
            //  </ local:DoneEntry >

            DoneEntry doneEntry = new DoneEntry
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Keyboard = Keyboard.Numeric,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                IsPassword = false,
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.End,
                WidthRequest = 100
            };
            //slider gets automatically updated with one way from source
            //writing the value is from Slider.ValueChanged
            doneEntry.Focused += ParameterFocused;
            doneEntry.Unfocused += ParameterUnfocused;
            doneEntry.Completed += ParameterCompleted;

            return doneEntry;
        }

        public Label SetBindingNameToLabel()
        {
            //< Label Text = "{Binding Path=Name,  StringFormat='{0}'}"  TextColor = "Black"   FontSize = "Default" VerticalOptions = "Center" HorizontalOptions = "FillAndExpand" HorizontalTextAlignment = "Start" >
            //</Label>

            Label label = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                TextColor = Color.Black,
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
            };
            label.SetBinding(Label.TextProperty, new Binding("Name"));

            return label;
        }

        private Label BuildNumericLabel()
        {
            Label numericLabel = new Label
            {
                HorizontalOptions = LayoutOptions.End,
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                IsVisible = true,
                HorizontalTextAlignment = TextAlignment.End,
                WidthRequest = 100
            };

            return numericLabel;
        }

        private Label CreateHelperLabel(string text)
        {
            return new Label()
            {
                //FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label)),
                //TextColor = Color.Gray,
                //LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                Text = $"{text}",
                IsVisible = false
            };
        }
        public void GetDescriptionFromBinding(ref List<string> wholeDescription)
        {
            //Testing is easier at http://regexstorm.net/tester
            //string matchString = @"(?<=EZGO_RXV_440A[\s\S].*Description:).*?(?= Binding:)";
            //TODO: just can't get regex searches like above to work..They do work in the online tester
            //so do it the old fashion way
            bool previousLineHadABinding = false;
            int indexOfLine = 0;
            foreach (var line in wholeDescription)
            {//get the word after Binding:
             //example Binding:PARPROFILENUMBER.enumListName(EZGO_RXV_440A,EZGO_RXV_600A,EZGO_RXV2016_440A,EZGO_RXV2016_600A)
             //this gets PARPROFILENUMBER
             //if no binding then grab the last description
             //if no ending default description (just a binding which does not apply) then empty it
                string matchString2 = @"(?<=Binding:)\s*(\w*)";//gets things like word after Binding: ie ., PARPROFILENUMBER
                Match bindingThing = Regex.Match(line, matchString2);
                //then get the next work after a leading .
                matchString2 = @"(?<=" + bindingThing.Value + @".)\s*(\w*)"; //gets things like word after PARPROFILENUMBER. ie., enumListName
                Match bindingThingType = Regex.Match(line, matchString2);

                if (indexOfLine == wholeDescription.Count - 1)
                {//we got to the end and nothing of interest was found so empty it
                    wholeDescription = wholeDescription.Skip(indexOfLine).ToList<string>();
                    break; //break out of foreach
                }
                if (line.Contains("Description:") && !previousLineHadABinding)
                {//hopefully this default description was placed at the end
                 //Description has nothing to do with model or no matches before this
                 //so remove everything before this
                 //not on the same line as the <Description> tag
                    wholeDescription = wholeDescription.Skip(indexOfLine).ToList<string>();
                    break; //break out of foreach
                }
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    //check for a match in the line and remove everything up to and including line containing profile number (model)
                    if (bindingThingType.Value == "enumListName" && line.Contains(App.ViewModelLocator.GetParameter(bindingThing.Value).parameterEnumString))
                    {
                        wholeDescription = wholeDescription.Skip(indexOfLine + 1).ToList<string>();
                        break; //break out of foreach
                    }
                    if (bindingThingType.Value == "parameterValue" && line.Contains("(" + App.ViewModelLocator.GetParameter(bindingThing.Value).parameterValue + ")"))
                    {
                        wholeDescription = wholeDescription.Skip(indexOfLine + 1).ToList<string>();
                        break; //break out of foreach
                    }
                }
                else
                {
                    //check for a match in the line and remove everything up to and including line containing profile number (model)
                    if (bindingThingType.Value == "enumListName" && line.Contains(App.ViewModelLocator.GetParameterTSX(bindingThing.Value).parameterEnumString))
                    {
                        wholeDescription = wholeDescription.Skip(indexOfLine + 1).ToList<string>();
                        break; //break out of foreach
                    }
                    if (bindingThingType.Value == "parameterValue" && line.Contains("(" + App.ViewModelLocator.GetParameterTSX(bindingThing.Value).parameterValue + ")"))
                    {
                        wholeDescription = wholeDescription.Skip(indexOfLine + 1).ToList<string>();
                        break; //break out of foreach
                    }
                }

                //else if(other bindings like boolean or equals ...) //don't do a case statement here because the break out of the foreach just breaks out of a case:


                //set up to look for a Description: tag that was not preceded by any binding as a default description
                if (bindingThing.Value.Length != 0)
                    previousLineHadABinding = true;
                else
                    previousLineHadABinding = false;
                indexOfLine++;
            }
        }
        public void RemoveAfterPart(ref List<string> wholeDescription)
        {
            var indexOfLine = 0;
            foreach (var line in wholeDescription)
            {//remove everthing including and after the next "Binding:" or the second "Description:"
                if (line.Contains("Binding:"))
                {//another Binding: was found so clip everthing following
                    wholeDescription = wholeDescription.Take(indexOfLine).ToList<string>();
                    break;
                }
                if (line.Contains("Description:") && indexOfLine != 0)
                {//if the first line is description it is valid, any others need to be clipped
                    wholeDescription = wholeDescription.Take(indexOfLine).ToList<string>();
                    break;
                }
                indexOfLine++;
            }
        }
        public void RemoveSpaceBeforeAndAfter(ref List<string> wholeDescription)
        {
            //Visual Studio adds leading spaces while auto formatting the dictionary descriptions
            //they line up nice so make sure you do it if using other editor
            int someEditorsAddThisManySpacesToXML = wholeDescription[0].Length - wholeDescription[0].TrimStart(' ').Length - 1;
            for (var index = 0; index < wholeDescription.Count; index++)
                if (wholeDescription[index].Length > someEditorsAddThisManySpacesToXML)
                    wholeDescription[index] = wholeDescription[index].Substring(someEditorsAddThisManySpacesToXML);
        }


        public async Task<string> ReplaceFormatData(string parsedDescription)
        {
            if (parsedDescription.Contains("String.Format"))
            {
                string pattern = @"\(S(.*?)\)\)";
                Regex FormatingData = new Regex(pattern);
                if (FormatingData.IsMatch(parsedDescription))
                {
                    Match m = Regex.Match(parsedDescription, (pattern));
                    var FormatParm = (m.Value.Substring(1, m.Value.Length - 2)).Split(new string[] { ", " }, StringSplitOptions.None);
                    pattern = @"{.*?}";
                    Match DataFormat = Regex.Match(FormatParm[0], (pattern));
                    var PropertyName = FormatParm[1].Substring(0, FormatParm[1].Length - 1);

                    if (DataFormat.Value != null)
                    {
                        await ReadOneParameter(App.ViewModelLocator.GetParameter(PropertyName));
                        var Value = String.Format(DataFormat.Value, (App.ViewModelLocator.GetParameter(PropertyName).parameterValue));
                        return (parsedDescription.Replace(m.Value, Value));
                    }
                }
            }
            return parsedDescription;
        }
        public void ImageProcessing(ref string dText, Xamarin.Forms.ScrollView harness)
        {
            string[] imageFiles = { };
            string[] imageOverlayFiles = { };

            ExtractImagesFromDescription(ref dText, ref imageFiles, ref imageOverlayFiles);
            AddImageToHelperScreen(imageFiles, imageOverlayFiles, harness);
        }
        public void ExtractImagesFromDescription(ref string dText, ref string[] imageFiles, ref string[] imageOverlayFiles)
        {
            dText = dText.Replace("Images:", "");
            int startIndex = dText.IndexOf("(");
            int stopIndex = dText.IndexOf(")");
            //get files
            string lineOfImages = dText.Substring(startIndex + 1, stopIndex - startIndex - 1);
            //remove from description
            dText = dText.Remove(startIndex, stopIndex - startIndex + 1);
            //better be comma delimited without spaces
            imageFiles = lineOfImages.Split(',');

            if (dText.Contains("(ImageOverlays:"))
            {
                dText = dText.Replace("ImageOverlays:", "");
                startIndex = dText.IndexOf("(");
                stopIndex = dText.IndexOf(")");
                //get files
                lineOfImages = dText.Substring(startIndex + 1, stopIndex - startIndex - 1);
                //remove from description
                dText = dText.Remove(startIndex, stopIndex - startIndex + 1);
                //better be comma delimited without spaces
                imageOverlayFiles = lineOfImages.Split(',');
            }
        }
        public void AddImageToHelperScreen(string[] imageFiles, string[] imageOverlayFiles, Xamarin.Forms.ScrollView harness)
        {
            //add files to screen
            Size size = Device.Info.PixelScreenSize;
            StackLayout harnessImagesContainer = new StackLayout { Orientation = StackOrientation.Horizontal };

            var platformScale = 1.0;
            if (Device.RuntimePlatform == Device.iOS)
                platformScale = 2.0;

            int indexOfOverlay = 0;

            foreach (var imageFile in imageFiles)
            {
                Grid harnessOverlays = new Grid { Padding = new Thickness(5, 0) };
                harnessOverlays.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                harnessOverlays.Children.Add(new Image { Source = imageFile, WidthRequest = size.Width * 0.3 * platformScale, Aspect = Aspect.AspectFit }, 0, 0);

                if (imageOverlayFiles.Length != 0) //you need to have them equal to match up
                {
                    harnessOverlays.Children.Add(new Image { Source = imageFile.Replace(".png", imageOverlayFiles[0]), WidthRequest = size.Width * 0.3 * platformScale, Aspect = Aspect.AspectFit }, 0, 0);
                }
                indexOfOverlay++;
                harnessImagesContainer.Children.Add(harnessOverlays);
            }

            harness.Content = harnessImagesContainer;
        }

        private StackLayout CreateHelpImages(string[] imageFiles)
        {
            StackLayout visibleImgContainer = null;
            Size size = Device.Info.PixelScreenSize;

            bool visibleImgContainerExists = false;
            foreach (var helpImageFileName in imageFiles)
            {
                Image img = new Image()
                {
                    WidthRequest = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density, // This calculation return the whole screen width
                    Aspect = Aspect.AspectFit,                                                                  // regardless any different density from each devices
                };

                // search for file
                var filelist = FileManager.GetExternalFirstOrInternalDirectoryFilesContaining(helpImageFileName);
                if (filelist.Length != 0)
                {
                    //below img.Source frofm embedded resources is handled differently
                    if (FileManager.isExternalFile(filelist[0]))
                        img.Source = filelist[0];
                    else
                        img.Source = helpImageFileName;

                    if (FileManager.isExternalFile(img.Source.ToString()) && Device.RuntimePlatform == Device.Android)
                    {
                        //A decent height is required, when the images are loaded externally on Android
                        img.HeightRequest = (size.Height / DeviceDisplay.MainDisplayInfo.Density) * 0.3; // 30% of the screen size which calculate from the tab bar up to the top
                    }

                    if (!visibleImgContainerExists)
                    {
                        visibleImgContainer = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = AbsoluteLayout.AutoSize
                        };

                        visibleImgContainerExists = true;
                    }

                    visibleImgContainer.Children.Add(img);
                }
            }

            return visibleImgContainer;
        }

        public void AddPropertyDescription(TextType blockTextType, StackLayout frameChildStackLayout, string dText)
        {
            //kind of toggling descriptions where we don't add these if they were already added
            //but if there are none we will continually come here, but it does not matter

            //see https://forums.xamarin.com/discussion/173486/label-with-texttype-html-not-working-on-android why android not following font sizing
            //probably does not support style= at all, it does support <font color=...> not sure about iOS
            //TODO consider
            //if (Device.RuntimePlatform == Device.Android)
            //{//iOS has no OnElementPropertyChanged in its webview but this gets the lastest file
            //    descriptionLabel.FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label))
            //}
            //else
            //{
            //    below stuff
            //}
            var fontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));

            //Take advantage of the List, so we could position the images easier base on its index
            //Moreover 
            var listDText = dText.Split('\n').ToList<string>();

            if (listDText[listDText.Count - 1].Trim() == "")
            {
                listDText.RemoveAt(listDText.Count - 1);
            }

            var htmlString = "";

            for (int i = 0; i < listDText.Count; i++)
            {
                //Next time we could applied this to any keywords
                if (listDText[i].Contains("(VisibleImages:"))
                {
                    listDText[i] = listDText[i].Replace("VisibleImages:", "");
                    int startIndex = listDText[i].IndexOf("(");
                    int stopIndex = listDText[i].IndexOf(")");
                    //get files
                    string lineOfImages = listDText[i].Substring(startIndex + 1, stopIndex - startIndex - 1);

                    string[] imageFiles = lineOfImages.Split(',');
                    Xamarin.Forms.ScrollView imgHelperScrollView = new Xamarin.Forms.ScrollView()
                    {
                        Orientation = ScrollOrientation.Horizontal,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    StackLayout visibleImgContainer = CreateHelpImages(imageFiles);
                    if (visibleImgContainer != null)
                    {
                        imgHelperScrollView.Content = visibleImgContainer;
                        imgHelperScrollView.IsVisible = false;
                        frameChildStackLayout.Children.Add(imgHelperScrollView);
                    }
                } //If next line contains a keyword or the last line, the description supposed to be added to the stacklayout
                else if (((i < listDText.Count - 1) && listDText[i + 1].Contains("(VisibleImages:")) || (i == listDText.Count - 1))
                {
                    Label descriptionLabel = CreateHelperLabel("Description");
                    htmlString += listDText[i];
                    descriptionLabel.Text = "<p style='font-size:" + (fontSize).ToString() + "px'>" + htmlString + "</p>";
                    descriptionLabel.TextType = blockTextType;
                    frameChildStackLayout.Children.Add(descriptionLabel);
                    htmlString = "";
                }
                else
                {
                    //fullfiling the html text until it is ready to be added later
                    htmlString += listDText[i];
                }
            }
        }
        public void AddMoreTroubleshootingSection(TextType blockTextType, StackLayout frameChildStackLayout, string parsedDescription, Xamarin.Forms.ScrollView harness)
        {
            var matchString = @"[\s\S].*Description:(.|\n)*(?=Troubleshooting)";
            var fontSize = Device.GetNamedSize(NamedSize.Default, typeof(Label));
            if (Regex.IsMatch(parsedDescription, matchString))
            {
                Label moreTroubleshootingLabel = CreateHelperLabel("More Troubleshooting >");
                moreTroubleshootingLabel.TextColor = Color.Black;
                moreTroubleshootingLabel.Padding = new Thickness(10); //give it more room to tap

                Label troubleshootingLabel = CreateHelperLabel("Troubleshooting Notes:");
                troubleshootingLabel.Text = "<p style='font-size:" + fontSize.ToString() + "px'>" + Regex.Replace(parsedDescription, matchString, "") + "</p>";
                troubleshootingLabel.TextType = blockTextType;

                frameChildStackLayout.Children.Add(moreTroubleshootingLabel);
                frameChildStackLayout.Children.Add(harness);
                frameChildStackLayout.Children.Add(troubleshootingLabel);

                TapGestureRecognizer expandMore = new TapGestureRecognizer();
                expandMore.Tapped += ExpandCollapseTroubleshooting;
                expandMore.NumberOfTapsRequired = 1;

                moreTroubleshootingLabel.GestureRecognizers.Add(expandMore);
                troubleshootingLabel.GestureRecognizers.Add(expandMore);
                harness.GestureRecognizers.Add(expandMore);
            }
        }

        public void ToggleHelperVisibility(StackLayout frameChildStackLayout)
        {
            var descriptionIsVisible = false;
            Label moreTroubleShootingLabel = null;
            List<View> troubleShootingViews = new List<View>();

            foreach (Xamarin.Forms.VisualElement element in frameChildStackLayout.Children)
            {
                if (element is Label label)
                {
                    if (label.Text.Contains("Description"))
                    {
                        label.IsVisible = !label.IsVisible;
                        descriptionIsVisible = label.IsVisible;
                    }

                    //it must mean "Troubleshooting:" or "Description:" are not filled in in the dictionary,
                    //"More Troubleshooting >" is always filled int so....

                    //if there is no Troubleshooting data so remove the "More Troubleshooting >"
                    moreTroubleShootingLabel = (Label)frameChildStackLayout.Children.Where(c => (c is Label) && (c as Label).Text.Contains("More Troubleshooting >")).FirstOrDefault();
                    if (moreTroubleShootingLabel != null) moreTroubleShootingLabel.IsVisible = false;

                    if (label.Text.Contains("Troubleshooting Notes:") || label.Text.Contains("Causes:"))
                    { //For first time through and to keep in sync with ExpandCollapseTroubleshooting()
                      //force "More Troubleshooting >" to visible
                        troubleShootingViews.Add(label);
                        moreTroubleShootingLabel = (Label)frameChildStackLayout.Children.Where(c => (c is Label) && (c as Label).Text.Contains("More Troubleshooting >")).FirstOrDefault();
                        if (moreTroubleShootingLabel != null) moreTroubleShootingLabel.IsVisible = true;
                    }
                }
                else if (element is Xamarin.Forms.ScrollView scrollView)
                {
                    scrollView.IsVisible = !scrollView.IsVisible;
                    troubleShootingViews.Add(scrollView);
                }
            }

            if (!descriptionIsVisible)
            {
                //Don't show any
                //hide parallel elements
                foreach (var view in troubleShootingViews)
                {
                    view.IsVisible = false;
                }
            }
        }
        private Xamarin.Forms.Picker BuildPicker()
        {
            Xamarin.Forms.Picker picker = new Xamarin.Forms.Picker();

            picker.SetBinding(Xamarin.Forms.Picker.ItemsSourceProperty, new Binding("enumListName"));
            picker.SetBinding(Xamarin.Forms.Picker.SelectedItemProperty, new Binding("parameterEnumString"));
            picker.ItemDisplayBinding = new Binding(".");
            picker.FontSize = 16;
            picker.HorizontalOptions = LayoutOptions.FillAndExpand;
            picker.HorizontalTextAlignment = TextAlignment.End;
            picker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);

            return picker;
        }


        // EEPROM Read
        int sector_num;
        int fileBlockCounter = 0;
        int fileBlockIndex = 0;
        int fileBlockComplete = 0;
        int[] fileBlock;
        int[] fileBlock_6;
        int[] OTFIndex = { 19, 31, 73, 74, 75 };
        int[] OTFIndex81 = { 24, 36, 78, 79, 80 };

        int MaxParameterOffset = 0;

        private bool S6EqualS7Check()
        {
            int Index = 0;
            int i = 0;
            bool result = false;

            //0xcccc Valid 0xcc88 page Invalid parameter page
            // Firmware verstion >= 8.4 should not have this fault
            // Firmware verstion >= 8.1 Parameter structure is changed
            if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= 8.100)
            {
                if ((fileBlock_6[0] == 0xcccc && fileBlock[0] == 0xcc88) || (fileBlock_6[0] == 0xcc88 && fileBlock[0] == 0xcccc))
                {
                    if (fileBlock_6[1] == fileBlock[1])
                    {
                        Index = fileBlock[1];
                        i = 2;
                    }
                }
                else Index = -1;
            }
            else if ((fileBlock_6.IndexOf(0xcccc) == fileBlock.IndexOf(0xcc88)) || (fileBlock.IndexOf(0xcccc) == fileBlock_6.IndexOf(0xcc88)) || (fileBlock.IndexOf(0xcccc) == fileBlock_6.IndexOf(0xffff)))
            {
                if (fileBlock.IndexOf(0xcccc) != -1) Index = fileBlock.IndexOf(0xcccc);
                else Index = fileBlock_6.IndexOf(0xcccc);
                if (fileBlock_6[Index - 1] != fileBlock[Index - 1]) Index = -1;
            }
            for (; i < Index; i++)
            {
                if (App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= 8.100)
                {
                    if (!Array.Exists(OTFIndex, element => element == i))
                    {
                        if (fileBlock_6[i] != fileBlock[i]) break;
                    }
                }
                else
                {
                    if (!Array.Exists(OTFIndex81, element => element == i))
                    {
                        if (fileBlock_6[i] != fileBlock[i]) break;
                    }
                }
            }
            if (i == Index) result = true;

            MaxParameterOffset = Index;
            return result;
        }
        private void Reset_Clicked(object sender, EventArgs e)
        {
            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("Otp_dcbusScale").Address, (float)1.0, null));
            QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("Otp_logicPowerScale").Address, (float)1.0, null));
        }
        private void READ_Clicked(object sender, EventArgs e)
        {
            ReadVEEPROM(true);
        }
        /// <summary>
        /// File Utilities for uploading Veeprom
        /// </summary>
        /// <param name="isRestore"></param>
        /// <returns></returns>
        public async Task ReadVEEPROM(bool isRestore)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                activityMessage.Text = "0%";
                PageIsBusy = true; //you can do it this way as well as message center below
            });

            MessagingCenter.Send<NavitasGeneralPage>(this, "ShowActivity");
            foreach (var x in DeviceComunication.PageCommunicationsListPointer)
            {
                x.parentPage.Active = false;
            }

            sector_num = 6; // First sector containing eeprom data
            App._devicecommunication.SetFlashBlockResponseHandler(this.ResponseFromFlashBlock);
            int offset = sector_num * 256;
            fileBlockCounter = 0;
            fileBlockIndex = 0;
            fileBlockComplete = 0; //respond leaves this at 1
            fileBlock = new int[8192]; //starts a fresh 0 size on instead of reusing the last
            fileBlock_6 = new int[8192]; //starts a fresh 0 size on instead of reusing the last
            System.Diagnostics.Debug.WriteLine("Attempting to retreive Sector 6.");
            for (int i = 0; ((i < 128) && (fileBlockComplete == 0)); i++)   // 128 reads of 64 words = 8K sector
            {
                //good lesson about async and threads and ui threads
                //without the below await no ui updates are done, meaning this routine has total blocked the ui thread
                //and is running synchronously
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityMessage.Text = ((int)((float)(fileBlockCounter) / 128 * 100)).ToString() + " %";
                });
                QueParameter(new SetParameterEventArgs(0x25, i + offset, null));


                int starttime = DateTime.Now.Millisecond;
                int timeout = DateTime.Now.Millisecond;

                while ((fileBlockCounter != (i + 1)) && (timeout < 1000))
                {
                    timeout = DateTime.Now.Millisecond - starttime;
                    await Task.Delay(50); //For any blocking while something, this method needs to be awaitable and await this to free up UI time
                }

                if (timeout >= 1000)
                {
                    System.Diagnostics.Debug.WriteLine("Timed out receiving sector 6 contents. block = " + fileBlockCounter.ToString() + " at " + DateTime.Now.Second.ToString());
                }

                System.Diagnostics.Debug.WriteLine("Receiving Sector 6 Contents. block = " + fileBlockCounter.ToString() + " at " + DateTime.Now.Second.ToString());
            }
            Array.Copy(fileBlock, fileBlock_6, 8192);

            sector_num = 7;  // Second sector containing eeprom data
            offset = sector_num * 256;
            fileBlockCounter = 0;
            fileBlockIndex = 0;
            fileBlockComplete = 0; //respond leaves this at 1
            fileBlock = new int[8192]; //starts a fresh 0 size on instead of reusing the last
            System.Diagnostics.Debug.WriteLine("Attempting to retreive Sector 7.");
            for (int i = 0; ((i < 128) && (fileBlockComplete == 0)); i++)   // 128 reads of 64 words = 8K sector
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityMessage.Text = ((int)((float)(fileBlockCounter) / 128 * 100)).ToString() + " %";
                });
                QueParameter(new SetParameterEventArgs(0x25, i + offset, null));

                int starttime = DateTime.Now.Millisecond;
                int timeout = DateTime.Now.Millisecond;

                while ((fileBlockCounter != (i + 1)) && (timeout < 1000))
                {
                    timeout = DateTime.Now.Millisecond - starttime;
                    await Task.Delay(50); //For any blocking while something, this method needs to be awaitable and await this to free up UI time
                }

                if (timeout >= 1000)
                {
                    System.Diagnostics.Debug.WriteLine("Timed out receiving Sector 7 Contents. block = " + fileBlockCounter.ToString() + " at " + DateTime.Now.Second.ToString());
                }

                System.Diagnostics.Debug.WriteLine("Receiving Sector 7 Contents. block = " + fileBlockCounter.ToString() + " at " + DateTime.Now.Second.ToString());
            }

            // Only upload the file when this function is called from restore button and the parameter fault is not caused by OTF Changes
            if (!isRestore || (isRestore && (!S6EqualS7Check())))
            {
                FileManager.CreateVeepromFileAndUpload(isRestore, fileBlock_6, fileBlock);
            }
            else // isRestore and s6 == s7
            {
                foreach (var v in App.ViewModelLocator.MainViewModel.GoiParameterList)
                {
                    GoiParameter parameter = App.ViewModelLocator.GetParameter(v.PropertyName);

                    var realFlashOffset = parameter.FlashOffset;

                    if ((App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue >= 8.100) && (parameter.FlashOffset != -1))
                    {
                        if (realFlashOffset < 178) realFlashOffset += 5;
                        else realFlashOffset += 4;  // version <v8.1 The first long variable start is in the odd position the offset is added one in def, it should be sustract for 8.1
                    }

                    if ((parameter.Address != -1) && (realFlashOffset != -1))
                    {
                        if (realFlashOffset < MaxParameterOffset)
                        {
                            var a = parameter.rawValue;
                            var b = fileBlock[realFlashOffset];

                            if (a != b)
                            {
                                parameter.rawValue = (ushort)b;
                                if (parameter.parameterValue <= parameter.MaximumParameterValue && parameter.parameterValue >= parameter.MinimumParameterValue)
                                {
                                    //TODO if b in a right range
                                    SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (ushort)b, null);
                                    QueParameter(SetParameterEventArgs);
                                }
                            }
                        }
                    }
                }
            }
            //TODO if restore is caused by OTF changes, using parameters pulled from eeprom and replaced the default parameters before save

            PageParameters.Active = true;
            MessagingCenter.Send<NavitasGeneralPage>(this, "StopActivity");
        }

        /// <summary>
        /// File Utilities for uploading Veeprom
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public void ResponseFromFlashBlock(object sender, FlashGetBlockResponseEventArgs e)//List<byte> listOfBytes
        {
            int value;
            List<byte> listOfBytes = e.listOfBytesFromResponse;
            fileBlockComplete = 1;  // Assume we have all the non-0xFFFF data from the sector until we find valid data.

            //System.Diagnostics.Debug.WriteLine("Receiving Sector Contents.");
            for (int i = 0; i < listOfBytes.Count; i += 2)
            {
                value = ((int)listOfBytes[i] << 8) | (int)listOfBytes[i + 1];
                fileBlock[fileBlockIndex++] = value;

                if (value != 0xFFFF)    // Still getting new data.
                {
                    fileBlockComplete = 0;
                }
            }

            fileBlockCounter += 1;
        }

        public ContentView fileListPopUp;
        public StackLayout FileButtonList;
        public void BuildfileListPopUp()
        {
            AbsoluteLayout overlayArea = this.Content as AbsoluteLayout;



            fileListPopUp = new ContentView()
            {
                IsVisible = true,
                BackgroundColor = Color.FromHex("#C0202020"),
                //                Padding = new Thickness(10, 0)
            };
            AbsoluteLayout.SetLayoutBounds(fileListPopUp, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(fileListPopUp, AbsoluteLayoutFlags.All);

            //                                                          x, y, width, height

            Xamarin.Forms.ScrollView popupScrollView = new Xamarin.Forms.ScrollView()
            {
                Orientation = ScrollOrientation.Vertical,
            };
            //                                                          x, y, width, height
            AbsoluteLayout.SetLayoutBounds(popupScrollView, new Rectangle(20, 50, 1, 1));
            AbsoluteLayout.SetLayoutFlags(popupScrollView, AbsoluteLayoutFlags.SizeProportional);

            StackLayout popupStackLayout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#C2C3C6"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10),
            };


            FileButtonList = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            popupStackLayout.Children.Add(FileButtonList);

            popupScrollView.Content = popupStackLayout;
            fileListPopUp.Content = popupScrollView;
            overlayArea.Children.Add(fileListPopUp);

        }

    }
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T val)
        {
            Value = val;
        }
    }



    public class PinchToZoomContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;
        double width = 0;
        double height = 0;
        double startX = 0;
        double startY = 0;

        public PinchToZoomContainer()
        {
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (!width.Equals(Content.Width) && !height.Equals(Content.Height))
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Started:
                        startX = Content.TranslationX;
                        startY = Content.TranslationY;
                        break;
                    case GestureStatus.Running:
                        if (!width.Equals(0))
                        {
                            Content.TranslationX = Math.Max(Math.Min(0, startX + e.TotalX), -Math.Abs(Content.Width - width));// App.ScreenWidth));
                        }
                        if (!height.Equals(0))
                        {
                            Content.TranslationY = Math.Max(Math.Min(0, startY + e.TotalY), -Math.Abs(Content.Height - height)); //App.ScreenHeight));    
                        }
                        break;
                    case GestureStatus.Completed:
                        // Store the translation applied during the pan
                        startX = Content.TranslationX;
                        startY = Content.TranslationY;
                        xOffset = Content.TranslationX;
                        yOffset = Content.TranslationY;
                        break;
                }
            }
        }
    }

    public class WebViewStringEventArgs : EventArgs
    {
        public string internalWebPage { get; set; }
    }

}