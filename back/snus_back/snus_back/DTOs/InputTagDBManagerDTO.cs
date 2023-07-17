using snus_back.Models;

namespace snus_back.DTOs
{
    public class InputTagDBManagerDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public OutputType Type { get; set; }
        public string Unit { get; set; }
        public bool IsScanOn { get; set; }
        public int ScanTime { get; set; }

        public enum OutputType
        {
            DIGITAL, ANALOG
        }

        public InputTagDBManagerDTO(Tag tag)
        {
            Id = tag.Id;
            Description = tag.Description;
            Value = tag.Value;
            if (tag is AnalogInput)
            {
                Type = OutputType.ANALOG;
                Unit = ((AnalogInput)tag).Unit;
                IsScanOn = ((AnalogInput)tag).IsScanOn;
                ScanTime = ((AnalogInput)tag).ScanTime;
            }
            else
            {
                Type = OutputType.DIGITAL;
                IsScanOn = ((DigitalInput)tag).IsScanOn;
                ScanTime = ((DigitalInput)tag).ScanTime;
            }
        }
    }
}
