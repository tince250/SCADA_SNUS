using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace snus_back.Models
{
    public class AlarmRecord
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int AlarmId { get; set; }
        public virtual Alarm Alarm { get; set; }
    }
}
