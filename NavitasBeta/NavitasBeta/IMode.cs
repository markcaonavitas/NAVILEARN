using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public interface IMode
    {
        

        void SetPacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate);

        void RemovePacketResponseDelegate(System.EventHandler<NavitasBeta.PacketReceivedEventArgs> PacketResponseDelegate);

        void WritePacket(byte[] packetfull);

        void SetParameter(SetParameterEventArgs e);

        void GetBlock(int bParameterNumber, float fBlock);

        void InitBootLoaderMode(byte BootLoaderBit);

        void fletcher16(byte[] checksum, byte[] data, int len);

        void Close();
    }
}
    