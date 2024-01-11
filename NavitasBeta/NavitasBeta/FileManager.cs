using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NavitasBeta
{
    public static class FileManager
    {
        public static IFileUtilities XPFileUtilities = DependencyService.Get<IFileUtilities>();
        public static async Task SendEmailWithOptionalAttachment(string content, string filePath = null)
        {
            try
            {
                var message = new EmailMessage
                {
                    Body = content,
                    To = new List<string>() { "datalog@navitasvs.com" }, // Tech Support Email 
                    BodyFormat = EmailBodyFormat.Html,
                };

                //Email include an attachment only file path existed
                if (filePath != null)
                {
                    message.Subject = $"{Authentication.GetUserCredentials()["UserName"]} datalog";
                    message.Attachments.Add(new EmailAttachment(filePath));
                }
                else
                    message.Subject = $"{ControllerTypeLocator.ControllerType} FAULTS";

                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device  
                System.Diagnostics.Debug.WriteLine("Email is not supported on this device" + fbsEx.Message.ToString());
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                System.Diagnostics.Debug.WriteLine("Send Email Exception" + ex.Message.ToString());
            }
        }
        public static string ConcatenateString(List<string> stringList)
        {
            string longString = "";
            for (int i = 0; i < stringList.Count; i++)
            {
                // last item will not end with commas
                longString += (i < stringList.Count - 1) ? $"{stringList[i]}," : $"{stringList[i]}";
            }
            return longString;
        }
        public static void AddVehicleInfoToPersistentProperty(List<Vehicle> vehicles)
        {
            List<string> RegisterControllers = new List<string>();
            List<string> ControllersNickName = new List<string>();

            for (int i = 0; i < vehicles.Count; i++)
            {
                RegisterControllers.Add(vehicles[i].Name);
                ControllersNickName.Add(vehicles[i].NickName);
            }

            Authentication.SaveUserListOfRegisterControllers(ConcatenateString(RegisterControllers));
            Authentication.SaveNickName(ConcatenateString(ControllersNickName));
        }

        static Assembly assembly = typeof(NavitasGeneralPage).GetTypeInfo().Assembly;
        /// <summary>
        /// Deserialize xml looking for External files with newer date else get internal file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="interalFilePath"></param>
        /// <returns></returns>
        public static T DeserializeExternalFirstOrInternalXml<T>(string fileName, string interalFilePath)
        {
            // Check this directory for latest file
            string[] fileList = GetNavitasDirectoryFiles();
            Stream streamToRead = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T deserializedObj = default(T);

            // Loop through and find a file contains a keyword
            foreach (var filePath in fileList)
            {
                //allows prefixed renaming, ie CustomerNameTACDictionary.xml
                if (filePath.Contains(fileName))
                {
                    var highestPriorityFileName = GetFileNameWithHighestPriority(Path.GetFileName(fileName));
                    // GetFileDateAndTime accepts file path instead
                    var externalXMLFileLastModified = XPFileUtilities
                                                      .GetFileDateAndTime(Path.Combine(GetNavitasDirectoryPath(), highestPriorityFileName))
                                                      .ToUniversalTime();

                    try
                    {
                        if (App.AppConfigurationLevel != "DEV")
                        {
                            //  resource might be newer than the one exists in external storage ??
                            if (App.ReleasedDateTime > externalXMLFileLastModified)
                            {
                                streamToRead = GetInternalFileStream(interalFilePath);
                            }
                            else
                            {
                                streamToRead = GetExternalFileStream(fileName);
                                if (fileName.Contains("Battery")) App.BatteryGroupLastModified = externalXMLFileLastModified;
                            }
                        }
                        else
                        {
                            streamToRead = GetExternalFileStream(fileName);
                            if (fileName.Contains("Battery")) App.BatteryGroupLastModified = externalXMLFileLastModified;
                        }
                        //Keep commented out writer below incase you would like to know how to
                        //create a new XML format while developing
                        //StreamWriter streamToWrite = new StreamWriter(CreateStream(Path.Combine(GetNavitasDirectoryPath(), "TACDictionary1")));
                        //new XmlSerializer(typeof(ObservableCollection<GoiParameter>)).Serialize(streamToWrite, GoiParameterList);//Writes to the file
                        //streamToWrite.Dispose(); //close the file for others to read

                        // deserilize a null file will throw exception
                        deserializedObj = (T)serializer.Deserialize(streamToRead);
                    }
                    catch (Exception ex)
                    {
                        // Must be failed to deserialize
                        System.Diagnostics.Debug.WriteLine($"ExtractXml ex: {ex.Message}");
                    }
                }
            }

            if (deserializedObj == null)
            {
                streamToRead = GetInternalFileStream(interalFilePath);
                deserializedObj = (T)serializer.Deserialize(streamToRead);
            }

            streamToRead.Dispose();
            return deserializedObj;
        }

        public static async Task DetermineBatteryGroup(string fileName)
        {
            var (contentLength, lastModified) = await XPFileUtilities.GetResponseInfoFromHeader($"https://navitascustomdownloads.oss.nodechef.com/{fileName}");
            Stream streamToRead = null;
            var filePath = Path.Combine(GetNavitasDirectoryPath(), GetFileNameWithHighestPriority(fileName));

            try
            {
                if (App.AppConfigurationLevel != "DEV")
                {
                    // Decice to Deserialize this file or not 
                    if (lastModified.ToUniversalTime() > App.BatteryGroupLastModified)
                    {
                        streamToRead = GetExternalFileStream(filePath);
                        var parameterSuperSet = DeserializeXML<List<ParameterFile>>(streamToRead);
                        App.ViewModelLocator.MainViewModel.ConstructBatteryGroupedParameterFile("Modified", parameterSuperSet);
                        App.BatteryGroupLastModified = lastModified.ToUniversalTime();
                    }
                }
                else
                {
                    // Don't change DEV's default behavior
                    if (XPFileUtilities.GetFileDateAndTime(filePath).ToUniversalTime() > App.BatteryGroupLastModified)
                    {
                        streamToRead = GetExternalFileStream(filePath);
                        var parameterSuperSet = DeserializeXML<List<ParameterFile>>(streamToRead);
                        App.ViewModelLocator.MainViewModel.ConstructBatteryGroupedParameterFile("Modified", parameterSuperSet);
                        App.BatteryGroupLastModified = XPFileUtilities.GetFileDateAndTime(filePath).ToUniversalTime();
                    }
                }
            }
            catch (Exception ex)
            {

                // Must be failed to deserialize
                System.Diagnostics.Debug.WriteLine($"ExtractXml ex: {ex.Message}");

                var interalFilePath = assembly.GetManifestResourceNames().Where(path => path.Contains(fileName)).FirstOrDefault();
                streamToRead = GetInternalFileStream(interalFilePath);
                var parameterSuperSet = DeserializeXML<List<ParameterFile>>(streamToRead);
                App.ViewModelLocator.MainViewModel.ConstructBatteryGroupedParameterFile("Modified", parameterSuperSet);
            }

            streamToRead?.Dispose();
        }

        public static T DeserializeXML<T>(Stream streamToRead)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(streamToRead);
        }


        public static async Task<Dictionary<string, object>> CreateMockObject(int numberOfObject)
        {
            List<Task<Dictionary<string, object>>> tasks = new List<Task<Dictionary<string, object>>>();


            for (int i = 0; i < numberOfObject; i++)
            {
                tasks.Add(GenerateParameter(i));
            }
            Dictionary<string, object>[] results = await Task.WhenAll(tasks);

            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            dictionary.Add("Test", results);

            return dictionary;
        }

        static async Task<Dictionary<string, object>> GenerateParameter(int index)
        {
            Dictionary<string, object> taskResult = new Dictionary<string, object>();
            taskResult.Add($"Parameter {index}", await GenerateTimeObjects(5));

            return taskResult;
        }

        static async Task<List<object>> GenerateTimeObjects(int second)
        {
            List<object> timeObjects = new List<object>();
            var starttime = DateTime.Now;
            while ((DateTime.Now - starttime) < TimeSpan.FromSeconds(second))
            {
                // "Time", DateTime.Now.ToString()
                Dictionary<string, object> timeanddata = new Dictionary<string, object>();
                timeanddata.Add("time", System.DateTime.Now.ToString());
                timeanddata.Add("value", 1);
                timeObjects.Add(timeanddata);
                // Simulate some asynchronous operation
                await Task.Delay(100);
            }

            return timeObjects;
        }

        public static void WriteMockObjectAsFile(string filePath, Dictionary<string, object> logObject)
        {
            string json = JsonConvert.SerializeObject(logObject);
            StreamWriter streamToWrite = new StreamWriter(CreateStream(filePath));
            streamToWrite.Write(json);
            streamToWrite.Dispose();
        }
        public static (int JSONFileCounter, int CSVFileCounter) SearchForJsonAndCSVExtensions()
        {
            return (GetNavitasDirectoryFilesContaining(".json").Length, GetNavitasDirectoryFilesContaining(".csv").Length);
        }

        public static T GetDeserializedObject<T>(string fileName)
        {
            //TODO: check priority
            // To get correct file, filelist should capable of sorting and finding the highest Priority file
            // OR may be filename without "P" prefix
            Stream streamToRead = GetExternalFirstOrInternalFileStream(fileName);
            if (streamToRead != null)
            {
                T deserializedStream = DeserializeXML<T>(streamToRead);
                streamToRead.Dispose();
                return deserializedStream;
            }
            else 
            {
                return default(T); //I don't thinks this returns an empty object
            }
        }

        public static string GetExternalFirstOrInternalFileText(string fileNameAndPath)
        {
            Stream streamToRead;
            if (fileNameAndPath.Contains("\\") || fileNameAndPath.Contains("/"))
                streamToRead = GetExternalFileStream(fileNameAndPath);
            else
                streamToRead = GetInternalFileStream(fileNameAndPath);

            StreamReader streamReader = new System.IO.StreamReader(streamToRead);
            string text = streamReader.ReadToEnd();
            streamToRead.Dispose();
            return text;
        }

        public static string[] GetNavitasDirectoryFiles()
        {
            return XPFileUtilities.DirectoryGetFiles("Navitas");
        }

        public static string[] GetNavitasDirectoryFilesContaining(string identifier)
        {
            List<string> fileList = GetNavitasDirectoryFiles().ToList();
            List<string> searchedFileList = new List<string>();
            if (fileList != null)
            {
                foreach (var file in fileList)
                {
                    if (file.Contains(identifier))
                        searchedFileList.Add(file);
                }
                return searchedFileList.ToArray();
            }
            else
                return searchedFileList.ToArray();
        }
        public static string GetNavitasDirectoryPath()
        {
            return XPFileUtilities.GetDirectoryPath("Navitas");
        }

        public static string[] GetInternalDirectoryFiles()
        {
            return assembly.GetManifestResourceNames();
        }
        public static string[] GetInternalDirectoryFilesContaining(string identifier)
        {
            List<string> fileList = assembly.GetManifestResourceNames().ToList();
            List<string> searchedFileList = new List<string>();
            if (fileList != null)
            {
                foreach (var file in fileList)
                {
                    if (file.Contains(identifier))
                        searchedFileList.Add(file);
                }
                return searchedFileList.ToArray();
            }
            else
                return searchedFileList.ToArray();
        }
        public static Stream GetInternalFileStream(string fileNameAndPath)
        {
            return assembly.GetManifestResourceStream(fileNameAndPath);
        }
        public static Stream GetExternalFileStream(string fileName)
        {
            return XPFileUtilities.GetFileStream(Path.Combine(GetNavitasDirectoryPath(), GetFileNameWithHighestPriority(fileName)));
        }

        public static Stream GetExternalFirstOrInternalFileStream(string fileName)
        {
            Stream searchedFile = GetExternalFileStream(GetFileNameWithHighestPriority(fileName));
            if (searchedFile == null)
            {
                var interalFilePath = assembly.GetManifestResourceNames()
                               .Where(path => path.Contains(fileName))
                               .FirstOrDefault();
                searchedFile = GetInternalFileStream(interalFilePath);
            }

            return searchedFile;
        }

        public static Stream GetInternalFirstOrExternalFileStream(string fileName)
        {
            var interalFilePath = assembly.GetManifestResourceNames()
                           .Where(path => path.Contains(fileName))
                           .FirstOrDefault();
            Stream searchedFile = null;
            if (interalFilePath != null)
                searchedFile = GetInternalFileStream(interalFilePath);

            if (searchedFile == null)
                searchedFile = GetExternalFileStream(GetFileNameWithHighestPriority(fileName)); //is this a name or name and path

            return searchedFile;
        }


        /// <summary>
        /// Search for external files first then internal
        /// 0 Length Array means no files found
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>non null Array</returns>
        public static string[] GetExternalFirstOrInternalDirectoryFilesContaining(string identifier)
        {
            string[] searchedFileList = GetNavitasDirectoryFilesContaining(identifier);

            if (searchedFileList.Length == 0)
                searchedFileList = GetInternalDirectoryFilesContaining(identifier);

            return searchedFileList;
        }
        /// <summary>
        /// Search for internal files first then external
        /// 0 Length Array means no files found
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>non null Array</returns>
        public static string[] GetInternalFirstOrExternalDirectoryFilesContaining(string identifier)
        {
            string[] searchedFileList = GetInternalDirectoryFilesContaining(identifier);

            if (searchedFileList.Length == 0)
                searchedFileList = GetNavitasDirectoryFilesContaining(identifier);

            return searchedFileList;
        }

        /// <summary>
        /// File Utilities to decide if supply chain is embedded or external from SupplyChain parse class
        /// with some date time logic
        /// </summary>
        /// <returns></returns>
        public static async Task CheckSupplierChainUpdates()
        {
            try
            {
                var prefix = ControllerTypeLocator.ControllerType == "TAC" ? "" : "TSX";
                //Check latest result from cloud
                var supplierChain = await App.ParseManagerAdapter.GetLatestModifiedSupplierChain(ControllerTypeLocator.ControllerType);
                var path = Path.Combine(GetNavitasDirectoryPath(), $"{prefix}SupplierChain.xml");

                if(!supplierChain.Keys.Any(key => key == "Error"))
                {
                    DateTime objectLastModifiedTime = (DateTime)supplierChain["UpdatedAt"];
                    DateTime xmlFileLastModifiedTime;

                    // The list must be old, so update it
                    if (XPFileUtilities.FileExists(path))
                        xmlFileLastModifiedTime = XPFileUtilities.GetFileDateAndTime(path).ToUniversalTime();
                    else
                        xmlFileLastModifiedTime = XPFileUtilities.GetAssemblyLastModifiedTimeUTC();

                    if (objectLastModifiedTime > xmlFileLastModifiedTime)
                    //await SerializeAndUpdateSupplierChain();
                    //private async Task SerializeAndUpdateSupplierChain()
                    {
                        await ExtractSupplierChainObjectFromCloud();
                    }
                }
            }
            catch (Exception e)
            {
                // We might or might not know what the exception was caused by
                // But we just let the app goes through without crashing
                System.Diagnostics.Debug.WriteLine("CheckSupplierChainUpdates Exception: " + e.Message);
            }
        }
        /// <summary>
        /// Remove files not in User Custom Access Object if user is not DEV
        /// except for some internally generated files:
        /// TODO: List the rules here.
        /// TODO: remove legacy stuff (things that don't indicated where they were downloaded from
        ///     Parameter files containing the "CRC=" before .xml
        ///     ScopeSettings
        ///     Screens?
        /// </summary>
        /// <returns></returns>
        /// 

        private static async Task ExtractSupplierChainObjectFromCloud()
        {
            var prefix = ControllerTypeLocator.ControllerType == "TAC" ? "" : "TSX";
            var supplierChainCollection = await App.ParseManagerAdapter.GetSupplierChain(ControllerTypeLocator.ControllerType);
            var serialize = JsonConvert.SerializeObject(supplierChainCollection);
            var supplierDeserialized = JsonConvert.DeserializeObject<List<SupplierChain>>(serialize);
            ExportAFileThroughStreamWriter(supplierDeserialized, $"{prefix}SupplierChain.xml");
            FillNewIndividualSupplierChain(supplierDeserialized);
        }

        public static void ExportAFileThroughStreamWriter<T>(List<T> deserializedObject, string fileName)
        {
            StreamWriter streamToWrite = new StreamWriter(
                    CreateStream(Path.Combine(GetNavitasDirectoryPath(),
                                fileName)
                    ));
            new XmlSerializer(typeof(List<T>)).Serialize(streamToWrite, deserializedObject);//Writes to the file
            streamToWrite.Dispose();
        }

        private static void FillNewIndividualSupplierChain(List<SupplierChain> supplierDeserialized)
        {
            //var strPartProfileNumber = App.ViewModelLocator.GetParameter("PARPROFILENUMBER").parameterValue.ToString();
            //Manually update InitialSC.SupplierChains's reference
            //UpdateExistingSupplierChains(supplierDeserialized);
            //private void UpdateExistingSupplierChains(List<SupplierChain> supplierDeserialized)
            var startInsertIndex = InitialSC.SupplierChains.Count;
            for (int i = 0; i < supplierDeserialized.Count; i++)
            {
                if (i < startInsertIndex)
                    InitialSC.SupplierChains[i] = supplierDeserialized[i];
                else
                    InitialSC.SupplierChains.Add(supplierDeserialized[i]);
            }
        }

        public static void CheckAndRemoveFiles(List<string> FileList, string objectType)
        {
            //TODO: check priority
            Regex parameterFileRegex = new Regex(@"CRC=(.*?).xml$");
            Regex scopeSettingsRegex = new Regex(@"^ScopeSettings(.*?).xml$");
            // Priority Excluded - P1, P2
            //Always, Always describe all regex patterns in English words
            Regex legacyCustomScreenRegex = new Regex(@"^(?![P]\d+)(\w+)_([-+?\w]+)_(\w+)(?=.(TACscreen|TSXscreen))");

            var externalPublicFileList = GetNavitasDirectoryFiles().ToList();

            // New rule
            //Always, Always describe all regex patterns in English words
            var newPriorityRegEx = new Regex(priorityPattern.Replace("P\\d+", GetPriority(objectType)));
            //For old Apps User Custom Access Objects should remove things without the P1 priority
            //var LegacyPriorityPattern = priorityPattern.Replace("P\\d+", "");
            //var LegacyPriorityRegEx = new Regex(LegacyPriorityPattern);

            if (externalPublicFileList != null && App.AppConfigurationLevel != "DEV") //nothing there? or didn't get access?
            {
                //clean everything except files from custom access object
                var filteredList = externalPublicFileList.Where(path => (legacyCustomScreenRegex.IsMatch(path) ||
                                                                        newPriorityRegEx.IsMatch(path)) &&
                                                                        !path.Contains("SupplierChain.xml"))
                                                         .ToList();
                // priorityRegex.Match(path).Success
                System.Diagnostics.Debug.WriteLine($"CheckAndRemoveFiles -- priority {GetPriority(objectType)}");
                if (App.AppConfigurationLevel == "ENG")
                {
                    // ENG
                    //Filtering out the list above, due to ENG has more files need to retain for later use
                    filteredList = filteredList.Where(path => !(parameterFileRegex.IsMatch(path) ||
                                                                scopeSettingsRegex.IsMatch(path) ||
                                                                path.Contains(".txt")))
                                                .ToList();
                }

                // Applied for USER, ADVANCED_USER, DEALER , ENG
                filteredList.ForEach(path => 
                {
                    if(!FileList.Contains(Regex.Replace(Path.GetFileName(path), @"^[.P\d_]+", ""))) DeleteFile(path);
                });
            }
        }

        public static async Task CheckAndRemoveFilesSupplierChain(string objectType)
        {
            //Always, Always describe all regex patterns in English words
            //Regex customScreenIncludePriorityRegex = new Regex(@"(?![.P\d_]+).*(?=.T(AC|SX)[Ss]creen)");

            var externalPublicFileList = GetNavitasDirectoryFiles().ToList();
            var newPriorityRegEx = new Regex(priorityPattern.Replace("P\\d+", GetPriority(objectType)));

            if (externalPublicFileList != null && App.AppConfigurationLevel != "DEV")
            {
                // USER, ADVANCED_USER, DEALER, ENG
                // Do not exclude any file name here
                var filteredList = externalPublicFileList.Where(path => newPriorityRegEx.IsMatch(path))
                                                         .ToList();
               
                // Iterate each fileName and double check on the cloud before remove it from the external storage
                foreach (var path in filteredList)
                {
                    var fileName = Regex.Replace(Path.GetFileName(path), @"^[.P\d_]+", "");
                    if (!await App.ParseManagerAdapter.DoesFileNameExist(fileName)) DeleteFile(path);
                }
            } 
        }

        //any of(
        //(capture (literally "P", digit, once or more, capture (literally ".", digit, once or more)), literally "_", anything, literally ".", capture(any of (literally "xml", (literally "TSX", optional, literally "hex"), (literally "txt", literally "T", capture(literally "AC", literally "SX"), capture(literally "Firmware"), one of "Ss", literally "creen"))), must end ), (anything, never or more, literally "_", capture (literally "P", digit, once or more, capture(literally ".", digit, once or more)), literally ".", capture (any of(literally "xml", (literally "TSX", optional, literally "hex"), (literally "txt", literally "T", capture(literally "AC", literally "SX"), capture(literally "Firmware"), one of "Ss", literally "creen"))), must end )
        //)
        //Always, Always describe all regex patterns in English words
        //TODO: check priority
        static string priorityPattern = @"(?:(P\d+(\.\d+)?)_.*\.([Ss]creen|html|hex|xml|TSX?hex|txt|T(AC|SX)(Firmware)?[Ss]creen|T(AC|SX)(Navigate)?[Ss]creen)$)|(?:.*_(P\d+(\.\d+)?)\.([Ss]creen|html|hex|xml|TSX?hex|txt|T(AC|SX)(Firmware)?[Ss]creen|T(AC|SX)(Navigate)?[Ss]creen)$)";
        static Regex priorityRegex = new Regex(priorityPattern);

        static long fileSizeFromRequestedURL;



        public static async Task ValidateAndDownloadFiles(List<string> fileList, string objectType = "")
        {
            //TODO: check priority
            foreach (var file in fileList)
            {
                string navitasFile = Path.Combine(GetNavitasDirectoryPath(), file);
                var (contentLength, lastModified) = await XPFileUtilities.GetResponseInfoFromHeader("https://navitascustomdownloads.oss.nodechef.com/" + file);

                // This function call guarantee backward compatible
                // It will replace original file name with the new priority prefix attached
                var fileName = GetFileNameWithHighestPriority(navitasFile);

                var b = XPFileUtilities.GetFileDateAndTime(Path.Combine(GetNavitasDirectoryPath(), fileName));
                var fileSize = GetFileSize(Path.Combine(GetNavitasDirectoryPath(), fileName));

                //DateTime.MinValue means the url failed (most likely a timeout)
                //check for newer url files or previous failed downloads
                if ((lastModified > b || (fileSize <= 4)) && lastModified != DateTime.MinValue)
                {
                    // WT Jun 09, next time replace these global vars by putting them into DownloadEventArgs
                    fileSizeFromRequestedURL = contentLength;

                    //if file being updated already exsists and is not 0 bytes
                    if (XPFileUtilities.FileExists(navitasFile) && fileSize != 0)
                    {
                        BackUpRecentFile(navitasFile, file);
                    }

                    await XPFileUtilities.DownloadFile("https://navitascustomdownloads.oss.nodechef.com/" + file, objectType, GetNavitasDirectoryPath());
                }
            }
        }

        public static string GetPriorityFilteredFileNameIfExist(string file, string objectType, string navitasFile)
        {
            // newPriorityRegEx will allow the LINQ filter out unexpedted priority file, but the selected one
            // For example P1 will accpet P1.x or P1 prefix
            // KEEP IN MIND the LINQ only select the first file that match the regex
            //var newPriorityPattern = priorityPattern.Replace("P\\d+", GetPriority(objectType));
            //var newPriorityRegEx = new Regex(newPriorityPattern);

            //var priorityFileNameFound = GetNavitasDirectoryFiles().ToList().Select(aFile => Path.GetFileName(aFile))
            //                                                               .Where(aFile => newPriorityRegEx.IsMatch(aFile) && aFile.Contains(file))
            //                                                               .FirstOrDefault() ?? "Not Exist";

            var highestPriorityFileNameFound = GetFileNameWithHighestPriority(navitasFile);
            // Should customize the query a bit

            return highestPriorityFileNameFound;
        }

        public static void BackUpRecentFile(string navitasFile, string file)
        {
            string backupFile = Path.Combine(GetNavitasDirectoryPath(), file + ".backup");

            //Copy and paste back up file in the same directory
            XPFileUtilities.CopyFile(navitasFile, backupFile);
        }

        private static string ModifiyFileNameBaseOnPriority(string fileName, string objectType)
        {
            return $"{GetPriority(objectType)}_{fileName}";
        }

        private static string GetPriority(string objectType)
        {
            string priority = "";
            switch (objectType)
            {
                case "CustomAccessObject":
                    priority = "P1";
                    break;
                case "SupplyChainAccessObject":
                    priority = "P2";
                    break;
                default:
                    priority = "Error";
                    break;
            }

            return priority;
        }

        private static string GetFileNameWithHighestPriority(string fileNameAndPath)
        {
            Regex PriorityNumberExtractor = new Regex(@"^(P\d+(\.\d+)?)_"); //(^(P\d + (\.\d +)?)_)
            //filename = "P2_BatteryGroupedParameterFile.xml"; //just for test
            string highestPriorityFileName = fileNameAndPath;
            float highestPriorityNumber;
            MatchCollection initialFilePriority = PriorityNumberExtractor.Matches(fileNameAndPath); //split Prioity from string
 
            if (initialFilePriority.Count > 0) //has a priority #
                highestPriorityNumber = float.Parse(initialFilePriority[0].Value.Replace("P", "").Replace("_", ""));
            else  //make this a low priority if it has no number
                highestPriorityNumber = 100;
            try
            {
                string noPriorityFilename = PriorityNumberExtractor.Replace(Path.GetFileName(fileNameAndPath), ""); //remove priority P#_
                string[] allMatchingFiles = GetNavitasDirectoryFilesContaining(noPriorityFilename); // get all with same name
                foreach (var name in allMatchingFiles)
                {//search for highest priority
                    MatchCollection namePriority = PriorityNumberExtractor.Matches(Path.GetFileName(name));  //split Prioity from string
                    float namePriorityNumber;
                    if (namePriority.Count > 0) //has a priority #
                        namePriorityNumber =  float.Parse(namePriority[0].Value.Replace("P", "").Replace("_", "")); //extract number
                    else  //make this a low priority if it has no number
                        namePriorityNumber = 100;
                    if (namePriorityNumber < highestPriorityNumber)
                    {//the lowest priority # is the highest priority
                        System.Diagnostics.Debug.WriteLine("FileManager overloaded " + highestPriorityFileName + " with " + name);
                        highestPriorityFileName = name;
                        highestPriorityNumber = namePriorityNumber;
                    }
                }
                
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return highestPriorityFileName;
        }

        /// <summary>
        /// File Utilities to either replace standard TABs or add Tabs
        /// TODO: describe this better Screens, or html
        /// </summary>
        static public async Task AddLocalPublicFiles(UserTabbedPage userTabbedPageDisplayed)
        {
            Regex PriorityNumberExtractor = new Regex(@"^(P\d+(\.\d+)?)_");
            List<string> distintFileNames = new List<string>();

            //Update fileList anytime this function has been called
            //So the tab bar could update the new custom screens in real-time
            string[] fileList = GetNavitasDirectoryFiles();
            if (fileList != null) //nothing there? or didn't get access?
            {
                foreach (string filePath in fileList)
                {
                    var strippedFileName = PriorityNumberExtractor.Replace(Path.GetFileName(filePath), "");
                    if (!distintFileNames.Contains(strippedFileName))
                        distintFileNames.Add(strippedFileName);
                }
            }

            foreach (var fileName in distintFileNames)
            {
                var filePath = Path.Combine(GetNavitasDirectoryPath(), fileName);
                if(!XPFileUtilities.FileExists(filePath))  // no matter lowest or highest priority file path
                    filePath = GetNavitasDirectoryFiles().Where(p => PriorityNumberExtractor.IsMatch(Path.GetFileName(p)) && p.Contains(fileName)).First();

                var highestPriorityfileName = Path.GetFileName(GetFileNameWithHighestPriority(filePath));

                await ModifyUserTabbedPage(userTabbedPageDisplayed, highestPriorityfileName);

                if (highestPriorityfileName.Contains("BatteryGroupedParameterFile.xml"))
                {
                    await DetermineBatteryGroup(fileName);
                }
            }
        }

        public static async Task ModifyUserTabbedPage(UserTabbedPage userTabbedPageDisplayed,string fileName)
        {
            try
            {
                var indexToChange = userTabbedPageDisplayed.Children.Select((page, index) => new { page, index })
                                        .FirstOrDefault(c =>
                                        {
                                            //TODO: add filename field to dynamic page that was loaded and use (c.page as Page).fileName instead of (c.page as Page).Title
                                            var titleTrimmed = (c.page as Page).Title.Replace(" ", "");
                                            return DoesFileNameMeetAllConditions(fileName, new string[] { titleTrimmed });
                                        })?.index ?? -1;
                string extension = $".{ControllerTypeLocator.ControllerType}screen";
                Regex customScreenRegex = new Regex(@"(\w+)_([-+?\w]+)_(\w+)(?=.(TACscreen|TSXscreen))");

                //looking for files that match the title to replace
                if (indexToChange != -1)
                {
                    //ReplaceDefaultScreen
                    if (fileName.Contains("Scripting") && fileName.Contains(".html") && !fileName.Contains("._"))
                    {//same name as Tab so replace it
                        await userTabbedPageDisplayed.ManipulateTabbedPage("Replace", new ScriptingPage(fileName), indexToChange);
                    }
                    else if (fileName.Contains(extension) && !customScreenRegex.IsMatch(fileName))
                    {//same name as Tab so replace it
                        System.Diagnostics.Debug.WriteLine($"AddLocalPublicFiles Replace fileName: {fileName} with index {indexToChange}");
                        await userTabbedPageDisplayed.ManipulateTabbedPage("Replace", new DynamicPage(fileName), indexToChange);
                    }
                } //or just add a new page
                else
                {
                    if (fileName.Contains(".html") &&
                        fileName.Contains("Scripting") &&
                        !(fileName.Contains("Diag") || fileName.Contains("._"))) //odd copies created by MAC connection so ignore
                    {//just insert it
                        await userTabbedPageDisplayed.ManipulateTabbedPage("Add", new ScriptingPage(fileName, false, true));
                    }
                    else if (fileName.Contains(extension) && !customScreenRegex.IsMatch(fileName))
                    {//just insert it
                        System.Diagnostics.Debug.WriteLine($"AddLocalPublicFiles Add fileName: {fileName}");
                        await userTabbedPageDisplayed.ManipulateTabbedPage("Add", new DynamicPage(fileName));
                    }
                }
            }
            catch (Exception ex)
            {
                // ModifyUserTabbedPage
                System.Diagnostics.Debug.WriteLine($"ModifyUserTabbedPage Exception: {ex.Message}");
            }
        }

        // Create overload
        /// <summary>
        /// File Utilities to either replace standard TABs if they contain a profile syntax
        /// TODO: describe syntax, maybe battery group here???
        /// </summary>
        public static async Task AddLocalPublicFiles(string strPartProfileNumber, UserTabbedPage userTabbedPageDisplayed)
        {
            string[] fileList = GetNavitasDirectoryFiles();
            string extension = $".{ControllerTypeLocator.ControllerType}screen";
            List<int> pageIndexList = new List<int>() { 0, 1, 2 };
            //empty partProfileNumber will cause the LINQ retreive unexpected results
            if (fileList != null && strPartProfileNumber != "") //nothing there? or didn't get access?
            {
                foreach (string filePath in fileList)
                {
                    var fileName = Path.GetFileName(filePath);

                    //System.Diagnostics.Debug.WriteLine($"AddLocalPublicFiles overloaded fileName {fileName}");
                    //Get index to replace default tabbed pages with a custom ones
                    var indexToChange = userTabbedPageDisplayed.Children.Select((element, index) => new { element, index })
                        .FirstOrDefault(v => DoesFileNameMeetAllConditions(fileName, new string[] { v.element.Title, strPartProfileNumber }))
                        ?.index ?? -1;

                    

                    if (indexToChange != -1 && fileName.Contains(extension))
                    {//same name as Tab so replace it
                     //userTabbedPageDisplayed.Children[indexToChange] = new DynamicPage(fileName);
                        System.Diagnostics.Debug.WriteLine($"AddLocalPublicFiles overloaded replace with {fileName} - indexToChange {indexToChange}");
                        await userTabbedPageDisplayed.ManipulateTabbedPage("Replace", new DynamicPage(fileName), indexToChange);
                        pageIndexList.RemoveAt(indexToChange);
                    }

                    if (fileName.Contains("BatteryGroupedParameterFile.xml"))
                    {
                        await DetermineBatteryGroup(fileName);
                    }
                }

                //System.Diagnostics.Debug.WriteLine("userTabbedPage count: " + userTabbedPageDisplayed.Children.Count);

                if (userTabbedPageDisplayed.Children.Count >= 3)
                {
                    //var result = await RevertToDefaultScreens(pageIndexList, extension);
                    //async Task<bool> RevertToDefaultScreens(List<int> pageIndexList, string extension)
                    {
                        foreach (var index in pageIndexList)
                        {
                            // 0 is gauge page
                            if (index != 0)
                            {
                                // Revert to default screen
                                await userTabbedPageDisplayed.ManipulateTabbedPage("Replace",
                                    (index == 1) ?
                                    new DynamicPage($"UserDiagnosticsPage{extension}") :
                                    new DynamicPage($"DealerSettingsPage{extension}"),
                                    index);
                            }
                        }

                        var result = true;
                    }
                }
            }
        }
        static bool DoesFileNameMeetAllConditions(string fileName, string[] conditions)
        {
            foreach (string element in conditions)
            {
                if (!fileName.Contains(element))
                {
                    return false;
                }
            }
            return true;
        }

        static int FileDownloadSuccessTracker = 0;
        static int FileDownloadErrorTracker = 0;
        /// <summary>
        /// File Utilities to service file downloads complete
        /// TODO: describe this better
        /// </summary>
        private static void OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                FileDownloadSuccessTracker++;
            }
            else
            {
                FileDownloadErrorTracker--;
            }

            if (e.FileName != "")
            {
                string navitasFile = Path.Combine(GetNavitasDirectoryPath(), e.FileName);
                string backupFile = $"{navitasFile}.backup";
                var downloadedFileSize = GetFileSize(navitasFile);
                bool isFileBroken = downloadedFileSize == 0 || fileSizeFromRequestedURL == 0 || downloadedFileSize != fileSizeFromRequestedURL;

                if (isFileBroken || !e.FileSaved) // download failed
                {
                    //DisplayAlert("Update Rollback", "We will try again later", "Ok");
                    // If everthing works as expected
                    // the backup directory should always have zero files in it except when updating a file.
                    if (XPFileUtilities.FileExists(backupFile))
                    {
                        XPFileUtilities.CopyFile(backupFile, navitasFile);
                        DeleteFile(backupFile);
                    }
                    else //0-byte or file downloaded'size is not equal to its cloud version, go DELETE it
                        DeleteFile(navitasFile);
                }
                else // download success
                {
                    // Object Type found, create a copy of it with a prefix priority attached to naming convetion
                    // So we could trace the class (on the cloud) where this file came from
                    if (e.ObjectType != "" && !priorityRegex.IsMatch(e.FileName))
                    {
                        var newFileName = ModifiyFileNameBaseOnPriority(e.FileName, e.ObjectType);
                        XPFileUtilities.MoveFile(Path.Combine(GetNavitasDirectoryPath(), e.FileName),
                                            Path.Combine(GetNavitasDirectoryPath(), newFileName));
                    }

                    if (XPFileUtilities.FileExists(backupFile))
                    {
                        DeleteFile(backupFile);
                    }
                }

                //Clean it for next time use
                fileSizeFromRequestedURL = 0;
            }
        }

        private static SemaphoreSlim DownloadUpdatesSemaphore = new SemaphoreSlim(1, 1);
        /// <summary>
        /// File Utilities to download file from somewhere?
        /// TODO: describe this
        /// </summary>
        public static async Task DownloadUpdates(List<string> fileList, string objectType, UserTabbedPage userTabbedPageDisplayed)
        {
            await DownloadUpdatesSemaphore.WaitAsync();

            //string[] existingLocalPublicfileList = GetNavitasDirectoryFiles();
            //foreach (string fileName in existingLocalPublicfileList)
            //    if ((fileName.Contains(".TAChex") && App.PresentConnectedController.Contains("TAC")) || (fileName.Contains(".TSXhex") App.PresentConnectedController.Contains("TSX")))
            //            turn on download in menue


            FileDownloadSuccessTracker = 0;
            FileDownloadErrorTracker = 0;
            XPFileUtilities.OnFileDownloaded += OnFileDownloaded;

            await ValidateAndDownloadFiles(fileList, objectType);

            //wix website help document download test
            //XPFileUtilities.DownloadFile("http://docs.wixstatic.com/ugd/86af49_0e3e153a9bc04f4193ccd1b79e960f8b.pdf", "Navitas");

            //wix website login test
            //XPFileUtilities.DownloadData("http://www.navitasvs.com/_functions/apiForTokenForLogIn?userName=" + usernameEntry.Text + "&password=" + passwordEntry.Text, "Navitas");
            await Device.InvokeOnMainThreadAsync(() =>
            {
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
            });

            string downloadMessage = "Success: " + FileDownloadSuccessTracker.ToString() + " Failed: " + FileDownloadErrorTracker.ToString();
            if (FileDownloadSuccessTracker != 0 || FileDownloadErrorTracker != 0)
                userTabbedPageDisplayed.DisplayAlert("Updates Installed", downloadMessage, "Ok");

            XPFileUtilities.OnFileDownloaded -= OnFileDownloaded;
            DownloadUpdatesSemaphore.Release();
        }

        public static void AddLocalNavigationPublicFiles(MasterPage masterPage)
        {
            string[] fileList = new string[0];

            if (ControllerTypeLocator.ControllerType == "TAC")
                fileList = GetNavitasDirectoryFilesContaining(".TACNavigatescreen");
            if (ControllerTypeLocator.ControllerType == "TSX")
                fileList = GetNavitasDirectoryFilesContaining(".TSXNavigatescreen");
            foreach (var fileName in fileList)
            {
                var FileNameOnly = Path.GetFileNameWithoutExtension(fileName);
                FileNameOnly = FileNameOnly.Replace('_', ' ');
                if (!masterPage.masterPageItems.Where(x => x.Title.Contains(FileNameOnly)).Any())
                {//main menu item has not already been added
                    var targetType = typeof(DynamicPage);
                    if (FileNameOnly.Contains("html"))
                        targetType = typeof(ScriptingPage); //new DynamicPage(fileName),

                    masterPage.masterPageItems.Add(new MasterPageItem
                    {
                        Title = FileNameOnly,
                        //IconSource = "contacts.png",
                        TargetType = targetType,
                        FileName = fileName
                    });
                }
            }
        }

        /// <summary>
        /// File Utilities to add Helper TABs (I forget when)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> AddHelperTab(string fileName, UserTabbedPage userTabbedPageDisplayed)
        {
            App._devicecommunication.bEnableCommunicationTransmissions = false;
            string[] fileList = GetNavitasDirectoryFiles();
            bool hasHelpScreenAdded = false;

            //Check if this help file has existed in usertabbedpage
            var existingIndex = userTabbedPageDisplayed.Children.Select((p, i) => new { p, i })
                               .FirstOrDefault(c => fileName.Contains((c.p as Page).Title))
                               ?.i ?? -1;

            if (existingIndex == -1)
            {
                //does not exist, create one
                foreach (var filePath in fileList)
                {
                    if (filePath.Contains(fileName))
                    {
                        userTabbedPageDisplayed.Children.Add(new DynamicPage(filePath));
                        hasHelpScreenAdded = true;
                    }
                }

                if (!hasHelpScreenAdded)
                {
                    //Add help page directly from embbedded resource
                    userTabbedPageDisplayed.Children.Add(new DynamicPage(fileName));
                }
            }

            var index = 0;
            foreach (var x in DeviceComunication.PageCommunicationsListPointer) //TAC and TSX gauge pages are always index 0
            {
                if (index++ != 0)
                    x.parentPage.Active = false;
                else
                    x.parentPage.Active = true;
            }

            App._devicecommunication.bEnableCommunicationTransmissions = true;
            return true;
        }


        /// <summary>
        /// File Utilities to create or append files only from html for now
        /// TODO: describe this better
        /// </summary>
        static StreamWriter StreamToAppend;
        static string AppendedStreamPath = ""; //hence this is not reenterant
        public static void OpenOrAppendOrCloseFIle(string fileName, string data)
        {
            if (StreamToAppend != null && AppendedStreamPath != Path.Combine(GetNavitasDirectoryPath(), fileName))
            {//Someone forgot to Close this before starting another
                XPFileUtilities.CloseAppendFile(StreamToAppend);
                StreamToAppend = null;
            }

            if (data == "Close")
            {//Syntax for Closing or nobody tried to open it
                if (StreamToAppend != null) //something to close
                    XPFileUtilities.CloseAppendFile(StreamToAppend);
                StreamToAppend = null;
            }
            else if (StreamToAppend == null)
            {//Syntax for Opening
                AppendedStreamPath = Path.Combine(GetNavitasDirectoryPath(), fileName);
                StreamToAppend = XPFileUtilities.AppendFile(AppendedStreamPath);
                if (StreamToAppend != null)
                {
                    StreamToAppend.WriteLine(data);
                }
                //else it is probably open in excel if you are running the UWP App
            }
            else
            {//Syntax for Appending
                StreamToAppend.WriteLine(data);
            }
        }

        static DateTime newLiveReloadTime;
        static public bool checkLiveReload(string hTMLFileNameAndPath, DateTime pageliveReloadTime, HybridWebView hybridWebView)
        {
            newLiveReloadTime = XPFileUtilities.GetFileDateAndTime(hTMLFileNameAndPath);
            if (newLiveReloadTime > pageliveReloadTime)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (Device.RuntimePlatform != Device.iOS)
                    {//Andoid Reload() does not get the new file (maybe it caches it or something so do this
                     //Seriously I couldn't find another way to reload URI
                     //so I override OnElementPropertyChanged and change the uri property
                     //adding a ? at the end causes it to change and will be removed in OnElementPropertyChanged
                     //or if it is there, remove it
                        string JustToCauseAChange = "?";
                        if (hybridWebView.Uri.Contains(JustToCauseAChange))
                            JustToCauseAChange = "";
                        //now cause a OnElementPropertyChanged to notice the above change
                        if (hTMLFileNameAndPath.Contains("\\") || hTMLFileNameAndPath.Contains("/"))
                            hybridWebView.Uri = "ExternalLoader.html" + JustToCauseAChange;
                        else
                            hybridWebView.Uri = Path.Combine(GetNavitasDirectoryPath(), hTMLFileNameAndPath + JustToCauseAChange);
                    }
                    hybridWebView.Reload();
                    pageliveReloadTime = newLiveReloadTime;
                });
                return true;
            }
            return false;

        }

        public static string FindPublicFirstOrEmbeddedFilePathAndName(string fileNameToSearchFor)
        {

            string[] fileList = GetNavitasDirectoryFiles(); //Public App Directory, iOS DirectoryGetFiles will automatically use App Name

            if (fileList != null) //nothing there? or didn't get access?
            {
                foreach (var filePathAndName in fileList)
                {
                    if (filePathAndName.Contains(fileNameToSearchFor) && !filePathAndName.Contains("._")) //odd copies created by MAC connection so ignore
                    {
                        return Path.Combine(GetNavitasDirectoryPath(), fileNameToSearchFor);
                    }
                }
            }

            fileList = GetInternalDirectoryFiles(); //Public App Directory, iOS DirectoryGetFiles will automatically use App Name

            if (fileList != null) //nothing there? or didn't get access?
            {
                foreach (var filePathAndName in fileList)
                {
                    if (filePathAndName.Contains(fileNameToSearchFor) && !filePathAndName.Contains("._")) //odd copies created by MAC connection so ignore
                    {
                        return fileNameToSearchFor; //found it in embedded resources
                    }
                }
            }

            return "";
        }

        public static string[] GetFirmwareFile(FileItem fileItem, bool isFirmwareScreenExtensionFound)
        {
            string[] fileList;

            if (isFirmwareScreenExtensionFound)
            {
                fileList = GetExternalFirstOrInternalDirectoryFilesContaining(fileItem.ActualFileName);
            }
            else //Non-neb
            {
                fileList = GetInternalFirstOrExternalDirectoryFilesContaining(fileItem.ActualFileName);
            }

            return fileList;
        }
        public static string FirmwareFileNameValidation(string friendlyFileNameAndVersion, string filePath)
        {
            //If this pattern fileName_v6.3XX.hex is match, then attach v6.3XX to modelName
            Match match;
            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                if (filePath.Contains("_Rev"))
                    match = Regex.Match(filePath, @"(?<=Rev_).*(?=\.\d{3}\.TSXhex)", RegexOptions.IgnoreCase);
                else
                    match = Regex.Match(filePath, @"(?<=Rev\s).*(?=\.\d{3}\.hex)", RegexOptions.IgnoreCase);

                if (match.Success)
                    friendlyFileNameAndVersion += " " + match.Value;
                else
                    friendlyFileNameAndVersion = "";
            }
            else
            {
                match = Regex.Match(filePath, @"(?:_v).*(?=\.hex)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    if (!friendlyFileNameAndVersion.Contains("Program"))
                        friendlyFileNameAndVersion += " " + Regex.Replace(match.Value, "_", "");
                }
                else
                {
                    //Not match, return empty string
                    friendlyFileNameAndVersion = "";
                }
            }
            return friendlyFileNameAndVersion;
        }

        /// <summary>
        /// Not presently being used?
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        static public async Task UploadFile(string filePath, string className)
        {
            var fileName = $"{Regex.Match(filePath, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value}";

            // Write Object as a File
            // Replace mock object with an object sent from js command
            //WriteMockObjectAsFile(filePath, await CreateMockObject(6));

            try
            {
                // After the file was saved to interal storage, then upload to the cloud
                await App.ParseManagerAdapter.UploadFileToObjectStorage(filePath, className);
                await FileUploadHandler(filePath, App.ParseManagerAdapter.GetDatalogUniqueId(), fileName);
            }
            catch (NullReferenceException nre)
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    App._MainFlyoutPage.userTabbedPageDisplayed.DisplayAlert("Connection Error", "Please Check Your Internet Connection and try again", "OK");
                else
                    System.Diagnostics.Debug.WriteLine($"btnUploadClicked NullReferenceException: {nre.Message}");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Could not store file.")
                {
                    // This error came once in a while
                    App._MainFlyoutPage.userTabbedPageDisplayed.DisplayAlert("Error", "Could not store file. Please try again", "OK");
                }
                else
                {
                    //Unknown errors
                    System.Diagnostics.Debug.WriteLine("btnUploadClicked exception - Unkonwn Error: " + ex.Message);
                    await FileUploadHandler(filePath, App.ParseManagerAdapter.GetDatalogUniqueId(), fileName);
                }
            }
        }

        static public bool isExternalFile(string filePathAndName)
        {
            return (filePathAndName.Contains("\\") || filePathAndName.Contains("/"));
        }

        public static async void CreateVeepromFileAndUpload(bool isRestore, int[] fileBlock_6, int[] fileBlock)
        {
            StreamWriter sw = new StreamWriter(CreateStream(Path.Combine(GetNavitasDirectoryPath(), DateTime.Now.ToString("s").Replace(":", ".") + $"_{App.PresentConnectedController}_{App.ViewModelLocator.GetParameter("SOFTWAREREVISION").parameterValue}" + $"_{App.InfoService.Version}.txt")));
            sw.WriteLine("Sector 6:");
            foreach (int value in fileBlock_6)
            {
                sw.WriteLine(value);
            }
            sw.WriteLine("Sector 7:");
            foreach (int value in fileBlock)
            {
                sw.WriteLine(value);
            }
            if (isRestore)
                await UploadToCloud(sw);

            sw.Dispose();
        }

        public static async Task UploadToCloud(StreamWriter sw)
        {
            string filePath = XPFileUtilities.GetFilePath(sw);
            if (await App.ParseManagerAdapter.UploadVEEPROM(filePath))
            {
                System.Diagnostics.Debug.WriteLine("EEPROM has been uploaded successfully.");
                //DeleteFile(filePath); Hollies last fix since learoy said some files don't get there, we should implement stuff like the vehicle test json and csv uploads
                System.Diagnostics.Debug.WriteLine("local EEPROM file has been deleted.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("EEPROM falied to upload.");
                bool isBtnEmailSelected = await App._MainFlyoutPage.userTabbedPageDisplayed.DisplayAlert("", "Press send email and we will add this error data as an attachment for customer support purposes", "Send Email", "Cancel");
                if (isBtnEmailSelected)
                {
                    await SendEmailWithOptionalAttachment("", filePath);
                    //Because the line above did not actually await, so do not delete this file.
                }
            }
        }
        public static void ParseJSONFromStreamReader(string filePath, Dictionary<string, object> subObjects)
        {
            StreamReader streamToRead = new StreamReader(GetExternalFileStream(filePath));
            string line;

            while ((line = streamToRead.ReadLine()) != null)
            {
                //line format is:
                //first object
                //{ "Chassis1": {"THROTTLEMIN":1.16992188,"THROTTLEMAX":4.459961,"THROTTLEFULL":7.79980469,"FWDLMTRPM":5806,"AvgPowerMinW":300,"AvgPowerMaxW":500,"ThrottleMinV":1,"ThrottleMaxV":4} }
                //file may have multiple lines
                //assume someone has not messed with the multiline file format
                var tempSubObjects = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(line).Values.First();
                foreach (var obj in tempSubObjects)
                {
                    if (!subObjects.ContainsKey(obj.Key))
                        subObjects.Add(obj.Key, obj.Value);
                    else
                        subObjects[obj.Key] = obj.Value;
                }
            }
            streamToRead?.Dispose();
        }

        public static void WriteJsonFile(string filePath, Dictionary<string, object> keyValuePairs)
        {
            StreamWriter streamToWrite = new StreamWriter(CreateStream(filePath));
            streamToWrite.Write(keyValuePairs);
            streamToWrite.Dispose();
        }
        public static void DeleteFile(string filePath)
        {
            XPFileUtilities.DeleteFile(filePath);
        }
        public static long GetFileSize(string filePath)
        {
            return XPFileUtilities.GetFileSize(filePath);
        }
        public static async Task<DateTime> GetUrlDateAndTime(string fileNameAndPath)
        {
            return await XPFileUtilities.GetUrlDateAndTime(fileNameAndPath);
        }
        public static Stream CreateStream(string fileNameAndPath)
        {
            return XPFileUtilities.CreateStream(fileNameAndPath);
        }
        public static void CreatePublicDirectory(string directoryName)
        {
            XPFileUtilities.CreatePublicDirectory("Navitas");
        }

        public static DateTime GetAssemblyLastModifiedTimeUTC()
        {
            return XPFileUtilities.GetAssemblyLastModifiedTimeUTC();
        }

        private static SemaphoreSlim CloudUploadSemaphore = new SemaphoreSlim(1, 1);
        public static async Task CloudUpload(string databaseName)
        {
            await CloudUploadSemaphore.WaitAsync();

            await App.ParseManagerAdapterTest.InitializeTest();
            var ext = ".json";
            var JSONList = FetchExternalFilesByExtension(ext);
            if (JSONList.Count > 0) await ExtractFileByExtension(JSONList, ext);

            ext = ".csv";
            var CSVList = FetchExternalFilesByExtension(ext);
            if (CSVList.Count > 0) await ExtractFileByExtension(CSVList, ext);

            App.ParseManagerAdapter.Initialize();
            IDictionary<string, string> strUserCredentials = Authentication.GetUserCredentials();
            string loginResult = await App.ParseManagerAdapter.Login(strUserCredentials["UserName"], strUserCredentials["Password"]);
            System.Diagnostics.Debug.WriteLine("CloudUpload login result is " + loginResult + " at " + DateTime.Now);

            CloudUploadSemaphore.Release();
        }

        public static List<string> FetchExternalFilesByExtension(string ext)
        {
            return GetNavitasDirectoryFiles().Where(filePath => filePath.Contains(ext)).ToList();
        }

        public static async Task ExtractFileByExtension(List<string> fileList, string ext)
        {
            foreach (var filePath in fileList)
            {//Store individual file objects for file containing multiple lines:
                var (controllerName, controllerTestID) = GetControllerNameAndTestID(filePath, ext);
                string tempFilePath = System.IO.Path.GetFileName(filePath);
                string databaseClass = tempFilePath.Substring(0, tempFilePath.IndexOf("_"));
                if (ext == ".json")
                {
                    Dictionary<string, object> subObjects = new Dictionary<string, object>();
                    //add controller and date because multiple files may be results from multiple tests on ths controller
                    subObjects.Add("MotorController", controllerName);
                    subObjects.Add("TestID", controllerTestID);

                    await SendJSONToCloud(databaseClass, subObjects, filePath);
                }
                else if (ext == ".csv")
                {
                    await SendCSVFileToCloud(databaseClass, controllerTestID, filePath);
                }
            }
        }

        private static (string JSONFileCounter, string CSVFileCounter) GetControllerNameAndTestID(string filePath, string ext)
        {
            string controllerName = filePath.Substring(filePath.IndexOf("TAC"), "TAC_0000000000".Length);
            string controllerTestID = filePath.Substring(filePath.IndexOf(controllerName) + controllerName.Length).Replace(ext, "");

            return (controllerName, controllerTestID);
        }

        private static async Task SendJSONToCloud(string databaseClass, Dictionary<string, object> subObjects, string filePath)
        {
            ParseJSONFromStreamReader(filePath, subObjects);

            if (subObjects != null)
            {
                bool databaseUpdated = await App.ParseManagerAdapterTest.UpdateClass(databaseClass, subObjects);
                if (databaseUpdated) DeleteFile(filePath);
            }
        }

        private static async Task SendCSVFileToCloud(string databaseClass, string controllerTestID, string filePath)
        {
            var fileName = $"{Regex.Match(filePath, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value}";

            if (GetFileSize(filePath) > 15000000)
            {//parse limit is around 2Mb
                await App.ParseManagerAdapterTest.UploadFileToObjectStorage("FileIsGreaterThan15Mb", databaseClass, "TestID", controllerTestID); //log it in the database
                DeleteFile(filePath);
            }
            else
            {
                await App.ParseManagerAdapterTest.UploadFileToObjectStorage(filePath, databaseClass, "TestID", controllerTestID);
                await FileUploadHandler(filePath, App.ParseManagerAdapterTest.GetDatalogUniqueId(), fileName);
            }
        }

        public static async Task FileUploadHandler(string filePath, string uniqueId, string fileName)
        {
            int checkDateTimeCounter = 0;
            bool isFileTransmissionSuccess = false;
            if (uniqueId != "" && uniqueId != null)
            {
                var stringTemplate = $"{App.ParseManagerAdapterTest.GetServer()}files/{App.ParseManagerAdapterTest.GetApplicationId()}/{uniqueId}_{fileName}";
                while (!isFileTransmissionSuccess && checkDateTimeCounter < 5)
                {
                    DateTime getUrlStartTime = DateTime.Now;
                    //System.Diagnostics.Debug.WriteLine($"Time get back reponse: {(DateTime.Now - getUrlStartTime).TotalSeconds} - {checkDateTimeCounter} attempt(s)");
                    DateTime dateTimeFileLastModified = await GetUrlDateAndTime(stringTemplate);


                    if (DateTime.Now - getUrlStartTime >= TimeSpan.FromSeconds(10))
                    {
                        //We found out the issue early so break the loop before more unhelpful attempts
                        //System.Diagnostics.Debug.WriteLine("Break the attempt of sending uploads loop");
                        break;
                    }

                    if (dateTimeFileLastModified == DateTime.MinValue || checkDateTimeCounter > 0)
                    {
                        //activityMessage.Text = "Retrying";
                        System.Diagnostics.Debug.WriteLine($"Retry uploading {checkDateTimeCounter} attempt(s)");
                    }

                    if (dateTimeFileLastModified > DateTime.MinValue)
                    {
                        //System.Diagnostics.Debug.WriteLine("File upload updated at: " + dateTimeFileExist);
                        System.Diagnostics.Debug.WriteLine("Transmit Complete");
                        isFileTransmissionSuccess = true;
                        //Delete Datalog back up file
                        DeleteFile(filePath);
                    }
                    checkDateTimeCounter++;
                }

                //if (!isFileTransmissionSuccess)
                //{
                //    System.Diagnostics.Debug.WriteLine($"Transmit Failed after {checkDateTimeCounter} attempt(s)");
                //}
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("The server encountered an internal error and was unable to send your datalog");
                // File Uploading must be failed due to an empty unique Id retrieved
            }

            //System.Diagnostics.Debug.WriteLine("FileUploadHandler --End");
        }
        public static async Task UploadRemainingCSVFiles()
        {
            System.Diagnostics.Debug.WriteLine($"UploadRemainingFiles");

            // Finding file with these extension .json .csv, if they exist
            var (JSONFileCounter, CSVFileCounter) = SearchForJsonAndCSVExtensions();

            if (JSONFileCounter + CSVFileCounter > 0)
            {
                CloudUpload("Chassis1");
                ConnectivityService.CheckInternetStabilityAndCleanUpRemaningFileUpload();
            }
        }

    }
}
