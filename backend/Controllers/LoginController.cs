using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using backend.Service;

using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LoginController : ControllerBase
    {
        private ApiSevice _apiService;

        public LoginController(ApiSevice apiSevice)
        {
            _apiService = apiSevice;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            string res = _apiService.Login(request);
            return Ok(res);
            // var employee = _context.Employees
            //     .FirstOrDefault(e => e.Email == request.Email);

            // if (employee == null) return Unauthorized("Invalid credentials");

            // if (!VerifyPassword(request.Password, employee.PasswordHash))
            //     return Unauthorized("Invalid credentials");

            // //could generate a JWT token if needed
            // return Ok(new { message = "Login successful", employeeId = employee.Employee_Id });
        }

        [HttpGet]
        public string Hello()
        {
            return "hello";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToBase64String(hashBytes);
            return hash == storedHash;
        }
    }
}
