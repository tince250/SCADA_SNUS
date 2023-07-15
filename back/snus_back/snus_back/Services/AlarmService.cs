using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class AlarmService: IAlarmService
    {
        private AlarmRepository allAlarms;

        public AlarmService(AlarmRepository allAlarms) 
        {
            this.allAlarms = allAlarms;
        }


        public ICollection<AlarmDTO> GetAlarmsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return allAlarms.GetAlarmsBetweenDates(startDate, endDate);
        }

        public ICollection<AlarmDTO> GetAlarmsByPriority(AlarmPriority priority)
        {
            return allAlarms.GetAlarmsByPriority(priority);
        }

    }
}
