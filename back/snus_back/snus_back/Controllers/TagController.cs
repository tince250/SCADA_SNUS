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
    }
}
