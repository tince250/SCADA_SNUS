using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Services.ServiceInterfaces
{
    public interface ITagService
    {
        public ICollection<TagRecordDTO> getAllTagRecords();

        public ICollection<Tag> getAllTagByIOAddress(string address);


    }
}
