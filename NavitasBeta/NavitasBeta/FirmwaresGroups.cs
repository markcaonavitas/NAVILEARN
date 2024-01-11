using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class FirmwaresGroups
    {
        public List<FirmwareGroup> FirmwareGroups = new List<FirmwareGroup>();
    }

    public class FirmwareGroup
    {
        public string Title;

        public string StorageType;

        public string SubDirectory;

        public List<FileItem> FileItems = new List<FileItem>();
    }

}
