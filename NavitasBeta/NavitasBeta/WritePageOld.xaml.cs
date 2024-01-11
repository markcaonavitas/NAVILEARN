using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NavitasBeta
{
    public partial class WritePageOld : ContentPage
    {
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        public WritePageOld()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            BindingContext = ViewModelLocator.MainViewModel;
            if (Device.OS == TargetPlatform.iOS)
            {
                MessagingCenter.Subscribe<DeviceComunication>(this, "Hi", (sender) =>
                {
                    // do something whenever the "Hi" message is sent
                    DisplayMessage();
                });
            }

        }

        void DisplayMessage()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                this.DisplayAlert("Error", "Device Communication timeout", "OK");
            });
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string strParameterName = "Default";
            byte bparameterNumber = 0;

            if (button == SPEEDOMETERMAXSPEED)
            {
                bparameterNumber = 154;
                strParameterName = "Speedometer Max Speed";
            }
            else if (button == FLAGGATEENABLE)
            {
                bparameterNumber = 63;
                strParameterName = "Flag Gate Enable";
            }
            else if (button == SERIALNUMBERHIGH)
            {
                bparameterNumber = 169;
                strParameterName = "Serial Number High";
            }
            else if (button == SERIALNUMBERLOW)
            {
                bparameterNumber = 170;
                strParameterName = "Serial Number Low";
            }
            else if (button == BRAKEMIN)
            {
                bparameterNumber = 179;
                strParameterName = "Brake Min";
            }
            else if (button == BRAKEMAX)
            {
                bparameterNumber = 180;
                strParameterName = "Brake Max";
            }
            else if (button == BRAKEFULL)
            {
                bparameterNumber = 181;
                strParameterName = "Brake Full";
            }
            else if (button == THROTTLEMIN)
            {
                bparameterNumber = 95;
                strParameterName = "Throttle Min";
            }
            else if (button == THROTTLEMAX)
            {
                bparameterNumber = 96;
                strParameterName = "Throttle Max";
            }
            else if (button == THROTTLEFULL)
            {
                bparameterNumber = 182;
                strParameterName = "Throttle Full";

            }
            else if (button == TIRERADIUS)
            {
                bparameterNumber = 155;
                strParameterName = "Tire Radius";
            }
            else if (button == REARAXLERATIO)
            {
                bparameterNumber = 156;
                strParameterName = "Rear Axle Ratio";
            }
            else if (button == MILESORKILOMETERS)
            {
                bparameterNumber = 157;
                strParameterName = "0 for miles and 1 for kilometers";  
            }
            else if (button == HILLBRAKESTRENGTH)
            {
                bparameterNumber = 158;
                strParameterName = "Hill Brake Strength";
            }
#if OV_TRIP_REMOVED
            else if (button == OVTripV)
            {
                bparameterNumber = 53;
                strParameterName = "O/V Trip (V)";
            }
#endif 
            else if (button == THROTTLECENTER)
            {
                bparameterNumber = 166;
                strParameterName = "Thottle Center";
            }
            else if (button == OTTripC)
            {
                bparameterNumber = 55;
                strParameterName = "O/T Trip (C)";
            }
#if UV_TRIP_REMOVED
            else if (button == UVTRIPV)
            {
                bparameterNumber = 52;
                strParameterName = "U/V Trip (V)";
            }
