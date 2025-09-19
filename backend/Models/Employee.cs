using System;

namespace backend.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
<<<<<<< HEAD
        public string? Phone { get; set; }
=======
        public string Phone { get; set; } = string.Empty;
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
        public int? Team_Id { get; set; }
        public int Salary { get; set; }
        public int DesignationId { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool Active { get; set; }
<<<<<<< HEAD
        public string PasswordHash { get; set; } = string.Empty;
=======
<<<<<<< Updated upstream

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [ForeignKey("Team_Id")]
        public Team? Team { get; set; }

        [ForeignKey("Designation_Id")]
        public Designation? Designation { get; set; }

        public ICollection<Leave>? Leaves { get; set; } = new List<Leave>();
=======
        public string Password_Hash { get; set; } = string.Empty;
>>>>>>> Stashed changes
>>>>>>> d089c4ad28b7152ed3a567d57fb70a8ececd3a28
    }
}