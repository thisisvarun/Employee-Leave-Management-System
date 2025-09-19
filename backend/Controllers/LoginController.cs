using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using backend.Service;

using backend.DTOs;
using backend.Common;

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
            Console.WriteLine("[LOGIN CONTROLLER] " + request.Email + " " + request.Password);
            LoginDTO res = _apiService.Login(request);

            Response.Cookies.Append("jwt_token",
            JwtHelper.GenerateToken(request.Email,
                "secretKey, i dont know what to put, but it will be changed after sometime",
                "secretKey i dont know what to put, but it will be changed after sometime",
                "audience, This is also another string that needs to be changed as well"
            ),
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            // TODO: Need to fetch the role here as well
            return Ok(new LoginResponseDTO
            {
                RefreshToken = JwtHelper.GenerateToken(
                    request.Email,
                    "secretKey i dont know what to put, but it will be changed after sometime",
                    "secretKey i dont know what to put, but it will be changed after sometime",
                    "audience, This is also another string that needs to be changed as well"
                ),
                Email = request.Email,
                Password = request.Password,
                EmployeeId = res.EmployeeId,
                // TODO: Hardcoding Employee for now
                // Ideally, the role should be coming from the DB
                Role = "Employee"
            });

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
