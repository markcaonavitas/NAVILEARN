using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class FlashGetBlockResponseEventArgs
    {

        public List<byte> listOfBytesFromResponse;

        public FlashGetBlockResponseEventArgs(List<byte> listOfBytes)
        {
            listOfBytesFromResponse = listOfBytes;

        }
    }
}
