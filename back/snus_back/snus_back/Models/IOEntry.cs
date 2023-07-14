using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public class IOEntry
    {
        [Key]
        public int Id { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }

        public IOEntry()
        {
        }

        public IOEntry(string IOaddress, double value)
        {
            IOAddress = IOaddress;
            Value = value;
        }
    }
}
