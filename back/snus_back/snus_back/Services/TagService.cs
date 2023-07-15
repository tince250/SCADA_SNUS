using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class TagService : ITagService
    {
        private TagRepository allTags;
        private ScanService scanService;

        public TagService(TagRepository allTags, ScanService scanService) 
        {
            this.allTags = allTags;
            this.scanService = scanService;
        }

        public ICollection<TagRecordDTO> getAllTagByIOAddress(string address)
        {
            return allTags.getAllTagByIOAddress(address);
        }

        public ICollection<TagRecordDTO> getAllTagRecords()
        {
            return allTags.getAllTagRecords();
        }

        public void UpdateAnalogOutputValue(int id, double value)
        {
            allTags.UpdateAnalogOutputValue(id, value);
        }

        public void UpdateDigitalOutputValue(int id, double value)
        {
            allTags.UpdateDigitalOutputValue(id, value);
        }

        public void DeleteDigitalOutput(int id)
        {
            allTags.DeleteDigitalOutput(id);
        }

        public void DeleteAnalogOutput(int id)
        {
            allTags.DeleteAnalogOutput(id);
        }

        public ICollection<InputTagDBManagerDTO> GetAllInputTagsDBManager()
        {
            return allTags.GetAllInputTagsDBManager();
        }

        public ICollection<OutputTagDBManagerDTO> GetAllOutputTagsDBManager()
        {
            return allTags.GetAllOutputTagsDBManager();
        }

        public void AddTag(AddTagDTO dto)
        {
            switch (dto.Type)
            {
                case "Analog Input":
                    this.addAnalogInputTag(dto);
                    break;
                case "Analog Output":
                    this.addAnalogOutputTag(dto);
                    break;
                case "Digital Input":
                    this.addDigitalInputTag(dto);
                    break;
                case "Digital Output":
                    this.addDigitalOutputTag(dto);
                    break;
                default:
                    throw new Exception("Tag type provided is not supported.");
            }
        }

        private void addDigitalOutputTag(AddTagDTO dto)
        {
            DigitalOutput newTag = new DigitalOutput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Value = (double)dto.InitialValue,
                Name = dto.Name
            };
            this.allTags.AddDigitalOutput(newTag);
        }

        private void addDigitalInputTag(AddTagDTO dto)
        {
            DigitalInput newTag = new DigitalInput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Name = dto.Name,
                Value = -1,
                ScanTime = (int)dto.ScanTime,
                IsScanOn = (bool)dto.IsScanOn
            };
            DigitalInput addedTag = this.allTags.AddDigitalInput(newTag);
            Console.WriteLine(addedTag.Id);
            this.scanService.AddNewTagThread(addedTag);
        }

        private void addAnalogOutputTag(AddTagDTO dto)
        {
            AnalogOutput newTag = new AnalogOutput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Name = dto.Name,
                Value = (double)dto.InitialValue,
                HighLimit = (double)dto.HighLimit,
                LowLimit = (double)dto.LowLimit,
                Unit = dto.Unit

            };
             this.allTags.AddAnalogOutput(newTag);
        }

        private void addAnalogInputTag(AddTagDTO dto)
        {
            AnalogInput newTag = new AnalogInput
            {
                IOAddress = dto.IOAddress,
                Description = dto.Description,
                Name = dto.Name,
                Value = -1,
                HighLimit = (double)dto.HighLimit,
                LowLimit = (double)dto.LowLimit,
                Unit = dto.Unit,
                ScanTime = (int)dto.ScanTime,
                IsScanOn = (bool)dto.IsScanOn

            };
            AnalogInput addedTag = this.allTags.AddAnalogInput(newTag);
            Console.WriteLine(addedTag.Id);
            this.scanService.AddNewTagThread(addedTag);

        }

        public void UpdateTagScan(UpdateTagScanDTO dto)
        {
            if (dto.Type == "DIGITAL")
            {
                DigitalInput tag = this.allTags.UpdateDigitalInputScan(dto.Id, dto.IsScanOn);
                this.scanService.UpdateScan(tag);
            } 
            else if (dto.Type == "ANALOG")
            {
                AnalogInput tag = this.allTags.UpdateAnalogInputScan(dto.Id, dto.IsScanOn);
                this.scanService.UpdateScan(tag);
            } 
        }
    }
}
