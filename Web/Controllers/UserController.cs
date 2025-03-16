using Microsoft.AspNetCore.Mvc;
using Model.Services.Interfaces;

namespace Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase {
        // GET api/user
        [HttpGet]
        public IActionResult GetAllUsers() {
            var users = userService.GetAllUsers();
            if (users.Length == 0) {
                return NotFound("No user found");
            }

            return Ok(users);
        }
    }
}