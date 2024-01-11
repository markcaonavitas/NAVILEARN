using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Parse;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace ParseManager.UWP
{
    public class ParseClientManager : IParseClientManager
    {
        private Dictionary<string, object> VehicleDataLog;
        private Dictionary<string, object> VehicleHealth;
        private Dictionary<string, object> VehicleSettings;
        private string datalogId;
        private string correctUsername;
        private string debugId = "junk";
        private string debugServer = "junk1";
        public void Initialize()
        {
            debugId = "5df4cd98e64a66c35c8282f69bf7472a";
            debugServer = "https://goi-608.nodechef.com/parse/";
            ParseClient.Initialize(new ParseClient.Configuration
            {

                ApplicationId = "5df4cd98e64a66c35c8282f69bf7472a",
                //ApplicationId = "2ab1ea065942cf32f1674be9a94d7680",
                //     WindowsKey = "11d15195c836c700aa598647d3ec243c",

                Server = "https://goi-608.nodechef.com/parse/"
                //Server = "https://goi-21063.nodechef.com/parse/"
            });
        }

        public void Initialize(string applicationId, string server)
        {
            debugId = applicationId;
            debugServer = server;
            ParseClient.Initialize(new ParseClient.Configuration
            {

                ApplicationId = applicationId,
                //ApplicationId = "2ab1ea065942cf32f1674be9a94d7680",
                //     WindowsKey = "11d15195c836c700aa598647d3ec243c",

                Server = server
                //Server = "https://goi-21063.nodechef.com/parse/"
            });
        }

        public async Task InitializeTest()
        {
            try
            {
                Initialize("24a28dde87fe5cf98d512c9410c3df19", "https://testdata-21063.nodechef.com/parse/");
                string loginResult = await Login("testdata", "testdata");
                System.Diagnostics.Debug.WriteLine("InitializeTest login result is " + loginResult + " at " + DateTime.Now);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeTest exeption: {exception.Message}");
            }
        }

        public string GetApplicationId()
        {
            return debugId;
        }

        public string GetServer()
        {
            return debugServer;
        }

        public void InitParametersList(string PresentConnectedController)
        {
            VehicleDataLog = new Dictionary<string, object>();
            VehicleDataLog.Add("Serial Number", PresentConnectedController);
            datalogId = "";
        }

        public void AddLoggedParameterData(string ParameterName, string[] Times, double[] Data)
        {
            int iIndex = 0;
            List<object> data = new List<object>();
            foreach (double d in Data)
            {
                if (Times[iIndex] != null)
                {
                    Dictionary<object, object> timeanddata = new Dictionary<object, object>();
                    timeanddata.Add("time", Double.Parse(Times[iIndex].Replace("{", "").Replace("}", "")));
                    timeanddata.Add("value", d);
                    data.Add(timeanddata);

                }
                else
                {
                    break;
                }
                iIndex++;
            }
            VehicleDataLog.Add(ParameterName, data);
        }

        public async Task Transmit(string DeviceName)
        {
            try
            {
                var Troubleshooting = new ParseObject("Troubleshooting");
                //var Troubleshooting = new ParseObject("TroubleshootingTest");
                Troubleshooting["deviceName"] = DeviceName;

                //Write as database record field
                //Old method Troubleshooting["data"] = Parameters;

                //Write as file which is referenced in database field
                //remove above writing as record when happy with this method

                //Keeps track vehicle health and vehicle settings object
                //within vehicleDataLog
                VehicleDataLog.Add("Vehicle Settings", VehicleSettings);
                VehicleDataLog.Add("Vehicle Health", VehicleHealth);

                byte[] bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(VehicleDataLog));
                var parseFile = new ParseFile("NavitasPhoneAppLog.txt", bytes);
                await parseFile.SaveAsync();
                Troubleshooting["FilePointer"] = parseFile;

                Troubleshooting["user"] = ParseUser.CurrentUser;
                Troubleshooting.ACL = new ParseACL(ParseUser.CurrentUser);
                Troubleshooting.ACL.PublicWriteAccess = true;
                Troubleshooting.ACL.PublicReadAccess = true;
                await Troubleshooting.SaveAsync();
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine("Dons Debug Transmit  Exception caught");
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAppConfigurationLevel(Dictionary<string, string> personalInfo)
        {
            var isUpdate = false;
            try
            {
                var userObject = ParseUser.CurrentUser;
                foreach (var info in personalInfo)
                {
                    switch (info.Key)
                    {
                        //Write personal info
                        case "firstName":
                            userObject[info.Key] = info.Value;
                            break;
                        case "lastName":
                            userObject[info.Key] = info.Value;
                            break;
                        case "address":
                            userObject[info.Key] = info.Value;
                            break;
                        case "PostalCode":
                            userObject[info.Key] = info.Value;
                            break;
                        case "city":
                            userObject[info.Key] = info.Value;
                            break;
                        case "province":
                            userObject[info.Key] = info.Value;
                            break;
                        case "country":
                            userObject[info.Key] = info.Value;
                            break;
                        case "phone":
                            userObject[info.Key] = info.Value;
                            break;
                        default:
                            break;
                    }
                }
                // update their ACL
                userObject["AppConfigurationLevel"] = "ADVANCED_USER";
                //Save it
                await userObject.SaveAsync();
                isUpdate = true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("UpdateAppConfigurationLevel exception: " + e.Message);
            }
            return isUpdate;
        }

        public async Task<bool> CheckUsernameExist(string username)
        {
            try
            {
                IEnumerable<ParseUser> result = await ParseUser.Query.WhereEqualTo("username", username).FindAsync();
                return result.Count() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddUsernameToRegisteredMotorControllerUsers(string bleControllerSerialNumber, string userName)
        {
            try
            {
                ParseObject vehicleObject = await GetVehicleParseObject(bleControllerSerialNumber);

                IList<string> listOfRegisteredMotorControllerUser = vehicleObject.Get<IList<string>>("registeredMotorControllerUsers").ToList();

                //Check Duplicate username
                foreach (string registeredUserName in listOfRegisteredMotorControllerUser)
                {
                    if (registeredUserName == userName)
                    {
                        return false;
                    }
                }

                //Granted this username by adding his account to the list
                vehicleObject.AddToList("registeredMotorControllerUsers", userName);
                await vehicleObject.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUserInfoFromVehicleRecord(string bleControllerSerialNumber, string driverBeingRemoved)
        {
            try
            {
                ParseObject vehicleObject = await GetVehicleParseObject(bleControllerSerialNumber);
                vehicleObject.RemoveAllFromList("registeredMotorControllerUsers", new List<string>() { driverBeingRemoved });
                if (vehicleObject.ContainsKey("NickName"))
                    vehicleObject.Remove("NickName");
                await vehicleObject.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DeleteUserInfoFromVehicleRecord exception: " + ex.Message);
                return false;
            }
        }

        public async Task<IList<string>> GetRegisteredMotorControllerUsersList(string bleControllerSerialNumber)
        {
            try
            {
                ParseObject vehicleObject = await GetVehicleParseObject(bleControllerSerialNumber);
                IList<string> listOfRegisteredMotorControllerUser = vehicleObject.Get<IList<string>>("registeredMotorControllerUsers").ToList();

                return listOfRegisteredMotorControllerUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsControllerRegistered(string bleControllerSerialNumber)
        {
            //Check if the controller was registered to anyone
            //if not, let the user registers the controller
            System.Diagnostics.Debug.WriteLine("Check Is Controller Registered !!!");
            ParseObject vehicleObject = await GetVehicleParseObject(bleControllerSerialNumber);
            if (vehicleObject == null || !vehicleObject.ContainsKey("registeredMotorControllerUsers"))
            {
                //Prevent undefined issue makes app crash
                return false;
            }

            if (vehicleObject.Get<IList<string>>("registeredMotorControllerUsers").ToList().Count() > 0)
            {
                return true;
            }
            else //The list was wiped out by RMA. May be
                return false;
        }

        public async Task<bool> IsRegisteredUser(string bleControllerSerialNumber)
        {
            //The controller has already registered
            //This step will check if the current user is a registered user
            //if not he becomes HasRegisteredButNotYou
            ParseObject vehicleObject = await GetVehicleParseObject(bleControllerSerialNumber);

            //var debug = parseClass.Get<IList<string>>("registeredMotorControllerUsers").ToList().Contains(ParseUser.CurrentUser.Username);
            int loginAttemptCounter = 0;
            bool isRegistered = false;

            if (vehicleObject != null)
            {
                while (ParseUser.CurrentUser == null && loginAttemptCounter < 6)
                {
                    loginAttemptCounter++;
                    await Task.Delay(500);
                    System.Diagnostics.Debug.WriteLine($"Login Attempt Counter = {loginAttemptCounter}");
                }
                if (ParseUser.CurrentUser != null)
                    isRegistered = vehicleObject.Get<IList<string>>("registeredMotorControllerUsers").ToList().Contains(ParseUser.CurrentUser.Username);
            }

            return isRegistered;
        }

        private async Task<ParseObject> GetVehicleParseObject(string bleControllerSerialNumber)
        {
            string dbVehicleClassName = "Vehicles";
            if (bleControllerSerialNumber.Contains("TSX"))
                dbVehicleClassName = "TSX" + dbVehicleClassName;

            ParseQuery<ParseObject> query = ParseObject.GetQuery(dbVehicleClassName);
            IEnumerable<ParseObject> result = await query.WhereEqualTo("MotorController", bleControllerSerialNumber).FindAsync();

            if (result != null || result.Count() != 0)
                return result.FirstOrDefault(); //there should be only one if everything is working
            else
                return null;
        }

        public async Task<Dictionary<string, string>> UpdateVehicleParseClass(string className, Dictionary<string, object> record, Dictionary<string, object> vehicleSettings)
        {
            Dictionary<string, string> returnObject = new Dictionary<string, string>();
            //string vehicleMemberOfList = "";
            string userMemberOfList = "";
            var queryTimeNow = DateTime.Now.Millisecond; //keep an eye on App delays for Cloud access

            try
            {
                var newParseClass = new ParseObject(className);
                ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                IEnumerable<ParseObject> result = await query.WhereEqualTo("MotorController", record["MotorController"]).FindAsync();

                ParseObject parseClass = new ParseObject(className);
                //sometimes I do both Lists or strings just to experiment with notations and ease of use
                IList<string> listOfUsers = new List<string>();

                if (result == null || result.Count() == 0)
                {//vehicle serial number is new so initialize it
                    parseClass = newParseClass;
                    listOfUsers.Add(ParseUser.CurrentUser.Username); //if it is "testc5a3nFD43M" it causes an exception and exits
                }
                else
                {//vehicle serial number is already there to get info
                    parseClass = result.First();
                    listOfUsers = parseClass.Get<IList<string>>("listOfUsers").ToList(); //one method
                }
                //sometimes I do both parseClass.Get<some type>("something")  or parseClass["something"]
                //just to experiment with speed, I'm not don't think either goes back to the cloud but is local
                //TODO: clarify differences and access
                //ease of use are things like: 
                // Can an apps eng enter JSON type list<string> in our Cloud tools for now 
                // or is it easier for them to add to a comma delimited string
                // or should we take the time ($ and release delays) to update the cloud tool UI for simpler interface to this class or record or....
                if (!listOfUsers.Contains(ParseUser.CurrentUser.Username))
                    listOfUsers.Add(ParseUser.CurrentUser.Username);

                //if (parseClass.ContainsKey("MemberOfList"))
                //    vehicleMemberOfList = parseClass["MemberOfList"] as string;

                if (ParseUser.CurrentUser.ContainsKey("MemberOfList"))
                    userMemberOfList = ParseUser.CurrentUser["MemberOfList"] as string;

                // We stop writing user member of list to vehicle member of list
                //if (userMemberOfList.Length != 0)
                //{
                //    foreach (var x in userMemberOfList.Split(','))
                //    {
                //        if (x.Contains("MANUFACTURER:") && !vehicleMemberOfList.Contains("MANUFACTURER:") && !vehicleMemberOfList.Contains(x))
                //        {
                //            if (vehicleMemberOfList.Length == 0)
                //                vehicleMemberOfList = x;
                //            else
                //                vehicleMemberOfList += "," + x;
                //        }
                //        if (x.Contains("OEM:") && !vehicleMemberOfList.Contains("OEM:") && !vehicleMemberOfList.Contains(x))
                //        {
                //            if (vehicleMemberOfList.Length == 0)
                //                vehicleMemberOfList = x;
                //            else
                //                vehicleMemberOfList += "," + x;
                //        }
                //        if (x.Contains("DISTRIBUTOR:") && !vehicleMemberOfList.Contains("DISTRIBUTOR:") && !vehicleMemberOfList.Contains(x))
                //        {
                //            if (vehicleMemberOfList.Length == 0)
                //                vehicleMemberOfList = x;
                //            else
                //                vehicleMemberOfList += "," + x;
                //        }
                //        if (x.Contains("DEALER:") && !vehicleMemberOfList.Contains("DEALER:") && !vehicleMemberOfList.Contains(x))
                //        {
                //            if (vehicleMemberOfList.Length == 0)
                //                vehicleMemberOfList = x;
                //            else
                //                vehicleMemberOfList += "," + x;
                //        }
                //    }
                //}

                //Write MotorController field
                parseClass["MotorController"] = record["MotorController"];
                parseClass["connectedUser"] = ParseUser.CurrentUser;
                //Write whole database record field
                parseClass["vehicleHealth"] = record;
                VehicleHealth = record;
                //Write whole vehicle settings and serial number record field
                if (vehicleSettings.Count > 0)
                {
                    parseClass["vehicleSettings"] = vehicleSettings;
                    VehicleSettings = vehicleSettings;
                }
                //Write whole List<string>
                parseClass["listOfUsers"] = listOfUsers;
                //Write whole comma delimited string
                //parseClass["MemberOfList"] = vehicleMemberOfList;

                parseClass.ACL = new ParseACL(ParseUser.CurrentUser);
                parseClass.ACL.PublicWriteAccess = true;
                parseClass.ACL.PublicReadAccess = true;
                await parseClass.SaveAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("UpdateVehicleParseClass exception: " + e.Message);
            }
            //returnObject.Add("VehicleMemberOfList", vehicleMemberOfList);
            returnObject.Add("UserMemberOfList", userMemberOfList);
            System.Diagnostics.Debug.WriteLine("Response From UpdateVehicleParseClass " + (queryTimeNow - DateTime.Now.Millisecond).ToString());
            return returnObject;
        }

        public async Task TransmitSupplierRoleClass(string className, Dictionary<string, object> record)
        {
            try
            {
                var newParseClass = new ParseObject(className);
                ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                IEnumerable<ParseObject> result = await query.WhereEqualTo("Name", "CVG").FindAsync(); // record["Name"]).FindAsync();

                ParseObject parseClass = new ParseObject(className);
                IList<string> listOfUsers = new List<string>();

                if (result == null || result.Count() == 0)
                {
                    parseClass = newParseClass;
                    listOfUsers.Add(ParseUser.CurrentUser.Username);
                }
                else
                {
                    parseClass = result.First();
                    listOfUsers = parseClass.Get<IList<string>>("listOfUsers").ToList();
                    if (!listOfUsers.Contains(ParseUser.CurrentUser.Username))
                        listOfUsers.Add(ParseUser.CurrentUser.Username);
                }
                //Write as database record field
                parseClass["vehicleHealth"] = record;
                parseClass["listOfUsers"] = listOfUsers;
                parseClass["connectedUser"] = ParseUser.CurrentUser;
                parseClass.ACL = new ParseACL(ParseUser.CurrentUser);
                parseClass.ACL.PublicWriteAccess = true;
                parseClass.ACL.PublicReadAccess = true;
                await parseClass.SaveAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("TransmitSupplierRoleClass exception: " + e.Message);
            }
        }

        public async Task TransmitRegisteredUsers(string className, string presentConnectedController)
        {
            try
            {
                var newParseClass = new ParseObject(className);
                ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                IEnumerable<ParseObject> result = await query.WhereEqualTo("MotorController", presentConnectedController).FindAsync();

                ParseObject parseClass = new ParseObject(className);
                IList<string> listOfUsers = new List<string>();

                if (result == null || result.Count() == 0 || ParseUser.CurrentUser.Username == "testc5a3nFD43M")
                {
                    return;
                }

                parseClass = result.First(); //there should be only one if everything is working
                if (!parseClass.ContainsKey("registeredMotorControllerUsers"))
                    parseClass["registeredMotorControllerUsers"] = new List<string> { ParseUser.CurrentUser.Username };
                else
                {
                    listOfUsers = parseClass.Get<IList<string>>("registeredMotorControllerUsers").ToList();
                    if (listOfUsers.Contains(ParseUser.CurrentUser.Username))
                        return;
                    listOfUsers.Add(ParseUser.CurrentUser.Username);
                    parseClass["registeredMotorControllerUsers"] = listOfUsers;
                }
                parseClass.ACL = new ParseACL(ParseUser.CurrentUser);
                parseClass.ACL.PublicWriteAccess = true;
                parseClass.ACL.PublicReadAccess = true;
                await parseClass.SaveAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("TransmitRegisteredUsers exception: " + e.Message);
            }
        }

        //a Custom Access is ment for user and vehicle specific limits possibly set by a configurable parent list (not totally flushed out)
        //It will limit a list of vehicle and UI access such as a dealer not wanting his customer to set wheel size or speed limits or anything
        //the supplier chain key controls the hieracrchy of this structure

        //the order of the returned value (who was added first) is important
        //calling routines will overwrite each item in the access object
        //the first example is a FileList object will be overwritten if a later object has it
        public async Task<Dictionary<string, object>> GetCustomAccessObjects(string presentConnectedController)
        {
            //TODO: The concept of list of users or maybe list of registered users can be search for supplier names
            //maybe using the list of users as an automatic notification as to who has touched this vehicle?
            //so that we don't need to attach a custom access object (which we cannot actually do in a sustainable way)

            //lost debugging break point ability so I modified this line and it started again
            Dictionary<string, object> customAccessObjects = new Dictionary<string, object>();
            var queryTimeNow = DateTime.Now.Millisecond; //keep an eye on App delays for Cloud access
            try
            {
                if (ParseUser.CurrentUser.ContainsKey("CustomAccessObject"))
                    customAccessObjects.Add("UserCustomAccessObject", ParseUser.CurrentUser.Get<Dictionary<string, object>>("CustomAccessObject"));

                string prefix = "";
                if (presentConnectedController.Contains("TSX"))
                    prefix = "TSX";

                IEnumerable<ParseObject> motorController = await ParseObject.GetQuery(prefix + "Vehicles").WhereEqualTo("MotorController", presentConnectedController).FindAsync();
                //TODO: right now CustomAccessObject overrides MemberOfList, think about it, works for now
                if (motorController.First().ContainsKey("CustomAccessObject"))
                {
                    customAccessObjects.Add("motorControllerAccessObject", motorController.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                    if ((customAccessObjects["motorControllerAccessObject"] as Dictionary<string, object>).ContainsKey("SupplierChain"))
                    {
                        var motorControllerSupplierChain = (customAccessObjects["motorControllerAccessObject"] as Dictionary<string, object>)["SupplierChain"] as Dictionary<string, object>;
                        //not include User in this list
                        for (int i = 0; i < ParseParentSchema.Count - 1; i++)
                        {
                            string className = ParseParentSchema[i];
                            string uppercaseCategory = className.Substring(0, className.Length - 1).ToUpper();

                            if (motorControllerSupplierChain.ContainsKey(uppercaseCategory))
                            {
                                string name = motorControllerSupplierChain[uppercaseCategory] as string;
                                IEnumerable<ParseObject> results = await ParseObject.GetQuery($"{className}").WhereEqualTo("Name", name).FindAsync();

                                if (results.First().ContainsKey("CustomAccessObject"))
                                {
                                    customAccessObjects.Add($"{className.Substring(0, className.Length - 1)}AccessObject",
                                    results.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                                }
                            }
                        }
                    }
                }
                else if (motorController.First().ContainsKey("MemberOfList"))
                {   //notice again I am using an alternative for ...First().Get<.... just as a lesson
                    string memberOfList = motorController.First()["MemberOfList"] as string;
                    //not include User in this list
                    for (int i = 0; i < ParseParentSchema.Count - 1; i++)
                    {
                        //category list include : MANUFACTURER, OEM, DISTRIBUTOR, DEALER 
                        //Find everthing up to an appropriate category:
                        string className = ParseParentSchema[i];
                        string uppercaseCategory = className.Substring(0, className.Length - 1).ToUpper();

                        if (memberOfList.Contains(uppercaseCategory))
                        {
                            string pattern = @"[\s\S].*(.|\n)*(?=" + uppercaseCategory + ":)";
                            string name = Regex.Replace(memberOfList, pattern, "");

                            //Then Remove category:
                            //then grab the first item up to the delimiter or end of string whichever comes first
                            //Didn't have time to figure out the one regex line that would do it all because I am not that interested in
                            //learning for regex...but you should be
                            name = name.Replace($"{uppercaseCategory}:", "").Split(",")[0];

                            IEnumerable<ParseObject> results = await ParseObject.GetQuery($"{className}").WhereEqualTo("Name", name).FindAsync();
                            if (results.First().ContainsKey("CustomAccessObject"))
                            {
                                customAccessObjects.Add($"{className.Substring(0, className.Length - 1)}AccessObject",
                                    results.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCustomAccessObject exception: " + e.Message);
            }
            System.Diagnostics.Debug.WriteLine("Response From Custom Objects " + (queryTimeNow - DateTime.Now.Millisecond).ToString());
            return customAccessObjects;
        }

        public async Task<Dictionary<string, object>> GetCustomAccessObjects(string presentConnectedController, CancellationToken cancellationToken)
        {
            //TODO: The concept of list of users or maybe list of registered users can be search for supplier names
            //maybe using the list of users as an automatic notification as to who has touched this vehicle?
            //so that we don't need to attach a custom access object (which we cannot actually do in a sustainable way)

            //lost debugging break point ability so I modified this line and it started again
            Dictionary<string, object> customAccessObjects = new Dictionary<string, object>();
            var queryTimeNow = DateTime.Now.Millisecond; //keep an eye on App delays for Cloud access
            try
            {
                if (ParseUser.CurrentUser.ContainsKey("CustomAccessObject"))
                    customAccessObjects.Add("UserCustomAccessObject", ParseUser.CurrentUser.Get<Dictionary<string, object>>("CustomAccessObject"));

                string prefix = "";
                if (presentConnectedController.Contains("TSX"))
                    prefix = "TSX";

                IEnumerable<ParseObject> motorController = await ParseObject.GetQuery(prefix + "Vehicles").WhereEqualTo("MotorController", presentConnectedController).FindAsync(cancellationToken);
                //TODO: right now CustomAccessObject overrides MemberOfList, think about it, works for now
                if (motorController.First().ContainsKey("CustomAccessObject"))
                {
                    customAccessObjects.Add("motorControllerAccessObject", motorController.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                    if ((customAccessObjects["motorControllerAccessObject"] as Dictionary<string, object>).ContainsKey("SupplierChain"))
                    {
                        var motorControllerSupplierChain = (customAccessObjects["motorControllerAccessObject"] as Dictionary<string, object>)["SupplierChain"] as Dictionary<string, object>;
                        //not include User in this list
                        for (int i = 0; i < ParseParentSchema.Count - 1; i++)
                        {
                            string className = ParseParentSchema[i];
                            string uppercaseCategory = className.Substring(0, className.Length - 1).ToUpper();

                            if (motorControllerSupplierChain.ContainsKey(uppercaseCategory))
                            {
                                string name = motorControllerSupplierChain[uppercaseCategory] as string;
                                IEnumerable<ParseObject> results = await ParseObject.GetQuery($"{className}").WhereEqualTo("Name", name).FindAsync();

                                if (results.First().ContainsKey("CustomAccessObject"))
                                {
                                    customAccessObjects.Add($"{className.Substring(0, className.Length - 1)}AccessObject",
                                    results.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                                }
                            }
                        }
                    }
                }
                else if (motorController.First().ContainsKey("MemberOfList"))
                {   //notice again I am using an alternative for ...First().Get<.... just as a lesson
                    string memberOfList = motorController.First()["MemberOfList"] as string;
                    //not include User in this list
                    for (int i = 0; i < ParseParentSchema.Count - 1; i++)
                    {
                        //category list include : MANUFACTURER, OEM, DISTRIBUTOR, DEALER 
                        //Find everthing up to an appropriate category:
                        string className = ParseParentSchema[i];
                        string uppercaseCategory = className.Substring(0, className.Length - 1).ToUpper();

                        if (memberOfList.Contains(uppercaseCategory))
                        {
                            string pattern = @"[\s\S].*(.|\n)*(?=" + uppercaseCategory + ":)";
                            string name = Regex.Replace(memberOfList, pattern, "");

                            //Then Remove category:
                            //then grab the first item up to the delimiter or end of string whichever comes first
                            //Didn't have time to figure out the one regex line that would do it all because I am not that interested in
                            //learning for regex...but you should be
                            name = name.Replace($"{uppercaseCategory}:", "").Split(",")[0];

                            IEnumerable<ParseObject> results = await ParseObject.GetQuery($"{className}").WhereEqualTo("Name", name).FindAsync();
                            if (results.First().ContainsKey("CustomAccessObject"))
                            {
                                customAccessObjects.Add($"{className.Substring(0, className.Length - 1)}AccessObject",
                                    results.First().Get<Dictionary<string, object>>("CustomAccessObject"));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetCustomAccessObject exception: " + e.Message);
            }
            System.Diagnostics.Debug.WriteLine("Response From Custom Objects " + (queryTimeNow - DateTime.Now.Millisecond).ToString());
            return customAccessObjects;
        }

        static List<string> ParseParentSchema = new List<string> { "Manufacturers", "OEMs", "Distributers", "Dealers", "User" };

        //a Parent and ParentRole for now is ment for mobile Devices UI access
        //It will limit a list of vehicle and UI access such as a manufacturing programming device only showing a custom programming screen
        //I think it extends to a future actual parent who owns a vehicle and limits his kids access through new interfaces in the App like "add a user to my vehicle"
        public async Task<Dictionary<string, object>> GetParentRoleAccessObjects(string presentConnectedController)
        {//lesson one youtube.com/watch?v=j0zDb9hVooQ
            Dictionary<string, object> parentRoleAccessObjects = new Dictionary<string, object>();
            var queryTimeNow = DateTime.Now.Millisecond; //keep an eye on App delays for Cloud access
            try
            {
                if (ParseUser.CurrentUser.ContainsKey("ParentName"))
                {
                    //added these to the database before running App
                    string parentName = ParseUser.CurrentUser.Get<string>("ParentName");
                    string parentRole = ParseUser.CurrentUser.Get<string>("ParentRole");

                    foreach (var schemaClass in ParseParentSchema)
                    {
                        var parent = await ParseObject.GetQuery(schemaClass).WhereEqualTo("Name", parentName).FindAsync();
                        if (parent.Count() != 0)
                        {
                            if (parent.First().ContainsKey(parentRole + "RoleAccessObject"))
                                parentRoleAccessObjects.Add(parentRole + "RoleAccessObject", parent.First().Get<Dictionary<string, object>>(parentRole + "RoleAccessObject"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetParentRoleAccessObject: " + e.Message); //developer did something wrong here or in the database
            }
            System.Diagnostics.Debug.WriteLine("Response From ParseParentSchema " + (queryTimeNow - DateTime.Now.Millisecond).ToString());
            return parentRoleAccessObjects;
        }
        public async Task TransmitUserLevel(string appConfigurationLevel)
        {
            try
            {
                ParseUser.CurrentUser["AppConfigurationLevel"] = appConfigurationLevel;
                await ParseUser.CurrentUser.SaveAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("TransmitUserLevel exception: " + e.Message);
            }
        }

        public async Task LogOut()
        {
            try
            {
                await ParseUser.LogOutAsync();
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.

                System.Diagnostics.Debug.WriteLine(e.Message);

                throw;


            }
        }

        public async Task<string> Login(string Username, string Password)
        {
            try
            {
                ParseSession currentSession = null;
                bool hasLoginBeenSuccessful = false;
                bool hasLoginBeenRequired = false;

                try
                {
                    if (ParseUser.CurrentUser.Username != Username)
                    {
                        await LogOut();
                    }

                    currentSession = await ParseSession.GetCurrentSessionAsync();
                    if (ParseUser.CurrentUser != null && currentSession != null)
                    {
                        //Guarantee no new session was created by logging in this way.
                        //Moreover ,current user also gets update
                        await ParseUser.BecomeAsync(currentSession.SessionToken);
                        hasLoginBeenSuccessful = true;
                    }
                    else
                    {
                        hasLoginBeenRequired = true;
                    }
                }
                catch (ParseException pe)
                {
                    if (pe.Message.Contains("invalid session token"))
                    {
                        hasLoginBeenRequired = true;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message + "Parse Login: no session or maybe no Internet to login");
                }

                if (hasLoginBeenRequired)
                {
                    try
                    {
                        await ParseUser.LogInAsync(Username, Password);
                        hasLoginBeenSuccessful = true;
                        System.Diagnostics.Debug.WriteLine("logging in 1");
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message + "Parse Login: no session or maybe no Internet to login");
                        System.Diagnostics.Debug.WriteLine("or Username case sensitive or not found");
                    }
                }

                if (!hasLoginBeenSuccessful)
                {
                    await LoginAnAccountWithCaseSensitive(Username, Password);
                }

                //ParseUser.CurrentUser auto update after login successfully
                return ParseUser.CurrentUser.Get<string>("AppConfigurationLevel");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Object"))
                    return "Login Failed: Check Internet Connection";
                return "Login Failed: " + e.Message;
            }
        }

        private async Task LoginAnAccountWithCaseSensitive(string Username, string Password)
        {
            //Looking for username case insensitive
            IEnumerable<ParseUser> results = await GetUserObjectsByUsername(Username);

            if (results.Count() == 0)
            {
                //This hack will help users log in by email when they forgot their usernames
                if (Regex.IsMatch(Username.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                {
                    ParseUser result = await GetUserObjectsByEmail(Username.Trim());

                    if (result.Count() == 0)
                        throw new Exception("This account does not exist");
                    else
                    {
                        correctUsername = result.Username;
                        //Remember that Parse accepts login with username only
                        await ParseUser.LogInAsync(correctUsername, Password);
                        System.Diagnostics.Debug.WriteLine("logging in 2");
                    }
                }
                else
                    throw new Exception("This account does not exist");
            }
            else
            {
                if (results.Count() == 1)
                {
                    correctUsername = results.FirstOrDefault().Username;
                    //Remember that Parse accepts login with username only
                    await ParseUser.LogInAsync(correctUsername, Password);
                    System.Diagnostics.Debug.WriteLine("logging in 2");
                }
                else
                {
                    //results.Count() >= 2
                    //Username must be duplicate stop them from logging in 
                    throw new Exception("Invalid username/password.");
                }
            }
        }

        public bool IsUserLoggedInAlready()
        {
            return (ParseUser.CurrentUser != null);
        }

        public async Task<bool> IsEmailVerified()
        {
            try
            {
                await ParseUser.CurrentUser.FetchAsync();
                return await Task.FromResult(ParseUser.CurrentUser.Get<bool>("emailVerified"));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("emailVerified"))
                    return await Task.FromResult(false); //probably has not been sent yet
                else throw;
            }
        }

        public async Task RemoveUser()
        {
            try
            {
                await ParseUser.CurrentUser.DeleteAsync();
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        public void ResetEmail(string emailAddress)
        {
            try
            {
                ParseUser.CurrentUser.Remove("email");
                ParseUser.CurrentUser["email"] = emailAddress;
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task SignUp(string username, string password, string email)
        {

            var user = new ParseUser()
            {
                Username = username,
                Password = password,
                Email = email
            };

            // other fields can be set just like with ParseObject
            //    user["phone"] = phonenumber;

            try
            {
                await user.SignUpAsync();
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<List<Dictionary<string, object>>> GetListOfRegisterControllers()
        {
            try
            {
                List<ParseObject> PrimaryListOfRegisterControllers = await FetchRegisteredVehicles();
                List<ParseObject> SecondaryListOfRegisterControllers = await FetchRegisteredVehicles("TSX");

                if (PrimaryListOfRegisterControllers != null && SecondaryListOfRegisterControllers != null)
                {
                    PrimaryListOfRegisterControllers.AddRange(SecondaryListOfRegisterControllers);
                    return CreateRegisterControllers(PrimaryListOfRegisterControllers);
                }
                else if (PrimaryListOfRegisterControllers != null)
                {
                    return CreateRegisterControllers(PrimaryListOfRegisterControllers);
                }
                else if (SecondaryListOfRegisterControllers != null)
                {
                    return CreateRegisterControllers(SecondaryListOfRegisterControllers);
                }
                else
                {
                    return new List<Dictionary<string, object>>();
                }
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }

        private List<Dictionary<string, object>> CreateRegisterControllers(List<ParseObject> vehicles)
        {
            List<Dictionary<string, object>> registeredVehicles = new List<Dictionary<string, object>>();

            foreach (var vehicle in vehicles)
            {

                if (!(vehicle["MotorController"] as string).Contains("Demo"))
                {
                    Dictionary<string, object> vehicleObject = new Dictionary<string, object>();
                    var nickname = vehicle.ContainsKey("NickName") ? (string)vehicle["NickName"] : string.Empty;
                    //Create an object  that match vehicle object
                    vehicleObject.Add("Name", (string)vehicle["MotorController"]);
                    vehicleObject.Add("NickName", nickname);
                    registeredVehicles.Add(vehicleObject);
                }
            }

            return registeredVehicles;
        }

        private async Task<List<ParseObject>> FetchRegisteredVehicles(string controllerType = "TAC")
        {
            var prefix = controllerType == "TAC" ? "" : "TSX";
            ParseQuery<ParseObject> VehiclesQuery = ParseObject.GetQuery(prefix + "Vehicles");
            IEnumerable<ParseObject> results = await VehiclesQuery.WhereContainedIn("registeredMotorControllerUsers", new List<string>() { ParseUser.CurrentUser.Username }).FindAsync();
            if (results != null || results.Count() != 0)
                return results.ToList();
            else
                return null;
        }

        public async Task RequestPasswordReset(string emailAddress)
        {
            try
            {
                ParseUser result = await GetUserObjectsByEmail(emailAddress);

                //return lowercase and remove all leading and trailing white-space character from email
                emailAddress = result.Email;

                //if email does not exist
                //build-in parse exception will throw and prompt message
                await ParseUser.RequestPasswordResetAsync(emailAddress);
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine("RequestPasswordReset" + e.Message);
            }
        }

        public async Task<bool> IsEmailVerified(string email)
        {
            ParseQuery<ParseUser> userQuery = ParseUser.Query;
            IEnumerable<ParseObject> results = await userQuery.WhereEqualTo("email", email).FindAsync();

            var userObject = results.FirstOrDefault();

            if (userObject != null && userObject.ContainsKey("emailVerified"))
            {
                bool result = (bool)userObject["emailVerified"];
                return result;
            }
            else
                throw new NullReferenceException($"No such an account with {email}");
        }

        public async Task<string[]> GetSupplierChainAndCompanyName(string supplierChainId, string companyId)
        {
            string[] modelMapArray = new string[2];

            ParseQuery<ParseObject> ModelMapQuery = ParseObject.GetQuery("ModelMap");

            //Querying SupplierChain and CompanyName
            IEnumerable<ParseObject> results = await ModelMapQuery.WhereEqualTo("SupplierChainId", supplierChainId).FindAsync();

            ParseObject modelMapObject = results.FirstOrDefault();

            if (modelMapObject != null)
            {
                modelMapArray[0] = modelMapObject.Get<string>("SupplierChain");
                modelMapArray[1] = (string)modelMapObject.Get<Dictionary<string, object>>("CompanyId").FirstOrDefault(c => c.Key == companyId).Value;
            }

            return modelMapArray;
        }

        public async Task<string> GetModelName(string className, string companyName, string modelId)
        {
            string modelName = "";
            //Query to this specific class
            ParseQuery<ParseObject> ModelMapQuery = ParseObject.GetQuery(className);

            //Querying this supplier chain name
            IEnumerable<ParseObject> results = await ModelMapQuery.WhereEqualTo("Name", companyName).FindAsync();

            ParseObject supplierChainObj = results.FirstOrDefault();

            if (supplierChainObj != null)
            {
                Dictionary<string, object> modelNumberObj = (Dictionary<string, object>)supplierChainObj.Get<Dictionary<string, object>>("ModelNumber").FirstOrDefault(modelNumber => modelNumber.Key == modelId).Value;
                modelName = (string)modelNumberObj.FirstOrDefault(modelItem => modelItem.Key == "ModelName").Value;
            }

            return modelName;
        }

        public async Task<bool> IsUserSessionValid()
        {
            try
            {
                ParseSession currentSession = await ParseSession.GetCurrentSessionAsync();
            }
            catch (ParseException pe)
            {
                if (pe.Message.Contains("invalid session token"))
                    return false;
            }
            return true;
        }
        public Dictionary<string, object> GetDatalogObject()
        {
            return VehicleDataLog;
        }

        public string GetDatalogUniqueId()
        {
            return datalogId;
        }

        public async Task<bool> UploadVEEPROM(string filePath, string className)
        {
            try
            {
                var Troubleshooting = new ParseObject(className);
                //Troubleshooting["deviceName"] = "Test";

                string connectedController = VehicleDataLog.ElementAt(0).Value as string;
                StreamReader sr = new StreamReader(filePath);

                byte[] bytes = Encoding.ASCII.GetBytes(sr.ReadToEnd());
                sr.Dispose();
                var parseFile = new ParseFile($"{Regex.Match(filePath, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value}", bytes);
                await parseFile.SaveAsync();
                Troubleshooting["FilePointer"] = parseFile;

                Troubleshooting["user"] = ParseUser.CurrentUser;
                Troubleshooting.ACL = new ParseACL(ParseUser.CurrentUser);
                Troubleshooting.ACL.PublicWriteAccess = true;
                Troubleshooting.ACL.PublicReadAccess = true;
                await Troubleshooting.SaveAsync();
                return true;
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine("UploadEEPROM Exception caught");
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UploadScopeLog(Dictionary<string, object> vehicleSettings)
        {
            try
            {
                var Troubleshooting = new ParseObject("Troubleshooting");
                Troubleshooting["deviceName"] = "Test";

                //This vehicle settings is different from vehicle's record one
                VehicleDataLog.Add("Vehicle Settings", vehicleSettings);

                byte[] bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(VehicleDataLog));
                var parseFile = new ParseFile($"Scope.txt", bytes);
                await parseFile.SaveAsync();
                Troubleshooting["FilePointer"] = parseFile;

                Troubleshooting["user"] = ParseUser.CurrentUser;
                Troubleshooting.ACL = new ParseACL(ParseUser.CurrentUser);
                Troubleshooting.ACL.PublicWriteAccess = true;
                Troubleshooting.ACL.PublicReadAccess = true;
                await Troubleshooting.SaveAsync();
                return true;
            }
            catch (Exception e)
            {
                // to do
                // Make this message pop up.
                System.Diagnostics.Debug.WriteLine("UploadScopeLog Exception caught");
                System.Diagnostics.Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task SaveMaxSpeedRecord(string bleControllerSerialNumber, double speed)
        {
            await Saveliabilities(bleControllerSerialNumber, speed.ToString(), null);
        }

        public async Task SaveMaxBatteryCurrentRecord(string bleControllerSerialNumber, double batteryCurrent)
        {
            await Saveliabilities(bleControllerSerialNumber, null, batteryCurrent.ToString());
        }

        public async Task Saveliabilities(string bleControllerSerialNumber, string speed, string batteryCurrent)
        {
            try
            {
                ParseObject vehicleObj = await GetVehicleParseObject(bleControllerSerialNumber);
                Dictionary<string, object> liabilityObj;
                if (speed != null)
                {
                    liabilityObj = new Dictionary<string, object>()
                    {
                        {"Username", ParseUser.CurrentUser.Username},
                        {"Date_Accepted", DateTime.Now },
                        {"Forward_Speed_Limit_MPH", double.Parse(speed) }
                    };
                }
                else
                {
                    liabilityObj = new Dictionary<string, object>()
                    {
                        {"Username", ParseUser.CurrentUser.Username},
                        {"Date_Accepted", DateTime.Now },
                        {"Maximum_Battery_Current", double.Parse(batteryCurrent) }
                    };
                }

                //Check if the field existed
                if (!vehicleObj.ContainsKey("listOfLiabilities"))
                {
                    IList<Dictionary<string, object>> listOfLiabilities = new List<Dictionary<string, object>>() { liabilityObj };
                    vehicleObj["listOfLiabilities"] = listOfLiabilities;
                }
                else
                    vehicleObj.AddToList("listOfLiabilities", liabilityObj);

                await vehicleObj.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<ParseUser> GetUserObjectsByEmail(string emailAddress)
        {
            //Assert position at start of a line,
            //Apply this pattern because there are many emails end with space
            string pattern = "^" + emailAddress;
            Regex regEx = new Regex(pattern, RegexOptions.ECMAScript);
            ParseQuery<ParseUser> userQuery = ParseUser.Query;
            //search email case insensitive
            IEnumerable<ParseUser> results = await userQuery.WhereMatches("email", regEx, "i").FindAsync();

            if (results.Count() > 1)
            {
                foreach (ParseUser result in results)
                {
                    //Check first char is UpperCase or not
                    if (!char.IsUpper(result.Email, 0))
                    {
                        return result;
                    }
                }
            }

            return results.FirstOrDefault();
        }

        private async Task<IEnumerable<ParseUser>> GetUserObjectsByUsername(string username)
        {
            ParseQuery<ParseUser> userQuery = ParseUser.Query;
            //search username case insensitive
            //username should match the whole word
            IEnumerable<ParseUser> results = await userQuery.WhereMatches("username", new Regex(@"^" + username.Trim() + @"$", RegexOptions.ECMAScript), "i").FindAsync();

            return results;
        }

        public string GetCorrectUsername()
        {
            return correctUsername;
        }

        public async Task<Dictionary<string, object>> GetCustomAccessObjectsFromSupplyChain(string className, Dictionary<string, object> supplierChain)
        {
            try
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                IEnumerable<ParseObject> results = await query.WhereEqualTo("ProfileNumber", supplierChain["ProfileNumber"]).FindAsync();

                //profileNumber must be unique
                var supplyChainObject = results.FirstOrDefault();
                if (supplyChainObject.ContainsKey("CustomAccessObject") &&
                    supplyChainObject.Get<Dictionary<string, object>>("CustomAccessObject").Values.Count != 0)
                {
                    return supplyChainObject.Get<Dictionary<string, object>>("CustomAccessObject");
                }
                else
                {
                    return null; //There is nothing to retrieve
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get object from supplierchain exception" + ex.Message);
                return null;
            }
        }

        public string GetMemberOfList()
        {
            return ParseUser.CurrentUser.ContainsKey("MemberOfList") ?
                    ParseUser.CurrentUser["MemberOfList"] as string :
                    "";
        }

        public async Task<Dictionary<string, object>> GetLatestModifiedSupplierChain(string controllerType)
        {
            try
            {
                var prefix = controllerType == "TAC" ? "" : "TSX";
                ParseQuery<ParseObject> query = ParseObject.GetQuery($"{prefix}SupplyChain").OrderByDescending("updatedAt").Limit(1);
                IEnumerable<ParseObject> results = await query.FindAsync();

                return new Dictionary<string, object>() { { "UpdatedAt", results.FirstOrDefault().UpdatedAt } };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>() { { "Error", $"GetLatestModifiedSupplierChain Exception: {ex.Message}" } };
            }
        }

        public async Task<List<Dictionary<string, object>>> GetSupplierChain(string controllerType)
        {
            List<Dictionary<string, object>> dictionaries = new List<Dictionary<string, object>>();
            string prefix = controllerType == "TAC" ? "" : "TSX";

            ParseQuery<ParseObject> query = ParseObject.GetQuery($"{prefix}SupplyChain").OrderBy("createdAt");
            IEnumerable<ParseObject> results = await query.FindAsync();
            //var correctFirmware = results.FirstOrDefault(f => f.Get<string>("ProfileNumber") == "1");
            
            foreach (var item in results)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (var key in item.Keys)
                {
                    if(key == "CurrentRating")
                        dictionary.Add(key, controllerType + (string)item[key]);
                    else
                        dictionary.Add(key, item[key]);
                }

                dictionaries.Add(dictionary);
            }
            
            return dictionaries;
        }
        public async Task<string> GetNickName(string bleControllerSerialNumber)
        {
            string className = "Vehicles";
            if (bleControllerSerialNumber.Contains("TSX"))
                className = "TSX" + className;
            ParseQuery<ParseObject> query = ParseObject.GetQuery(className);

            IEnumerable<ParseObject> results = await query.WhereEqualTo("MotorController", bleControllerSerialNumber).FindAsync();

            if (results.FirstOrDefault().ContainsKey("NickName"))
                return results.FirstOrDefault().Get<string>("NickName");
            else
                return null;
        }

        public async Task<bool> SaveNickName(string bleControllerSerialNumber, string nickName)
        {
            string className = "Vehicles";
            if (bleControllerSerialNumber.Contains("TSX"))
                className = "TSX" + className;
            ParseQuery<ParseObject> query = ParseObject.GetQuery(className);

            IEnumerable<ParseObject> results = await query.WhereEqualTo("MotorController", bleControllerSerialNumber).FindAsync();

            var vehicle = results.FirstOrDefault();

            if (vehicle != null)
            {
                if (vehicle.ContainsKey("NickName"))
                {
                    if (nickName != "")
                        vehicle["NickName"] = nickName;
                    else
                        vehicle.Remove("NickName");
                }
                else
                    vehicle.Add("NickName", nickName);

                await vehicle.SaveAsync();
                return true;
            }

            return false;
        }

        public async Task UploadFileToObjectStorage(string filePath, string className)
        {
            await UploadFileToObjectStorage(filePath, className);
        }
        public async Task UploadFileToObjectStorage(string filePath, string className, string key, string value)
        {
            try
            {
                if (className != "TroubleshootingVEEPROM")
                {
                    var parseObject = new ParseObject(className);

                    if (key != "" && value != "")  //there should be only one
                    {
                        ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                        IEnumerable<ParseObject> results = await query.WhereEqualTo(key, value).FindAsync();

                        if (results.Count() != 0)
                            parseObject = results.FirstOrDefault(); //there should be only one if everything is working 
                    }

                    ParseFile parseFile;
                    var fileName = $"{Regex.Match(filePath, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value}";
                    if (filePath != "FileIsGreaterThan15Mb")
                    {
                        StreamReader sr = new StreamReader(filePath);
                        byte[] bytes = Encoding.ASCII.GetBytes(sr.ReadToEnd());
                        sr.Dispose();
                        parseFile = new ParseFile(fileName, bytes);
                        await parseFile.SaveAsync();
                        datalogId = Regex.Match(parseFile.Url.ToString(), @"(?<=\/)[a-zA-Z\d]*(?=_.)", RegexOptions.IgnoreCase).Value;
                    }
                    else
                    {//just log "FileIsGreaterThan15Mb" without SaveAsync
                        parseFile = new ParseFile(fileName, new byte[0]);
                    }
                    parseObject["FilePointer"] = parseFile;

                    parseObject["user"] = ParseUser.CurrentUser;
                    parseObject.ACL = new ParseACL(ParseUser.CurrentUser);
                    parseObject.ACL.PublicWriteAccess = true;
                    parseObject.ACL.PublicReadAccess = true;
                    await parseObject.SaveAsync();
                }
                else
                {
                    await UploadVEEPROM(filePath, className);
                }
            }
            catch (ParseException pe)
            {
                System.Diagnostics.Debug.WriteLine($"UploadFileToObjectStorage exception: {pe.Message}");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"UploadFileToObjectStorage exception 2: {e.Message}");
            }
        }
        public async Task<bool> UpdateClass(string className, Dictionary<string, object> records)
        {
            try
            {
                var parseObject = new ParseObject(className);

                var recordRecieved = records.Where(obj => obj.Key == "TestID"); // May be contain "ID" should work too
                if(recordRecieved.ToList().Count == 1) //there should be only one
                {
                    var obj = recordRecieved.ToList()[0];
                    ParseQuery<ParseObject> query = ParseObject.GetQuery(className);
                    IEnumerable<ParseObject> results = await query.WhereEqualTo(obj.Key, obj.Value).FindAsync(); ;

                    if (results.Count() != 0)
                        parseObject = results.FirstOrDefault(); //there should be only one if everything is working 
                }

                foreach ( var record in records)
                {
                    parseObject[record.Key] = record.Value;
                }

                parseObject["user"] = ParseUser.CurrentUser;
                parseObject.ACL = new ParseACL(ParseUser.CurrentUser);
                parseObject.ACL.PublicWriteAccess = true;
                parseObject.ACL.PublicReadAccess = true;

                await parseObject.SaveAsync();
                return true;
            }
            catch (ParseException pe)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateClass parse exception: {pe.Message}");
                return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateClass exception: {e.Message}");
                return false;
            }
        }

        public async Task<Dictionary<string, object>> GetOEMRecordsThatHaveMemberMatched(string className, string member)
        {
            try
            {
                Dictionary<string, object> supplyChainAccessObjects = new Dictionary<string, object>();
                Dictionary<string, object> customAccessObjects = new Dictionary<string, object>();
                ParseQuery<ParseObject> query = ParseObject.GetQuery("SupplyChain");

                // Query supplier Chain here
                IEnumerable<ParseObject> results = await query.WhereEqualTo("CompanyName", member).FindAsync();

                if (results.Any())
                {
                    foreach (var result in results)
                    {
                        // Format {"CustomAccessObject": { "FileList": [ "OEMTest.TACScreen" ] }}
                        customAccessObjects.Add(
                            result.Get<string>("ProfileNumber"),
                            result.Get<Dictionary<string, object>>("CustomAccessObject"));
                    }

                    supplyChainAccessObjects.Add("SupplyChains", customAccessObjects);
                    return supplyChainAccessObjects;
                }

                return new Dictionary<string, object>() { { "Error", $"Nothing here!!!" } }; ;
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object>() { { "Error", $"SomethingNew Exception: {ex.Message}" } };
            }
        }

        public async Task<bool> DoesFileNameExist(string fileName)
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("SupplyChain");
            int numberOfResult = await query.WhereContainedIn("CustomAccessObject.FileList", new List<string>() { fileName })
                                            .CountAsync();
            return numberOfResult > 0;
        }
    }
}