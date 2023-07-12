namespace snus_back.Models
{
    public class AnalogOutput: Tag
    {
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }
}
