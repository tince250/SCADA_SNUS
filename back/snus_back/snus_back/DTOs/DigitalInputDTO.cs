using snus_back.Models;

namespace snus_back.DTOs
{
    public class DigitalInputDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }

        public DigitalInputDTO(DigitalInput digitalInput)
        {
            Id = digitalInput.Id;
            Description = digitalInput.Description;
            IOAddress = digitalInput.IOAddress;
            Value = digitalInput.Value;
        }
    }
}
