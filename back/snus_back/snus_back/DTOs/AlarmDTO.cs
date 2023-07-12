using snus_back.Models;

namespace snus_back.DTOs
{
    public class AlarmDTO
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string IOAddress { get; set; }
        public AlarmPriority Priority { get; set; }
        public DateTime Timestamp { get; set; }

        public AlarmDTO(AlarmRecord record)
        {
            Id = record.Id;
            Value = record.Alarm.Value;
            IOAddress = record.Tag.IOAddress;
            Priority = record.Alarm.Priority;
            Timestamp = record.Timestamp;
        }

    }
}
