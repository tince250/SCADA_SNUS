using snus_back.Models;

namespace snus_back.DTOs
{
    public class AnalogInputDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }

        public AnalogInputDTO(AnalogInput analogInput)
        {
            Id = analogInput.Id;
            Description = analogInput.Description;
            IOAddress = analogInput.IOAddress;
            Value = analogInput.Value;
            LowLimit = analogInput.LowLimit;
            HighLimit = analogInput.HighLimit;
            Unit = analogInput.Unit;
        }
    }
}
