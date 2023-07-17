using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTU_Client
{
    public enum IOEntryType
    {
        ANALOG,
        DIGITAL
    }

    public class IOEntry
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public IOEntryType Type { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }

    }
}
