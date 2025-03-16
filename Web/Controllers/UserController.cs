using Microsoft.AspNetCore.Mvc;
using Model.Services.Interfaces;

namespace API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() {
            var users = await _userService.GetAllUsersAsync();
            if (users.Length == 0) {
                return NotFound("No user found");
            }

            return Ok(users);
        }
    }
}