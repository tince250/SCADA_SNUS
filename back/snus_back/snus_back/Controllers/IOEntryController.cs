using Microsoft.AspNetCore.Mvc;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Controllers
{
    [ApiController]
    [Route("api/ioentries")]
    public class IOEntryController : Controller
    {
        private IIOEntryService IOEntryService;

        public IOEntryController(IIOEntryService iOEntryService)
        {
            this.IOEntryService = iOEntryService;
        }

        [HttpPut]
        public ActionResult UpdateEntries(List<IOEntryDTO> newEntries)
        {
            Console.WriteLine("tu");
            foreach (var entry in newEntries)
                Console.WriteLine(entry.Value.ToString() + entry.IOAddress.ToString());
            try
            {
                this.IOEntryService.BatchUpdate(newEntries);
                return Ok("Entry values updated successfully.");
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("free")]
        public ActionResult GetFreeAddresses()
        {
            try
            {
                return Ok(this.IOEntryService.GetFreeAdresses());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("free/output")]
        public ActionResult GetFreeOutputAddresses()
        {
            try
            {
                return Ok(this.IOEntryService.GetFreeOutputAdresses());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