#endif 
            else if (button == OCTRIPA)
            {
                bparameterNumber = 54;
                strParameterName = "O/C Trip (A)";
            }
            else if (button == AGAINAV)
            {
                bparameterNumber = 132;
                strParameterName = "A gain A/V";
            }
            else if (button == CGAINAV)
            {
                bparameterNumber = 48;
                strParameterName = "C gain A/V";
            }
            else if (button == NONFATALOTTRIPC)
            {
                bparameterNumber = 90;
                strParameterName = "non fatal O/T Trip (C)";
            }
            else if (button == MOTTRIPC)
            {
                bparameterNumber = 117;
                strParameterName = "M O/T Trip (C)";
            }
            else if (button == SAVEPARAMS)
            {
                bparameterNumber = 50;
                strParameterName = "Save Params";
            }
            else if (button == INITPARAMS)
            {
                bparameterNumber = 49;
                strParameterName = "Init Params";
            }
            else if (button == TESTMODE)
            {
                bparameterNumber = 60;
                strParameterName = "Test Mode";
            }
            else if (button == RATEDIRMS)
            {
                bparameterNumber = 69;
                strParameterName = "Rated Irms";
            }
            else if (button == RATEDVRMS)
            {
                bparameterNumber = 68;
                strParameterName = "Rated Vrms";
            }
            else if (button == RATEDFREQ)
            {
                bparameterNumber = 67;
                strParameterName = "Rated Freq";
            }
            else if (button == POLEPAIRS)
            {
                bparameterNumber = 65;
                strParameterName = "Pole Pairs";
            }
            else if (button == ENCPPR)
            {
                bparameterNumber = 64;
                strParameterName = "Enc PPR";
            }
            else if (button == RISERPMSEC)
            {
                bparameterNumber = 72;
                strParameterName = "Rise RPM/sec";
            }
            else if (button == FALLRPMSEC)
            {
                bparameterNumber = 73;
                strParameterName = "Fall RPM/sec";
            }
            else if (button == RISEHZSEC)
            {
                bparameterNumber = 79;
                strParameterName = "Rise Hz/sec";
            }
            else if (button == FALLHZSEC)
            {
                bparameterNumber = 80;
                strParameterName = "Fall Hz/sec";
            }
            else if (button == ENCRPMSEC)
            {
                bparameterNumber = 85;
                strParameterName = "Enc RPM/sec";
            }
            else if (button == DEADTIMECORRECTION)
            {
                bparameterNumber = 108;
                strParameterName = "Dead time correction";
            }
            else if (button == MSLIPVRMS)
            {
                bparameterNumber = 77;
                strParameterName = "M slip Vrms";
            }
            else if (button == RSLIPVRMS)
            {
                bparameterNumber = 81;
                strParameterName = "R slip Vrms";
            }
            else if (button == MINVOLTAGE)
            {
                bparameterNumber = 76;
                strParameterName = "Min Voltage";
            }
            else if (button == SPDKP)
            {
                bparameterNumber = 74;
                strParameterName = "Spd Kp";
            }
            else if (button == SPDKI)
            {
                bparameterNumber = 133;
                strParameterName = "Spd Ki";
            }
            else if (button == MSLIPFREQHZ)
            {
                bparameterNumber = 78;
                strParameterName = "M Slip Freq Hz";
            }
            else if (button == RSLIPFREQHZ)
            {
                bparameterNumber = 84;
                strParameterName = "R Slip Freq Hz";
            }
            else if (button == MSLIPGAIN)
            {
                bparameterNumber = 82;
                strParameterName = "M slip gain";
            }
            else if (button == RSLIPGAIN)
            {
                bparameterNumber = 83;
                strParameterName = "R slip gain";
            }
            else if (button == FWDLMTRPM)
            {
                bparameterNumber = 70;
                strParameterName = "Fwd Lmt RPM";
            }
            else if (button == RVSLMTRPM)
            {
                bparameterNumber = 71;
                strParameterName = "Rvs Lmt RPM";
            }
            else if (button == FAULTRESET)
            {
                bparameterNumber = 51;
                strParameterName = "Fault Reset";
            }
            else if (button == KPBATTERY)
            {
                bparameterNumber = 100;
                strParameterName = "Kp Battery";
            }
            else if (button == KPACCEL)
            {
                bparameterNumber = 152;
                strParameterName = "Kp Accel";
            }
            else if (button == KPPHASE)
            {
                bparameterNumber = 151;
                strParameterName = "Kp Phase";
            }
            else if (button == IBATLIMIT)
            {
                bparameterNumber = 43;
                strParameterName = "Ibat Limit";
            }
            else if (button == FREQ)
            {
                bparameterNumber = 61;
                strParameterName = "Openloop freq";
                System.Diagnostics.Debug.WriteLine("Setting Freq");

            }
            else if (button == MODINDEX)
            {
                bparameterNumber = 62;
                strParameterName = "Openloop mod";
                System.Diagnostics.Debug.WriteLine("Setting Modindex");
              
            }
            else if (button == REVERSEENCODER)
            {
                bparameterNumber = 153;
                strParameterName = "Reverse Encoder";
            }
            else if (button == DISABLEANALOGBRAKE)
            {
                bparameterNumber = 153;
                strParameterName = "Disable Analog Brake";
            }
            else if (button == DISABLEBRAKESWITCH)
            {
                bparameterNumber = 153;
                strParameterName = "Disable Brake Switch";
            }
            else if (button == DISABLEMOTOROVERTEMPTRIP)
            {
                bparameterNumber = 153;
                strParameterName = "Disable Motor Temp Trip";
            }
            else if (button == NOMINALBATTERYV)
            {
                bparameterNumber = 177;
                strParameterName = "Nominal Battery v";
            }
            else if (button == BOOTSLIPHZ)
            {
                bparameterNumber = 178;
                strParameterName = "Boot slip Hz";
            }
            else if (button == ADDRESS183)
            {
                bparameterNumber = 183;
                strParameterName = "Address 183";
            }
            else if (button == ADDRESS184)
            {
                bparameterNumber = 184;
                strParameterName = "Address 184";
            }
            else if (button == ADDRESS185)
            {
                bparameterNumber = 185;
                strParameterName = "Address 185";
            }
            else if (button == ADDRESS186)
            {
                bparameterNumber = 186;
                strParameterName = "Address 186";
            }
            else if (button == ADDRESS187)
            {
                bparameterNumber = 187;
                strParameterName = "Address 187";
            }
            else if (button == ADDRESS188)
            {
                bparameterNumber = 188;
                strParameterName = "Address 188";
            }
            else if (button == ADDRESS189)
            {
                bparameterNumber = 189;
                strParameterName = "Address 189";
            }
            else if (button == ADDRESS190)
            {
                bparameterNumber = 190;
                strParameterName = "Address 190";
            }




            string myinput = await InputBox(this.Navigation, strParameterName);
            try
            {
                float fParmeterValue = float.Parse(myinput);

                SetParameterEventArgs SetParameterEventArgs = new SetParameterEventArgs(bparameterNumber, fParmeterValue, strParameterName);
    


                AddParamValuesToQueue(this, SetParameterEventArgs);
            }
            catch (ArgumentNullException)
            {

            }
            catch (FormatException f)
            {
                DisplayAlert("Input Error", f.Message, "OK");
            }

        }

        public static Task<string> InputBox(INavigation navigation, string strTitle)
        {
            // wait in this proc, until user did his input 
            var tcs = new TaskCompletionSource<string>();

            var lblTitle = new Label { Text = strTitle, HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
            var lblMessage = new Label { Text = "Enter new value:" };
            var txtInput = new Entry { Text = "" };
            txtInput.Keyboard = Xamarin.Forms.Keyboard.Numeric;

            var btnOk = new Button
            {
                Text = "Ok",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8),
            };
            btnOk.Clicked += async (s, e) =>
            {
                // close page
                var result = txtInput.Text;
                await navigation.PopModalAsync();
                // pass result
                tcs.SetResult(result);
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                WidthRequest = 100,
                BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8)
            };
            btnCancel.Clicked += async (s, e) =>
            {
                // close page
                await navigation.PopModalAsync();
                // pass empty result
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnOk, btnCancel },
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblTitle, lblMessage, txtInput, slButtons },
            };

            // create and show page
            var page = new ContentPage();
            page.BackgroundImage = "drawableportxxhdpiscreenlogin.png";
            page.Content = layout;
            navigation.PushModalAsync(page);
            // open keyboard
            txtInput.Focus();

            // code is waiting her, until result is passed with tcs.SetResult() in btn-Clicked
            // then proc returns the result
            return tcs.Task;
        }

    }
}
