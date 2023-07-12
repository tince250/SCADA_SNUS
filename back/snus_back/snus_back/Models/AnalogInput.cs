namespace snus_back.Models
{
    public class AnalogInput : Tag
    {
        public int ScanTime { get; set; }
        public bool IsScanOn { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<Alarm> Alarms { get; set; } = new List<Alarm>();

    }
}
