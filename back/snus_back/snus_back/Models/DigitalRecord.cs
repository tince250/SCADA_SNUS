namespace snus_back.Models
{
    public class DigitalRecord
    {
        public string IOAddress { get; set; }
        public int Value { get; set; }

        public DigitalRecord(string address, int value)
        {
            IOAddress = address;
            Value = value;
        }
    }
}
