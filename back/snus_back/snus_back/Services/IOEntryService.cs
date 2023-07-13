using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class IOEntryService : IIOEntryService
    {
        private IOEntryRepository allIOEntries;

        public IOEntryService(IOEntryRepository entryRepository)
        {
            this.allIOEntries = entryRepository;
        }

        public void BatchUpdate(List<IOEntryDTO> dtos)
        {
            this.allIOEntries.BatchUpdate(dtos);
        }
    }
}
