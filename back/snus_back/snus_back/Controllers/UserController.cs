using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService) 
        {
            this.userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Login(CredentialsDTO creds)
        {
            try
            {
                User user = this.userService.Login(creds);
                return Ok(new UserDTO(user.Id, user.Username, user.Name, user.LastName, user.Role.ToString()));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
