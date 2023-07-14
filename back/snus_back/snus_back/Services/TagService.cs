using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class TagService : ITagService
    {
        private TagRepository allTags;

        public TagService(TagRepository allTags) 
        {
            this.allTags = allTags;
        }

        public ICollection<TagRecordDTO> getAllTagByIOAddress(string address)
        {
            return allTags.getAllTagByIOAddress(address);
        }

        public ICollection<TagRecordDTO> getAllTagRecords()
        {
            return allTags.getAllTagRecords();
        }

        public void DeleteDigitalOutput(int id)
        {
            allTags.DeleteDigitalOutput(id);
        }

        public void DeleteAnalogOutput(int id)
        {
            allTags.DeleteAnalogOutput(id);
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
            //this.allTags.AddDigitalOutput();
        }

        private void addDigitalInputTag(AddTagDTO dto)
        {
            //this.allTags.AddDigitalInput();

        }

        private void addAnalogOutputTag(AddTagDTO dto)
        {
            //this.allTags.AddAnalogOutput();

        }

        private void addAnalogInputTag(AddTagDTO dto)
        {
            //this.allTags.AddAnalogInput();

        }
    }
}
