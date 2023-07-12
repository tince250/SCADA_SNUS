namespace snus_back.Models
{
    public class AnalogInput : Tag
    {
        public int ScanTime { get; set; }
        public bool IsScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public double Value { get; set; }
        public string Units { get; set; }
        public ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();

    }
}
