using System;
using Xamarin.Forms;
using NavitasBeta;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Text.RegularExpressions;

namespace NavitasBeta
{
    public partial class LoginPage : ContentPage
    {
        //ActivityIndicator activityIndicator;

        public LoginPage()
        {
            InitializeComponent();
            //activityIndicator = new ActivityIndicator();
            IDictionary<string, string> strUserCredentials = Authentication.GetUserCredentials();
            if (strUserCredentials["UserName"] != "testc5a3nFD43M")
            {
                usernameEntry.Text = strUserCredentials["UserName"];
                passwordEntry.Text = strUserCredentials["Password"];
            }
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
        async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            //popupForgotUsername.IsVisible = false;
            popupForgotPassword.IsVisible = true;
        }
        
        //void OnCloseClicked(object sender, EventArgs e)
        //{
        //    popupForgotUsername.IsVisible = false;
        //}
        
        //async void OnForgotUsernameClicked(object sender, EventArgs e)
        //{
        //    popupForgotUsername.IsVisible = true;
        //}

        async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            try
            {
                if(emailResetPassordEntry.Text != null && emailResetPassordEntry.Text != "")
                {
                    string emailAddress = emailResetPassordEntry.Text;
                    if (Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    {
                        await App.ParseManagerAdapter.RequestPasswordReset(emailAddress);
                        await DisplayAlert("Success", "Your password reset link has been sent to your email", "OK");
                        popupForgotPassword.IsVisible = false;
                    }
                    else
                        await DisplayAlert("Invalid Email format", "Use name@example.com", "OK");
                }
                else
                    await DisplayAlert("Warning", "Email is empty", "OK");
            }
            catch(NullReferenceException ex)
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    await DisplayAlert("Connection Error", "Please Check Your Internet Connection and try again !", "OK");
                else
                    System.Diagnostics.Debug.WriteLine("NullReferenceException" + ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No user found"))
                    await DisplayAlert("Error", ex.Message, "Try Again");
                else
                    System.Diagnostics.Debug.WriteLine("OnResetPasswordClicked Exception" + ex.Message);
            }
        }

        public void OnCancelResetPasswordClicked(object sender, EventArgs e)
        {
            popupForgotPassword.IsVisible = false;
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            //TODO: RG Oct 2020, it might be clearer to consolidata automatic login with login
            try
            {
                activityIndicator.IsRunning = true;
                App.ParseManagerAdapter.Initialize();
                string loginResult = "";

                IDictionary<string, string> strUserCredentials = Authentication.GetUserCredentials();
                
                loginResult = await App.ParseManagerAdapter.Login(usernameEntry.Text, passwordEntry.Text);
 
                //check successful user login including uninitialized AppConfigurationLevel (ie. failed with "AppConfigurationLevel" in message:)
                //or first time login will have default password NO_ACCESS
                if (!loginResult.Contains("Fail") || loginResult.Contains("AppConfigurationLevel") || loginResult.Contains("NO_ACCESS"))
                {
                    if ((loginResult == "" || loginResult.Contains("AppConfigurationLevel") || loginResult.Contains("NO_ACCESS")) && usernameEntry.Text != "testc5a3nFD43M")
                    {
                        //if we got here then if we are not the default user (ie., "testc5a3nFD43M"), we have logged in before 
                        //but have default or uninitialized access level, so initialize it
                        //if someone externally, with parse database access,modified it by hand then we won't get here
#if DEALER
                        App.AppConfigurationLevel = "DEALER";
#elif ENG
                        App.AppConfigurationLevel = "ENG";
#else
                        App.AppConfigurationLevel = "USER";
#endif
                        await App.ParseManagerAdapter.TransmitUserLevel(App.AppConfigurationLevel);
                    }
                    else
                    {
                        //otherwise use the Access level someone assigned from the database
                        App.AppConfigurationLevel = loginResult;
                        Authentication.SaveMemberOfList(App.ParseManagerAdapter.GetMemberOfList());
                    }

                    //Get correct username from Parse, unless it a null value
                    usernameEntry.Text = App.ParseManagerAdapter.GetCorrectUsername() ?? usernameEntry.Text;

                    Authentication.SaveUserCredentialsAndAccessLevel(usernameEntry.Text, passwordEntry.Text, App.AppConfigurationLevel);
                    Authentication.SetUserLoginState("True");
                    activityIndicator.IsRunning = false;
                    MessagingCenter.Send<LoginPage>(this, "ShowTabPage"); //turn on views according to credentials
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = loginResult;
                    await DisplayAlert("Login Error", messageLabel.Text, "OK");

                }
                activityIndicator.IsRunning = false;
            }
            catch (Exception exception)
            {
                //TODO: ParseManagerAdapter don't throw exceptions, they just respond with a string that contains the word Fail
                activityIndicator.IsRunning = false;
                if ((exception.Message.Contains("Object reference not set to an instance of an object")) && Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    messageLabel.Text = "Can't login.  Check Internet connection";
                }
                else
                {
                    messageLabel.Text = exception.Message;
                }
                await DisplayAlert("Login Error", messageLabel.Text, "OK");
                passwordEntry.Text = string.Empty;
                Authentication.SetUserLoginState("False");
            }

        }

        public void ShowHideToggle(object sender, EventArgs e)
        {
            if (passwordEntry.IsPassword == true) {
                passwordShowHideIcon.Source = "hide.png";
                passwordEntry.IsPassword = false;
            }
            else
            {
                passwordShowHideIcon.Source = "show.png";
                passwordEntry.IsPassword = true;
            }
        }
        private async void BackbuttonClicked(object sender, EventArgs e)
        {
            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
            await Navigation.PopAsync();
        }
    }
}