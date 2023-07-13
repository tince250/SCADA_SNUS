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

        public void UpdateAnalogInput(string address, double value)
        {
            AnalogInput analogInput = dbContext.AnalogInputs.FirstOrDefault(input => input.IOAddress == address);
            if (analogInput == null)
            {
                throw new Exception("AnalogInput not found.");
            }
            else
            {
                analogInput.Value = value;
            }
            dbContext.SaveChanges();
        }

        public void UpdateDigitalInput(string address, double value)
        {
            DigitalInput digitalInput = dbContext.DigitalInputs.FirstOrDefault(input => input.IOAddress == address);
            if (digitalInput == null)
            {
                throw new Exception("DigitalInput not found.");
            }
            else
            {
                digitalInput.Value = value;
            }
            dbContext.SaveChanges();
        }
    }
}
