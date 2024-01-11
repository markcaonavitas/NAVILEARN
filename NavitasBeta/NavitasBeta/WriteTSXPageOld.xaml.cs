

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WriteTSXPageOld : ContentPage
    {
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        public WriteTSXPageOld()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            BindingContext = ViewModelLocator.MainViewModelTSX;
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
        /*
                 public static void SaveUserCredentials(string strUserName, string strPassword)
                {
                    IDictionary<string, object> properties = Application.Current.Properties;
                    properties["UserName"] = strUserName;
                    properties["Password"] = strPassword;
                    Application.Current.SavePropertiesAsync();
                }

                public static void SetUserLoginState(string strState)
                {
                    IDictionary<string, object> properties = Application.Current.Properties;
                    properties["LoggedIn"] = strState;
                   Application.Current.SavePropertiesAsync();
                }

                public static bool IsUserLoggedIn()
                {
                    IDictionary<string, object> properties = Application.Current.Properties;
                    if (properties.ContainsKey("LoggedIn"))
                    {
                        if ((properties["LoggedIn"] as string) == "True")
                        {
                            return true;
                        }
                    }
                    return false;
                }



         */





        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string strParameterName = "Default";
            byte bparameterNumber = 0;
            IDictionary<string, object> properties = Application.Current.Properties;

            string myinput = await InputBox(this.Navigation, strParameterName);
            try
            {
                float fParmeterValue = float.Parse(myinput);

                if (button == SPEEDOMETERMAXSPEED)
                {
                  
                }
                else if (button == TIRERADIUS)
                {
                    bparameterNumber = 150;
                   
                }
                else if (button == REARAXLERATIO)
                {
                    bparameterNumber = 151;
                   
                }
                else if (button == MILESORKILOMETERS)
                {
                   
                }

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

        private void tireradius_Completed(object sender, EventArgs e)
        {
            int i = 0;
            i++;
        }
    }
}
