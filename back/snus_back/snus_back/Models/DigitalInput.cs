namespace snus_back.Models
{
    public class DigitalInput : Tag
    {
        public int ScanTime { get; set; }
        public bool IsScanOn { get; set; }
        public int Value { get; set; }
    }
}
