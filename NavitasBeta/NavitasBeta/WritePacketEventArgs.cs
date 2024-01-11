using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class WritePacketEventArgs
    {

        /// <summary>
        /// byte to write
        /// </summary>
        /// <value>ParameterNumber</value>
        public byte[] _packetfull { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="WritePacketEventArgs"/> class.
        /// </summary>
        /// <param name="packetfull">Packet to send </param>

        /// 
        public WritePacketEventArgs(byte[] packetfull)
        {
            _packetfull = packetfull;

        }
    }
}
