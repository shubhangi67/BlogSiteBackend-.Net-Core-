using BlogSite.Service;
using BlogSite.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateViewModel input)
        {
            await _userService.CreateUserAsync(input);
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var User = await _userService.GetUserAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var User = await _userService.GetAllUserAsync();
            return Ok(User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateViewModel input)
        {
            try
            {
                await _userService.UpdateUserAsync(id, input);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}