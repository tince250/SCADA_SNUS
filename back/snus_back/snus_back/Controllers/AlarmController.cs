using Microsoft.AspNetCore.Mvc;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services.ServiceInterfaces;
using System;

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
    }
}
