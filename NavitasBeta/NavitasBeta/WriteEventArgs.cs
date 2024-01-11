using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class WriteEventArgs
    {
        /// <summary>
        /// byte to write
        /// </summary>
        /// <value>ParameterNumber</value>
        public byte bSendByte { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEventArgs"/> class.
        /// </summary>
        /// <param name="bsendByte">Parameter Number to set</param>

        /// 
        public WriteEventArgs(byte bsendByte)
        {
            bSendByte = bsendByte;

        }
    }

    public class Write2ByteEventArgs
    {
        /// <summary>
        /// byte to write
        /// </summary>
        /// <value>ParameterNumber</value>
        public byte[] bSendBytes { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="WriteEventArgs"/> class.
        /// </summary>
        /// <param name="bsendByte">Parameter Number to set</param>

        /// 
        public Write2ByteEventArgs(byte[] bsendBytes)
        {
            bSendBytes = bsendBytes;
#if OLD_CODE
            bSendBytes = new byte[2];
            bSendBytes[0] = bsendBytes[0];
            bSendBytes[1] = bsendBytes[1];
#endif
        }
    }

}
