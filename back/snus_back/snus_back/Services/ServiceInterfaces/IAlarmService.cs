using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Services.ServiceInterfaces
{
    public interface IAlarmService
    {
        public ICollection<AlarmDTO> GetAlarmsBetweenDates(DateTime startDate, DateTime endDate);

        public ICollection<AlarmDTO> GetAlarmsByPriority(AlarmPriority priority);
        public AlarmReturnedDTO AddAlarm(AddAlarmDTO dto);
        public List<AlarmReturnedDTO> GetAlarmsForTag(int tagId);
        public void DeleteAlarm(int id, int tagId);
    }
}
