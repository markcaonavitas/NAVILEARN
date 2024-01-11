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

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            try
            {
                InitializeComponent();
                //***Testing pusposes***
                //usernameEntry.Text = "q22";
                //emailEntry.Text = "q22@test.com";
                //emailConfirmationEntry.Text = "q22@test.com";
                //passwordEntry.Text = "q23";
                //passwordConfirmationEntry.Text = "q23";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: SignUpPage.xaml.cs" + ex.Message);
            }
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            DateTime starttime;
            DateTime endtime;
            DateTime endtimePhase;
            TimeSpan phaseInterval;

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Connection Error", "Please Check Your Internet Connection, then press Sign Up Again !", "OK");
                    return;
                }

                App.ParseManagerAdapter.Initialize();

                if (usernameEntry.Text == null || usernameEntry.Text == "")
                {
                    await DisplayAlert("User Name Error", "You can not leave Username blank", "OK");
                    return;
                }
                else if(emailEntry.Text == null || emailEntry.Text == "")
                {
                    await DisplayAlert("Email Error", "You can not leave Email blank", "OK");
                    return;
                }
                else if(passwordEntry.Text == null || passwordEntry.Text == "")
                {
                    await DisplayAlert("Password Error", "You can not leave Password blank", "OK");
                    return;
                }

                usernameEntry.Text = usernameEntry.Text.TrimStart(' ').TrimEnd(' ');

                emailEntry.Text = emailEntry.Text.TrimStart(' ').TrimEnd(' ');
                emailEntry.Text = emailEntry.Text.ToLower();
                emailConfirmationEntry.Text = emailConfirmationEntry.Text.TrimStart(' ').TrimEnd(' ');
                emailConfirmationEntry.Text = emailConfirmationEntry.Text.ToLower();

                passwordEntry.Text = passwordEntry.Text.TrimStart(' ').TrimEnd(' ');
                passwordConfirmationEntry.Text = passwordConfirmationEntry.Text.TrimStart(' ').TrimEnd(' ');

                if (emailEntry.Text != emailConfirmationEntry.Text)
                {
                    await DisplayAlert("Email Address Error", "Confirmation email address does not match", "OK");
                    return;
                }
                else if (!Regex.IsMatch(emailEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                {//http://regexstorm.net/tester works this time
                    await DisplayAlert("Invalid Email format", "Use name@example.com", "OK");
                    return;
                }
                else if (passwordEntry.Text != passwordConfirmationEntry.Text)
                {
                    await DisplayAlert("Password Error", "Confirmation password does not match", "OK");
                    return;
                }
                else
                {
                    await App.ParseManagerAdapter.SignUp(usernameEntry.Text, passwordEntry.Text, emailEntry.Text);
                }
                try
                {
                    var rootPage = Navigation.NavigationStack.FirstOrDefault();
                    var isEmailVerified = false;
                    if (rootPage != null)
                    {
                        starttime = DateTime.Now;
                        //Extend sign up time up to 10 minutes
                        endtime = DateTime.Now.AddSeconds(660);
                        EmailAddress.Text = emailEntry.Text;
                        popupWaitingForEmailVerification.IsVisible = true;
                        SignUpBackButton.IsEnabled = false;
                        WaitingMessage.Text = "Waiting 1 minute for verification";
                        endtimePhase = DateTime.Now.AddSeconds(61);
                        int nullCounter = 0;
                        DateTime secondTracker = DateTime.Now;

                        while (!isEmailVerified && ((int)(endtime - DateTime.Now).TotalSeconds > 0) && !messageLabel.Text.Contains("Sign up was cancelled"))
                        {
                            try
                            {
                                await Task.Delay(1000 - DateTime.Now.Subtract(secondTracker).Milliseconds);
                                secondTracker = DateTime.Now;

                                if (await App.ParseManagerAdapter.IsEmailVerified())
                                {
                                    isEmailVerified = true;
                                }

                                phaseInterval = endtimePhase - DateTime.Now;
                                if (phaseInterval.TotalSeconds > 0 && (DateTime.Now - starttime) <= TimeSpan.FromSeconds(60))
                                {
                                    WaitingMessage.Text = $"Waiting {phaseInterval.Seconds} seconds for verification";
                                }
                                else if (phaseInterval.TotalSeconds > 0 && (DateTime.Now - starttime) <= TimeSpan.FromSeconds(660))
                                {
                                    if (phaseInterval.Minutes != 0)
                                    {
                                        WaitingMessage.Text = $"Waiting {phaseInterval.Minutes} minute {phaseInterval.Seconds} seconds for verification";
                                    }
                                    else
                                    {
                                        WaitingMessage.Text = $"Waiting {phaseInterval.Seconds} seconds for verification";
                                    }
                                }
                                else
                                {
                                    //Time out
                                }

                                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                                    popupWaitingForInternetReConnection.IsVisible = false;

                                if(WaitingMessage.Text != "Retrying signup access")
                                    nullCounter = 0;

                                if ((DateTime.Now - starttime) > TimeSpan.FromSeconds(60) && !CancelButtons.IsVisible)
                                {
                                    CancelButtons.IsVisible = true;
                                    endtimePhase = DateTime.Now.AddSeconds(600);
                                    WaitingMessage.Text = "Waiting 10 more minutes for verification";
                                }
                            }
                            catch (NullReferenceException)
                            {
                                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                                {
                                    popupWaitingForInternetReConnection.IsVisible = true;
                                }
                                else
                                {
                                    nullCounter++;
                                    if (nullCounter < 10)
                                        WaitingMessage.Text = "Retrying signup access";
                                    else
                                        throw new MaximumNullException();
                                    //System.Diagnostics.Debug.WriteLine("After: " + DateTime.Now.Subtract(starttime).TotalSeconds + "There is/are " + (nullCounter) + "Null exception(s)");   
                                }
                            }
                        }
                        popupWaitingForInternetReConnection.IsVisible = false;
                        popupWaitingForEmailVerification.IsVisible = false;

                        if (isEmailVerified)
                        {
                            Authentication.SaveUserCredentials(usernameEntry.Text, emailEntry.Text, passwordEntry.Text);
                            Authentication.SetUserLoginState("True");
                            await DisplayAlert("Email verification", "Successful", "Continue");
                            App.AppConfigurationLevel = await App._MainFlyoutPage.GetAppConfigurationLevel(); //automatically logs in with new credentials
                            MessagingCenter.Send<SignUpPage>(this, "ShowTabPage"); //turn on views according to credentials
                            await Navigation.PopToRootAsync();
                        }
                        else if ((DateTime.Now - starttime) >= TimeSpan.FromSeconds(660))
                        {
                            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
                            {
                                messageLabel.Text = "Email Verification timed out because of intermittent internet connection  " + usernameEntry.Text + " is not registered";
                                await DisplayAlert("Email Verification timed out because of intermittent internet connection ", usernameEntry.Text + " is not registered", "OK");
                            }
                            else
                            {
                                messageLabel.Text = "Verification timed out, " + usernameEntry.Text + " is not registered";
                                await App.ParseManagerAdapter.RemoveUser();
                                await DisplayAlert("Email Verification timed out", usernameEntry.Text + " is not registered", "OK");
                            }
                            //RG Mar 2021: My code is poorly written but if this sign up was called because an unregistered user tried to change
                            //a parameter this value must be set to properly catch this next time.
                            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                        }
                        else
                        {
                            messageLabel.Text = "Email Verification canceled, " + usernameEntry.Text + " is not registered";
                            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                                await App.ParseManagerAdapter.RemoveUser();
                            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                            await DisplayAlert("Email Verification Canceled", usernameEntry.Text + " is not registered", "OK");
                        }
                        resetState();
                    }
                }
                catch(MaximumNullException MaxNullExp)
                {
                    //null counter reach 10 times so this line of code was triggered
                    resetState();
                    await DisplayAlert("Email Verification System Error", "", "Retry");

                    //Remove this particular username to prevent account already exists message pops up for next try
                    try
                    {
                        if (usernameEntry.Text != Authentication.GetUserCredentials()["UserName"])
                            await App.ParseManagerAdapter.RemoveUser();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception from Remove User: " + ex.Message);
                    }
                }
                catch (Exception exception)
                {
                    if (exception.Message.Contains("Object reference not set to an instance of an object") && Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        messageLabel.Text = "Email Verification Connection Failed " + exception.Message;
                        await DisplayAlert("Email Verification Connection Failed", "Email Verification Failed", "OK");
                    }
                    else
                    {
                        messageLabel.Text = "Cannot Sign Up User.  " + exception.Message;
                        await DisplayAlert("Email Verification System Error", messageLabel.Text, "OK");
                    }

                    resetState();
                    Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this

                    try
                    {
                        if (usernameEntry.Text != Authentication.GetUserCredentials()["UserName"])
                            await App.ParseManagerAdapter.RemoveUser();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception from Remove User: " + ex.Message);
                    }
                }
            }
            catch (Exception exception)
            {
                popupWaitingForEmailVerification.IsVisible = false;
                SignUpBackButton.IsEnabled = true;
                CancelButtons.IsVisible = false;
                string errorMessage = "";

                if(exception.Message.Contains("Account already exists for this username"))
                {
                    // Rephrase parse api error to a understandble message
                    errorMessage = "This user name has already been taken. Please retry with a different user name.";
                }
                else
                {
                    // We don't know how many different exceptions could be genereted from the SignUp method
                    errorMessage = exception.Message;
                }

                messageLabel.Text = "Sign up user failed. " + errorMessage;
                Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this

                try
                {
                    // User attempt to sign up an account which already exists,
                    // Or any exceptions caught by SignUp method
                    //This catch causes the logged in account (still valid) get deleted unexpectedly !!!

                    //Don't remove a this real user just because he failed some kind of
                    //attempt to re sign up or sign up as a different user

                    // 02/02/23 Do not understand what the logic behind this code
                    // What make an account to be removed so important while it still valid
                    // If we don't do it, what issues could cause ?
                    // Why I have an valid account, but I still want to sign up a different or same username ??
                    //if (usernameEntry.Text != Authentication.GetUserCredentials()["UserName"])
                    //    await App.ParseManagerAdapter.RemoveUser();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception from Remove User: " + ex.Message);
                }
            }
        }
        //private async void ResendVerificationEmail(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await App.ParseManagerAdapter.RemoveUser();
        //        await App.ParseManagerAdapter.SignUp(usernameEntry.Text, passwordEntry.Text, emailEntry.Text);
        //        starttime = DateTime.Now; //wait some more
        //        WaitingMessage.Text = "Waiting for another verification .";
        //        ResendButtons.IsVisible = false;
        //    }
        //    catch (Exception exception)
        //    {
        //        //            messageLabel.Text = "Sign up failed";
        //        messageLabel.Text = "Resend Email Failed  " + exception.Message;
        //    }
        //}

        private void resetState()
        {
            popupWaitingForEmailVerification.IsVisible = false;
            SignUpBackButton.IsEnabled = true;
            CancelButtons.IsVisible = false;
        }

        private void CancelAndRemoveNewUserSignUp(object sender, EventArgs e)
        {
            messageLabel.Text = "Sign up was cancelled"; //above we just detect if this text was written
        }

        private async void BackToLoginPage(object sender, EventArgs e)
        {
            try
            {
                messageLabel.Text = "Sign up was cancelled";
                CancelButtons.IsVisible = false;
                Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                await Navigation.PopAsync(); //back to login screen
            }
            catch (Exception exception)
            {
                messageLabel.Text = "Sign up was cancelled.  " + exception.Message;
                CancelButtons.IsVisible = false;
                Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                await Navigation.PopAsync(); //back to login screen
            }
        }

        public void ShowHideToggle(object sender, EventArgs e)
        {
            if (passwordEntry.IsPassword == true)
            {
                passwordShowHideIcon.Source = "hide.png";
                passwordEntry.IsPassword = false;
                passwordConfirmationEntry.IsPassword = false;
            }
            else
            {
                passwordShowHideIcon.Source = "show.png";
                passwordEntry.IsPassword = true;
                passwordConfirmationEntry.IsPassword = true;
            }
        }
    }

    class MaximumNullException : Exception
    {
        public MaximumNullException() { }
    }
}
