using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public class AlarmRecord
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int AlarmId { get; set; }
        public virtual Alarm Alarm { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
