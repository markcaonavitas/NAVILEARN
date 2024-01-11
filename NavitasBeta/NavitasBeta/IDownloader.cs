using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public interface IFileUtilities
    {
        Task DownloadFile(string url, string folder);
        Task DownloadFile(string url, string objectType, string folder);
        void DownloadData(string url, string folder); //http request for login for now
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        string GetDirectoryPath(string folder);
        string[] DirectoryGetFiles(string folder);
        System.IO.Stream GetFileStream(string fileNameAndPath);
        DateTime GetFileDateAndTime(string fileNameAndPath);
        long GetFileSize(string fileNameAndPath);
        Task<DateTime> GetUrlDateAndTime(string fileNameAndPath);
        
        System.IO.Stream CreateStream(string fileNameAndPath);
        void CreatePublicDirectory(string folder);

        void DeleteFile(string fileNameAndPath);

        void CopyFile(string sourceFileName, string destFileName);

        void MoveFile(string sourceFileName, string destFileName);

        bool FileExists(string filePath);

        DateTime GetAssemblyLastModifiedTimeUTC();

        string GetFilePath(System.IO.StreamWriter sw);

        Task<(long contentLength, DateTime lastModified)> GetResponseInfoFromHeader(string url);
        StreamWriter AppendFile(string fileNameAndPath);
        void CloseAppendFile(StreamWriter file);
    }

    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public string FileName = "";
        public string ObjectType = "";
        public DownloadEventArgs(bool fileSaved, string fileName, string objectType = "")
        {
            FileSaved = fileSaved;
            FileName = fileName;
            ObjectType = objectType;
        }
    }
}
