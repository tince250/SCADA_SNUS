﻿using snus_back.data_access;
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
    }
}