using Microsoft.AspNetCore.Mvc;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services.ServiceInterfaces;
using System;
using System.Text.Json;

namespace snus_back.Controllers
{
    [ApiController]
    [Route("api/alarm")]
    public class AlarmController: Controller
    {
        private IAlarmService alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            this.alarmService = alarmService;
        }

        [HttpPost]
        [Route("between/dates")]
        public ActionResult GetAllAlarmsBetweenDates(DateRangeDTO dto)
        {
            try
            {
                ICollection<AlarmDTO> ret = this.alarmService.GetAlarmsBetweenDates(dto.StartTime, dto.EndTime);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("{priorityStr}")]
        public ActionResult GetAllAlarmsByPriority(string priorityStr)
        {
            try
            {
                AlarmPriority priority = (AlarmPriority)Enum.Parse(typeof(AlarmPriority), priorityStr);
                ICollection<AlarmDTO> ret = this.alarmService.GetAlarmsByPriority(priority);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult AddAlarm(AddAlarmDTO dto)
        {
            try
            {
                Console.WriteLine(JsonSerializer.Serialize(dto));
                AlarmReturnedDTO ret = this.alarmService.AddAlarm(dto);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        public ActionResult GetAlarmsForTag([FromQuery] int tagId)
        {
            try
            {
                Console.WriteLine(tagId);
                List<AlarmReturnedDTO> ret = this.alarmService.GetAlarmsForTag(tagId);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteAlarm(int id, [FromQuery] int tagId)
        {
            try
            {
                this.alarmService.DeleteAlarm(id, tagId);
                return Ok(new { Message = "Alarm deleted successfully." });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
