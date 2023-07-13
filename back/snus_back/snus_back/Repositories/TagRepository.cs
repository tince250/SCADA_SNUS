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
            var  tagRecrods = dbContext.TagRecords
                .Where(tr => tr.Tag.IOAddress == address)
                .ToList();

            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in dbContext.TagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }
    }
}
