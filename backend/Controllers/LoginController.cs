using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using backend.DTOs;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly string _connectionString;

        public LoginController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new Exception("Failed to connect to database!");
            }
        }

        [HttpPost("/login/")]
        public IActionResult Login([FromBody] LoginDTO request)
        {
<<<<<<< Updated upstream
            string res = _apiService.Login(request);

            // We should probably create one
            // Currently, I'm testing somethings;
            string JWT = "TIHS_IS_A_SUPER_SECRET_JWT_TOKEN!";
            Response.Cookies.Append("jwt_token", JWT, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return Ok(res);
            // var employee = _context.Employees
            //     .FirstOrDefault(e => e.Email == request.Email);
=======
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and Password are required");
            }

            using SqlConnection conn = new(_connectionString);
            conn.Open();
            SqlCommand cmd = new("SELECT Employee_Id, Password_Hash FROM Employee WHERE Email=@Email", conn);
            cmd.Parameters.AddWithValue("@Email", request.Email);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return Unauthorized("Invalid credentials");
            }

            int employeeId = (int)reader["Employee_Id"];
            string storedHash = reader["Password_Hash"].ToString() ?? "";

            if (!VerifyPassword(request.Password, storedHash))
            {
                return Unauthorized("Invalid credentials");
            }

            // TODO: Replace with real JWT generation
            string jwt = "THIS_IS_A_SUPER_SECRET_JWT_TOKEN!";
            Response.Cookies.Append("jwt_token", jwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
>>>>>>> Stashed changes

            return Ok(new { message = "Login successful", employeeId });
        }

        [HttpGet("/test/")]
        public IActionResult Hello()
        {
            return Ok("hello");
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToBase64String(hashBytes);
            return hash == storedHash;
        }
    }
}