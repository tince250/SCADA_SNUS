using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Repositories
{
    public class TagRepository
    {
        private SNUSDbContext dbContext;

        public TagRepository(SNUSDbContext context)
        {
            this.dbContext = context;
        }

        public ICollection<TagRecordDTO> getAllTagRecords()
        {
            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in dbContext.TagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }

        public ICollection<TagRecordDTO> getAllTagByIOAddress(string address)
        {
            var  tagRecords = dbContext.TagRecords
                .Where(tr => tr.Tag.IOAddress == address)
                .ToList();

            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in tagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }

        public ICollection<AnalogInputDTO> getAllAITags()
        {
            ICollection<AnalogInputDTO> ret = new List<AnalogInputDTO>();
            foreach (var analogInput in dbContext.AnalogInputs)
            {
                ret.Add(new AnalogInputDTO(analogInput));
            }

            return ret;
        }

        public ICollection<DigitalInputDTO> getAllDITags()
        {
            ICollection<DigitalInputDTO> ret = new List<DigitalInputDTO>();
            foreach (var digitalInput in dbContext.DigitalInputs)
            {
                ret.Add(new DigitalInputDTO(digitalInput));
            }

            return ret;
        }
    }
}
