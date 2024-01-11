using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class SetParameterEventArgs
    {

        /// <summary>
        /// Paramater Number to set
        /// </summary>
        /// <value>ParameterNumber</value>
        public int bParameterNumber { get; set; }

        /// <summary>
        /// Paramater Value to set
        /// </summary>
        /// <value>fParameterValue</value>
        public float fParameterValue { get; set; }
        public ushort rawParameterValue { get; set; }

        /// <summary>
        /// Paramater String 
        /// </summary>
        /// <value>strParameter</value>
        public string strParameter { get; set; }

        public bool parameterTypeIsFloat = true;


        /// <summary>
        /// Initializes a new instance of the <see cref="BluetoothLE.Core.Events.SetParameterEventArgs"/> class.
        /// </summary>
        /// <param name="bparameterNumber">Parameter Number to set</param>
        /// <param name="fparameterValue">Parameter Value to set</param>
        public SetParameterEventArgs(int bparameterNumber, float fparameterValue,string strparameter)
        {
            bParameterNumber = bparameterNumber;
            fParameterValue = fparameterValue;
            strParameter = strparameter;
        }
        public SetParameterEventArgs(int bparameterNumber, ushort rawparameterValue, string strparameter)
        {
            parameterTypeIsFloat = false;
            bParameterNumber = bparameterNumber;
            rawParameterValue = rawparameterValue;
            strParameter = strparameter;
        }
    }
}
