using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Repositories
{
    public class IOEntryRepository
    {
        private const int ENTRIES_NUMBER = 20;
        Dictionary<string, double> entries = new Dictionary<string, double>();
        private SNUSDbContext dbContext;

        public IOEntryRepository(SNUSDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.InitDictionary();
        }

        private void InitDictionary()
        {
            var entities = this.dbContext.IOEntries.ToList();

            if (entities.Count == 0)
            {
                for (int i = 0; i < ENTRIES_NUMBER; i++)
                {
                    this.entries.Add(i.ToString(), -1);
                }
                this.BatchUpdateDb();
            }
            else
            {
                for (int i = 0; i < ENTRIES_NUMBER; i++)
                {
                    this.entries.Add(entities[i].IOAddress, entities[i].Value);
                }
            }
        }

        public void BatchUpdate(List<IOEntryDTO> newEntries)
        {
            for (int i = 0; i < this.entries.Count; i++)
            {
                this.entries[newEntries[i].IOAddress] = newEntries[i].Value;
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
                    dbContext.IOEntries.Add(new IOEntry(this.entries.Keys.ElementAt(i), this.entries.Values.ElementAt(i)));
                }
            }
            else
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Value = this.entries[entities[i].IOAddress];
                }
            }
            
            dbContext.SaveChanges();
        }
    }
}
