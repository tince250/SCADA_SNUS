using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }
    }
}
