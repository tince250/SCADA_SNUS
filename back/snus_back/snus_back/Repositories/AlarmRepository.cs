using snus_back.data_access;
using snus_back.Models;

namespace snus_back.Repositories
{
    public class AlarmRepository
    {
        private SNUSDbContext dbContext;

        public AlarmRepository(SNUSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddAlarmRecords(ICollection<AlarmRecord> alarmRecords)
        {
            foreach (AlarmRecord alarmRecord in alarmRecords)
            {
                dbContext.AlarmRecords.Add(alarmRecord);
            }
            dbContext.SaveChanges();
        }
    }
}
