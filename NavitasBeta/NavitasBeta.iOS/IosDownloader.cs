using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using NavitasBeta.iOS;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.RegularExpressions;

[assembly: Dependency(typeof(IosDownloader))]
namespace NavitasBeta.iOS
{
    public class IosDownloader : IFileUtilities
    {
        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

        public void CreatePublicDirectory(string folder)
        {
            string pathToNewFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //CheckAppPermissions();
            Directory.CreateDirectory(pathToNewFolder);
        }

        async public Task DownloadFile(string url, string folder)
        {
            string pathToNewFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName = Regex.Match(url, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value;
            //CheckAppPermissions();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += (sender, e) =>
                {
                    //var contentLength = long.Parse((sender as WebClient).ResponseHeaders["Content-Length"]);
                    OnFileDownloaded?.Invoke(this, new DownloadEventArgs((e.Error == null), fileName));
                };
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                await webClient.DownloadFileTaskAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                // We do not need to invoke this twice when there is an error occured !!
                //OnFileDownloaded?.Invoke(this, new DownloadEventArgs(false, fileName));
                System.Diagnostics.Debug.WriteLine("Debug this exception: IosDownloader.xaml.cs" + ex.Message);
            }
        }

        async public Task DownloadFile(string url, string objectType, string folder)
        {
            string pathToNewFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName = Regex.Match(url, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value;
            //CheckAppPermissions();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += (sender, e) =>
                {
                    //var contentLength = long.Parse((sender as WebClient).ResponseHeaders["Content-Length"]);
                    OnFileDownloaded?.Invoke(this, new DownloadEventArgs((e.Error == null), fileName, objectType));
                };
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                await webClient.DownloadFileTaskAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                // We do not need to invoke this twice when there is an error occured !!
                //OnFileDownloaded?.Invoke(this, new DownloadEventArgs(false, fileName));
                System.Diagnostics.Debug.WriteLine("Debug this exception: IosDownloader.xaml.cs" + ex.Message);
            }
        }

        public string GetDirectoryPath(string folder)
        {

            //CheckAppPermissions();
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        }
        public System.IO.Stream CreateStream(string fileNameAndPath)
        {

            //CheckAppPermissions();
            FileStream file = new FileStream(fileNameAndPath, FileMode.Create);
            return file;

        }

        public string[] DirectoryGetFiles(string folder)
        {
            string pathToNewFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //CheckAppPermissions();
            string[] fileList = null;
            try
            {
                fileList = Directory.GetFiles(pathToNewFolder);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(pathToNewFolder.ToString() + " Failed" + e.ToString());
            }
            return fileList;
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }

        public System.IO.Stream GetFileStream(string fileNameAndPath)
        {
            System.IO.Stream file;
            //CheckAppPermissions();
            try
            {
                file = File.OpenRead(fileNameAndPath);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(fileNameAndPath.ToString() + " Failed" + e.ToString());
                file = null;
            }
            return file;
        }

        public StreamWriter AppendFile(string fileNameAndPath)
        {
            StreamWriter file;
            //CheckAppPermissions();
            try
            {
                file = File.AppendText(fileNameAndPath);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(fileNameAndPath.ToString() + " Failed" + e.ToString());
                file = null;
            }
            return file;
        }

        public void CloseAppendFile(StreamWriter file)
        {
            //CheckAppPermissions();
            try
            {
                file.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("File failed to close" + e.ToString());
            }
        }

        public DateTime GetFileDateAndTime(string fileNameAndPath)
        {
            DateTime lastModified;
            //CheckAppPermissions();
            try
            {
                lastModified = System.IO.File.GetLastWriteTime(fileNameAndPath);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(fileNameAndPath.ToString() + " Failed" + e.ToString());
                lastModified = DateTime.Now; //not the best way to indicate there is a failure
            }
            return lastModified;
        }

        public DateTime GetAssemblyLastModifiedTimeUTC()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                UriBuilder uri = new UriBuilder(assembly.CodeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                FileInfo fileInfo = new FileInfo(path);
                return fileInfo.LastWriteTimeUtc;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(" Failed" + e.ToString());
                return DateTime.MinValue; //not the best way to indicate there is a failure
            }
        }
        public long GetFileSize(string fileNameAndPath)
        {
            try
            {
                return new FileInfo(fileNameAndPath).Length;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(fileNameAndPath.ToString() + " Failed" + e.ToString());
                return 0; //not the best way to indicate there is a failure
            }
        }

        async public Task<DateTime> GetUrlDateAndTime(string url)
        {
            try
            {
                var myHttpWebRequest = WebRequest.Create(url);
                myHttpWebRequest.Timeout = 10000;
                WebResponse myHttpWebResponse = await myHttpWebRequest.GetResponseAsync();
                DateTime fileDateAndTime = (myHttpWebResponse as HttpWebResponse).LastModified;
                myHttpWebResponse.Close();
                return fileDateAndTime;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(url.ToString() + " Failed" + e.ToString());
                return DateTime.MinValue; //not the best way to indicate there is a failure
            }
        }

        async public Task<(long contentLength, DateTime lastModified)> GetResponseInfoFromHeader(string url)
        {
            try
            {
                var myHttpWebRequest = WebRequest.Create(url);
                myHttpWebRequest.Timeout = 10000;
                WebResponse myHttpWebResponse = await myHttpWebRequest.GetResponseAsync();
                var contentLength = (myHttpWebResponse as HttpWebResponse).ContentLength;
                var lastModified = (myHttpWebResponse as HttpWebResponse).LastModified;
                myHttpWebResponse.Close();
                return (contentLength, lastModified);
            }
            catch (WebException webEx)
            {
                System.Diagnostics.Debug.WriteLine($"GetResponseInfoFromHeader WebException: {webEx.Status} - {webEx.Message}");
                return (0, DateTime.MinValue);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(url.ToString() + " Failed" + e.ToString());
                return (0, DateTime.MinValue); //not the best way to indicate there is a failure
            }
        }

        public void DownloadData(string url, string folder)
        {
            string pathToNewFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string fileName = Regex.Match(url, @"([^\\\/]+$)", RegexOptions.IgnoreCase).Value;
            //CheckAppPermissions();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                WebClient webClient = new WebClient();
                webClient.DownloadDataCompleted += (sender, e) =>
                {
                    //var contentLength = long.Parse((sender as WebClient).ResponseHeaders["Content-Length"]);
                    OnFileDownloaded?.Invoke(this, new DownloadEventArgs((e.Error == null), fileName));
                };
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                webClient.DownloadDataAsync(new Uri("http://www.navitasvs.com/_functions/apiForTokenForLogIn?userName=ransom@navitasvehiclesystems.com&password=rockii11"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: AndroidDownloader.xaml.cs" + ex.Message);
            }
        }
        public void CopyFile(string sourceFileName, string destFileName)
        {
            try
            {
                File.Copy(sourceFileName, destFileName, true);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Copy Failed" + e.ToString());
            }
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void MoveFile(string sourceFileName, string destFileName)
        {
            try
            {
                File.Move(sourceFileName, destFileName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Copy Failed" + e.ToString());
            }
        }
        public string GetFilePath(StreamWriter sw)
        {
            return ((FileStream)(sw.BaseStream)).Name;
        }
    }
}