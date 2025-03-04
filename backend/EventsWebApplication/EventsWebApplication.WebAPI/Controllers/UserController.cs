using EventsWebApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApplication.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken ct = default)
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserByIdAsync(id, ct);
            return Ok(user);
        }

        [HttpGet("{id}/events")]
        public async Task<IActionResult> GetEventsByUserId(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserWithEventsByIdAsync(id, ct);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto, CancellationToken ct = default)
        {
            var user = await _userService.CreateUserAsync(userRegisterDto, ct);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto userUpdateDto, CancellationToken ct = default)
        {
            userUpdateDto.Id = id;
            var user = await _userService.UpdateUserAsync(userUpdateDto, ct);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct = default)
        {
            await _userService.DeleteUserByIdAsync(id, ct);
            return Ok();
        }
    }
}
