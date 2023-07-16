using snus_back.Models;

namespace snus_back.DTOs
{
    public class AlarmRecordDTO
    {
        public int TagId { get; set; }
        public double Value { get; set; }
        public AlarmPriority Priority { get; set; }
        public AlarmType Type { get; set; }
    }
}
