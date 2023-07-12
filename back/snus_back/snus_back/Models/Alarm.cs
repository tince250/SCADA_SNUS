using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public enum AlarmPriority{
        LOW,
        MEDIUM,
        HIGH
    }

    public enum AlarmType
    {
        LOWER,
        HIGHER
    }

    public class Alarm
    {
        [Key]
        public int Id { get; set; }

        public double Value { get; set; }

        public AlarmType Type { get; set; }

        public AlarmPriority Priority { get; set; }

    }
}
