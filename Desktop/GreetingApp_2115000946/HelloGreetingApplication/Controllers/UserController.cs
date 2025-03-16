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
            var result = _userBL.Register(user);
            if (result)
                return Ok(new { message = "User registered successfully" });
            return BadRequest(new { message = "Registration failed" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var result = _userBL.Login(login.Email, login.Password);
            if (result == "Login Successful")
                return Ok(new { message = result });
            return Unauthorized(new { message = "Invalid Credentials" });
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
