using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NavitasBeta
{
    public class ParameterFile
    {
        public string FileName { get; set; }
        public string FriendlyFileName { get; set; }
        public ObservableCollection<ParameterFileItem> ParameterFileItemList { get; set; }
    }
}
