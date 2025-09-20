using backend.Models.Enums;

namespace backend.DTOs
{
    public class LoginDTO
    {
        public int EmployeeId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleTitle { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.Employee;
    }
}