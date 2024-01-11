using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavitasBeta;

using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvancedRequestAccessPage : ContentPage
    {
        private countries_states vm;
        public AdvancedRequestAccessPage()
        {
            try
            {
                InitializeComponent();
                GetCountry();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: AdvancedRequestAccessPage.xaml.cs" + ex.Message);
            }
        }

        private void GetCountry()
        {
            countries_states CountryList = FileManager.GetDeserializedObject<countries_states>("CountriesStates.xml");
            vm = new countries_states(CountryList.country_states);
            this.BindingContext = vm;
        }

        DateTime starttime;
        async void OnSendRequestButtonClicked(object sender, EventArgs e)
        {
            try
            {
                bool isValidated = false;
                bool isUpdate = false;
                double currentPageHeight = Xamarin.Forms.Application.Current.MainPage.Height;
                double currentPageWidth = Xamarin.Forms.Application.Current.MainPage.Width;
                string provinceOrState = vm.StateLabel;

                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Connection Error", "Please Check Your Internet Connection, then press Sign Up Again !", "OK");
                    return;
                }

                App.ParseManagerAdapter.Initialize();

                if (vm.phdPostalCode == "Not Available")
                {
                    postalCodeEntry.Text = "";
                }

                //Prevent Object Null
                if (firstNameEntry.Text == null || firstNameEntry.Text == "")
                {
                    await DisplayAlert("First Name Error", "You can not leave First Name blank", "OK");
                    return;
                }
                else if (lastNameEntry.Text == null || lastNameEntry.Text == "")
                {
                    await DisplayAlert("Last Name Error", "You can not leave Last Name blank", "OK");
                    return;
                }
                else if (addressEntry.Text == null || addressEntry.Text == "")
                {
                    await DisplayAlert("Address Error", "You can not leave Address blank", "OK");
                    return;
                }
                else if (cityEntry.Text == null || cityEntry.Text == "")
                {
                    await DisplayAlert("City Error", "You can not leave City blank", "OK");
                    return;
                }
                else if (postalCodeEntry.Text == null)
                {
                    await DisplayAlert("Postal Code Error", "You can not leave Postal Code blank", "OK");
                    return;
                }
                else if (phoneNumberEntry.Text == null || phoneNumberEntry.Text == "")
                {
                    await DisplayAlert("Phone Number Error", "You can not leave Phone Number blank", "OK");
                    return;
                }

                //Trim Space Start and End for Each entry
                firstNameEntry.Text = firstNameEntry.Text.TrimStart(' ').TrimEnd(' ');
                lastNameEntry.Text = lastNameEntry.Text.TrimStart(' ').TrimEnd(' ');
                addressEntry.Text = addressEntry.Text.TrimStart(' ').TrimEnd(' ');
                cityEntry.Text = cityEntry.Text.TrimStart(' ').TrimEnd(' ');
                postalCodeEntry.Text = postalCodeEntry.Text.TrimStart(' ').TrimEnd(' ');
                phoneNumberEntry.Text = phoneNumberEntry.Text.TrimStart(' ').TrimEnd(' ');

                country_state country = (country_state)Country.SelectedItem;
                state state = (state)State.SelectedItem;

                if (vm.IsDropDownProvinceAvailable)
                {
                    if (Country.SelectedIndex == -1 && State.SelectedIndex == -1)
                    {
                        await DisplayAlert("Empty field", $"You can not leave both Country and {provinceOrState} blank", "OK");
                        return;
                    }
                    else if (Country.SelectedIndex == -1)
                    {
                        await DisplayAlert("Empty field", "You can not leave Country blank", "OK");
                        return;
                    }
                    else if (State.SelectedIndex == -1)
                    {
                        await DisplayAlert("Empty field", $"You can not leave {provinceOrState} blank", "OK");
                        return;
                    }
                }
                else
                {
                    if (Country.SelectedIndex == -1 && EntryState.Text == "")
                    {
                        await DisplayAlert("Empty field", $"You can not leave both Country and {provinceOrState} blank", "OK");
                        return;
                    }
                    else if (Country.SelectedIndex == -1)
                    {
                        await DisplayAlert("Empty field", "You can not leave Country blank", "OK");
                        return;
                    }
                    else if (EntryState.Text == "")
                    {
                        await DisplayAlert("Empty field", $"You can not leave {provinceOrState} blank", "OK");
                        return;
                    }
                }

                if (country.name == "Canada")
                {
                    //Canadian Postal Code
                    if (!Regex.IsMatch(postalCodeEntry.Text, @"^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z][ -]?\d[ABCEGHJ-NPRSTV-Z]\d$", RegexOptions.IgnoreCase))
                    {
                        await DisplayAlert("Invalid Postal Code format", "Use A1B 2C3 or A1B2C3", "OK");
                        return;
                    }
                }
                else if (country.name == "United States")
                {
                    //US Zip Code
                    if (!Regex.IsMatch(postalCodeEntry.Text, @"^[0-9]{5}(?:-[0-9]{4})?$"))
                    {
                        await DisplayAlert("Invalid Zip Code format", "Use 12345 or 12345-6789", "OK");
                        return;
                    }
                }

                Regex phoneRegex;
                if (phoneNumberEntry.Text == "")
                {
                    await DisplayAlert("Empty field", $"You can not leave phone number blank", "OK");
                    return;
                }
                else if (country.name == "Canada" || country.name == "United States")
                {
                    //Check Phone Number format (Apply for US and Canada)
                    phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
                    if (!phoneRegex.IsMatch(phoneNumberEntry.Text))
                    {
                        await DisplayAlert("Invalid Phone Number format", "Use 123-123-1234", "OK");
                        return;
                    }
                    else
                    {
                        phoneNumberEntry.Text = phoneRegex.Replace(phoneNumberEntry.Text, "($1) $2-$3");
                        isValidated = true;
                    }
                }
                else
                {
                    phoneRegex = new Regex(@"^[\d \-\+()]+$");
                    if (!phoneRegex.IsMatch(phoneNumberEntry.Text))
                    {
                        await DisplayAlert("Invalid Phone Number format", "This entry can only contain numbers", "OK");
                        return;
                    }
                    else
                    {
                        isValidated = true;
                    }
                }

                if (isValidated)
                {
                    Dictionary<string, string> PersonalInfo = new Dictionary<string, string>();

                    PersonalInfo.Add("firstName", firstNameEntry.Text);
                    PersonalInfo.Add("lastName", lastNameEntry.Text);
                    PersonalInfo.Add("address", addressEntry.Text);
                    PersonalInfo.Add("city", cityEntry.Text);
                    if(vm.phdPostalCode != "Not Available")
                        PersonalInfo.Add("PostalCode", postalCodeEntry.Text);
                    if (vm.IsDropDownProvinceAvailable)
                        PersonalInfo.Add("province", state.name);
                    else
                        PersonalInfo.Add("province", EntryState.Text);
                    PersonalInfo.Add("country", country.name);
                    PersonalInfo.Add("phone", phoneNumberEntry.Text);

                    isUpdate = await App.ParseManagerAdapter.UpdateAppConfigurationLevel(PersonalInfo);
                    popupRequestInProgress.IsVisible = true;
                }
                starttime = DateTime.Now;
                //do some animation here
                WaitingMessage.Text = "Your request is now processing .";

                bool isReloginSucessfully = false;

                if (isUpdate)
                {
                    App.AppConfigurationLevel = await App._MainFlyoutPage.GetAppConfigurationLevel(); //automatically logs in with new credentials
                    while ((DateTime.Now - starttime) < TimeSpan.FromSeconds(4))
                    {
                        WaitingMessage.Text += ".";
                        await Task.Delay(1000);
                    }
                    popupRequestInProgress.IsVisible = false;
                    await DisplayAlert("Request Sucessfully", "You are now Advanced User", "Continue");
                    MessagingCenter.Send<AdvancedRequestAccessPage>(this, "ShowTabPage"); //turn on views according to credentials
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Request Fail", "Your Request to server was failed \n\n Please try it again!", "Close");
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnSendRequestButtonClicked Exception: " + ex.Message);
            }
        }

        private void UnfocusEvent(object sender, FocusEventArgs e)
        {
            string textSent = (sender as Entry).Text;
            if (textSent != null)
            {
                (sender as Entry).Text = UppercaseWords(textSent);
            }
            else
                return;
        }

        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
        private async void BackToLoginPage(object sender, EventArgs e)
        {
            try
            {
                messageLabel.Text = "Advanced user request  was cancelled";
                /*Authentication.AlreadyCheckingCredentials = false;*/ //in case it was dismissed then set this
                await Navigation.PopAsync(); //back to login screen
            }
            catch (Exception exception)
            {
                messageLabel.Text = "Advanced user request was cancelled.  " + exception.Message;
                /*Authentication.AlreadyCheckingCredentials = false;*/ //in case it was dismissed then set this
                await Navigation.PopAsync(); //back to login screen
            }
        }
    }
}