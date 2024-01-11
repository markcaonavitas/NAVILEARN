using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class WriteBlockEventArgs
    {
        /// <summary>
        /// byte to write
        /// </summary>
        /// <value>ParameterNumber</value>
        public List<byte> _list { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEventArgs"/> class.
        /// </summary>
        /// <param name="bsendByte">Parameter Number to set</param>

        /// 
        public WriteBlockEventArgs(List<byte> list)
        {
            _list = list;

        }
    }
}
