using snus_back.data_access;
using snus_back.Models;
using System.Net;

namespace snus_back.Repositories
{
    public class TagRepository
    {
        private SNUSDbContext dbContext;

        public TagRepository(SNUSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<AnalogInput> getAllAnalogInputs()
        {
            return dbContext.AnalogInputs.ToList();
        }

        public ICollection<DigitalInput> getAllDigitalInputs()
        {
            return dbContext.DigitalInputs.ToList();
        }

        public void UpdateAnalogInputs(Dictionary<string, AnalogInput> activeAnalogInputs)
        {
            foreach (string address in activeAnalogInputs.Keys)
            {
                AnalogInput analogInput = dbContext.AnalogInputs.FirstOrDefault(input => input.IOAddress == address);
                if (analogInput == null)
                {
                    throw new Exception("AnalogInput not found.");
                }
                else
                {
                    analogInput.Value = activeAnalogInputs[address].Value;
                }
            }
            dbContext.SaveChanges();
        }

        public void UpdateDigitalInputs(Dictionary<string, DigitalInput> activeDigitalInputs)
        {
            foreach (string address in activeDigitalInputs.Keys) { 

                DigitalInput digitalInput = dbContext.DigitalInputs.FirstOrDefault(input => input.IOAddress == address);
                if (digitalInput == null)
                {
                    throw new Exception("DigitalInput not found.");
                }
                else
                {
                    digitalInput.Value = activeDigitalInputs[address].Value;
                }
            }
            dbContext.SaveChanges();
        }

        public void AddTagRecords(ICollection<TagRecord> tagRecords)
        {
            foreach (TagRecord tagRecord in tagRecords)
            {
                dbContext.TagRecords.Add(tagRecord);
            }
            dbContext.SaveChanges();
        }


    }
}
