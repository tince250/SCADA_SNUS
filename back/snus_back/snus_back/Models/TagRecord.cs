using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public class TagRecord
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
