using snus_back.Models;

namespace snus_back.DTOs
{
    public class TagRecordDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }

        public TagRecordDTO(TagRecord record)
        {
            Id = record.Id;
            Timestamp = record.Timestamp;
            Value = record.Value;
            Description = record.Tag.Description;
            IOAddress = record.Tag.IOAddress; 
        }
    }
}
