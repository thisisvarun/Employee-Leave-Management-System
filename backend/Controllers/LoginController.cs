using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Repositories;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Failed to connect to database!");
            var repository = new LoginRepository(connectionString);
            _loginService = new LoginService(repository);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            var employeeId = _loginService.Login(request);

            if (employeeId == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { message = "Login successful", employeeId });
        }

        [HttpGet("test")]
        public IActionResult Hello()
        {
            return Ok("hello");
        }
    }
}
