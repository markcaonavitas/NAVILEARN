using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class ScopeGetBlockResponseEventArgs
    {

        public List<byte> listOfBytesFromResponse;

        /// 
        public ScopeGetBlockResponseEventArgs(List<byte> listOfBytes)
        {
            listOfBytesFromResponse = listOfBytes;

        }
    }
}
