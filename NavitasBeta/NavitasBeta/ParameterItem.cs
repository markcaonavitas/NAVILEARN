using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class ParameterItem
    {
        public enum ViewTypes : int { Float, Enum, String};

        public ViewTypes ViewType;
        public string PropertyName;
    }
}
