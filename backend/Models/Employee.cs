using System;

namespace backend.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? Team_Id { get; set; }
        public int Salary { get; set; }
        public int DesignationId { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool Active { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        
        public Team? Team { get; set; }

    
        public Designation? Designation { get; set; }

        public ICollection<Leave>? Leaves { get; set; } = new List<Leave>();

        public string Password_Hash { get; set; } = string.Empty;

    }
}