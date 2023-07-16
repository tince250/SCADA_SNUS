using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class AlarmService: IAlarmService
    {
        private AlarmRepository allAlarms;
        private TagRepository allTags;
        private ScanService scanService;

        public AlarmService(AlarmRepository allAlarms, TagRepository allTags, ScanService scanService) 
        {
            this.allAlarms = allAlarms;
            this.allTags = allTags;
            this.scanService = scanService;
        }

        public AlarmReturnedDTO AddAlarm(AddAlarmDTO dto)
        {

            AnalogInput tag = this.allTags.GetAnalogInputById(dto.TagId);
            if (tag == null)
            {
                throw new Exception("There is no tag with given id");
            }

            Alarm newAlarm = new Alarm
            {
                Priority = Enum.Parse<AlarmPriority>(dto.Priority),
                Type = Enum.Parse<AlarmType>(dto.Type),
                Value = dto.Value
            };

            Alarm ret = this.allAlarms.AddAlarm(newAlarm);
            this.allTags.AddAlarmToTag(ret, tag.Id);

            //TODO: oktomentarisi kad spojis
            //this.scanService.AddNewAlarm(ret, tag.IOAddress);

            return new AlarmReturnedDTO
            {
                Id = ret.Id,
                Priority = ret.Priority.ToString(),
                Type = ret.Type.ToString(),
                Value = ret.Value,
                TagId = tag.Id
            };
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
