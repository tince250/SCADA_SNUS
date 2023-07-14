using Microsoft.AspNetCore.Mvc;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : Controller
    {
        private ITagService tagService;

        public TagController(ITagService tagService) 
        {
            this.tagService = tagService;
        }

        [HttpGet]
        [Route("{address}")]
        public ActionResult GetAllTagsByIOAddress(string address)
        {
            try
            {
                ICollection<TagRecordDTO> ret = this.tagService.getAllTagByIOAddress(address);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAllTags()
        {
            try
            {
                ICollection<TagRecordDTO> ret = this.tagService.getAllTagRecords();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("output-dbm")]
        public ActionResult GetAllOutputTagsDBManager()
        {
            try
            {
                ICollection<OutputTagDBManagerDTO> ret = this.tagService.GetAllOutputTagsDBManager();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPut]
        [Route("digital-output-value/{id}")]
        public ActionResult UpdateDigitalOutput(int id, [FromQuery] string value)
        {
            try
            {
                double doubleValue;
                Double.TryParse(value, out doubleValue);
                this.tagService.UpdateDigitalOutputValue(id, doubleValue);
                return Ok("Successfully updated tag with id: " + id);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPut]
        [Route("analog-output-value/{id}")]
        public ActionResult UpdateAnalogOutput(int id, [FromQuery] string value)
        {
            try
            {
                double doubleValue;
                Double.TryParse(value, out doubleValue);
                this.tagService.UpdateAnalogOutputValue(id, doubleValue);
                return Ok("Successfully updated tag with id: " + id);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("digital-output/{id}")]
        public ActionResult DeleteDigitalOutput(int id)
        {
            try
            {
                this.tagService.DeleteDigitalOutput(id);
                return Ok("Successfully deleted tag with id: " + id);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("analog-output/{id}")]
        public ActionResult DeleteAnalogOutput(int id)
        {
            try
            {
                this.tagService.DeleteAnalogOutput(id);
                return Ok("Successfully deleted tag with id: " + id);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
