using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class IOEntryService : IIOEntryService
    {
        private IOEntryRepository allIOEntries;
        private TagRepository allTags;

        public IOEntryService(IOEntryRepository entryRepository, TagRepository allTags)
        {
            this.allIOEntries = entryRepository;
            this.allTags = allTags;
        }

        public void BatchUpdate(List<IOEntryDTO> dtos)
        {
            this.allIOEntries.BatchUpdate(dtos);
        }

        public List<string> GetFreeAdresses()
        {
            var takenAdresses = this.allTags.GetTakenAdresses();
            return this.allIOEntries.GetEntries().Keys.Where(x => !takenAdresses.Contains(x)).ToList();
        }

        public List<string> GetFreeOutputAdresses()
        {
            var takenAdresses = this.allTags.GetTakenOutputAdresses();
            return this.allIOEntries.GetOutputEntries().Keys.Where(x => !takenAdresses.Contains(x)).ToList();
        }
    }
}
