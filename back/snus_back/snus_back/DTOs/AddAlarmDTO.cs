using snus_back.Models;

namespace snus_back.DTOs
{
    public class AddAlarmDTO
    {
        public int TagId { get; set; }
        public double Value { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }

    }
}
