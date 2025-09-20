using Microsoft.AspNetCore.Mvc;
using backend.Service.Interfaces;
using backend.DTOs;
using backend.Common;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public LoginController(IApiService apiService, IEmailService emailService, IConfiguration configuration)
        {
            _apiService = apiService;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            LoginDTO res = _apiService.Login(request);

            if (string.IsNullOrEmpty(res.Email))
            {
                return Unauthorized();
            }

            var token = JwtHelper.GenerateToken(
                res.Email,
                res.EmployeeId,
                res.RoleTitle,
                _configuration["Jwt:Key"]!,
                _configuration["Jwt:Issuer"]!,
                _configuration["Jwt:Audience"]!
            );

            Response.Cookies.Append("jwt_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return Ok(new { Token = token, EmployeeId = res.EmployeeId, RoleTitle = res.RoleTitle, Role = res.Role.ToString() });
        }

        [HttpGet("test")]
        public async Task<IActionResult> Hello()
        {
            bool done = await _emailService.SendEmailAsync("harish.16633@gmail.com", "Test", "Test");
            if (done)
            {
                return Ok("Hello");
            }
            else
            {
                return Ok("Not ok!");
            }
        }
    }
}
