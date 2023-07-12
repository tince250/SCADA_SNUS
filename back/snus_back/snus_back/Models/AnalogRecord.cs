namespace snus_back.Models
{
    public class AnalogRecord
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }

        public AnalogRecord(string address, double value)
        {
            IOAddress = address;
            Value = value;
        }
    }
}
