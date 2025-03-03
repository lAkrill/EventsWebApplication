using EventsWebApplication.Application.Services;
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

        

        public IActionResult Index()
        {
            return View();
        }
    }
}
