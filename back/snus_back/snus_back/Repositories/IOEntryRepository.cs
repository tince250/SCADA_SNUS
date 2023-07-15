using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;
using System.Net;

namespace snus_back.Repositories
{
    public class IOEntryRepository
    {
        private const int ENTRIES_NUMBER = 20;
        Dictionary<string, double> entriesOutput = new Dictionary<string, double>();
        public static Dictionary<string, double> entries = new Dictionary<string, double>();
        private SNUSDbContext dbContext;
        private TagRepository tagRepository;

        public IOEntryRepository(SNUSDbContext dbContext, TagRepository tagRepository)
        {
            this.dbContext = dbContext;
            this.tagRepository = tagRepository;
            this.InitDictionary();
        }

        private void InitDictionary()
        {
            var entities = this.dbContext.IOEntries.ToList();

            if (entities.Count == 0)
            {
                for (int i = 0; i < ENTRIES_NUMBER; i++)
                {
                    this.entriesOutput.Add(i.ToString(), -1);
                    entries.Add(i.ToString(), -1);
                }
                this.BatchUpdateDb();
            }
            else
            {
                for (int i = 0; i < ENTRIES_NUMBER; i++)
                {
                    this.entriesOutput.Add(i.ToString(), -1);
                    if (!entries.ContainsKey(entities[i].IOAddress))
                        entries.Add(entities[i].IOAddress, entities[i].Value);
                }
            }
        }

        public Dictionary<string, double> GetEntries()
        {
            return entries;
        }

        public Dictionary<string, double> GetOutputEntries()
        {
            return this.entriesOutput;
        }

        public void BatchUpdate(List<IOEntryDTO> newEntries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                entries[newEntries[i].IOAddress] = newEntries[i].Value;
            }

            this.BatchUpdateDb();
        }

        public void BatchUpdateDb()
        {
            var entities = dbContext.IOEntries.ToList();
            if (entities.Count == 0)
            {
                for (int i = 0; i < ENTRIES_NUMBER; i++)
                {
                    dbContext.IOEntries.Add(new IOEntry(entries.Keys.ElementAt(i), entries.Values.ElementAt(i)));
                }
            }
            else
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Value = entries[entities[i].IOAddress];
                    /*Tag tag =  dbContext.AnalogInputs.FirstOrDefault(input => input.IOAddress == entities[i].IOAddress);
                    if (tag == null) 
                        tag = dbContext.DigitalInputs.FirstOrDefault(input => input.IOAddress == entities[i].IOAddress);
                    if (tag != null)
                        dbContext.TagRecords.Add(new TagRecord { Tag = tag, TagId = tag.Id, Timestamp = DateTime.Now, Value = entries[entities[i].IOAddress] });*/
                }
            }
            
            dbContext.SaveChanges();
        }

    }
}
