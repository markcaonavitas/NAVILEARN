using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    /// <summary>
    /// Packet verified event arguments.
    /// </summary>
    public class PacketReceivedEventArgs : EventArgs
    {

        /// <summary>
        /// Gets or sets the ReceivedPacket
        /// </summary>
        /// <value>byte[]</value>
        public byte[] ReceivedPacket { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="receivedpacket">The validated received packet</param>
        public PacketReceivedEventArgs(byte[] receivedpacket)
        {
            ReceivedPacket = receivedpacket;
        }
    }
}
