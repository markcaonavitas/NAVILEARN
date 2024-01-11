using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParseManager
{
    public interface IParseClientManager
    {
 
        /// <summary>
        /// Start scanning for devices.
        /// </summary>
        void Initialize();
        void Initialize(string applicationId, string server);
        Task InitializeTest();

        string GetApplicationId();
        string GetServer();

        /// <summary>
        /// Initialize the parameters list so that datalogged info can be added. 
        /// </summary>
        void InitParametersList(string PresentConnectedController);
        Task<bool> CheckUsernameExist(string username);

        /// <summary>
        /// Granted specific user to unlock the golf cart by add their username to owner's registeredControllerUser array
        /// </summary>
        /// <param name="controllerSerialNumber"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> AddUsernameToRegisteredMotorControllerUsers(string controllerSerialNumber, string userName);
        Task RequestPasswordReset(string emailAddress);

        /// <summary>
        /// Adds the logged parameter data to the dictionary.
        /// </summary>
        /// <param name="ParameterName">The logged parameter name</param>
        /// <param name="Times">logged parameter timestamps</param>
        /// <param name="Data">logged parameter timestamps</param>

        void AddLoggedParameterData(string ParameterName, string[] Times, double[] Data);
        Task<IList<string>> GetRegisteredMotorControllerUsersList(string controllerName);

        /// <summary>
        /// Transmits the data to the ParseServer
        /// </summary>
        /// <param name="DeviceName">Serial Number of the device</param>
        Task Transmit(string DeviceName);
        Task<Dictionary<string, string> >UpdateVehicleParseClass(string className, Dictionary<string, object> vehicleHealthRecord, Dictionary<string, object> vehicleSettingsRecord);

        Task TransmitSupplierRoleClass(string className, Dictionary<string, object> record);

        Task TransmitRegisteredUsers(string className, string presentConnectedController);

        Task<bool> IsRegisteredUser(string bleControllerSerialNumber);

        Task TransmitUserLevel(string appConfigurationLevel);

        Task<Dictionary<string, object>> GetCustomAccessObjects(string presentConnectedController);
        Task<Dictionary<string, object>> GetCustomAccessObjects(string presentConnectedController, CancellationToken cancellationTokens);

        Task<Dictionary<string, object>> GetParentRoleAccessObjects(string presentConnectedController);
        /// <summary>
        /// Logs out of the  ParseServer
        /// </summary>
        Task LogOut();

        /// <summary>
        /// Logs in to the  ParseServer
        /// </summary>
        /// <param name="Username">User Name</param>
        /// <param name="Password">Password</param>
        Task <string> Login(string Username, string Password);

        /// <summary>
        /// Checks if the user is logged in already
        /// </summary>
        bool IsUserLoggedInAlready();

        Task<bool> IsEmailVerified();
        Task<bool> IsEmailVerified(string emailAddress);
        Task RemoveUser();
        void ResetEmail(string emailAddress);

        /// <summary>
        /// Creats a user on the parse server
        /// </summary>

        /// <param name="username">User Name</param>
        /// <param name="password">Password</param>
        /// <param name="email">User email</param>
        /// <param name="phonenumber">phonenumber</param>

        Task SignUp(string username, string password, string email);
        string GetDatalogUniqueId();
        Dictionary<string, object> GetDatalogObject();
        Task<bool> UpdateAppConfigurationLevel(Dictionary<string, string> personalInfo);
        Task<bool> IsControllerRegistered(string presentConnectedController);
        Task<bool> DeleteUserInfoFromVehicleRecord(string controllerSerialNumber, string driverBeingRemoved);
        Task<List<Dictionary<string, object>>> GetListOfRegisterControllers();

        //Task<double> RequestSoftwareRevision(string MotorController);

        //bool doesParentRoleExist();
        //Task<Dictionary<string, object>> GetParentCustomFileScreen(int lastTwoDigit);

        Task<string[]> GetSupplierChainAndCompanyName(string supplierChainId, string companyId);
        Task<string> GetModelName(string className, string companyName, string modelId);

        Task<bool> IsUserSessionValid();
        Task<bool> UploadVEEPROM(string filePath, string className = "TroubleshootingVEEPROM");

        Task<bool> UploadScopeLog(Dictionary<string, object> vehicleSettings);
        Task SaveMaxSpeedRecord(string presentConnectedController, double maxSpeed);
        Task SaveMaxBatteryCurrentRecord(string presentConnectedController, double maxSpeed);        
        string GetCorrectUsername();
        Task<Dictionary<string, object>> GetCustomAccessObjectsFromSupplyChain(string className, Dictionary<string, object> testDictionary);
        string GetMemberOfList();
        Task<Dictionary<string, object>> GetLatestModifiedSupplierChain(string controllerType);
        Task<List<Dictionary<string, object>>> GetSupplierChain(string controllerType);
        Task<string> GetNickName(string controllerSerialNumber);
        Task<bool> SaveNickName(string controllerSerialNumber, string nickName);
        // WT May 2023, the reason I default className as troubleshooting
        // because when I first created it, I did not think further ahead to have a function that could be extended
        Task UploadFileToObjectStorage(string filePath, string className = "TroubleshootingVEEPROM");

        Task UploadFileToObjectStorage(string filePath, string className = "TroubleshootingVEEPROM", string key = "", string value = "");
        Task <bool>UpdateClass(string className, Dictionary<string, object> record);
        Task<Dictionary<string, object>> GetOEMRecordsThatHaveMemberMatched(string className, string member);
        Task<bool> DoesFileNameExist(string fileName);
    }
}
