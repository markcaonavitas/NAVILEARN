using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace NavitasBeta
{
    public static class Authentication
    {
        public static bool AlreadyCheckingCredentials = false;

        private static IDictionary<string, object> properties = Application.Current.Properties;
        public static void SaveUserCredentials(string strUserName, string strEmail, string strPassword)
        {
            properties["UserName"] = strUserName;
            properties["Password"] = strPassword;
            properties["Email"] = strEmail;
            Application.Current.SavePropertiesAsync();
        }

        public static void SaveUserAccessLevel(string accessLevel)
        {
            properties["AccessLevel"] = accessLevel;
            Application.Current.SavePropertiesAsync();
        }

        public static void SaveUserListOfRegisterControllers(string listOfRegisterControllers)
        {
            properties["ListOfRegisterControllers"] = listOfRegisterControllers;
            Application.Current.SavePropertiesAsync();
        }

        public static void SaveUserCredentialsAndAccessLevel(string strUserName, string strPassword, string strAccessLevel)
        {
            properties["UserName"] = strUserName;
            properties["Password"] = strPassword;
            properties["AccessLevel"] = strAccessLevel;
            Application.Current.SavePropertiesAsync();
        }
        public static void SaveRegisteredVehicles(string presentConnectedController)
        {
            properties["ListOfRegisterControllers"] += presentConnectedController;
            Application.Current.SavePropertiesAsync();
        }
        public static void RemoveFromRegisteredVehicles(string presentConnectedController)
        {
            string value = properties["ListOfRegisterControllers"] as string;
            properties["ListOfRegisterControllers"] = value.Replace("presentConnectedController", "");
            Application.Current.SavePropertiesAsync();
       }
        public static void SetUserLoginState(string strState)
        {
            properties["LoggedIn"] = strState;
            Application.Current.SavePropertiesAsync();
        }

        public static bool IsUserLoggedIn()
        {
            if (properties.ContainsKey("LoggedIn"))
            {
                if ((properties["LoggedIn"] as string) == "True")
                {
                    return true;
                }
            }
            return false;
        }

        public static IDictionary<string, string> GetUserCredentials()
        {
            IDictionary<string, string> UserCredentials = new Dictionary<string, string>();

            if (!properties.ContainsKey("UserName"))
                properties.Add("UserName", "testc5a3nFD43M");
            UserCredentials["UserName"] = properties["UserName"] as string;
 
            if (!properties.ContainsKey("Password"))
                properties.Add("Password", "test123");
            UserCredentials["Password"] = properties["Password"] as string;

            if (!properties.ContainsKey("AccessLevel"))
                properties.Add("AccessLevel", "USER");
            UserCredentials["AccessLevel"] = properties["AccessLevel"] as string;
#if ADD_THIS_LATER
            UserCredentials[2] = properties["Email"] as string;
#endif

            if (!properties.ContainsKey("ListOfRegisterControllers"))
                properties.Add("ListOfRegisterControllers", "");
            UserCredentials["ListOfRegisterControllers"] = properties["ListOfRegisterControllers"] as string;
            
            if (properties.ContainsKey("NickName"))
                UserCredentials["NickName"] = properties["NickName"] as string;

            if (!properties.ContainsKey("DoNotShowMessage"))
                properties.Add("DoNotShowMessage", "False");
            UserCredentials["DoNotShowMessage"] = properties["DoNotShowMessage"] as string;

            return UserCredentials;
        }

        public static void SetUserWasShownVehicleOperatorVerification()
        {
            properties["UserWasShownVehicleOperatorVerification"] = "True";
            Application.Current.SavePropertiesAsync();
        }

        public static bool CheckUserWasShownVehicleOperatorVerification()
        {
            if (properties.ContainsKey("UserWasShownVehicleOperatorVerification"))
            {
                if ((properties["UserWasShownVehicleOperatorVerification"] as string) == "True")
                {
                    return true;
                }
            }
            return false;
        }

        public static void SaveMemberOfList(string strMemberOfList)
        {
            if (strMemberOfList != "")
            {
                if (!properties.ContainsKey("MemberOfList"))
                {
                    properties.Add("MemberOfList", strMemberOfList);
                }
                else
                {
                    if (properties["MemberOfList"] as string != strMemberOfList)
                    {
                        properties["MemberOfList"] = strMemberOfList;
                    }
                }
                Application.Current.SavePropertiesAsync();
            }
            else
            {
                if (properties.ContainsKey("MemberOfList"))
                {
                    Application.Current.Properties.Remove("MemberOfList");
                    Application.Current.SavePropertiesAsync();
                }
            }
        }

        public static string GetMemberOfList()
        {
            return properties.ContainsKey("MemberOfList") ? properties["MemberOfList"] as string : "";
        }

        public static void SaveNickName(string nickName)
        {

            if (nickName != "")
            {
                if (!properties.ContainsKey("NickName"))
                {
                    properties.Add("NickName", nickName);
                }
                else
                {
                    if (properties["NickName"] as string != nickName)
                    {
                        properties["NickName"] = nickName;
                    }
                }
                Application.Current.SavePropertiesAsync(); 
            }
            else
            {
                if (properties.ContainsKey("NickName"))
                {
                    Application.Current.Properties.Remove("NickName");
                    Application.Current.SavePropertiesAsync();
                }
            }
        }

        public static bool GetDoNotShowMessage()
        {
            return (string)properties["DoNotShowMessage"] == "True" ? true : false;
        }

        public static void SaveDoNotShowMessage(bool isChecked)
        {
            properties["DoNotShowMessage"] = isChecked ? "True" : "False";
            Application.Current.SavePropertiesAsync();
        }
    }
}
