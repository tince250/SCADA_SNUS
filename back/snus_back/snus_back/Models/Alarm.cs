using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public enum AlarmPriority{
        LOW,
        MEDIUM,
        HIGH
    }
    public class Alarm
    {
        public string TagName { get; set; }

        public double Value { get; set; }

        public AlarmPriority Priority { get; set; }

    }
}
