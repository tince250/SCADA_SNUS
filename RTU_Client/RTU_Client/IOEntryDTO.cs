using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTU_Client
{
    public class IOEntryDTO
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }

        public IOEntryDTO(IOEntry entry)
        {
            IOAddress = entry.IOAddress;
            Value = entry.Value;
        }
    }
}
