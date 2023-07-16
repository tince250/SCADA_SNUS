using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Repositories
{
    public class AlarmRepository
    {
        private SNUSDbContext dbContext;
        public AlarmRepository(SNUSDbContext context)
        {
            this.dbContext = context;
        }

        public ICollection<AlarmDTO> GetAlarmsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var alarmRecords = dbContext.AlarmRecords
                .Where(ar => ar.Timestamp >= startDate && ar.Timestamp <= endDate)
                .ToList();

            return generateRetCollection(alarmRecords);
        }

        public ICollection<AlarmDTO> GetAlarmsByPriority(AlarmPriority priority)
        {
            var alarmRecords = dbContext.AlarmRecords
                .Where(ar => ar.Alarm.Priority == priority)
                .ToList();

            return generateRetCollection(alarmRecords);
        }

        private ICollection<AlarmDTO> generateRetCollection(List<AlarmRecord> alarmRecords)
        {
            ICollection<AlarmDTO> ret = new List<AlarmDTO>();
            foreach (var alarm in alarmRecords)
            {
                ret.Add(new AlarmDTO(alarm));
            }

            return ret;
        }

        public void AddAlarmRecords(ICollection<AlarmRecord> alarmRecords)
        {
            foreach (AlarmRecord alarmRecord in alarmRecords)
            {
                dbContext.AlarmRecords.Add(alarmRecord);
            }
            dbContext.SaveChanges();
        }

        public Alarm AddAlarm(Alarm alarm)
        {
            this.dbContext.Alarms.Add(alarm);
            this.dbContext.SaveChanges();
            return alarm;
        }

        public Alarm GetById(int id)
        {
            Alarm alarm = this.dbContext.Alarms.Find(id);
            if (alarm == null)
                throw new Exception("Alar with given id doesn't exist.");
            return alarm;
        }

        internal void DeleteAlarm(Alarm alarm)
        {
            
            this.dbContext.Alarms.Remove(alarm);
            this.dbContext.SaveChanges();

        }
    }
}
