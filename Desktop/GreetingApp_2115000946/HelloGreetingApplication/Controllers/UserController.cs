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
        private readonly IEmailService _emailService;


        public UserController(IUserBL userBL, IEmailService emailService)
        {
            _userBL = userBL;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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



        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordModel model)
        {
            var user = _userBL.GetUserByEmail(model.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Generate a JWT Token for password reset
            var token = _userBL.GeneratePasswordResetToken(user.Email);

            // Send email with the reset link
            var resetLink = $"https://localhost:7150/reset-password?token={token}";
            var emailBody = $"Click <a href='{resetLink}'>here</a> to reset your password.";

            bool isMailSent = await _emailService.SendEmailAsync(user.Email, "Password Reset Request", emailBody);

            if (isMailSent)
                return Ok(new { message = "Password reset link sent to email." });
            else
                return StatusCode(500, new { message = "Error sending email." });
        }


        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            var email = _userBL.ValidateResetToken(model.Token);
            if (email == null)
            {
                return BadRequest(new { message = "Invalid or expired token." });
            }

            bool isUpdated = _userBL.ResetPassword(email, model.NewPassword);
            if (!isUpdated)
            {
                return BadRequest(new { message = "Password reset failed." });
            }

            return Ok(new { message = "Password reset successfully." });
        }

    }
}
