using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var employee = _context.Employees
                .FirstOrDefault(e => e.Email == request.Email);

            if (employee == null) return Unauthorized("Invalid credentials");

            if (!VerifyPassword(request.Password, employee.PasswordHash))
                return Unauthorized("Invalid credentials");

            //could generate a JWT token if needed
            return Ok(new { message = "Login successful", employeeId = employee.Employee_Id });
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToBase64String(hashBytes);
            return hash == storedHash;
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
