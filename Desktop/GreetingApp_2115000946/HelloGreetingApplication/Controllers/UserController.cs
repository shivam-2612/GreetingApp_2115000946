using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            var token = _userBL.Register(user);
            if (token == "Registration failed")
                return BadRequest(new { message = "User registration failed" });

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var token = _userBL.Login(login.Email, login.Password);
            if (token != null)
                return Ok(new { Token = token });

            return Unauthorized(new { message = "Invalid Credentials" });
        }
    }
}
