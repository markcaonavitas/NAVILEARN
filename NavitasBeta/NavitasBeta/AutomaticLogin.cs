using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Essentials;

namespace NavitasBeta
{
    public class AutomaticLogin : ContentPage
    {

        public AutomaticLogin()
        {
        }

        public async Task Login()
        {
            try
            {

                App.ParseManagerAdapter.Initialize();
                IDictionary<string, string> strUserCredentials = Authentication.GetUserCredentials();
                System.Diagnostics.Debug.WriteLine("Login with " + strUserCredentials["UserName"] + " " + strUserCredentials["Password"] + " at " + DateTime.Now);

                string loginResult = "";

                loginResult = await App.ParseManagerAdapter.Login(strUserCredentials["UserName"], strUserCredentials["Password"]);

                //check successful user login including uninitialized AppConfigurationLevel (ie. failed with "AppConfigurationLevel" in message:)
                //or first time login will have default password NO_ACCESS
                if (!loginResult.Contains("Fail") || loginResult.Contains("AppConfigurationLevel") || loginResult.Contains("NO_ACCESS"))
                {
                    if ((loginResult == "" || loginResult.Contains("AppConfigurationLevel") || loginResult.Contains("NO_ACCESS")) && strUserCredentials["UserName"] != "testc5a3nFD43M")
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
                        //and app.config in the public Navitas directory of the phone
                        //containing a non blank AppConfigurationLevel
                        //will override any network or hard coded App Level
                        
                        AppConfiguration AppConfiguration = FileManager.GetDeserializedObject<AppConfiguration>($"app.config");
                        if(AppConfiguration.AppConfigurationLevel != null && AppConfiguration.AppConfigurationLevel != "")
                            App.AppConfigurationLevel = AppConfiguration.AppConfigurationLevel;

                        await App.ParseManagerAdapter.TransmitUserLevel(App.AppConfigurationLevel);
                    }
                    else
                    {
                        //otherwise use the Access level someone assigned from the database
#if ENG
                            App.AppConfigurationLevel = "ENG";
#elif DEALER
                        if (loginResult == "ENG") //database eng has higher priority than in DEALER APP
                            App.AppConfigurationLevel = "ENG";
                        else
                            App.AppConfigurationLevel = "DEALER";
#else
                        App.AppConfigurationLevel = loginResult;
                        System.Diagnostics.Debug.WriteLine("Login result 1 is " + loginResult + " at " + DateTime.Now);
                        Authentication.SaveMemberOfList(App.ParseManagerAdapter.GetMemberOfList());
#endif
                    }

                    Authentication.SaveUserAccessLevel(App.AppConfigurationLevel);
                }
                System.Diagnostics.Debug.WriteLine("Login result 2 is " + loginResult + " at " + DateTime.Now);

                Authentication.SetUserLoginState("True");
            }
            catch (Exception exception)
            {
                //TODO: ParseManagerAdapter don't throw exceptions, they just respond with a string that contains the word Fail

                if ((exception.Message.Contains("Object reference not set to an instance of an object")) && Connectivity.NetworkAccess != NetworkAccess.Internet)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.DisplayAlert("Can't login", "Check Internet connection", "OK");
                    });


                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.DisplayAlert("Can't login", exception.Message, "OK");
                    });


                }


            }
        }

    }
}
