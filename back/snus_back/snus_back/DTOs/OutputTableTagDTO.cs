using snus_back.Models;

namespace snus_back.DTOs
{
    public class OutputTableTagDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public OutputType Type { get; set; }
        public string Unit { get; set; }

        public enum OutputType
        {
            DIGITAL, ANALOG
        }

        public OutputTableTagDTO(Tag tag)
        {
            Id = tag.Id;
            Description = tag.Description;
            Value = tag.Value;
            if (tag is AnalogOutput)
            {
                Type = OutputType.ANALOG;
                Unit = ((AnalogOutput)tag).Unit;
            }
            else
                Type = OutputType.DIGITAL;
            
        }

        public OutputTableTagDTO(int id, string description, double value, OutputType type, string unit)
        {
            Id = id;
            Description = description;
            Value = value;
            Type = type;
            Unit = unit;
        }
    }
}
