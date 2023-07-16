using snus_back.Models;

namespace snus_back.DTOs
{
    public class AlarmReturnedDTO
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public double Value { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
    }
}
