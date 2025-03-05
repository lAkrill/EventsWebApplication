using EventsWebApplication.Application.Dtos;
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

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken ct = default)
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserByIdAsync(id, ct);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("{id}/events")]
        public async Task<IActionResult> GetEventsByUserId(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserWithEventsByIdAsync(id, ct);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserLoginDto request)
        {
            var result = await _userService.LoginAsync(request);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto, CancellationToken ct = default)
        {
            var user = await _userService.CreateUserAsync(userRegisterDto, ct);
            return Ok(user);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await _userService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto userUpdateDto, CancellationToken ct = default)
        {
            userUpdateDto.Id = id;
            var user = await _userService.UpdateUserAsync(userUpdateDto, ct);
            return Ok(user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct = default)
        {
            await _userService.DeleteUserByIdAsync(id, ct);
            return Ok();
        }
    }
}
