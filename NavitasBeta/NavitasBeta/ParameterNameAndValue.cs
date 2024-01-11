using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    class ParameterNameAndValue
    {
        public string PropertyName;
        public float parameterValue;
    }

    class TimeAndValuePair //invented to possibly keep similar formatting between scope and datalogging
    {
        public string time;
        public float value;
    }
    class ParameterNameAndValueList
    {
        public string PropertyName;
        public List<TimeAndValuePair> parameterTimeAndValuePairList;
    }
}
